using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exhaustLogin
{
    public partial class fastReport : Form
    {
        public fastReport()
        {
            InitializeComponent();
        }

        private void fastReport_Load(object sender, EventArgs e)
        {
            //FastReport.Report report = new FastReport.Report();
            string filename = @"./fastReport/vmas.frx";
            this.previewControl1.Load(filename);
            this.previewControl1.Show();
            //report.Load(filename);
        }
    }
}
