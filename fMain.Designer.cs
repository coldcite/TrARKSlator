namespace TrARKSlator
{
    partial class fMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.tsddbPreferences = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbServices = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsslSeparator = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsddbOpacity = new System.Windows.Forms.ToolStripDropDownButton();
            this.tstbOpacity = new Microsoft.ParallelComputingPlatform.ParallelExtensions.Samples.ToolStripTrackBar();
            this.tsddbAlwaysOnTopOff = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbAlwaysOnTopOn = new System.Windows.Forms.ToolStripDropDownButton();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStripMain.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripMain
            // 
            this.statusStripMain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbPreferences,
            this.tsddbServices,
            this.tsslSeparator,
            this.tsddbOpacity,
            this.tsddbAlwaysOnTopOff,
            this.tsddbAlwaysOnTopOn});
            this.statusStripMain.Location = new System.Drawing.Point(0, 160);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.ShowItemToolTips = true;
            this.statusStripMain.Size = new System.Drawing.Size(298, 24);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 2;
            // 
            // tsddbPreferences
            // 
            this.tsddbPreferences.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbPreferences.Image = global::TrARKSlator.Properties.Resources.properties_16xSM;
            this.tsddbPreferences.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbPreferences.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsddbPreferences.Name = "tsddbPreferences";
            this.tsddbPreferences.ShowDropDownArrow = false;
            this.tsddbPreferences.Size = new System.Drawing.Size(20, 20);
            this.tsddbPreferences.ToolTipText = "Preferences";
            this.tsddbPreferences.Click += new System.EventHandler(this.tsddbPreferences_Click);
            // 
            // tsddbServices
            // 
            this.tsddbServices.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsddbServices.Image = global::TrARKSlator.Properties.Resources.Message_16xSM;
            this.tsddbServices.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbServices.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsddbServices.Name = "tsddbServices";
            this.tsddbServices.Size = new System.Drawing.Size(48, 20);
            this.tsddbServices.Text = "---";
            this.tsddbServices.ToolTipText = "Translation Service";
            // 
            // tsslSeparator
            // 
            this.tsslSeparator.BackColor = System.Drawing.Color.Transparent;
            this.tsslSeparator.Name = "tsslSeparator";
            this.tsslSeparator.Size = new System.Drawing.Size(162, 19);
            this.tsslSeparator.Spring = true;
            // 
            // tsddbOpacity
            // 
            this.tsddbOpacity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tstbOpacity});
            this.tsddbOpacity.Font = new System.Drawing.Font("Segoe UI Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsddbOpacity.Image = global::TrARKSlator.Properties.Resources.Template_Application_16xSM;
            this.tsddbOpacity.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbOpacity.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.tsddbOpacity.Name = "tsddbOpacity";
            this.tsddbOpacity.Size = new System.Drawing.Size(53, 20);
            this.tsddbOpacity.Text = "--%";
            this.tsddbOpacity.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsddbOpacity.ToolTipText = "Opacity";
            // 
            // tstbOpacity
            // 
            this.tstbOpacity.Minimum = 25;
            this.tstbOpacity.Name = "tstbOpacity";
            this.tstbOpacity.Size = new System.Drawing.Size(104, 16);
            this.tstbOpacity.Value = 100;
            this.tstbOpacity.ValueChanged += new System.EventHandler(this.tstbOpacity_ValueChanged);
            // 
            // tsddbAlwaysOnTopOff
            // 
            this.tsddbAlwaysOnTopOff.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tsddbAlwaysOnTopOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbAlwaysOnTopOff.Image = global::TrARKSlator.Properties.Resources.pushpin_16xSM;
            this.tsddbAlwaysOnTopOff.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbAlwaysOnTopOff.Margin = new System.Windows.Forms.Padding(0, 2, -10, 2);
            this.tsddbAlwaysOnTopOff.Name = "tsddbAlwaysOnTopOff";
            this.tsddbAlwaysOnTopOff.ShowDropDownArrow = false;
            this.tsddbAlwaysOnTopOff.Size = new System.Drawing.Size(20, 20);
            this.tsddbAlwaysOnTopOff.ToolTipText = "Disable Always-On-Top";
            this.tsddbAlwaysOnTopOff.Visible = false;
            this.tsddbAlwaysOnTopOff.Click += new System.EventHandler(this.tsddbAlwaysOnTopOff_Click);
            // 
            // tsddbAlwaysOnTopOn
            // 
            this.tsddbAlwaysOnTopOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbAlwaysOnTopOn.Image = global::TrARKSlator.Properties.Resources.unpin_16xSM;
            this.tsddbAlwaysOnTopOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbAlwaysOnTopOn.Margin = new System.Windows.Forms.Padding(0, 2, -10, 2);
            this.tsddbAlwaysOnTopOn.Name = "tsddbAlwaysOnTopOn";
            this.tsddbAlwaysOnTopOn.ShowDropDownArrow = false;
            this.tsddbAlwaysOnTopOn.Size = new System.Drawing.Size(20, 20);
            this.tsddbAlwaysOnTopOn.ToolTipText = "Enable Always-On-Top";
            this.tsddbAlwaysOnTopOn.Visible = false;
            this.tsddbAlwaysOnTopOn.Click += new System.EventHandler(this.tsddbAlwaysOnTopOn_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblAppTitle);
            this.pnlTop.Cursor = System.Windows.Forms.Cursors.Default;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(298, 22);
            this.pnlTop.TabIndex = 6;
            this.pnlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTop_MouseDown);
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.Location = new System.Drawing.Point(3, 4);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(66, 13);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "TrARKSlator";
            // 
            // pnlMid
            // 
            this.pnlMid.Controls.Add(this.txtLog);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMid.Location = new System.Drawing.Point(0, 22);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Size = new System.Drawing.Size(298, 138);
            this.pnlMid.TabIndex = 7;
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Margin = new System.Windows.Forms.Padding(35, 5, 5, 35);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtLog.Size = new System.Drawing.Size(298, 138);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDark;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::TrARKSlator.Properties.Resources.Symbols_Critical_32xSM;
            this.btnClose.Location = new System.Drawing.Point(258, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.btnClose.Size = new System.Drawing.Size(25, 15);
            this.btnClose.TabIndex = 5;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(298, 184);
            this.ControlBox = false;
            this.Controls.Add(this.pnlMid);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.pnlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(50, 50);
            this.MinimumSize = new System.Drawing.Size(250, 200);
            this.Name = "fMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.fMain_Paint);
            this.Resize += new System.EventHandler(this.fMain_Resize);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlMid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripDropDownButton tsddbServices;
        private System.Windows.Forms.ToolStripDropDownButton tsddbAlwaysOnTopOn;
        private System.Windows.Forms.ToolStripStatusLabel tsslSeparator;
        private System.Windows.Forms.ToolStripDropDownButton tsddbAlwaysOnTopOff;
        private System.Windows.Forms.ToolStripDropDownButton tsddbOpacity;
        private Microsoft.ParallelComputingPlatform.ParallelExtensions.Samples.ToolStripTrackBar tstbOpacity;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlMid;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.ToolStripDropDownButton tsddbPreferences;
    }
}