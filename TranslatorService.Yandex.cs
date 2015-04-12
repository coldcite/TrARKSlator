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
    public class TranslatorService_Yandex : TranslatorService
    {

        public TranslatorService_Yandex()
        {

            this.Name = "Yandex";
            this.Fields.Add(new TranslatorServiceField("API Key", "trnsl.1.1.20150408T214800Z.14e18f16466443df.012cd433e247225cad264276715c83084b25e71e"));

        }

        override public string DetectLanguage(string text)
        {

            string msgLang = "";
            string APIKey = this.Fields.Find(x => x.Name == "API Key").Value;
            string detectURL = "https://translate.yandex.net/api/v1.5/tr.json/detect?key=" + APIKey + "&text=" + HttpUtility.UrlEncode(text);

            try
            {

                string detectResp = "";
                using (WebClient client = new WebClient())
                {
                    detectResp = client.DownloadString(detectURL);
                    JObject dtObject = JObject.Parse(detectResp);
                    msgLang = dtObject["lang"].ToString();
                }

            }
            catch (Exception e) { msgLang = "en"; } // Fallback to "en"

            return msgLang;

        }

        override public string Translate(string text, ref string from)
        {

            string transText = "";
            string APIKey = this.Fields.Find(x => x.Name=="API Key").Value;
            string transURL = "https://translate.yandex.net/api/v1.5/tr.json/translate?key=" + APIKey + "&lang=" + from + "-en&text=" + HttpUtility.UrlEncode(text);

            try
            {
                using (WebClient client = new WebClient())
                {
                    string transResp = client.DownloadString(transURL);
                    JObject trObject = JObject.Parse(transResp);
                    transText = trObject["text"][0].ToString();
                }
            }
            catch (Exception e) { transText = text; from = "ERROR"; }

            return transText.Length > 0 ? transText : text;

        }

    }

}
