﻿using System;
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
    public partial class reportViewSYVmasBefore : Form
    {
        SYS_DAL.VMASdal vmasdal = new SYS_DAL.VMASdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewSYVmasBefore()
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
                    new ReportParameter("parameterCllx",carinf.CLLX),
                    new ReportParameter("parameterQdfs",carinf.QDXS),
                    new ReportParameter("parameterChzz",carinf.CHZZ),
                    new ReportParameter("parameterClph",carinf.CLHP),
                    new ReportParameter("parameterCpys",carinf.CPYS),
                    new ReportParameter("parameterClsbm",carinf.CLSBM),
                    new ReportParameter("parameterCz",carinf.CZ),                  
                    new ReportParameter("parameterSbzzc",vmas_data.SBZZC), 
                    new ReportParameter("parameterSbmc",vmas_data.SBMC),
                    new ReportParameter("parameterSbxh",vmas_data.SBXH),
                    new ReportParameter("parameterCgjxh",vmas_data.CGJXH),
                    new ReportParameter("parameterCgjbh",vmas_data.CGJBH),
                    new ReportParameter("parameterFxyxh",vmas_data.FXYXH),
                    new ReportParameter("parameterFxybh",vmas_data.FXYBH),
                    new ReportParameter("parameterCgjcj",vmas_data.CGJZZC),
                    new ReportParameter("parameterFxycj",vmas_data.FXYZZC),
                    new ReportParameter("parameterLljcj",vmas_data.LLJZZC),
                    new ReportParameter("parameterLljxh",vmas_data.LLJXH),
                    new ReportParameter("parameterLljbh",vmas_data.LLJBH),
                    new ReportParameter("parameterLLj",vmas_data.LLJXH),
                    new ReportParameter("parameterLLjbh",vmas_data.LLJBH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterWd",vmas_data.WD+"℃"),
                    new ReportParameter("parameterDqy",vmas_data.DQY+"kPa"),
                    new ReportParameter("parameterSd",vmas_data.SD+"%"),
                    new ReportParameter("parameterCOCLZ",float.Parse(vmas_data.COZL).ToString("0.00")),
                    new ReportParameter("parameterCOXZ","≤"+vmas_data.COXZ),
                    new ReportParameter("parameterCOPD",vmas_data.COPD),
                    new ReportParameter("parameterHCCLZ",float.Parse(vmas_data.HCZL).ToString("0.00")),
                    new ReportParameter("parameterHCXZ","≤"+vmas_data.HCXZ),
                    new ReportParameter("parameterHCPD",vmas_data.HCPD),
                    new ReportParameter("parameterNOXCLZ",float.Parse(vmas_data.NOXZL).ToString("0.00")),
                    new ReportParameter("parameterNOXXZ","≤"+vmas_data.NOXXZ),
                    new ReportParameter("parameterNOXPD",vmas_data.NOXPD),
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
