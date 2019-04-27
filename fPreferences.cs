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

namespace TrARKSlator
{
    public partial class fPreferences : Form
    {

        public fPreferences()
        {
            InitializeComponent();
        }

        private void fPreferences_Load(object sender, EventArgs e)
        {

            // Log Font
            fdFont.Font = Properties.Settings.Default.Font;
            btnFont.Text = fdFont.Font.Name + ", " + fdFont.Font.Size;
            btnFont.Font = fdFont.Font;

            // Colors
            btnColorDefault.BackColor = Properties.Settings.Default.ColorDefault;
            btnColorDefault.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.ColorDefault;
            btnColorParty.BackColor = Properties.Settings.Default.ColorParty;
            btnColorParty.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.ColorParty;
            btnColorGuild.BackColor = Properties.Settings.Default.ColorGuild;
            btnColorGuild.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.ColorGuild;
            btnColorReply.BackColor = Properties.Settings.Default.ColorReply;
            btnColorReply.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.ColorReply;
            btnColorGroup.BackColor = Properties.Settings.Default.ColorGroup;
            btnColorGroup.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.ColorGroup;
            btnColorBackground.BackColor = Properties.Settings.Default.ColorBackground;
            btnColorBackground.FlatAppearance.MouseOverBackColor = Properties.Settings.Default.ColorBackground;

        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            if (fdFont.ShowDialog() == DialogResult.OK)
            {
                btnFont.Text = fdFont.Font.Name + ", " + fdFont.Font.Size;
                btnFont.Font = fdFont.Font;
            }
        }

        private void generic_btnColor_Click(object sender, EventArgs e)
        {

            Button thisBtn = (Button)sender;
            cdColor.Color = thisBtn.BackColor;
            if (cdColor.ShowDialog() == DialogResult.OK)
            {
                thisBtn.BackColor = cdColor.Color;
                thisBtn.FlatAppearance.MouseOverBackColor = cdColor.Color;
            }

        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.Font = fdFont.Font;
            Properties.Settings.Default.ColorDefault = btnColorDefault.BackColor;
            Properties.Settings.Default.ColorParty = btnColorParty.BackColor;
            Properties.Settings.Default.ColorGuild = btnColorGuild.BackColor;
            Properties.Settings.Default.ColorReply = btnColorReply.BackColor;
            Properties.Settings.Default.ColorGroup = btnColorGroup.BackColor;
            Properties.Settings.Default.ColorBackground = btnColorBackground.BackColor;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void btnFont_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { 
                fdFont.Font = new Font("Segoe UI", 9);
                btnFont.Font = fdFont.Font;
                btnFont.Text = fdFont.Font.Name + ", " + fdFont.Font.Size;
            }
        }

        private void generic_btnColor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button thisBtn = (Button)sender;
                Color defColor = Color.White;
                switch (thisBtn.Name)
                {
                    case "btnColorDefault": defColor = Color.White; break;
                    case "btnColorParty": defColor = Color.FromArgb(76,228,255); break;
                    case "btnColorGuild": defColor = Color.Orange; break;
                    case "btnColorReply": defColor = Color.FromArgb(255, 135, 204); break;
                    case "btnColorGroup": defColor = Color.FromArgb(255, 150, 240, 85); break;
                    case "btnColorBackground": defColor = Color.FromArgb(64, 64, 64); break;
                }

                thisBtn.BackColor = defColor;
                thisBtn.FlatAppearance.MouseOverBackColor = defColor;

            }
        }

    }
}
