using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Heroes.Core.Remoting
{
    public class RegisterServer : SylRegister
    {
        public new const string CLASSNAME = "SPMSRemoteLib.Register";

        public RegisterServer()
        {
            base._protocol = "tcp";
            base._hostName = "localhost";
            base._port = 4952;
        }

        public void RegisterServices()
        {
            // Function
            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(Game),
                Game.CLASSNAME,
                WellKnownObjectMode.SingleCall);
        }

    }
}
