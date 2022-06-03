using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;

using static TwinCAT.BA.BaApi;


namespace Beckhoff.BA.TerminalClient.Samples
{
    class Program
    {
        #region Settings
        /// <summary>
        /// AMS-NetID of device to connect to.
        /// </summary>
        private static string DevNetID = "127.0.0.1.1.1:851";
        #endregion


        static async Task Main(string[] args)
        {
            // Sample 1) Set used language:
            BaApi.Language.SelectedLanguage = BaLanguageManager.BaLanguage.en_US;

            // Sample 2) Set additional property filter:
            // -> This filter will be used at a device's initialization time to ensure that the specified properties are initialized by reading them once.
            // -> This filter is optional and works in addition to the internal default filter.
            var MyFilter = new Tc3_BA2.BaVariableID[]
            {
                Tc3_BA2.BaVariableID.eAction,
                Tc3_BA2.BaVariableID.eAddress
            };
            BaSite.InitialObjectPropertyFilter = (iVar) => MyFilter.Contains(iVar.ID);

            // Initialize:
            BaSite.OnLog += OnLog;

            // Sample 3) Customize device before adding it to site:
            var iDevice = BaSite.CreateDevice(DevNetID);

            // Do something application specific with 'iDevice' here ...

            // Establish site connection:
            BaSite.AddDevice(iDevice);
            await BaSite.ConnectAsync();
            {
                var iObjects = iDevice.ObjectTable.Values;

                // Sample 4.1) Add a device tag:
                iDevice.AddTag(new MyTag());

                // Sample 4.2) Add tag to some objects:
                int i = 0, u = 0;
                iObjects
                    .Where(iObj => (i++ % 10) == 0).ToList()
                    .ForEach(iObj => iObj.AddTag(new MyTag(string.Format("This is object no {0}.", ++u))));

                Console.WriteLine("\nSample 4.3) Show tagged objects:");
                iObjects
                    .Where(iObj => iObj.ContainsTag<MyTag>()).ToList()
                    .ForEach(iObj => Console.WriteLine("- [{0}] {1}", iObj.InstDescription, iObj.GetTag<MyTag>()));
            }

            // Disconnect from site:
            await BaSite.DisconnectAsync();

            Console.WriteLine();
            Console.WriteLine("Press key to exit...");
            Console.ReadKey();
        }


        #region Events
        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "", IBaLog iContext = null)
        {
            Console.WriteLine(oEvent.ToString());
        }
        #endregion
    }


    class MyTag
    {
        public MyTag(string sNote = "")
        {
            this.RandomNumber = rRandom.Next();
            this.Note = sNote;
        }


        public int RandomNumber { get; }
        public string Note { get; }


        public override string ToString()
        {
            string sText = ("Some number: " + RandomNumber.ToString());
            if (!string.IsNullOrEmpty(Note))
                sText += (", Note: " + Note);
            return (sText);
        }


        private static Random rRandom = new Random();
    }
}
