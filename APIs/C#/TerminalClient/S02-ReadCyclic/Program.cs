using System;
using System.Linq;
using System.Collections.Generic;
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
        /// <summary>
        /// Symbolpath of object to be tested.
        /// </summary>
        private static string ObjectPath = "MyPlcProject.MAIN.AnalogValue";
        /// <summary>
        /// Variable to read.
        /// </summary>
        private static Tc3_BA2.BaPlcVariableID VariableId = Tc3_BA2.BaPlcVariableID.ePresentValue;
        #endregion


        static async Task Main(string[] args)
        {
            // Initialize:
            BaSite.OnLog += OnLog;
            BaSite.OnPostReadCycle += OnPostReadCycle;

            // Add devices to site:
            BaSite.AddDevice(DevNetID);

            // Establish site connection:
            await BaSite.ConnectAsync();
            {
                // Find object:
                var iObj = BaSite.FindObject(ObjectPath);
                if (iObj == null)
                    Console.WriteLine("Unable to find '{0}'!", ObjectPath);
                else
                    // Add job to cyclically read a variable's value:
                    await iObj.Device.AddReadJobAsync_Cyclic(new BaSumCommand(iObj, false, (_bPlcVar) => (_bPlcVar.ID == VariableId)));

                Console.WriteLine("Press key to exit...");
                Console.WriteLine();
                Console.ReadKey();
            }

            // Disconnect from site:
            await BaSite.DisconnectAsync();
        }


        #region Events
        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "")
        {
            Console.WriteLine(oEvent.ToString());
        }
        private static void OnPostReadCycle(IReadOnlyDictionary<IBaBaseDevice, BaReadJobList> iDoneJobs)
        {
            // Iterate over read devices:
            foreach (var _iDev in iDoneJobs)
            {
                // Iterate over executed read jobs:
                foreach (var _iJob in _iDev.Value)
                {
                    // Display all read variable values:
                    foreach (var _iVar in _iJob.Command.Variables)
                        Console.WriteLine("Value of variable '{0}': {1}", _iVar.Symbol.InstancePath, _iVar.Value);
                }
            }
        }
        #endregion
    }
}
