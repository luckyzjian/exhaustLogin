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
    public partial class reportViwStatistics : Form
    {
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViwStatistics()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“NEIMENG_VMASDataSet.已检车辆信息”中。您可以根据需要移动或删除它。
            //this.已检车辆信息TableAdapter.Fill(this.NEIMENG_VMASDataSet.已检车辆信息);
            //this.reportViewer1.RefreshReport();
        }
        public void display_Asm(string stationid, string lineid, DateTime starttime, DateTime endtime, string plate, string jcff, string result)
        {
            int totalCount = 0;
            int OKcount = 0;
            int notOkcount = 0;
            double detectedMoney = 0;
            double OKrate = 0;
            double notOKrate = 0;
            DataTable datatableDetectedMoney = null;
            string JCXH = "";
            string JCFF = "";
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                DataSet model = logininfcontrol.getStationLineDetectedDataset(stationid,lineid,starttime,endtime,plate,jcff,result);
                OKcount = mainPanel.logininfcontrol.getStationLineDetectedCount(stationid, lineid, starttime, endtime, plate, jcff, "1");
                notOkcount = mainPanel.logininfcontrol.getStationLineDetectedCount(stationid, lineid, starttime, endtime, plate, jcff, "-1");
                datatableDetectedMoney = mainPanel.logininfcontrol.getDetectedCarMoney(stationid, lineid, starttime, endtime, plate, jcff);
                totalCount = OKcount + notOkcount;
                if (datatableDetectedMoney != null)
                    foreach (DataRow dr in datatableDetectedMoney.Rows)
                    {
                        detectedMoney += double.Parse(dr["JCFY"].ToString());
                    }
                if (totalCount == 0)
                {
                    OKrate = 0;
                }
                else
                {
                    OKrate = OKcount / (totalCount * 1.0);
                    notOKrate = notOkcount / (totalCount * 1.0);
                }
                if (lineid == "0") JCXH = "所有线";
                else JCXH = lineid + "号线";
                switch (jcff)
                {
                    case "0": JCFF = "所有方法"; break;
                    case "ASM": JCFF = "稳态工况法"; break;
                    case "VMAS": JCFF = "简易瞬态工况法"; break;
                    case "JZJS": JCFF = "加载减速法"; break;
                    case "ZYJS": JCFF = "自由加速法"; break;
                    case "SDS": JCFF = "双怠速法"; break;
                    default: JCFF = "所有方法"; break;
                }
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();
                reportViewer1.LocalReport.Dispose();
                reportViewer1.Visible = true;
                ReportParameter[] rptpara =
                {
                    new ReportParameter("starttime", starttime.ToShortDateString()),
                    new ReportParameter("endtime", endtime.ToShortDateString()),
                    new ReportParameter("jcff", JCFF),
                    new ReportParameter("jcxh", JCXH),
                    new ReportParameter("totalCount",totalCount.ToString()),
                    new ReportParameter("OKcount", OKcount.ToString()),
                    new ReportParameter("notOKcount",notOkcount.ToString()),
                    new ReportParameter("OKratecount", (OKrate*100).ToString("0.0")+"%"),
                    new ReportParameter("money", detectedMoney.ToString("0.0")+"元"),
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
            int totalCount = 0;
            int OKcount = 0;
            int notOkcount = 0;
            double detectedMoney = 0;
            double OKrate = 0;
            double notOKrate = 0;
            DataTable datatableDetectedMoney = null;

            string JCXH = "";
            string JCFF = "";
            string JCRQ = "";
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                DataSet model = logininfcontrol.getStationLineDetectedDataset(stationid, lineid, lx, plate, jcff, result);
                OKcount = mainPanel.logininfcontrol.getStationLineDetectedCount(stationid, lineid, lx, plate, jcff, "1");
                notOkcount = mainPanel.logininfcontrol.getStationLineDetectedCount(stationid, lineid, lx, plate, jcff, "-1");
                datatableDetectedMoney = mainPanel.logininfcontrol.getDetectedCarMoney(stationid, lineid, lx, plate, jcff);
                totalCount = OKcount + notOkcount;
                switch (lx)
                {
                    case "0": JCRQ = "所有时间"; break;
                    case "1": JCRQ = "本年"; break;
                    case "2": JCRQ = "本月"; break;
                    case "3": JCRQ = "本日"; break;
                    default:break;
                }
                if (datatableDetectedMoney != null)
                    foreach (DataRow dr in datatableDetectedMoney.Rows)
                    {
                        detectedMoney += double.Parse(dr["JCFY"].ToString());
                    }
                if (totalCount == 0)
                {
                    OKrate = 0;
                }
                else
                {
                    OKrate = OKcount / (totalCount * 1.0);
                    notOKrate = notOkcount / (totalCount * 1.0);
                }
                if (lineid == "0") JCXH = "所有线";
                else JCXH = lineid + "号线";
                switch (jcff)
                {
                    case "0": JCFF = "所有方法"; break;
                    case "ASM": JCFF = "稳态工况法"; break;
                    case "VMAS": JCFF = "简易瞬态工况法"; break;
                    case "JZJS": JCFF = "加载减速法"; break;
                    case "ZYJS": JCFF = "自由加速法"; break;
                    case "SDS": JCFF = "双怠速法"; break;
                    default: JCFF = "所有方法"; break;
                }
                reportViewer1.LocalReport.ReleaseSandboxAppDomain();
                reportViewer1.LocalReport.Dispose();
                reportViewer1.Visible = true;
                ReportParameter[] rptpara =
                {
                    new ReportParameter("starttime", JCRQ),
                    new ReportParameter("endtime", JCRQ),
                    new ReportParameter("jcff", JCFF),
                    new ReportParameter("jcxh", JCXH),
                    new ReportParameter("totalCount",totalCount.ToString()),
                    new ReportParameter("OKcount", OKcount.ToString()),
                    new ReportParameter("notOKcount",notOkcount.ToString()),
                    new ReportParameter("OKratecount", (OKrate*100).ToString("0.0")+"%"),
                    new ReportParameter("money", detectedMoney.ToString("0.0")+"元"),
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
        private void reportView_panelFormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
