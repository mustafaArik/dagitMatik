using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DersDagitim
{
    public partial class formHakkinda : Form
    {
        public formHakkinda()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://dagitmatik.blogspot.com");
        }

        private void formHakkinda_Load(object sender, EventArgs e)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = string.Format("Version : v{0}.{1}.{2} ({3})", version.Major, version.Minor, version.Build, version.Revision);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
