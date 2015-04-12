using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrARKSlator
{
    public class TranslatorService_Bing : TranslatorService
    {

        public TranslatorService_Bing()
        {

            this.Name = "Bing";

        }

        override public string DetectLanguage(string text)
        {

            return "";

        }

        override public string Translate(string text, ref string from)
        {

            string transText = "";

            // This guy is expecting an array of strings as input, one element per line
            JArray ja_text = JArray.FromObject(text.Split('\n'));
            string transURL = "https://api.microsofttranslator.com/v2/ajax.svc/TranslateArray2?appId=%22TfGg2JyestCqtAUlxPeLUv3c0Tf5xZCh-8d5EQlAWmQ7dLS1IWlqX7DpD_gg1rU8a%22&texts=" +  HttpUtility.UrlEncode( ja_text.ToString() ) + "&from=%22%22&to=%22en%22";

            try
            {
                using (WebClient client = new WebClient())
                {

                    string transResp = client.DownloadString(transURL);
                    JArray trObject = JArray.Parse(transResp);

                    List<string> lstLangs = new List<string>();
                    foreach (JObject line in trObject)
                    {
                        transText += line["TranslatedText"].ToString() + Environment.NewLine;
                        string thisLnFrom = line["From"].ToString();
                        if ((thisLnFrom != "en") && !lstLangs.Any(x => x==thisLnFrom)) lstLangs.Add(thisLnFrom);
                    }

                    transText = transText.TrimEnd(Environment.NewLine.ToCharArray());
                    if (lstLangs.Count > 0) from = string.Join(",", lstLangs.ToArray());

                }
            }
            catch (Exception e) { transText = text; from = "ERROR"; }

            return transText.Length > 0 ? transText : text;

        }

    }

}
