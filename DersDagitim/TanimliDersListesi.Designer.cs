namespace DersDagitim
{
    partial class TanimliDersListesi
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTanimliDersler = new System.Windows.Forms.DataGridView();
            this.KolonId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolonDersAdi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolonOgretmenler = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolonSiniflar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolonDerslikler = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolonToplamDers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KolonYerlesim = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTanimliDersler)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTanimliDersler
            // 
            this.dgvTanimliDersler.AllowUserToAddRows = false;
            this.dgvTanimliDersler.AllowUserToDeleteRows = false;
            this.dgvTanimliDersler.AllowUserToResizeColumns = false;
            this.dgvTanimliDersler.AllowUserToResizeRows = false;
            this.dgvTanimliDersler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTanimliDersler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KolonId,
            this.KolonDersAdi,
            this.KolonOgretmenler,
            this.KolonSiniflar,
            this.KolonDerslikler,
            this.KolonToplamDers,
            this.KolonYerlesim});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTanimliDersler.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTanimliDersler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTanimliDersler.Location = new System.Drawing.Point(0, 0);
            this.dgvTanimliDersler.MultiSelect = false;
            this.dgvTanimliDersler.Name = "dgvTanimliDersler";
            this.dgvTanimliDersler.ReadOnly = true;
            this.dgvTanimliDersler.RowHeadersVisible = false;
            this.dgvTanimliDersler.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTanimliDersler.RowTemplate.Height = 45;
            this.dgvTanimliDersler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTanimliDersler.Size = new System.Drawing.Size(742, 476);
            this.dgvTanimliDersler.TabIndex = 26;
            this.dgvTanimliDersler.TabStop = false;
            // 
            // KolonId
            // 
            this.KolonId.DataPropertyName = "id";
            this.KolonId.HeaderText = "id";
            this.KolonId.Name = "KolonId";
            this.KolonId.ReadOnly = true;
            this.KolonId.Visible = false;
            // 
            // KolonDersAdi
            // 
            this.KolonDersAdi.DataPropertyName = "dersadi";
            this.KolonDersAdi.HeaderText = "Ders Adı";
            this.KolonDersAdi.Name = "KolonDersAdi";
            this.KolonDersAdi.ReadOnly = true;
            this.KolonDersAdi.Width = 180;
            // 
            // KolonOgretmenler
            // 
            this.KolonOgretmenler.DataPropertyName = "ogretmenler";
            this.KolonOgretmenler.HeaderText = "Öğretmenler";
            this.KolonOgretmenler.Name = "KolonOgretmenler";
            this.KolonOgretmenler.ReadOnly = true;
            this.KolonOgretmenler.Width = 120;
            // 
            // KolonSiniflar
            // 
            this.KolonSiniflar.DataPropertyName = "sinifgruplar";
            this.KolonSiniflar.HeaderText = "Sınıf Gruplar";
            this.KolonSiniflar.Name = "KolonSiniflar";
            this.KolonSiniflar.ReadOnly = true;
            this.KolonSiniflar.Width = 130;
            // 
            // KolonDerslikler
            // 
            this.KolonDerslikler.DataPropertyName = "derslikler";
            this.KolonDerslikler.HeaderText = "Derslikler";
            this.KolonDerslikler.Name = "KolonDerslikler";
            this.KolonDerslikler.ReadOnly = true;
            this.KolonDerslikler.Width = 120;
            // 
            // KolonToplamDers
            // 
            this.KolonToplamDers.DataPropertyName = "toplamders";
            this.KolonToplamDers.HeaderText = "Toplam Ders";
            this.KolonToplamDers.Name = "KolonToplamDers";
            this.KolonToplamDers.ReadOnly = true;
            this.KolonToplamDers.Width = 80;
            // 
            // KolonYerlesim
            // 
            this.KolonYerlesim.DataPropertyName = "yerlesim";
            this.KolonYerlesim.HeaderText = "Yerleşim";
            this.KolonYerlesim.Name = "KolonYerlesim";
            this.KolonYerlesim.ReadOnly = true;
            this.KolonYerlesim.Width = 80;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 43);
            this.button1.TabIndex = 27;
            this.button1.Text = "Yeni Ders";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(115, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 43);
            this.button2.TabIndex = 28;
            this.button2.Text = "Dersi Düzenle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(227, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 43);
            this.button3.TabIndex = 29;
            this.button3.Text = "Sil";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 476);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(742, 49);
            this.panel1.TabIndex = 30;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(369, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 43);
            this.button4.TabIndex = 30;
            this.button4.Text = "Analiz Et";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvTanimliDersler);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(742, 476);
            this.panel2.TabIndex = 31;
            // 
            // TanimliDersListesi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 525);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TanimliDersListesi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tanımlanmış Dersler Listesi";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTanimliDersler)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTanimliDersler;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonId;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonDersAdi;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonOgretmenler;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonSiniflar;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonDerslikler;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonToplamDers;
        private System.Windows.Forms.DataGridViewTextBoxColumn KolonYerlesim;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
    }
}