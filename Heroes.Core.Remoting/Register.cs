using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Heroes.Core.Remoting
{
    public class SylRegister : IDisposable
    {
        public const string CLASSNAME = "SylLib.SylRemoting.SylRegister";

        public string _protocol;
        public string _hostName;
        public int _port;

        public SylRegister()
        {
            _protocol = "tcp";
            _hostName = "localhost";
            _port = 8086;
        }

        public string Protocol
        {
            get { return _protocol; }
            set { _protocol = value; }
        }

        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public bool GetSetting()
        {
            _protocol = System.Configuration.ConfigurationManager.AppSettings["Remote_Protocol"];
            _hostName = System.Configuration.ConfigurationManager.AppSettings["Remote_HostName"];
            _port = System.Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Remote_Port"]);

            if (_protocol.Length < 1)
            {
                return false;
            }

            if (_hostName.Length < 1)
            {
                return false;
            }

            if (_port < 1)
            {
                return false;
            }

            return true;
        }

        public void RegisterChannel()
        {
            if (ChannelServices.RegisteredChannels.Length < 1)
                ChannelServices.RegisterChannel(new TcpClientChannel(), false);
        }

        public virtual void RegisterServer()
        {
            TcpServerChannel channel = new TcpServerChannel(_port);
            ChannelServices.RegisterChannel(channel, false);
        }

        public void ListServices()
        {
            WellKnownServiceTypeEntry[] serviceTypes
                = RemotingConfiguration.GetRegisteredWellKnownServiceTypes();
            foreach (WellKnownServiceTypeEntry serviceType in serviceTypes)
            {
                Console.WriteLine("Service URI = '{0}'", serviceType.ObjectUri);
            }
        }

        public string GetUrl(string uri)
        {
            return string.Format("{0}://{1}:{2}/{3}", _protocol, _hostName, _port, uri);
        }

        public object GetObject(Type type, string uri)
        {
            object obj = null;
            try
            {
                obj = Activator.GetObject(type, GetUrl(uri));
                if (obj == null)
                {
                    throw new Exception("CANNOT get remoting object.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return obj;
        }

        #region Write Info
        public static void ConsoleWriteInfo(string mn, string state)
        {
            Console.WriteLine(string.Format("{0:HH:mm:ss} {1} {2}", DateTime.Now, state, mn));
        }

        public static void ConsoleWriteInfo(string mn)
        {
            ConsoleWriteInfo(mn, "START");
        }
        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            if (ChannelServices.RegisteredChannels.Length > 0)
            {
                IChannel channel = ChannelServices.RegisteredChannels[0];
                ChannelServices.UnregisterChannel(channel);
            }
        }

        #endregion
    }
}
