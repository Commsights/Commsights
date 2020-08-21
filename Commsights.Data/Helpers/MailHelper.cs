using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Commsights.Data.Helpers
{
    public class MailHelper
    {
        public static string WriteContentFromDictionary(string templatePath, Dictionary<string, string> contents)
        {
            StreamReader streamReader = new StreamReader(templatePath);
            StringBuilder stringBuilder = new StringBuilder(streamReader.ReadToEnd());
            streamReader.Close();

            foreach (KeyValuePair<string, string> content in contents)
            {
                stringBuilder.Replace("[" + content.Key + "]", content.Value);
            }

            return stringBuilder.ToString();
        }

        public static string WriteContentFromObject<T>(string templatePath, T obj)
        {
            if (obj != null)
            {
                string result = AppGlobal.InitString;
                var contents = obj.ToDictionaryStringString();
                if (contents != null)
                {
                    return WriteContentFromDictionary(templatePath, contents);
                }
                return string.Empty;
            }
            return string.Empty;
        }
    }
}
