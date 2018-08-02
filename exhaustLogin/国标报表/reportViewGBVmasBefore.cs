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
    public partial class reportViewGBVmasBefore : Form
    {
        SYS_DAL.VMASdal asmdal = new SYS_DAL.VMASdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewGBVmasBefore()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
            //reportViewer1.LocalReport.ReportPath = Application.StartupPath + "\\ReportBDAsm.rdlc";
        }
        private void init_wsd()
        {
            StringBuilder temp = new StringBuilder();
            temp.Length = 2048;
            ini.INIIO.GetPrivateProfileString("环境", "温度", "", temp, 2048, @".\报表数据.ini");
            printer.wsdthisTime.wd = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("环境", "湿度", "", temp, 2048, @".\报表数据.ini");
            printer.wsdthisTime.sd = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("环境", "大气压", "", temp, 2048, @".\报表数据.ini");
            printer.wsdthisTime.dqy = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("环境", "是否应用", "", temp, 2048, @".\报表数据.ini");
            if (temp.ToString().Trim() == "是")
                printer.wsdthisTime.isUseWsd = true;
            else
                printer.wsdthisTime.isUseWsd = false;
        }
        public void display_Asm(string clid)
        {
            SYS.Model.VMAS vmas_data = asmdal.Get_VMAS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            init_wsd();
            if (printer.wsdthisTime.isUseWsd)
            {
                vmas_data.WD = printer.wsdthisTime.wd;
                vmas_data.SD = printer.wsdthisTime.sd;
                vmas_data.DQY = printer.wsdthisTime.dqy;
            }
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
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB3847-2005"),
                    new ReportParameter("parameterJCFF","加载减速法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterLxfs", carinf.LXDH),
                    new ReportParameter("parameterClxh", carinf.XH),
                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL+"kg"),
                    new ReportParameter("parameterZzl", carinf.ZZL+"kg"),
                    new ReportParameter("parameterQdltqy", carinf.QDLTQY+"kPa"),
                    new ReportParameter("parameterBsqxs",carinf.BSQXS),
                    new ReportParameter("parameterJqfs",carinf.JQFS),
                    new ReportParameter("parameterFdjxh",carinf.FDJXH),
                    new ReportParameter("parameterGyfs",carinf.GYFS),
                    new ReportParameter("parameterRlzl",carinf.RLZL),
                    new ReportParameter("parameterDws",carinf.DWS),
                    new ReportParameter("parameterQGS",carinf.QGS),
                    new ReportParameter("parameterFDJH",carinf.FDJHM),
                    new ReportParameter("parameterXslc",carinf.XSLC+"km"),
                    new ReportParameter("parameterFdjpl",carinf.FDJPL+"L"),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",carinf.CLLX),
                    new ReportParameter("parameterHpzl",carinf.CPYS),
                    new ReportParameter("parameterEdgl",carinf.EDGL+"kW"),
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
                    new ReportParameter("parameterLljxh",vmas_data.LLJXH),

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
                    new ReportParameter("parameterZHPD",(vmas_data.ZHPD=="合格")?"通过":"未通过"),

                    new ReportParameter("parameterJCCS","第"+carinf.JCCS+"次"), 
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONPHONE),
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

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
