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
    public partial class reportViewSYASM : Form
    {
        SYS_DAL.ASMdal asmdal = new SYS_DAL.ASMdal();
        SYS_DAL.loginInfControl logininfcontrol = new SYS_DAL.loginInfControl();
        public reportViewSYASM()
        {
            InitializeComponent();
        }

        private void reportView_Load(object sender, EventArgs e)
        {
            //this.reportViewer1.RefreshReport();
        }
        public void display_Asm(string clid)
        {
            SYS.Model.ASM asm_data = asmdal.Get_ASM(clid);
            SYS.Model.CARDETECTED carinf = logininfcontrol.getCarbjbycarID(clid);
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
                    new ReportParameter("parameterSbmc",asm_data.SBZZC),
                    new ReportParameter("parameterCgjxh",asm_data.CGJZZC+":"+asm_data.CGJXH),
                    new ReportParameter("parameterCgjbh",asm_data.CGJBH),
                    new ReportParameter("parameterFxyxh",asm_data.FXYZZC+":"+asm_data.FXYXH),
                    new ReportParameter("parameterFxybh",asm_data.FXYBH),
                    new ReportParameter("parameterWd",asm_data.WD+"℃"),
                    new ReportParameter("parameterDqy",asm_data.DQY+"KPa"),
                    new ReportParameter("parameterSd",asm_data.SD+"%"),
                    new ReportParameter("parameterHC25CLZ",asm_data.HC25CLZ),
                    new ReportParameter("parameterCO25CLZ",asm_data.CO25CLZ),
                    new ReportParameter("parameterNOX25CLZ",asm_data.NOX25CLZ),
                    new ReportParameter("parameterHC40CLZ",asm_data.HC40CLZ),
                    new ReportParameter("parameterCO40CLZ",asm_data.CO40CLZ),
                    new ReportParameter("parameterNOX40CLZ",asm_data.NOX40CLZ),
                    new ReportParameter("parameterHC25PD",asm_data.HC25PD),
                    new ReportParameter("parameterCO25PD",asm_data.CO25PD),
                    new ReportParameter("parameterNOX25PD",asm_data.NOX25PD),
                    new ReportParameter("parameterHC40PD",asm_data.HC40PD),
                    new ReportParameter("parameterCO40PD",asm_data.CO40PD),
                    new ReportParameter("parameterNOX40PD",asm_data.NOX40PD),
                    new ReportParameter("parameterHC25XZ","≤"+asm_data.HC25XZ),
                    new ReportParameter("parameterCO25XZ","≤"+asm_data.CO25XZ),
                    new ReportParameter("parameterNOX25XZ","≤"+asm_data.NOX25XZ),
                    new ReportParameter("parameterHC40XZ","≤"+asm_data.HC40XZ),
                    new ReportParameter("parameterCO40XZ","≤"+asm_data.CO40XZ),
                    new ReportParameter("parameterNOX40XZ","≤"+asm_data.NOX40XZ),
                    new ReportParameter("parameterZHPD",(asm_data.ZHPD=="合格")?"通过":"未通过"),
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
