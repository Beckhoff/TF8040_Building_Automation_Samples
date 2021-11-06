using System;
using System.Diagnostics;
using Beckhoff.BA.TerminalClient.Api;
using Beckhoff.BA.TerminalClient.Tc3;

namespace S01_ReadValues
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
                    // Get variable from object and read current value:
                    if (!bObject.PlcVariables.TryGetValue(bVariableID, out var bVariable))
                        Console.WriteLine(string.Format("Variable '{0}~{1}' not found!", sObjectPath, bVariableID));
                    else
                    {
                        // Read variable value:
                        bool _bRepeat;
                        do
                        {
                            _bRepeat = !bVariable.ReadValue();
                            if (_bRepeat)
                                Console.WriteLine(string.Format("Failed to read variable '{0}~{1}'!", sObjectPath, bVariableID));
                            else
                            {
                                Console.WriteLine(string.Format("Value of variable '{0}~{1}': {2}", sObjectPath, bVariableID, bVariable.Value));

                                Console.WriteLine("Press [enter] to read again...");
                                _bRepeat = (Console.ReadKey().Key == ConsoleKey.Enter);
                            }

                        } while (_bRepeat);
                    }
                }
            }

            // Disconnect from site:
            BaApi.BaSite.Connection = false;
            Debug.Assert(!BaApi.BaSite.Connection);

            Console.WriteLine();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }


        #region Events
        private static void OnLog(BaApi.BaLogType bIcon, string sCode, object oEvent, string sProcess = "")
        {
            Console.WriteLine(oEvent.ToString());
        }
        #endregion
    }
}
