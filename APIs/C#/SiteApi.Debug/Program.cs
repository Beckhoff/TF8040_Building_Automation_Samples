using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;

using static TwinCAT.BA.BaApi;


namespace Beckhoff.BA.SiteApi.Samples
{
    public class Program : Loader
    {
        #region Settings
        public static ISample DebugSample = new Sample30();

        public static string DebugDevice = "5.57.161.218.1.1:851";
        public static IReadOnlyDictionary<Tuple<Type, string>, object> DebugSettings = new Dictionary<Tuple<Type, string>, object>()
        {
            // General:
            { new(default, "ObjectPath"), "Tc3_BA2.MAIN.General.AVSp" },
            { new(default, "VariableId"), Tc3_BA2.BaParameterId.ePresentValue },

            // Sample specific:
            { new(typeof(Sample11), "Details"), new BaAds.BaCovNotificationType[] { BaAds.BaCovNotificationType.eActivePriority, BaAds.BaCovNotificationType.eStatus } },
            { new(typeof(Sample20), "AnotherDevNetID"), "1.2.3.4.5.6:851" },
            { new(typeof(Sample21), "SymbolPath"), "MAIN.General.MVSp.nValueRm" },
            { new(typeof(Sample50), "SomeDevNetID"), DebugDevice },
        };
        #endregion


        static async Task Main(string[] args)
        {
            BaSite.OnLog += OnLog;

            // Apply debug settings:
            foreach (var field in DebugSample.GetType().GetFields())
            {
                try
                {
                    var val = DebugSettings.First(item =>
                    {
                        if (item.Key.Item2 == field.Name)
                        {
                            if (item.Key.Item1 == default)
                                return (true);
                            else if (item.Key.Item1 == DebugSample.GetType())
                                return (true);
                        }
                        return (false);
                    });
                    field.SetValue(DebugSample, val.Value);
                }
                catch (Exception)
                {
                    throw new NotImplementedException($"Failed to apply debug settings for missing field '{field.Name}'!");
                }
            }

            // Run sample:
            await RunSampleAsync(DebugDevice, DebugSample);
        }


        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "", IBaLog iContext = null)
        {
            Console.WriteLine(oEvent.ToString());
        }
    }
}
