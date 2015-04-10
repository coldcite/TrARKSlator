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
using Hakusai.Pso2;

namespace TrARKSlator
{
    public partial class fMain : Form
    {

        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text += String.Format(" {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

            // Populate translation menu
            AvailableTranslationServices.List().ForEach(
                delegate(TranslatorService tr)
                {

                    var ttb = new ToolStripButton(tr.Name);
                    ttb.Click += new EventHandler(tsddbServicesItemHandler);
                    tsddbServices.DropDownItems.Add(ttb);

                    if ((Properties.Settings.Default.TranslatorEngine == tr.Name) || (AvailableTranslationServices.Active == tr))
                        ActivateService(tr.Name);

                });

            // Load settings
            this.Size = Properties.Settings.Default.FormSize;
            this.Location = Properties.Settings.Default.FormPos;
            this.OnResize(e);

            // Start the log watcher
            using (IPso2LogWatcherFactory factory = new Pso2LogWatcherFactory())
            using (IPso2LogWatcher watcher = factory.CreatePso2LogWatcher())
            {
                 watcher.Pso2LogEvent += (sndr, ev) => {

                     // That's a command, let's quit
                     if (ParsingSupport.isPSO2ChatCommand(ev.Message)) return;

                     // Let's remove all crap
                     ev.Message = ParsingSupport.chatCleanUp(ev.Message);

                     // If message didn't turned out all crap, let's display
                     if (ev.Message != "") addLine(ev); 
                 
                 };
                 watcher.Start();
             }

        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Saving settings
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.FormPos = this.Location;
            Properties.Settings.Default.TranslatorEngine = AvailableTranslationServices.Active.Name;
            Properties.Settings.Default.Save();
        }
        
        private void fMain_Resize(object sender, EventArgs e)
        {

            txtLog.Width = this.Width-21;
            txtLog.Height = this.Height-60;

        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length; txtLog.ScrollToCaret();
        }


        private void tsddbServicesItemHandler(object sender, EventArgs e) {

            ActivateService(sender.ToString());

        }

        private void ActivateService(string service)
        {

            // Update menu items
            foreach (ToolStripButton d in tsddbServices.DropDownItems) { d.Checked = (d.Text == service); }
            tsslActive.Text = service;
            
            // Update current active
            AvailableTranslationServices.Active = AvailableTranslationServices.List().Find(x => x.Name == service);

        }
        
        // Add chalog message to our log
        delegate void addLineCallback(Pso2LogEventArgs msg);
        public void addLine(Pso2LogEventArgs msg)
        {
            // Callback
            if (this.txtLog.InvokeRequired)
            {
                addLineCallback d = new addLineCallback(addLine);
                this.Invoke(d, new object[] { msg });
            }
            else
            {

                // Let's pick a color to print
                Color msgColor;
                switch (msg.SendTo) {
                    case "PARTY": msgColor = Color.FromArgb(76,228,255); break;
                    case "GUILD": msgColor = Color.FromArgb(255,165,0); break;
                    case "REPLY": msgColor = Color.FromArgb(255,135,204); break;
                    default: msgColor = Color.FromArgb(255,255,255); break;
                }

                TranslatorService tr = AvailableTranslationServices.Active;

                // Which language is it?
                string msgLang = tr.DetectLanguage(msg.Message);

                // If not EN, translate
                string transText = "";
                if (msgLang != "en") transText = tr.Translate(msg.Message, msgLang);

                // Let's add whatever
                // Only add translation if not english AND it's not the same text (sometimes gibberish gets translated because it's detected as some other language)
                txtLog.AppendText(msg.From + "\r\n", msgColor, 0, true);
                txtLog.AppendText(msg.Message + "\r\n", msgColor, 15);
                if ( (msgLang != "en") && (msg.Message != transText) ) 
                    txtLog.AppendText(transText + "\r\n", msgColor, 15, false, true, 12);

            }

        }



    }
}
