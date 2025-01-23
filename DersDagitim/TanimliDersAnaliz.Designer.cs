namespace DersDagitim
{
    partial class TanimliDersAnaliz
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
            this.pbYerlesim = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOlasilikToplami = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbYerlesim)).BeginInit();
            this.SuspendLayout();
            // 
            // pbYerlesim
            // 
            this.pbYerlesim.Location = new System.Drawing.Point(130, 12);
            this.pbYerlesim.Name = "pbYerlesim";
            this.pbYerlesim.Size = new System.Drawing.Size(200, 80);
            this.pbYerlesim.TabIndex = 0;
            this.pbYerlesim.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Yerleşebileceği Alanlar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Olasılık Toplamı";
            // 
            // lblOlasilikToplami
            // 
            this.lblOlasilikToplami.AutoSize = true;
            this.lblOlasilikToplami.Location = new System.Drawing.Point(127, 115);
            this.lblOlasilikToplami.Name = "lblOlasilikToplami";
            this.lblOlasilikToplami.Size = new System.Drawing.Size(13, 13);
            this.lblOlasilikToplami.TabIndex = 3;
            this.lblOlasilikToplami.Text = "0";
            // 
            // TanimliDersAnaliz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 466);
            this.Controls.Add(this.lblOlasilikToplami);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbYerlesim);
            this.Name = "TanimliDersAnaliz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tanımlı Ders Analiz";
            this.Load += new System.EventHandler(this.TanimliDersAnaliz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbYerlesim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbYerlesim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOlasilikToplami;
    }
}