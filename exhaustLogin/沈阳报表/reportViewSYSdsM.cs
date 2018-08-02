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
    public partial class reportViewSYSdsM : Form
    {
        SYS_DAL.SDSdal vmasdal = new SYS_DAL.SDSdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewSYSdsM()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }
        public void display_Vmas(string clid)
        {
            SYS.Model.SDS vmas_data = vmasdal.Get_SDS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
                rzbh = mainPanel.stationinfmodel.STATIONDATE;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", carinf.JCZMC),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterClxh", carinf.XH),
                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",carinf.BSQXS),
                    new ReportParameter("parameterJqfs",carinf.JQFS),
                    new ReportParameter("parameterFdjxh",carinf.FDJXH),
                    new ReportParameter("parameterGyfs",carinf.GYFS),
                    new ReportParameter("parameterRlzl",carinf.RLZL),
                    new ReportParameter("parameterDws",carinf.DWS),
                    new ReportParameter("parameterXslc",carinf.XSLC+"km"),
                    new ReportParameter("parameterFdjpl",carinf.FDJPL),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("D")),
                    new ReportParameter("parameterCCRQ", carinf.SCRQ.ToString("D")),
                    new ReportParameter("parameterCCS", carinf.CCS),
                    new ReportParameter("parameterCllx",carinf.CLLX),
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",carinf.CHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    new ReportParameter("parameterSBMC",vmas_data.SBMC),
                    new ReportParameter("parameterSBXH",vmas_data.SBXH),
                    new ReportParameter("parameterSBBH",vmas_data.FXYBH),              
                    new ReportParameter("parameterFxyxh",vmas_data.FXYXH),
                    new ReportParameter("parameterFxybh",vmas_data.FXYBH),
                    new ReportParameter("parameterFxycj",vmas_data.FXYZZC),
                    new ReportParameter("parameterZsjcj",vmas_data.ZSJZZC),
                    new ReportParameter("parameterZsjxh",vmas_data.ZSJXH),
                    new ReportParameter("parameterZsjbh",vmas_data.ZSJBH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterSbzzc",vmas_data.SBZZC),
                    new ReportParameter("parameterZZC",vmas_data.FXYZZC),
                    new ReportParameter("parameterWd",vmas_data.WD+"℃"),
                    new ReportParameter("parameterDqy",vmas_data.DQY+"kPa"),
                    new ReportParameter("parameterSd",vmas_data.SD+"%"),
                    new ReportParameter("parameterLOWHC",vmas_data.HCLOWCLZ),
                    new ReportParameter("parameterLOWHCXZ","≤"+vmas_data.HCLOWXZ),
                    new ReportParameter("parameterLOWCO",double.Parse(vmas_data.COLOWCLZ).ToString("0.00")),
                    new ReportParameter("parameterLOWCOXZ","≤"+double.Parse(vmas_data.COLOWXZ).ToString("0.0")),
                    new ReportParameter("parameterLOWPD",vmas_data.LOWPD),
                    new ReportParameter("parameterHIGHCO",double.Parse(vmas_data.COHIGHCLZ).ToString("0.00")),
                    new ReportParameter("parameterHIGHCOXZ",vmas_data.COHIGHXZ==""?"—":"≤"+double.Parse(vmas_data.COHIGHXZ).ToString("0.0")),
                    new ReportParameter("parameterHIGHHC",vmas_data.HCHIGHCLZ),
                    new ReportParameter("parameterHIGHHCXZ",vmas_data.HCHIGHXZ==""?"—":"≤"+vmas_data.HCHIGHXZ),
                    new ReportParameter("parameterHIGHPD",vmas_data.HIGHPD==""?"—":vmas_data.HIGHPD),

                    new ReportParameter("parameterLOWZS",vmas_data.ZSLOW),
                    new ReportParameter("parameterHIGHZS",vmas_data.ZSHIGH),
                    new ReportParameter("parameterLOWCOXZZ",vmas_data.COLOWXXZ),
                    new ReportParameter("parameterHIGHCOXZZ",vmas_data.COHIGHXXZ),
                    new ReportParameter("parameterLOWCOXYZ",vmas_data.COLOWXYZ),
                    new ReportParameter("parameterHIGHCOXYZ",vmas_data.COHIGHXYZ),
                    new ReportParameter("parameterLOWCO2XYZ",vmas_data.CO2LOWXYZ),
                    new ReportParameter("parameterHIGHCO2XYZ",vmas_data.CO2HIGHXYZ),
                    new ReportParameter("parameterLOWCO2",double.Parse(vmas_data.CO2LOW).ToString("0.00")),
                    new ReportParameter("parameterHIGHCO2",double.Parse(vmas_data.CO2HIGH).ToString("0.00")),
                    new ReportParameter("parameterLOWHCXYZ",vmas_data.HCLOWXYZ),
                    new ReportParameter("parameterHIGHHCXYZ",vmas_data.HCHIGHXYZ),

                    new ReportParameter("parameterZHPD",(vmas_data.ZHPD=="合格")?"通过":"未通过"),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),                    
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),
                    new ReportParameter("parameterJCZMC",mainPanel.stationinfmodel.STATIONNAME)
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
