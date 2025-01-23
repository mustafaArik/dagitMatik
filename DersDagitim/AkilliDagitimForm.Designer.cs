namespace DersDagitim
{
    partial class AkilliDagitimForm
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
            this.prbYerlesmeYuzdesi = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYerlesmeYuzdesi = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblYerlesmeyenSayisi = new System.Windows.Forms.Label();
            this.lstYerlesmeyenler = new System.Windows.Forms.ListBox();
            this.lstEnZorOgretmenler = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // prbYerlesmeYuzdesi
            // 
            this.prbYerlesmeYuzdesi.Location = new System.Drawing.Point(12, 31);
            this.prbYerlesmeYuzdesi.Name = "prbYerlesmeYuzdesi";
            this.prbYerlesmeYuzdesi.Size = new System.Drawing.Size(317, 23);
            this.prbYerlesmeYuzdesi.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Yerleşme Yüzdesi : ";
            // 
            // lblYerlesmeYuzdesi
            // 
            this.lblYerlesmeYuzdesi.AutoSize = true;
            this.lblYerlesmeYuzdesi.Location = new System.Drawing.Point(117, 11);
            this.lblYerlesmeYuzdesi.Name = "lblYerlesmeYuzdesi";
            this.lblYerlesmeYuzdesi.Size = new System.Drawing.Size(21, 13);
            this.lblYerlesmeYuzdesi.TabIndex = 2;
            this.lblYerlesmeYuzdesi.Text = "%0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Yerleşmeyen Ders Sayısı : ";
            // 
            // lblYerlesmeyenSayisi
            // 
            this.lblYerlesmeyenSayisi.AutoSize = true;
            this.lblYerlesmeyenSayisi.Location = new System.Drawing.Point(298, 11);
            this.lblYerlesmeyenSayisi.Name = "lblYerlesmeyenSayisi";
            this.lblYerlesmeyenSayisi.Size = new System.Drawing.Size(13, 13);
            this.lblYerlesmeyenSayisi.TabIndex = 5;
            this.lblYerlesmeyenSayisi.Text = "0";
            // 
            // lstYerlesmeyenler
            // 
            this.lstYerlesmeyenler.FormattingEnabled = true;
            this.lstYerlesmeyenler.Location = new System.Drawing.Point(12, 80);
            this.lstYerlesmeyenler.Name = "lstYerlesmeyenler";
            this.lstYerlesmeyenler.Size = new System.Drawing.Size(317, 186);
            this.lstYerlesmeyenler.TabIndex = 6;
            // 
            // lstEnZorOgretmenler
            // 
            this.lstEnZorOgretmenler.FormattingEnabled = true;
            this.lstEnZorOgretmenler.Location = new System.Drawing.Point(12, 288);
            this.lstEnZorOgretmenler.Name = "lstEnZorOgretmenler";
            this.lstEnZorOgretmenler.Size = new System.Drawing.Size(187, 147);
            this.lstEnZorOgretmenler.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Yerleşmeyen Dersler";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "En Zor 10 Öğretmen";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 288);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 66);
            this.button2.TabIndex = 11;
            this.button2.Text = "İptal/Çık";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AkilliDagitimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 442);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstEnZorOgretmenler);
            this.Controls.Add(this.lstYerlesmeyenler);
            this.Controls.Add(this.lblYerlesmeyenSayisi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblYerlesmeYuzdesi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.prbYerlesmeYuzdesi);
            this.Name = "AkilliDagitimForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AkilliDagitimForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AkilliDagitimForm_FormClosing);
            this.Load += new System.EventHandler(this.AkilliDagitimForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar prbYerlesmeYuzdesi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYerlesmeYuzdesi;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblYerlesmeyenSayisi;
        private System.Windows.Forms.ListBox lstYerlesmeyenler;
        private System.Windows.Forms.ListBox lstEnZorOgretmenler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
    }
}