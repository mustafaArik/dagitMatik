namespace DersDagitim
{
    partial class frmHavuzluTarama
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
            this.lblDurum = new System.Windows.Forms.Label();
            this.pbYuzde = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.lblEnIyiYerlesimYuzde = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDurum
            // 
            this.lblDurum.AutoSize = true;
            this.lblDurum.Location = new System.Drawing.Point(12, 9);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(35, 13);
            this.lblDurum.TabIndex = 0;
            this.lblDurum.Text = "label1";
            // 
            // pbYuzde
            // 
            this.pbYuzde.Location = new System.Drawing.Point(15, 25);
            this.pbYuzde.Name = "pbYuzde";
            this.pbYuzde.Size = new System.Drawing.Size(489, 23);
            this.pbYuzde.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 750;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "En iyi yerleşim yüzdesi :";
            // 
            // lblEnIyiYerlesimYuzde
            // 
            this.lblEnIyiYerlesimYuzde.AutoSize = true;
            this.lblEnIyiYerlesimYuzde.Location = new System.Drawing.Point(134, 69);
            this.lblEnIyiYerlesimYuzde.Name = "lblEnIyiYerlesimYuzde";
            this.lblEnIyiYerlesimYuzde.Size = new System.Drawing.Size(21, 13);
            this.lblEnIyiYerlesimYuzde.TabIndex = 3;
            this.lblEnIyiYerlesimYuzde.Text = "%0";
            // 
            // frmHavuzluTarama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 426);
            this.Controls.Add(this.lblEnIyiYerlesimYuzde);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbYuzde);
            this.Controls.Add(this.lblDurum);
            this.Name = "frmHavuzluTarama";
            this.Text = "frmHavuzluTarama";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHavuzluTarama_FormClosing);
            this.Load += new System.EventHandler(this.frmHavuzluTarama_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.ProgressBar pbYuzde;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblEnIyiYerlesimYuzde;
    }
}