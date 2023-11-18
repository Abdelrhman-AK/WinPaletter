using System.Collections.Generic;

namespace WinPaletter.TypesExtensions
{
    public static class StringListsExtensions
    {
        /// <summary>
        /// Deduplicate list of string
        /// </summary>
        public static List<string> DeDuplicate(this List<string> List)
        {
            var Result = new List<string>();

            bool Exist;

            foreach (string ElementString in List)
            {
                Exist = false;
                foreach (string ElementStringInResult in Result)
                {
                    if ((ElementString ?? string.Empty) == (ElementStringInResult ?? string.Empty))
                    {
                        Exist = true;
                        break;
                    }
                }
                if (!Exist)
                {
                    Result.Add(ElementString);
                }
            }

            return Result;
        }

        /// <summary>
        /// Return String from List, each item is in a separate line
        /// </summary>
        public static string CString(this List<string> List, string JoinBy = "\r\n")
        {
            return string.Join(JoinBy, List.ToArray());
        }

    }
}
