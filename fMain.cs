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

        // MouseLeave trick
        Timer ptOnWinTimer = new Timer();



        public fMain()
        {

            InitializeComponent();

            // Aero trick
            if (GlassText.IsCompositionEnabled())
            {
                pnlTop.BackColor = Color.Black;
                lblAppTitle.Location = new Point(-5, -5); lblAppTitle.Visible = false;
                GlassText.MARGINS mg = new GlassText.MARGINS();
                mg.m_Buttom = 0; mg.m_Left = 0; mg.m_Right = 0; mg.m_Top = pnlTop.Height;
                GlassText.DwmExtendFrameIntoClientArea(this.Handle, ref mg);
            }

            // MouseLeave trick
            ptOnWinTimer.Tick += new EventHandler(ptOnWinTimer_Tick);
            ptOnWinTimer.Interval = 50;
            ptOnWinTimer.Enabled = true;

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
            toggleAlwaysOnTop(Properties.Settings.Default.AlwaysOnTop);
            SetOpacity(Properties.Settings.Default.Opacity);
            this.Size = Properties.Settings.Default.FormSize;
            this.Location = Properties.Settings.Default.FormPos;
            txtLog.BackColor = Properties.Settings.Default.ColorBackground;
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
            Debug.WriteLine(txtLog.GetPositionFromCharIndex(0));
            txtLog.SelectionStart = txtLog.Text.Length; txtLog.ScrollToCaret();
        }

        private void tsddbAlwaysOnTopOff_Click(object sender, EventArgs e)
        {
            toggleAlwaysOnTop(false);
        }

        private void tsddbAlwaysOnTopOn_Click(object sender, EventArgs e)
        {
            toggleAlwaysOnTop(true);
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
                glasstxt.DrawTextOnGlass(pnlTop.Handle, lblAppTitle.Text, lblAppTitle.Font, lblAppTitle.Bounds, 5);

            }

        }

        private void ptOnWinTimer_Tick(object sender, EventArgs e)
        {
            Point pos = Control.MousePosition;
            bool inForm = pos.X >= Left && pos.Y >= Top && pos.X < Right && pos.Y < Bottom;
            if (!inForm) {
                if (this.TopMost && haveBorders()) toggleBordersOnly(true);
            } else { if (!haveBorders()) toggleBordersOnly(false); }
        }

        private void tsddbPreferences_Click(object sender, EventArgs e)
        {
            fPreferences pref = new fPreferences();
            pref.TopMost = this.TopMost;
            if (pref.ShowDialog() == DialogResult.OK) txtLog.BackColor = Properties.Settings.Default.ColorBackground;
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
        private void toggleAlwaysOnTop(bool state) {

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
            this.Invalidate();

        }

        // Toggle border only
        private void toggleBordersOnly(bool state)
        {
            Debug.WriteLine("toggleBordersOnly");

            pnlTop.Height = state ? 0 : 22;
            statusStripMain.Visible = !state;

            GlassText.MARGINS mg = new GlassText.MARGINS();
            mg.m_Buttom = 0; mg.m_Left = 0; mg.m_Right = 0; mg.m_Top = pnlTop.Height;
            GlassText.DwmExtendFrameIntoClientArea(this.Handle, ref mg);
            this.Invalidate();

        }
        private bool haveBorders()
        {
            return pnlTop.Height != 0;
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
                    case "PARTY": msgColor = Properties.Settings.Default.ColorParty; break;
                    case "GUILD": msgColor = Properties.Settings.Default.ColorGuild; break;
                    case "REPLY": msgColor = Properties.Settings.Default.ColorReply; break;
                    default: msgColor = Properties.Settings.Default.ColorDefault; break;
                }

                TranslatorService tr = AvailableTranslationServices.Active;

                // Which language is it?
                string msgLang = tr.DetectLanguage(msg.Message);

                // If not EN, translate
                string transText = "";
                if (msgLang != "en") transText = tr.Translate(msg.Message, msgLang);

                // Let's add whatever
                txtLog.AppendText(msg.From + "\r\n", msgColor, 0, true);
                txtLog.AppendText(msg.Message + "\r\n", msgColor, 15);
                if ((msgLang != "en") && (msg.Message != transText))      // Sometimes gibberish gets translated because it's detected as some other language
                    txtLog.AppendText(transText + "\r\n", msgColor, 15, false, true, 0.7F);

            }

        }



    }
}
