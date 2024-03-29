/*
 * RewardSciences.PCL
 *
 * This file was automatically generated by APIMATIC v2.0 ( https://apimatic.io ) on 09/27/2016
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using unirest_net.request;

namespace RewardSciences.PCL
{
    public static class APIHelper
    {
        //DateTime format to use for parsing and converting dates
        public static string DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        /// <summary>
        /// JSON Serialization of a given object.
        /// </summary>
        /// <param name="obj">The object to serialize into JSON</param>
        /// <returns>The serialized Json string representation of the given object</returns>
        public static string JsonSerialize(object obj)
        {
            if (null == obj)
                return null;

            return JsonConvert.SerializeObject(obj, Formatting.None,
                 new IsoDateTimeConverter() { DateTimeFormat = DateTimeFormat });
        }

        /// <summary>
        /// JSON Deserialization of the given json string.
        /// </summary>
        /// <param name="json">The json string to deserialize</param>
        /// <typeparam name="T">The type of the object to desialize into</typeparam>
        /// <returns>The deserialized object</returns>
        public static T JsonDeserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            return JsonConvert.DeserializeObject<T>(json,
                 new IsoDateTimeConverter() { DateTimeFormat = DateTimeFormat });
        }

        /// <summary>
        /// Replaces template parameters in the given url
        /// </summary>
        /// <param name="queryUrl">The query url string to replace the template parameters</param>
        /// <param name="parameters">The parameters to replace in the url</param>
        public static void AppendUrlWithTemplateParameters
            (StringBuilder queryBuilder, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            //perform parameter validation
            if (null == queryBuilder)
                throw new ArgumentNullException("queryBuilder");

            if (null == parameters)
                return;

            //iterate and replace parameters
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                string replaceValue = string.Empty;

                //load element value as string
                if (null == pair.Value)
                    replaceValue = "";
                else if (pair.Value is ICollection)
                    replaceValue = flattenCollection(pair.Value as ICollection, "{0}{1}", '/', false);
                else if (pair.Value is DateTime)
                    replaceValue = ((DateTime)pair.Value).ToString(DateTimeFormat);
                else
                    replaceValue = pair.Value.ToString();

                replaceValue = Uri.EscapeUriString(replaceValue);

                //find the template parameter and replace it with its value
                queryBuilder.Replace(string.Format("{{{0}}}", pair.Key), replaceValue);
            }
        }

        /// <summary>
        /// Appends the given set of parameters to the given query string
        /// </summary>
        /// <param name="queryUrl">The query url string to append the parameters</param>
        /// <param name="parameters">The parameters to append</param>
        public static void AppendUrlWithQueryParameters
            (StringBuilder queryBuilder, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            //perform parameter validation
            if (null == queryBuilder)
                throw new ArgumentNullException("queryBuilder");

            if (null == parameters)
                return;

            //does the query string already has parameters
            bool hasParams = (indexOf(queryBuilder, "?") > 0);

            //iterate and append parameters
            foreach (KeyValuePair<string, object> pair in parameters)
            {
                //ignore null values
                if (pair.Value == null)
                    continue;

                //if already has parameters, use the &amp; to append new parameters
                queryBuilder.Append((hasParams) ? '&' : '?');

                //indicate that now the query has some params
                hasParams = true;

                string paramKeyValPair;

                //load element value as string
                if (pair.Value is ICollection)
                    paramKeyValPair = flattenCollection(pair.Value as ICollection, string.Format("{0}[]={{0}}{{1}}", pair.Key), '&', true);
                else if (pair.Value is DateTime)
                    paramKeyValPair = string.Format("{0}={1}", Uri.EscapeDataString(pair.Key), ((DateTime)pair.Value).ToString(DateTimeFormat));
                else
                    paramKeyValPair = string.Format("{0}={1}", Uri.EscapeDataString(pair.Key), Uri.EscapeDataString(pair.Value.ToString()));

                //append keyval pair for current parameter
                queryBuilder.Append(paramKeyValPair);
            }
        }

        /// <summary>
        /// StringBuilder extension method to implement IndexOf functionality.
        /// This does a StringComparison.Ordinal kind of comparison.
        /// </summary>
        /// <param name="stringBuilder">The string builder to find the index in</param>
        /// <param name="strCheck">The string to locate in the string builder</param>
        /// <returns>The index of string inside the string builder</returns>
        private static int indexOf(StringBuilder stringBuilder, string strCheck)
        {
            if (stringBuilder == null)
                throw new ArgumentNullException("stringBuilder");

            if (strCheck == null)
                return 0;

            //iterate over the input
            for (int inputCounter = 0; inputCounter < stringBuilder.Length; inputCounter++)
            {
                int matchCounter;

                //attempt to locate a potential match
                for (matchCounter = 0;
                        (matchCounter < strCheck.Length)
                        && (inputCounter + matchCounter < stringBuilder.Length)
                        && (stringBuilder[inputCounter + matchCounter] == strCheck[matchCounter]);
                    matchCounter++) ;

                //verify the match
                if (matchCounter == strCheck.Length)
                    return inputCounter;
            }

            return -1;
        }

        /// <summary>
        /// Validates and processes the given query Url to clean empty slashes
        /// </summary>
        /// <param name="queryBuilder">The given query Url to process</param>
        /// <returns>Clean Url as string</returns>
        public static string CleanUrl(StringBuilder queryBuilder)
        {
            //convert to immutable string
            string url = queryBuilder.ToString();

            //ensure that the urls are absolute
            Match match = Regex.Match(url, "^https?://[^/]+");
            if (!match.Success)
                throw new ArgumentException("Invalid Url format.");

            //remove redundant forward slashes
            int index = url.IndexOf('?');
            string protocol = match.Value;
            string query = url.Substring(protocol.Length, (index == -1 ? url.Length : index) - protocol.Length);
            query = Regex.Replace(query, "//+", "/");
            string parameters = index == -1 ? "" : url.Substring(index);

            //return process url
            return string.Concat(protocol, query, parameters);;
        }

        /// <summary>
        /// Used for flattening a collection of objects into a string 
        /// </summary>
        /// <param name="array">Array of elements to flatten</param>
        /// <param name="fmt">Format string to use for array flattening</param>
        /// <param name="separator">Separator to use for string concat</param>
        /// <returns>Representative string made up of array elements</returns>
        private static string flattenCollection(ICollection array, string fmt, char separator, bool urlEncode)
        {
            StringBuilder builder = new StringBuilder();

            //append all elements in the array into a string
            foreach (object element in array)
            {
                string elemValue = null;

                //replace null values with empty string to maintain index order
                if (null == element)
                    elemValue = string.Empty;
                else if (element is DateTime)
                    elemValue = ((DateTime)element).ToString(DateTimeFormat);
                else
                    elemValue = element.ToString();

                if (urlEncode)
                    elemValue = Uri.EscapeDataString(elemValue);

                builder.AppendFormat(fmt, elemValue, separator);
            }

            //remove the last separator, if appended
            if ((builder.Length > 1) && (builder[builder.Length - 1] == separator))
                builder.Length -= 1;

            return builder.ToString();
        }

        /// <summary>
        /// Prepares the object as form fields using the provided name.
        /// </summary>
        /// <param name="name">root name for the variable</param>
        /// <param name="value">form field value</param>
        /// <param name="keys">Contains a flattend and form friendly values</param>
        /// <returns>Contains a flattend and form friendly values</returns>
        public static Dictionary<string, object> PrepareFormFieldsFromObject(
            string name, object value, Dictionary<string, object> keys = null)
        {
            keys = keys ?? new Dictionary<string, object>();

            if (value == null)
            {
                return keys;
            }
            else if (value is Stream)
            {
                keys[name] = value;
                return keys;
            }
            else if (value is JObject)
            {
                var valueAccept = (value as Newtonsoft.Json.Linq.JObject);
                foreach (var property in valueAccept.Properties())
                {
                    string pKey = property.Name;
                    object pValue = property.Value;
                    var fullSubName = name + '[' + pKey + ']';
                    PrepareFormFieldsFromObject(fullSubName, pValue, keys);
                }
            }
            else if (value is IList)
            {
                int i = 0;
                var enumerator = ((IEnumerable)value).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var subValue = enumerator.Current;
                    if (subValue == null) continue;
                    var fullSubName = name + '[' + i + ']';
                    PrepareFormFieldsFromObject(fullSubName, subValue, keys);
                    i++;
                }
            }
            else if (value is JToken)
            {
                keys[name] = value.ToString();
            }
            else if (value is Enum)
            {
#if WINDOWS_UWP
                Assembly thisAssembly = typeof(APIHelper).GetTypeInfo().Assembly;
#else
                Assembly thisAssembly = Assembly.GetExecutingAssembly();
#endif
                string enumTypeName = value.GetType().FullName;
                Type enumHelperType = thisAssembly.GetType(string.Format("{0}Helper", enumTypeName));
                object enumValue = (int)value;

                if (enumHelperType != null)
                {
                    //this enum has an associated helper, use that to load the value
                    MethodInfo enumHelperMethod = enumHelperType.GetMethod("ToValue", new[] { value.GetType() });
                    if (enumHelperMethod != null)
                        enumValue = enumHelperMethod.Invoke(null, new object[] { value });
                }

                keys[name] = enumValue;
            }
            else if (value is IDictionary)
            {
                var obj = (IDictionary)value;
                foreach (var sName in obj.Keys)
                {
                    var subName = sName.ToString();
                    var subValue = obj[subName];
                    string fullSubName = string.IsNullOrWhiteSpace(name) ? subName : name + '[' + subName + ']';
                    PrepareFormFieldsFromObject(fullSubName, subValue, keys);
                }
            }
            else if (!(value.GetType().Namespace.StartsWith("System")))
            {
                //Custom object Iterate through its properties
                var enumerator = value.GetType().GetProperties().GetEnumerator();
                PropertyInfo pInfo = null;
                var t = new JsonPropertyAttribute().GetType();
                while (enumerator.MoveNext())
                {
                    pInfo = enumerator.Current as PropertyInfo;

                    var jsonProperty = (JsonPropertyAttribute)pInfo.GetCustomAttributes(t, true).FirstOrDefault();
                    var subName = (jsonProperty != null) ? jsonProperty.PropertyName : pInfo.Name;
                    string fullSubName = string.IsNullOrWhiteSpace(name) ? subName : name + '[' + subName + ']';
                    var subValue = pInfo.GetValue(value, null);
                    PrepareFormFieldsFromObject(fullSubName, subValue, keys);
                }
            }
            else if (value is DateTime)
            {
                keys[name] = ((DateTime)value).ToString(DateTimeFormat);
            }
            else
            {
                keys[name] = value;
            }
            return keys;
        }

        /// <summary>
        /// Add/update entries with the new dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="dictionary2"></param>
        public static void Add(this Dictionary<string, object> dictionary, Dictionary<string, object> dictionary2)
        {
            foreach (var kvp in dictionary2)
            {
                dictionary[kvp.Key] = kvp.Value;
            }
        }
    }
}
