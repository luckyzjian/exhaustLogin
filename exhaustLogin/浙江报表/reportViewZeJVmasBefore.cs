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
    public partial class reportViewZeJVmasBefore : Form
    {
        SYS_DAL.VMASdal vmasdal = new SYS_DAL.VMASdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewZeJVmasBefore()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }
        public void display_VmasAfter(string clid)
        {
            SYS.Model.VMAS vmas_data = vmasdal.Get_VMAS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
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
                    new ReportParameter("parameterJCYJ","GB3847-2005"),
                    new ReportParameter("parameterJCFF","加载减速法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterHdzk", carinf.HDZK),
                    new ReportParameter("parameterRygg", carinf.RYPH),
                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterClscqy", carinf.SCQY),
                    new ReportParameter("parameterCllx", carinf.CLLX),
                    new ReportParameter("parameterPdyj", "依据GB18285-2005和DB33/660-2016标准,对"+carinf.CLHP),

                    new ReportParameter("parameterLljxh", vmas_data.LLJXH),
                    new ReportParameter("parameterLljbh", vmas_data.LLJBH),

                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterClxh", carinf.XH),
                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterQdltqy", carinf.QDLTQY),
                    new ReportParameter("parameterBsqxs",carinf.BSQXS),
                    new ReportParameter("parameterJqfs",carinf.JQFS),
                    new ReportParameter("parameterFdjxh",carinf.FDJXH),
                    new ReportParameter("parameterGyfs",carinf.GYFS),
                    new ReportParameter("parameterRlzl",carinf.RLZL),
                    new ReportParameter("parameterDws",carinf.DWS),
                    new ReportParameter("parameterQGS",carinf.QGS),
                    new ReportParameter("parameterFDJH",carinf.FDJHM),
                    new ReportParameter("parameterXslc",carinf.XSLC),
                    new ReportParameter("parameterFdjpl",carinf.FDJPL),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",carinf.CLLX),
                    new ReportParameter("parameterHpzl",carinf.CPYS),
                    new ReportParameter("parameterEdgl",carinf.EDGL),
                    new ReportParameter("parameterEdzs",carinf.EDZS),
                    new ReportParameter("parameterRylx",carinf.RYPH),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",carinfo.CZDZ),
                    new ReportParameter("parameterCzdh",carinf.LXDH),
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    
                    new ReportParameter("parameterDczz",carinfo.HDZZL+"kg"),
                    new ReportParameter("parameterFdjcs",carinfo.FDJSCQY),
                    new ReportParameter("parameterDpxh",carinfo.SSXQ),
                    
                    new ReportParameter("parameterSbrzbm",mainPanel.stationinfmodel.STATIONDATE),
                    new ReportParameter("parameterSbmc",vmas_data.SBMC),
                    new ReportParameter("parameterSbbh",vmas_data.CGJBH+"/"+vmas_data.FXYBH),
                    new ReportParameter("parameterSbxh",vmas_data.SBXH),
                    new ReportParameter("parameterSbcj",vmas_data.SBZZC),
                    new ReportParameter("parameterCgjxh",vmas_data.CGJXH),
                    new ReportParameter("parameterCgjbh",vmas_data.CGJBH),
                    new ReportParameter("parameterCgjcj",vmas_data.CGJZZC),
                    new ReportParameter("parameterFxyxh",vmas_data.FXYXH),
                    new ReportParameter("parameterFxybh",vmas_data.FXYBH),
                    new ReportParameter("parameterFxycj",vmas_data.FXYZZC),

                    new ReportParameter("parameterWd",vmas_data.WD+"℃"),
                    new ReportParameter("parameterDqy",vmas_data.DQY+"kPa"),
                    new ReportParameter("parameterSd",vmas_data.SD+"%"),
                    
                    new ReportParameter("parameterCOCLZ",vmas_data.COZL),
                    new ReportParameter("parameterCOXZ","≤"+vmas_data.COXZ),
                    new ReportParameter("parameterCOPD",vmas_data.COPD),
                    new ReportParameter("parameterHCCLZ",vmas_data.HCZL),
                    new ReportParameter("parameterHCXZ","--"),
                    new ReportParameter("parameterHCPD","--"),
                    new ReportParameter("parameterNOXCLZ",vmas_data.NOXZL),
                    new ReportParameter("parameterNOXXZ","--"),
                    new ReportParameter("parameterNOXPD","--"),
                    new ReportParameter("parameterHcnoxclz",(float.Parse(vmas_data.HCZL)+float.Parse(vmas_data.NOXZL)).ToString("0.00")),
                    new ReportParameter("parameterHcnoxxz","≤"+vmas_data.HCXZ),
                    new ReportParameter("parameterHcnoxpd",vmas_data.HCPD),
                    new ReportParameter("parameterZHPD",vmas_data.ZHPD),
                    new ReportParameter("parameterJCCS","第"+carinf.JCCS+"次"), 
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),

                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),

                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONADD+" "+mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterJCZMC",carinf.JCZMC.Split('_')[1]+"号线")
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
        public void display_VmasBefore(string clid)
        {
            SYS.Model.VMAS vmas_data = vmasdal.Get_VMAS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
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
                    new ReportParameter("parameterJCYJ","GB3847-2005"),
                    new ReportParameter("parameterJCFF","加载减速法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterHdzk", carinf.HDZK),
                    new ReportParameter("parameterRygg", carinf.RYPH),
                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterClscqy", carinf.SCQY),
                    new ReportParameter("parameterCllx", carinf.CLLX),
                    new ReportParameter("parameterPdyj", "依据GB18285-2005和DB33/660-2013标准,对"+carinf.CLHP),

                    new ReportParameter("parameterLljxh", vmas_data.LLJXH),
                    new ReportParameter("parameterLljbh", vmas_data.LLJBH),

                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterClxh", carinf.XH),
                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterQdltqy", carinf.QDLTQY),
                    new ReportParameter("parameterBsqxs",carinf.BSQXS),
                    new ReportParameter("parameterJqfs",carinf.JQFS),
                    new ReportParameter("parameterFdjxh",carinf.FDJXH),
                    new ReportParameter("parameterGyfs",carinf.GYFS),
                    new ReportParameter("parameterRlzl",carinf.RLZL),
                    new ReportParameter("parameterDws",carinf.DWS),
                    new ReportParameter("parameterQGS",carinf.QGS),
                    new ReportParameter("parameterFDJH",carinf.FDJHM),
                    new ReportParameter("parameterXslc",carinf.XSLC),
                    new ReportParameter("parameterFdjpl",carinf.FDJPL),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",carinf.CLLX),
                    new ReportParameter("parameterHpzl",carinf.CPYS),
                    new ReportParameter("parameterEdgl",carinf.EDGL),
                    new ReportParameter("parameterEdzs",carinf.EDZS),
                    new ReportParameter("parameterRylx",carinf.RYPH),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",carinfo.CZDZ),
                    new ReportParameter("parameterCzdh",carinf.LXDH),
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    
                    new ReportParameter("parameterDczz",carinfo.HDZZL+"kg"),
                    new ReportParameter("parameterFdjcs",carinfo.FDJSCQY),
                    new ReportParameter("parameterDpxh",carinfo.SSXQ),
                    
                    new ReportParameter("parameterSbrzbm",mainPanel.stationinfmodel.STATIONDATE),
                    new ReportParameter("parameterSbmc",vmas_data.SBMC),
                    new ReportParameter("parameterSbbh",vmas_data.CGJBH+"/"+vmas_data.FXYBH),
                    new ReportParameter("parameterSbxh",vmas_data.SBXH),
                    new ReportParameter("parameterSbcj",vmas_data.SBZZC),
                    new ReportParameter("parameterCgjxh",vmas_data.CGJXH),
                    new ReportParameter("parameterCgjbh",vmas_data.CGJBH),
                    new ReportParameter("parameterCgjcj",vmas_data.CGJZZC),
                    new ReportParameter("parameterFxyxh",vmas_data.FXYXH),
                    new ReportParameter("parameterFxybh",vmas_data.FXYBH),
                    new ReportParameter("parameterFxycj",vmas_data.FXYZZC),

                    new ReportParameter("parameterWd",vmas_data.WD+"℃"),
                    new ReportParameter("parameterDqy",vmas_data.DQY+"kPa"),
                    new ReportParameter("parameterSd",vmas_data.SD+"%"),
                    
                    new ReportParameter("parameterCOCLZ",vmas_data.COZL),
                    new ReportParameter("parameterCOXZ","≤"+vmas_data.COXZ),
                    new ReportParameter("parameterCOPD",vmas_data.COPD),
                    new ReportParameter("parameterHCCLZ",vmas_data.HCZL),
                    new ReportParameter("parameterHCXZ","≤"+vmas_data.HCXZ),
                    new ReportParameter("parameterHCPD",vmas_data.HCPD),
                    new ReportParameter("parameterNOXCLZ",vmas_data.NOXZL),
                    new ReportParameter("parameterNOXXZ","≤"+vmas_data.NOXXZ),
                    new ReportParameter("parameterNOXPD",vmas_data.NOXPD),
                    new ReportParameter("parameterHcnoxclz",(float.Parse(vmas_data.HCZL)+float.Parse(vmas_data.NOXZL)).ToString("0.00")),
                    new ReportParameter("parameterHcnoxxz","--"),
                    new ReportParameter("parameterHcnoxpd","--"),
                    new ReportParameter("parameterZHPD",vmas_data.ZHPD),
                    new ReportParameter("parameterJCCS","第"+carinf.JCCS+"次"), 
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),

                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONADD+" "+mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterJCZMC",carinf.JCZMC.Split('_')[1]+"号线")
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
