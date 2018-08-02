using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using SYS.Model;
using SYS_DAL;

namespace exhaustLogin
{
    public partial class systemConfig : Form
    {
        private loginInfModel logininfmodel = new loginInfModel();
        //VMAS_XZBZ vmas_xzdb = new SYS.Model.VMAS_XZBZ();                                 //VMAS限值地标
        //JZJS_XZBZ jzjs_xzdb = new SYS.Model.JZJS_XZBZ();
        //ZYJS_XZGB zyjs_xzgb = new SYS.Model.ZYJS_XZGB();//被检测车辆的限值国标Model层
        //SDS_XZGB sds_xzgb = new SDS_XZGB();
        GBDal gbdal = new GBDal();

        DataTable dt_vmasxz1 = null, dt_vmasxz2 = null;
        VMAS_XZDB vmas_xzdb = new SYS.Model.VMAS_XZDB();
        string thissystemversion = "v180716";
        public static baseControl basecontrol = new baseControl();
        public systemConfig()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string printerName = comboBoxPrinter.Text;
            ini.INIIO.WritePrivateProfileString("设备", "printer", printerName, @".\appConfig.ini");
            mainPanel.printerName = printerName;
        }

        private void systemConfig_Load(object sender, EventArgs e)
        {
            logininfmodel = mainPanel.logininfcontrol.getLoginDefaultInf();
            foreach (String fPrinterName in LocalPrinter.GetLocalPrinters())
                comboBoxPrinter.Items.Add(fPrinterName);
            comboBoxPrinter.Text = mainPanel.printerName;
            comboBoxCopies.Text = mainPanel.copies.ToString();
            checkBoxCMA.Checked = mainPanel.isdisplayCMA;
            checkBoxRZBH.Checked = mainPanel.isdisplayCMANo;
            comboBoxReportStyle.Text = mainPanel.reportStyle;
            checkBoxJCCS.Checked = mainPanel.printJccs;
            textBoxAddName.Text = mainPanel.addname;
            comboBoxNetMode.Text = mainPanel.netMode;
            textBoxNhWsdl.Text = mainPanel.nhwebinf.weburl;
            textBoxNhLineID.Text = mainPanel.nhwebinf.lineid;
            textBoxZjUrl.Text = mainPanel.zjurl;

            textBoxJXUSER.Text = mainPanel.jxwebinf.user;
            textBoxJXPASSWORD.Text = mainPanel.jxwebinf.password;
            textBoxJXurl.Text = mainPanel.jxwebinf.url;

            textBoxHNHYURL.Text = mainPanel.hnhywebinf.weburl;
            textBoxHNHYJCZBH.Text = mainPanel.hnhywebinf.stationid;
            textBoxHNHYJCXBH.Text = mainPanel.hnhywebinf.lineid;

            comboBoxFontSize.Text = mainPanel.fontSize.ToString();
            comboBoxExLoginDefault.SelectedIndex = (int)(mainPanel.loginmode);

            textBoxDALIJCXBH.Text = mainPanel.daliwebinf.LINEID;
            textBoxDALISERVERIP.Text = mainPanel.daliwebinf.SERVERIP;
            textBoxDALISERVERPORT.Text = mainPanel.daliwebinf.SERVERPORT;

            textBoxOrtIp.Text = mainPanel.ortsocketinf.IP;
            textBoxOrtPort.Text = mainPanel.ortsocketinf.PORT;

            checkBoxQueryDataFromGA.Checked = mainPanel.isQueryInfFromGA;
            textBoxGAWEBURL.Text = mainPanel.gawebinf.weburl;
            textBoxGAXTLB.Text = mainPanel.gawebinf.xtlb;
            textBoxGAJKXLH.Text = mainPanel.gawebinf.jkxlh;
            textBoxGAJKID.Text = mainPanel.gawebinf.jyjgbh;

            textBoxAcHBJIP.Text= mainPanel.netInf.MOE_ip;
            textBoxAcZDIP.Text = mainPanel.netInf.StationServer_ip;
            textBoxAcJKXLH.Text= mainPanel.netInf.InterfaceNumber;
            textBoxAcHPQZ.Text = mainPanel.netInf.PlatePrefix;
            comboBoxAcArea.Text= mainPanel.netInf.Area;

            textBoxHhzUrl.Text = mainPanel.hhzwebinf.weburl;
            checkBoxHhzEnableQuery.Checked = mainPanel.hhzwebinf.enableQuery;
            textBoxHhzQueryAddr.Text = mainPanel.hhzwebinf.queryUrl;

            textBoxPlatePrefix.Text = logininfmodel.CLHP;

            showVmasXzInf();
            showbBTGXzInf();
            showASMXzInf();
            showSDSXzInf();
            showLugdownXzInf();
        }

        private void showVmasXzInf()
        {
            DataTable vmasxzdt = gbdal.Get_ALL_VMAS_XZBZ();
            if (vmasxzdt != null)
            {
                dataGridViewVmasXz1.DataSource = vmasxzdt;
                dataGridViewVmasXz1.Columns["ID"].HeaderText = "号牌前缀";
                dataGridViewVmasXz1.Columns["S1020bco"].HeaderText = "排放限值I-CO(RM≤1020)";
                dataGridViewVmasXz1.Columns["S10201470bco"].HeaderText = "排放限值I-CO(1020＜RM≤1470)";
                dataGridViewVmasXz1.Columns["S14701930bco"].HeaderText = "排放限值I-CO(1470＜RM≤1930)";
                dataGridViewVmasXz1.Columns["S1930bco"].HeaderText = "排放限值I-CO(1930＜RM)";
                dataGridViewVmasXz1.Columns["S1020bhc"].HeaderText = "排放限值I-HC(RM≤1020)";
                dataGridViewVmasXz1.Columns["S10201470bhc"].HeaderText = "排放限值I-HC(1020＜RM≤1470)";
                dataGridViewVmasXz1.Columns["S14701930bhc"].HeaderText = "排放限值I-HC(1470＜RM≤1930)";
                dataGridViewVmasXz1.Columns["S1930bhc"].HeaderText = "排放限值I-HC(1930＜RM)";
                dataGridViewVmasXz1.Columns["S1020bno"].HeaderText = "排放限值I-NOx(RM≤1020)";
                dataGridViewVmasXz1.Columns["S10201470bno"].HeaderText = "排放限值I-NOx(1020＜RM≤1470)";
                dataGridViewVmasXz1.Columns["S14701930bno"].HeaderText = "排放限值I-NOx(1470＜RM≤1930)";
                dataGridViewVmasXz1.Columns["S1930bno"].HeaderText = "排放限值I-NOx(1930＜RM)";
                dataGridViewVmasXz1.Columns["Sg1d1co"].HeaderText = "排放限值II-一类车CO(全部)";
                dataGridViewVmasXz1.Columns["S1250g1d2co"].HeaderText = "排放限值II-二类车CO(RM≤1250)";
                dataGridViewVmasXz1.Columns["S12501700g1d2co"].HeaderText = "排放限值II-二类车CO(1250＜RM≤1700)";
                dataGridViewVmasXz1.Columns["S1700g1d2co"].HeaderText = "排放限值II-二类车CO(1700＜RM)";
                dataGridViewVmasXz1.Columns["Sg1d1hcnox"].HeaderText = "排放限值II-一类车HC+NOx(全部)";
                dataGridViewVmasXz1.Columns["S1250g1d2hcnox"].HeaderText = "排放限值II-二类车HC+NOx(RM≤1250)";
                dataGridViewVmasXz1.Columns["S12501700g1d2hcnox"].HeaderText = "排放限值II-二类车HC+NOx(1250＜RM≤1700)";
                dataGridViewVmasXz1.Columns["S1700g1d2hcnox"].HeaderText = "排放限值II-二类车HC+NOx(1700＜RM)";


                dataGridViewVmasXz1.Columns["Sg2d1co"].Visible = false;
                dataGridViewVmasXz1.Columns["S1250g2d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["S12501700g2d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["S1700g2d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["Sg2d1hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S1250g2d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S12501700g2d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S1700g2d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["Sg3d1co"].Visible = false;
                dataGridViewVmasXz1.Columns["S1250g3d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["S12501700g3d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["S1700g3d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["Sg3d1hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S1250g3d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S12501700g3d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S1700g3d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["Sg4d1co"].Visible = false;
                dataGridViewVmasXz1.Columns["S1250g4d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["S12501700g4d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["S1700g4d2co"].Visible = false;
                dataGridViewVmasXz1.Columns["Sg4d1hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S1250g4d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S12501700g4d2hcnox"].Visible = false;
                dataGridViewVmasXz1.Columns["S1700g4d2hcnox"].Visible = false;
            }
        }
        private void showSDSXzInf()
        {
            DataTable vmasxzdt = gbdal.Get_ALL_SDS_XZBZ();
            if (vmasxzdt != null)
            {
                dataGridViewSDSXZ.DataSource = vmasxzdt;
                dataGridViewSDSXZ.Columns["ID"].Visible = false;
                dataGridViewSDSXZ.Columns["Hd1Date19950701qHC"].HeaderText = "95年7月1日前第一类车-高怠速HC";
                dataGridViewSDSXZ.Columns["Hd1Date19950701qCO"].HeaderText = "95年7月1日前第一类车-高怠速CO";
                dataGridViewSDSXZ.Columns["d1Date19950701qHC"].HeaderText = "95年7月1日前第一类车-怠速HC";
                dataGridViewSDSXZ.Columns["d1Date19950701qCO"].HeaderText = "95年7月1日前第一类车-怠速CO";
                dataGridViewSDSXZ.Columns["Hd1Date1995070120000701HC"].HeaderText = "95年7月1日~00年7月1日第一类车-高怠速HC";
                dataGridViewSDSXZ.Columns["Hd1Date1995070120000701CO"].HeaderText = "95年7月1日~00年7月1日第一类车-高怠速CO";
                dataGridViewSDSXZ.Columns["d1Date1995070120000701HC"].HeaderText = "95年7月1日~00年7月1日第一类车-怠速HC";
                dataGridViewSDSXZ.Columns["d1Date1995070120000701CO"].HeaderText = "95年7月1日~00年7月1日第一类车-怠速CO";
                dataGridViewSDSXZ.Columns["Hd1Date20000701HC"].HeaderText = "00年7月1日后第一类车-高怠速HC";
                dataGridViewSDSXZ.Columns["Hd1Date20000701CO"].HeaderText = "00年7月1日后第一类车-高怠速CO";
                dataGridViewSDSXZ.Columns["d1Date20000701HC"].HeaderText = "00年7月1日后第一类车-怠速HC";
                dataGridViewSDSXZ.Columns["d1Date20000701CO"].HeaderText = "00年7月1日后第一类车-怠速CO";
                dataGridViewSDSXZ.Columns["Hd2Date19950701qHC"].HeaderText = "95年7月1日前第二类车-高怠速HC";
                dataGridViewSDSXZ.Columns["Hd2Date19950701qCO"].HeaderText = "95年7月1日前第二类车-高怠速CO";
                dataGridViewSDSXZ.Columns["d2Date19950701qHC"].HeaderText = "95年7月1日前第二类车-怠速HC";
                dataGridViewSDSXZ.Columns["d2Date19950701qCO"].HeaderText = "95年7月1日前第二类车-怠速CO";
                dataGridViewSDSXZ.Columns["Hd2Date1995070120011001HC"].HeaderText = "95年7月1日~01年10月1日第一类车-高怠速HC";
                dataGridViewSDSXZ.Columns["Hd2Date1995070120011001CO"].HeaderText = "95年7月1日~01年10月1日第一类车-高怠速CO";
                dataGridViewSDSXZ.Columns["d2Date1995070120011001HC"].HeaderText = "95年7月1日~01年10月1日第一类车-怠速HC";
                dataGridViewSDSXZ.Columns["d2Date1995070120011001CO"].HeaderText = "95年7月1日~01年10月1日第一类车-怠速CO";
                dataGridViewSDSXZ.Columns["Hd2Date20011001HC"].HeaderText = "01年10月1日后第一类车-高怠速HC";
                dataGridViewSDSXZ.Columns["Hd2Date20011001CO"].HeaderText = "01年10月1日后第一类车-高怠速CO";
                dataGridViewSDSXZ.Columns["d2Date20011001HC"].HeaderText = "01年10月1日后第一类车-怠速HC";
                dataGridViewSDSXZ.Columns["d2Date20011001CO"].HeaderText = "01年10月1日后第一类车-怠速CO";
                dataGridViewSDSXZ.Columns["HzxDate19950701qHC"].HeaderText = "95年7月1日前重型车-高怠速HC";
                dataGridViewSDSXZ.Columns["HzxDate19950701qCO"].HeaderText = "95年7月1日前重型车-高怠速CO";
                dataGridViewSDSXZ.Columns["zxDate19950701qHC"].HeaderText = "95年7月1日前重型车-怠速HC";
                dataGridViewSDSXZ.Columns["zxDate19950701qCO"].HeaderText = "95年7月1日前重型车-怠速CO";
                dataGridViewSDSXZ.Columns["HzxDate1995070120040901HC"].HeaderText = "95年7月1日~04年9月1日重型车-高怠速HC";
                dataGridViewSDSXZ.Columns["HzxDate1995070120040901CO"].HeaderText = "95年7月1日~04年9月1日重型车-高怠速CO";
                dataGridViewSDSXZ.Columns["zxDate1995070120040901HC"].HeaderText = "95年7月1日~04年9月1日重型车-怠速HC";
                dataGridViewSDSXZ.Columns["zxDate1995070120040901CO"].HeaderText = "95年7月1日~04年9月1日重型车-怠速CO";
                dataGridViewSDSXZ.Columns["HzxDate20040901HC"].HeaderText = "04年9月1日后重型车-高怠速HC";
                dataGridViewSDSXZ.Columns["HzxDate20040901CO"].HeaderText = "04年9月1日后重型车-高怠速CO";
                dataGridViewSDSXZ.Columns["zxDate20040901HC"].HeaderText = "04年9月1日后重型车-怠速HC";
                dataGridViewSDSXZ.Columns["zxDate20040901CO"].HeaderText = "04年9月1日后重型车-怠速CO";

                dataGridViewSDSXZ.Columns["HNewd1HC"].Visible = false;
                dataGridViewSDSXZ.Columns["HNewd1CO"].Visible = false;
                dataGridViewSDSXZ.Columns["Newd1HC"].Visible = false;
                dataGridViewSDSXZ.Columns["Newd1CO"].Visible = false;
                dataGridViewSDSXZ.Columns["HNewd2HC"].Visible = false;
                dataGridViewSDSXZ.Columns["HNewd2CO"].Visible = false;
                dataGridViewSDSXZ.Columns["Newd2HC"].Visible = false;
                dataGridViewSDSXZ.Columns["Newd2CO"].Visible = false;
                dataGridViewSDSXZ.Columns["zxHNewHC"].Visible = false;
                dataGridViewSDSXZ.Columns["zxHNewCO"].Visible = false;
                dataGridViewSDSXZ.Columns["zxNewHC"].Visible = false;
                dataGridViewSDSXZ.Columns["zxNewCO"].Visible = false;
            }
        }
        private void showASMXzInf()
        {
            DataTable vmasxzdt = gbdal.Get_ALL_ASM_XZBZ();
            if (vmasxzdt != null)
            {
                dataGridViewASMXZ.DataSource = vmasxzdt;
                dataGridViewASMXZ.Columns["ID"].HeaderText = "号牌前缀";
                dataGridViewASMXZ.Columns["d1Date20000701qHC5025"].HeaderText = "排放限值I-HC(ASM5025)";
                dataGridViewASMXZ.Columns["d1Date20000701qCO5025"].HeaderText = "排放限值I-CO(ASM5025)";
                dataGridViewASMXZ.Columns["d1Date20000701qNO5025"].HeaderText = "排放限值I-NO(ASM5025)";
                dataGridViewASMXZ.Columns["d1Date20000701qHC2540"].HeaderText = "排放限值I-HC(ASM2540)";
                dataGridViewASMXZ.Columns["d1Date20000701qCO2540"].HeaderText = "排放限值I-CO(ASM2540)";
                dataGridViewASMXZ.Columns["d1Date20000701qNO2540"].HeaderText = "排放限值I-NO(ASM2540)";
                dataGridViewASMXZ.Columns["d1Date20000701HC5025"].HeaderText = "排放限值II-HC(ASM5025)";
                dataGridViewASMXZ.Columns["d1Date20000701CO5025"].HeaderText = "排放限值II-CO(ASM5025)";
                dataGridViewASMXZ.Columns["d1Date20000701NO5025"].HeaderText = "排放限值II-NO(ASM5025)";
                dataGridViewASMXZ.Columns["d1Date20000701HC2540"].HeaderText = "排放限值II-HC(ASM2540)";
                dataGridViewASMXZ.Columns["d1Date20000701CO2540"].HeaderText = "排放限值II-CO(ASM2540)";
                dataGridViewASMXZ.Columns["d1Date20000701NO2540"].HeaderText = "排放限值II-NO(ASM2540)";
                dataGridViewASMXZ.Columns["d2Date20011001qHC5025"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001qCO5025"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001qNO5025"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001qHC2540"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001qCO2540"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001qNO2540"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001HC5025"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001CO5025"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001NO5025"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001HC2540"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001CO2540"].Visible = false;
                dataGridViewASMXZ.Columns["d2Date20011001NO2540"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901qHC5025"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901qCO5025"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901qNO5025"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901qHC2540"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901qCO2540"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901qNO2540"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901HC5025"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901CO5025"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901NO5025"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901HC2540"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901CO2540"].Visible = false;
                dataGridViewASMXZ.Columns["zxDate20040901NO2540"].Visible = false;
            }
        }
        private void showbBTGXzInf()
        {
            DataTable vmasxzdt = gbdal.Get_ALL_BTG_XZBZ();
            if (vmasxzdt != null)
            {
                dataGridViewBTGXZ.DataSource = vmasxzdt;
                //dataGridViewVmasXz1.DataSource = vmasxzdt;
            }
            ZYJS_XZGB zyjs_xzgb = gbdal.Get_ZYJS_XZGB();
            textBoxBTGZRXQ.Text = zyjs_xzgb.ZRDate20011001btgxz;
            textBoxBTGWLZY.Text = zyjs_xzgb.WLDate20011001btgxz;
            checkBoxBtgXz.Checked = zyjs_xzgb.onlyUseThis;
        }
        private void showLugdownXzInf()
        {
            DataTable vmasxzdt = gbdal.Get_ALL_JZJS_XZBZ();
            if (vmasxzdt != null)
            {
                dataGridViewLUGDOWNXZ.DataSource = vmasxzdt;
                dataGridViewLUGDOWNXZ.Columns["ID"].Visible = false;
                dataGridViewLUGDOWNXZ.Columns["d1Date20000701b"].HeaderText = "2000年7月1日前第一类车";
                dataGridViewLUGDOWNXZ.Columns["d1Date2000070120050630"].HeaderText = "2001年7月1日~2005年6月30日第一类车";
                dataGridViewLUGDOWNXZ.Columns["d1Date20050701"].HeaderText = "2005年7月1日起第一类车";
                dataGridViewLUGDOWNXZ.Columns["d2Date20011001b"].HeaderText = "2001年10月1日前第二类车";
                dataGridViewLUGDOWNXZ.Columns["d2Date2001100120060630"].HeaderText = "2001年10月1日~2006年6月30日第二类车";
                dataGridViewLUGDOWNXZ.Columns["d2Date20060701"].HeaderText = "2006年7月1日起第二类车";
                dataGridViewLUGDOWNXZ.Columns["zxDate20010901b"].HeaderText = "2001年1月以前生产的重型车";
                dataGridViewLUGDOWNXZ.Columns["zxDate2001090120040831"].HeaderText = "2001年1月~2004年12月生产的重型车";
                dataGridViewLUGDOWNXZ.Columns["zxDate20040901"].HeaderText = "2004年1月起生产的重型车";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Copies = comboBoxCopies.Text;
            ini.INIIO.WritePrivateProfileString("设备", "份数", Copies, @".\appConfig.ini");
            mainPanel.copies = int.Parse(Copies);
            string cma = checkBoxCMA.Checked ? "是" : "否";
            ini.INIIO.WritePrivateProfileString("设备", "CMA标志显示", cma, @".\appConfig.ini");
            mainPanel.isdisplayCMA = checkBoxCMA.Checked;
            string cmaNo = checkBoxRZBH.Checked ? "是" : "否";
            ini.INIIO.WritePrivateProfileString("设备", "CMA编号显示", cmaNo, @".\appConfig.ini");
            mainPanel.isdisplayCMANo = checkBoxRZBH.Checked;
            ini.INIIO.WritePrivateProfileString("检测线", "报表样式", comboBoxReportStyle.Text, @".\appConfig.ini");
            mainPanel.reportStyle = comboBoxReportStyle.Text;
            string printJccs = checkBoxJCCS.Checked ? "Y" : "N";
            ini.INIIO.WritePrivateProfileString("检测线", "显示检测次数", printJccs, @".\appConfig.ini");
            mainPanel.printJccs = checkBoxJCCS.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ini.INIIO.WritePrivateProfileString("检测线", "地区", textBoxAddName.Text, @".\appConfig.ini");
            mainPanel.addname = textBoxAddName.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string cllx, clxh, fdjxh, fdjcj, xshzz, xz, qccj;
            cllx = textBoxBTGCLLX.Text;
            clxh = textBoxBTGCLXH.Text;
            fdjcj = textBoxBTGFDJSCQY.Text;
            fdjxh = textBoxBTGFDJXH.Text;
            xshzz = textBoxBTGFDJXSHZZ.Text;
            qccj = textBoxBTGCLSCQY.Text;
            if (cllx == "")
            {
                MessageBox.Show("请填写车辆类型");
                return;
            }
            if (clxh == "")
            {
                MessageBox.Show("请填写车辆型号");
                return;
            }
            if (fdjcj == "")
            {
                MessageBox.Show("请填写车辆发动机厂家");
                return;
            }
            if (fdjxh == "")
            {
                MessageBox.Show("请填写发动机型号");
                return;
            }
            if (qccj == "")
            {
                MessageBox.Show("请填写车辆生产厂家");
                return;
            }
            if (xshzz == "")
            {
                MessageBox.Show("请填写形式核准值");
                return;
            }
            double xshzzd = 0, xzd = 0;
            if (!double.TryParse(xshzz, out xshzzd))
            {
                MessageBox.Show("形式核准值不是有效数,请检查");
                return;
            }
            xshzz = xshzzd.ToString("0.00");
            xz = (xshzzd + 0.5).ToString("0.00");
            gbdal.saveBtgXz(clxh, cllx, fdjxh, fdjcj, xshzz, xz, qccj);
            showbBTGXzInf();
            MessageBox.Show("添加成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            ini.INIIO.WritePrivateProfileString("南华联网", "WEBURL", textBoxNhWsdl.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("南华联网", "LINEID", textBoxNhLineID.Text, @".\appConfig.ini");
            MessageBox.Show("success to update!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mainPanel.zjurl = textBoxZjUrl.Text;
            ini.INIIO.WritePrivateProfileString("浙江联网", "WEBURL", textBoxZjUrl.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");

        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            ini.INIIO.WritePrivateProfileString("江西联网", "账号", textBoxJXUSER.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("江西联网", "密码", textBoxJXPASSWORD.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("江西联网", "url", textBoxJXurl.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ini.INIIO.WritePrivateProfileString("湖南衡阳联网", "WEBURL", textBoxHNHYURL.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("湖南衡阳联网", "STATIONID", textBoxHNHYJCZBH.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("湖南衡阳联网", "LINEID", textBoxHNHYJCXBH.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                mainPanel.fontSize =double.Parse(comboBoxFontSize.Text);
                mainPanel.loginmode =(mainPanel.LoginDefaultMode)comboBoxExLoginDefault.SelectedIndex;
                ini.INIIO.WritePrivateProfileString("界面设置", "字体大小", comboBoxFontSize.Text, @".\appConfig.ini");
                ini.INIIO.WritePrivateProfileString("界面设置", "登录默认", ((int)mainPanel.loginmode).ToString(), @".\appConfig.ini");
                mainPanel.logininfcontrol.updatePlatePrefix(textBoxPlatePrefix.Text);
                MessageBox.Show("保存成功!");
            }
            catch
            { MessageBox.Show("输入有误!"); }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            mainPanel.daliwebinf.LINEID = textBoxDALIJCXBH.Text;
            mainPanel.daliwebinf.SERVERIP = textBoxDALISERVERIP.Text;
            mainPanel.daliwebinf.SERVERPORT = textBoxDALISERVERPORT.Text;
            ini.INIIO.WritePrivateProfileString("大理联网", "LINEID", textBoxDALIJCXBH.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("大理联网", "SERVERIP", textBoxDALISERVERIP.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("大理联网", "SERVERPORT", textBoxDALISERVERPORT.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");

        }

        private void button12_Click(object sender, EventArgs e)
        {
            mainPanel.ortsocketinf.IP = textBoxOrtIp.Text;
            mainPanel.ortsocketinf.PORT = textBoxOrtPort.Text;
            ini.INIIO.WritePrivateProfileString("欧润特联网", "服务器IP", textBoxOrtIp.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("欧润特联网", "服务器PORT", textBoxOrtPort.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            mainPanel.gawebinf.xtlb = textBoxGAXTLB.Text;
            mainPanel.gawebinf.jkxlh = textBoxGAJKXLH.Text;
            mainPanel.gawebinf.jyjgbh = textBoxGAJKID.Text;
            mainPanel.gawebinf.weburl = textBoxGAWEBURL.Text;
            ini.INIIO.WritePrivateProfileString("公安联网", "WEBURL", textBoxGAWEBURL.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("公安联网", "XTLB", textBoxGAXTLB.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("公安联网", "JKXLH", textBoxGAJKXLH.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("公安联网", "JYJGBH", textBoxGAJKID.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            mainPanel.netInf.MOE_ip = textBoxAcHBJIP.Text;
            mainPanel.netInf.StationServer_ip = textBoxAcZDIP.Text;
            mainPanel.netInf.InterfaceNumber = textBoxAcJKXLH.Text;
            mainPanel.netInf.PlatePrefix = textBoxAcHPQZ.Text;
            mainPanel.netInf.Area = comboBoxAcArea.Text;
            ini.INIIO.WritePrivateProfileString("联网信息", "环保局服务器IP", textBoxAcHBJIP.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("联网信息", "检测站服务器IP", textBoxAcZDIP.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("联网信息", "接口序列号", textBoxAcJKXLH.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("联网信息", "号牌前缀", textBoxAcHPQZ.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("联网信息", "安车联网地区", comboBoxAcArea.Text, @".\appConfig.ini");
            MessageBox.Show("保存成功!");
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            mainPanel.hhzwebinf.weburl= textBoxHhzUrl.Text;
            mainPanel.hhzwebinf.enableQuery = checkBoxHhzEnableQuery.Checked;
            mainPanel.hhzwebinf.queryUrl = textBoxHhzQueryAddr.Text;
            ini.INIIO.WritePrivateProfileString("红河州联网", "WEBURL", textBoxHhzUrl.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("红河州联网", "QUERYURL", textBoxHhzQueryAddr.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("红河州联网", "ENABLEQUERY", checkBoxHhzEnableQuery.Checked?"Y":"N", @".\appConfig.ini");
            MessageBox.Show("保存成功!");

        }

        private void button19_Click(object sender, EventArgs e)
        {

            string presystemversion = basecontrol.GetSystemVersion();
            if (basecontrol.GetSystemVersion() != thissystemversion)
            {
                textBoxCheck.AppendText("当前版本：" + presystemversion + "\r\n");
                textBoxCheck.AppendText("不是最新版本，请进行升级" + "\r\n");
            }
            else
            {
                textBoxCheck.AppendText("当前版本：" + presystemversion + "\r\n");
                textBoxCheck.AppendText("已是最新版本" + "\r\n");

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string listname;
            listname = "车辆信息";
            string[] addkeyarraycarInf = { "HPZL", "CSYS", "ZXBZ", "CCS" };
            //keyname = "FOURTHDATA";
            foreach (string keyname in addkeyarraycarInf)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "stationNormalInf ";
            string[] addkeyarrayStationNormalInf = { "YWLSH", "CLEARMODE", "JJLX", "LXR", "FZR", "ZCSJ", "JCSJ", "DJLSH", "JCXS", "LSHRULE" };
            //keyname = "FOURTHDATA";
            foreach (string keyname in addkeyarrayStationNormalInf)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            string[] addkeyarrayStationNormalInf2 = { "STATIONCOUNTDATE", "LOGINCOUNTDATE" };
            foreach (string keyname in addkeyarrayStationNormalInf2)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addDateTimekeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "Zyjs_Btg";
            string[] addkeyarray = { "FOURTHDATA", "JCFF", "JYLSH", "JYCS", "SBRZBM" };
            //keyname = "FOURTHDATA";
            foreach (string keyname in addkeyarray)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "ZYJS_XZGB";
            string[] addkeyarrayBTG = { "ONLYTHIS" };
            //keyname = "FOURTHDATA";
            foreach (string keyname in addkeyarrayBTG)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "ASM";
            string[] addkeyarray2 = { "CO25025", "O25025", "CO22540", "O22540", "JYLSH", "JYCS", "SBRZBM", "LAMBDA5025", "LAMBDA2540", "JZZGL5025", "JZZGL2540" };
            foreach (string keyname in addkeyarray2)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "SDS";
            string[] addkeyarraySDS = { "CO2HIGH", "O2HIGH", "CO2LOW", "O2LOW", "JYLSH", "JYCS", "SBRZBM", "COLOWXXZ", "COHIGHXXZ", "COLOWXYZ", "COHIGHXYZ", "CO2LOWXYZ", "CO2HIGHXYZ", "HCLOWXYZ", "HCHIGHXYZ" };
            foreach (string keyname in addkeyarraySDS)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "JZJS";
            string[] addkeyarrayJZJS = { "GLXZXS", "ACTMAXHP", "REALVELMAXHP", "VELMAXHP", "VELMAXHPZS", "RATEREVUP", "RATEREVDOWN", "JYLSH", "JYCS", "SBRZBM", "HNO", "NNO", "ENO" };
            foreach (string keyname in addkeyarrayJZJS)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "VMAS";
            string[] addkeyarrayVMAS = { "CSLJCCSJ", "XSLC", "JYLSH", "JYCS", "SBRZBM" };
            foreach (string keyname in addkeyarrayVMAS)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "检测线设备";
            string[] addkeyarrayLZ = { "LZYDJXH", "LZYDJBH", "LZYDJZZC" };
            foreach (string keyname in addkeyarrayLZ)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "待检车辆";
            string[] addkeyarray3 = { "SFCJ", "JYLSH", "HPZL", "ZT", "WXBJ", "WXCD", "WXSJ", "WXFY", "GYXTXS", "SFLJ", "FWLX", "ICCARDNO", "DPFS" };
            foreach (string keyname in addkeyarray3)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            listname = "已检车辆信息";
            string[] addkeyarray4 = { "QDLTQY", "RYPH", "HPZL", "JCGCSJ", "WJY", "YWLSH", "BGFFYY", "CCS", "JYLSH" };
            foreach (string keyname in addkeyarray4)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            textBoxUpdate.AppendText("检查表[变载荷滑行测试]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("变载荷滑行测试"))
            {
                textBoxUpdate.AppendText("表[变载荷滑行测试]不存在\r\n");
                basecontrol.createTalbe("变载荷滑行测试", "STATIONID varchar(50) not null, LINEID varchar(50) not null, HXQJ varchar(50), CCDT varchar(50), ACDT varchar(50), WC varchar(50), BDJG varchar(50), BZSM varchar(50), CZY varchar(50), BDRQ datetime");
                textBoxUpdate.AppendText("表[变载荷滑行测试]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[变载荷滑行测试]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[操作日志]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("操作日志"))
            {
                textBoxUpdate.AppendText("表[操作日志]不存在\r\n");
                basecontrol.createTalbe("操作日志", "PROID varchar(50) not null primary key, PRONAME varchar(50),STATIONID varchar(50), LINEID varchar(50), CZY varchar(50), DATA varchar(100), STATE varchar(50), RESULT varchar(50), BZSM varchar(100), BDRQ datetime");
                textBoxUpdate.AppendText("表[操作日志]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[操作日志]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[废气仪检查]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("废气仪检查"))
            {
                textBoxUpdate.AppendText("表[废气仪检查]不存在\r\n");
                basecontrol.createTalbe("废气仪检查", "STATIONID varchar(50), LINEID varchar(50), CO2BZ varchar(50), CO2CLZ varchar(50), COBZ varchar(50), COCLZ varchar(50), HCBZ varchar(50), HCCLZ varchar(50), NOBZ varchar(50), NOCLZ varchar(50), JZDS varchar(50), GDJZ varchar(50), BZSM varchar(50), BDJG varchar(50), BDRQ datetime");
                textBoxUpdate.AppendText("表[废气仪检查]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[废气仪检查]已存在\r\n");
            }


            textBoxUpdate.AppendText("检查表[气象站校准]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("气象站校准"))
            {
                textBoxUpdate.AppendText("表[气象站校准]不存在\r\n");
                basecontrol.createTalbe("气象站校准", "STATIONID varchar(50), LINEID varchar(50), WDBZ varchar(50), WDSCZ varchar(50), SDBZ varchar(50), SDSCZ varchar(50), DQYBZ varchar(50), DQYSCZ varchar(50), WDWC varchar(50), SDWC varchar(50), DQYWC varchar(50), BZSM varchar(50), BDJG varchar(50), CZY varchar(50), BDRQ datetime");
                textBoxUpdate.AppendText("表[气象站校准]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[气象站校准]已存在\r\n");
            }

            textBoxUpdate.AppendText("检查表[认证信息]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("认证信息"))
            {
                textBoxUpdate.AppendText("表[认证信息]不存在\r\n");
                basecontrol.createTalbe("认证信息", "ID varchar(50) not null primary key, RZBH varchar(50), RZYXQ varchar(50)");
                textBoxUpdate.AppendText("表[认证信息]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[认证信息]已存在\r\n");
            }

            textBoxUpdate.AppendText("检查表[响应时间测试]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("响应时间测试"))
            {
                textBoxUpdate.AppendText("表[响应时间测试]不存在\r\n");
                basecontrol.createTalbe("响应时间测试", "STATIONID varchar(50) not null, LINEID varchar(50) not null, SPEED varchar(50), STARTPOWER varchar(50), STARTFORCE varchar(50), ENDPOWER varchar(50), ENDFORCE varchar(50), XYTIME varchar(50), WDTIME varchar(50), BDJG varchar(50), CZY varchar(50), BDRQ datetime");
                textBoxUpdate.AppendText("表[响应时间测试]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[响应时间测试]已存在\r\n");
            }

            textBoxUpdate.AppendText("检查表[烟度计标定]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("烟度计标定"))
            {
                textBoxUpdate.AppendText("表[烟度计标定]不存在\r\n");
                basecontrol.createTalbe("烟度计标定", "STATIONID varchar(50) not null, LINEID varchar(50) not null, KBZ varchar(50), KSCZ varchar(50), KABSWC varchar(50), KRELWC varchar(50), BDJG varchar(50), BZSM varchar(50), CZY varchar(50), BDRQ datetime");
                textBoxUpdate.AppendText("表[烟度计标定]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[烟度计标定]已存在\r\n");
            }

            textBoxUpdate.AppendText("检查表[自检记录]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("自检记录"))
            {
                textBoxUpdate.AppendText("表[自检记录]不存在\r\n");
                basecontrol.createTalbe("自检记录", "ID varchar(50) not null primary key, WORKTIME datetime, WORKER varchar(50), ISQXZCHECK varchar(1), ISFQYCHECK varchar(1), ISBTGCHECK varchar(1), ISCGJCHECK varchar(1), ISLLJCHECK varchar(1), ISZSJCHECK varchar(1), TEMPOK varchar(1), HUMIOK varchar(1), AIRPOK varchar(1), TEMP varchar(50), HUMI varchar(50), AIRP varchar(50), FQYTX varchar(1), FQYYR varchar(1), FQYTL varchar(1), FQYJL varchar(1), FQYLC varchar(1), FQYO2 varchar(50), BTGTX varchar(1), BTGYR varchar(1), BTGTL varchar(1), BTGLC varchar(1), BTGJZ varchar(1), CGJTX varchar(1), CGJYR varchar(1), CGJQL varchar(1), CGJJSGL varchar(1), CGJJZHX varchar(1), CGJEDGL varchar(50), CGJSJGL varchar(50), CGJGLWC varchar(50), CGJVITRUALTIME varchar(50), CGJREALTIME varchar(50), CGJTIMEWC varchar(50), LLJTX varchar(1), LLJLL varchar(1), ZSJTX varchar(1), ZSJLC varchar(1)");
                textBoxUpdate.AppendText("表[自检记录]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[自检记录]已存在\r\n");
            }

            listname = "自检记录";
            string[] addkeyarray5 = { "HSSlideBeginTime", "HSSlideEndTime", "HSSlideTheoreticalTime", "HSSlideActualTime", "HSSlideLoadPower", "LSSlideBeginTime", "LSSlideEndTime", "LSSlideTheoreticalTime", "LSSlideActualTime", "LSSlideLoadPower",
            "WattlessMaxSpeed1","WattlessMinSpeed1","WattlessNorminalSpeed1","WattlessBeginTime1","WattlessEndTime1","WattlessOutput1",
            "WattlessMaxSpeed2","WattlessMinSpeed2","WattlessNorminalSpeed2","WattlessBeginTime2","WattlessEndTime2","WattlessOutput2",
            "WattlessMaxSpeed3","WattlessMinSpeed3","WattlessNorminalSpeed3","WattlessBeginTime3","WattlessEndTime3","WattlessOutput3",
            "WattlessMaxSpeed4","WattlessMinSpeed4","WattlessNorminalSpeed4","WattlessBeginTime4","WattlessEndTime4","WattlessOutput4",
            "O2MRBeginTime","O2MREndTime","WQBeginTime","WQEndTime","SlideJudge","WattlessJudge","O2MRJudge","WQJudge","AllJudge"};
            foreach (string keyname in addkeyarray5)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            textBoxUpdate.AppendText("检查表[运行状态]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("运行状态"))
            {
                textBoxUpdate.AppendText("表[运行状态]不存在\r\n");
                basecontrol.createTalbe("运行状态", "CLHP varchar(50) not null primary key, UPDATETIME datetime, ID varchar(50), LINEID varchar(50), STATUS varchar(50), YL1 varchar(50), YL2 varchar(50), YL3 varchar(50), YL4 varchar(50)");
                textBoxUpdate.AppendText("表[运行状态]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[运行状态]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[温湿度共享]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("温湿度共享"))
            {
                textBoxUpdate.AppendText("表[温湿度共享]不存在\r\n");
                basecontrol.createTalbe("温湿度共享", "ID varchar(50) not null primary key, UPDATETIME datetime, WD varchar(50), SD varchar(50), DQY varchar(50)");
                textBoxUpdate.AppendText("表[温湿度共享]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[温湿度共享]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[ZYJS_DATASECONDS]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("ZYJS_DATASECONDS"))
            {
                textBoxUpdate.AppendText("表[ZYJS_DATASECONDS]不存在\r\n");
                basecontrol.createTalbe("ZYJS_DATASECONDS", "CLID varchar(50) not null primary key, CLHP varchar(50), JCSJ datetime, MMTIME varchar(8000), MMSX varchar(8000), MMLB varchar(8000), MMK varchar(8000), MMZS varchar(8000)");
                textBoxUpdate.AppendText("表[ZYJS_DATASECONDS]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[ZYJS_DATASECONDS]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[VMAS_DATASECONDS]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("VMAS_DATASECONDS"))
            {
                textBoxUpdate.AppendText("表[VMAS_DATASECONDS]不存在\r\n");
                basecontrol.createTalbe("VMAS_DATASECONDS", "CLID varchar(50) not null primary key, CLHP varchar(50), JCSJ datetime, MMTIME varchar(8000), MMSX varchar(8000), MMLB varchar(8000), MMCO varchar(8000), MMHC varchar(8000), MMNO varchar(8000), MMCO2 varchar(8000), MMO2 varchar(8000), MMCOZL varchar(8000), MMHCZL varchar(8000), MMNOZL varchar(8000), MMXSO2 varchar(8000), MMHJO2 varchar(8000), MMSJLL varchar(8000), MMBZLL varchar(8000), MMWQLL varchar(8000), MMXSB varchar(8000), MMWD varchar(8000), MMSD varchar(8000), MMDQY varchar(8000), MMLLJWD varchar(8000), MMLLJYL varchar(8000), MMCS varchar(8000), MMBZCS varchar(8000), MMXSXZ varchar(8000), MMSDXZ varchar(8000), MMGLYL varchar(8000), MMYW varchar(8000), MMJSGL varchar(8000), MMNJ varchar(8000), MMGL varchar(8000)");
                textBoxUpdate.AppendText("表[VMAS_DATASECONDS]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[VMAS_DATASECONDS]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[ASM_GDXZ]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("ASM_GDXZ"))
            {
                textBoxUpdate.AppendText("表[ASM_GDXZ]不存在\r\n");
                basecontrol.createTalbe("ASM_GDXZ", "ID varchar(50) not null primary key, CO25 varchar(50), HC25 varchar(50), NO25 varchar(50), CO40 varchar(50), HC40 varchar(50), NO40 varchar(50)");
                textBoxUpdate.AppendText("表[ASM_GDXZ]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[ASM_GDXZ]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[补偿参数]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("补偿参数"))
            {
                textBoxUpdate.AppendText("表[补偿参数]不存在\r\n");
                basecontrol.createTalbe("补偿参数", "LINEID varchar(50), ASM_HC varchar(50), ASM_CO varchar(50), ASM_NO varchar(50), VMAS_CO varchar(50), VMAS_HC varchar(50), VMAS_NO varchar(50), SDS_CO varchar(50), SDS_HC varchar(50), ZYJS_K varchar(50), JZJS_K varchar(50), JZJS_GL varchar(50), ISUSE varchar(1)");
                textBoxUpdate.AppendText("表[补偿参数]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[ASM_GDXZ]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[设备自检数据]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("设备自检数据"))
            {
                textBoxUpdate.AppendText("表[设备自检数据]不存在\r\n");
                basecontrol.createTalbe("设备自检数据", "JCZBH varchar(50), JCGWH varchar(50), SBBH varchar(50), ZJLX varchar(50), ZJSJ datetime," +
                    " DATA1 varchar(50), DATA2 varchar(50), DATA3 varchar(50), DATA4 varchar(50), DATA5 varchar(50), DATA6 varchar(50)," +
                    " DATA7 varchar(50), DATA8 varchar(50), DATA9 varchar(50), DATA10 varchar(50), DATA11 varchar(50), DATA12 varchar(50)," +
                    " DATA13 varchar(50), DATA14 varchar(50), DATA15 varchar(50), DATA16 varchar(50), DATA17 varchar(50), DATA18 varchar(50)," +
                    " DATA19 varchar(50), DATA20 varchar(50), DATA21 varchar(50), DATA22 varchar(50), DATA23 varchar(50), DATA24 varchar(50)," +
                    " DATA25 varchar(50), DATA26 varchar(50), DATA27 varchar(50), DATA28 varchar(50), DATA29 varchar(50), DATA30 varchar(50)," +
                    " DATA31 varchar(50), DATA32 varchar(50), DATA33 varchar(50), DATA34 varchar(50), DATA35 varchar(50), DATA36 varchar(50)," +
                    " DATA37 varchar(50), DATA38 varchar(50), DATA39 varchar(50), DATA40 varchar(50), DATA41 varchar(50), DATA42 varchar(50)," +
                    " ZJJG varchar(50), ZT varchar(50)");
                textBoxUpdate.AppendText("表[设备自检数据]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[设备自检数据]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[设备标定数据]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("设备标定数据"))
            {
                textBoxUpdate.AppendText("表[设备标定数据]不存在\r\n");
                basecontrol.createTalbe("设备标定数据", "JCZBH varchar(50), JCGWH varchar(50), SBBH varchar(50), BDR varchar(50), BDLX varchar(50), BDSJ datetime," +
                    " DATA1 varchar(50), DATA2 varchar(50), DATA3 varchar(50), DATA4 varchar(50), DATA5 varchar(50), DATA6 varchar(50)," +
                    " DATA7 varchar(50), DATA8 varchar(50), DATA9 varchar(50), DATA10 varchar(50), DATA11 varchar(50), DATA12 varchar(50)," +
                    " DATA13 varchar(50), DATA14 varchar(50), DATA15 varchar(50), DATA16 varchar(50), DATA17 varchar(50), DATA18 varchar(50)," +
                    " DATA19 varchar(50), DATA20 varchar(50), DATA21 varchar(50), DATA22 varchar(50), DATA23 varchar(50), DATA24 varchar(50)," +
                    " DATA25 varchar(50), DATA26 varchar(50), DATA27 varchar(50), DATA28 varchar(50), DATA29 varchar(50), DATA30 varchar(50)," +
                    " DATA31 varchar(50), DATA32 varchar(50), DATA33 varchar(50), DATA34 varchar(50), DATA35 varchar(50), DATA36 varchar(50)," +
                    " DATA37 varchar(50), DATA38 varchar(50), DATA39 varchar(50), DATA40 varchar(50), DATA41 varchar(50), DATA42 varchar(50)," +
                    " BDJG varchar(50), ZT varchar(50)");
                textBoxUpdate.AppendText("表[设备标定数据]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[设备标定数据]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[车辆检测状态]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("车辆检测状态"))
            {
                textBoxUpdate.AppendText("表[车辆检测状态]不存在\r\n");
                basecontrol.createTalbe("车辆检测状态", "JCZBH varchar(50), LINEID varchar(50), JYLSH varchar(50), JYCS varchar(50), JCSJ datetime," +
                    " CLHP varchar(50), HPZL varchar(50), ZT varchar(50), YCLZT varchar(50), JCFF varchar(50), BZ varchar(50)");
                textBoxUpdate.AppendText("表[车辆检测状态]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[车辆检测状态]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[不透光限值]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("不透光限值"))
            {
                textBoxUpdate.AppendText("表[不透光限值]不存在\r\n");
                basecontrol.createTalbe("不透光限值", "ID int not null primary key, 车辆型号 varchar(255), 车辆类型 varchar(255), 发动机型号 varchar(255)," +
                    " 发动机生产企业 varchar(255), 形式核准值 varchar(50), 限值 varchar(50), 汽车生产企业 varchar(255)");
                textBoxUpdate.AppendText("表[不透光限值]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[不透光限值]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[ASM_GSXZ]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("ASM_GSXZ"))
            {
                textBoxUpdate.AppendText("表[ASM_GSXZ]不存在\r\n");
                basecontrol.createTalbe("ASM_GSXZ", "ID int not null primary key IDENTITY(1, 1), AREA varchar(50), PFBZ varchar(50), MINRM int, MAXRM int, CO25 varchar(50), HC25 varchar(50), NO25 varchar(50), CO40 varchar(50), HC40 varchar(50), NO40 varchar(50)");
                textBoxUpdate.AppendText("表[ASM_GSXZ]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[ASM_GSXZ]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[综检待检车辆]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("综检待检车辆"))
            {
                textBoxUpdate.AppendText("表[综检待检车辆]不存在\r\n");
                basecontrol.createTalbe("综检待检车辆", "ID int not null primary key IDENTITY(1, 1), JYLSH varchar(50), HPZL varchar(50), JCXZ varchar(50), JCXM varchar(50), SFGC varchar(50),"
                    + " SFKC varchar(50), CLLX varchar(50), SYXZ varchar(50), ZZL varchar(50), ZBZL varchar(50), JGL varchar(50), EDGL varchar(50), EDZS varchar(50), EDNJ varchar(50), EDNJZS varchar(50),"
                    + " EDYH varchar(50), PL varchar(50), LTLX varchar(50), LTDMKD varchar(50), QCGD varchar(50), QLJ varchar(50), QCCD varchar(50), KCDJ varchar(50), HCCSXS varchar(50), QDZKZZL varchar(50),"
                    + " QYCMZZL varchar(50), DLXPJBZ varchar(50), CSBXX varchar(50), CSBSX varchar(50), YHXZ varchar(50), YHCS varchar(50), QDXS varchar(50), BSXStr varchar(50), DWCount varchar(50), QGS varchar(50),"
                    + " CH varchar(50), PQHCLZZ varchar(50), DCZZ varchar(50), RLZL varchar(50), GYTypeID varchar(50), GYTypeStr varchar(50), PQGCount varchar(50), ZKRS varchar(50), ZYTypeID varchar(50), ZYTypeStr varchar(50),"
                    + " DW varchar(50), DWTelephone varchar(50), DWAddres varchar(50)");
                textBoxUpdate.AppendText("表[综检待检车辆]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[综检待检车辆]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[综检已检车辆]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("综检已检车辆"))
            {
                textBoxUpdate.AppendText("表[综检已检车辆]不存在\r\n");
                basecontrol.createTalbe("综检已检车辆", "ID int not null primary key IDENTITY(1, 1), JYLSH varchar(50), HPZL varchar(50), JCXZ varchar(50), JCXM varchar(50), SFGC varchar(50),"
                    + " SFKC varchar(50), CLLX varchar(50), SYXZ varchar(50), ZZL varchar(50), ZBZL varchar(50), JGL varchar(50), EDGL varchar(50), EDZS varchar(50), EDNJ varchar(50), EDNJZS varchar(50),"
                    + " EDYH varchar(50), PL varchar(50), LTLX varchar(50), LTDMKD varchar(50), QCGD varchar(50), QLJ varchar(50), QCCD varchar(50), KCDJ varchar(50), HCCSXS varchar(50), QDZKZZL varchar(50),"
                    + " QYCMZZL varchar(50), DLXPJBZ varchar(50), CSBXX varchar(50), CSBSX varchar(50), YHXZ varchar(50), YHCS varchar(50), QDXS varchar(50), BSXStr varchar(50), DWCount varchar(50), QGS varchar(50),"
                    + " CH varchar(50), PQHCLZZ varchar(50), DCZZ varchar(50), RLZL varchar(50), GYTypeID varchar(50), GYTypeStr varchar(50), PQGCount varchar(50), ZKRS varchar(50), ZYTypeID varchar(50), ZYTypeStr varchar(50),"
                    + " DW varchar(50), DWTelephone varchar(50), DWAddres varchar(50), JCSJ datetime");
                textBoxUpdate.AppendText("表[综检已检车辆]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[综检已检车辆]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[综检待检车辆状态]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("综检待检车辆状态"))
            {
                textBoxUpdate.AppendText("表[综检待检车辆状态]不存在\r\n");
                basecontrol.createTalbe("综检待检车辆状态", "ID int not null primary key IDENTITY(1, 1), JYLSH varchar(50), CSZT varchar(50), YHZT varchar(50), DLZT varchar(50)");
                textBoxUpdate.AppendText("表[综检待检车辆状态]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[综检待检车辆状态]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[综检检测结果]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("综检检测结果"))
            {
                textBoxUpdate.AppendText("表[综检检测结果]不存在\r\n");
                basecontrol.createTalbe("综检检测结果", "ID int not null primary key IDENTITY(1, 1), JYLSH varchar(50), CLHP varchar(50), HPZL varchar(50), DLZT varchar(50), JYXM varchar(50)"
                    + ", RESULTXML varchar(MAX), JCSJ datetime");
                textBoxUpdate.AppendText("表[综检检测结果]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[综检检测结果]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[驱动轴重量]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("驱动轴重量"))
            {
                textBoxUpdate.AppendText("表[驱动轴重量]不存在\r\n");
                basecontrol.createTalbe("驱动轴重量", "ID int not null primary key IDENTITY(1, 1), CLHP varchar(50), HPZL varchar(50), QDZKZZL varchar(50), YL varchar(50), JCSJ datetime");
                textBoxUpdate.AppendText("表[驱动轴重量]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[驱动轴重量]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[油耗仪状态]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("油耗仪状态"))
            {
                textBoxUpdate.AppendText("表[油耗仪状态]不存在\r\n");
                basecontrol.createTalbe("油耗仪状态", "ZT varchar(50), LINEID varchar(50), UPDATETIME datetime");
                textBoxUpdate.AppendText("表[油耗仪状态]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[油耗仪状态]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[SpecialVehicleList]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("SpecialVehicleList"))
            {
                textBoxUpdate.AppendText("表[SpecialVehicleList]不存在\r\n");
                basecontrol.createTalbe("SpecialVehicleList", "ID int not null primary key IDENTITY(1, 1), CLPH varchar(50), HPZL varchar(50), CPYS varchar(50), ZT varchar(50), JCJG varchar(50), JCSJ datetime");
                textBoxUpdate.AppendText("表[SpecialVehicleList]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[SpecialVehicleList]已存在\r\n");
            }
            textBoxUpdate.AppendText("检查表[SpecialVehicleXz]是否存在？\r\n");
            if (!basecontrol.checkTableIsExist("SpecialVehicleXz"))
            {
                textBoxUpdate.AppendText("表[SpecialVehicleXz]不存在\r\n");
                basecontrol.createTalbe("SpecialVehicleXz", "ID int not null primary key IDENTITY(1, 1), VMAS_CO varchar(50), VMAS_HC varchar(50), VMAS_NOX varchar(50), VMAS_HCNOX varchar(50)");
                textBoxUpdate.AppendText("表[SpecialVehicleXz]已创建\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("表[SpecialVehicleXz]已存在\r\n");
            }
            listname = "车辆检测状态";
            string[] addkeyarrayTestState = { "JCCZY", "YCY", "DLY" };
            foreach (string keyname in addkeyarrayTestState)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (basecontrol.addkeyInList(listname, keyname))
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                    else
                        textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                }
            }
            if (basecontrol.setKeyIsIdentity(listname))
            {
                textBoxUpdate.AppendText("List:" + listname + "添加identity ID成功\r\n");
            }
            else
            {
                textBoxUpdate.AppendText("List:" + listname + "添加identity ID失败\r\n");
            }
            listname = "ASM_DATASECONDS";
            string[] addkeyarrayASMD = { "MMZSGL", "MMNL", "MMZT", "JYLSH", "JYCS", "CYDS" };
            foreach (string keyname in addkeyarrayASMD)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (keyname.Contains("MM"))
                    {
                        if (basecontrol.addkeyVacharMaxInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                    else
                    {
                        if (basecontrol.addkeyInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                }
            }
            listname = "VMAS_DATASECONDS";
            string[] addkeyarrayVMASD = { "MMZS", "MMZSGL", "MMJZGL", "MMCO2ZL", "JYLSH", "JYCS", "CYDS" };
            foreach (string keyname in addkeyarrayVMASD)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (keyname.Contains("MM"))
                    {
                        if (basecontrol.addkeyVacharMaxInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                    else
                    {
                        if (basecontrol.addkeyInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                }
            }
            listname = "SDS_DATASECONDS";
            string[] addkeyarraySDSD = { "JYLSH", "JYCS", "CYDS" };
            foreach (string keyname in addkeyarraySDSD)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (keyname.Contains("MM"))
                    {
                        if (basecontrol.addkeyVacharMaxInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                    else
                    {
                        if (basecontrol.addkeyInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                }
            }
            listname = "JZJS_DATASECONDS";
            string[] addkeyarrayJZJSD = { "JYLSH", "JYCS", "CYDS", "MMZS", "MMZGL", "MMZSGL", "MMGLXZXS", "MMJSGL", "MMBTGD", "MMDQYL", "MMXDSD", "MMHJWD", "MMNL", "MMNO" };
            foreach (string keyname in addkeyarrayJZJSD)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (keyname.Contains("MM"))
                    {
                        if (basecontrol.addkeyVacharMaxInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                    else
                    {
                        if (basecontrol.addkeyInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                }
            }
            listname = "ZYJS_DATASECONDS";
            string[] addkeyarrayZYJSD = { "JYLSH", "JYCS", "CYDS" };
            foreach (string keyname in addkeyarrayZYJSD)
            {
                if (basecontrol.testKeyIsExist(listname, keyname))
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 已存在\r\n");
                }
                else
                {
                    textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 不存在\r\n");
                    if (keyname.Contains("MM"))
                    {
                        if (basecontrol.addkeyVacharMaxInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                    else
                    {
                        if (basecontrol.addkeyInList(listname, keyname))
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加成功\r\n");
                        else
                            textBoxUpdate.AppendText("List:" + listname + " Key:" + keyname + " 添加失败\r\n");
                    }
                }
            }

            basecontrol.UpdateSystemVersion(thissystemversion);

            textBoxUpdate.AppendText("升级完毕\r\n");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请注意该操作不可恢复，是否确定要将业务流水号清零？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                mainPanel.stationcontrol.setStationLshCount(mainPanel.stationid, "0");
                MessageBox.Show("业务流水号已清零.", "系统提示");
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请注意该操作不可恢复，是否确定要将登记流水号清零？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                mainPanel.stationcontrol.setStationLshCount(mainPanel.stationid, "0");
                MessageBox.Show("业务流水号已清零.", "系统提示");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainPanel.netMode = comboBoxNetMode.Text;
            mainPanel.isQueryInfFromGA = checkBoxQueryDataFromGA.Checked;
            ini.INIIO.WritePrivateProfileString("工作模式", "联网模式", comboBoxNetMode.Text, @".\appConfig.ini");
            ini.INIIO.WritePrivateProfileString("工作模式", "读取公安信息", mainPanel.isQueryInfFromGA?"Y":"N", @".\appConfig.ini");
        }
    }
    public class LocalPrinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();
        /// <summary>
        /// 获取本机默认打印机名称
        /// </summary>
        public static String DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }
        /// <summary>
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。
        /// </summary>
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<string>();
            fPrinters.Add(DefaultPrinter); // 默认打印机始终出现在列表的第一项
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                    fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }
    }
}
