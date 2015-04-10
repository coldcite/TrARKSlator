using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Web;

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
            string detectURL = "https://translate.yandex.net/api/v1.5/tr/detect?key=" + APIKey + "&text=" + HttpUtility.UrlEncode(text);
            XmlReader xmlDetect = XmlReader.Create(detectURL);
            while (xmlDetect.Read())
            {
                if ((xmlDetect.NodeType == XmlNodeType.Element) && (xmlDetect.Name == "DetectedLang"))
                { msgLang = xmlDetect.GetAttribute("lang"); if ((msgLang == "") || (msgLang == "zh")) msgLang = "ja"; } // Sometimes Yandex mistakes CH for JP, so let's force it for now
            }

            return msgLang;

        }

        override public string Translate(string text, string from)
        {

            string transText = "";
            string APIKey = this.Fields.Find(x => x.Name=="API Key").Value;
            string transURL = "https://translate.yandex.net/api/v1.5/tr/translate?key=" + APIKey + "&lang=" + from + "-en&text=" + HttpUtility.UrlEncode(text);
            XmlReader xmlTrans = XmlReader.Create(transURL);
            while (xmlTrans.Read())
            {
                if ((xmlTrans.NodeType == XmlNodeType.Text))
                { transText += xmlTrans.Value; }
            }

            return transText.Length > 0 ? transText : text;

        }

    }

}
