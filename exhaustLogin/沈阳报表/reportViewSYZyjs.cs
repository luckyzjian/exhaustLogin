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
    public partial class reportViewSYZyjs : Form
    {
        SYS_DAL.Zyjsdal vmasdal = new SYS_DAL.Zyjsdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewSYZyjs()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }
        public void display_Vmas(string clid)
        {
            SYS.Model.Zyjs_Btg vmas_data = vmasdal.Get_Zyjs(clid);
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
                    new ReportParameter("parameterJcrq", carinf.JCSJ.ToString("G")),
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
                    new ReportParameter("parameterSbxh",vmas_data.SBXH),
                    new ReportParameter("parameterSbmc",vmas_data.SBMC),
                    new ReportParameter("parameterFxyxh",vmas_data.YDJXH),
                    new ReportParameter("parameterFxybh",vmas_data.YDJBH),
                    new ReportParameter("parameterFxycj",vmas_data.YDJZZC),
                    new ReportParameter("parameterZsjcj",vmas_data.ZSJZZC),
                    new ReportParameter("parameterZsjxh",vmas_data.ZSJXH),
                    new ReportParameter("parameterZsjbh",vmas_data.ZSJBH),
                    new ReportParameter("parameterCMA",imageCMA),
                    new ReportParameter("parameterRZBH",rzbh),
                    new ReportParameter("parameterSbzzc",vmas_data.SBZZC),
                    new ReportParameter("parameterSBBH",vmas_data.YDJBH),
                    new ReportParameter("parameterDSZS",vmas_data.DSZS),
                    new ReportParameter("parameterFIRSTDATA",double.Parse(vmas_data.FIRSTDATA).ToString("0.0")),
                    new ReportParameter("parameterSECONDDATA",double.Parse(vmas_data.SECONDDATA).ToString("0.0")),
                    new ReportParameter("parameterTHIRDDATA",double.Parse(vmas_data.THIRDDATA).ToString("0.0")),
                    new ReportParameter("parameterWd",vmas_data.WD+"℃"),
                    new ReportParameter("parameterDqy",vmas_data.DQY+"kPa"),
                    new ReportParameter("parameterSd",vmas_data.SD+"%"),
                    new ReportParameter("parameterXZ","≤"+double.Parse(vmas_data.YDXZ).ToString("0.0")),
                    new ReportParameter("parameterPJZ",double.Parse(vmas_data.AVERAGEDATA).ToString("0.0")),
                    new ReportParameter("parameterHGPD",(vmas_data.ZHPD=="合格")?"通过":"未通过"),
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
