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

namespace exhaustLogin
{
    public partial class reportViewSCVmas : Form
    {
        SYS_DAL.VMASdal vmasdal = new SYS_DAL.VMASdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewSCVmas()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }
        public void display_Vmas(string clid)
        {
            SYS.Model.VMAS vmas_data = vmasdal.Get_VMAS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
            }
            if (mainPanel.isdisplayCMANo)
            {
                rzbh = "认证编号:" + mainPanel.stationinfmodel.STATIONDATE;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    //检验机构
                    new ReportParameter("parameterJCZMC",mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJCzdh",mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterLineID", carinf.LINEID),
                    new ReportParameter("parameterStationID", carinf.STATIONID),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),

                    //报告单
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("G")),

                    //车辆信息
                    new ReportParameter("parameterCz",carinf.CZ+"/"+carinf.LXDH),
                    new ReportParameter("parameterClph",carinf.CLHP+"/"+carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterClxh", carinf.PP+"/"+carinf.XH),
                    new ReportParameter("parameterXslc",carinf.XSLC),
                    new ReportParameter("parameterScrq",carinf.SCRQ.ToString("D")),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("D")),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",carinf.BSQXS+"/"+carinf.DWS),
                    new ReportParameter("parameterFdjpl",(float.Parse(carinf.FDJPL)*1000).ToString()+"/"+carinf.QGS),
                    new ReportParameter("parameterRlzl",carinf.RLZL+"/"+carinf.RYPH),
                    new ReportParameter("parameterChzz",carinf.JHZZ),
                    new ReportParameter("parameterFdjxh",carinf.FDJXH+"/"+carinf.SCQY),
                    new ReportParameter("parameterJqfs",carinf.JQFS),
                    new ReportParameter("parameterQdfs",carinf.QDXS+"/"+carinf.QDLTQY),

                    //检测设备
                    new ReportParameter("parameterSbrzbm",vmas_data.SBXH),
                    new ReportParameter("parameterCgjxh",vmas_data.CGJXH+":"+vmas_data.CGJZZC),
                    new ReportParameter("parameterCgjbh",vmas_data.CGJBH),
                    new ReportParameter("parameterFxyxh",vmas_data.FXYXH+":"+vmas_data.FXYZZC),
                    new ReportParameter("parameterFxybh",vmas_data.FXYBH),
                    new ReportParameter("parameterLLj",vmas_data.LLJXH+":"+vmas_data.LLJZZC),
                    new ReportParameter("parameterLLjbh",vmas_data.LLJBH),

                    //环境参数
                    new ReportParameter("parameterWd",vmas_data.WD),
                    new ReportParameter("parameterDqy",vmas_data.DQY),
                    new ReportParameter("parameterSd",vmas_data.SD),

                    //检测结论
                    new ReportParameter("parameterCOCLZ",vmas_data.COZL),
                    new ReportParameter("parameterCOXZ","≤"+vmas_data.COXZ),
                    new ReportParameter("parameterCOPD",(vmas_data.COPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterHCCLZ",(float.Parse(vmas_data.HCZL)).ToString("0.00")),
                    new ReportParameter("parameterHCXZ","≤"+vmas_data.HCXZ),
                    new ReportParameter("parameterHCPD",(vmas_data.HCPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterNOXCLZ",(float.Parse(vmas_data.NOXZL)).ToString("0.00")),
                    new ReportParameter("parameterNOXXZ","≤"+vmas_data.NOXXZ),
                    new ReportParameter("parameterNOXPD",(vmas_data.NOXPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterHCNOXCLZ",(float.Parse(vmas_data.HCZL)+float.Parse(vmas_data.NOXZL)).ToString("0.00")),
                    new ReportParameter("parameterHCNOXXZ","HC+NOx≤"+vmas_data.HCXZ),
                    new ReportParameter("parameterHCNOXPD",vmas_data.HCPD),

                    new ReportParameter("parameterZHPD",vmas_data.ZHPD)
                    
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
