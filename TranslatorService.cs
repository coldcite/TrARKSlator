using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrARKSlator
{

    public abstract class TranslatorService
    {

        public string Name { get; set; }

        public TranslatorService(){
            AvailableTranslationServices.Add(this);
        }

        void Enable()
        {
            AvailableTranslationServices.Active = this;
        }

        abstract public string DetectLanguage(string text);
        abstract public string Translate(string text, string from);

        public List<TranslatorServiceField> Fields = new List<TranslatorServiceField>();

    }

    public class TranslatorServiceField
    {

        public TranslatorServiceField(string Name, string Value) {
            this.Name = Name;
            this.Value = Value;
        }

        public string Name { get; set; }
        public string Value { get; set; }

    }

    public static class AvailableTranslationServices
    {

        static List<TranslatorService> _list;
        static public TranslatorService Active = null;

        static AvailableTranslationServices()
        {
            _list = new List<TranslatorService>();
        }

        public static void Add(TranslatorService service)
        {
            _list.Add(service);
            if (Active == null) Active = service;
        }

        public static List<TranslatorService> List()
        {
            return _list;
        }


    }

    public class TranslatorService_Dummy : TranslatorService
    {
        public TranslatorService_Dummy()
        {
            this.Name = "No translation";
        }

        override public string DetectLanguage(string text)
        {
            return "en";
        }
        override public string Translate(string text, string from)
        {
            return text;
        }

    }

}


