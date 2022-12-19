using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;


namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates how to use logging properly.
    /// </summary>
    public class Sample51 : IInitializableSample
    {
        public void Initialize()
        {
            BaSite.OnLog += OnLog;
        }
        public async Task Run()
        {
            // Find some object:
            var iObj = BaSite.Devices.First().ObjectTable.First().Value;
            var iParam = iObj.Parameters.First();

            Console.WriteLine("\nSample 1) Simple logging:");
            BaLog.Write(BaLogType.eInfo, "CA39", "Some logged info.");
            BaLog.Write(BaLogType.eError, "CA40", "Some logged error.");

            Console.WriteLine("\nSample 2) Context related logging:");
            iObj.WriteLog(BaLogType.eInfo, "CA43", "Logged some object's info.");
            iParam.WriteLog(BaLogType.eInfo, "CA44", "Logged some parameters's info.");

            Console.WriteLine("\nSample 3) Typed logging:");
            IEnumerable<IBaLog> iFruits = new SomeFruit[]
            {
                new SomeFruit("Apple"),
                new SomeFruit("Peach"),
                new SomeFruit("Mango")
            };
            int i = 0;
            foreach (var _iFruit in iFruits)
                _iFruit.WriteLog(BaLogType.eInfo, "CA55", string.Format("Eating fruit no. {0}", ++i));
        }


        /// <summary>
        /// Write detailed log to debugger output window.
        /// </summary>
        private static void OnLog(object oSender, BaLogEventArgs bArgs)
        {
            string sContext = "";
            if (bArgs.Context != null)
                sContext = bArgs.Context.LogName + " | ";
            Debug.WriteLine(string.Format("{0:MM.dd.yyyy HH:mm:ss} [{1}] {2}{3}: {4}", DateTime.Now, bArgs.Code, sContext, bArgs.Icon, bArgs.Event));
        }
    }

    /// <summary>
    /// Some tasty fruit type.
    /// </summary>
    public class SomeFruit : IBaLog
    {
        public SomeFruit(string sKind)
        {
            this.Kind = sKind;
        }


        string IBaLog.LogName => Kind;
        public string Kind { init; get; }
    }
}
