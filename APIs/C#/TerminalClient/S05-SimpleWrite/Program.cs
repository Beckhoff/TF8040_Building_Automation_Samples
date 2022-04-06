using System;
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
        private static Tc3_BA2.BaVariableID VariableId = Tc3_BA2.BaVariableID.eValueRm;
        #endregion


        static async Task Main(string[] args)
        {
            // Initialize:
            BaSite.OnLog += OnLog;

            // Establish site connection:
            BaSite.AddDevice(DevNetID);

            await BaSite.ConnectAsync();
            {
                // Find object:
                var iObj = BaSite.FindObject(ObjectPath);
                if (iObj == null)
                    Console.WriteLine("Object '{0}' not found!", ObjectPath);
                else
                {
                    var iVar = iObj.StandardParameters[VariableId];

                    Console.WriteLine("\nSample 1) Write primitive variable value:");
                    if (iVar.Value is IBaPrimitiveValue iVal)
                    {
                        // Sample 1.1) Set (type dependant) default value:
                        iVal.SetValue(Activator.CreateInstance(iVal.DataType));

                        // Sample 1.2) Set value:
                        if (iVar.Value is IBaPrimitiveValue<float> iAVal)
                            iAVal.Primitive = 10.5F;
                        else if (iVar.Value is IBaPrimitiveValue<bool> iBVal)
                            iBVal.Primitive = true;
                        else if (iVar.Value is IBaPrimitiveValue<uint> iMVal)
                            iMVal.Primitive = 3;
                        else
                            ; // Not handled yet.

                        // Write value:
                        await iVal.WriteAsync();
                        Console.WriteLine("Written value: {0}", iVar.Value);
                    }
                    else
                        Console.WriteLine("<Non-primitive values not handled yet>");
                }
            }

            // Disconnect from site:
            await BaSite.DisconnectAsync();

            Console.WriteLine();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }


        #region Events
        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "")
        {
            Console.WriteLine(oEvent.ToString());
        }
        #endregion
    }
}
