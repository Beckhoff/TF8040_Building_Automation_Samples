using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;
using TwinCAT.BA.Tc3_XBA;
using TwinCAT.BA.Tc3_XBA.Ads;

namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates how to write lazy code with pre-defined notification types.
    /// </summary>
    public class Sample11a : IExecutableSample
    {
        #region Settings
        /// <summary>
        /// Details to observe in summary.
        /// </summary>
        public BaCovNotificationType[] Details = new[] { BaCovNotificationType.eActivePriority, BaCovNotificationType.eStatus, BaCovNotificationType.eEvent };
        #endregion


        public async Task Run()
        {
            // Get device:
            Debug.Assert(BaSite.Devices.Count == 1);
            var iDevice = BaSite.Devices[0];

            // Get some plant:
            var iPlant = (IBaView)iDevice.ObjectTable.Values.FirstOrDefault(iObj => (iObj.NodeType == BaNodeType.ePlant));
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
                        if (Details.Contains(BaCovNotificationType.eActivePriority))
                            _sValue.AppendLine($"- Active priorities: {string.Join(", ", iPlant.ActivePriorityCount)}");
                        if (Details.Contains(BaCovNotificationType.eStatus))
                            _sValue.AppendLine($"- Status flags: {string.Join(", ", iPlant.StatusFlagCount)}");
                        if (Details.Contains(BaCovNotificationType.eEvent))
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

    /// <summary>
    /// Demonstrates how to write lazy code with customized COV-Indicators.
    /// </summary>
    public class Sample11b : IExecutableSample
    {
        #region Settings
        /// <summary>
        /// Interval for periodic summary updates.
        /// </summary>
        public TimeSpan UpdateInterval = TimeSpan.FromMinutes(2);
        #endregion


        #region Types.COV-Indicators
        /// <summary>
        /// Indicator with writable <see cref="Dirty"/> flag for manual indications.
        /// </summary>
        public class ManualIndicator : IBaCovIndicator<IBaView>
        {
            public ManualIndicator(IBaView iContext)
            {
                this.Context = iContext;

                Debug.Assert(iContext != null);
            }


            #region Properties.Management
            public IBaView Context { get; }
            #endregion
            #region Properties
            public bool Dirty { set; get; }
            #endregion


            #region Management
            public void Save()
            {
                Dirty = false;
            }
            #endregion
        }
        /// <summary>
        /// Indicator with periodic update interval.
        /// </summary>
        public class TimedIndicator : IBaCovIndicator<IBaView>
        {
            public TimedIndicator(IBaView iContext) : this(iContext, TimeSpan.FromMinutes(30))
            {
            }
            public TimedIndicator(IBaView iContext, TimeSpan interval)
            {
                this.Context = iContext;
                this.Interval = interval;

                Debug.Assert(iContext != null);
            }


            #region Properties.Management
            public IBaView Context { get; }
            public TimeSpan Interval { get; }
            public DateTime LastUpdate { private set; get; }
            #endregion
            #region Properties
            public bool Dirty => (LastUpdate == default) || ((DateTime.Now - LastUpdate) >= Interval);
            #endregion


            #region Management
            public void Save()
            {
                LastUpdate = DateTime.Now;
            }
            #endregion
        }
        #endregion


        public async Task Run()
        {
            // Get device:
            Debug.Assert(BaSite.Devices.Count == 1);
            var iDevice = BaSite.Devices[0];

            // Get some plant:
            var iPlant = (IBaView)iDevice.ObjectTable.Values.FirstOrDefault(iObj => (iObj.NodeType == BaNodeType.ePlant));
            if (iPlant == null)
                Console.WriteLine("\nNo plants found.");
            else
            {
                // Create (periodic refreshed) event summary:
                var iSummary = BaLazyFactory.CreateValue(
                    iPlant,
                    new TimedIndicator(iPlant, UpdateInterval),
                    (_iContext) => $"'{_iContext}' has {_iContext.Events.Count} event(s) at {DateTime.Now}."
                );

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
