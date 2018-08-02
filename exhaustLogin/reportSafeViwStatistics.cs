using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace exhaustLogin
{
    public partial class reportSafeViwStatistics : Form
    {
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportSafeViwStatistics()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“NEIMENG_VMASDataSet1.安检记录”中。您可以根据需要移动或删除它。
            //this.安检记录TableAdapter.Fill(this.NEIMENG_VMASDataSet1.安检记录);
            // TODO: 这行代码将数据加载到表“NEIMENG_VMASDataSet.已检车辆信息”中。您可以根据需要移动或删除它。
            //this.已检车辆信息TableAdapter.Fill(this.NEIMENG_VMASDataSet.已检车辆信息);
            //this.reportViewer1.RefreshReport();
        }
        public void display_Asm(string stationid, string lineid, DateTime starttime, DateTime endtime, string plate, string jcff, string result)
        {

            int safetotalCount = 0;
            int safeOKcount = 0;
            int safenotOkcount = 0;
            double safedetectedMoney = 0;
            double safeOKrate = 0;
            double safenotOKrate = 0;
            DataTable datatableSafeDetectedMoney = null;
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                DataSet model = logininfcontrol.getSafeDetectedDataset( starttime, endtime, plate, jcff, result);
                safeOKcount = mainPanel.logininfcontrol.getSafeDetectedCount(starttime, endtime, plate, "1", "Y");
                safenotOkcount = mainPanel.logininfcontrol.getSafeDetectedCount(starttime, endtime, plate, "-1", "Y");
                
                datatableSafeDetectedMoney = mainPanel.logininfcontrol.getSafeDetectedCarMoney(starttime, endtime, plate);
                safetotalCount = safeOKcount + safenotOkcount;
                if (datatableSafeDetectedMoney != null)
                    foreach (DataRow dr in datatableSafeDetectedMoney.Rows)
                    {
                        safedetectedMoney += double.Parse(dr["JCFY"].ToString());
                    }
                if (safetotalCount == 0)
                {
                    safeOKrate = 0;
                }
                else
                {
                    safeOKrate = safeOKcount / (safetotalCount * 1.0);
                    safenotOKrate = safenotOkcount / (safetotalCount * 1.0);
                }
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();
                reportViewer1.LocalReport.Dispose();
                reportViewer1.Visible = true;
                ReportParameter[] rptpara =
                {
                    new ReportParameter("starttime", starttime.ToShortDateString()),
                    new ReportParameter("endtime", endtime.ToShortDateString()),
                    new ReportParameter("totalCount",safetotalCount.ToString()),
                    new ReportParameter("OKcount", safeOKcount.ToString()),
                    new ReportParameter("notOKcount",safenotOkcount.ToString()),
                    new ReportParameter("OKratecount", (safeOKrate*100).ToString("0.0")+"%"),
                    new ReportParameter("money", safedetectedMoney.ToString("0.0")+"元"),
                    new ReportParameter("stationname",mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("datetime",DateTime.Now.ToShortDateString())
                };
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.SetParameters(rptpara);
                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", model.Tables[0]));
            }
            catch
            {
                throw;
            }
            reportViewer1.RefreshReport();
        }
        public void display_Asm(string stationid, string lineid, string lx, string plate, string jcff, string result)
        {
            int safetotalCount = 0;
            int safeOKcount = 0;
            int safenotOkcount = 0;
            double safedetectedMoney = 0;
            double safeOKrate = 0;
            double safenotOKrate = 0;
            DataTable datatableSafeDetectedMoney = null;
            string JCRQ = "";
            switch (lx)
            {
                case "0": JCRQ = "所有时间"; break;
                case "1": JCRQ = "本年"; break;
                case "2": JCRQ = "本月"; break;
                case "3": JCRQ = "本日"; break;
                default: break;
            }
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                DataSet model = logininfcontrol.getSafeDetectedDataset( lx, plate, jcff, result);
                safeOKcount = mainPanel.logininfcontrol.getSafeDetectedCount(lx, plate, "1", "Y");
                safenotOkcount = mainPanel.logininfcontrol.getSafeDetectedCount(lx, plate, "-1", "Y");
                datatableSafeDetectedMoney = mainPanel.logininfcontrol.getSafeDetectedCarMoney(lx, plate);
                safetotalCount = safeOKcount + safenotOkcount;
                if (datatableSafeDetectedMoney != null)
                    foreach (DataRow dr in datatableSafeDetectedMoney.Rows)
                    {
                        safedetectedMoney += double.Parse(dr["JCFY"].ToString());
                    }
                if (safetotalCount == 0)
                {
                    safeOKrate = 0;
                }
                else
                {
                    safeOKrate = safeOKcount / (safetotalCount * 1.0);
                    safenotOKrate = safenotOkcount / (safetotalCount * 1.0);
                }
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();
                reportViewer1.LocalReport.Dispose();
                reportViewer1.Visible = true;
                ReportParameter[] rptpara =
                {
                    new ReportParameter("starttime", JCRQ),
                    new ReportParameter("endtime", JCRQ),
                    new ReportParameter("totalCount",safetotalCount.ToString()),
                    new ReportParameter("OKcount", safeOKcount.ToString()),
                    new ReportParameter("notOKcount",safenotOkcount.ToString()),
                    new ReportParameter("OKratecount", (safeOKrate*100).ToString("0.0")+"%"),
                    new ReportParameter("money", safedetectedMoney.ToString("0.0")+"元"),
                    new ReportParameter("stationname",mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("datetime",DateTime.Now.ToShortDateString())
                };
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.SetParameters(rptpara);
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", model.Tables[0]));
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
