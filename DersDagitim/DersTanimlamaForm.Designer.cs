namespace DersDagitim
{
    partial class DersTanimlamaForm
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
            this.cmbDersler = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOgretmenSil = new System.Windows.Forms.Button();
            this.btnOgretmenEkle = new System.Windows.Forms.Button();
            this.lstOgretmenler = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbOgretmenler = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDerslikSil = new System.Windows.Forms.Button();
            this.btnDerslikEkle = new System.Windows.Forms.Button();
            this.lstDerslikler = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDerslikler = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSinifGrupSil = new System.Windows.Forms.Button();
            this.btnSinifGrupEkle = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGruplar = new System.Windows.Forms.ComboBox();
            this.lstSinifGrup = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSiniflar = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtYerlesimSekli = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtToplamDersSaati = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTanimliDersEkle = new System.Windows.Forms.Button();
            this.btnYardim = new System.Windows.Forms.Button();
            this.btnEkleDevam = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbDersler
            // 
            this.cmbDersler.BackColor = System.Drawing.Color.MistyRose;
            this.cmbDersler.FormattingEnabled = true;
            this.cmbDersler.Location = new System.Drawing.Point(82, 15);
            this.cmbDersler.Name = "cmbDersler";
            this.cmbDersler.Size = new System.Drawing.Size(463, 21);
            this.cmbDersler.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dersi Seçin";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbDersler);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(562, 54);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnOgretmenSil);
            this.panel2.Controls.Add(this.btnOgretmenEkle);
            this.panel2.Controls.Add(this.lstOgretmenler);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbOgretmenler);
            this.panel2.Location = new System.Drawing.Point(12, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(278, 160);
            this.panel2.TabIndex = 2;
            // 
            // btnOgretmenSil
            // 
            this.btnOgretmenSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOgretmenSil.Location = new System.Drawing.Point(186, 35);
            this.btnOgretmenSil.Name = "btnOgretmenSil";
            this.btnOgretmenSil.Size = new System.Drawing.Size(75, 23);
            this.btnOgretmenSil.TabIndex = 2;
            this.btnOgretmenSil.Text = "Sil";
            this.btnOgretmenSil.UseVisualStyleBackColor = true;
            this.btnOgretmenSil.Click += new System.EventHandler(this.btnOgretmenSil_Click);
            // 
            // btnOgretmenEkle
            // 
            this.btnOgretmenEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOgretmenEkle.Location = new System.Drawing.Point(82, 35);
            this.btnOgretmenEkle.Name = "btnOgretmenEkle";
            this.btnOgretmenEkle.Size = new System.Drawing.Size(75, 23);
            this.btnOgretmenEkle.TabIndex = 1;
            this.btnOgretmenEkle.Text = "Ekle";
            this.btnOgretmenEkle.UseVisualStyleBackColor = true;
            this.btnOgretmenEkle.Click += new System.EventHandler(this.btnOgretmenEkle_Click);
            // 
            // lstOgretmenler
            // 
            this.lstOgretmenler.BackColor = System.Drawing.Color.MistyRose;
            this.lstOgretmenler.FormattingEnabled = true;
            this.lstOgretmenler.Location = new System.Drawing.Point(82, 64);
            this.lstOgretmenler.Name = "lstOgretmenler";
            this.lstOgretmenler.Size = new System.Drawing.Size(179, 82);
            this.lstOgretmenler.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Öğretmen";
            // 
            // cmbOgretmenler
            // 
            this.cmbOgretmenler.FormattingEnabled = true;
            this.cmbOgretmenler.Location = new System.Drawing.Point(82, 11);
            this.cmbOgretmenler.Name = "cmbOgretmenler";
            this.cmbOgretmenler.Size = new System.Drawing.Size(179, 21);
            this.cmbOgretmenler.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btnDerslikSil);
            this.panel3.Controls.Add(this.btnDerslikEkle);
            this.panel3.Controls.Add(this.lstDerslikler);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.cmbDerslikler);
            this.panel3.Location = new System.Drawing.Point(296, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(278, 160);
            this.panel3.TabIndex = 3;
            // 
            // btnDerslikSil
            // 
            this.btnDerslikSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDerslikSil.Location = new System.Drawing.Point(186, 35);
            this.btnDerslikSil.Name = "btnDerslikSil";
            this.btnDerslikSil.Size = new System.Drawing.Size(75, 23);
            this.btnDerslikSil.TabIndex = 2;
            this.btnDerslikSil.Text = "Sil";
            this.btnDerslikSil.UseVisualStyleBackColor = true;
            this.btnDerslikSil.Click += new System.EventHandler(this.btnDerslikSil_Click);
            // 
            // btnDerslikEkle
            // 
            this.btnDerslikEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDerslikEkle.Location = new System.Drawing.Point(82, 35);
            this.btnDerslikEkle.Name = "btnDerslikEkle";
            this.btnDerslikEkle.Size = new System.Drawing.Size(75, 23);
            this.btnDerslikEkle.TabIndex = 1;
            this.btnDerslikEkle.Text = "Ekle";
            this.btnDerslikEkle.UseVisualStyleBackColor = true;
            this.btnDerslikEkle.Click += new System.EventHandler(this.btnDerslikEkle_Click);
            // 
            // lstDerslikler
            // 
            this.lstDerslikler.BackColor = System.Drawing.Color.MistyRose;
            this.lstDerslikler.FormattingEnabled = true;
            this.lstDerslikler.Location = new System.Drawing.Point(82, 64);
            this.lstDerslikler.Name = "lstDerslikler";
            this.lstDerslikler.Size = new System.Drawing.Size(179, 82);
            this.lstDerslikler.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Derslik";
            // 
            // cmbDerslikler
            // 
            this.cmbDerslikler.FormattingEnabled = true;
            this.cmbDerslikler.Location = new System.Drawing.Point(82, 11);
            this.cmbDerslikler.Name = "cmbDerslikler";
            this.cmbDerslikler.Size = new System.Drawing.Size(179, 21);
            this.cmbDerslikler.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnSinifGrupSil);
            this.panel4.Controls.Add(this.btnSinifGrupEkle);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cmbGruplar);
            this.panel4.Controls.Add(this.lstSinifGrup);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.cmbSiniflar);
            this.panel4.Location = new System.Drawing.Point(12, 238);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(562, 120);
            this.panel4.TabIndex = 4;
            // 
            // btnSinifGrupSil
            // 
            this.btnSinifGrupSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSinifGrupSil.Location = new System.Drawing.Point(186, 77);
            this.btnSinifGrupSil.Name = "btnSinifGrupSil";
            this.btnSinifGrupSil.Size = new System.Drawing.Size(75, 23);
            this.btnSinifGrupSil.TabIndex = 3;
            this.btnSinifGrupSil.Text = "Sil";
            this.btnSinifGrupSil.UseVisualStyleBackColor = true;
            this.btnSinifGrupSil.Click += new System.EventHandler(this.btnSinifGrupSil_Click);
            // 
            // btnSinifGrupEkle
            // 
            this.btnSinifGrupEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSinifGrupEkle.Location = new System.Drawing.Point(82, 77);
            this.btnSinifGrupEkle.Name = "btnSinifGrupEkle";
            this.btnSinifGrupEkle.Size = new System.Drawing.Size(75, 23);
            this.btnSinifGrupEkle.TabIndex = 2;
            this.btnSinifGrupEkle.Text = "Ekle";
            this.btnSinifGrupEkle.UseVisualStyleBackColor = true;
            this.btnSinifGrupEkle.Click += new System.EventHandler(this.btnSinifGrupEkle_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Sınıf Grup";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Grup";
            // 
            // cmbGruplar
            // 
            this.cmbGruplar.FormattingEnabled = true;
            this.cmbGruplar.Location = new System.Drawing.Point(82, 50);
            this.cmbGruplar.Name = "cmbGruplar";
            this.cmbGruplar.Size = new System.Drawing.Size(179, 21);
            this.cmbGruplar.TabIndex = 1;
            // 
            // lstSinifGrup
            // 
            this.lstSinifGrup.BackColor = System.Drawing.Color.MistyRose;
            this.lstSinifGrup.FormattingEnabled = true;
            this.lstSinifGrup.Location = new System.Drawing.Point(366, 23);
            this.lstSinifGrup.Name = "lstSinifGrup";
            this.lstSinifGrup.Size = new System.Drawing.Size(179, 82);
            this.lstSinifGrup.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Sınıf";
            // 
            // cmbSiniflar
            // 
            this.cmbSiniflar.FormattingEnabled = true;
            this.cmbSiniflar.Location = new System.Drawing.Point(82, 23);
            this.cmbSiniflar.Name = "cmbSiniflar";
            this.cmbSiniflar.Size = new System.Drawing.Size(179, 21);
            this.cmbSiniflar.TabIndex = 0;
            this.cmbSiniflar.SelectedIndexChanged += new System.EventHandler(this.cmbSiniflar_SelectedIndexChanged);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txtYerlesimSekli);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.txtToplamDersSaati);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Location = new System.Drawing.Point(12, 364);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(562, 58);
            this.panel5.TabIndex = 5;
            // 
            // txtYerlesimSekli
            // 
            this.txtYerlesimSekli.BackColor = System.Drawing.Color.MistyRose;
            this.txtYerlesimSekli.Location = new System.Drawing.Point(366, 17);
            this.txtYerlesimSekli.Name = "txtYerlesimSekli";
            this.txtYerlesimSekli.Size = new System.Drawing.Size(100, 20);
            this.txtYerlesimSekli.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(287, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Yerleşim Şekli";
            // 
            // txtToplamDersSaati
            // 
            this.txtToplamDersSaati.BackColor = System.Drawing.Color.MistyRose;
            this.txtToplamDersSaati.Location = new System.Drawing.Point(103, 17);
            this.txtToplamDersSaati.Name = "txtToplamDersSaati";
            this.txtToplamDersSaati.Size = new System.Drawing.Size(100, 20);
            this.txtToplamDersSaati.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Toplam Ders Saati";
            // 
            // btnTanimliDersEkle
            // 
            this.btnTanimliDersEkle.Location = new System.Drawing.Point(467, 428);
            this.btnTanimliDersEkle.Name = "btnTanimliDersEkle";
            this.btnTanimliDersEkle.Size = new System.Drawing.Size(107, 38);
            this.btnTanimliDersEkle.TabIndex = 6;
            this.btnTanimliDersEkle.Text = "Ekle";
            this.btnTanimliDersEkle.UseVisualStyleBackColor = true;
            this.btnTanimliDersEkle.Click += new System.EventHandler(this.btnTanimliDersEkle_Click);
            // 
            // btnYardim
            // 
            this.btnYardim.Location = new System.Drawing.Point(12, 428);
            this.btnYardim.Name = "btnYardim";
            this.btnYardim.Size = new System.Drawing.Size(75, 38);
            this.btnYardim.TabIndex = 6;
            this.btnYardim.TabStop = false;
            this.btnYardim.Text = "Yardım";
            this.btnYardim.UseVisualStyleBackColor = true;
            // 
            // btnEkleDevam
            // 
            this.btnEkleDevam.Location = new System.Drawing.Point(354, 428);
            this.btnEkleDevam.Name = "btnEkleDevam";
            this.btnEkleDevam.Size = new System.Drawing.Size(107, 38);
            this.btnEkleDevam.TabIndex = 7;
            this.btnEkleDevam.Text = "Ekle Devam";
            this.btnEkleDevam.UseVisualStyleBackColor = true;
            this.btnEkleDevam.Click += new System.EventHandler(this.button1_Click);
            // 
            // DersTanimlamaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 475);
            this.Controls.Add(this.btnEkleDevam);
            this.Controls.Add(this.btnYardim);
            this.Controls.Add(this.btnTanimliDersEkle);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DersTanimlamaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ders Tanımlama";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDersler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lstOgretmenler;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbOgretmenler;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListBox lstDerslikler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDerslikler;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGruplar;
        private System.Windows.Forms.ListBox lstSinifGrup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSiniflar;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtYerlesimSekli;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtToplamDersSaati;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTanimliDersEkle;
        private System.Windows.Forms.Button btnYardim;
        private System.Windows.Forms.Button btnOgretmenSil;
        private System.Windows.Forms.Button btnOgretmenEkle;
        private System.Windows.Forms.Button btnDerslikSil;
        private System.Windows.Forms.Button btnDerslikEkle;
        private System.Windows.Forms.Button btnSinifGrupSil;
        private System.Windows.Forms.Button btnSinifGrupEkle;
        private System.Windows.Forms.Button btnEkleDevam;

    }
}