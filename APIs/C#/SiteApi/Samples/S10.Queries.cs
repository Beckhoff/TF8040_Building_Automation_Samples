using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwinCAT.BA;
using TwinCAT.BA.Site;


namespace Beckhoff.BA.SiteApi.Samples
{
    public class Sample10 : IExecutableSample
    {
        public async Task Run()
        {
            // Get device:
            Debug.Assert(BaSite.Devices.Count == 1);
            var iDevice = BaSite.Devices[0];

            // Get objects:
            var iObjects = iDevice.ObjectTable.Values;

            // Create a lookup of one object per object-type:
            var iLookup = iDevice.ObjectIndex.ToLookup(bEntry => bEntry.Key, bEntry => bEntry.Value.First());

            // Create enumerable from looked up objects:
            var iTypedObjects = iLookup.Select(iGroup => iGroup.First());

            #region Object-Samples
            Console.WriteLine("\nSample 1.1) Show some objects:");
            iObjects
                .Take(10)
                .ListObjects();

            Console.WriteLine("\nSample 1.2) Show some analog objects:");
            iObjects
                .Where(_iObj => _iObj is IBaAnalogObject)
                .Take(5)
                .ListObjects();

            Console.WriteLine("\nSample 1.3) Show some objects that can be acknowledged:");
            iObjects
                .Where(_iObj => _iObj is IBaAcknowledgeEnabled)
                .Take(5)
                .ListObjects();

            Console.WriteLine("\nSample 1.4) Show some event objects:");
            iObjects
                .Where(_iObj => _iObj is IBaEventObject)
                .Take(5)
                .ListObjects();

            Console.WriteLine("\nSample 1.5) Show one object per object-type:");
            iLookup
                .ListObjects();
            #endregion


            #region Variable-Samples
            Console.WriteLine("\nSample 2.1) Show some present values:");
            await iTypedObjects
                .ListParameterValuesAsync(Tc3_BA2.BaParameterId.ePresentValue);
            #endregion


            #region Event-Samples
            Console.WriteLine("\nSample 3.1) Show some of the present events:");
            BaSite.Events
                .Take(10)
                .ListEvents();

            Console.WriteLine("\nSample 3.2) Show some historical events:");
            BaSite.EventHistory
                .Take(5)
                .ListEvents();
            #endregion
        }
    }

    internal static class Ext
    {
        public static void ListObjects(this IEnumerable<IBaBasicObject> iSource)
        {
            foreach (var _iObj in iSource)
                Console.WriteLine("- {0} ({1})", _iObj.SymbolPath, _iObj.Identifier);
        }
        public static void ListObjects(this ILookup<Tc3_BA2.BaObjectType, IBaBasicObject> iSource)
        {
            foreach (var _iEntry in iSource)
                Console.WriteLine("- {0} ({1})", _iEntry.ElementAt(0).Description, _iEntry.Key.GetEnumDescription());
        }

        /// <summary>
        /// List a certain value from all parameters standard that provide these certain parameter.
        /// </summary>
        public static async Task ListParameterValuesAsync(this IEnumerable<IBaBasicObject> iSource, Tc3_BA2.BaParameterId bId)
        {
            await iSource
                .Where(_iObj => _iObj.StandardParameters.ContainsKey(bId))
                .Select(_iObj => _iObj.StandardParameters[bId].Value)
                .ListValuesAsync();
        }

        public static async Task ListValuesAsync(this IEnumerable<IBaValue> iSource)
        {
            // Create sum command of to read values:
            var iCmd = BaSumCommandFactory.Create<IBaReadSumCommand>(iSource);

            // [Hint]
            // To get all values displyed correctly, some dependencies are to be considered:
            // For example a multistate value depends on the configured state texts!
            // -> So read all dependant values before values are displayed:

            // Determine affected objects:
            var iObjects = iSource
                .Select(iVal => iVal.ParentObject)
                .ToHashSet();

            // Filter affected variables:
            iCmd.AddVariables(iObjects, BaSite.ParameterFilter.ObjectInfo);

            // Read all variables:
            if (await iCmd.ReadAsync())
            {
                foreach (var _iValue in iSource)
                    Console.WriteLine("- {0}: {1}", _iValue.Parent.InstancePath, _iValue.ToString());
            }
        }

        public static void ListEvents(this IEnumerable<IBaEvent> iSource)
        {
            foreach (var _iEvent in iSource)
                Console.WriteLine("- [{0}] {1} | {2}", _iEvent.TimeStamp, _iEvent.State, _iEvent.RelatedObject.Description);
        }

        /// <summary>
        /// Receive description from an enumeration value.
        /// </summary>
        public static string GetEnumDescription(this Enum eValue)
        {
            var fField = eValue.GetType().GetField(eValue.ToString());
            var dAttrib = (DescriptionAttribute[])fField.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((dAttrib != null) && (dAttrib.Length > 0))
                return (dAttrib[0].Description);
            else
                return (eValue.ToString());
        }
    }
}
