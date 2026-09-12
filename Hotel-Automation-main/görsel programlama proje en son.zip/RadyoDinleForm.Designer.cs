namespace görsel_programlama_proje_en_son
{
    partial class RadyoDinleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RadyoDinleForm));
            this.BtnVirgin = new System.Windows.Forms.Button();
            this.BtnSlow = new System.Windows.Forms.Button();
            this.BtnKral = new System.Windows.Forms.Button();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnVirgin
            // 
            this.BtnVirgin.Font = new System.Drawing.Font("Franklin Gothic Heavy", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnVirgin.Location = new System.Drawing.Point(248, 413);
            this.BtnVirgin.Name = "BtnVirgin";
            this.BtnVirgin.Size = new System.Drawing.Size(107, 32);
            this.BtnVirgin.TabIndex = 1;
            this.BtnVirgin.Text = "90 LAR RADYO";
            this.BtnVirgin.UseVisualStyleBackColor = true;
            this.BtnVirgin.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnSlow
            // 
            this.BtnSlow.Font = new System.Drawing.Font("Franklin Gothic Heavy", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnSlow.Location = new System.Drawing.Point(466, 413);
            this.BtnSlow.Name = "BtnSlow";
            this.BtnSlow.Size = new System.Drawing.Size(116, 32);
            this.BtnSlow.TabIndex = 2;
            this.BtnSlow.Text = "SLOW TURK";
            this.BtnSlow.UseVisualStyleBackColor = true;
            this.BtnSlow.Click += new System.EventHandler(this.button2_Click);
            // 
            // BtnKral
            // 
            this.BtnKral.Font = new System.Drawing.Font("Franklin Gothic Heavy", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BtnKral.Location = new System.Drawing.Point(33, 413);
            this.BtnKral.Name = "BtnKral";
            this.BtnKral.Size = new System.Drawing.Size(92, 32);
            this.BtnKral.TabIndex = 3;
            this.BtnKral.Text = "KRAL POP";
            this.BtnKral.UseVisualStyleBackColor = true;
            this.BtnKral.Click += new System.EventHandler(this.button3_Click);
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(-4, -1);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(662, 411);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            this.axWindowsMediaPlayer1.Enter += new System.EventHandler(this.axWindowsMediaPlayer1_Enter);
            // 
            // RadyoDinleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(658, 463);
            this.Controls.Add(this.BtnKral);
            this.Controls.Add(this.BtnSlow);
            this.Controls.Add(this.BtnVirgin);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RadyoDinleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RadyoDinleForm";
            this.Load += new System.EventHandler(this.RadyoDinleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Button BtnVirgin;
        private System.Windows.Forms.Button BtnSlow;
        private System.Windows.Forms.Button BtnKral;
    }
}