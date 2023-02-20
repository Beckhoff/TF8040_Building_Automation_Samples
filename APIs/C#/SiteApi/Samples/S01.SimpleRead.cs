using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;
using TwinCAT.BA.Tc3_XBA;


namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates simple read operations.
    /// </summary>
    public class Sample01 : IExecutableSample
    {
        #region Settings
        /// <summary>
        /// Symbolpath of object to be tested.
        /// </summary>
        public string ObjectPath = "MyPlcProject.MAIN.AnalogValue";
        /// <summary>
        /// Parameter to read.
        /// </summary>
        public BaParameterId VariableId = BaParameterId.ePresentValue;
        #endregion


        public async Task Run()
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
            if ((await iCommand.ReadAsync()).Succeeded)
            {
                await BaSumCommandFactory.CreateFromDependencies(iCommand).ReadAsync();

                // List variables:
                foreach (var iVar in iCommand.Variables)
                    Console.WriteLine($"- {iVar.InstancePath}: {iVar}");
            }
        }
    }
}