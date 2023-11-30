using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace WinPaletter.Theme
{
    public partial class Manager
    {
        private JObject DeserializeProps(Type StructureType, object Structure)
        {
            JObject j = new();

            j.RemoveAll();

            foreach (var field in StructureType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                JToken result;
                try { result = JToken.FromObject(field.GetValue(Structure)); }
                catch { result = default; }
                j.Add(field.Name, result);
            }
            return j;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput))
            {
                return false;
            }
            strInput = strInput.Trim();
            if (strInput.StartsWith("{") && strInput.EndsWith("}") || strInput.StartsWith("[") && strInput.EndsWith("]")) // For object
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException) { return false; }   // Exception in parsing json
                catch { return false; }                         // Another exception
            }
            else
            {
                return false;
            }
        }
    }
}