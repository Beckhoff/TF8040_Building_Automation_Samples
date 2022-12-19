using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;
using TwinCAT.BA.Tc3_BA2;


namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates cyclic read operations.
    /// </summary>
    public class Sample02 : IInitializableSample
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

        public void Initialize()
        {
            BaSite.OnPostReadCycle += OnPostReadCycle;
        }
        public async Task Run()
        {
            // Find object:
            var iObj = BaSite.FindObject(ObjectPath);
            if (iObj == null)
                Console.WriteLine("Unable to find '{0}'!", ObjectPath);
            else
                // Add job to cyclically read a variable's value:
                await iObj.Device.AddCyclicReadJobAsync(BaSumCommandFactory.Create<IBaReadSumCommand>(iObj, false, (_bPlcVar) => (_bPlcVar.ID == VariableId)));

            Console.WriteLine();
            Console.WriteLine("Press key to stop...");
            Console.ReadKey();
        }


        private static void OnPostReadCycle(object oSender, IReadOnlyDictionary<IBaBaseDevice, BaReadJobList> iDoneJobs)
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
    }
}