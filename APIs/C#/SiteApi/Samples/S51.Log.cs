using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;

using static TwinCAT.BA.BaApi;


namespace Beckhoff.BA.SiteApi.Samples
{
    public class Sample51 : IExecutableSample
    {
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
