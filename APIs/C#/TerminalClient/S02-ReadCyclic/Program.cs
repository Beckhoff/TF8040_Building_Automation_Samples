using System;
using System.Diagnostics;
using Beckhoff.BA.TerminalClient.Api;
using Beckhoff.BA.TerminalClient.Tc3;

namespace S02_ReadCyclic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Settings:
            var sDevNetID = "127.0.0.1.1.1:851";
            var sObjectPath = "MyPlcProject.MAIN.AnalogValue";
            var bVariableID = Tc3_BA2.BaPlcVariableID.ePresentValue;

            // Initialize:
            BaApi.BaSite.OnLog += OnLog;
            BaApi.BaSite.OnReadCyclic += OnCyclicRead;

            // Add device(s) to site:
            BaApi.BaSite.AddDevice(sDevNetID);

            // Establish site connection:
            BaApi.BaSite.Connection = true;
            if (BaApi.BaSite.Connection)
            {
                // Find object:
                var bObject = BaApi.BaSite.FindObject(sObjectPath);
                if (bObject == null)
                    Console.WriteLine(string.Format("Object '{0}' not found!", sObjectPath));
                else
                {
                    // Add job to cyclically read a variable's value:
                    BaApi.BaSite.Devices[0].AddReadJob_Cyclic(new BaApi.BaSumCommand(bObject, false, (_bPlcVar) => (_bPlcVar.ID == bVariableID)));

                    Console.WriteLine("Press key to exit...");
                    Console.WriteLine();
                    Console.ReadKey();
                }

                // Disconnect from site:
                BaApi.BaSite.Connection = false;
                Debug.Assert(!BaApi.BaSite.Connection);
            }
        }


        #region Events
        private static void OnLog(BaApi.BaLogType bIcon, string sCode, object oEvent, string sProcess = "")
        {
            Console.WriteLine(oEvent.ToString());
        }
        private static void OnCyclicRead(BaApi.BaSumCommand sCommand, Object oTag)
        {
            // Display all read variable values:
            foreach (BaApi.BaPlcVariable _bVar in sCommand.Variables)
                Console.WriteLine(string.Format("Value of variable '{0}': {1}", _bVar.Symbol.InstancePath, _bVar.Value));
        }
        #endregion
    }
}
