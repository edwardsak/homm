using System;
using System.Collections.Generic;
using System.Text;

namespace HeroesServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Heroes Remoting Server");
                Console.WriteLine();

                Console.WriteLine("Get Settings...");
                string appStartupPath 
                    = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                if (!Heroes.Core.Setting.GetSettings(appStartupPath))
                {
                    Console.WriteLine("Error");
                    System.Console.WriteLine("Press return to exit.");
                    System.Console.ReadLine();
                    return;
                }

                Console.WriteLine("Get remoting settings...");
                Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
                if (!register.GetSetting())
                {
                    Console.WriteLine("Error");
                    System.Console.WriteLine("Press return to exit.");
                    System.Console.ReadLine();
                    return;
                }
                Console.WriteLine("URL = '{0}'", register.GetUrl(""));
                Console.WriteLine();

                Console.WriteLine("Register server...");
                register.RegisterServer();
                Console.WriteLine();

                Console.WriteLine("Register services...");
                register.RegisterServices();
                Console.WriteLine();

                Console.WriteLine("Services Available:");
                register.ListServices();
                Console.WriteLine();

                Console.WriteLine("Server started.");

                string s = "";
                while (true)
                {
                    System.Console.WriteLine("Type quit to exit.");
                    s = System.Console.ReadLine();
                    if (string.Compare(s, "quit", true) == 0)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                System.Console.ReadLine();
            }
        }
    }
}
