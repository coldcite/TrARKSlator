using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrARKSlator
{
    public class TranslatorService_FreeTranslation_com : TranslatorService
    {

        public TranslatorService_FreeTranslation_com()
        {

            this.Name = "FreeTranslation.com (JP only)";

        }

        override public string DetectLanguage(string text)
        {

            return ParsingSupport.isJapanese(text) ? "jpn" : "en";

        }

        override public string Translate(string text, ref string from)
        {

            string transText = "";
            string transURL = "http://www.freetranslation.com/gw-mt-proxy-service-web/mt-translation";


            try
            {

                using (WebClient client = new WebClient())
                {

                    NameValueCollection headers = new NameValueCollection() 
                    { 
                        { "Content-Type", "application/json; charset=UTF-8" },
                        { "Tracking", "applicationKey=dlWbNAC2iLJWujbcIHiNMQ%3D%3D applicationInstance=freetranslation" }   // Public translarion service
                    };
                    client.Headers.Add(headers);

                    JObject postData = new JObject();
                    postData["text"] = HttpUtility.UrlEncode(text);
                    postData["from"] = from;
                    postData["to"] = "eng";

                    byte[] b_transResp = client.UploadData(transURL, Encoding.UTF8.GetBytes(postData.ToString()));
                    string transResp = Encoding.UTF8.GetString(b_transResp);
                    JObject trObject = JObject.Parse(transResp);
                    transText = trObject["translation"].ToString().TrimStart();

                }

            }
            catch (Exception e) { transText = text; from = "ERROR"; }

            return transText.Length > 0 ? transText : text;

        }

    }

}
