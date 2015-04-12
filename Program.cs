using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Drawing;

namespace TrARKSlator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Let's create TranslatorServices instances
            TranslatorService_Dummy tlDummy = new TranslatorService_Dummy();
            TranslatorService_Google tlGoogle = new TranslatorService_Google();
            TranslatorService_Bing tlBing = new TranslatorService_Bing();
            TranslatorService_Yandex tlYandex = new TranslatorService_Yandex();
            TranslatorService_Babylon tlBabylon = new TranslatorService_Babylon();
            TranslatorService_FreeTranslation_com tlFreeTrans_com = new TranslatorService_FreeTranslation_com();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fMain());

        }

    }



    // RTF Extension
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, int indent=0, bool bold=false, bool italic = false, float fontScale=1)
        {

            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionIndent = indent;
            box.SelectionFont = new Font(
                Properties.Settings.Default.Font.FontFamily,
                Properties.Settings.Default.Font.Size * fontScale,
                Properties.Settings.Default.Font.Style | (bold ? FontStyle.Bold : 0) | (italic ? FontStyle.Italic : 0)
            );

            box.AppendText(text);

            //box.SelectionColor = box.ForeColor;

        }
    }



}
