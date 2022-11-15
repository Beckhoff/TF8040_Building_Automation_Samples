using System;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;
using TwinCAT.BA.Tc3_BA2;
using TwinCAT.BA.Utilities;


namespace Beckhoff.BA.SiteApi.Samples
{
    /// <summary>
    /// Includes some miscellaneous examples and code snippets.
    /// </summary>
    public class Sample50 : IGeneralSample
    {
        #region Settings
        /// <summary>
        /// AMS-NetID of any device.
        /// </summary>
        public string SomeDevNetID = "127.0.0.1.1.1:851";

        /// <summary>
        /// Symbolpath of object to be tested.
        /// </summary>
        public string ObjectPath = "MyPlcProject.MAIN.SomeValue";
        /// <summary>
        /// Parameter to read.
        /// </summary>
        public BaParameterId VariableId = BaParameterId.ePresentValue;
        #endregion


        public async Task Run()
        {
            Console.WriteLine("\nSample 1.1) List device-to-device communication:");
            try
            {
                var iCom = await IBaDevice.ReadDeviceCommunicationAsync(SomeDevNetID);
                var iSubscr = iCom.Connections
                    .Where(iConection => (iConection.ServiceType == BaServiceType.eSubscribe)).ToList();

                Console.WriteLine("'{0}' has remote subscriptions on:", SomeDevNetID);
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


        #region Samples.CodeSnippets
        public async Task SnipResolve()
        {
            // Snippet "Resolve" 1.1) Quick-resolve an objects variable value:
            var iVal11 = (IBaPrimitiveValue<float>)BaSite.FindObject(ObjectPath)[VariableId].Value;

            // Snippet "Resolve" 1.2) Quick-read an objects variable:
            await ((IBaPrimitiveValue<float>)BaSite.FindObject(ObjectPath)[VariableId].Value).ReadAsync();

            // Snippet "Resolve" 1.3) Quick-write an objects variable:
            var iVal21 = (IBaPrimitiveValue<float>)BaSite.FindObject(ObjectPath)[VariableId].Value;
            iVal21.Primitive = 123;
            await iVal21.WriteAsync();

            // Snippet "Resolve" 1.4) Quick-write an objects variable value:
            await ((IBaPrimitiveValue<float>)BaSite.FindObject(ObjectPath)[VariableId].Value).WriteAsync(123);

            // Snippet "Resolve" 1.5) Quick-write multiple variable values:
            await BaSite.FindObject(ObjectPath).Parameters
                .Where(iParam => iParam.Value is IBaPrimitiveValue<bool>)
                .WriteAsync(true);


            // Snippet "Resolve" 2) Top-down resolving a variable value in single steps:
            IBaBasicObject iObj = BaSite.FindObject(ObjectPath);
            IBaVariable iPlcVar = iObj[VariableId];
            IBaValue iVal1 = iPlcVar.Value;


            // Snippet "Resolve" 1.5) Quick-acknowledge multiple objects:
            await BaSite.Devices.First()
                .ObjectTable.Values.Take(5)
                .AcknowledgeAsync();
        }
        public async Task SnipTests(IBaObject iSomeObject)
        {
            // Snippet "Object test" 1) Test for object type:
            if (iSomeObject is IBaAnalogObject iAObj)
                // E.g. modify some analog-type properties:
                iAObj.CovIncrement.Primitive = 1.0F;

            if (iSomeObject is IBaEventObject iEvtObj)
                // E.g. do some event related operations:
                await iEvtObj.AcknowledgeAsync();

            if (iSomeObject is IBaAcknowledgeEnabled iAckObj)
            {
                // E.g. check if object can be acknowledged:
                if (iAckObj.CanAcknowledge)
                    ; // ...
            }
            

            Console.WriteLine("\nSnippet \"Object test\" 2) List additional parameters:");
            foreach (var iParam in iSomeObject.AdditionalParameters.Values)
                Console.WriteLine(string.Format("- {0}", iParam.Title));
        }
        public void SnipTests(IBaValue iSomeValue)
        {
            if (iSomeValue is IBaPrimitiveValue iPrimVal)
            {
                // Snippet "Value test" 1) Test for primitive value:
                if (iPrimVal is IBaPrimitiveValue<float> iFloatVal)
                    iFloatVal.Primitive = 123;

                Console.WriteLine("\nSnippet \"Value test\" 2) Test out value range of a primitive value:");
                switch (iPrimVal.GetValueRange())
                {
                    case null:
                        Console.WriteLine("Value not limited.");
                        break;

                    case IBaLimitedValueRange iLimit:
                        Console.WriteLine(string.Format("Value limited from {0} to {1}.", iLimit.MinLimit, iLimit.MaxLimit));
                        break;

                    case IBaEnumeratedValueRange iEnum:
                        Console.WriteLine("\nValue limited to following values:");
                        foreach (var _sVal in iEnum.Values)
                            Console.WriteLine(string.Format("- {0}", _sVal));
                        break;

                    default:
                        throw new NotImplementedException("Not implemented range of value!");
                }
            }
        }
        public void SnipParameterInfo()
        {
            Console.WriteLine("\nSnippet \"Parameter info\" 1) List description:");
            foreach (var item in BaPlc.VariableInfo)
            {
                foreach (var _iParam in item.Value)
                {
                    Console.Write($"- {_iParam} | {item.Key,-40}");
                    switch (_iParam)
                    {
                        case IBaNotImplementedParameterInfo:
                            // [Hint] Will be provided soon...
                            Console.Write("< Not implemented yet >");
                            break;


                        case IBaDecoratedParameterInfo decorated:
                            Console.Write($"Decorated parameter ({Format(decorated.DecorValue)})");
                            break;
                        case IBaDecoratingParameterInfo:
                            // [Hint] Decoration only available when used in a variable's context.
                            Console.Write("Decorating parameter");
                            break;

                        case IBaEnumeratedParameterInfo enumerated:
                            Console.Write($"Enumerated parameter ({string.Join(", ", enumerated.GetValues().Take(3))})");
                            break;
                        case IBaEnumeratingParameterInfo:
                            // [Hint] Enumerated values only available when used in a variable's context.
                            Console.Write("Enumerating parameter");
                            break;

                        case IBaParameterInfo:
                            Console.Write("Parameter");
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                    Console.WriteLine("");
                }
            }
        }
        #endregion


        #region Helper
        private static string Format(object value)
        {
            if (value is Enum val)
                return (val.GetDescription());
            else
                return (value.ToString());
        }
        #endregion
    }
}
