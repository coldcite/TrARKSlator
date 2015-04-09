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
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.lnkYandex = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.Gray;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.White;
            this.txtLog.Location = new System.Drawing.Point(5, 0);
            this.txtLog.Margin = new System.Windows.Forms.Padding(35, 5, 5, 35);
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txtLog.Size = new System.Drawing.Size(100, 96);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // lnkYandex
            // 
            this.lnkYandex.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkYandex.AutoSize = true;
            this.lnkYandex.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkYandex.LinkColor = System.Drawing.Color.Blue;
            this.lnkYandex.Location = new System.Drawing.Point(243, 349);
            this.lnkYandex.Name = "lnkYandex";
            this.lnkYandex.Size = new System.Drawing.Size(224, 16);
            this.lnkYandex.TabIndex = 2;
            this.lnkYandex.TabStop = true;
            this.lnkYandex.Text = "Powered by Yandex.Translate";
            this.lnkYandex.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkYandex.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkYandex_LinkClicked);
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(609, 376);
            this.Controls.Add(this.lnkYandex);
            this.Controls.Add(this.txtLog);
            this.Location = new System.Drawing.Point(50, 50);
            this.Name = "fMain";
            this.Text = "TrARKSlator 0.01";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fMain_FormClosing);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.Resize += new System.EventHandler(this.fMain_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.LinkLabel lnkYandex;
    }
}