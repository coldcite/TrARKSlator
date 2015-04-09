using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fMain());
        }

    }


    // RTF Extension
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color, int indent=0, bool bold=false, bool italic = false)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.SelectionIndent = indent;
            if (bold) box.SelectionFont = new Font(box.Font, FontStyle.Bold);
            if (italic) box.SelectionFont = new Font(box.Font, FontStyle.Italic);
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }


}
