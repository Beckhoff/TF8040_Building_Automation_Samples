using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;


namespace Beckhoff.BA.SiteApi.Samples
{
    public class Sample11 : IExecutableSample
    {
        #region Settings
        /// <summary>
        /// Details to observe in summary.
        /// </summary>
        public BaAds.BaCovNotificationType[] Details = new[] { BaAds.BaCovNotificationType.eActivePriority, BaAds.BaCovNotificationType.eStatus, BaAds.BaCovNotificationType.eEvent };
        #endregion


        public async Task Run()
        {
            // Get device:
            Debug.Assert(BaSite.Devices.Count == 1);
            var iDevice = BaSite.Devices[0];

            // Get some plant:
            var iPlant = (IBaView)iDevice.ObjectTable.Values.FirstOrDefault(iObj => (iObj.NodeType == Tc3_BA2.BaNodeType.ePlant));
            if (iPlant == null)
                Console.WriteLine("\nNo plants found.");
            else
            {
                // Create (lazy loaded) summary:
                var iSummary = BaLazyFactory.CreateValue(iPlant, (_iContext) =>
                {
                    var _sValue = new StringBuilder();
                    {
                        _sValue.AppendLine($"[{DateTime.Now.ToLongTimeString()}] Summary:");
                        if (Details.Contains(BaAds.BaCovNotificationType.eActivePriority))
                            _sValue.AppendLine($"- Active priorities: {string.Join(", ", iPlant.ActivePriorityCount)}");
                        if (Details.Contains(BaAds.BaCovNotificationType.eStatus))
                            _sValue.AppendLine($"- Status flags: {string.Join(", ", iPlant.StatusFlagCount)}");
                        if (Details.Contains(BaAds.BaCovNotificationType.eEvent))
                        {
                            _sValue.AppendLine($"- Lock priorities: {string.Join(", ", iPlant.LockPriorityCount)}");
                            _sValue.AppendLine($"- Events: {iPlant.Events.Count}");
                        }
                    }
                    return (_sValue.ToString());

                }, Details);

                // Poll for changes and print summary:
                Console.WriteLine($"Started observing '{iPlant}'.");
                Console.WriteLine("Press key to stop...\n");
                while (!Console.KeyAvailable)
                {
                    // Check for changes:
                    if (iSummary.Dirty)
                        Console.WriteLine(iSummary.Content);

                    await Task.Delay(500);
                }
            }
        }
    }
}
