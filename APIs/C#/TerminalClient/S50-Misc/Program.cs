using System;
using System.Linq;
using System.Threading.Tasks;
using Beckhoff.BA.TerminalClient.Api;
using Beckhoff.BA.TerminalClient.Api.Site;

using static Beckhoff.BA.TerminalClient.Api.BaApi;


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
            Console.WriteLine("\nSample 1.1) List device-to-device communication:");
            try
            {
                var iCom = await IBaDevice.ReadDeviceCommunicationAsync(DevNetID);
                var iSubscr = iCom.Connections
                    .Where(iConection => (iConection.ServiceType == Tc3_BA2.BaServiceType.eSubscribe)).ToList();

                Console.WriteLine("'{0}' has remote subscriptions on:", DevNetID);
                if (iSubscr.Count == 0)
                    Console.WriteLine("<none>");
                else
                {
                    foreach (var iConection in iSubscr)
                        Console.WriteLine("- " + iConection);
                }
            }
            catch (Exception eExcpt)
            {
                Console.WriteLine("<Failed> {0}", eExcpt.Message);
            }
        }


        #region Events
        private static void OnLog(BaLogType bIcon, string sCode, object oEvent, string sProcess = "")
        {
            Console.WriteLine(oEvent.ToString());
        }
        #endregion


        #region CodeSnippets
        static async Task Snippets()
        {
            // Snippet 1.1) Quick-resolve an objects variable value:
            var iVal11 = (IBaPrimitiveValue<float>)BaSite.FindObject("MyPlcProject.MAIN.SomeValue")[Tc3_BA2.BaPlcVariableID.ePresentValue].Value;

            // Snippet 1.2) Fast-read an objects variable:
            await((IBaPrimitiveValue<float>)BaSite.FindObject("MyPlcProject.MAIN.SomeValue")[Tc3_BA2.BaPlcVariableID.ePresentValue].Value).ReadAsync();

            // Snippet 2.1) Quick-write an objects variable:
            var iVal21 = (IBaPrimitiveValue<float>)BaSite.FindObject("MyPlcProject.MAIN.SomeValue")[Tc3_BA2.BaPlcVariableID.eValueRm].Value;
            iVal21.Primitive = 123;
            await iVal21.WriteAsync();

            // Snippet 2.2) Fast-write an objects variable:
            await((IBaPrimitiveValue<float>)BaSite.FindObject("MyPlcProject.MAIN.SomeValue")[Tc3_BA2.BaPlcVariableID.eValueRm].Value).WriteAsync(123);

            // Snippet 3) Top-down resolving a variable value in single steps:
            IBaBasicObject iObj = BaSite.FindObject("MyPlcProject.MAIN.SomeValue");
            IBaPlcVariable iPlcVar = iObj[Tc3_BA2.BaPlcVariableID.eValueRm];

            IBaValue iVal1 = iPlcVar.Value;

            // Test for primitive value:
            IBaPrimitiveValue iPrimVal = iVal1 as IBaPrimitiveValue;
            if (iPrimVal != null)
            {
                IBaPrimitiveValue<float> iFloatVal = iPrimVal as IBaPrimitiveValue<float>;
                if (iFloatVal != null)
                    iFloatVal.Primitive = 123;
            }

            // Test for object type:
            IBaAnalogObject iAnalogObj = iObj as IBaAnalogObject;
            if (iAnalogObj != null)
                // E.g. modify some analog-type properties:
                iAnalogObj.CovIncrement.Primitive = 1.0F;

            IBaEventObject iEventObj = iObj as IBaEventObject;
            if (iEventObj != null)
                // E.g. do some event related operations:
                await iEventObj.AcknowledgeAsync();

            IBaAcknowledgeEnabled iAckObj = iObj as IBaAcknowledgeEnabled;
            if (iAckObj != null)
            {
                // E.g. check if object can be acknowledged:
                if (iAckObj.CanAcknowledge)
                    ; // ...
            }
        }
        #endregion
    }
}
