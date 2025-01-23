namespace DersDagitim
{
    partial class KontrolForm
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
            this.lstHatalar = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnKorDagitimYap = new System.Windows.Forms.Button();
            this.btnAkilliDagitim = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstHatalar
            // 
            this.lstHatalar.FormattingEnabled = true;
            this.lstHatalar.Location = new System.Drawing.Point(15, 26);
            this.lstHatalar.Name = "lstHatalar";
            this.lstHatalar.Size = new System.Drawing.Size(429, 446);
            this.lstHatalar.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hatalar";
            // 
            // btnKorDagitimYap
            // 
            this.btnKorDagitimYap.Enabled = false;
            this.btnKorDagitimYap.Location = new System.Drawing.Point(332, 478);
            this.btnKorDagitimYap.Name = "btnKorDagitimYap";
            this.btnKorDagitimYap.Size = new System.Drawing.Size(109, 44);
            this.btnKorDagitimYap.TabIndex = 4;
            this.btnKorDagitimYap.Text = "Kör Dağıtım Yap";
            this.btnKorDagitimYap.UseVisualStyleBackColor = true;
            this.btnKorDagitimYap.Visible = false;
            this.btnKorDagitimYap.Click += new System.EventHandler(this.btnDagitimYap_Click);
            // 
            // btnAkilliDagitim
            // 
            this.btnAkilliDagitim.Enabled = false;
            this.btnAkilliDagitim.Location = new System.Drawing.Point(130, 478);
            this.btnAkilliDagitim.Name = "btnAkilliDagitim";
            this.btnAkilliDagitim.Size = new System.Drawing.Size(196, 44);
            this.btnAkilliDagitim.TabIndex = 5;
            this.btnAkilliDagitim.Text = "Ders Dağıtımını Başlat";
            this.btnAkilliDagitim.UseVisualStyleBackColor = true;
            this.btnAkilliDagitim.Click += new System.EventHandler(this.btnAkilliDagitim_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 478);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 44);
            this.button1.TabIndex = 6;
            this.button1.Text = "Havuzlu Tarama";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // KontrolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 534);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAkilliDagitim);
            this.Controls.Add(this.btnKorDagitimYap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstHatalar);
            this.Name = "KontrolForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ders Programı Kontrol";
            this.Load += new System.EventHandler(this.KontrolForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstHatalar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnKorDagitimYap;
        private System.Windows.Forms.Button btnAkilliDagitim;
        private System.Windows.Forms.Button button1;
    }
}