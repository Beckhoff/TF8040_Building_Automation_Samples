using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;


namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Demonstrates how to customite <see cref="Beckhoff.BA.SiteApi">SiteApi</see>.
    /// </summary>
    public class Sample20 : IInitializableSample
    {
        #region Settings
        /// <summary>
        /// AMS-NetID of device to connect to.
        /// </summary>
        public string AnotherDevNetID = "127.0.0.2.1.1:851";
        #endregion


        public void Initialize()
        {
            // Sample 1) Set used language:
            BaApi.Language.SelectedLanguage = BaLanguageManager.BaLanguage.en_US;

            // Sample 2) Set additional property filter:
            // -> This filter will be used at a device's initialization time to ensure that the specified properties are initialized by reading them once.
            // -> This filter is optional and works in addition to the internal default filter.
            var MyFilter = new Tc3_BA2.BaParameterId[]
            {
                Tc3_BA2.BaParameterId.eAction,
                Tc3_BA2.BaParameterId.eAddress
            };
            BaSite.InitialObjectPropertyFilter = (iVar) => MyFilter.Contains(iVar.ID);

            // Sample 3) Customize device before adding it to site:
            var someDevice = BaSite.CreateDevice(AnotherDevNetID);
            {
                // Do something application specific with 'iDevice' here ...
            }
            BaSite.AddDevice(someDevice);
        }
        public async Task Run()
        {
            var iDevice = BaSite.Devices.First();
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
