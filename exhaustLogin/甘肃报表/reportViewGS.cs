using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using ini;

namespace exhaustLogin
{
    public partial class reportViewGS : Form
    {
        SYS_DAL.ASMdal asmdal = new SYS_DAL.ASMdal();
        SYS_DAL.JZJSdal jzjsdal = new SYS_DAL.JZJSdal();
        SYS_DAL.Zyjsdal zyjsdal = new SYS_DAL.Zyjsdal();
        SYS_DAL.SDSdal sdsdal = new SYS_DAL.SDSdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewGS()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
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
            SYS.Model.ASM asm_data = asmdal.Get_ASM(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            init_wsd();
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            string jhzz = "—", pqhclzz = "—";
            if (carinf.RLZL == "柴油") pqhclzz = carinf.JHZZ;
            else jhzz = carinf.JHZZ;
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB18285-2005、DB62/T 2575-2015"),
                    new ReportParameter("parameterJCFF","稳态工况法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterBJY", carinf.DLY),

                    new ReportParameter("parameterQdltqy",carinfo.QDLTQY==""?"—":carinfo.QDLTQY),
                    new ReportParameter("parameterRygg",carinfo.RYPH==""?"—":carinfo.RYPH),

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
                    new ReportParameter("parameterQGS",carinf.QGS),
                    new ReportParameter("parameterFDJH",carinf.FDJHM),
                    new ReportParameter("parameterXslc",carinf.XSLC),
                    new ReportParameter("parameterFdjpl",carinf.FDJPL),
                    new ReportParameter("parameterZcrq", carinf.ZCRQ.ToString("yyyy-MM-dd")),
                    new ReportParameter("parameterCllx",carinf.CLLX),
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",jhzz),
                    new ReportParameter("parameterPqhclzz",pqhclzz),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    new ReportParameter("parameterSbmc",asm_data.SBZZC),
                    new ReportParameter("parameterCgjxh",asm_data.CGJXH),
                    new ReportParameter("parameterCgjbh",asm_data.CGJBH),
                    new ReportParameter("parameterCgjcj",asm_data.CGJZZC),
                    new ReportParameter("parameterFxyxh",asm_data.FXYXH),
                    new ReportParameter("parameterFxybh",asm_data.FXYBH),
                    new ReportParameter("parameterFxycj",asm_data.FXYZZC),
                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterHC25CLZ",asm_data.HC25CLZ),
                    new ReportParameter("parameterCO25CLZ",asm_data.CO25CLZ),
                    new ReportParameter("parameterNOX25CLZ",asm_data.NOX25CLZ),
                    new ReportParameter("parameterHC40CLZ",asm_data.HC40CLZ==""?"—":asm_data.HC40CLZ),
                    new ReportParameter("parameterCO40CLZ",asm_data.CO40CLZ==""?"—":asm_data.CO40CLZ),
                    new ReportParameter("parameterNOX40CLZ",asm_data.NOX40CLZ==""?"—":asm_data.NOX40CLZ),
                    new ReportParameter("parameterHC25PD",asm_data.HC25PD=="合格"?"○" : "×"),
                    new ReportParameter("parameterCO25PD",asm_data.CO25PD=="合格"?"○" : "×"),
                    new ReportParameter("parameterNOX25PD",asm_data.NOX25PD=="合格"?"○" : "×"),
                    new ReportParameter("parameterHC40PD",asm_data.HC40PD==""?"—":(asm_data.HC40PD=="合格"?"○" : "×")),
                    new ReportParameter("parameterCO40PD",asm_data.CO40PD==""?"—":(asm_data.CO40PD=="合格"?"○" : "×")),
                    new ReportParameter("parameterNOX40PD",asm_data.NOX40PD==""?"—":(asm_data.NOX40PD=="合格"?"○" : "×")),
                    new ReportParameter("parameterHC25XZ","≤"+asm_data.HC25XZ),
                    new ReportParameter("parameterCO25XZ","≤"+asm_data.CO25XZ),
                    new ReportParameter("parameterNOX25XZ","≤"+asm_data.NOX25XZ),
                    new ReportParameter("parameterHC40XZ","≤"+asm_data.HC40XZ),
                    new ReportParameter("parameterCO40XZ","≤"+asm_data.CO40XZ),
                    new ReportParameter("parameterNOX40XZ","≤"+asm_data.NOX40XZ),

                    new ReportParameter("parameterHK","—"),
                    new ReportParameter("parameterNK","—"),
                    new ReportParameter("parameterEK","—"),
                    new ReportParameter("parameterJZJSKXZ","—"),
                    new ReportParameter("parameterHKPD","—"),
                    new ReportParameter("parameterNKPD","—"),
                    new ReportParameter("parameterEKPD","—"),
                    new ReportParameter("parameterLBGL","—"),
                    new ReportParameter("parameterGLXZ","—"),
                    new ReportParameter("parameterGLPD","—"),

                    new ReportParameter("parameterLOWHC","—"),
                    new ReportParameter("parameterLOWHCXZ","—"),
                    new ReportParameter("parameterLOWCO","—"),
                    new ReportParameter("parameterLOWCOXZ","—"),
                    new ReportParameter("parameterLOWPD","—"),
                    new ReportParameter("parameterHIGHCO","—"),
                    new ReportParameter("parameterHIGHCOXZ","—"),
                    new ReportParameter("parameterHIGHHC","—"),
                    new ReportParameter("parameterHIGHHCXZ","—"),
                    new ReportParameter("parameterLAMDA","—"),
                    new ReportParameter("parameterLAMDAXZ","—"),
                    new ReportParameter("parameterLAMDAPD","—"),
                    new ReportParameter("parameterHIGHPD","—"),

                    new ReportParameter("parameterDSZS","—"),
                    new ReportParameter("parameterFIRSTDATA","—"),
                    new ReportParameter("parameterSECONDDATA","—"),
                    new ReportParameter("parameterTHIRDDATA","—"),
                    new ReportParameter("parameterBTGXZ","—"),
                    new ReportParameter("parameterPJZ","—"),
                    new ReportParameter("parameterBTGPD","—"),

                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"○" : "×"),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterWGJCY",mainPanel.wgjcy),
                    new ReportParameter("parameterBJY",mainPanel.bjy),
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
        public void display_SDSCH(string clid)
        {
            SYS.Model.SDS asm_data = sdsdal.Get_SDS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            init_wsd();
            string lambdapd = "/";
            if (carinf.JHZZ != "无" && carinf.JHZZ != "否")
                lambdapd = asm_data.LAMDAHIGHPD;
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            string jhzz = "—", pqhclzz = "—";
            if (carinf.RLZL == "柴油") pqhclzz = carinf.JHZZ;
            else jhzz = carinf.JHZZ;
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB18285-2005"),
                    new ReportParameter("parameterJCFF","双怠速法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterBJY", carinf.DLY),
                    new ReportParameter("parameterClxh", carinf.XH),


                    new ReportParameter("parameterQdltqy",carinfo.QDLTQY==""?"—":carinfo.QDLTQY),
                    new ReportParameter("parameterRygg",carinfo.RYPH==""?"—":carinfo.RYPH),

                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
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
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",jhzz),
                    new ReportParameter("parameterPqhclzz",pqhclzz),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    new ReportParameter("parameterSbmc",asm_data.SBZZC),
                    new ReportParameter("parameterCgjxh","—"),
                    new ReportParameter("parameterCgjbh","—"),
                    new ReportParameter("parameterCgjcj","—"),
                    new ReportParameter("parameterFxyxh",asm_data.FXYXH),
                    new ReportParameter("parameterFxybh",asm_data.FXYBH),
                    new ReportParameter("parameterFxycj",asm_data.FXYZZC),
                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterHC25CLZ","—"),
                    new ReportParameter("parameterCO25CLZ","—"),
                    new ReportParameter("parameterNOX25CLZ","—"),
                    new ReportParameter("parameterHC40CLZ","—"),
                    new ReportParameter("parameterCO40CLZ","—"),
                    new ReportParameter("parameterNOX40CLZ","—"),
                    new ReportParameter("parameterHC25PD","—"),
                    new ReportParameter("parameterCO25PD","—"),
                    new ReportParameter("parameterNOX25PD","—"),
                    new ReportParameter("parameterHC40PD","—"),
                    new ReportParameter("parameterCO40PD","—"),
                    new ReportParameter("parameterNOX40PD","—"),
                    new ReportParameter("parameterHC25XZ","—"),
                    new ReportParameter("parameterCO25XZ","—"),
                    new ReportParameter("parameterNOX25XZ","—"),
                    new ReportParameter("parameterHC40XZ","—"),
                    new ReportParameter("parameterCO40XZ","—"),
                    new ReportParameter("parameterNOX40XZ","—"),

                    new ReportParameter("parameterHK","—"),
                    new ReportParameter("parameterNK","—"),
                    new ReportParameter("parameterEK","—"),
                    new ReportParameter("parameterJZJSKXZ","—"),
                    new ReportParameter("parameterHKPD","—"),
                    new ReportParameter("parameterNKPD","—"),
                    new ReportParameter("parameterEKPD","—"),
                    new ReportParameter("parameterLBGL","—"),
                    new ReportParameter("parameterGLXZ","—"),
                    new ReportParameter("parameterGLPD","—"),

                    new ReportParameter("parameterLOWHC",asm_data.HCLOWCLZ),
                    new ReportParameter("parameterLOWHCXZ","≤"+asm_data.HCLOWXZ),
                    new ReportParameter("parameterLOWCO",asm_data.COLOWCLZ),
                    new ReportParameter("parameterLOWCOXZ","≤"+asm_data.COLOWXZ),
                    new ReportParameter("parameterLOWPD",asm_data.LOWPD),
                    new ReportParameter("parameterHIGHCO",asm_data.COHIGHCLZ),
                    new ReportParameter("parameterHIGHCOXZ","≤"+asm_data.COHIGHXZ),
                    new ReportParameter("parameterHIGHHC",asm_data.HCHIGHCLZ),
                    new ReportParameter("parameterHIGHHCXZ","≤"+asm_data.HCHIGHXZ),
                    new ReportParameter("parameterLAMDA",asm_data.LAMDAHIGHCLZ),
                    new ReportParameter("parameterLAMDAXZ",asm_data.LAMDAHIGHXZ),
                    new ReportParameter("parameterLAMDAPD",lambdapd),
                    new ReportParameter("parameterHIGHPD",asm_data.HIGHPD),

                    new ReportParameter("parameterDSZS","—"),
                    new ReportParameter("parameterFIRSTDATA","—"),
                    new ReportParameter("parameterSECONDDATA","—"),
                    new ReportParameter("parameterTHIRDDATA","—"),
                    new ReportParameter("parameterBTGXZ","—"),
                    new ReportParameter("parameterPJZ","—"),
                    new ReportParameter("parameterBTGPD","—"),


                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"通过":"不通过"),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterWGJCY",mainPanel.wgjcy),
                    new ReportParameter("parameterBJY",mainPanel.bjy),
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
        public void display_SDS(string clid)
        {
            SYS.Model.SDS asm_data = sdsdal.Get_SDS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            init_wsd();

            string lambdapd = "—";
            if (carinf.JHZZ != "无"&&carinf.JHZZ!="否")
                lambdapd = asm_data.LAMDAHIGHPD == "" ? "—" : (asm_data.LAMDAHIGHPD == "合格" ? "○" : "×");
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            string jhzz = "—", pqhclzz = "—";
            if (carinf.RLZL == "柴油") pqhclzz = carinf.JHZZ;
            else jhzz = carinf.JHZZ;
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB18285-2005"),
                    new ReportParameter("parameterJCFF","双怠速法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterBJY", carinf.DLY),
                    new ReportParameter("parameterClxh", carinf.XH),


                    new ReportParameter("parameterQdltqy",carinfo.QDLTQY==""?"—":carinfo.QDLTQY),
                    new ReportParameter("parameterRygg",carinfo.RYPH==""?"—":carinfo.RYPH),


                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
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
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",jhzz),
                    new ReportParameter("parameterPqhclzz",pqhclzz),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    new ReportParameter("parameterSbmc",asm_data.SBZZC),
                    new ReportParameter("parameterCgjxh","—"),
                    new ReportParameter("parameterCgjbh","—"),
                    new ReportParameter("parameterCgjcj","—"),
                    new ReportParameter("parameterFxyxh",asm_data.FXYXH),
                    new ReportParameter("parameterFxybh",asm_data.FXYBH),
                    new ReportParameter("parameterFxycj",asm_data.FXYZZC),
                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterHC25CLZ","—"),
                    new ReportParameter("parameterCO25CLZ","—"),
                    new ReportParameter("parameterNOX25CLZ","—"),
                    new ReportParameter("parameterHC40CLZ","—"),
                    new ReportParameter("parameterCO40CLZ","—"),
                    new ReportParameter("parameterNOX40CLZ","—"),
                    new ReportParameter("parameterHC25PD","—"),
                    new ReportParameter("parameterCO25PD","—"),
                    new ReportParameter("parameterNOX25PD","—"),
                    new ReportParameter("parameterHC40PD","—"),
                    new ReportParameter("parameterCO40PD","—"),
                    new ReportParameter("parameterNOX40PD","—"),
                    new ReportParameter("parameterHC25XZ","—"),
                    new ReportParameter("parameterCO25XZ","—"),
                    new ReportParameter("parameterNOX25XZ","—"),
                    new ReportParameter("parameterHC40XZ","—"),
                    new ReportParameter("parameterCO40XZ","—"),
                    new ReportParameter("parameterNOX40XZ","—"),

                    new ReportParameter("parameterHK","—"),
                    new ReportParameter("parameterNK","—"),
                    new ReportParameter("parameterEK","—"),
                    new ReportParameter("parameterJZJSKXZ","—"),
                    new ReportParameter("parameterHKPD","—"),
                    new ReportParameter("parameterNKPD","—"),
                    new ReportParameter("parameterEKPD","—"),
                    new ReportParameter("parameterLBGL","—"),
                    new ReportParameter("parameterGLXZ","—"),
                    new ReportParameter("parameterGLPD","—"),

                    new ReportParameter("parameterLOWHC",asm_data.HCLOWCLZ),
                    new ReportParameter("parameterLOWHCXZ","≤"+asm_data.HCLOWXZ),
                    new ReportParameter("parameterLOWCO",asm_data.COLOWCLZ),
                    new ReportParameter("parameterLOWCOXZ","≤"+asm_data.COLOWXZ),
                    new ReportParameter("parameterLOWPD",asm_data.LOWPD=="合格"?"○" : "×"),
                    new ReportParameter("parameterHIGHCO",asm_data.COHIGHCLZ),
                    new ReportParameter("parameterHIGHCOXZ","≤"+asm_data.COHIGHXZ),
                    new ReportParameter("parameterHIGHHC",asm_data.HCHIGHCLZ),
                    new ReportParameter("parameterHIGHHCXZ","≤"+asm_data.HCHIGHXZ),
                    new ReportParameter("parameterLAMDA",asm_data.LAMDAHIGHCLZ),
                    new ReportParameter("parameterLAMDAXZ",asm_data.LAMDAHIGHXZ),
                    new ReportParameter("parameterLAMDAPD",lambdapd),
                    new ReportParameter("parameterHIGHPD",asm_data.HIGHPD=="合格"?"○" : "×"),

                    new ReportParameter("parameterDSZS","—"),
                    new ReportParameter("parameterFIRSTDATA","—"),
                    new ReportParameter("parameterSECONDDATA","—"),
                    new ReportParameter("parameterTHIRDDATA","—"),
                    new ReportParameter("parameterBTGXZ","—"),
                    new ReportParameter("parameterPJZ","—"),
                    new ReportParameter("parameterBTGPD","—"),


                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"○" : "×"),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterWGJCY",mainPanel.wgjcy),
                    new ReportParameter("parameterBJY",mainPanel.bjy),
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
        public void display_Jzjs(string clid)
        {
            SYS.Model.JZJS asm_data = jzjsdal.Get_JZJS(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            init_wsd();
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            string jhzz = "—", pqhclzz = "—";
            if (carinf.RLZL == "柴油") pqhclzz = carinf.JHZZ;
            else jhzz = carinf.JHZZ;
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB3847-2005、DB62/T 2576-2015"),
                    new ReportParameter("parameterJCFF","加载减速法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterBJY", carinf.DLY),
                    new ReportParameter("parameterClxh", carinf.XH),


                    new ReportParameter("parameterQdltqy",carinfo.QDLTQY==""?"—":carinfo.QDLTQY),
                    new ReportParameter("parameterRygg",carinfo.RYPH==""?"—":carinfo.RYPH),

                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
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
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",jhzz),
                    new ReportParameter("parameterPqhclzz",pqhclzz),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    new ReportParameter("parameterSbmc",asm_data.SBZZC),
                    new ReportParameter("parameterCgjxh",asm_data.CGJXH),
                    new ReportParameter("parameterCgjbh",asm_data.CGJBH),
                    new ReportParameter("parameterCgjcj",asm_data.CGJZZC),
                    new ReportParameter("parameterFxyxh",asm_data.YDJXH),
                    new ReportParameter("parameterFxybh",asm_data.YDJBH),
                    new ReportParameter("parameterFxycj",asm_data.YDJZZC),
                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterHC25CLZ","—"),
                    new ReportParameter("parameterCO25CLZ","—"),
                    new ReportParameter("parameterNOX25CLZ","—"),
                    new ReportParameter("parameterHC40CLZ","—"),
                    new ReportParameter("parameterCO40CLZ","—"),
                    new ReportParameter("parameterNOX40CLZ","—"),
                    new ReportParameter("parameterHC25PD","—"),
                    new ReportParameter("parameterCO25PD","—"),
                    new ReportParameter("parameterNOX25PD","—"),
                    new ReportParameter("parameterHC40PD","—"),
                    new ReportParameter("parameterCO40PD","—"),
                    new ReportParameter("parameterNOX40PD","—"),
                    new ReportParameter("parameterHC25XZ","—"),
                    new ReportParameter("parameterCO25XZ","—"),
                    new ReportParameter("parameterNOX25XZ","—"),
                    new ReportParameter("parameterHC40XZ","—"),
                    new ReportParameter("parameterCO40XZ","—"),
                    new ReportParameter("parameterNOX40XZ","—"),

                    new ReportParameter("parameterHK",asm_data.HK==""?"—":asm_data.HK),
                    new ReportParameter("parameterNK",asm_data.NK==""?"—":asm_data.NK),
                    new ReportParameter("parameterEK",asm_data.EK==""?"—":asm_data.EK),
                    new ReportParameter("parameterJZJSKXZ","≤"+asm_data.YDXZ),
                    new ReportParameter("parameterHKPD",asm_data.HKPD==""?"—":(asm_data.HKPD=="合格"?"○" : "×")),
                    new ReportParameter("parameterNKPD",asm_data.NKPD==""?"—":(asm_data.HKPD=="合格"?"○" : "×")),
                    new ReportParameter("parameterEKPD",asm_data.EKPD==""?"—":(asm_data.HKPD=="合格"?"○" : "×")),
                    new ReportParameter("parameterLBGL",asm_data.MAXLBGL),
                    new ReportParameter("parameterGLXZ","≥"+asm_data.GLXZ),
                    new ReportParameter("parameterGLPD",asm_data.GLPD=="合格"?"○" : "×"),

                    new ReportParameter("parameterLOWHC","—"),
                    new ReportParameter("parameterLOWHCXZ","—"),
                    new ReportParameter("parameterLOWCO","—"),
                    new ReportParameter("parameterLOWCOXZ","—"),
                    new ReportParameter("parameterLOWPD","—"),
                    new ReportParameter("parameterHIGHCO","—"),
                    new ReportParameter("parameterHIGHCOXZ","—"),
                    new ReportParameter("parameterHIGHHC","—"),
                    new ReportParameter("parameterHIGHHCXZ","—"),
                    new ReportParameter("parameterLAMDA","—"),
                    new ReportParameter("parameterLAMDAXZ","—"),
                    new ReportParameter("parameterLAMDAPD","—"),
                    new ReportParameter("parameterHIGHPD","—"),

                    new ReportParameter("parameterDSZS","—"),
                    new ReportParameter("parameterFIRSTDATA","—"),
                    new ReportParameter("parameterSECONDDATA","—"),
                    new ReportParameter("parameterTHIRDDATA","—"),
                    new ReportParameter("parameterBTGXZ","—"),
                    new ReportParameter("parameterPJZ","—"),
                    new ReportParameter("parameterBTGPD","—"),

                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"○" : "×"),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterWGJCY",mainPanel.wgjcy),
                    new ReportParameter("parameterBJY",mainPanel.bjy),
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
        public void display_Zyjs(string clid)
        {
            SYS.Model.Zyjs_Btg asm_data =zyjsdal.Get_Zyjs(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
            SYS.Model.CARINF carinfo = logininfcontrol.getCarInfbyPlate(carinf.CLHP);
            init_wsd();
            if (printer.wsdthisTime.isUseWsd)
            {
                asm_data.WD = printer.wsdthisTime.wd;
                asm_data.SD = printer.wsdthisTime.sd;
                asm_data.DQY = printer.wsdthisTime.dqy;
            }
            string jhzz = "—", pqhclzz = "—";
            if (carinf.RLZL == "柴油") pqhclzz = carinf.JHZZ;
            else jhzz = carinf.JHZZ;
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            reportViewer1.LocalReport.Dispose();
            reportViewer1.Visible = true;
            string teststring = DateTime.Now.ToShortDateString();
            try
            {
                ReportParameter[] rptpara =
                {
                    new ReportParameter("parameterJCYJ","GB3847-2005"),
                    new ReportParameter("parameterJCFF","自由加速不透光度法"),
                    new ReportParameter("parameterLsh", carinf.LSH),
                    new ReportParameter("parameterStationName", mainPanel.stationinfmodel.STATIONNAME),
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("yyyy-MM-dd HH:mm:ss")),
                    new ReportParameter("parameterCzy", carinf.CZY),
                    new ReportParameter("parameterJsy", carinf.JSY),
                    new ReportParameter("parameterBJY", carinf.DLY),
                    new ReportParameter("parameterClxh", carinf.XH),
                    
                    new ReportParameter("parameterQdltqy",carinfo.QDLTQY==""?"—":carinfo.QDLTQY),
                    new ReportParameter("parameterRygg",carinfo.RYPH==""?"—":carinfo.RYPH),

                    new ReportParameter("parameterScqy",carinf.SCQY),
                    new ReportParameter("parameterJzzl", carinf.JZZL),
                    new ReportParameter("parameterZzl", carinf.ZZL),
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
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",jhzz),
                    new ReportParameter("parameterPqhclzz",pqhclzz),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),
                    new ReportParameter("parameterSbmc",asm_data.SBZZC),
                    new ReportParameter("parameterCgjxh","—"),
                    new ReportParameter("parameterCgjbh","—"),
                    new ReportParameter("parameterCgjcj","—"),
                    new ReportParameter("parameterFxyxh",asm_data.YDJXH),
                    new ReportParameter("parameterFxybh",asm_data.YDJBH),
                    new ReportParameter("parameterFxycj",asm_data.YDJZZC),
                    new ReportParameter("parameterWd",asm_data.WD),
                    new ReportParameter("parameterDqy",asm_data.DQY),
                    new ReportParameter("parameterSd",asm_data.SD),

                    new ReportParameter("parameterHC25CLZ","—"),
                    new ReportParameter("parameterCO25CLZ","—"),
                    new ReportParameter("parameterNOX25CLZ","—"),
                    new ReportParameter("parameterHC40CLZ","—"),
                    new ReportParameter("parameterCO40CLZ","—"),
                    new ReportParameter("parameterNOX40CLZ","—"),
                    new ReportParameter("parameterHC25PD","—"),
                    new ReportParameter("parameterCO25PD","—"),
                    new ReportParameter("parameterNOX25PD","—"),
                    new ReportParameter("parameterHC40PD","—"),
                    new ReportParameter("parameterCO40PD","—"),
                    new ReportParameter("parameterNOX40PD","—"),
                    new ReportParameter("parameterHC25XZ","—"),
                    new ReportParameter("parameterCO25XZ","—"),
                    new ReportParameter("parameterNOX25XZ","—"),
                    new ReportParameter("parameterHC40XZ","—"),
                    new ReportParameter("parameterCO40XZ","—"),
                    new ReportParameter("parameterNOX40XZ","—"),

                    new ReportParameter("parameterHK","—"),
                    new ReportParameter("parameterNK","—"),
                    new ReportParameter("parameterEK","—"),
                    new ReportParameter("parameterJZJSKXZ","—"),
                    new ReportParameter("parameterHKPD","—"),
                    new ReportParameter("parameterNKPD","—"),
                    new ReportParameter("parameterEKPD","—"),
                    new ReportParameter("parameterLBGL","—"),
                    new ReportParameter("parameterGLXZ","—"),
                    new ReportParameter("parameterGLPD","—"),

                    new ReportParameter("parameterLOWHC","—"),
                    new ReportParameter("parameterLOWHCXZ","—"),
                    new ReportParameter("parameterLOWCO","—"),
                    new ReportParameter("parameterLOWCOXZ","—"),
                    new ReportParameter("parameterLOWPD","—"),
                    new ReportParameter("parameterHIGHCO","—"),
                    new ReportParameter("parameterHIGHCOXZ","—"),
                    new ReportParameter("parameterHIGHHC","—"),
                    new ReportParameter("parameterHIGHHCXZ","—"),
                    new ReportParameter("parameterLAMDA","—"),
                    new ReportParameter("parameterLAMDAXZ","—"),
                    new ReportParameter("parameterLAMDAPD","—"),
                    new ReportParameter("parameterHIGHPD","—"),

                    new ReportParameter("parameterDSZS",asm_data.DSZS),
                    new ReportParameter("parameterFIRSTDATA",asm_data.FIRSTDATA),
                    new ReportParameter("parameterSECONDDATA",asm_data.SECONDDATA),
                    new ReportParameter("parameterTHIRDDATA",asm_data.THIRDDATA),
                    new ReportParameter("parameterBTGXZ","≤"+asm_data.YDXZ),
                    new ReportParameter("parameterPJZ",asm_data.AVERAGEDATA),
                    new ReportParameter("parameterBTGPD",asm_data.ZHPD=="合格"?"○" : "×"),

                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"○" : "×"),
                    new ReportParameter("parameterJDTEL",mainPanel.otherinf.JDDH),
                    new ReportParameter("parameterFWTEL",mainPanel.otherinf.FWDH),
                    new ReportParameter("parameterWGJCY",mainPanel.wgjcy),
                    new ReportParameter("parameterBJY",mainPanel.bjy),
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
