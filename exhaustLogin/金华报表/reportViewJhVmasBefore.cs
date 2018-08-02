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
using JHWebClient;

namespace exhaustDetect
{
    public partial class reportViewJhVmasBefore : Form
    {
        SYS_DAL.VMASdal vmasdal = new SYS_DAL.VMASdal();
        SYS_DAL.JZJSdal jzjsdal = new SYS_DAL.JZJSdal();
        SYS_DAL.Zyjsdal zyjsdal = new SYS_DAL.Zyjsdal();
        SYS_DAL.SDSdal sdsdal = new SYS_DAL.SDSdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewJhVmasBefore()
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
            //if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ZJNETMODE)
            //{
            //    carinf.LSH = printer.ZJNETLSH;
            //}
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
                rzbh =  mainPanel.stationinfmodel.STATIONDATE;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB18285-2005《点燃式发动机汽车排气污染物排放限值及测量方法(双怠速法及简易工况法)》"+"\r\n"+"DB33/660-2016《在用点燃式发动机轻型汽车简易工况法排气污染物排放限值》"),
                    new ReportParameter("parameterBbbt",mainPanel.addname+"机动车排气污染物检测报告单"),
                    new ReportParameter("parameterJCFF","简易瞬态工况法"),
                    new ReportParameter("parameterLsh",carinf.LSH),
                    new ReportParameter("parameterLwbh", printer.ZJNETLSH==""?"—":printer.ZJNETLSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.jcgcsj),
                    new ReportParameter("parameterJcxh", carinf.JCZMC.Split('_')[1]+"号线"),

                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    //new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterJCCS", carinf.JCCS),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterWgjyy", carinf.wjy),
                    new ReportParameter("parameterClxh", (carinf.XH=="")?"--":carinf.XH),
                    new ReportParameter("parameterScqy",(carinf.SCQY=="")?"--":carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",(carinf.BSQXS == "") ? "--" : carinf.BSQXS),
                    new ReportParameter("parameterJqfs",(carinf.JQFS == "") ? "--" : carinf.JQFS),
                    new ReportParameter("parameterFdjxh",(carinf.FDJXH == "") ? "--" : carinf.FDJXH),
                    new ReportParameter("parameterFdjcs",(carinfo.FDJSCQY == "") ? "--" : carinfo.FDJSCQY),
                    new ReportParameter("parameterGyfs",(carinf.GYFS == "") ? "--" : carinf.GYFS),
                    new ReportParameter("parameterRlzl",(carinf.RLZL == "") ? "--" : carinf.RLZL),
                    new ReportParameter("parameterRylx",(carinf.RYPH == "") ? "--" : carinf.RYPH),
                    new ReportParameter("parameterDws",(carinf.DWS == "") ? "--" : carinf.DWS),
                    new ReportParameter("parameterQGS",(carinf.QGS == "") ? "--" : carinf.QGS),
                    new ReportParameter("parameterFDJH",(carinf.FDJHM == "") ? "--" : carinf.FDJHM),
                    new ReportParameter("parameterXslc",(carinf.XSLC == "") ? "--" : carinf.XSLC),
                    new ReportParameter("parameterFdjpl",(carinf.FDJPL == "") ? "--" : carinf.FDJPL),
                    new ReportParameter("parameterQdltqy", (carinf.QDLTQY == "") ? "--" : carinf.QDLTQY),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",(carinf.CLLX == "") ? "--" : carinf.CLLX),
                    new ReportParameter("parameterHpzl",(carinf.CPYS == "") ? "--" : carinf.CPYS),
                    new ReportParameter("parameterEdgl",(carinf.EDGL == "") ? "--" : carinf.EDGL),
                    new ReportParameter("parameterEdzs",(carinf.EDZS == "") ? "--" : carinf.EDZS),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",""),
                    new ReportParameter("parameterCzdh",(carinf.LXDH == "") ? "--" : carinf.LXDH),
                    new ReportParameter("parameterQdfs",(carinf.QDXS == "") ? "--" : carinf.QDXS),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",(carinf.CLSBM == "") ? "--" : carinf.CLSBM),
                    new ReportParameter("parameterCz",(carinf.CZ == "") ? "--" : carinf.CZ),
                    new ReportParameter("parameterSyxz",carinf.SYXZ),
                    new ReportParameter("parameterHdzk",carinf.HDZK),

                    new ReportParameter("parameterDczz",carinfo.HDZZL+"kg"),
                    new ReportParameter("parameterFdjcs",carinfo.FDJSCQY),
                    new ReportParameter("parameterDpxh",carinfo.SSXQ),

                    new ReportParameter("parameterCgjxh",(vmas_data.CGJXH=="")?"--":vmas_data.CGJXH),
                    new ReportParameter("parameterCgjbh",(vmas_data.CGJBH=="")?"--":vmas_data.CGJBH),
                    new ReportParameter("parameterCgjzzc",(vmas_data.CGJZZC=="")?"--":vmas_data.CGJZZC),
                    new ReportParameter("parameterFxyxh",(vmas_data.FXYXH=="")?"--":vmas_data.FXYXH),
                    new ReportParameter("parameterFxybh",(vmas_data.FXYBH=="")?"--":vmas_data.FXYBH),
                    new ReportParameter("parameterFxyzzc",(vmas_data.FXYZZC=="")?"--":vmas_data.FXYZZC),
                    new ReportParameter("parameterLljxh",(vmas_data.LLJXH=="")?"--":vmas_data.LLJXH),
                    new ReportParameter("parameterLljbh", (vmas_data.LLJBH=="")?"--":vmas_data.LLJBH),
                    new ReportParameter("parameterLljzzc", (vmas_data.LLJZZC=="")?"--":vmas_data.LLJZZC),

                    new ReportParameter("parameterWd",vmas_data.WD),
                    new ReportParameter("parameterDqy",vmas_data.DQY),
                    new ReportParameter("parameterSd",vmas_data.SD),

                    new ReportParameter("parameterVmassbmc","机动车排气分析仪"),
                    new ReportParameter("parameterCOCLZ",vmas_data.COZL),
                    new ReportParameter("parameterCOXZ",vmas_data.COXZ),
                    new ReportParameter("parameterCOPD",(vmas_data.COPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterHCCLZ",vmas_data.HCZL),
                    new ReportParameter("parameterHCXZ","--"),
                    new ReportParameter("parameterHCPD","/"),
                    new ReportParameter("parameterNOXCLZ",vmas_data.NOXZL),
                    new ReportParameter("parameterNOXXZ","--"),
                    new ReportParameter("parameterNOXPD","/"),
                    new ReportParameter("parameterHcnoxclz",(float.Parse(vmas_data.HCZL)+float.Parse(vmas_data.NOXZL)).ToString("0.00")),
                    new ReportParameter("parameterHcnoxxz",vmas_data.HCXZ),
                    new ReportParameter("parameterHcnoxpd",(vmas_data.HCPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterSdssbmc","--"),
                     new ReportParameter("parameterSdssbxh","--"),
                    new ReportParameter("parameterSdssbrzbm","--"),

                     new ReportParameter("parameterLOWHC","--"),
                    new ReportParameter("parameterLOWHCXZ","--"),
                    new ReportParameter("parameterLOWCO","--"),
                    new ReportParameter("parameterLOWCOXZ","--"),
                    new ReportParameter("parameterLOWPD","--"),
                    new ReportParameter("parameterHIGHCO","--"),
                    new ReportParameter("parameterHIGHCOXZ","--"),
                    new ReportParameter("parameterHIGHHC","--"),
                    new ReportParameter("parameterHIGHHCXZ","--"),
                    new ReportParameter("parameterLAMDA","--"),
                    new ReportParameter("parameterLAMDAXZ","--"),
                    new ReportParameter("parameterLAMDAPD","--"),
                    new ReportParameter("parameterHIGHPD","--"),
                    new ReportParameter("parameterLOWCOPD","--"),
                    new ReportParameter("parameterLOWHCPD","--"),
                    new ReportParameter("parameterHIGHHCPD","--"),
                    new ReportParameter("parameterHIGHCOPD","--"),

                    new ReportParameter("parameterLugydjmc","--"),
                    new ReportParameter("parameterLugydjcj","--"),
                    new ReportParameter("parameterLugcgjxh","--"),
                    new ReportParameter("parameterLugcgjrzbm","--"),
                    new ReportParameter("parameterLugydjxh","--"),
                    new ReportParameter("parameterLugydjrzbm","--"),

                    new ReportParameter("parameterHK","--"),
                    new ReportParameter("parameterNK","--"),
                    new ReportParameter("parameterEK","--"),
                    new ReportParameter("parameterJZJSKXZ","--"),
                    new ReportParameter("parameterHKPD","--"),
                    new ReportParameter("parameterNKPD","--"),
                    new ReportParameter("parameterEKPD","--"),
                    new ReportParameter("parameterMaxHP","--"),
                    new ReportParameter("parameterLBGL","--"),
                    new ReportParameter("parameterLBZS","--"),
                    new ReportParameter("parameterGLXZ","--"),
                    new ReportParameter("parameterGLPD","--"),
                    new ReportParameter("parameterYdpd","--"),
                    new ReportParameter("parameterLUGGLB","--"),

                    new ReportParameter("parameterBtgsbmc","--"),
                    new ReportParameter("parameterBtgsbcj","--"),
                    new ReportParameter("parameterBtgsbxh","--"),
                    new ReportParameter("parameterBtgsbrzbm","--"),

                    new ReportParameter("parameterDSZS","--"),
                    new ReportParameter("parameterFIRSTDATA","--"),
                    new ReportParameter("parameterSECONDDATA","--"),
                    new ReportParameter("parameterTHIRDDATA","--"),
                    new ReportParameter("parameterBTGXZ","--"),
                    new ReportParameter("parameterPJZ","--"),
                    new ReportParameter("parameterBTGPD","--"),

                    new ReportParameter("parameterLzDszs","--"),
                new ReportParameter("parameterLzFirstData", "--"),
                new ReportParameter("parameterLzSecondData", "--"),
                new ReportParameter("parameterLzThirdData", "--"),
                new ReportParameter("parameterLzForthData", "--"),
                new ReportParameter("parameterLzAveData", "--"),
                new ReportParameter("parameterLzXz", "--"),
                new ReportParameter("parameterLzPd", "--"),
                new ReportParameter("parameterLzSbrzbm", "--"),
                new ReportParameter("parameterLzSbxh", "--"),
                
                new ReportParameter("parameterPdyj","依据GB18285-2005，DB33/660-2016标准，对"+vmas_data.CLPH+"(汽油车)进行"),
                new ReportParameter("parameterZHPD",vmas_data.ZHPD),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),

                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),

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
        public void display_VmasBefore(string clid)
        {
            SYS.Model.VMAS vmas_data = vmasdal.Get_VMAS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            //if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ZJNETMODE)
            //{
            //    carinf.LSH = printer.ZJNETLSH;
            //}
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
                rzbh =  mainPanel.stationinfmodel.STATIONDATE;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            //string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB18285-2005《点燃式发动机汽车排气污染物排放限值及测量方法(双怠速法及简易工况法)》"+"\r\n"+"DB33/660-2016《在用点燃式发动机轻型汽车简易工况法排气污染物排放限值》"),
                    new ReportParameter("parameterBbbt",mainPanel.addname+"机动车排气污染物检测报告单"),
                    new ReportParameter("parameterJCFF","简易瞬态工况法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterLwbh", printer.ZJNETLSH==""?"—":printer.ZJNETLSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.jcgcsj),
                    new ReportParameter("parameterJcxh", carinf.JCZMC.Split('_')[1]+"号线"),


                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    //new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterJCCS", carinf.JCCS),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterWgjyy", carinf.wjy),
                    new ReportParameter("parameterClxh", (carinf.XH=="")?"--":carinf.XH),
                    new ReportParameter("parameterScqy",(carinf.SCQY=="")?"--":carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",(carinf.BSQXS == "") ? "--" : carinf.BSQXS),
                    new ReportParameter("parameterJqfs",(carinf.JQFS == "") ? "--" : carinf.JQFS),
                    new ReportParameter("parameterFdjxh",(carinf.FDJXH == "") ? "--" : carinf.FDJXH),
                    new ReportParameter("parameterFdjcs",(carinfo.FDJSCQY == "") ? "--" : carinfo.FDJSCQY),
                    new ReportParameter("parameterGyfs",(carinf.GYFS == "") ? "--" : carinf.GYFS),
                    new ReportParameter("parameterRlzl",(carinf.RLZL == "") ? "--" : carinf.RLZL),
                    new ReportParameter("parameterRylx",(carinf.RYPH == "") ? "--" : carinf.RYPH),
                    new ReportParameter("parameterDws",(carinf.DWS == "") ? "--" : carinf.DWS),
                    new ReportParameter("parameterQGS",(carinf.QGS == "") ? "--" : carinf.QGS),
                    new ReportParameter("parameterFDJH",(carinf.FDJHM == "") ? "--" : carinf.FDJHM),
                    new ReportParameter("parameterXslc",(carinf.XSLC == "") ? "--" : carinf.XSLC),
                    new ReportParameter("parameterFdjpl",(carinf.FDJPL == "") ? "--" : carinf.FDJPL),
                    new ReportParameter("parameterQdltqy", (carinf.QDLTQY == "") ? "--" : carinf.QDLTQY),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",(carinf.CLLX == "") ? "--" : carinf.CLLX),
                    new ReportParameter("parameterHpzl",(carinf.CPYS == "") ? "--" : carinf.CPYS),
                    new ReportParameter("parameterEdgl",(carinf.EDGL == "") ? "--" : carinf.EDGL),
                    new ReportParameter("parameterEdzs",(carinf.EDZS == "") ? "--" : carinf.EDZS),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",""),
                    new ReportParameter("parameterCzdh",(carinf.LXDH == "") ? "--" : carinf.LXDH),
                    new ReportParameter("parameterQdfs",(carinf.QDXS == "") ? "--" : carinf.QDXS),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",(carinf.CLSBM == "") ? "--" : carinf.CLSBM),
                    new ReportParameter("parameterCz",(carinf.CZ == "") ? "--" : carinf.CZ),
                    new ReportParameter("parameterSyxz",carinf.SYXZ),
                    new ReportParameter("parameterHdzk",carinf.HDZK),

                    new ReportParameter("parameterDczz",carinfo.HDZZL+"kg"),
                    new ReportParameter("parameterFdjcs",carinfo.FDJSCQY),
                    new ReportParameter("parameterDpxh",carinfo.SSXQ),

                    new ReportParameter("parameterSbrzbm",mainPanel.stationinfmodel.STATIONDATE),
                    new ReportParameter("parameterSbmc",vmas_data.SBMC),
                    new ReportParameter("parameterSbbh",vmas_data.CGJBH+"/"+vmas_data.FXYBH),
                    new ReportParameter("parameterSbxh",vmas_data.SBXH),
                    new ReportParameter("parameterSbcj",vmas_data.SBZZC),
                    
                    new ReportParameter("parameterLljxh",(vmas_data.LLJXH=="")?"--":vmas_data.LLJXH),
                    new ReportParameter("parameterLljbh", (vmas_data.LLJBH=="")?"--":vmas_data.LLJBH),
                    new ReportParameter("parameterLljzzc", (vmas_data.LLJZZC=="")?"--":vmas_data.LLJZZC),
                    new ReportParameter("parameterCgjxh",(vmas_data.CGJXH=="")?"--":vmas_data.CGJXH),
                    new ReportParameter("parameterCgjbh",(vmas_data.CGJBH=="")?"--":vmas_data.CGJBH),
                    new ReportParameter("parameterCgjzzc",(vmas_data.CGJZZC=="")?"--":vmas_data.CGJZZC),
                    new ReportParameter("parameterFxyxh",(vmas_data.FXYXH=="")?"--":vmas_data.FXYXH),
                    new ReportParameter("parameterFxybh",(vmas_data.FXYBH=="")?"--":vmas_data.FXYBH),
                    new ReportParameter("parameterFxyzzc",(vmas_data.FXYZZC=="")?"--":vmas_data.FXYZZC),

                    new ReportParameter("parameterWd",vmas_data.WD),
                    new ReportParameter("parameterDqy",vmas_data.DQY),
                    new ReportParameter("parameterSd",vmas_data.SD),

                    new ReportParameter("parameterVmassbmc","机动车排气分析仪"),
                    new ReportParameter("parameterCOCLZ",vmas_data.COZL),
                    new ReportParameter("parameterCOXZ",vmas_data.COXZ),
                    new ReportParameter("parameterCOPD",(vmas_data.COPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterHCCLZ",vmas_data.HCZL),
                    new ReportParameter("parameterHCXZ",vmas_data.HCXZ),
                    new ReportParameter("parameterHCPD",(vmas_data.HCPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterNOXCLZ",vmas_data.NOXZL),
                    new ReportParameter("parameterNOXXZ",vmas_data.NOXXZ),
                    new ReportParameter("parameterNOXPD",(vmas_data.NOXPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterHcnoxclz",(float.Parse(vmas_data.HCZL)+float.Parse(vmas_data.NOXZL)).ToString("0.00")),
                    new ReportParameter("parameterHcnoxxz","--"),
                    new ReportParameter("parameterHcnoxpd","/"),

                    new ReportParameter("parameterSdssbmc","--"),
                     new ReportParameter("parameterSdssbxh","--"),
                    new ReportParameter("parameterSdssbrzbm","--"),

                     new ReportParameter("parameterLOWHC","--"),
                    new ReportParameter("parameterLOWHCXZ","--"),
                    new ReportParameter("parameterLOWCO","--"),
                    new ReportParameter("parameterLOWCOXZ","--"),
                    new ReportParameter("parameterLOWPD","--"),
                    new ReportParameter("parameterHIGHCO","--"),
                    new ReportParameter("parameterHIGHCOXZ","--"),
                    new ReportParameter("parameterHIGHHC","--"),
                    new ReportParameter("parameterHIGHHCXZ","--"),
                    new ReportParameter("parameterLAMDA","--"),
                    new ReportParameter("parameterLAMDAXZ","--"),
                    new ReportParameter("parameterLAMDAPD","--"),
                    new ReportParameter("parameterHIGHPD","--"),
                    new ReportParameter("parameterLOWCOPD","--"),
                    new ReportParameter("parameterLOWHCPD","--"),
                    new ReportParameter("parameterHIGHHCPD","--"),
                    new ReportParameter("parameterHIGHCOPD","--"),

                    new ReportParameter("parameterLugydjmc","--"),
                    new ReportParameter("parameterLugydjcj","--"),
                    new ReportParameter("parameterLugcgjxh","--"),
                    new ReportParameter("parameterLugcgjrzbm","--"),
                    new ReportParameter("parameterLugydjxh","--"),
                    new ReportParameter("parameterLugydjrzbm","--"),
                    
                    new ReportParameter("parameterHK","--"),
                    new ReportParameter("parameterNK","--"),
                    new ReportParameter("parameterEK","--"),
                    new ReportParameter("parameterJZJSKXZ","--"),
                    new ReportParameter("parameterHKPD","--"),
                    new ReportParameter("parameterNKPD","--"),
                    new ReportParameter("parameterEKPD","--"),
                    new ReportParameter("parameterMaxHP","--"),
                    new ReportParameter("parameterLBGL","--"),
                    new ReportParameter("parameterLBZS","--"),
                    new ReportParameter("parameterGLXZ","--"),
                    new ReportParameter("parameterGLPD","--"),
                    new ReportParameter("parameterYdpd","--"),
                    new ReportParameter("parameterLUGGLB","--"),

                    new ReportParameter("parameterBtgsbmc","--"),
                    new ReportParameter("parameterBtgsbcj","--"),
                    new ReportParameter("parameterBtgsbxh","--"),
                    new ReportParameter("parameterBtgsbrzbm","--"),
                    
                    new ReportParameter("parameterDSZS","--"),
                    new ReportParameter("parameterFIRSTDATA","--"),
                    new ReportParameter("parameterSECONDDATA","--"),
                    new ReportParameter("parameterTHIRDDATA","--"),
                    new ReportParameter("parameterBTGXZ","--"),
                    new ReportParameter("parameterPJZ","--"),
                    new ReportParameter("parameterBTGPD","--"),

                    new ReportParameter("parameterLzDszs","--"),
                new ReportParameter("parameterLzFirstData", "--"),
                new ReportParameter("parameterLzSecondData", "--"),
                new ReportParameter("parameterLzThirdData", "--"),
                new ReportParameter("parameterLzForthData", "--"),
                new ReportParameter("parameterLzAveData", "--"),
                new ReportParameter("parameterLzXz", "--"),
                new ReportParameter("parameterLzPd", "--"),
                new ReportParameter("parameterLzSbrzbm", "--"),
                new ReportParameter("parameterLzSbxh", "--"),
                
                new ReportParameter("parameterPdyj","依据GB18285-2005，DB33/660-2016标准，对"+vmas_data.CLPH+"(汽油车)进行"),
                   new ReportParameter("parameterZHPD",vmas_data.ZHPD),
                 new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),

                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
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
        public void display_Sds(string clid)
        {
            SYS.Model.SDS asm_data = sdsdal.Get_SDS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ZJNETMODE)
            {
                carinf.LSH = printer.ZJNETLSH;
            }
            string lambdapd = "--";
            if (asm_data.LAMDAHIGHPD != "")
                lambdapd = (asm_data.LAMDAHIGHPD == "合格") ? "〇" : "×";
            else
                lambdapd = "/";

            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
            }
            if (mainPanel.isdisplayCMANo)
            {
                rzbh =  mainPanel.stationinfmodel.STATIONDATE;
            }
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB18285-2005《点燃式发动机汽车排气污染物排放限值及测量方法(双怠速法及简易工况法)》"),
                    new ReportParameter("parameterBbbt",mainPanel.addname+"机动车排气污染物检测报告单"),
                    new ReportParameter("parameterJCFF","双怠速法"),
                    new ReportParameter("parameterLsh",carinf.LSH),
                    new ReportParameter("parameterLwbh", printer.ZJNETLSH==""?"—":printer.ZJNETLSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.jcgcsj),
                    new ReportParameter("parameterJcxh", carinf.JCZMC.Split('_')[1]+"号线"),

                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    //new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterJCCS", carinf.JCCS),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterWgjyy", carinf.wjy),
                    new ReportParameter("parameterClxh", (carinf.XH=="")?"--":carinf.XH),
                    new ReportParameter("parameterScqy",(carinf.SCQY=="")?"--":carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",(carinf.BSQXS == "") ? "--" : carinf.BSQXS),
                    new ReportParameter("parameterJqfs",(carinf.JQFS == "") ? "--" : carinf.JQFS),
                    new ReportParameter("parameterFdjxh",(carinf.FDJXH == "") ? "--" : carinf.FDJXH),
                    new ReportParameter("parameterFdjcs",(carinfo.FDJSCQY == "") ? "--" : carinfo.FDJSCQY),
                    new ReportParameter("parameterGyfs",(carinf.GYFS == "") ? "--" : carinf.GYFS),
                    new ReportParameter("parameterRlzl",(carinf.RLZL == "") ? "--" : carinf.RLZL),
                    new ReportParameter("parameterRylx",(carinf.RYPH == "") ? "--" : carinf.RYPH),
                    new ReportParameter("parameterDws",(carinf.DWS == "") ? "--" : carinf.DWS),
                    new ReportParameter("parameterQGS",(carinf.QGS == "") ? "--" : carinf.QGS),
                    new ReportParameter("parameterFDJH",(carinf.FDJHM == "") ? "--" : carinf.FDJHM),
                    new ReportParameter("parameterXslc",(carinf.XSLC == "") ? "--" : carinf.XSLC),
                    new ReportParameter("parameterFdjpl",(carinf.FDJPL == "") ? "--" : carinf.FDJPL),
                    new ReportParameter("parameterQdltqy", (carinf.QDLTQY == "") ? "--" : carinf.QDLTQY),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",(carinf.CLLX == "") ? "--" : carinf.CLLX),
                    new ReportParameter("parameterHpzl",(carinf.CPYS == "") ? "--" : carinf.CPYS),
                    new ReportParameter("parameterEdgl",(carinf.EDGL == "") ? "--" : carinf.EDGL),
                    new ReportParameter("parameterEdzs",(carinf.EDZS == "") ? "--" : carinf.EDZS),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",""),
                    new ReportParameter("parameterCzdh",(carinf.LXDH == "") ? "--" : carinf.LXDH),
                    new ReportParameter("parameterQdfs",(carinf.QDXS == "") ? "--" : carinf.QDXS),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",(carinf.CLSBM == "") ? "--" : carinf.CLSBM),
                    new ReportParameter("parameterCz",(carinf.CZ == "") ? "--" : carinf.CZ),
                    new ReportParameter("parameterSyxz",carinf.SYXZ),
                    new ReportParameter("parameterHdzk",carinf.HDZK),

                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterSdssbmc","机动车排气分析仪"),
                    new ReportParameter("parameterSdssbxh",(asm_data.FXYXH=="")?"--":asm_data.FXYXH),
                    new ReportParameter("parameterSdssbrzbm",(asm_data.FXYBH=="")?"--":asm_data.FXYBH),
                    
                    new ReportParameter("parameterLljxh","--"),
                    new ReportParameter("parameterLljbh", "--"),
                    new ReportParameter("parameterLljzzc", "--"),
                    new ReportParameter("parameterCgjxh","--"),
                    new ReportParameter("parameterCgjbh","--"),
                    new ReportParameter("parameterCgjzzc","--"),
                    new ReportParameter("parameterFxyxh","--"),
                    new ReportParameter("parameterFxybh","--"),
                    new ReportParameter("parameterFxyzzc","--"),


                    new ReportParameter("parameterVmassbmc","--"),
                    new ReportParameter("parameterCOCLZ","--"),
                    new ReportParameter("parameterCOXZ","--"),
                    new ReportParameter("parameterCOPD","--"),
                    new ReportParameter("parameterHCCLZ","--"),
                    new ReportParameter("parameterHCXZ","--"),
                    new ReportParameter("parameterHCPD","--"),
                    new ReportParameter("parameterNOXCLZ","--"),
                    new ReportParameter("parameterNOXXZ","--"),
                    new ReportParameter("parameterNOXPD","--"),
                    new ReportParameter("parameterHcnoxclz","--"),
                    new ReportParameter("parameterHcnoxxz","--"),
                    new ReportParameter("parameterHcnoxpd","--"),

                     new ReportParameter("parameterLOWHC",asm_data.HCLOWCLZ),
                    new ReportParameter("parameterLOWHCXZ",asm_data.HCLOWXZ),
                    new ReportParameter("parameterLOWCO",asm_data.COLOWCLZ),
                    new ReportParameter("parameterLOWCOXZ",asm_data.COLOWXZ),
                    new ReportParameter("parameterLOWPD",(asm_data.LOWPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterHIGHCO",asm_data.COHIGHCLZ),
                    new ReportParameter("parameterHIGHCOXZ",asm_data.COHIGHXZ),
                    new ReportParameter("parameterHIGHHC",asm_data.HCHIGHCLZ),
                    new ReportParameter("parameterHIGHHCXZ",asm_data.HCHIGHXZ),
                    new ReportParameter("parameterLAMDA",asm_data.LAMDAHIGHCLZ),
                    new ReportParameter("parameterLAMDAXZ",asm_data.LAMDAHIGHXZ),
                    new ReportParameter("parameterLAMDAPD",lambdapd),
                    new ReportParameter("parameterHIGHPD",(asm_data.HIGHPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterLOWCOPD",(asm_data.COLOWPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterLOWHCPD",(asm_data.HCLOWPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterHIGHHCPD",(asm_data.COHIGHPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterHIGHCOPD",(asm_data.HCHIGHPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterLugydjmc","--"),
                    new ReportParameter("parameterLugydjcj","--"),
                    new ReportParameter("parameterLugcgjxh","--"),
                    new ReportParameter("parameterLugcgjrzbm","--"),
                    new ReportParameter("parameterLugydjxh","--"),
                    new ReportParameter("parameterLugydjrzbm","--"),
                    
                    new ReportParameter("parameterHK","--"),
                    new ReportParameter("parameterNK","--"),
                    new ReportParameter("parameterEK","--"),
                    new ReportParameter("parameterJZJSKXZ","--"),
                    new ReportParameter("parameterHKPD","--"),
                    new ReportParameter("parameterNKPD","--"),
                    new ReportParameter("parameterEKPD","--"),
                    new ReportParameter("parameterMaxHP","--"),
                    new ReportParameter("parameterLBGL","--"),
                    new ReportParameter("parameterLBZS","--"),
                    new ReportParameter("parameterGLXZ","--"),
                    new ReportParameter("parameterGLPD","--"),
                    new ReportParameter("parameterYdpd","--"),
                    new ReportParameter("parameterLUGGLB","--"),

                    new ReportParameter("parameterBtgsbmc","--"),
                    new ReportParameter("parameterBtgsbcj","--"),
                    new ReportParameter("parameterBtgsbxh","--"),
                    new ReportParameter("parameterBtgsbrzbm","--"),
                    
                    new ReportParameter("parameterDSZS","--"),
                    new ReportParameter("parameterFIRSTDATA","--"),
                    new ReportParameter("parameterSECONDDATA","--"),
                    new ReportParameter("parameterTHIRDDATA","--"),
                    new ReportParameter("parameterBTGXZ","--"),
                    new ReportParameter("parameterPJZ","--"),
                    new ReportParameter("parameterBTGPD","--"),

                    new ReportParameter("parameterLzDszs","--"),
                new ReportParameter("parameterLzFirstData", "--"),
                new ReportParameter("parameterLzSecondData", "--"),
                new ReportParameter("parameterLzThirdData", "--"),
                new ReportParameter("parameterLzForthData", "--"),
                new ReportParameter("parameterLzAveData", "--"),
                new ReportParameter("parameterLzXz", "--"),
                new ReportParameter("parameterLzPd", "--"),
                new ReportParameter("parameterLzSbrzbm", "--"),
                new ReportParameter("parameterLzSbxh", "--"),
                
                new ReportParameter("parameterPdyj","依据GB18285-2005标准，对"+asm_data.CLPH+"(汽油车)进行"),
                    new ReportParameter("parameterZHPD",asm_data.ZHPD),

                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterJCZMC",carinf.JCZMC.Split('_')[1])
                };
                //reportViewer1.EnableExternalImages = true;
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.SetParameters(rptpara);
            }
            catch
            {
                throw;
            }
            reportViewer1.RefreshReport();
        }
        public void display_Lugdown(string clid)
        {
            SYS.Model.JZJS asm_data = jzjsdal.Get_JZJS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);

            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ZJNETMODE)
            {
                carinf.LSH = printer.ZJNETLSH;
            }
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
            }
            if (mainPanel.isdisplayCMANo)
            {
                rzbh =  mainPanel.stationinfmodel.STATIONDATE;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                   new ReportParameter("parameterJCYJ","GB3847-2005《车用压燃式发动机和压燃式发动机汽车排气烟度排放限值及测量方法》"+"\r\n"+"DB33/843-2013《在用压燃式发动机汽车加载减速排气烟度排放限值》"),
                    new ReportParameter("parameterBbbt",mainPanel.addname+"机动车排气污染物检测报告单"),
                    new ReportParameter("parameterJCFF","加载减速法"),
                    new ReportParameter("parameterLsh",carinf.LSH),
                    new ReportParameter("parameterLwbh", printer.ZJNETLSH==""?"—":printer.ZJNETLSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.jcgcsj),
                    new ReportParameter("parameterJcxh", carinf.JCZMC.Split('_')[1]+"号线"),

                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    //new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterJCCS", carinf.JCCS),

                   new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterWgjyy", carinf.wjy),
                    new ReportParameter("parameterClxh", (carinf.XH=="")?"--":carinf.XH),
                    new ReportParameter("parameterScqy",(carinf.SCQY=="")?"--":carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",(carinf.BSQXS == "") ? "--" : carinf.BSQXS),
                    new ReportParameter("parameterJqfs",(carinf.JQFS == "") ? "--" : carinf.JQFS),
                    new ReportParameter("parameterFdjxh",(carinf.FDJXH == "") ? "--" : carinf.FDJXH),
                    new ReportParameter("parameterFdjcs",(carinfo.FDJSCQY == "") ? "--" : carinfo.FDJSCQY),
                    new ReportParameter("parameterGyfs",(carinf.GYFS == "") ? "--" : carinf.GYFS),
                    new ReportParameter("parameterRlzl",(carinf.RLZL == "") ? "--" : carinf.RLZL),
                    new ReportParameter("parameterRylx",(carinf.RYPH == "") ? "--" : carinf.RYPH),
                    new ReportParameter("parameterDws",(carinf.DWS == "") ? "--" : carinf.DWS),
                    new ReportParameter("parameterQGS",(carinf.QGS == "") ? "--" : carinf.QGS),
                    new ReportParameter("parameterFDJH",(carinf.FDJHM == "") ? "--" : carinf.FDJHM),
                    new ReportParameter("parameterXslc",(carinf.XSLC == "") ? "--" : carinf.XSLC),
                    new ReportParameter("parameterFdjpl",(carinf.FDJPL == "") ? "--" : carinf.FDJPL),
                    new ReportParameter("parameterQdltqy", (carinf.QDLTQY == "") ? "--" : carinf.QDLTQY),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",(carinf.CLLX == "") ? "--" : carinf.CLLX),
                    new ReportParameter("parameterHpzl",(carinf.CPYS == "") ? "--" : carinf.CPYS),
                    new ReportParameter("parameterEdgl",(carinf.EDGL == "") ? "--" : carinf.EDGL),
                    new ReportParameter("parameterEdzs",(carinf.EDZS == "") ? "--" : carinf.EDZS),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",""),
                    new ReportParameter("parameterCzdh",(carinf.LXDH == "") ? "--" : carinf.LXDH),
                    new ReportParameter("parameterQdfs",(carinf.QDXS == "") ? "--" : carinf.QDXS),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",(carinf.CLSBM == "") ? "--" : carinf.CLSBM),
                    new ReportParameter("parameterCz",(carinf.CZ == "") ? "--" : carinf.CZ),
                    new ReportParameter("parameterSyxz",carinf.SYXZ),
                    new ReportParameter("parameterHdzk",carinf.HDZK),

                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterSdssbmc","--"),
                    new ReportParameter("parameterSdssbxh","--"),
                    new ReportParameter("parameterSdssbrzbm","--"),
                    
                    new ReportParameter("parameterLljxh","--"),
                    new ReportParameter("parameterLljbh", "--"),
                    new ReportParameter("parameterLljzzc", "--"),
                    new ReportParameter("parameterCgjxh","--"),
                    new ReportParameter("parameterCgjbh","--"),
                    new ReportParameter("parameterCgjzzc","--"),
                    new ReportParameter("parameterFxyxh","--"),
                    new ReportParameter("parameterFxybh","--"),
                    new ReportParameter("parameterFxyzzc","--"),


                    new ReportParameter("parameterVmassbmc","--"),
                    new ReportParameter("parameterCOCLZ","--"),
                    new ReportParameter("parameterCOXZ","--"),
                    new ReportParameter("parameterCOPD","--"),
                    new ReportParameter("parameterHCCLZ","--"),
                    new ReportParameter("parameterHCXZ","--"),
                    new ReportParameter("parameterHCPD","--"),
                    new ReportParameter("parameterNOXCLZ","--"),
                    new ReportParameter("parameterNOXXZ","--"),
                    new ReportParameter("parameterNOXPD","--"),
                    new ReportParameter("parameterHcnoxclz","--"),
                    new ReportParameter("parameterHcnoxxz","--"),
                    new ReportParameter("parameterHcnoxpd","--"),

                     new ReportParameter("parameterLOWHC","--"),
                    new ReportParameter("parameterLOWHCXZ","--"),
                    new ReportParameter("parameterLOWCO","--"),
                    new ReportParameter("parameterLOWCOXZ","--"),
                    new ReportParameter("parameterLOWPD","--"),
                    new ReportParameter("parameterHIGHCO","--"),
                    new ReportParameter("parameterHIGHCOXZ","--"),
                    new ReportParameter("parameterHIGHHC","--"),
                    new ReportParameter("parameterHIGHHCXZ","--"),
                    new ReportParameter("parameterLAMDA","--"),
                    new ReportParameter("parameterLAMDAXZ","--"),
                    new ReportParameter("parameterLAMDAPD","--"),
                    new ReportParameter("parameterHIGHPD","--"),
                    new ReportParameter("parameterLOWCOPD","--"),
                    new ReportParameter("parameterLOWHCPD","--"),
                    new ReportParameter("parameterHIGHHCPD","--"),
                    new ReportParameter("parameterHIGHCOPD","--"),

                    new ReportParameter("parameterLugydjmc","不透光烟度计"),
                    new ReportParameter("parameterLugydjcj",asm_data.YDJZZC),
                    new ReportParameter("parameterLugcgjxh",(asm_data.CGJXH=="")?"--":asm_data.CGJXH),
                    new ReportParameter("parameterLugcgjrzbm",(asm_data.CGJBH=="")?"--":asm_data.CGJBH),
                    new ReportParameter("parameterLugydjxh",(asm_data.YDJXH=="")?"--":asm_data.YDJXH),
                    new ReportParameter("parameterLugydjrzbm",(asm_data.YDJBH=="")?"--":asm_data.YDJBH),
                    
                    new ReportParameter("parameterHK",asm_data.HK),
                    new ReportParameter("parameterNK",asm_data.NK),
                    new ReportParameter("parameterEK",asm_data.EK),
                    new ReportParameter("parameterJZJSKXZ",asm_data.YDXZ),
                    new ReportParameter("parameterHKPD",(asm_data.HKPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterNKPD",(asm_data.NKPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterEKPD",(asm_data.EKPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterMaxHP",asm_data.MAXLBGL),
                    new ReportParameter("parameterLBGL",asm_data.MAXLBGL),
                    new ReportParameter("parameterLBZS",asm_data.MAXLBZS),
                    new ReportParameter("parameterGLXZ","≥50.0"),
                    new ReportParameter("parameterGLPD",(asm_data.GLPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterYdpd",(asm_data.HKPD=="合格"&&asm_data.NKPD=="合格"&&asm_data.EKPD=="合格")?"〇":"×"),
                    new ReportParameter("parameterLUGGLB",(Math.Round(double.Parse(asm_data.MAXLBGL)*50.0/double.Parse(asm_data.GLXZ),1)).ToString("0.0")),

                    new ReportParameter("parameterBtgsbmc","--"),
                    new ReportParameter("parameterBtgsbcj","--"),
                    new ReportParameter("parameterBtgsbxh","--"),
                    new ReportParameter("parameterBtgsbrzbm","--"),
                    
                    new ReportParameter("parameterDSZS","--"),
                    new ReportParameter("parameterFIRSTDATA","--"),
                    new ReportParameter("parameterSECONDDATA","--"),
                    new ReportParameter("parameterTHIRDDATA","--"),
                    new ReportParameter("parameterBTGXZ","--"),
                    new ReportParameter("parameterPJZ","--"),
                    new ReportParameter("parameterBTGPD","--"),

                    new ReportParameter("parameterLzDszs","--"),
                new ReportParameter("parameterLzFirstData", "--"),
                new ReportParameter("parameterLzSecondData", "--"),
                new ReportParameter("parameterLzThirdData", "--"),
                new ReportParameter("parameterLzForthData", "--"),
                new ReportParameter("parameterLzAveData", "--"),
                new ReportParameter("parameterLzXz", "--"),
                new ReportParameter("parameterLzPd", "--"),
                new ReportParameter("parameterLzSbrzbm", "--"),
                new ReportParameter("parameterLzSbxh", "--"),
                
                new ReportParameter("parameterPdyj","依据GB3847-2005,DB33/843-2013标准，对"+asm_data.CLPH+"(柴油车)进行"),
                    new ReportParameter("parameterZHPD",asm_data.ZHPD),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterCMA",imageCMA),

                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterJCZMC",carinf.JCZMC.Split('_')[1])
                };
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.SetParameters(rptpara);
            }
            catch(Exception er)
            {
                throw new Exception(er.Message);
            }
            reportViewer1.RefreshReport();
        }
        public void display_Btg(string clid)
        {
            SYS.Model.Zyjs_Btg asm_data = zyjsdal.Get_Zyjs(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ZJNETMODE)
            {
                carinf.LSH = printer.ZJNETLSH;
            }
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
            }
            if (mainPanel.isdisplayCMANo)
            {
                rzbh =  mainPanel.stationinfmodel.STATIONDATE;
            }
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB3847-2005《车用压燃式发动机和压燃式发动机汽车排气烟度排放限值及测量方法》"),
                    new ReportParameter("parameterBbbt",mainPanel.addname+"机动车排气污染物检测报告单"),
                    new ReportParameter("parameterJCFF","自由加速不透光烟度法"),
                    new ReportParameter("parameterLsh",carinf.LSH),
                    new ReportParameter("parameterLwbh", printer.ZJNETLSH==""?"—":printer.ZJNETLSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.jcgcsj),                    
                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterJcxh", carinf.JCZMC.Split('_')[1]+"号线"),
                    new ReportParameter("parameterJCCS", carinf.JCCS),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterWgjyy", carinf.wjy),
                    new ReportParameter("parameterClxh", (carinf.XH=="")?"--":carinf.XH),
                    new ReportParameter("parameterScqy",(carinf.SCQY=="")?"--":carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",(carinf.BSQXS == "") ? "--" : carinf.BSQXS),
                    new ReportParameter("parameterJqfs",(carinf.JQFS == "") ? "--" : carinf.JQFS),
                    new ReportParameter("parameterFdjxh",(carinf.FDJXH == "") ? "--" : carinf.FDJXH),
                    new ReportParameter("parameterFdjcs",(carinfo.FDJSCQY == "") ? "--" : carinfo.FDJSCQY),
                    new ReportParameter("parameterGyfs",(carinf.GYFS == "") ? "--" : carinf.GYFS),
                    new ReportParameter("parameterRlzl",(carinf.RLZL == "") ? "--" : carinf.RLZL),
                    new ReportParameter("parameterRylx",(carinf.RYPH == "") ? "--" : carinf.RYPH),
                    new ReportParameter("parameterDws",(carinf.DWS == "") ? "--" : carinf.DWS),
                    new ReportParameter("parameterQGS",(carinf.QGS == "") ? "--" : carinf.QGS),
                    new ReportParameter("parameterFDJH",(carinf.FDJHM == "") ? "--" : carinf.FDJHM),
                    new ReportParameter("parameterXslc",(carinf.XSLC == "") ? "--" : carinf.XSLC),
                    new ReportParameter("parameterFdjpl",(carinf.FDJPL == "") ? "--" : carinf.FDJPL),
                    new ReportParameter("parameterQdltqy", (carinf.QDLTQY == "") ? "--" : carinf.QDLTQY),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",(carinf.CLLX == "") ? "--" : carinf.CLLX),
                    new ReportParameter("parameterHpzl",(carinf.CPYS == "") ? "--" : carinf.CPYS),
                    new ReportParameter("parameterEdgl",(carinf.EDGL == "") ? "--" : carinf.EDGL),
                    new ReportParameter("parameterEdzs",(carinf.EDZS == "") ? "--" : carinf.EDZS),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",""),
                    new ReportParameter("parameterCzdh",(carinf.LXDH == "") ? "--" : carinf.LXDH),
                    new ReportParameter("parameterQdfs",(carinf.QDXS == "") ? "--" : carinf.QDXS),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",(carinf.CLSBM == "") ? "--" : carinf.CLSBM),
                    new ReportParameter("parameterCz",(carinf.CZ == "") ? "--" : carinf.CZ),
                    new ReportParameter("parameterSyxz",carinf.SYXZ),
                    new ReportParameter("parameterHdzk",carinf.HDZK),

                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),
                    
                    new ReportParameter("parameterLljxh","--"),
                    new ReportParameter("parameterLljbh", "--"),
                    new ReportParameter("parameterLljzzc", "--"),
                    new ReportParameter("parameterCgjxh","--"),
                    new ReportParameter("parameterCgjbh","--"),
                    new ReportParameter("parameterCgjzzc","--"),
                    new ReportParameter("parameterFxyxh","--"),
                    new ReportParameter("parameterFxybh","--"),
                    new ReportParameter("parameterFxyzzc","--"),

                    new ReportParameter("parameterVmassbmc","--"),
                    new ReportParameter("parameterCOCLZ","--"),
                    new ReportParameter("parameterCOXZ","--"),
                    new ReportParameter("parameterCOPD","--"),
                    new ReportParameter("parameterHCCLZ","--"),
                    new ReportParameter("parameterHCXZ","--"),
                    new ReportParameter("parameterHCPD","--"),
                    new ReportParameter("parameterNOXCLZ","--"),
                    new ReportParameter("parameterNOXXZ","--"),
                    new ReportParameter("parameterNOXPD","--"),
                    new ReportParameter("parameterHcnoxclz","--"),
                    new ReportParameter("parameterHcnoxxz","--"),
                    new ReportParameter("parameterHcnoxpd","--"),

                    new ReportParameter("parameterSdssbmc","--"),
                     new ReportParameter("parameterSdssbxh","--"),
                    new ReportParameter("parameterSdssbrzbm","--"),

                     new ReportParameter("parameterLOWHC","--"),
                    new ReportParameter("parameterLOWHCXZ","--"),
                    new ReportParameter("parameterLOWCO","--"),
                    new ReportParameter("parameterLOWCOXZ","--"),
                    new ReportParameter("parameterLOWPD","--"),
                    new ReportParameter("parameterHIGHCO","--"),
                    new ReportParameter("parameterHIGHCOXZ","--"),
                    new ReportParameter("parameterHIGHHC","--"),
                    new ReportParameter("parameterHIGHHCXZ","--"),
                    new ReportParameter("parameterLAMDA","--"),
                    new ReportParameter("parameterLAMDAXZ","--"),
                    new ReportParameter("parameterLAMDAPD","--"),
                    new ReportParameter("parameterHIGHPD","--"),
                    new ReportParameter("parameterLOWCOPD","--"),
                    new ReportParameter("parameterLOWHCPD","--"),
                    new ReportParameter("parameterHIGHHCPD","--"),
                    new ReportParameter("parameterHIGHCOPD","--"),

                    new ReportParameter("parameterLugydjmc","--"),
                    new ReportParameter("parameterLugydjcj","--"),
                    new ReportParameter("parameterLugcgjxh","--"),
                    new ReportParameter("parameterLugcgjrzbm","--"),
                    new ReportParameter("parameterLugydjxh","--"),
                    new ReportParameter("parameterLugydjrzbm","--"),
                    
                    new ReportParameter("parameterHK","--"),
                    new ReportParameter("parameterNK","--"),
                    new ReportParameter("parameterEK","--"),
                    new ReportParameter("parameterJZJSKXZ","--"),
                    new ReportParameter("parameterHKPD","--"),
                    new ReportParameter("parameterNKPD","--"),
                    new ReportParameter("parameterEKPD","--"),
                    new ReportParameter("parameterMaxHP","--"),
                    new ReportParameter("parameterLBGL","--"),
                    new ReportParameter("parameterLBZS","--"),
                    new ReportParameter("parameterGLXZ","--"),
                    new ReportParameter("parameterGLPD","--"),
                    new ReportParameter("parameterYdpd","--"),
                    new ReportParameter("parameterLUGGLB","--"),

                    new ReportParameter("parameterBtgsbmc","不透光烟度计"),
                    new ReportParameter("parameterBtgsbcj",asm_data.YDJZZC),
                    new ReportParameter("parameterBtgsbxh",(asm_data.YDJXH=="")?"--":asm_data.YDJXH),
                    new ReportParameter("parameterBtgsbrzbm",(asm_data.YDJBH=="")?"--":asm_data.YDJBH),
                    
                    new ReportParameter("parameterDSZS",asm_data.DSZS),
                    new ReportParameter("parameterFIRSTDATA",asm_data.FIRSTDATA),
                    new ReportParameter("parameterSECONDDATA",asm_data.SECONDDATA),
                    new ReportParameter("parameterTHIRDDATA",asm_data.THIRDDATA),
                    new ReportParameter("parameterBTGXZ",asm_data.YDXZ),
                    new ReportParameter("parameterPJZ",asm_data.AVERAGEDATA),
                    new ReportParameter("parameterBTGPD",(asm_data.ZHPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterLzDszs","--"),
                new ReportParameter("parameterLzFirstData", "--"),
                new ReportParameter("parameterLzSecondData", "--"),
                new ReportParameter("parameterLzThirdData", "--"),
                new ReportParameter("parameterLzForthData", "--"),
                new ReportParameter("parameterLzAveData", "--"),
                new ReportParameter("parameterLzXz", "--"),
                new ReportParameter("parameterLzPd", "--"),
                new ReportParameter("parameterLzSbrzbm", "--"),
                new ReportParameter("parameterLzSbxh", "--"),
                
                new ReportParameter("parameterPdyj","依据GB3847-2005标准，对"+asm_data.CLPH+"(柴油车)进行"),
                    new ReportParameter("parameterZHPD",asm_data.ZHPD),

                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterJCZMC",carinf.JCZMC.Split('_')[1])
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
        public void display_Lz(string clid)
        {
            SYS.Model.Zyjs_Btg asm_data = zyjsdal.Get_Zyjs(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            string imageCMA = "file:///" + Application.StartupPath + "\\png\\WHITE.png", rzbh = "";
            if (mainPanel.isdisplayCMA)
            {
                imageCMA = "file:///" + Application.StartupPath + "\\png\\CMA.jpg";
            }
            if (mainPanel.isdisplayCMANo)
            {
                rzbh =  mainPanel.stationinfmodel.STATIONDATE;
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
                    new ReportParameter("parameterBbbt",mainPanel.addname+"机动车排气污染物检测报告单"),
                    new ReportParameter("parameterJCFF","自由加速不透光烟度法"),
                    new ReportParameter("parameterLsh", carinf.JCSJ.ToString("yyyyMMdd")+ carinf.LSH.Remove(0,carinf.LSH.Count()-4)),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterScrq", carinf.SCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterJcxh", carinf.JCZMC.Split('_')[1]+"号线"),
                    new ReportParameter("parameterJCCS", carinf.JCCS),

                    new ReportParameter("parameterDly", carinf.DLY),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterWgjyy", carinf.wjy),

                    new ReportParameter("parameterClxh", (carinf.XH=="")?"--":carinf.XH),
                    new ReportParameter("parameterScqy",(carinf.SCQY=="")?"--":carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
                    new ReportParameter("parameterBsqxs",(carinf.BSQXS == "") ? "--" : carinf.BSQXS),
                    new ReportParameter("parameterJqfs",(carinf.JQFS == "") ? "--" : carinf.JQFS),
                    new ReportParameter("parameterFdjxh",(carinf.FDJXH == "") ? "--" : carinf.FDJXH),
                    new ReportParameter("parameterFdjcs",(carinfo.FDJSCQY == "") ? "--" : carinfo.FDJSCQY),
                    new ReportParameter("parameterGyfs",(carinf.GYFS == "") ? "--" : carinf.GYFS),
                    new ReportParameter("parameterRlzl",(carinf.RLZL == "") ? "--" : carinf.RLZL),
                    new ReportParameter("parameterRylx",(carinf.RYPH == "") ? "--" : carinf.RYPH),
                    new ReportParameter("parameterDws",(carinf.DWS == "") ? "--" : carinf.DWS),
                    new ReportParameter("parameterQGS",(carinf.QGS == "") ? "--" : carinf.QGS),
                    new ReportParameter("parameterFDJH",(carinf.FDJHM == "") ? "--" : carinf.FDJHM),
                    new ReportParameter("parameterXslc",(carinf.XSLC == "") ? "--" : carinf.XSLC),
                    new ReportParameter("parameterFdjpl",(carinf.FDJPL == "") ? "--" : carinf.FDJPL),
                    new ReportParameter("parameterQdltqy", (carinf.QDLTQY == "") ? "--" : carinf.QDLTQY),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",(carinf.CLLX == "") ? "--" : carinf.CLLX),
                    new ReportParameter("parameterHpzl",(carinf.CPYS == "") ? "--" : carinf.CPYS),
                    new ReportParameter("parameterEdgl",(carinf.EDGL == "") ? "--" : carinf.EDGL),
                    new ReportParameter("parameterEdzs",(carinf.EDZS == "") ? "--" : carinf.EDZS),
                    new ReportParameter("parameterHbbz","有"),
                    new ReportParameter("parameterCzdz",""),
                    new ReportParameter("parameterCzdh",(carinf.LXDH == "") ? "--" : carinf.LXDH),
                    new ReportParameter("parameterQdfs",(carinf.QDXS == "") ? "--" : carinf.QDXS),
                    new ReportParameter("parameterChzz",(carinf.JHZZ == "") ? "--" : carinf.JHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",(carinf.CLSBM == "") ? "--" : carinf.CLSBM),
                    new ReportParameter("parameterCz",(carinf.CZ == "") ? "--" : carinf.CZ),

                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterLljxh","--"),
                    new ReportParameter("parameterLljbh", "--"),
                    new ReportParameter("parameterLljzzc", "--"),
                    new ReportParameter("parameterCgjxh","--"),
                    new ReportParameter("parameterCgjbh","--"),
                    new ReportParameter("parameterCgjzzc","--"),
                    new ReportParameter("parameterFxyxh","--"),
                    new ReportParameter("parameterFxybh","--"),
                    new ReportParameter("parameterFxyzzc","--"),

                    new ReportParameter("parameterCOCLZ","--"),
                    new ReportParameter("parameterCOXZ","--"),
                    new ReportParameter("parameterCOPD","--"),
                    new ReportParameter("parameterHCCLZ","--"),
                    new ReportParameter("parameterHCXZ","--"),
                    new ReportParameter("parameterHCPD","--"),
                    new ReportParameter("parameterNOXCLZ","--"),
                    new ReportParameter("parameterNOXXZ","--"),
                    new ReportParameter("parameterNOXPD","--"),
                    new ReportParameter("parameterHcnoxclz","--"),
                    new ReportParameter("parameterHcnoxxz","--"),
                    new ReportParameter("parameterHcnoxpd","--"),

                     new ReportParameter("parameterSdssbxh","--"),
                    new ReportParameter("parameterSdssbrzbm","--"),

                     new ReportParameter("parameterLOWHC","--"),
                    new ReportParameter("parameterLOWHCXZ","--"),
                    new ReportParameter("parameterLOWCO","--"),
                    new ReportParameter("parameterLOWCOXZ","--"),
                    new ReportParameter("parameterLOWPD","--"),
                    new ReportParameter("parameterHIGHCO","--"),
                    new ReportParameter("parameterHIGHCOXZ","--"),
                    new ReportParameter("parameterHIGHHC","--"),
                    new ReportParameter("parameterHIGHHCXZ","--"),
                    new ReportParameter("parameterLAMDA","--"),
                    new ReportParameter("parameterLAMDAXZ","--"),
                    new ReportParameter("parameterLAMDAPD","--"),
                    new ReportParameter("parameterHIGHPD","--"),

                    new ReportParameter("parameterLugcgjxh","--"),
                    new ReportParameter("parameterLugcgjrzbm","--"),
                    new ReportParameter("parameterLugydjxh","--"),
                    new ReportParameter("parameterLugydjrzbm","--"),

                    new ReportParameter("parameterHK","--"),
                    new ReportParameter("parameterNK","--"),
                    new ReportParameter("parameterEK","--"),
                    new ReportParameter("parameterJZJSKXZ","--"),
                    new ReportParameter("parameterHKPD","--"),
                    new ReportParameter("parameterNKPD","--"),
                    new ReportParameter("parameterEKPD","--"),
                    new ReportParameter("parameterMaxHP","--"),
                    new ReportParameter("parameterLBGL","--"),
                    new ReportParameter("parameterLBZS","--"),
                    new ReportParameter("parameterGLXZ","--"),
                    new ReportParameter("parameterGLPD","--"),
                    new ReportParameter("parameterLUGGLB","--"),

                    new ReportParameter("parameterBtgsbxh","--"),
                    new ReportParameter("parameterBtgsbrzbm","--"),

                    new ReportParameter("parameterDSZS","--"),
                    new ReportParameter("parameterFIRSTDATA","--"),
                    new ReportParameter("parameterSECONDDATA","--"),
                    new ReportParameter("parameterTHIRDDATA","--"),
                    new ReportParameter("parameterBTGXZ","--"),
                    new ReportParameter("parameterPJZ","--"),
                    new ReportParameter("parameterBTGPD","--"),

                    new ReportParameter("parameterLzSbxh",(asm_data.YDJXH=="")?"--":asm_data.YDJXH),
                    new ReportParameter("parameterLzSbrzbm",(asm_data.YDJBH=="")?"--":asm_data.YDJBH),

                    new ReportParameter("parameterLzDszs",asm_data.DSZS),
                    new ReportParameter("parameterLzFirstData",  asm_data.FOURTHDATA),
                    new ReportParameter("parameterLzSecondData",asm_data.FIRSTDATA),
                    new ReportParameter("parameterLzThirdData",asm_data.SECONDDATA),
                    new ReportParameter("parameterLzForthData",asm_data.THIRDDATA),
                    new ReportParameter("parameterLzXz","≤"+asm_data.YDXZ),
                    new ReportParameter("parameterLzAveData",asm_data.AVERAGEDATA),
                    new ReportParameter("parameterLzPd",(asm_data.ZHPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"〇":"×"),

                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterBGRQ",DateTime.Now.ToString("D")),
                    new ReportParameter("parameterJczdz",mainPanel.stationinfmodel.STATIONADD),
                    new ReportParameter("parameterJczdh",mainPanel.stationinfmodel.STATIONPHONE),
                    new ReportParameter("parameterJCZMC",carinf.JCZMC.Split('_')[1])
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
