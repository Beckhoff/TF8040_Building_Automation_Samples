using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;

using static TwinCAT.BA.BaApi;


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
        private static Tc3_BA2.BaParameterId VariableId = Tc3_BA2.BaParameterId.ePresentValue;
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

                    // Read variable and dependencies:
                    await iVar.ReadValueAsync();
                    await BaSumCommandFactory.CreateFromDependencies(iVar).ReadAsync();

                    Console.WriteLine("\nSample 1.1) Format variable value:");
                    Console.WriteLine("{0}: {1}", iVar.Parent.SymbolPath, iVar.Value);

                    Console.WriteLine("\nSample 1.2) Show typed variable value:");
                    if (iVar.Value is IBaPrimitiveValue<float> iAVal)
                        Console.WriteLine("Some analog value: {0}", iAVal.Primitive);
                    else if (iVar.Value is IBaPrimitiveValue<bool> iBVal)
                        Console.WriteLine("Some binary value: {0}", iBVal.Primitive);
                    else if (iVar.Value is IBaPrimitiveValue<uint> iMVal)
                        Console.WriteLine("Some multistate value: {0}", iMVal.Primitive);
                    else
                        Console.WriteLine("<Not handled yet>");
                }

                Console.WriteLine($"\nSample 2) Read all '{VariableId}' variables:");
                var iDevice = BaSite.Devices.First();
                var iCommand = BaSumCommandFactory.Create<IBaReadSumCommand>(iDevice.ProjectStructure, VariableId);
                if (await iCommand.ReadAsync())
                {
                    await BaSumCommandFactory.CreateFromDependencies(iCommand).ReadAsync();

                    // List variables:
                    foreach (var iVar in iCommand.Variables)
                        Console.WriteLine($"- {iVar.InstancePath}: {iVar}");
                }
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
