namespace DersDagitim
{
    partial class KorDagitimiForm
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
            this.pbYerlesmisYuzde = new System.Windows.Forms.ProgressBar();
            this.timerYuzdeleriAl = new System.Windows.Forms.Timer(this.components);
            this.pbGenelYuzde = new System.Windows.Forms.ProgressBar();
            this.lblGenelTaramaYuzde = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblYerlesimYuzdesi = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYerlesmeyenDersSayisi = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUzerindeCalisilanDers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbYerlesmisYuzde
            // 
            this.pbYerlesmisYuzde.Location = new System.Drawing.Point(11, 25);
            this.pbYerlesmisYuzde.Name = "pbYerlesmisYuzde";
            this.pbYerlesmisYuzde.Size = new System.Drawing.Size(326, 23);
            this.pbYerlesmisYuzde.TabIndex = 1;
            // 
            // timerYuzdeleriAl
            // 
            this.timerYuzdeleriAl.Interval = 500;
            this.timerYuzdeleriAl.Tick += new System.EventHandler(this.timerYuzdeleriAl_Tick);
            // 
            // pbGenelYuzde
            // 
            this.pbGenelYuzde.Location = new System.Drawing.Point(9, 122);
            this.pbGenelYuzde.Name = "pbGenelYuzde";
            this.pbGenelYuzde.Size = new System.Drawing.Size(328, 23);
            this.pbGenelYuzde.TabIndex = 2;
            // 
            // lblGenelTaramaYuzde
            // 
            this.lblGenelTaramaYuzde.AutoSize = true;
            this.lblGenelTaramaYuzde.Location = new System.Drawing.Point(135, 106);
            this.lblGenelTaramaYuzde.Name = "lblGenelTaramaYuzde";
            this.lblGenelTaramaYuzde.Size = new System.Drawing.Size(21, 13);
            this.lblGenelTaramaYuzde.TabIndex = 3;
            this.lblGenelTaramaYuzde.Text = "%0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ders Yerleştirme Yüzdesi :";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Genel Tarama Yüzdesi :";
            // 
            // lblYerlesimYuzdesi
            // 
            this.lblYerlesimYuzdesi.AutoSize = true;
            this.lblYerlesimYuzdesi.Location = new System.Drawing.Point(136, 9);
            this.lblYerlesimYuzdesi.Name = "lblYerlesimYuzdesi";
            this.lblYerlesimYuzdesi.Size = new System.Drawing.Size(21, 13);
            this.lblYerlesimYuzdesi.TabIndex = 6;
            this.lblYerlesimYuzdesi.Text = "%0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Yerleşmeyen Ders Sayısı : ";
            // 
            // lblYerlesmeyenDersSayisi
            // 
            this.lblYerlesmeyenDersSayisi.AutoSize = true;
            this.lblYerlesmeyenDersSayisi.Location = new System.Drawing.Point(324, 9);
            this.lblYerlesmeyenDersSayisi.Name = "lblYerlesmeyenDersSayisi";
            this.lblYerlesmeyenDersSayisi.Size = new System.Drawing.Size(13, 13);
            this.lblYerlesmeyenDersSayisi.TabIndex = 8;
            this.lblYerlesmeyenDersSayisi.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Üzerinde Çalışılan Ders : ";
            // 
            // lblUzerindeCalisilanDers
            // 
            this.lblUzerindeCalisilanDers.AutoSize = true;
            this.lblUzerindeCalisilanDers.Location = new System.Drawing.Point(136, 62);
            this.lblUzerindeCalisilanDers.Name = "lblUzerindeCalisilanDers";
            this.lblUzerindeCalisilanDers.Size = new System.Drawing.Size(29, 13);
            this.lblUzerindeCalisilanDers.TabIndex = 10;
            this.lblUzerindeCalisilanDers.Text = "Ders";
            // 
            // KorDagitimiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 167);
            this.Controls.Add(this.lblUzerindeCalisilanDers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblYerlesmeyenDersSayisi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblYerlesimYuzdesi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblGenelTaramaYuzde);
            this.Controls.Add(this.pbGenelYuzde);
            this.Controls.Add(this.pbYerlesmisYuzde);
            this.Name = "KorDagitimiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DersDagitimiForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DersDagitimiForm_FormClosing);
            this.Load += new System.EventHandler(this.DersDagitimiForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbYerlesmisYuzde;
        private System.Windows.Forms.Timer timerYuzdeleriAl;
        private System.Windows.Forms.ProgressBar pbGenelYuzde;
        private System.Windows.Forms.Label lblGenelTaramaYuzde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblYerlesimYuzdesi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYerlesmeyenDersSayisi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUzerindeCalisilanDers;
    }
}