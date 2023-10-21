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
            var j = new JObject();

            j.RemoveAll();

            foreach (var field in StructureType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                JToken result;

                try
                {
                    result = JToken.FromObject(field.GetValue(Structure));
                }
                catch
                {
                    result = default;
                }

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
                // For array
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    // Exception in parsing json
                    return false;
                }
                catch // some other exception
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}