using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Xml;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrARKSlator
{
    public class TranslatorService_Google : TranslatorService
    {

        public TranslatorService_Google()
        {

            this.Name = "Google";

        }

        override public string DetectLanguage(string text)
        {

            return "auto";

        }

        override public string Translate(string text, string from)
        {

            string transURL = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + from + "&tl=en&dt=t&ie=UTF-8&oe=UTF-8&q=" + HttpUtility.UrlEncode( text );

            string transResp = "";
            using(WebClient client = new WebClient()) {
               transResp = client.DownloadString(transURL);
            }
            JArray trObject = JArray.Parse(transResp);
            string transText = trObject[0][0][0].ToString();

            return transText.Length > 0 ? transText : text;

        }

    }

}
