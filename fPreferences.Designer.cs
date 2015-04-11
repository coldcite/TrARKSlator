namespace TrARKSlator
{
    partial class fPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnFont = new System.Windows.Forms.Button();
            this.fdFont = new System.Windows.Forms.FontDialog();
            this.lblFont = new System.Windows.Forms.Label();
            this.cdColor = new System.Windows.Forms.ColorDialog();
            this.btnColorDefault = new System.Windows.Forms.Button();
            this.lblColorDefault = new System.Windows.Forms.Label();
            this.lblColorParty = new System.Windows.Forms.Label();
            this.btnColorParty = new System.Windows.Forms.Button();
            this.lblColorGuild = new System.Windows.Forms.Label();
            this.btnColorGuild = new System.Windows.Forms.Button();
            this.lblColorReply = new System.Windows.Forms.Label();
            this.btnColorReply = new System.Windows.Forms.Button();
            this.lblColorBackground = new System.Windows.Forms.Label();
            this.btnColorBackground = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.ttProtip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnFont
            // 
            this.btnFont.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnFont.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFont.Location = new System.Drawing.Point(12, 25);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(260, 50);
            this.btnFont.TabIndex = 0;
            this.ttProtip.SetToolTip(this.btnFont, "Right click to reset.");
            this.btnFont.UseVisualStyleBackColor = true;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            this.btnFont.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnFont_MouseDown);
            // 
            // fdFont
            // 
            this.fdFont.AllowScriptChange = false;
            this.fdFont.ShowApply = true;
            this.fdFont.ShowEffects = false;
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(12, 9);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(53, 13);
            this.lblFont.TabIndex = 1;
            this.lblFont.Text = "Log Font";
            // 
            // btnColorDefault
            // 
            this.btnColorDefault.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorDefault.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorDefault.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnColorDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorDefault.Location = new System.Drawing.Point(12, 106);
            this.btnColorDefault.Name = "btnColorDefault";
            this.btnColorDefault.Size = new System.Drawing.Size(260, 23);
            this.btnColorDefault.TabIndex = 2;
            this.ttProtip.SetToolTip(this.btnColorDefault, "Right click to reset.");
            this.btnColorDefault.UseVisualStyleBackColor = true;
            this.btnColorDefault.Click += new System.EventHandler(this.generic_btnColor_Click);
            this.btnColorDefault.MouseDown += new System.Windows.Forms.MouseEventHandler(this.generic_btnColor_MouseDown);
            // 
            // lblColorDefault
            // 
            this.lblColorDefault.AutoSize = true;
            this.lblColorDefault.Location = new System.Drawing.Point(12, 90);
            this.lblColorDefault.Name = "lblColorDefault";
            this.lblColorDefault.Size = new System.Drawing.Size(43, 13);
            this.lblColorDefault.TabIndex = 3;
            this.lblColorDefault.Text = "Nearby";
            // 
            // lblColorParty
            // 
            this.lblColorParty.AutoSize = true;
            this.lblColorParty.Location = new System.Drawing.Point(12, 132);
            this.lblColorParty.Name = "lblColorParty";
            this.lblColorParty.Size = new System.Drawing.Size(32, 13);
            this.lblColorParty.TabIndex = 5;
            this.lblColorParty.Text = "Party";
            // 
            // btnColorParty
            // 
            this.btnColorParty.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorParty.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorParty.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnColorParty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorParty.Location = new System.Drawing.Point(12, 148);
            this.btnColorParty.Name = "btnColorParty";
            this.btnColorParty.Size = new System.Drawing.Size(260, 23);
            this.btnColorParty.TabIndex = 4;
            this.ttProtip.SetToolTip(this.btnColorParty, "Right click to reset.");
            this.btnColorParty.UseVisualStyleBackColor = true;
            this.btnColorParty.Click += new System.EventHandler(this.generic_btnColor_Click);
            this.btnColorParty.MouseDown += new System.Windows.Forms.MouseEventHandler(this.generic_btnColor_MouseDown);
            // 
            // lblColorGuild
            // 
            this.lblColorGuild.AutoSize = true;
            this.lblColorGuild.Location = new System.Drawing.Point(12, 174);
            this.lblColorGuild.Name = "lblColorGuild";
            this.lblColorGuild.Size = new System.Drawing.Size(33, 13);
            this.lblColorGuild.TabIndex = 7;
            this.lblColorGuild.Text = "Team";
            // 
            // btnColorGuild
            // 
            this.btnColorGuild.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorGuild.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorGuild.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnColorGuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorGuild.Location = new System.Drawing.Point(12, 190);
            this.btnColorGuild.Name = "btnColorGuild";
            this.btnColorGuild.Size = new System.Drawing.Size(260, 23);
            this.btnColorGuild.TabIndex = 6;
            this.ttProtip.SetToolTip(this.btnColorGuild, "Right click to reset.");
            this.btnColorGuild.UseVisualStyleBackColor = true;
            this.btnColorGuild.Click += new System.EventHandler(this.generic_btnColor_Click);
            this.btnColorGuild.MouseDown += new System.Windows.Forms.MouseEventHandler(this.generic_btnColor_MouseDown);
            // 
            // lblColorReply
            // 
            this.lblColorReply.AutoSize = true;
            this.lblColorReply.Location = new System.Drawing.Point(12, 216);
            this.lblColorReply.Name = "lblColorReply";
            this.lblColorReply.Size = new System.Drawing.Size(50, 13);
            this.lblColorReply.TabIndex = 9;
            this.lblColorReply.Text = "Whisper";
            // 
            // btnColorReply
            // 
            this.btnColorReply.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorReply.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorReply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnColorReply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorReply.Location = new System.Drawing.Point(12, 232);
            this.btnColorReply.Name = "btnColorReply";
            this.btnColorReply.Size = new System.Drawing.Size(260, 23);
            this.btnColorReply.TabIndex = 8;
            this.ttProtip.SetToolTip(this.btnColorReply, "Right click to reset.");
            this.btnColorReply.UseVisualStyleBackColor = true;
            this.btnColorReply.Click += new System.EventHandler(this.generic_btnColor_Click);
            this.btnColorReply.MouseDown += new System.Windows.Forms.MouseEventHandler(this.generic_btnColor_MouseDown);
            // 
            // lblColorBackground
            // 
            this.lblColorBackground.AutoSize = true;
            this.lblColorBackground.Location = new System.Drawing.Point(12, 270);
            this.lblColorBackground.Name = "lblColorBackground";
            this.lblColorBackground.Size = new System.Drawing.Size(70, 13);
            this.lblColorBackground.TabIndex = 11;
            this.lblColorBackground.Text = "Background";
            // 
            // btnColorBackground
            // 
            this.btnColorBackground.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorBackground.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnColorBackground.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnColorBackground.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorBackground.Location = new System.Drawing.Point(12, 286);
            this.btnColorBackground.Name = "btnColorBackground";
            this.btnColorBackground.Size = new System.Drawing.Size(260, 23);
            this.btnColorBackground.TabIndex = 10;
            this.ttProtip.SetToolTip(this.btnColorBackground, "Right click to reset.");
            this.btnColorBackground.UseVisualStyleBackColor = true;
            this.btnColorBackground.Click += new System.EventHandler(this.generic_btnColor_Click);
            this.btnColorBackground.MouseDown += new System.Windows.Forms.MouseEventHandler(this.generic_btnColor_MouseDown);
            // 
            // btnApply
            // 
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnApply.Location = new System.Drawing.Point(197, 327);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // fPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblColorBackground);
            this.Controls.Add(this.btnColorBackground);
            this.Controls.Add(this.lblColorReply);
            this.Controls.Add(this.btnColorReply);
            this.Controls.Add(this.lblColorGuild);
            this.Controls.Add(this.btnColorGuild);
            this.Controls.Add(this.lblColorParty);
            this.Controls.Add(this.btnColorParty);
            this.Controls.Add(this.lblColorDefault);
            this.Controls.Add(this.btnColorDefault);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.btnFont);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fPreferences";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.Load += new System.EventHandler(this.fPreferences_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFont;
        private System.Windows.Forms.FontDialog fdFont;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.ColorDialog cdColor;
        private System.Windows.Forms.Button btnColorDefault;
        private System.Windows.Forms.Label lblColorDefault;
        private System.Windows.Forms.Label lblColorParty;
        private System.Windows.Forms.Button btnColorParty;
        private System.Windows.Forms.Label lblColorGuild;
        private System.Windows.Forms.Button btnColorGuild;
        private System.Windows.Forms.Label lblColorReply;
        private System.Windows.Forms.Button btnColorReply;
        private System.Windows.Forms.Label lblColorBackground;
        private System.Windows.Forms.Button btnColorBackground;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ToolTip ttProtip;

    }
}