using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using exhaustLogin;

namespace exhaustDetect
{
    public partial class reportViewPay : Form
    {
        private static string[] ls_shz = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖", "拾" };
        private static string[] ls_dw_zh = { "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "万", "拾", "佰", "仟", "万" };
        private static string[] num_dw = { "元", "拾", "佰", "仟", "万", "拾", "佰", "仟", "万", "拾", "佰", "仟", "万" };
        private static string[] ls_dw_x = { "角", "分"};
        public reportViewPay()
        {
            InitializeComponent();
        }
        string numGetStr(Double num)
        {
            Boolean ixsh_bool = false;
            Boolean izhsh_bool = true;
            string numstr;
            string numstr_zh;
            string numstr_x = "";
            string numstr_dq;
            string numstr_r = "";
            num = Math.Round(num, 1);
            if (num < 0)
                return ("不转换欠条");
            if(num>9999999999999999.9)
                return ("钱太多了");
            if (num == 0)
                return ls_shz[0];
            if (num < 1.0)
                izhsh_bool = false;
            numstr = num.ToString();
            numstr_zh = numstr;
            if (numstr_zh.Contains("."))
            {
                numstr_zh= numstr.Substring((numstr.IndexOf(".")));
                numstr_x = numstr.Substring((numstr.IndexOf(".") + 1), (numstr.Length - numstr.IndexOf(".") - 1));
                ixsh_bool=true;
            }
            if(numstr_x==""||int.Parse(numstr_x)<=0)
                ixsh_bool=false;
            return "";
        }
        
        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }
        public void display_Vmas(string clid,string cz,string safestandard,string safefact,string safebz,string exhauststandard,string exhaustfact,string exhaustbz,string money)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("clid", clid),
                    new ReportParameter("cz",cz),
                    new ReportParameter("safestandard",safestandard),
                    new ReportParameter("safefact", safefact),
                    new ReportParameter("safebz", safebz),
                    new ReportParameter("exhauststandard", exhauststandard),
                    new ReportParameter("exhaustfact",exhaustfact),
                    new ReportParameter("exhaustbz", exhaustbz),
                    new ReportParameter("money", money),
                    new ReportParameter("moneyinchinese",money),
                    new ReportParameter("user",mainPanel.nowUser.userName),
                    new ReportParameter("datetime",DateTime.Now.ToString("D")),
                    new ReportParameter("stationname",mainPanel.stationinfmodel.STATIONNAME)
                };
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.SetParameters(rptpara);
            }
            catch
            {
                throw;
            }
            reportViewer1.RefreshReport();
        }
        private void reportView_panelFormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
