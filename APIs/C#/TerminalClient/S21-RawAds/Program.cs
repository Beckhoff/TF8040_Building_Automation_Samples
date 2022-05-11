using System;
using System.Linq;
using System.Threading.Tasks;
using Beckhoff.BA.TerminalClient.Api;
using Beckhoff.BA.TerminalClient.Api.Site;

using static Beckhoff.BA.TerminalClient.Api.BaApi;


namespace Beckhoff.BA.TerminalClient.Samples
{
    class Program
    {
        #region Settings
        /// <summary>
        /// AMS-NetID of device to connect to.
        /// </summary>
        private static string DevNetID = "127.0.0.1.1.1:851";
        #endregion


        static async Task Main(string[] args)
        {
            // Initialize:
            BaSite.OnLog += OnLog;

            // Establish site connection:
            BaSite.AddDevice(DevNetID);

            await BaSite.ConnectAsync();
            {
                var iDevice = BaSite.Devices.First();

                // Do some ADS communication.

                Console.WriteLine("\nSample 1.1) Read some symbol's value:");
                string _sSymbol = "MAIN.someSymbol";
                Console.WriteLine(string.Format("\n{0}: {1}", _sSymbol, iDevice.Client.ReadValue<int>(_sSymbol)));

                // Sample 1.2) Write some symbol's value:
                iDevice.Client.WriteValue("MAIN.someSymbol", 100);
            }

            // Disconnect from site:
            await BaSite.DisconnectAsync();

            Console.WriteLine();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }


        #region Events
        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "", IBaLog iContext = null)
        {
            Console.WriteLine(oEvent.ToString());
        }
        #endregion
    }
}
