using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;
using TwinCAT.BA.Tc3_XBA;


namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates simple write operations.
    /// </summary>
    public class Sample05 : IExecutableSample
    {
        #region Settings
        /// <summary>
        /// Symbolpath of object to be tested.
        /// </summary>
        public string ObjectPath = "MyPlcProject.MAIN.AnalogValue";
        /// <summary>
        /// Variable to write.
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

            Console.WriteLine($"\nSample 2) Write '{VariableId}' variables (operational objects only).");
            var iDevice = BaSite.Devices.First();
            var iObjects = iDevice.ObjectTable.Values.Where(iObj => (iObj.Purpose == BaObjectPurpose.eOperation));
            var iCommand = BaSumCommandFactory.Create<IBaWriteSumCommand>(iObjects, VariableId);

            foreach (var iVar in iCommand.Variables)
            {
                // Do something...
                // iVar.Value.SetValue();
            }
            await iCommand.WriteAsync();
        }
    }
}