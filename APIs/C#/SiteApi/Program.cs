using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;

using static TwinCAT.BA.BaApi;


namespace Beckhoff.BA.SiteApi.Samples
{
    public class Program : Loader
    {
        #region Settings
        /// <summary>
        /// Sample instance to load.
        /// </summary>
        /// <remarks>Feel free to change to any other sample like <see cref="Sample02"/>, <see cref="Sample05"/>, etc.</remarks>
        public static ISample TestSample = new Sample01();
        /// <summary>
        /// AMS NetID of device to be used for testing purposes.
        /// </summary>
        public static string TestDevice = "127.0.0.1.1.1:851";
        #endregion


        static async Task Main(string[] args)
        {
            // Initialize:
            BaSite.OnLog += OnLog;

            // Run selected sample:
            await RunSampleAsync(TestDevice, TestSample);

            Console.WriteLine();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }


        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "", IBaLog iContext = null)
        {
            Console.WriteLine(oEvent.ToString());
        }
    }
}
