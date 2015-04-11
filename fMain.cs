using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Hakusai.Pso2;
using Microsoft.ParallelComputingPlatform.ParallelExtensions.Samples;
using System.Runtime.InteropServices;
using TextOnGlass;

namespace TrARKSlator
{
    public partial class fMain : Form
    {

        // Move with label

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public fMain()
        {

            InitializeComponent();

            if (GlassText.IsCompositionEnabled())
            {
                pnlTop.BackColor = Color.Black;
                lblAppTitle.Location = new Point(-10, -9);
                GlassText.MARGINS mg = new GlassText.MARGINS();
                mg.m_Buttom = 0; mg.m_Left = 0; mg.m_Right = 0; mg.m_Top = pnlTop.Height;
                GlassText.DwmExtendFrameIntoClientArea(this.Handle, ref mg);
            }
        }


        private void fMain_Load(object sender, EventArgs e)
        {

            // Title label
            lblAppTitle.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            // Populate translation menu
            AvailableTranslationServices.List().ForEach(
                delegate(TranslatorService tr)
                {

                    var ttb = new ToolStripButton(tr.Name);
                    ttb.Click += new EventHandler(tsddbGenericHandler);
                    tsddbServices.DropDownItems.Add(ttb);

                    if ((Properties.Settings.Default.TranslatorEngine == tr.Name) || (AvailableTranslationServices.Active == tr))
                        ActivateService(tr.Name);

                });

            // Load settings
            ToggleAlwaysOnTop(Properties.Settings.Default.AlwaysOnTop);
            SetOpacity(Properties.Settings.Default.Opacity);
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
            Properties.Settings.Default.AlwaysOnTop = this.TopMost;
            Properties.Settings.Default.Opacity = this.Opacity;
            Properties.Settings.Default.Save();
        }
        
        private void fMain_Resize(object sender, EventArgs e)
        {

            txtLog.Width = this.Width-17;
            txtLog.Height = this.Height-60;
            btnClose.Location= new Point(this.Width - 41, 0);

        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            txtLog.SelectionStart = txtLog.Text.Length; txtLog.ScrollToCaret();
        }

        private void tsddbAlwaysOnTopOff_Click(object sender, EventArgs e)
        {
            ToggleAlwaysOnTop(false);
        }

        private void tsddbAlwaysOnTopOn_Click(object sender, EventArgs e)
        {
            ToggleAlwaysOnTop(true);
        }

        private void tstbOpacity_ValueChanged(object sender, EventArgs e)
        {
            SetOpacity(Convert.ToDouble(this.tstbOpacity.Value) / 100, false);
        }

        private void pnlTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void fMain_Paint(object sender, PaintEventArgs e)
        {

            if (GlassText.IsCompositionEnabled())
            {

                e.Graphics.Clear(Color.Black);
                Rectangle clientArea = new Rectangle(0, pnlTop.Height, this.ClientRectangle.Width, this.ClientRectangle.Height - pnlTop.Height);
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);

                GlassText glasstxt = new GlassText();
                glasstxt.FillBlackRegion(pnlTop.CreateGraphics(), pnlTop.ClientRectangle);
                glasstxt.DrawTextOnGlass(pnlTop.Handle, lblAppTitle.Text, lblAppTitle.Font, lblAppTitle.Bounds, 10);

            }

        }



        // Generic handler for menu items
        private void tsddbGenericHandler(object sender, EventArgs e) {

            ActivateService(sender.ToString());

        }

        // Starts using translation service selected
        private void ActivateService(string service)
        {

            // Update menu items
            foreach (ToolStripButton d in tsddbServices.DropDownItems) { d.Checked = (d.Text == service); }
            tsddbServices.Text = service;
            
            // Update current active
            AvailableTranslationServices.Active = AvailableTranslationServices.List().Find(x => x.Name == service);

        }

        // Toggles always-on-top on/or
        private void ToggleAlwaysOnTop(bool state) {

            this.TopMost = state;

            tsddbAlwaysOnTopOff.Visible = state;
            tsddbAlwaysOnTopOn.Visible = !state;

        }

        // Sets opacity
        private void SetOpacity(double value, bool setControl=true)
        {

            tsddbOpacity.Text = Convert.ToInt32(value * 100).ToString() + "%";
            if (setControl) tstbOpacity.Value = Convert.ToInt32(value*100);
            this.Opacity = value;


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
