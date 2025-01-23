namespace DersDagitim
{
    partial class DersProgramiOnIzlemeForm
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
            this.cmbOgretmenler = new System.Windows.Forms.ComboBox();
            this.cmbSiniflar = new System.Windows.Forms.ComboBox();
            this.cmbDerslikler = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProgramSahibi = new System.Windows.Forms.Label();
            this.pbOnizleme = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnizleme)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbOgretmenler
            // 
            this.cmbOgretmenler.FormattingEnabled = true;
            this.cmbOgretmenler.Location = new System.Drawing.Point(6, 32);
            this.cmbOgretmenler.Name = "cmbOgretmenler";
            this.cmbOgretmenler.Size = new System.Drawing.Size(228, 21);
            this.cmbOgretmenler.TabIndex = 0;
            this.cmbOgretmenler.SelectedIndexChanged += new System.EventHandler(this.cmbOgretmenler_SelectedIndexChanged);
            // 
            // cmbSiniflar
            // 
            this.cmbSiniflar.FormattingEnabled = true;
            this.cmbSiniflar.Location = new System.Drawing.Point(240, 32);
            this.cmbSiniflar.Name = "cmbSiniflar";
            this.cmbSiniflar.Size = new System.Drawing.Size(228, 21);
            this.cmbSiniflar.TabIndex = 1;
            this.cmbSiniflar.SelectedIndexChanged += new System.EventHandler(this.cmbSiniflar_SelectedIndexChanged);
            // 
            // cmbDerslikler
            // 
            this.cmbDerslikler.FormattingEnabled = true;
            this.cmbDerslikler.Location = new System.Drawing.Point(474, 32);
            this.cmbDerslikler.Name = "cmbDerslikler";
            this.cmbDerslikler.Size = new System.Drawing.Size(228, 21);
            this.cmbDerslikler.TabIndex = 2;
            this.cmbDerslikler.SelectedIndexChanged += new System.EventHandler(this.cmbDerslikler_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Öğretmenler";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(237, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Sınıflar";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(471, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Derslikler";
            // 
            // lblProgramSahibi
            // 
            this.lblProgramSahibi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblProgramSahibi.Location = new System.Drawing.Point(6, 66);
            this.lblProgramSahibi.Name = "lblProgramSahibi";
            this.lblProgramSahibi.Size = new System.Drawing.Size(696, 24);
            this.lblProgramSahibi.TabIndex = 7;
            this.lblProgramSahibi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pbOnizleme
            // 
            this.pbOnizleme.Location = new System.Drawing.Point(6, 3);
            this.pbOnizleme.Name = "pbOnizleme";
            this.pbOnizleme.Size = new System.Drawing.Size(585, 394);
            this.pbOnizleme.TabIndex = 8;
            this.pbOnizleme.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbOgretmenler);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblProgramSahibi);
            this.groupBox1.Controls.Add(this.cmbDerslikler);
            this.groupBox1.Controls.Add(this.cmbSiniflar);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(712, 93);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbOnizleme);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 488);
            this.panel1.TabIndex = 13;
            // 
            // DersProgramiOnIzlemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 581);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "DersProgramiOnIzlemeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ders Programı Önizleme";
            this.Load += new System.EventHandler(this.DersProgramiOnIzlemeForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbOnizleme)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbOgretmenler;
        private System.Windows.Forms.ComboBox cmbSiniflar;
        private System.Windows.Forms.ComboBox cmbDerslikler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProgramSahibi;
        private System.Windows.Forms.PictureBox pbOnizleme;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
    }
}