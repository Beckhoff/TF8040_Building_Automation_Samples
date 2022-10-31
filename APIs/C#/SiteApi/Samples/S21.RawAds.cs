using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;


namespace Beckhoff.BA.SiteApi.Samples
{
    public class Sample21 : IExecutableSample
    {
        #region Settings
        /// <summary>
        /// Some symbolpath to be tested.
        /// </summary>
        public string SymbolPath = "MAIN.someSymbol";
        /// <summary>
        /// Variable to write.
        /// </summary>
        public Tc3_BA2.BaParameterId VariableId = Tc3_BA2.BaParameterId.ePresentValue;
        #endregion


        public async Task Run()
        {
            var iDevice = BaSite.Devices.First();

            // Do some ADS communication:
            Console.WriteLine("\nSample 1.1) Read some symbol's value:");
            Console.WriteLine(string.Format("\n{0}: {1}", SymbolPath, iDevice.Client.ReadValue<int>(SymbolPath)));

            // Sample 1.2) Write some symbol's value:
            iDevice.Client.WriteValue(SymbolPath, 100);
        }
    }
}
