using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;
using System.Web;
using Hakusai.Pso2;

namespace TrARKSlator
{
    public partial class fMain : Form
    {

        static readonly string YandexAPIKey = "trnsl.1.1.20150408T214800Z.14e18f16466443df.012cd433e247225cad264276715c83084b25e71e";

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {

            this.Size = Properties.Settings.Default.FormSize;
            this.Location = Properties.Settings.Default.FormPos;
            this.OnResize(e);

            using (IPso2LogWatcherFactory factory = new Pso2LogWatcherFactory())
            using (IPso2LogWatcher watcher = factory.CreatePso2LogWatcher())
            {
                 watcher.Pso2LogEvent += (sndr, ev) => { addLine(ev); };
                 watcher.Start();
             }

        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.FormPos = this.Location;
            Properties.Settings.Default.Save();
        }
        
        private void fMain_Resize(object sender, EventArgs e)
        {

            txtLog.Width = this.Width-21;
            txtLog.Height = this.Height-60;

            lnkYandex.Location = new Point(this.Width - lnkYandex.Width-15, this.Height - lnkYandex.Height-40);

        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length; txtLog.ScrollToCaret();
        }



        // Add chalog message to log
        delegate void addLineCallback(Pso2LogEventArgs msg);
        public void addLine(Pso2LogEventArgs msg)
        {

            // That's a command, let's ignore it
            if ( msg.Message.StartsWith("/la") ||
                 msg.Message.StartsWith("/ci") ||
                 msg.Message.StartsWith("/vo") ||
                 msg.Message.StartsWith("/pal") || 
                 msg.Message.StartsWith("/mpal") ) 
            return;

            // Callback
            if (this.txtLog.InvokeRequired) {
                addLineCallback d = new addLineCallback(addLine);
                this.Invoke(d, new object[] { msg });
            } else {

                Color msgColor;
                switch (msg.SendTo) {
                    case "PARTY": msgColor = Color.FromArgb(76,228,255); break;
                    case "GUILD": msgColor = Color.FromArgb(255,165,0); break;
                    case "REPLY": msgColor = Color.FromArgb(255,135,204); break;
                    default: msgColor = Color.FromArgb(255,255,255); break;
                }

                // Is it not EN?
                string msgLang = "??";
                string detectURL = "https://translate.yandex.net/api/v1.5/tr/detect?key=" + YandexAPIKey + "&text=" + HttpUtility.UrlEncode(msg.Message);
                XmlReader xmlDetect = XmlReader.Create(detectURL);
                while (xmlDetect.Read()) {
                    if ((xmlDetect.NodeType == XmlNodeType.Element) && (xmlDetect.Name == "DetectedLang"))
                    { msgLang = xmlDetect.GetAttribute("lang"); if ((msgLang == "") || (msgLang == "zh")) msgLang = "ja"; } // Sometimes Yandex mistakes CH for JP
                }

                // If not, just translate
                string transText = "";
                if (msgLang != "en") {

                    string transURL = "https://translate.yandex.net/api/v1.5/tr/translate?key=" + YandexAPIKey + "&lang=" + msgLang + "-en&text=" + HttpUtility.UrlEncode(msg.Message);
                    XmlReader xmlTrans = XmlReader.Create(transURL);
                    while (xmlTrans.Read())
                    {
                        if ((xmlTrans.NodeType == XmlNodeType.Text))
                        { transText += xmlTrans.Value; }
                    }
                
                }


                txtLog.AppendText(msg.From + "\r\n", msgColor, 0, true);
                txtLog.AppendText(msg.Message + "\r\n", msgColor, 15);
                if (msgLang != "en")
                    txtLog.AppendText("[" + msgLang.ToUpper() + "] " + transText + "\r\n", msgColor, 15, false, true);

            } 

        }

        private void lnkYandex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://translate.yandex.com/");
        }




    }
}
