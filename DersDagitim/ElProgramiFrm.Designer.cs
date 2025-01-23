namespace DersDagitim
{
    partial class ElProgramiFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbSinif = new System.Windows.Forms.RadioButton();
            this.rbDerslik = new System.Windows.Forms.RadioButton();
            this.rbOgretmen = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstListe = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbSinif);
            this.panel1.Controls.Add(this.rbDerslik);
            this.panel1.Controls.Add(this.rbOgretmen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(444, 40);
            this.panel1.TabIndex = 1;
            // 
            // rbSinif
            // 
            this.rbSinif.AutoSize = true;
            this.rbSinif.Location = new System.Drawing.Point(175, 12);
            this.rbSinif.Name = "rbSinif";
            this.rbSinif.Size = new System.Drawing.Size(56, 17);
            this.rbSinif.TabIndex = 2;
            this.rbSinif.TabStop = true;
            this.rbSinif.Text = "Sınıflar";
            this.rbSinif.UseVisualStyleBackColor = true;
            this.rbSinif.CheckedChanged += new System.EventHandler(this.rbSinif_CheckedChanged);
            // 
            // rbDerslik
            // 
            this.rbDerslik.AutoSize = true;
            this.rbDerslik.Location = new System.Drawing.Point(101, 12);
            this.rbDerslik.Name = "rbDerslik";
            this.rbDerslik.Size = new System.Drawing.Size(68, 17);
            this.rbDerslik.TabIndex = 1;
            this.rbDerslik.TabStop = true;
            this.rbDerslik.Text = "Derslikler";
            this.rbDerslik.UseVisualStyleBackColor = true;
            this.rbDerslik.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbOgretmen
            // 
            this.rbOgretmen.AutoSize = true;
            this.rbOgretmen.Location = new System.Drawing.Point(12, 12);
            this.rbOgretmen.Name = "rbOgretmen";
            this.rbOgretmen.Size = new System.Drawing.Size(82, 17);
            this.rbOgretmen.TabIndex = 0;
            this.rbOgretmen.TabStop = true;
            this.rbOgretmen.Text = "Öğretmenler";
            this.rbOgretmen.UseVisualStyleBackColor = true;
            this.rbOgretmen.CheckedChanged += new System.EventHandler(this.rbOgretmen_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lstListe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 415);
            this.panel2.TabIndex = 2;
            // 
            // lstListe
            // 
            this.lstListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstListe.FormattingEnabled = true;
            this.lstListe.Location = new System.Drawing.Point(0, 0);
            this.lstListe.Name = "lstListe";
            this.lstListe.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstListe.Size = new System.Drawing.Size(244, 415);
            this.lstListe.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(244, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 415);
            this.panel3.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 143);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 38);
            this.button3.TabIndex = 2;
            this.button3.Text = "Rapor Al";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(182, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Seçimi Kaldır";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Tümünü Seç";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ElProgramiFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 455);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "ElProgramiFrm";
            this.Text = "El Programı Raporlama";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbDerslik;
        private System.Windows.Forms.RadioButton rbOgretmen;
        private System.Windows.Forms.RadioButton rbSinif;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstListe;
    }
}