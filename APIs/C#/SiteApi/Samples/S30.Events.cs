using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;
using TwinCAT.BA.Tc3_BA2;
using TwinCAT.BA.Tc3_BA2.Ads;

namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates how handle <see cref="IBaEvent">events</see>.
    /// </summary>
    public class Sample30 : IInitializableSample
    {
        public void Initialize()
        {
            BaSite.OnNotification += OnNotification;
        }
        public async Task Run()
        {
            // Print actual event state:
            BaSite.EventHistory.Print("Historical events");
            BaSite.Events.Print("Active events");

            // Listen for changing events:
            Console.WriteLine($"Started listening for changing events.");
            Console.WriteLine("Press key to stop...\n");
            while (!Console.KeyAvailable)
                await Task.Delay(500);
        }

        /// <summary>
        /// Listen for <see cref="BaCovNotificationType">changes</see>.
        /// </summary>
        private void OnNotification(IBaDevice iSender, BaNotificationEventArgs bArgs)
        {
            switch (bArgs.Type)
            {
                case BaCovNotificationType.eEvent:
                    // Cast to get event specific details:
                    var _bArgs = (BaEventNotificationEventArgs)bArgs;

                    _bArgs.Changes.Print("Changed events");
                    break;
            }
        }
    }


    internal static class ExtS30
    {
        public static void Print(this IEnumerable<IBaEvent> iSource, string sTitle)
        {
            Console.WriteLine($"{sTitle}:");
            foreach (var _iEvent in iSource)
                Console.WriteLine($"- {_iEvent} ");
            Console.WriteLine("");
        }
    }
}
