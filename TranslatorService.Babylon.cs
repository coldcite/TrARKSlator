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
    public class TranslatorService_Babylon : TranslatorService
    {

        public TranslatorService_Babylon()
        {

            this.Name = "Babylon (JP only)";

        }

        override public string DetectLanguage(string text)
        {

            return ParsingSupport.isJapanese(text) ? "ja" : "en";

        }

        override public string Translate(string text, ref string from)
        {

            string transText = "";
            string transURL = "http://translation.babylon.com/translate/babylon.php?v=1.0&q=" + HttpUtility.UrlEncode(text) + "&langpair=8%7C0&callback=babylonTranslator.callback&context=babylon.8.0._babylon_api_response";

            try
            {
                using (WebClient client = new WebClient())
                {

                    string transResp = client.DownloadString(transURL);
                    string[] a_transResp = transResp.Split(',');

                    JObject trObject = JObject.Parse(a_transResp[1]);
                    transText = trObject["translatedText"].ToString();

                }
            }
            catch (Exception e) { transText = text; from = "ERROR"; }

            return transText.Length > 0 ? transText : text;

        }

    }

}
