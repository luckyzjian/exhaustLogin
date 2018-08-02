using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using SYS_DAL;
using SYS_MODEL;
using SYS.Model;
using Microsoft.Reporting.WinForms;
using System.Collections;
using JxWebClient;
using ini;

namespace exhaustLogin
{
    public partial class carLoginNew : Form
    {
        private loginInfModel logininfmodel = new loginInfModel();
        private loginInfControl logininfcontrol = new loginInfControl();
        private stationControl stationcontrol = new stationControl();
        private bool isCarLoginOkOnNet = false;
        public struct carAtWaitInf
        {
            public string plate;
            public string loginTime;
            public string jccs;
        };
        private List<carAtWaitInf> carAtWaitlist = new List<carAtWaitInf>();
        List<string> asmNotNeedControl;// = { "comboBoxDLSP", "textBoxDWS", "textBoxQGS", "comboBoxOBD", "comboBoxSRL", "textBoxEDZS", "textBoxRYPH", "comboBoxDKGY", "textBoxQDLTQY", "textBoxSCS", "textBoxFDJSCQY" };
        //private List<Stream> m_streams;
        //用来记录当前打印到第几页了 
        //private int m_currentPageIndex;

        /// <summary>
        /// 用来记录当前打印到第几页了
        /// </summary>
        private int m_currentPageIndex;

        /// <summary>
        /// 声明一个Stream对象的列表用来保存报表的输出数据,LocalReport对象的Render方法会将报表按页输出为多个Stream对象。
        /// </summary>
        private IList<Stream> m_streams;

        private bool isLandSapces = false;

        public carLoginNew()
        {
            InitializeComponent();
        }
        private void init_notNotNeededControl()
        {
            // "comboBoxDLSP", "textBoxDWS", "textBoxQGS", "comboBoxOBD", "comboBoxSRL", "textBoxEDZS", "textBoxRYPH", "comboBoxDKGY", "textBoxQDLTQY", "textBoxSCS", "textBoxFDJSCQY"
            asmNotNeedControl.Add( "comboBoxDLSP");

        }
        private void carLogin_Load(object sender, EventArgs e)
        {
            this.Font = new Font("宋体", (float)mainPanel.fontSize);
            init_loginDefault();
            init_waitList();
            //init_safewaitList();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            buttonSave.Enabled = false;
            buttonClear.Enabled = false;

        }
        private bool isUseINI = false;
        private void init_loginDefault()
        {
            if (File.Exists(@".\登录默认.ini") && (mainPanel.loginmode == mainPanel.LoginDefaultMode.DEFAULT_INI))
            {
                isUseINI = true;
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                ini.INIIO.GetPrivateProfileString("默认值", "车辆号牌", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.CLHP = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "车牌颜色", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.CPYS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "核定载客", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.HDZK = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "车辆类型", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.CLLX = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "使用性质", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.SYXZ = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "号牌种类", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.hpzl = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "车辆种类", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.clzl = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "所属辖区", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.ssxq = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "驱动形式", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.QDXS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "燃料种类", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.RLZL = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "进气方式", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.JQFS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "供油方式", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.GYFS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "侧滑装置", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.CHZZ = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "净化装置", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.JHZZ = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "变速器形式", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.BSQXS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "电控高压油泵", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.DKGYYB = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "是否外地转入", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.sfwdzs = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "是否延期报废", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.sfyqbf = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "发动机排量", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.FDJPL = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "电喷形式", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.DPFS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "档位数", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.DWS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "气缸数", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.QGS = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "是否OBD", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.OBD = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "是否双燃料", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.SFSRL = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "独立双排", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.DLSP = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "汽油燃油牌号", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.qyryph = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "柴油燃料牌号", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.cyryph = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "驱动轮胎气压", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.qdltqy = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "排放标准", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.pfbz = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "检测类别", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.jclb = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "检测方式", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.jcfs = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "驾驶室载客", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.JSSZK = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "联系电话", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.LXDH = temp.ToString().Trim();
                ini.INIIO.GetPrivateProfileString("默认值", "行驶里程", "", temp, 2048, @".\登录默认.ini");
                logininfmodel.xslc = temp.ToString().Trim();
            }
            else
            {
                logininfmodel = logininfcontrol.getLoginDefaultInf();
            }
            if(mainPanel.isNetUsed&&mainPanel.netMode==mainPanel.NHNETMODE)
            {
                panelExNHINF.Visible = true;
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                IDictionaryEnumerator myEnumerator = mainPanel.nhinterface.NH_PlateType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCPYS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("号牌种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxHPZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.nhinterface.NH_VehicleType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCLLX.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_UseCharacter.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSYXZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_FuelType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxRLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_GearBoxType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxBSQXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_OilSupplyMode.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxGYFS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("电喷方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDPXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.nhinterface.NH_AirIntakeMode.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJQFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_DriveType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxQDXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_TCS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCHZZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_TestCharacter.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJCLB.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("车辆种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCLZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否外地转入");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFWDZR.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否延期报废");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFYQBF.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.nhinterface.NH_HasCatalyticConverter.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJHZZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.nhinterface.NH_DoubleFuel.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSRL.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否选择");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDLSP.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxOBD.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxDKGY.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                comboBoxPFBZ.Items.Add("国〇");
                comboBoxPFBZ.Items.Add("国Ⅰ");
                comboBoxPFBZ.Items.Add("国Ⅱ");
                comboBoxPFBZ.Items.Add("国Ⅲ");
                comboBoxPFBZ.Items.Add("国Ⅳ");
                comboBoxPFBZ.Items.Add("国Ⅴ");
                comboBoxPFBZ.Items.Add("新能源");
            }
            else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.JXNETMODE)
            {
                panelExNHINF.Visible = false;
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                //comboBoxCLPH.Text = logininfmodel.CLHP;
                //textBoxPlateAtWait.Text = logininfmodel.CLHP;
                IDictionaryEnumerator myEnumerator = mainPanel.jxinterface.JX_VEHICLELICENSETYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCPYS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("号牌种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxHPZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                //comboBoxCPYS.Text = logininfmodel.CPYS;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆类型");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Display"].ToString() == "Y")
                        comboBoxCLLX.Items.Add(dt.Rows[i]["ID"].ToString() + "_" + dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.jxinterface.JX_USETYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSYXZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.jxinterface.JX_FUELTYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxRLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.jxinterface.JX_GEARTYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxBSQXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.jxinterface.JX_FUELSUPPLY.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxGYFS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("电喷方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDPXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.jxinterface.JX_AIRINTYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJQFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.jxinterface.JX_DRIVEMODE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxQDXS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("侧滑装置");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.jxinterface.JX_TESTCATEGORY.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJCLB.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("车辆种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCLZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否外地转入");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFWDZR.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否延期报废");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFYQBF.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.jxinterface.JX_HCLTYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJHZZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.jxinterface.JX_FLAGDK.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxDKGY.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.jxinterface.JX_FLAGOBD.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxOBD.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否选择");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDLSP.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxSRL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.jxinterface.JX_EMISSIONSTANDARD.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxPFBZ.Items.Add((string)(myEnumerator.Value));
                }
            }
            else if(mainPanel.isNetUsed&&mainPanel.netMode==mainPanel.DALINETMODE)
            {
                panelExNHINF.Visible = false;
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                //comboBoxCLPH.Text = logininfmodel.CLHP;
                //textBoxPlateAtWait.Text = logininfmodel.CLHP;
                IDictionaryEnumerator myEnumerator = mainPanel.daliinterface.DL_CPYS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCPYS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("号牌种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxHPZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                //comboBoxCPYS.Text = logininfmodel.CPYS;
                myEnumerator = mainPanel.daliinterface.DL_CLLX.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCLLX.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_SYXZ.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSYXZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_RLZL.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxRLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_BSQXS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxBSQXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_GYFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxGYFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_DPFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxDPXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_JQFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJQFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_QDFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxQDXS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("侧滑装置");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.daliinterface.DL_JCLB.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJCLB.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.daliinterface.DL_CLZL.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_ZRBZ.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSFWDZR.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_YQBF.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSFYQBF.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.daliinterface.DL_JHZZ.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJHZZ.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否选择");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDLSP.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxSRL.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxOBD.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxDKGY.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                comboBoxPFBZ.Items.Add("国0");
                comboBoxPFBZ.Items.Add("国I");
                comboBoxPFBZ.Items.Add("国II");
                comboBoxPFBZ.Items.Add("国III");
                comboBoxPFBZ.Items.Add("国IV");
                comboBoxPFBZ.Items.Add("国V");
            }
            else if(mainPanel.isNetUsed&&mainPanel.netMode==mainPanel.ORTNETMODE)
            {
                panelExNHINF.Visible = false;
                labelX29.Text = "是否进入城镇建成区";
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                //comboBoxCLPH.Text = logininfmodel.CLHP;
                //textBoxPlateAtWait.Text = logininfmodel.CLHP;
                IDictionaryEnumerator myEnumerator = mainPanel.ortsocket.ORT_LicenseClass.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCPYS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("号牌种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxHPZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                //comboBoxCPYS.Text = logininfmodel.CPYS;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆类型");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Display"].ToString() == "Y")
                        comboBoxCLLX.Items.Add(dt.Rows[i]["ID"].ToString() + "_" + dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ortsocket.ORT_UseType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSYXZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ortsocket.ORT_Fuel.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxRLZL.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("变速器形式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxBSQXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ortsocket.ORT_Cleaners.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxGYFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ortsocket.ORT_IsClosedLoopEFI.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxDPXS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("进气方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJQFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ortsocket.ORT_DriveType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxQDXS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("侧滑装置");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测类别");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCLB.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("车辆种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCLZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ortsocket.ORT_IfGoIntoCity.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSFWDZR.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否延期报废");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFYQBF.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ortsocket.ORT_Is3WCC.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJHZZ.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否选择");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDLSP.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxSRL.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxOBD.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxDKGY.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                comboBoxPFBZ.Items.Add("国0");
                comboBoxPFBZ.Items.Add("国I");
                comboBoxPFBZ.Items.Add("国II");
                comboBoxPFBZ.Items.Add("国III");
                comboBoxPFBZ.Items.Add("国IV");
                comboBoxPFBZ.Items.Add("国V");
            }
            else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.EZNETMODE)
            {
                panelExNHINF.Visible = false;
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                IDictionaryEnumerator myEnumerator = mainPanel.ezinterface.EZ_licensePlateColor.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCPYS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_licensePlateCategory.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxHPZL.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("车辆类型");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Display"].ToString() == "Y")
                        comboBoxCLLX.Items.Add(dt.Rows[i]["ID"].ToString() + "_" + dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ezinterface.EZ_useProperties.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSYXZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_fueltype.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxRLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_transmissionType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxBSQXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_fuelSupply.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxGYFS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("电喷方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDPXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ezinterface.EZ_intakeMode.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJQFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_driveMode.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxQDXS.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("侧滑装置");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测类别");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCLB.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("车辆种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCLZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否外地转入");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFWDZR.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否延期报废");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFYQBF.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.ezinterface.EZ_afterTreatmentType.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJHZZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_yn.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSRL.Items.Add((string)(myEnumerator.Value));
                    comboBoxDLSP.Items.Add((string)(myEnumerator.Value));
                    comboBoxOBD.Items.Add((string)(myEnumerator.Value));
                    comboBoxDKGY.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.ezinterface.EZ_emissionStandard.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxPFBZ.Items.Add((string)(myEnumerator.Value));
                }
            }
            else
            {
                panelExNHINF.Visible = false;
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                //comboBoxCLPH.Text = logininfmodel.CLHP;
                //textBoxPlateAtWait.Text = logininfmodel.CLHP;
                dt = logininfcontrol.getComBoBoxItemsInf("车牌颜色");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCPYS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("号牌种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxHPZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                //comboBoxCPYS.Text = logininfmodel.CPYS;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆类型");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Display"].ToString() == "Y")
                        comboBoxCLLX.Items.Add(dt.Rows[i]["ID"].ToString() + "_" + dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("使用性质");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSYXZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("燃料种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxRLZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("变速器形式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxBSQXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("供油方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxGYFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("电喷方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDPXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("进气方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJQFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("驱动形式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxQDXS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("侧滑装置");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测类别");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCLB.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("车辆种类");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxCLZL.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否外地转入");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFWDZR.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否延期报废");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxSFYQBF.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("催化转化器");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                dt = logininfcontrol.getComBoBoxItemsInf("是否选择");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxDLSP.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxSRL.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxOBD.Items.Add(dt.Rows[i]["MC"].ToString());
                    comboBoxDKGY.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                comboBoxPFBZ.Items.Add("国0");
                comboBoxPFBZ.Items.Add("国I");
                comboBoxPFBZ.Items.Add("国II");
                comboBoxPFBZ.Items.Add("国III");
                comboBoxPFBZ.Items.Add("国IV");
                comboBoxPFBZ.Items.Add("国V");
            }
            clear_loginInf(false);
            //comboBoxCPYS.Text = logininfmodel.CPYS;
            
        }
        
        private void init_waitList()
        {
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.NHNETMODE)
            {
                /*listBoxCarAtWait.Items.Clear();
                DataTable waitTable = null;
                if (mainPanel.isNetUsed)
                {
                    waitTable = logininfcontrol.getAllCarAtWait("Y");
                }
                else
                {
                    waitTable = logininfcontrol.getAllCarAtWait("N");
                }
                for (int i = 0; i < waitTable.Rows.Count; i++)
                {
                    listBoxCarAtWait.Items.Add(waitTable.Rows[i]["CLHP"].ToString()+ " "+waitTable.Rows[i]["CLID"].ToString());
                }
                listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;*/

                listBoxCarAtWait.Items.Clear();
                DataTable waitTable = null;
                int nhcode, nhexpcode;
                string nhmsg, nhexpmsg;
                DataTable vehicleList = mainPanel.nhinterface.GetVehicleList(out nhcode, out nhmsg, out nhexpcode, out nhexpmsg);
                if (nhcode == 0 && nhexpcode == 0)
                {
                    if (vehicleList != null)
                    {
                        for (int i = 0; i < vehicleList.Rows.Count; i++)
                        {
                            listBoxCarAtWait.Items.Add(vehicleList.Rows[i]["PlateNumber"] + " " + vehicleList.Rows[i]["TestRunningNumber"]);
                        }
                        listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                    }
                }
            }
            else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.JXNETMODE)
            {
                #region 江西
                string code, msg;
                listBoxCarAtWait.Items.Clear();
                List<jxcarinf> jxcarlist = new List<jxcarinf>();
                if (mainPanel.jxinterface.fetchVehicles(out code, out msg, out jxcarlist))
                {
                    DataRow dr = null;
                    if (jxcarlist.Count >= 0)
                    {
                        foreach (jxcarinf jxcarchild in jxcarlist)
                        {
                            listBoxCarAtWait.Items.Add(jxcarchild.vehicleLicense + " " + jxcarchild.detectionId);
                        }
                    }
                }
                #endregion
            }
            else
            {
                listBoxCarAtWait.Items.Clear();
                DataTable waitTable = null;
                if (mainPanel.isNetUsed)
                {
                    waitTable = logininfcontrol.getAllCarAtWait("Y");
                }
                else
                {
                    waitTable = logininfcontrol.getAllCarAtWait("N");
                }
                for (int i = 0; i < waitTable.Rows.Count; i++)
                {
                    listBoxCarAtWait.Items.Add(waitTable.Rows[i]["CLHP"]);
                }
                listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
            }
        }

        /*private void init_safewaitList()
        {
            listBoxCarSafeWait.Items.Clear();
            DataTable waitTable = null;
            waitTable = logininfcontrol.getallCarSafeAtWait();
            for (int i = 0; i < waitTable.Rows.Count; i++)
            {
                listBoxCarSafeWait.Items.Add(waitTable.Rows[i]["CLHP"]);
            }
            listBoxCarSafeWait.SelectedIndex = listBoxCarSafeWait.Items.Count - 1;
        }*/

        private void comboBoxCLPH_Validated(object sender, EventArgs e)
        {
            int jccs=0;
            comboBoxCLPH.Text = comboBoxCLPH.Text.ToUpper();
            bool isnhcarinfexist = false;
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.NHNETMODE)
            {
                #region nh
                int nhcode, nhexpcode;
                string nhmsg, nhexpmsg;
                string platenumber = comboBoxCLPH.Text;
                string platetype =  comboBoxCPYS.Text;
                string vin = "";
                //CARATWAIT carwait = logininfcontrol.getCarInfatWaitList(dataGrid_waitcar.SelectedRows[0].Cells["车牌号"].Value.ToString());
                NhWebClient.VehicleInfo vehicleinfo = mainPanel.nhinterface.GetVehicleInfo(platenumber, platetype, vin, out nhcode, out nhmsg, out nhexpcode, out nhexpmsg);

                if (nhcode == 0 && nhexpcode == 0)
                {
                    if (vehicleinfo != null)
                    {
                        labelClhp.Text = "(平台数据)";
                        DateTime a, b;
                        CARINF model = new CARINF();
                        model.CLHP = vehicleinfo.PlateNumber;
                        model.ZCRQ = vehicleinfo.RegistrationDate;
                        model.CLSBM = vehicleinfo.VIN;

                        model.CPYS = vehicleinfo.PlateType;
                        model.CLLX = vehicleinfo.VehicleType;

                        model.CZ = vehicleinfo.Owner;
                        model.SYXZ = vehicleinfo.UseCharacter;
                        model.PP = vehicleinfo.Brand;//5
                        model.XH = vehicleinfo.Model;
                        model.FDJHM = vehicleinfo.EngineNumber;
                        model.FDJXH = vehicleinfo.EngineModel;
                        model.SCQY = vehicleinfo.Manufacturer;//10
                        model.HDZK = vehicleinfo.RatedPassengerCapacity;
                        model.JSSZK = "0";
                        model.ZZL = vehicleinfo.MaximumTotalMass;
                        model.HDZZL = vehicleinfo.RatedLoadingMass;
                        model.ZBZL = vehicleinfo.UnladenMass;//15
                        model.JZZL = (int.Parse(model.ZBZL) + 100).ToString();

                        model.SCRQ = vehicleinfo.ProductionDate;
                        model.FDJPL = vehicleinfo.Displacement;
                        model.RLZL = vehicleinfo.FuelType;
                        if (vehicleinfo.RatedPower != "")
                        {
                            model.EDGL =((int)(double.Parse( vehicleinfo.RatedPower))).ToString();
                        }
                        else
                        { model.EDGL = ""; }
                        if (vehicleinfo.RatedRev != "")
                        {
                            model.EDZS = ((int)(double.Parse(vehicleinfo.RatedRev))).ToString();
                        }
                        else
                        { model.EDZS = ""; }
                        model.BSQXS = vehicleinfo.GearBoxType;
                        model.DWS = vehicleinfo.GearNumber;
                        model.GYFS = vehicleinfo.OilSupplyMode;
                        model.DPFS = vehicleinfo.OilSupplyMode;
                        model.JQFS = vehicleinfo.AirIntakeMode;
                        model.QGS = vehicleinfo.CylinderNumber;
                        model.QDXS = vehicleinfo.DriverType;
                        model.CHZZ = vehicleinfo.TCS;
                        model.DLSP = "";
                        model.SFSRL = vehicleinfo.FuelMark;
                        model.JHZZ = (vehicleinfo.HasCatalyticConverter == "0" ? "否" : "是");

                        model.OBD = "";
                        model.DKGYYB = "";
                        model.LXDH = vehicleinfo.Phone;
                        model.CZDZ = vehicleinfo.Address;
                        model.JCFS = "";
                        model.JCLB = "";
                        model.CLZL = "";
                        model.SSXQ = "";
                        model.SFWDZR = "";
                        model.SFYQBF = "";
                        model.DKGYYB = "";
                        model.FDJSCQY = vehicleinfo.EngineManufacturer;
                        model.QDLTQY = vehicleinfo.TyrePressure;
                        model.RYPH = vehicleinfo.FuelMark;

                        comboBoxCPYS.Text = model.CPYS;
                        comboBoxCLLX.Text = model.CLLX;
                        textBoxCZ.Text = model.CZ;
                        comboBoxSYXZ.Text = model.SYXZ;
                        textBoxPP.Text = model.PP;
                        comboBoxXH.Text = model.XH;
                        textBoxDPHM.Text = model.CLSBM;
                        textBoxFDJH.Text = model.FDJHM;
                        textBoxFDJXH.Text = model.FDJXH;
                        textBoxSCS.Text = model.SCQY;
                        textBoxHDZK.Text = model.HDZK;
                        textBoxJSSZK.Text = model.JSSZK;
                        textBoxZZL.Text = model.ZZL;
                        textBoxHDZZL.Text = model.HDZZL;
                        textBoxZBZL.Text = model.ZBZL;
                        textBoxJZZL.Text = model.JZZL;
                        dateZCRQ.Value = model.ZCRQ;
                        dateSCRQ.Value = model.SCRQ;
                        textBoxFDJPL.Text = model.FDJPL;
                        comboBoxRLZL.Text = model.RLZL;
                        textBoxEDGL.Text = model.EDGL;
                        textBoxEDZS.Text = model.EDZS;
                        comboBoxBSQXS.Text = model.BSQXS;
                        textBoxDWS.Text = model.DWS;
                        comboBoxGYFS.Text = model.GYFS;
                        comboBoxDPXS.Text = model.DPFS;
                        comboBoxJQFS.Text = model.JQFS;
                        textBoxQGS.Text = model.QGS;
                        comboBoxQDXS.Text = model.QDXS;
                        comboBoxCHZZ.Text = model.CHZZ;
                        comboBoxDLSP.Text = model.DLSP;
                        comboBoxSRL.Text = model.SFSRL;
                        comboBoxJHZZ.Text = model.JHZZ;
                        comboBoxOBD.Text = model.OBD;
                        comboBoxDKGY.Text = model.DKGYYB;
                        textBoxLXDH.Text = model.LXDH;
                        textBoxCZDZ.Text = model.CZDZ;
                        comboBoxJCFS.Text = model.JCFS;
                        comboBoxJCLB.Text = model.JCLB;
                        comboBoxCLZL.Text = model.CLZL;
                        textBoxSSXQ.Text = model.SSXQ;
                        comboBoxSFWDZR.Text = model.SFWDZR;
                        comboBoxSFYQBF.Text = model.SFYQBF;
                        textBoxFDJSCQY.Text = model.FDJSCQY;
                        textBoxQDLTQY.Text = model.QDLTQY;
                        textBoxRYPH.Text = model.RYPH;
                        isnhcarinfexist = true;
                    }
                }
                #endregion
            }
            if (!isnhcarinfexist)
            {
                if (!logininfcontrol.checkCarInfIsAlive(comboBoxCLPH.Text, comboBoxCPYS.Text))
                {
                    labelClhp.Text = "(未找到信息)";
                    clear_loginInf(true);
                }
                else
                {
                    labelClhp.Text = "(本地数据)";
                    CARINF model = new CARINF();
                    model = logininfcontrol.getCarInfbyPlate(comboBoxCLPH.Text, comboBoxCPYS.Text);
                    if (model.CLHP != "-2")
                    {
                        //comboBoxCLPH.Text = model.CLHP;
                        if (mainPanel.isNetUsed && (mainPanel.netMode == mainPanel.NHNETMODE||mainPanel.netMode==mainPanel.JXNETMODE||mainPanel.netMode==mainPanel.DALINETMODE||mainPanel.netMode==mainPanel.ORTNETMODE || mainPanel.netMode == mainPanel.EZNETMODE))
                        {
                            comboBoxCPYS.Text = model.CPYS;
                            comboBoxHPZL.Text = model.HPZL;
                            comboBoxCLLX.Text = model.CLLX;
                            textBoxCZ.Text = model.CZ;
                            comboBoxSYXZ.Text = model.SYXZ;
                            textBoxPP.Text = model.PP;
                            comboBoxXH.Text = model.XH;
                            textBoxDPHM.Text = model.CLSBM;
                            textBoxFDJH.Text = model.FDJHM;
                            textBoxFDJXH.Text = model.FDJXH;
                            textBoxSCS.Text = model.SCQY;
                            textBoxHDZK.Text = model.HDZK;
                            textBoxJSSZK.Text = model.JSSZK;
                            textBoxZZL.Text = model.ZZL;
                            textBoxHDZZL.Text = model.HDZZL;
                            textBoxZBZL.Text = model.ZBZL;
                            textBoxJZZL.Text = model.JZZL;
                            dateZCRQ.Value = model.ZCRQ;
                            dateSCRQ.Value = model.SCRQ;
                            textBoxFDJPL.Text = model.FDJPL;
                            comboBoxRLZL.Text = model.RLZL;
                            textBoxEDGL.Text = model.EDGL;
                            textBoxEDZS.Text = model.EDZS;
                            comboBoxBSQXS.Text = model.BSQXS;
                            textBoxDWS.Text = model.DWS;
                            comboBoxGYFS.Text = model.GYFS;
                            comboBoxDPXS.Text = model.DPFS;
                            comboBoxJQFS.Text = model.JQFS;
                            textBoxQGS.Text = model.QGS;
                            comboBoxQDXS.Text = model.QDXS;
                            comboBoxCHZZ.Text = model.CHZZ;
                            comboBoxDLSP.Text = model.DLSP;
                            comboBoxSRL.Text = model.SFSRL;
                            comboBoxJHZZ.Text = model.JHZZ;
                            comboBoxOBD.Text = model.OBD;
                            comboBoxDKGY.Text = model.DKGYYB;
                            textBoxLXDH.Text = model.LXDH;
                            textBoxCZDZ.Text = model.CZDZ;
                            comboBoxJCFS.Text = model.JCFS;
                            comboBoxJCLB.Text = model.JCLB;
                            comboBoxCLZL.Text = model.CLZL;
                            textBoxSSXQ.Text = model.SSXQ;
                            comboBoxSFWDZR.Text = model.SFWDZR;
                            comboBoxSFYQBF.Text = model.SFYQBF;
                            textBoxFDJSCQY.Text = model.FDJSCQY;
                            textBoxQDLTQY.Text = model.QDLTQY;
                            textBoxRYPH.Text = model.RYPH;
                            comboBoxPFBZ.Text = model.ZXBZ;
                        }
                        else
                        {
                            comboBoxCPYS.Text = model.CPYS;
                            comboBoxHPZL.Text = model.HPZL;
                            comboBoxCLLX.Text = model.CLLX + "_" + logininfcontrol.getComBoBoxItemsNAME("车辆类型", model.CLLX);
                            textBoxCZ.Text = model.CZ;
                            comboBoxSYXZ.Text = logininfcontrol.getComBoBoxItemsNAME("使用性质", model.SYXZ);
                            textBoxPP.Text = model.PP;
                            comboBoxXH.Text = model.XH;
                            textBoxDPHM.Text = model.CLSBM;
                            textBoxFDJH.Text = model.FDJHM;
                            textBoxFDJXH.Text = model.FDJXH;
                            textBoxSCS.Text = model.SCQY;
                            textBoxHDZK.Text = model.HDZK;
                            textBoxJSSZK.Text = model.JSSZK;
                            textBoxZZL.Text = model.ZZL;
                            textBoxHDZZL.Text = model.HDZZL;
                            textBoxZBZL.Text = model.ZBZL;
                            textBoxJZZL.Text = model.JZZL;
                            dateZCRQ.Value = model.ZCRQ;
                            dateSCRQ.Value = model.SCRQ;
                            textBoxFDJPL.Text = model.FDJPL;
                            comboBoxRLZL.Text = logininfcontrol.getComBoBoxItemsNAME("燃料种类", model.RLZL);
                            textBoxEDGL.Text = model.EDGL;
                            textBoxEDZS.Text = model.EDZS;
                            comboBoxBSQXS.Text = logininfcontrol.getComBoBoxItemsNAME("变速器形式", model.BSQXS);
                            textBoxDWS.Text = model.DWS;
                            comboBoxGYFS.Text = logininfcontrol.getComBoBoxItemsNAME("供油方式", model.GYFS);
                            comboBoxDPXS.Text = logininfcontrol.getComBoBoxItemsNAME("电喷方式", model.DPFS);
                            comboBoxJQFS.Text = logininfcontrol.getComBoBoxItemsNAME("进气方式", model.JQFS);
                            textBoxQGS.Text = model.QGS;
                            comboBoxQDXS.Text = logininfcontrol.getComBoBoxItemsNAME("驱动形式", model.QDXS);
                            comboBoxCHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("侧滑装置", model.CHZZ);
                            comboBoxDLSP.Text = model.DLSP;
                            comboBoxSRL.Text = model.SFSRL;
                            comboBoxJHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("催化转化器", model.JHZZ);
                            comboBoxOBD.Text = model.OBD;
                            comboBoxDKGY.Text = model.DKGYYB;
                            textBoxLXDH.Text = model.LXDH;
                            textBoxCZDZ.Text = model.CZDZ;
                            comboBoxJCFS.Text = logininfcontrol.getComBoBoxItemsNAME("检测方式", model.JCFS);
                            comboBoxJCLB.Text = logininfcontrol.getComBoBoxItemsNAME("检测类别", model.JCLB);
                            comboBoxCLZL.Text = logininfcontrol.getComBoBoxItemsNAME("车辆种类", model.CLZL);
                            textBoxSSXQ.Text = model.SSXQ;
                            comboBoxSFWDZR.Text = logininfcontrol.getComBoBoxItemsNAME("是否外地转入", model.SFWDZR);
                            comboBoxSFYQBF.Text = logininfcontrol.getComBoBoxItemsNAME("是否延期报废", model.SFYQBF);
                            textBoxFDJSCQY.Text = model.FDJSCQY;
                            textBoxQDLTQY.Text = model.QDLTQY;
                            textBoxRYPH.Text = model.RYPH;
                            comboBoxPFBZ.Text = model.ZXBZ;
                            comboBoxExCCS.Text = model.CCS;
                        }
                    }
                }
            }
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            buttonSave.Enabled = false;
            buttonClear.Enabled = false;
            checkBoxXABS.Checked = false;
            checkBoxXESP.Checked = false;
            checkBoxXESC.Checked = false;
            checkBoxXASR.Checked = false;
            checkBoxXCZ.Checked = false;
            checkBoxXXS.Checked = false;
            checkBoxXSQ.Checked = false;
        }

        private void clear_loginInf(bool isSearchVehicle)
        {
            if (isUseINI)
            {
                if (!isSearchVehicle)
                {
                    comboBoxCLPH.Text = logininfmodel.CLHP;
                    comboBoxCPYS.Text = logininfmodel.CPYS;
                }
                comboBoxCLLX.Text = logininfmodel.CLLX;
                textBoxCZ.Text = logininfmodel.CZ;
                comboBoxSYXZ.Text = logininfmodel.SYXZ;
                textBoxPP.Text = logininfmodel.PP;
                comboBoxXH.Text = logininfmodel.XH;
                textBoxDPHM.Text = logininfmodel.CLSBM;
                textBoxFDJH.Text = logininfmodel.FDJHM;
                textBoxFDJXH.Text = logininfmodel.FDJXH;
                textBoxSCS.Text = logininfmodel.SCQY;
                textBoxHDZK.Text = logininfmodel.HDZK;
                textBoxJSSZK.Text = logininfmodel.JSSZK;
                textBoxZZL.Text = logininfmodel.ZZL;
                textBoxHDZZL.Text = logininfmodel.HDZZL;
                textBoxZBZL.Text = logininfmodel.ZBZL;
                textBoxJZZL.Text = logininfmodel.JZZL;
                dateZCRQ.Value = logininfmodel.ZCRQ;
                dateSCRQ.Value = logininfmodel.SCRQ;
                textBoxFDJPL.Text = logininfmodel.FDJPL;
                comboBoxRLZL.Text = logininfmodel.RLZL;
                textBoxEDGL.Text = logininfmodel.EDGL;
                textBoxEDZS.Text = logininfmodel.EDZS;
                comboBoxBSQXS.Text = logininfmodel.BSQXS;
                textBoxDWS.Text = logininfmodel.DWS;
                comboBoxGYFS.Text = logininfmodel.GYFS;
                comboBoxDPXS.Text = logininfmodel.DPFS;
                comboBoxJQFS.Text = logininfmodel.JQFS;
                textBoxQGS.Text = logininfmodel.QGS;
                comboBoxQDXS.Text = logininfmodel.QDXS;
                comboBoxCHZZ.Text = logininfmodel.CHZZ;
                comboBoxDLSP.Text = logininfmodel.DLSP;
                comboBoxSRL.Text = logininfmodel.SFSRL;
                comboBoxJHZZ.Text = logininfmodel.JHZZ;
                comboBoxOBD.Text = logininfmodel.OBD;
                comboBoxDKGY.Text = logininfmodel.DKGYYB;
                textBoxLXDH.Text = logininfmodel.LXDH;
                textBoxQDLTQY.Text = logininfmodel.qdltqy;
                comboBoxPFBZ.Text = logininfmodel.pfbz;
                comboBoxJCLB.Text = logininfmodel.jclb;
                comboBoxJCFS.Text = logininfmodel.jcfs;
                comboBoxHPZL.Text = logininfmodel.hpzl;
                comboBoxCLZL.Text = logininfmodel.clzl;
                textBoxSSXQ.Text = logininfmodel.ssxq;
                comboBoxSFWDZR.Text = logininfmodel.sfwdzs;
                comboBoxSFYQBF.Text = logininfmodel.sfyqbf;
                textBoxXSLC.Text = logininfmodel.xslc;
                textBoxLXDH.Text = logininfmodel.LXDH;
                textBoxJCCS.Text = "1";
                comboBoxExCCS.Text = "2";
            }
            else
            {
                if (!isSearchVehicle)
                {
                    comboBoxCLPH.Text = logininfmodel.CLHP;
                    comboBoxCPYS.SelectedIndex = 0;
                }
                comboBoxCLLX.SelectedIndex = 0;
                textBoxCZ.Text = "";
                comboBoxSYXZ.SelectedIndex = 0;
                textBoxPP.Text = "";
                comboBoxXH.Text = "";
                textBoxDPHM.Text = "";
                textBoxFDJH.Text = "";
                textBoxFDJXH.Text = "";
                textBoxSCS.Text = "";
                textBoxHDZK.Text = "";
                textBoxJSSZK.Text = "";
                textBoxZZL.Text = "";
                textBoxHDZZL.Text = "";
                textBoxZBZL.Text = "";
                textBoxJZZL.Text = "";
                dateZCRQ.Value = DateTime.Now;
                dateSCRQ.Value = DateTime.Now;
                textBoxFDJPL.Text = "";
                comboBoxRLZL.SelectedIndex = 0;
                textBoxEDGL.Text = "";
                textBoxEDZS.Text = "";
                comboBoxBSQXS.SelectedIndex = 0;
                textBoxDWS.Text = "";
                comboBoxGYFS.SelectedIndex = 0;
                comboBoxDPXS.SelectedIndex = 0;
                comboBoxJQFS.SelectedIndex = 0;
                textBoxQGS.Text = "";
                comboBoxQDXS.SelectedIndex = 0;
                comboBoxCHZZ.SelectedIndex = 0;
                comboBoxDLSP.Text = "";
                comboBoxSRL.Text = "";
                comboBoxJHZZ.SelectedIndex = 0;
                comboBoxOBD.Text = "";
                comboBoxDKGY.Text = "";
                textBoxLXDH.Text = "";
                textBoxCZDZ.Text = "";
                comboBoxJCFS.SelectedIndex = 0;
                comboBoxJCLB.SelectedIndex = 0;
                comboBoxCLZL.SelectedIndex = 0;
                textBoxSSXQ.Text = "";
                comboBoxSFWDZR.SelectedIndex = 0;
                comboBoxSFYQBF.SelectedIndex = 0;
                textBoxFDJSCQY.Text = "";
                textBoxQDLTQY.Text = "";
                textBoxRYPH.Text = "";
                textBoxXSLC.Text = "";
                textBoxJCCS.Text = "1";
                comboBoxExCCS.Text = "2";
            }

        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clear_loginInf(false);
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            MONEY moneymodel = new MONEY();
            double safeFyStandard = 100;
            double exhaustFyStandard = 0;
            string safeBZ = "";
            string exhaustBZ = "";
            double safeFy = 0;
            double exhaustFy = 0;
            double totalFy = 0;
            string clhp=comboBoxCLPH.Text;
            RMBCapitalization rmbcapitalization = new RMBCapitalization();
            
            if (true)
            {
                if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.NHNETMODE)
                {
                }
                else
                { 
                    if (logininfcontrol.checkCarIsAtWait(clhp))
                    {
                        MessageBox.Show("环检保存失败，该车已经在环检待检序列中");
                        return;
                    }
                }
            }

            
            if (true)//如果参检环保
            {
                
                safeBZ = "未参检";
                foreach (Control control in groupPanel1.Controls)
                {
                    if (control is TextBox || control is ComboBox)
                    {
                        if (control.Text == "")
                        {
                            MessageBox.Show("请填写完所有车辆信息后再保存", "系统提示");
                            return;
                        }
                    }
                }
                foreach (Control control in groupBox1.Controls)
                {
                    if (control is TextBox || control is ComboBox)
                    {
                        if (control.Text == "")
                        {
                            MessageBox.Show("请填写完所有车辆信息后再保存", "系统提示");
                            return;
                        }
                    }
                }
                foreach (Control control in groupBox2.Controls)
                {
                    if (control is TextBox || control is ComboBox)
                    {
                        if (control.Text == "")
                        {
                            //if()
                            MessageBox.Show("请填写完所有车辆信息后再保存", "系统提示");
                            return;
                        }
                    }
                }
                foreach (Control control in groupBox3.Controls)
                {
                    if (control is TextBox || control is ComboBox)
                    {
                        if (control.Text == "")
                        {
                            MessageBox.Show("请填写完所有车辆信息后再保存", "系统提示");
                            return;
                        }
                    }
                }
                string linestring = "";
                string fy = "";
                string cxlb = "";
                CARINF model = new CARINF();
                CARATWAIT waitmodel = new CARATWAIT();
                CARSAFEDETECTED safemodel = new CARSAFEDETECTED();
                DateTime loginTime = DateTime.Now;//.Replace(':', ' ').Replace('/', ' ').Replace('-', ' ').Replace(" ","");
                if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.NHNETMODE)
                {
                    #region 南华
                    model.CLHP = comboBoxCLPH.Text;
                    model.CPYS = comboBoxCPYS.Text;
                    model.CLLX =comboBoxCLLX.Text;
                    model.CZ = textBoxCZ.Text;
                    model.SYXZ = comboBoxSYXZ.Text;
                    model.PP = textBoxPP.Text;
                    model.XH = comboBoxXH.Text;
                    model.CLSBM = textBoxDPHM.Text;
                    model.FDJHM = textBoxFDJH.Text;
                    model.FDJXH = textBoxFDJXH.Text;
                    model.SCQY = textBoxSCS.Text;
                    model.HDZK = textBoxHDZK.Text;
                    model.JSSZK = textBoxJSSZK.Text;
                    model.ZZL = textBoxZZL.Text;
                    model.HDZZL = textBoxHDZZL.Text;
                    model.ZBZL = textBoxZBZL.Text;
                    model.JZZL = textBoxJZZL.Text;
                    model.ZCRQ = dateZCRQ.Value;
                    model.SCRQ = dateSCRQ.Value;
                    model.FDJPL = textBoxFDJPL.Text;
                    model.RLZL =comboBoxRLZL.Text;
                    model.EDGL = textBoxEDGL.Text;
                    model.EDZS = textBoxEDZS.Text;
                    model.BSQXS = comboBoxBSQXS.Text;
                    model.DWS = textBoxDWS.Text;
                    model.GYFS = comboBoxGYFS.Text;
                    model.DPFS = comboBoxDPXS.Text;
                    model.JQFS = comboBoxJQFS.Text;
                    model.QGS = textBoxQGS.Text;
                    model.QDXS = comboBoxQDXS.Text;
                    model.CHZZ =  comboBoxCHZZ.Text;
                    model.DLSP = comboBoxDLSP.Text;
                    model.SFSRL = comboBoxSRL.Text;
                    model.JHZZ = comboBoxJHZZ.Text;
                    model.OBD = comboBoxOBD.Text;
                    model.DKGYYB = comboBoxDKGY.Text;
                    model.LXDH = textBoxLXDH.Text;
                    model.CZDZ = textBoxCZDZ.Text;
                    model.JCFS = comboBoxJCFS.Text;
                    model.JCLB =comboBoxJCLB.Text;
                    model.CLZL =  comboBoxCLZL.Text;
                    model.SSXQ = textBoxSSXQ.Text;
                    model.SFWDZR = comboBoxSFWDZR.Text;
                    model.SFYQBF = comboBoxSFYQBF.Text;
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    logininfcontrol.saveCarInfbyPlate(model);
                    waitmodel.CLHP = comboBoxCLPH.Text;
                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";
                    
                    
                    //waitmodel.XSLC = textBoxXSLC.Text;
                    if (int.Parse(model.ZZL) <= 3500)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                        cxlb = "轻型车";
                    }
                    else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("MIDDLE"));
                        cxlb = "中型车";
                    }
                    else
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                        cxlb = "重型车";
                    }
                    bool nhbgff = false;
                    string nhbgffyy = "";
                    if (checkBoxXESC.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "ESC|";
                    }
                    if (checkBoxXESP.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "ESP|";
                    }
                    if (checkBoxXABS.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "ABS|";
                    }
                    if (checkBoxXASR.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "ASR|";
                    }
                    if (checkBoxXXS.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "限速装置|";
                    }
                    if (checkBoxXSQ.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "适时四驱|";
                    }
                    if (checkBoxXCZ.Checked)
                    {
                        nhbgff = true;
                        nhbgffyy += "超重|";
                    }
                    exhaustFyStandard = exhaustFy;//标准费用
                    if (comboBoxRLZL.Text.Contains("柴油"))
                    {
                        if ((DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) < 0) && mainPanel.lz)//首先判断是否满足滤低烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (model.CLZL=="农用运输车" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (!nhbgff&&!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else if (int.Parse(model.ZZL) <= 3500 && int.Parse(model.EDGL) <= 150)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_LIGHT");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");

                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "自)";
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "ZYJS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                    return;
                                }
                            }
                        }

                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "自)";
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "ZYJS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (!nhbgff && !comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "双)";
                                   MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "SDS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                    return;
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, mainPanel.stationinfmodel.STATIONJCFF);
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                    {
                                        exhaustBZ += "(" + linestring + "稳)";
                                       MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    waitmodel.JCFF = mainPanel.stationinfmodel.STATIONJCFF;
                                }
                                else
                                {
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                        MessageBox.Show("该检测站未配置稳态工况法线，该车不能参检");
                                    else
                                        MessageBox.Show("该检测站未配置简易瞬态工况工况法线，该车不能参检");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }


                    waitmodel.XSLC = textBoxXSLC.Text;
                    waitmodel.BGJCFFYY = nhbgffyy;
                    if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.NHNETMODE)
                    {
                        Hashtable hs_v = new Hashtable();
                        hs_v.Add("TestSN", textBoxJCCS.Text.Trim());
                        switch (waitmodel.JCFF)
                        {
                            case "ASM":
                                hs_v.Add("TestType","1"); break;
                            case "VMAS":
                                hs_v.Add("TestType", "2"); break;
                            case "JZJS":
                                hs_v.Add("TestType", "3"); break;
                            case "SDS":
                                hs_v.Add("TestType", "4"); break;
                            case "ZYJS":
                                hs_v.Add("TestType", "5"); break;
                            case "LZ":
                                hs_v.Add("TestType", "6"); break;
                            default:break;
                        }
                        hs_v.Add("TestCharacter", comboBoxJCLB.Text.Trim());
                        hs_v.Add("EntryClerk", waitmodel.DLY);
                        hs_v.Add("FirstTestDate",cjdatetime.ToString("yyyy-MM-dd HH:mm:ss"));
                        hs_v.Add("TestPeriod", "360");
                        hs_v.Add("EntryTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        hs_v.Add("EntryPCIP", mainPanel.localIP);
                        hs_v.Add("PlateNumber", waitmodel.CLHP);
                        hs_v.Add("PlateType", waitmodel.CPYS);
                        hs_v.Add("RegistrationDate", model.ZCRQ.ToString("yyyy-MM-dd"));
                        hs_v.Add("Owner", model.CZ);
                        hs_v.Add("Phone", model.LXDH);
                        hs_v.Add("Address", model.CZDZ);
                        hs_v.Add("UseCharacter", model.SYXZ);
                        hs_v.Add("Manufacturer", model.SCQY);
                        hs_v.Add("Brand",model.PP);
                        hs_v.Add("Model", model.XH);
                        hs_v.Add("ProductionDate", model.SCRQ.ToString("yyyy-MM-dd"));
                        hs_v.Add("VIN", model.CLSBM);
                        hs_v.Add("ChassisModel", "");
                        hs_v.Add("VehicleType",model.CLLX);
                        hs_v.Add("MaximumTotalMass", model.ZZL);
                        hs_v.Add("UnladenMass", model.ZBZL);
                        hs_v.Add("AxleMass", "");
                        hs_v.Add("RatedLoadingMass", model.HDZZL);
                        hs_v.Add("RatedPassengerCapacity", model.HDZK);
                        hs_v.Add("TyrePressure", model.QDLTQY);
                        hs_v.Add("TravelledDistance", waitmodel.XSLC);
                        hs_v.Add("EngineManufacturer", model.FDJSCQY);
                        hs_v.Add("EngineModel",model.FDJXH);
                        hs_v.Add("EngineNumber",model.FDJHM);
                        hs_v.Add("EngineStroke", "");
                        hs_v.Add("Displacement",model.FDJPL);
                        hs_v.Add("CylinderNumber",model.QGS);
                        hs_v.Add("OilSupplyMode", model.GYFS);
                        hs_v.Add("AirIntakeMode", model.JQFS);
                        if (model.JHZZ == "是")
                        {
                            hs_v.Add("HasCatalyticConverter", "1");
                        }
                        else
                        {
                            hs_v.Add("HasCatalyticConverter","0");
                        }
                        hs_v.Add("FuelType", model.RLZL);
                        hs_v.Add("FuelMark", model.RYPH);
                        if (model.SFSRL == "是")
                        {
                            hs_v.Add("DoubleFuel", "1");
                        }
                        else
                        {
                            hs_v.Add("DoubleFuel", "0");
                        }
                        hs_v.Add("FuelType2", "");
                        hs_v.Add("FuelMark2","");
                        hs_v.Add("RatedRev", model.EDZS);
                        hs_v.Add("RatedPower", model.EDGL);
                        hs_v.Add("MaximumNetPower", model.EDGL);
                        hs_v.Add("GearBoxType", model.BSQXS);
                        hs_v.Add("GearNumber", model.DWS);
                        hs_v.Add("DriveType", model.QDXS);
                        hs_v.Add("EPSign", "");
                        hs_v.Add("CertificateNumber","");
                        hs_v.Add("ExhaustPipeNumber", "");
                        //hs_v.Add("TCS", model.CHZZ);
                        switch (model.CHZZ)
                        {
                            case "无":
                                hs_v.Add("TCS", "0");break;
                            case "可摘除":
                                hs_v.Add("TCS", "1"); break;
                            case "不可摘除":
                                hs_v.Add("TCS", "2"); break;
                            default:break;
                        }
                        hs_v.Add("FuelPumpMode", "");
                        hs_v.Add("IsPassengerVehicle", "1");
                        hs_v.Add("EmissionStandard",comboBoxPFBZ.Text);
                        if (model.JHZZ == "是")
                        {
                            hs_v.Add("BDE", "1");
                        }
                        else
                        {
                            hs_v.Add("BDE", "0");
                        }
                        int nhcode, nhexpcode;
                        string nhmsg, nhexpmsg;
                        string jylsh = "";
                        jylsh = mainPanel.nhinterface.SendVehicleInspection(hs_v, out nhcode, out nhmsg, out nhexpcode, out nhexpmsg);
                        if (nhcode == 0 && nhexpcode == 0)
                        {
                            waitmodel.CLID = jylsh;
                        }
                        else if(nhcode!=0)
                        {
                            MessageBox.Show("车辆报检至联网平台失败，code:" + nhcode + "|msg:" + nhmsg);
                            return;
                        }
                        else if (nhexpcode != 0)
                        {
                            MessageBox.Show("车辆报检至联网平台发生异常，code:" + nhexpcode + "|msg:" + nhexpmsg);
                            return;
                        }
                    }
                    string addmsg = "";
                    bool addSuccess = true;
                    
                    if (true)
                    {
                        logininfcontrol.deleteCarAtWaitbyPlate(waitmodel.CLHP);
                        string addmessage = logininfcontrol.addCarToWaitList(waitmodel);
                        if (addmessage == "环检添加成功")
                        {
                            addmsg += "环检添加成功.";
                            init_waitList();
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "成功";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                        else
                        {
                            addmsg += "未知原因导致环检添加失败.";
                            addSuccess = false;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "失败";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                    }
                    if (addSuccess)
                    {
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClear.Enabled = false;
                        moneymodel.CLID = waitmodel.CLID;
                        moneymodel.CLHP = waitmodel.CLHP;
                        moneymodel.DLSJ = waitmodel.DLSJ;
                        moneymodel.SAFE =  "0";
                        moneymodel.EXHAUST = "1";
                        moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                        moneymodel.SAFEFY = safeFy.ToString("0.0");
                        moneymodel.SAFEBZ = safeBZ;
                        moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                        moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                        moneymodel.EXHAUSTBZ = exhaustBZ + "(" + cxlb + "）";
                        moneymodel.FY = totalFy.ToString("0.0");
                        moneymodel.SKR = mainPanel.nowUser.userName;
                        moneymodel.SFSJ = DateTime.Now;
                        moneymodel.TF = "0";
                        moneymodel.TFFY = "0.0";
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                    #endregion
                }
                else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.JXNETMODE)
                {
                    #region 江西
                    model.CLHP = comboBoxCLPH.Text;
                    model.CPYS = comboBoxCPYS.Text;
                    model.CLLX = comboBoxCLLX.Text;
                    model.CZ = textBoxCZ.Text;
                    model.SYXZ = comboBoxSYXZ.Text;
                    model.PP = textBoxPP.Text;
                    model.XH = comboBoxXH.Text;
                    model.CLSBM = textBoxDPHM.Text;
                    model.FDJHM = textBoxFDJH.Text;
                    model.FDJXH = textBoxFDJXH.Text;
                    model.SCQY = textBoxSCS.Text;
                    model.HDZK = textBoxHDZK.Text;
                    model.JSSZK = textBoxJSSZK.Text;
                    model.ZZL = textBoxZZL.Text;
                    model.HDZZL = textBoxHDZZL.Text;
                    model.ZBZL = textBoxZBZL.Text;
                    model.JZZL = textBoxJZZL.Text;
                    model.ZCRQ = dateZCRQ.Value;
                    model.SCRQ = dateSCRQ.Value;
                    model.FDJPL = textBoxFDJPL.Text;
                    model.RLZL = comboBoxRLZL.Text;
                    model.EDGL = textBoxEDGL.Text;
                    model.EDZS = textBoxEDZS.Text;
                    model.BSQXS = comboBoxBSQXS.Text;
                    model.DWS = textBoxDWS.Text;
                    model.GYFS = comboBoxGYFS.Text;
                    model.DPFS = comboBoxDPXS.Text;
                    model.JQFS = comboBoxJQFS.Text;
                    model.QGS = textBoxQGS.Text;
                    model.QDXS = comboBoxQDXS.Text;
                    model.CHZZ = comboBoxCHZZ.Text;
                    model.DLSP = comboBoxDLSP.Text;
                    model.SFSRL = comboBoxSRL.Text;
                    model.JHZZ = comboBoxJHZZ.Text;
                    model.OBD = comboBoxOBD.Text;
                    model.DKGYYB = comboBoxDKGY.Text;
                    model.LXDH = textBoxLXDH.Text;
                    model.CZDZ = textBoxCZDZ.Text;
                    model.JCFS = comboBoxJCFS.Text;
                    model.JCLB = comboBoxJCLB.Text;
                    model.CLZL = comboBoxCLZL.Text;
                    model.SSXQ = textBoxSSXQ.Text;
                    model.SFWDZR = comboBoxSFWDZR.Text;
                    model.SFYQBF = comboBoxSFYQBF.Text;
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    logininfcontrol.saveCarInfbyPlate(model);
                    waitmodel.CLHP = comboBoxCLPH.Text;
                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";


                    //waitmodel.XSLC = textBoxXSLC.Text;
                    if (int.Parse(model.ZZL) <= 3500)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                        cxlb = "轻型车";
                    }
                    else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("MIDDLE"));
                        cxlb = "中型车";
                    }
                    else
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                        cxlb = "重型车";
                    }
                    exhaustFyStandard = exhaustFy;//标准费用
                    if (comboBoxRLZL.Text.Contains("柴油"))
                    {
                        if ((DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) < 0) && mainPanel.lz)//首先判断是否满足滤低烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (model.CLZL == "农用运输车" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                               MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else if (int.Parse(model.ZZL) <= 3500 && int.Parse(model.EDGL) <= 150)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_LIGHT");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");

                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "自)";
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "ZYJS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                    return;
                                }
                            }
                        }

                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "自)";
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "ZYJS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "双)";
                                     MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "SDS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                    return;
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, mainPanel.stationinfmodel.STATIONJCFF);
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                    {
                                        exhaustBZ += "(" + linestring + "稳)";
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                         MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    waitmodel.JCFF = mainPanel.stationinfmodel.STATIONJCFF;
                                }
                                else
                                {
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                        MessageBox.Show("该检测站未配置稳态工况法线，该车不能参检");
                                    else
                                        MessageBox.Show("该检测站未配置简易瞬态工况工况法线，该车不能参检");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }


                    waitmodel.XSLC = textBoxXSLC.Text;
                    JxWebClient.logonDetectionData detectiondata = new JxWebClient.logonDetectionData();
                    JxWebClient.logonAppDetectionData appdetectiondata = new JxWebClient.logonAppDetectionData();
                    if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.JXNETMODE)
                    {
                        detectiondata.testLineId = "";
                        detectiondata.testCategory = mainPanel.jxinterface.RJX_TESTCATEGORY.GetValue(comboBoxJCLB.Text,"");// (comboBoxJCLB.SelectedIndex + 1).ToString();
                        switch (waitmodel.JCFF)
                        {
                            case "ASM":
                                detectiondata.testType="2"; break;
                            case "VMAS":
                                detectiondata.testType = "3"; break;
                            case "JZJS":
                                detectiondata.testType = "4"; break;
                            case "SDS":
                                detectiondata.testType = "1"; break;
                            case "ZYJS":
                                detectiondata.testType = "6"; break;
                            case "LZ":
                                detectiondata.testType = "5"; break;
                            default: break;
                        }
                        detectiondata.testPerson = "";
                        detectiondata.detectionDate = DateTime.Now.ToString("yyyy-MM-dd");
                        detectiondata.logonPerson = waitmodel.DLY;
                        detectiondata.guidePerson = "";
                        appdetectiondata.vehicleLicense = waitmodel.CLHP;
                        appdetectiondata.vehicleLicenseType =mainPanel.jxinterface.RJX_VEHICLELICENSETYPE.GetValue(waitmodel.CPYS, "");
                        appdetectiondata.vehicleLicenseTypeGa247= logininfcontrol.getComBoBoxItemsID("号牌种类", comboBoxHPZL.Text);
                        appdetectiondata.vehicleVin = model.CLSBM;
                        appdetectiondata.engineIdentification = model.FDJHM;
                        appdetectiondata.vehicleType = comboBoxCLLX.Text.Split('_')[0];
                        appdetectiondata.useType = mainPanel.jxinterface.RJX_USETYPE.GetValue(model.SYXZ, "");
                        appdetectiondata.registerDate = model.ZCRQ.ToString("yyyy-MM-dd");
                        appdetectiondata.flagExempted = "N";
                        appdetectiondata.odometerNumber = waitmodel.XSLC;
                        appdetectiondata.ownerName = model.CZ;
                        appdetectiondata.ownerTel = model.LXDH;
                        appdetectiondata.ownerAddress = model.CZDZ;
                        appdetectiondata.vehicleModel = model.XH;
                        appdetectiondata.engineModel = model.FDJXH;
                        appdetectiondata.emissionStandard = mainPanel.jxinterface.RJX_EMISSIONSTANDARD.GetValue(comboBoxPFBZ.Text, "");
                        appdetectiondata.gearType= mainPanel.jxinterface.RJX_GEARTYPE.GetValue(model.BSQXS, "");
                        appdetectiondata.airInType = mainPanel.jxinterface.RJX_AIRINTYPE.GetValue(model.JQFS, "");
                        appdetectiondata.engineDisplacement = model.FDJPL;
                        appdetectiondata.fuelType= mainPanel.jxinterface.RJX_FUELTYPE.GetValue(model.RLZL, "");
                        appdetectiondata.fuelSupply = mainPanel.jxinterface.RJX_FUELSUPPLY.GetValue(model.GYFS, "");
                        appdetectiondata.engineRatedPower = model.EDGL;
                        appdetectiondata.engineRatedSpeed = model.EDZS;
                        appdetectiondata.driveMode= mainPanel.jxinterface.RJX_DRIVEMODE.GetValue(model.QDXS, "");
                        appdetectiondata.massMax = model.ZZL;
                        appdetectiondata.massReference = model.JZZL;
                        appdetectiondata.cylindersNumber = model.QGS;
                        appdetectiondata.flagEgr = "Y";
                        appdetectiondata.flagTg = "Y";
                        appdetectiondata.flagHcl = model.JHZZ == "无" ? "N" : "Y";
                        appdetectiondata.hclType=model.JHZZ=="无"?"6" : mainPanel.jxinterface.RJX_HCLTYPE.GetValue(model.JHZZ, "");
                        appdetectiondata.flagDk = mainPanel.jxinterface.RJX_FLAGDK.GetValue(model.DKGYYB, "");
                        appdetectiondata.flagObd = mainPanel.jxinterface.RJX_FLAGOBD.GetValue(model.OBD, "");
                        appdetectiondata.flagObdAccess = "Y";
                        appdetectiondata.flagObdFailure = "";
                        appdetectiondata.flagObdGood = "N";
                        appdetectiondata.flagTurn = "Y";
                        appdetectiondata.flagAirOut = "N";
                        appdetectiondata.flagLeak = "N";
                        appdetectiondata.flagCloseSpecial = "Y";
                        appdetectiondata.fuelSpec = model.RYPH;
                        appdetectiondata.vehicleManufacturer = model.SCQY;
                        appdetectiondata.manufacturingDate = model.SCRQ.ToString("yyyy-MM-dd");
                        appdetectiondata.passengersCount = model.HDZK;
                        
                    }
                    string addmsg = "";
                    bool addSuccess = true;
                    
                    if (true)
                    {
                        string code, msg;
                        if (mainPanel.jxinterface.logon(detectiondata, appdetectiondata,out code, out msg))
                        {
                            addmsg += "环检添加成功.";
                            init_waitList();
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "成功";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                        else
                        {
                            addmsg += "环检添加失败.code"+code+"\r\nmsg:"+msg;
                            addSuccess = false;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "失败";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                    }
                    if (addSuccess)
                    {
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClear.Enabled = false;
                        moneymodel.CLID = waitmodel.CLID;
                        moneymodel.CLHP = waitmodel.CLHP;
                        moneymodel.DLSJ = waitmodel.DLSJ;
                        moneymodel.SAFE = "0";
                        moneymodel.EXHAUST = "1";
                        moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                        moneymodel.SAFEFY = safeFy.ToString("0.0");
                        moneymodel.SAFEBZ = safeBZ;
                        moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                        moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                        moneymodel.EXHAUSTBZ = exhaustBZ + "(" + cxlb + "）";
                        moneymodel.FY = totalFy.ToString("0.0");
                        moneymodel.SKR = mainPanel.nowUser.userName;
                        moneymodel.SFSJ = DateTime.Now;
                        moneymodel.TF = "0";
                        moneymodel.TFFY = "0.0";
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                    #endregion
                }
                else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.DALINETMODE)
                {
                    #region 大理
                    model.CLHP = comboBoxCLPH.Text;
                    model.CPYS = comboBoxCPYS.Text;
                    model.CLLX = comboBoxCLLX.Text;
                    model.CZ = textBoxCZ.Text;
                    model.SYXZ = comboBoxSYXZ.Text;
                    model.CSYS ="";
                    model.PP = textBoxPP.Text;
                    model.XH = comboBoxXH.Text;
                    model.CLSBM = textBoxDPHM.Text;
                    model.FDJHM = textBoxFDJH.Text;
                    model.FDJXH = textBoxFDJXH.Text;
                    model.SCQY = textBoxSCS.Text;
                    model.HDZK = textBoxHDZK.Text;
                    model.JSSZK = textBoxJSSZK.Text;
                    model.ZZL = textBoxZZL.Text;
                    model.HDZZL = textBoxHDZZL.Text;
                    model.ZBZL = textBoxZBZL.Text;
                    model.JZZL = textBoxJZZL.Text;
                    model.ZCRQ = dateZCRQ.Value;
                    model.SCRQ = dateSCRQ.Value;
                    model.FDJPL = textBoxFDJPL.Text;
                    model.RLZL = comboBoxRLZL.Text;
                    model.EDGL = textBoxEDGL.Text;
                    model.EDZS = textBoxEDZS.Text;
                    model.BSQXS = comboBoxBSQXS.Text;
                    model.DWS = textBoxDWS.Text;
                    model.GYFS = comboBoxGYFS.Text;
                    model.DPFS = comboBoxDPXS.Text;
                    model.JQFS = comboBoxJQFS.Text;
                    model.QGS = textBoxQGS.Text;
                    model.QDXS = comboBoxQDXS.Text;
                    model.CHZZ = comboBoxCHZZ.Text;
                    model.DLSP = comboBoxDLSP.Text;
                    model.SFSRL = comboBoxSRL.Text;
                    model.JHZZ = comboBoxJHZZ.Text;
                    model.OBD = comboBoxOBD.Text;
                    model.DKGYYB = comboBoxDKGY.Text;
                    model.LXDH = textBoxLXDH.Text;
                    model.CZDZ = textBoxCZDZ.Text;
                    model.JCFS = comboBoxJCFS.Text;
                    model.JCLB = comboBoxJCLB.Text;
                    model.CLZL = comboBoxCLZL.Text;
                    model.SSXQ = textBoxSSXQ.Text;
                    model.SFWDZR = comboBoxSFWDZR.Text;
                    model.SFYQBF = comboBoxSFYQBF.Text;
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    logininfcontrol.saveCarInfbyPlate(model);
                    waitmodel.CLHP = comboBoxCLPH.Text;

                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString("yyyyMMddHHmmss");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";


                    //waitmodel.XSLC = textBoxXSLC.Text;
                    if (int.Parse(model.ZZL) <= 3500)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                        cxlb = "轻型车";
                    }
                    else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("MIDDLE"));
                        cxlb = "中型车";
                    }
                    else
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                        cxlb = "重型车";
                    }
                    exhaustFyStandard = exhaustFy;//标准费用
                    if (comboBoxRLZL.Text.Contains("柴油"))
                    {
                        if ((DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) < 0) && mainPanel.lz)//首先判断是否满足滤低烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (model.CLZL == "农用运输车" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else if (int.Parse(model.ZZL) <= 3500 && int.Parse(model.EDGL) <= 150)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_LIGHT");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");

                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "自)";
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "ZYJS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                    return;
                                }
                            }
                        }

                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "自)";
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "ZYJS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "双)";
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "SDS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                    return;
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, mainPanel.stationinfmodel.STATIONJCFF);
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                    {
                                        exhaustBZ += "(" + linestring + "稳)";
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    waitmodel.JCFF = mainPanel.stationinfmodel.STATIONJCFF;
                                }
                                else
                                {
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                        MessageBox.Show("该检测站未配置稳态工况法线，该车不能参检");
                                    else
                                        MessageBox.Show("该检测站未配置简易瞬态工况工况法线，该车不能参检");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }


                    waitmodel.XSLC = textBoxXSLC.Text;
                    string addmsg = "";
                    bool addSuccess = true;

                    if (true)
                    {
                        string addmessage = logininfcontrol.addCarToWaitList(waitmodel);
                        if (addmessage == "环检添加成功")
                        {
                            addmsg += "环检添加成功.";
                            listBoxCarAtWait.Items.Add(model.CLHP);
                            listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "成功";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                        else
                        {
                            addmsg += "未知原因导致环检添加失败.";
                            addSuccess = false;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "失败";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                    }
                    if (addSuccess)
                    {
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClear.Enabled = false;
                        moneymodel.CLID = waitmodel.CLID;
                        moneymodel.CLHP = waitmodel.CLHP;
                        moneymodel.DLSJ = waitmodel.DLSJ;
                        moneymodel.SAFE = "0";
                        moneymodel.EXHAUST = "1";
                        moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                        moneymodel.SAFEFY = safeFy.ToString("0.0");
                        moneymodel.SAFEBZ = safeBZ;
                        moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                        moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                        moneymodel.EXHAUSTBZ = exhaustBZ + "(" + cxlb + "）";
                        moneymodel.FY = totalFy.ToString("0.0");
                        moneymodel.SKR = mainPanel.nowUser.userName;
                        moneymodel.SFSJ = DateTime.Now;
                        moneymodel.TF = "0";
                        moneymodel.TFFY = "0.0";
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                    #endregion
                }
                else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ORTNETMODE)
                {
                    #region 欧润特
                    model.CLHP = comboBoxCLPH.Text;
                    model.CPYS = comboBoxCPYS.Text;
                    model.CLLX = comboBoxCLLX.Text;
                    model.CZ = textBoxCZ.Text;
                    model.SYXZ = comboBoxSYXZ.Text;
                    model.CSYS = "";
                    model.PP = textBoxPP.Text;
                    model.XH = comboBoxXH.Text;
                    model.CLSBM = textBoxDPHM.Text;
                    model.FDJHM = textBoxFDJH.Text;
                    model.FDJXH = textBoxFDJXH.Text;
                    model.SCQY = textBoxSCS.Text;
                    model.HDZK = textBoxHDZK.Text;
                    model.JSSZK = textBoxJSSZK.Text;
                    model.ZZL = textBoxZZL.Text;
                    model.HDZZL = textBoxHDZZL.Text;
                    model.ZBZL = textBoxZBZL.Text;
                    model.JZZL = textBoxJZZL.Text;
                    model.ZCRQ = dateZCRQ.Value;
                    model.SCRQ = dateSCRQ.Value;
                    model.FDJPL = textBoxFDJPL.Text;
                    model.RLZL = comboBoxRLZL.Text;
                    model.EDGL = textBoxEDGL.Text;
                    model.EDZS = textBoxEDZS.Text;
                    model.BSQXS = comboBoxBSQXS.Text;
                    model.DWS = textBoxDWS.Text;
                    model.GYFS = comboBoxGYFS.Text;
                    model.DPFS = comboBoxDPXS.Text;
                    model.JQFS = comboBoxJQFS.Text;
                    model.QGS = textBoxQGS.Text;
                    model.QDXS = comboBoxQDXS.Text;
                    model.CHZZ = comboBoxCHZZ.Text;
                    model.DLSP = comboBoxDLSP.Text;
                    model.SFSRL = comboBoxSRL.Text;
                    model.JHZZ = comboBoxJHZZ.Text;
                    model.OBD = comboBoxOBD.Text;
                    model.DKGYYB = comboBoxDKGY.Text;
                    model.LXDH = textBoxLXDH.Text;
                    model.CZDZ = textBoxCZDZ.Text;
                    model.JCFS = comboBoxJCFS.Text;
                    model.JCLB = comboBoxJCLB.Text;
                    model.CLZL = comboBoxCLZL.Text;
                    model.SSXQ = textBoxSSXQ.Text;
                    model.SFWDZR = comboBoxSFWDZR.Text;
                    model.SFYQBF = comboBoxSFYQBF.Text;
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    
                    logininfcontrol.saveCarInfbyPlate(model);
                    waitmodel.CLHP = comboBoxCLPH.Text;

                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString("yyyyMMddHHmmss");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";



                    //waitmodel.XSLC = textBoxXSLC.Text;
                    if (int.Parse(model.ZZL) <= 3500)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                        cxlb = "轻型车";
                    }
                    else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("MIDDLE"));
                        cxlb = "中型车";
                    }
                    else
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                        cxlb = "重型车";
                    }
                    exhaustFyStandard = exhaustFy;//标准费用
                    if (comboBoxRLZL.Text.Contains("柴油"))
                    {
                        if ((DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) < 0) && mainPanel.lz)//首先判断是否满足滤低烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (model.CLZL == "农用运输车" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else if (int.Parse(model.ZZL) <= 3500 && int.Parse(model.EDGL) <= 150)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_LIGHT");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");

                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "自)";
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "ZYJS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                    return;
                                }
                            }
                        }

                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "自)";
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "ZYJS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "双)";
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "SDS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                    return;
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, mainPanel.stationinfmodel.STATIONJCFF);
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                    {
                                        exhaustBZ += "(" + linestring + "稳)";
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    waitmodel.JCFF = mainPanel.stationinfmodel.STATIONJCFF;
                                }
                                else
                                {
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                        MessageBox.Show("该检测站未配置稳态工况法线，该车不能参检");
                                    else
                                        MessageBox.Show("该检测站未配置简易瞬态工况工况法线，该车不能参检");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }


                    waitmodel.XSLC = textBoxXSLC.Text;
                    string addmsg = "";
                    bool addSuccess = true;

                    if (true)
                    {
                        string addmessage = logininfcontrol.addCarToWaitList(waitmodel);
                        if (addmessage == "环检添加成功")
                        {
                            addmsg += "环检添加成功.";
                            listBoxCarAtWait.Items.Add(model.CLHP);
                            listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "成功";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                        else
                        {
                            addmsg += "未知原因导致环检添加失败.";
                            addSuccess = false;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "失败";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                    }
                    if (addSuccess)
                    {
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClear.Enabled = false;
                        moneymodel.CLID = waitmodel.CLID;
                        moneymodel.CLHP = waitmodel.CLHP;
                        moneymodel.DLSJ = waitmodel.DLSJ;
                        moneymodel.SAFE = "0";
                        moneymodel.EXHAUST = "1";
                        moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                        moneymodel.SAFEFY = safeFy.ToString("0.0");
                        moneymodel.SAFEBZ = safeBZ;
                        moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                        moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                        moneymodel.EXHAUSTBZ = exhaustBZ + "(" + cxlb + "）";
                        moneymodel.FY = totalFy.ToString("0.0");
                        moneymodel.SKR = mainPanel.nowUser.userName;
                        moneymodel.SFSJ = DateTime.Now;
                        moneymodel.TF = "0";
                        moneymodel.TFFY = "0.0";
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                    #endregion
                }
                else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.EZNETMODE)
                {
                    #region 鄂州
                    model.CLHP = comboBoxCLPH.Text;
                    model.CPYS = comboBoxCPYS.Text;
                    model.CLLX = comboBoxCLLX.Text;
                    model.CZ = textBoxCZ.Text;
                    model.SYXZ = comboBoxSYXZ.Text;
                    model.CSYS = "";
                    model.PP = textBoxPP.Text;
                    model.XH = comboBoxXH.Text;
                    model.CLSBM = textBoxDPHM.Text;
                    model.FDJHM = textBoxFDJH.Text;
                    model.FDJXH = textBoxFDJXH.Text;
                    model.SCQY = textBoxSCS.Text;
                    model.HDZK = textBoxHDZK.Text;
                    model.JSSZK = textBoxJSSZK.Text;
                    model.ZZL = textBoxZZL.Text;
                    model.HDZZL = textBoxHDZZL.Text;
                    model.ZBZL = textBoxZBZL.Text;
                    model.JZZL = textBoxJZZL.Text;
                    model.ZCRQ = dateZCRQ.Value;
                    model.SCRQ = dateSCRQ.Value;
                    model.FDJPL = textBoxFDJPL.Text;
                    model.RLZL = comboBoxRLZL.Text;
                    model.EDGL = textBoxEDGL.Text;
                    model.EDZS = textBoxEDZS.Text;
                    model.BSQXS = comboBoxBSQXS.Text;
                    model.DWS = textBoxDWS.Text;
                    model.GYFS = comboBoxGYFS.Text;
                    model.DPFS = comboBoxDPXS.Text;
                    model.JQFS = comboBoxJQFS.Text;
                    model.QGS = textBoxQGS.Text;
                    model.QDXS = comboBoxQDXS.Text;
                    model.CHZZ = comboBoxCHZZ.Text;
                    model.DLSP = comboBoxDLSP.Text;
                    model.SFSRL = comboBoxSRL.Text;
                    model.JHZZ = comboBoxJHZZ.Text;
                    model.OBD = comboBoxOBD.Text;
                    model.DKGYYB = comboBoxDKGY.Text;
                    model.LXDH = textBoxLXDH.Text;
                    model.CZDZ = textBoxCZDZ.Text;
                    model.JCFS = comboBoxJCFS.Text;
                    model.JCLB = comboBoxJCLB.Text;
                    model.CLZL = comboBoxCLZL.Text;
                    model.SSXQ = textBoxSSXQ.Text;
                    model.SFWDZR = comboBoxSFWDZR.Text;
                    model.SFYQBF = comboBoxSFYQBF.Text;
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    logininfcontrol.saveCarInfbyPlate(model);
                    waitmodel.CLHP = comboBoxCLPH.Text;

                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString("yyyyMMddHHmmss");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";


                    //waitmodel.XSLC = textBoxXSLC.Text;
                    if (int.Parse(model.ZZL) <= 3500)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                        cxlb = "轻型车";
                    }
                    else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("MIDDLE"));
                        cxlb = "中型车";
                    }
                    else
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                        cxlb = "重型车";
                    }
                    exhaustFyStandard = exhaustFy;//标准费用
                    if (comboBoxRLZL.Text.Contains("柴油"))
                    {
                        if ((DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) < 0) && mainPanel.lz)//首先判断是否满足滤低烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (model.CLZL == "农用运输车" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else if (int.Parse(model.ZZL) <= 3500 && int.Parse(model.EDGL) <= 150)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_LIGHT");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");

                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "自)";
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "ZYJS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                    return;
                                }
                            }
                        }

                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "自)";
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "ZYJS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "双)";
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "SDS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                    return;
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, mainPanel.stationinfmodel.STATIONJCFF);
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                    {
                                        exhaustBZ += "(" + linestring + "稳)";
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    waitmodel.JCFF = mainPanel.stationinfmodel.STATIONJCFF;
                                }
                                else
                                {
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                        MessageBox.Show("该检测站未配置稳态工况法线，该车不能参检");
                                    else
                                        MessageBox.Show("该检测站未配置简易瞬态工况工况法线，该车不能参检");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }


                    waitmodel.XSLC = textBoxXSLC.Text;
                    string addmsg = "";
                    bool addSuccess = true;

                    if (true)
                    {
                        string addmessage = logininfcontrol.addCarToWaitList(waitmodel);
                        if (addmessage == "环检添加成功")
                        {
                            addmsg += "环检添加成功.";
                            listBoxCarAtWait.Items.Add(model.CLHP);
                            listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "成功";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                        else
                        {
                            addmsg += "未知原因导致环检添加失败.";
                            addSuccess = false;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "失败";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                    }
                    if (addSuccess)
                    {
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClear.Enabled = false;
                        moneymodel.CLID = waitmodel.CLID;
                        moneymodel.CLHP = waitmodel.CLHP;
                        moneymodel.DLSJ = waitmodel.DLSJ;
                        moneymodel.SAFE = "0";
                        moneymodel.EXHAUST = "1";
                        moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                        moneymodel.SAFEFY = safeFy.ToString("0.0");
                        moneymodel.SAFEBZ = safeBZ;
                        moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                        moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                        moneymodel.EXHAUSTBZ = exhaustBZ + "(" + cxlb + "）";
                        moneymodel.FY = totalFy.ToString("0.0");
                        moneymodel.SKR = mainPanel.nowUser.userName;
                        moneymodel.SFSJ = DateTime.Now;
                        moneymodel.TF = "0";
                        moneymodel.TFFY = "0.0";
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                    #endregion
                }
                else
                {
                    #region 本地
                    model.CLHP = comboBoxCLPH.Text;
                    model.CPYS = comboBoxCPYS.Text;
                    model.CLLX = logininfcontrol.getComBoBoxItemsID("车辆类型", comboBoxCLLX.Text.Split('_')[1]);
                    model.CZ = textBoxCZ.Text;
                    model.SYXZ = logininfcontrol.getComBoBoxItemsID("使用性质", comboBoxSYXZ.Text);
                    model.PP = textBoxPP.Text;
                    model.XH = comboBoxXH.Text;
                    model.CLSBM = textBoxDPHM.Text;
                    model.FDJHM = textBoxFDJH.Text;
                    model.FDJXH = textBoxFDJXH.Text;
                    model.SCQY = textBoxSCS.Text;
                    model.HDZK = textBoxHDZK.Text;
                    model.JSSZK = textBoxJSSZK.Text;
                    model.ZZL = textBoxZZL.Text;
                    model.HDZZL = textBoxHDZZL.Text;
                    model.ZBZL = textBoxZBZL.Text;
                    model.JZZL = textBoxJZZL.Text;
                    model.ZCRQ = dateZCRQ.Value;
                    model.SCRQ = dateSCRQ.Value;
                    model.FDJPL = textBoxFDJPL.Text;
                    model.RLZL = logininfcontrol.getComBoBoxItemsID("燃料种类", comboBoxRLZL.Text);
                    model.EDGL = textBoxEDGL.Text;
                    model.EDZS = textBoxEDZS.Text;
                    model.BSQXS = logininfcontrol.getComBoBoxItemsID("变速器形式", comboBoxBSQXS.Text);
                    model.DWS = textBoxDWS.Text;
                    model.GYFS = logininfcontrol.getComBoBoxItemsID("供油方式", comboBoxGYFS.Text);
                    model.DPFS = logininfcontrol.getComBoBoxItemsID("电喷方式", comboBoxDPXS.Text);
                    model.JQFS = logininfcontrol.getComBoBoxItemsID("进气方式", comboBoxJQFS.Text);
                    model.QGS = textBoxQGS.Text;
                    model.QDXS = logininfcontrol.getComBoBoxItemsID("驱动形式", comboBoxQDXS.Text);
                    model.CHZZ = logininfcontrol.getComBoBoxItemsID("侧滑装置", comboBoxCHZZ.Text);
                    model.DLSP = comboBoxDLSP.Text;
                    model.SFSRL = comboBoxSRL.Text;
                    if (comboBoxRLZL.Text.Contains("柴油"))
                        model.JHZZ = logininfcontrol.getComBoBoxItemsID("排气后处理装置", comboBoxJHZZ.Text);
                    else
                        model.JHZZ = logininfcontrol.getComBoBoxItemsID("催化转化器", comboBoxJHZZ.Text);
                    model.OBD = comboBoxOBD.Text;
                    model.DKGYYB = comboBoxDKGY.Text;
                    model.LXDH = textBoxLXDH.Text;
                    model.CZDZ = textBoxCZDZ.Text;
                    model.JCFS = logininfcontrol.getComBoBoxItemsID("检测方式", comboBoxJCFS.Text);
                    model.JCLB = logininfcontrol.getComBoBoxItemsID("检测类别", comboBoxJCLB.Text);
                    model.CLZL = logininfcontrol.getComBoBoxItemsID("车辆种类", comboBoxCLZL.Text);
                    model.SSXQ = textBoxSSXQ.Text;
                    model.SFWDZR = logininfcontrol.getComBoBoxItemsID("是否外地转入", comboBoxSFWDZR.Text);
                    model.SFYQBF = logininfcontrol.getComBoBoxItemsID("是否延期报废", comboBoxSFYQBF.Text);
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    model.CCS = comboBoxExCCS.Text;
                    logininfcontrol.saveCarInfbyPlate(model);
                    waitmodel.CLHP = comboBoxCLPH.Text;
                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";
                    string code, message;
                    if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.CCNETMODE)
                    {
                        if (!mainPanel.ccsocket.SendLoginOk(waitmodel.CLHP, model.CLLX, waitmodel.CLID, waitmodel.JCCS, out code, out message))
                        {
                            MessageBox.Show("发送车辆号牌、号牌种类给联网平台时出错:" + message);
                            return;
                        }
                    }
                    
                    //waitmodel.XSLC = textBoxXSLC.Text;
                    if (int.Parse(model.ZZL) <= 3500)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                        cxlb = "轻型车";
                    }
                    else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("MIDDLE"));
                        cxlb = "中型车";
                    }
                    else
                    {
                        exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                        cxlb = "重型车";
                    }
                    exhaustFyStandard = exhaustFy;//标准费用
                    
                    if (comboBoxRLZL.Text.Contains("柴油"))
                    {
                        if ((DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) < 0) && mainPanel.lz)//首先判断是否满足滤低烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (comboBoxCLZL.Text == "农用运输车" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "LZ");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "滤)";
                                MessageBox.Show("环保:请到" + linestring + "进行滤纸烟度法检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "LZ";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置滤纸烟度法检测，该车不能参检");
                                return;
                            }
                        }
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                         MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else if (int.Parse(model.ZZL) <= 3500 && int.Parse(model.EDGL) <= 150)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_LIGHT");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "加)";
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");

                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += model.SYXZ;
                                        }
                                        if (waitmodel.JCCS == "1")
                                            exhaustFy = exhaustFy;
                                        else if (waitmodel.JCCS == "2")
                                        {
                                            exhaustFy = 0;
                                            exhaustBZ += "第一次复检";
                                        }
                                        else
                                        {
                                            exhaustFy = exhaustFy / 2.0;
                                            exhaustBZ += "二次以上复检";
                                        }
                                        totalFy = exhaustFy + safeFy;
                                        //fy = exhaustFy.ToString("0.0");
                                        waitmodel.JCFY = exhaustFy.ToString("0.0");
                                        exhaustBZ += "(" + linestring + "自)";
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                        waitmodel.JCFF = "ZYJS";
                                    }
                                    else
                                    {
                                        MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                if (linestring != "-2")
                                {
                                    if (model.SYXZ.Contains("出租") || model.SYXZ.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "自)";
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "ZYJS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                    return;
                                }
                            }
                        }

                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                            if (linestring != "-2")
                            {

                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "自)";
                                 MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "ZYJS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置自由加速不透光线，该车不能参检");
                                return;
                            }
                        }
                    }
                    else
                    {                        
                        if (model.CLLX.StartsWith("M"))//摩托车双怠速
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速(摩)检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDSM";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不可摘除")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    exhaustBZ += "(" + linestring + "双)";
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "SDS";
                                }
                                else
                                {
                                    MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                    return;
                                }
                            }
                            else
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, mainPanel.stationinfmodel.STATIONJCFF);
                                if (linestring != "-2")
                                {
                                    if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += model.SYXZ;
                                    }
                                    if (waitmodel.JCCS == "1")
                                        exhaustFy = exhaustFy;
                                    else if (waitmodel.JCCS == "2")
                                    {
                                        exhaustFy = 0;
                                        exhaustBZ += "第一次复检";
                                    }
                                    else
                                    {
                                        exhaustFy = exhaustFy / 2.0;
                                        exhaustBZ += "二次以上复检";
                                    }
                                    totalFy = exhaustFy + safeFy;
                                    //fy = exhaustFy.ToString("0.0");
                                    waitmodel.JCFY = exhaustFy.ToString("0.0");
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                    {
                                        exhaustBZ += "(" + linestring + "稳)";
                                       MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    waitmodel.JCFF = mainPanel.stationinfmodel.STATIONJCFF;
                                }
                                else
                                {
                                    if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                        MessageBox.Show("该检测站未配置稳态工况法线，该车不能参检");
                                    else
                                        MessageBox.Show("该检测站未配置简易瞬态工况工况法线，该车不能参检");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                            if (linestring != "-2")
                            {
                                if (comboBoxSYXZ.Text.Contains("出租") || comboBoxSYXZ.Text.Contains("公交"))
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += model.SYXZ;
                                }
                                if (waitmodel.JCCS == "1")
                                    exhaustFy = exhaustFy;
                                else if (waitmodel.JCCS == "2")
                                {
                                    exhaustFy = 0;
                                    exhaustBZ += "第一次复检";
                                }
                                else
                                {
                                    exhaustFy = exhaustFy / 2.0;
                                    exhaustBZ += "二次以上复检";
                                }
                                totalFy = exhaustFy + safeFy;
                                //fy = exhaustFy.ToString("0.0");
                                waitmodel.JCFY = exhaustFy.ToString("0.0");
                                exhaustBZ += "(" + linestring + "双)";
                                 MessageBox.Show("环保:请到" + linestring + "进行双怠速检测");//+，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }


                    waitmodel.XSLC = textBoxXSLC.Text;
                    string addmsg = "";
                    bool addSuccess = true;
                    
                    if (true)
                    {
                        string addmessage = logininfcontrol.addCarToWaitList(waitmodel);
                        if (addmessage == "环检添加成功")
                        {
                            addmsg += "环检添加成功.";
                            listBoxCarAtWait.Items.Add(model.CLHP);
                            listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "成功";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                        else
                        {
                            addmsg += "未知原因导致环检添加失败.";
                            addSuccess = false;
                            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                            mainPanel.worklogdata.ProjectName = "操作日志";
                            mainPanel.worklogdata.Stationid = mainPanel.stationid;
                            mainPanel.worklogdata.Lineid = "00";
                            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                            mainPanel.worklogdata.Data = "注册车辆" + waitmodel.CLHP;
                            mainPanel.worklogdata.State = "失败";
                            mainPanel.worklogdata.Result = "";
                            mainPanel.worklogdata.Date = DateTime.Now;
                            mainPanel.worklogdata.Bzsm = "";
                            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                        }
                    }
                    if (addSuccess)
                    {
                        groupBox1.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        buttonSave.Enabled = false;
                        buttonClear.Enabled = false;
                        moneymodel.CLID = waitmodel.CLID;
                        moneymodel.CLHP = waitmodel.CLHP;
                        moneymodel.DLSJ = waitmodel.DLSJ;
                        moneymodel.SAFE = "0";
                        moneymodel.EXHAUST = "1";
                        moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                        moneymodel.SAFEFY = safeFy.ToString("0.0");
                        moneymodel.SAFEBZ = safeBZ;
                        moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                        moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                        moneymodel.EXHAUSTBZ = exhaustBZ + "(" + cxlb + "）";
                        moneymodel.FY = totalFy.ToString("0.0");
                        moneymodel.SKR = mainPanel.nowUser.userName;
                        moneymodel.SFSJ = DateTime.Now;
                        moneymodel.TF = "0";
                        moneymodel.TFFY = "0.0";
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                    #endregion
                }
            }
        }
        /*
        /// <summary>
        /// 用来提供Stream对象的函数，用于LocalReport对象的Render方法的第三个参数。
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileNameExtension"></param>
        /// <param name="encoding"></param>
        /// <param name="mimeType"></param>
        /// <param name="willSeek"></param>
        /// <returns></returns>
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        /// <summary>
        /// 为Report.rdlc创建本地报告加载数据,输出报告到.emf文件,并打印,同时释放资源
        /// </summary>
        /// <param name="rv">参数:ReportViewer.LocalReport</param>
        public void PrintStream(LocalReport rvDoc)
        {
            //获取LocalReport中的报表页面方向
            isLandSapces = rvDoc.GetDefaultPageSettings().IsLandscape;
            Export(rvDoc);
            PrintSetting();
            Dispose();
        }

        private void Export(LocalReport report)
        {
            string deviceInfo = "<DeviceInfo>" +
                           "  <OutputFormat>EMF</OutputFormat>" +
                           "  <PageWidth>7.0in</PageWidth>" +
                           "  <PageHeight>3.35in</PageHeight>" +
                           "  <MarginTop>0.0in</MarginTop>" +
                           "  <MarginLeft>0.0in</MarginLeft>" +
                           "  <MarginRight>0.0in</MarginRight>" +
                           "  <MarginBottom>0.0in</MarginBottom>" +
                           "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            //将报表的内容按照deviceInfo指定的格式输出到CreateStream函数提供的Stream中。
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintSetting()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("错误:没有检测到打印数据流");
            //声明PrintDocument对象用于数据的打印
            PrintDocument printDoc = new PrintDocument();
            //获取配置文件的清单打印机名称
            System.Configuration.AppSettingsReader appSettings = new System.Configuration.AppSettingsReader();
            printDoc.PrinterSettings.PrinterName = mainPanel.printerName;// appSettings.GetValue(mainPanel.printerName, Type.GetType("System.String")).ToString();
            printDoc.PrintController = new System.Drawing.Printing.StandardPrintController();//指定打印机不显示页码 
            //判断指定的打印机是否可用
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("错误:找不到打印机");
            }
            else
            {
                //设置打印机方向遵从报表方向
                printDoc.DefaultPageSettings.Landscape = isLandSapces;
                //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                //设置打印机打印份数
                printDoc.PrinterSettings.Copies = 1;
                //执行打印操作，Print方法将触发PrintPage事件。
                printDoc.Print();
            }
        }

        /// <summary>
        /// 处理程序PrintPageEvents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ev"></param>
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            //Metafile对象用来保存EMF或WMF格式的图形，
            //我们在前面将报表的内容输出为EMF图形格式的数据流。
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //调整打印机区域的边距
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
               ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width ,
                ev.PageBounds.Height);

            //绘制一个白色背景的报告
            //ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

            //获取报告内容
            //这里的Graphics对象实际指向了打印机
            ev.Graphics.DrawImage(pageImage,adjustedRect);
            //ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            // 准备下一个页,已确定操作尚未结束
            m_currentPageIndex++;

            //设置是否需要继续打印
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }*/
        
        //声明一个Stream对象的列表用来保存报表的输出数据 
        //LocalReport对象的Render方法会将报表按页输出为多个Stream对象。 

        //用来提供Stream对象的函数，用于LocalReport对象的Render方法的第三个参数。
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。  
            //Stream stream = new MemoryStream(); 
            Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }


        private void Print()
        {
            //m_currentPageIndex = 0;
            
            if (m_streams == null || m_streams.Count == 0)
                return;
            //声明PrintDocument对象用于数据的打印 
            PrintDocument printDoc = new PrintDocument();
            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机 
            printDoc.PrinterSettings.PrinterName = mainPanel.printerName;
            //printDoc.OriginAtMargins = false;
            
            //判断指定的打印机是否可用  
            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show("Can't find printer");
                return;
            }
            //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。
            PrinterSettings.PaperSizeCollection papersize = printDoc.PrinterSettings.PaperSizes;
            printDoc.DefaultPageSettings.PaperSize=new PaperSize("票据",748,366);
            //printDoc.PrinterSettings.PaperSizes  = new PrinterSettings.PaperSizeCollection("A4");
            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.PrinterSettings.Copies = 1;//设置打印份数
            //执行打印操作，Print方法将触发PrintPage事件。 
            printDoc.Print();
            //printDoc.Print(); 
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            
            //Metafile对象用来保存EMF或WMF格式的图形， 
            //我们在前面将报表的内容输出为EMF图形格式的数据流。
            m_streams[m_currentPageIndex].Position = 0;
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);
            //指定是否横向打印  
            ev.PageSettings.Landscape = false;
            //这里的Graphics对象实际指向了打印机  
            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);
            ev.Graphics.DrawImage(pageImage, adjustedRect);
            m_streams[m_currentPageIndex].Close();
            m_currentPageIndex++;
            //设置是否需要继续打印  
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);

        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.NHNETMODE)
            {
                string jylsh = listBoxCarAtWait.SelectedItem.ToString().Split(' ')[1];
                int nhcode, nhexpcode;
                string nhmsg, nhexpmsg;
                if (!mainPanel.nhinterface.SendVehicleInspectionCancel(jylsh, out nhcode, out nhmsg, out nhexpcode, out nhexpmsg))
                {
                    MessageBox.Show("联网平台删除失败，code:" + nhcode + "msg:" + nhmsg, "系统提示");
                    return;
                }
                else
                {
                    CARATWAIT model = logininfcontrol.getCarInfatWaitList(listBoxCarAtWait.SelectedItem.ToString().Split(' ')[0]);
                    if (model.CLID != "-2")
                    {
                        logininfcontrol.deleteCarAtWaitbyPlate(listBoxCarAtWait.SelectedItem.ToString().Split(' ')[0]);
                    }
                    init_waitList();
                    MessageBox.Show("删除成功", "系统提示");
                    mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                    mainPanel.worklogdata.ProjectName = "操作日志";
                    mainPanel.worklogdata.Stationid = mainPanel.stationid;
                    mainPanel.worklogdata.Lineid = "00";
                    mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                    mainPanel.worklogdata.Data = "删除待检车辆" + model.CLHP;
                    mainPanel.worklogdata.State = "成功";
                    mainPanel.worklogdata.Result = "";
                    mainPanel.worklogdata.Date = DateTime.Now;
                    mainPanel.worklogdata.Bzsm = "";
                    mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                }
            }
            else if(mainPanel.isNetUsed&mainPanel.netMode==mainPanel.JXNETMODE)
            {
                string jylsh = listBoxCarAtWait.SelectedItem.ToString().Split(' ')[1];
                string code, msg;
                if (!mainPanel.jxinterface.cancel(jylsh, out code, out msg))
                {
                    MessageBox.Show("cancel失败,\r\ncode:" + code + "\r\nmessage:" + msg);
                    return;
                }
                init_waitList();
            }
            else
            {
                CARATWAIT model = logininfcontrol.getCarInfatWaitList(listBoxCarAtWait.SelectedItem.ToString().Split(' ')[0]);
                CARINF modelcarinf = logininfcontrol.getCarInfbyPlate(listBoxCarAtWait.SelectedItem.ToString().Split(' ')[0]);
                if (model.CLID != "-2")
                {
                    if (MessageBox.Show("该操作不可恢复，是否确认删除待检车辆" + model.CLHP + "？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        logininfcontrol.deleteCarAtWaitbyPlate(listBoxCarAtWait.SelectedItem.ToString());
                    listBoxCarAtWait.Items.Remove(listBoxCarAtWait.SelectedItem.ToString());
                    listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                        if (mainPanel.moneyinfcontrol.checkMoneyIsAlive(model.CLID))//未缴费,更改费用信息
                        {

                            double exhaustfy = 0.0;
                            double safefy = 0.0;
                            MONEY moneymodel = mainPanel.moneyinfcontrol.GetMoneyInf(model.CLID);
                            if (moneymodel.SAFE == "0")//如果不检安检，则直接删除费用信息
                            {
                                mainPanel.moneyinfcontrol.deleteOneMoney(model.CLID);
                                MessageBox.Show("删除成功", "系统提示");
                                mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                                mainPanel.worklogdata.ProjectName = "操作日志";
                                mainPanel.worklogdata.Stationid = mainPanel.stationid;
                                mainPanel.worklogdata.Lineid = "00";
                                mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                                mainPanel.worklogdata.Data = "删除待检车辆" + model.CLHP;
                                mainPanel.worklogdata.State = "成功";
                                mainPanel.worklogdata.Result = "";
                                mainPanel.worklogdata.Date = DateTime.Now;
                                mainPanel.worklogdata.Bzsm = "";
                                mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                            }
                            else
                            {
                                moneymodel.EXHAUST = "0";
                                moneymodel.EXHAUSTFY = "0.0";
                                moneymodel.EXHAUSTBZ = "未参检";
                                moneymodel.SAFESTANDARDFY = "0.0";
                                safefy = double.Parse(moneymodel.SAFEFY);
                                moneymodel.FY = (exhaustfy + safefy).ToString("0.0");
                                mainPanel.moneyinfcontrol.updateMoneyInf(moneymodel);
                                MessageBox.Show("删除成功，该车安检还未缴费，请前往收费台缴费", "系统提示");

                            }
                        }
                        else//已缴费,添加退费记录
                        {
                            double exhaustfy = 0.0;
                            double safefy = 0.0;
                            double tffy = 0.0;
                            DateTime deletetime = DateTime.Now;
                            string clid = model.CLHP + "T" + deletetime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                            MONEY moneymodel = mainPanel.moneyrecordinfcontrol.GetMoneyRecordInf(model.CLID);
                            if (moneymodel.CLID != "-2")
                            {
                                tffy = -double.Parse(moneymodel.EXHAUSTFY);
                                if (tffy != 0)
                                {
                                    moneymodel.CLID = clid;
                                    moneymodel.CLHP = model.CLHP;
                                    moneymodel.DLSJ = deletetime;
                                    moneymodel.SAFE = "0";
                                    moneymodel.EXHAUST = "0";
                                    moneymodel.SAFESTANDARDFY = "100.0";
                                    moneymodel.SAFEFY = "0.0";
                                    moneymodel.SAFEBZ = "";
                                    moneymodel.EXHAUSTSTANDARDFY = exhaustfy.ToString("0.0");
                                    moneymodel.EXHAUSTFY = exhaustfy.ToString("0.0");
                                    moneymodel.EXHAUSTBZ = "";
                                    moneymodel.FY = tffy.ToString("0.0");
                                    moneymodel.SKR = mainPanel.nowUser.userName;
                                    moneymodel.SFSJ = DateTime.Now;
                                    moneymodel.TF = "1";
                                    moneymodel.TFFY = tffy.ToString("0.0");
                                    moneymodel.CZ = modelcarinf.CZ;
                                    mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);
                                    MessageBox.Show("删除成功", "系统提示");
                                    mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                                    mainPanel.worklogdata.ProjectName = "操作日志";
                                    mainPanel.worklogdata.Stationid = mainPanel.stationid;
                                    mainPanel.worklogdata.Lineid = "00";
                                    mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                                    mainPanel.worklogdata.Data = "删除待检车辆" + model.CLHP;
                                    mainPanel.worklogdata.State = "成功";
                                    mainPanel.worklogdata.Result = "";
                                    mainPanel.worklogdata.Date = DateTime.Now;
                                    mainPanel.worklogdata.Bzsm = "";
                                    mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                                }
                                else
                                {
                                    MessageBox.Show("删除成功", "系统提示");
                                    mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                                    mainPanel.worklogdata.ProjectName = "操作日志";
                                    mainPanel.worklogdata.Stationid = mainPanel.stationid;
                                    mainPanel.worklogdata.Lineid = "00";
                                    mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
                                    mainPanel.worklogdata.Data = "删除待检车辆" + model.CLHP;
                                    mainPanel.worklogdata.State = "成功";
                                    mainPanel.worklogdata.Result = "";
                                    mainPanel.worklogdata.Date = DateTime.Now;
                                    mainPanel.worklogdata.Bzsm = "";
                                    mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
                                }
                            }
                        }
                    }
                    //MessageBox.Show("删除成功，退费："+model.JCFY+"元","系统提示");
                }
            }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            init_waitList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string plateSearch = textBoxPlateAtWait.Text.ToUpper();
            for (int i=0;i<listBoxCarAtWait.Items.Count;i++)
            {
                if (listBoxCarAtWait.Items[i].ToString() == plateSearch)
                {
                    listBoxCarAtWait.SelectedIndex = i;
                    return;
                }
            }
            MessageBox.Show("未找到该车辆,请先登录");
        }

        private void textBoxZBZL_TextChanged(object sender, EventArgs e)
        {
            if(textBoxZBZL.Text!="")
                textBoxJZZL.Text = (int.Parse(textBoxZBZL.Text) + 100).ToString();
            else
                textBoxJZZL.Text ="0";
        }
        private void onlyNumber(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == 46)
                {
                    if (this.Text.Length <= 0)
                        e.Handled = true;
                }

            }
            catch (Exception)
            {
                return;
            }
        }
        private void onlyIntNumber(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8))
                {
                    e.Handled = true;
                }

            }
            catch (Exception)
            {
                return;
            }
        }

        private void textBoxHDZK_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyIntNumber(sender, e);
        }

        private void comboBoxCLPH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                comboBoxCLPH.Text = "鄂" + e.KeyCode.ToString();
            }
        }

        private void textBoxPP_Validated(object sender, EventArgs e)
        {
            DataTable dt = logininfcontrol.getClxhbySB(textBoxPP.Text.Trim());
            comboBoxXH.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxXH.Items.Add(dt.Rows[i]["CLXH"].ToString());
            }
        }

        private void comboBoxXH_Validated(object sender, EventArgs e)
        {
            vehicleInfModel model = new vehicleInfModel();
            if (comboBoxXH.Text != "")
            {
                model = logininfcontrol.getVehicleInfbyClxh(comboBoxXH.Text);
                textBoxFDJXH.Text = model.FDJXH;
                textBoxSCS.Text = model.MANUF;
            }
        }
        private DateTime cjdatetime;
        private void buttonPlate_Click(object sender, EventArgs e)
        {
            //bool isSatisfyNewRule = false;
            //bool isStatisfyDetectTime = false;
            //DateTime registerTime = dateZCRQ.Value;
            //int passengersCount = int.Parse(textBoxHDZK.Text);
            //string vehicleStyle = "";
            //bool isVehiclePersonal = false;
            //if (comboBoxSYXZ.Text == "非营运") isVehiclePersonal = true;
            //if (isVehiclePersonal)//如果为私家车
            //{ 

            //}
            foreach (Control control in groupPanel1.Controls)
            {
                if (control is TextBox || control is ComboBox)
                {
                    if (control.Text == "")
                    {
                        MessageBox.Show("请填写完所有车辆信息后再保存", "系统提示");
                        return;
                    }
                }
            }
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.CCNETMODE)
            {
                carinfor.CcVehicleInf vehicleinf = new carinfor.CcVehicleInf();
                string code = "", message = "";
                if (mainPanel.ccsocket.SendLoginBegin(comboBoxCLPH.Text, comboBoxCLLX.Text.Split('_')[0], out code, out message, out vehicleinf))
                {
                    if (code == "1")
                    {
                        isCarLoginOkOnNet = true;
                        labelClhp.Text = "找到信息";
                        comboBoxCPYS.Text = logininfcontrol.getComBoBoxItemsNAME("号牌种类", vehicleinf.VLPNType);
                        comboBoxCLLX.Text = vehicleinf.GAVType + "_" + logininfcontrol.getComBoBoxItemsNAME("车辆类型", vehicleinf.GAVType);
                        textBoxCZ.Text = vehicleinf.OwnerName;
                        comboBoxSYXZ.Text = logininfcontrol.getComBoBoxItemsNAME("使用性质", vehicleinf.OCHA);
                        textBoxPP.Text = "";
                        comboBoxXH.Text = vehicleinf.FactoryPlateModel;
                        textBoxDPHM.Text = vehicleinf.VIN;
                        textBoxFDJH.Text = vehicleinf.EngineNum;
                        textBoxFDJXH.Text = vehicleinf.IUETYPE;
                        textBoxSCS.Text = vehicleinf.IUVMANU;
                        textBoxHDZK.Text = vehicleinf.RatedSeats;
                        textBoxJSSZK.Text = "2";
                        textBoxZZL.Text = vehicleinf.VML;
                        textBoxHDZZL.Text = "";
                        textBoxZBZL.Text = vehicleinf.KerbMass;
                        textBoxJZZL.Text = vehicleinf.BenchmarkMass;
                        try
                        {
                            dateZCRQ.Value = DateTime.Parse(vehicleinf.VRDATE);
                            dateSCRQ.Value = DateTime.Parse(vehicleinf.ProductDate);
                        }
                        catch
                        {
                            ini.INIIO.saveLogInf("日期格式有误");
                        }
                        textBoxFDJPL.Text = vehicleinf.EDSPL;
                        comboBoxRLZL.Text = logininfcontrol.getComBoBoxItemsNAME("燃料种类", vehicleinf.FuelType);
                        textBoxEDGL.Text = vehicleinf.EnginePower;
                        textBoxEDZS.Text = vehicleinf.EngineRatedSpeed;
                        comboBoxBSQXS.Text = logininfcontrol.getComBoBoxItemsNAME("变速器形式", vehicleinf.VariableForm);
                        textBoxDWS.Text = vehicleinf.GearCount;
                        comboBoxGYFS.Text = logininfcontrol.getComBoBoxItemsNAME("供油方式", vehicleinf.OilSupplyWay);
                        comboBoxDPXS.SelectedIndex = 0;
                        comboBoxJQFS.Text = logininfcontrol.getComBoBoxItemsNAME("进气方式", vehicleinf.IntakeWay);
                        textBoxQGS.Text = vehicleinf.CylinderCount;
                        comboBoxQDXS.Text = logininfcontrol.getComBoBoxItemsNAME("驱动形式", vehicleinf.DriveForm);
                        //comboBoxCHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("侧滑装置", model.CHZZ);
                        //comboBoxDLSP.Text = model.DLSP;
                        //comboBoxSRL.Text = model.SFSRL;
                        comboBoxJHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("催化转化器", vehicleinf.HasCCA);
                        comboBoxOBD.Text = (vehicleinf.HasOBD == "1") ? "有" : "没有";
                        //comboBoxDKGY.Text = model.DKGYYB;
                        textBoxLXDH.Text = vehicleinf.Tel;
                        textBoxCZDZ.Text = vehicleinf.Address;
                        //comboBoxJCFS.Text = logininfcontrol.getComBoBoxItemsNAME("检测方式", model.JCFS);
                        //comboBoxJCLB.Text = logininfcontrol.getComBoBoxItemsNAME("检测类别", model.JCLB);
                        comboBoxCLZL.Text = logininfcontrol.getComBoBoxItemsNAME("车辆种类", vehicleinf.VehicleBigType);
                        textBoxSSXQ.Text = vehicleinf.County;
                        //comboBoxSFWDZR.Text = logininfcontrol.getComBoBoxItemsNAME("是否外地转入", model.SFWDZR);
                        //comboBoxSFYQBF.Text = logininfcontrol.getComBoBoxItemsNAME("是否延期报废", model.SFYQBF);
                        textBoxFDJSCQY.Text = vehicleinf.IUEMANU;
                        // textBoxQDLTQY.Text = model.QDLTQY;
                        // textBoxRYPH.Text = model.RYPH;
                        textBoxXSLC.Text = vehicleinf.Mileage;
                    }
                    else
                    {
                        isCarLoginOkOnNet = false;
                        labelClhp.Text = "未找到信息";
                        CARINF model = new CARINF();
                        model = logininfcontrol.getCarInfbyPlate(comboBoxCLPH.Text, comboBoxCPYS.Text);
                        if (model.CLHP != "-2")
                        {
                            //comboBoxCLPH.Text = model.CLHP;
                            comboBoxCPYS.Text = model.CPYS;
                            comboBoxCLLX.Text = model.CLLX + "_" + logininfcontrol.getComBoBoxItemsNAME("车辆类型", model.CLLX);
                            textBoxCZ.Text = model.CZ;
                            comboBoxSYXZ.Text = logininfcontrol.getComBoBoxItemsNAME("使用性质", model.SYXZ);
                            textBoxPP.Text = model.PP;
                            comboBoxXH.Text = model.XH;
                            textBoxDPHM.Text = model.CLSBM;
                            textBoxFDJH.Text = model.FDJHM;
                            textBoxFDJXH.Text = model.FDJXH;
                            textBoxSCS.Text = model.SCQY;
                            textBoxHDZK.Text = model.HDZK;
                            textBoxJSSZK.Text = model.JSSZK;
                            textBoxZZL.Text = model.ZZL;
                            textBoxHDZZL.Text = model.HDZZL;
                            textBoxZBZL.Text = model.ZBZL;
                            textBoxJZZL.Text = model.JZZL;
                            dateZCRQ.Value = model.ZCRQ;
                            dateSCRQ.Value = model.SCRQ;
                            textBoxFDJPL.Text = model.FDJPL;
                            comboBoxRLZL.Text = logininfcontrol.getComBoBoxItemsNAME("燃料种类", model.RLZL);
                            textBoxEDGL.Text = model.EDGL;
                            textBoxEDZS.Text = model.EDZS;
                            comboBoxBSQXS.Text = logininfcontrol.getComBoBoxItemsNAME("变速器形式", model.BSQXS);
                            textBoxDWS.Text = model.DWS;
                            comboBoxGYFS.Text = logininfcontrol.getComBoBoxItemsNAME("供油方式", model.GYFS);
                            comboBoxDPXS.Text = logininfcontrol.getComBoBoxItemsNAME("电喷方式", model.DPFS);
                            comboBoxJQFS.Text = logininfcontrol.getComBoBoxItemsNAME("进气方式", model.JQFS);
                            textBoxQGS.Text = model.QGS;
                            comboBoxQDXS.Text = logininfcontrol.getComBoBoxItemsNAME("驱动形式", model.QDXS);
                            comboBoxCHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("侧滑装置", model.CHZZ);
                            comboBoxDLSP.Text = model.DLSP;
                            comboBoxSRL.Text = model.SFSRL;
                            comboBoxJHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("催化转化器", model.JHZZ);
                            comboBoxOBD.Text = model.OBD;
                            comboBoxDKGY.Text = model.DKGYYB;
                            textBoxLXDH.Text = model.LXDH;
                            textBoxCZDZ.Text = model.CZDZ;
                            comboBoxJCFS.Text = logininfcontrol.getComBoBoxItemsNAME("检测方式", model.JCFS);
                            comboBoxJCLB.Text = logininfcontrol.getComBoBoxItemsNAME("检测类别", model.JCLB);
                            comboBoxCLZL.Text = logininfcontrol.getComBoBoxItemsNAME("车辆种类", model.CLZL);
                            textBoxSSXQ.Text = model.SSXQ;
                            comboBoxSFWDZR.Text = logininfcontrol.getComBoBoxItemsNAME("是否外地转入", model.SFWDZR);
                            comboBoxSFYQBF.Text = logininfcontrol.getComBoBoxItemsNAME("是否延期报废", model.SFYQBF);
                            textBoxFDJSCQY.Text = model.FDJSCQY;
                            textBoxQDLTQY.Text = model.QDLTQY;
                            textBoxRYPH.Text = model.RYPH;
                        }
                        MessageBox.Show("车辆未受理无法联网检测");
                        return;
                    }
                }
                else
                {
                    isCarLoginOkOnNet = false;
                    labelClhp.Text = "未找到信息";
                    CARINF model = new CARINF();
                    model = logininfcontrol.getCarInfbyPlate(comboBoxCLPH.Text, comboBoxCPYS.Text);
                    if (model.CLHP != "-2")
                    {
                        //comboBoxCLPH.Text = model.CLHP;
                        comboBoxCPYS.Text = model.CPYS;
                        comboBoxCLLX.Text = model.CLLX + "_" + logininfcontrol.getComBoBoxItemsNAME("车辆类型", model.CLLX);
                        textBoxCZ.Text = model.CZ;
                        comboBoxSYXZ.Text = logininfcontrol.getComBoBoxItemsNAME("使用性质", model.SYXZ);
                        textBoxPP.Text = model.PP;
                        comboBoxXH.Text = model.XH;
                        textBoxDPHM.Text = model.CLSBM;
                        textBoxFDJH.Text = model.FDJHM;
                        textBoxFDJXH.Text = model.FDJXH;
                        textBoxSCS.Text = model.SCQY;
                        textBoxHDZK.Text = model.HDZK;
                        textBoxJSSZK.Text = model.JSSZK;
                        textBoxZZL.Text = model.ZZL;
                        textBoxHDZZL.Text = model.HDZZL;
                        textBoxZBZL.Text = model.ZBZL;
                        textBoxJZZL.Text = model.JZZL;
                        dateZCRQ.Value = model.ZCRQ;
                        dateSCRQ.Value = model.SCRQ;
                        textBoxFDJPL.Text = model.FDJPL;
                        comboBoxRLZL.Text = logininfcontrol.getComBoBoxItemsNAME("燃料种类", model.RLZL);
                        textBoxEDGL.Text = model.EDGL;
                        textBoxEDZS.Text = model.EDZS;
                        comboBoxBSQXS.Text = logininfcontrol.getComBoBoxItemsNAME("变速器形式", model.BSQXS);
                        textBoxDWS.Text = model.DWS;
                        comboBoxGYFS.Text = logininfcontrol.getComBoBoxItemsNAME("供油方式", model.GYFS);
                        comboBoxDPXS.Text = logininfcontrol.getComBoBoxItemsNAME("电喷方式", model.DPFS);
                        comboBoxJQFS.Text = logininfcontrol.getComBoBoxItemsNAME("进气方式", model.JQFS);
                        textBoxQGS.Text = model.QGS;
                        comboBoxQDXS.Text = logininfcontrol.getComBoBoxItemsNAME("驱动形式", model.QDXS);
                        comboBoxCHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("侧滑装置", model.CHZZ);
                        comboBoxDLSP.Text = model.DLSP;
                        comboBoxSRL.Text = model.SFSRL;
                        comboBoxJHZZ.Text = logininfcontrol.getComBoBoxItemsNAME("催化转化器", model.JHZZ);
                        comboBoxOBD.Text = model.OBD;
                        comboBoxDKGY.Text = model.DKGYYB;
                        textBoxLXDH.Text = model.LXDH;
                        textBoxCZDZ.Text = model.CZDZ;
                        comboBoxJCFS.Text = logininfcontrol.getComBoBoxItemsNAME("检测方式", model.JCFS);
                        comboBoxJCLB.Text = logininfcontrol.getComBoBoxItemsNAME("检测类别", model.JCLB);
                        comboBoxCLZL.Text = logininfcontrol.getComBoBoxItemsNAME("车辆种类", model.CLZL);
                        textBoxSSXQ.Text = model.SSXQ;
                        comboBoxSFWDZR.Text = logininfcontrol.getComBoBoxItemsNAME("是否外地转入", model.SFWDZR);
                        comboBoxSFYQBF.Text = logininfcontrol.getComBoBoxItemsNAME("是否延期报废", model.SFYQBF);
                        textBoxFDJSCQY.Text = model.FDJSCQY;
                        textBoxQDLTQY.Text = model.QDLTQY;
                        textBoxRYPH.Text = model.RYPH;
                    }
                    MessageBox.Show("车辆未受理无法联网检测");
                    return;
                }
            }
            else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.ORTNETMODE)
            {
                string code = "", message = "";
                if (!mainPanel.ortsocket.SendLoginBegin(comboBoxCLPH.Text, comboBoxCPYS.Text, mainPanel.stationid, out code, out message))
                {
                    MessageBox.Show("该车不能参检，原因："+message);
                    return;

                }
            }
            string cjclph = comboBoxCLPH.Text;
            string cjcpys = comboBoxCPYS.Text;
            CARDETECTED latestRecord = logininfcontrol.getPreTestDatebyPlate(cjclph,cjcpys);
            CARDETECTED latestcjRecord = logininfcontrol.getPreCjTestDatebyPlate(cjclph, cjcpys);
            CARSAFEDETECTED latestSafeRecord = logininfcontrol.getPreSafeTestDatebyPlate(cjclph);
            if (true)//只参加环保
            {
                if (latestRecord.CLID == "-2")//如果没有记录，则提示为初检
                {
                    if (MessageBox.Show("该车为初次环保检测，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                    {
                        cjdatetime = DateTime.Now;
                        //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                        textBoxJCCS.Text = "1";
                        groupBox1.Enabled = true;
                        groupBox2.Enabled = true;
                        groupBox3.Enabled = true;
                        buttonSave.Enabled = true;
                        buttonClear.Enabled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                else//如果有环保记录，则判定记录内容
                {
                    int latestMonths = caculateMonth(latestRecord.JCSJ, DateTime.Now);//计算最近的检测记录距离此次的时间
                    if (latestMonths > 9)//如果距离超过9个月，则认为达到检测时间
                    {
                        if (MessageBox.Show("该车为年检车辆，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                        {
                            cjdatetime = DateTime.Now;
                            //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                            textBoxJCCS.Text = "1";
                            groupBox1.Enabled = true;
                            groupBox2.Enabled = true;
                            groupBox3.Enabled = true;
                            buttonSave.Enabled = true;
                            buttonClear.Enabled = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (latestRecord.JCJG == "合格")//如果有记录上次检测已经合格，则提示相关信息
                        {
                            if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测合格，确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                            {
                                cjdatetime = DateTime.Now;
                                //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                textBoxJCCS.Text = "1";
                                groupBox1.Enabled = true;
                                groupBox2.Enabled = true;
                                groupBox3.Enabled = true;
                                buttonSave.Enabled = true;
                                buttonClear.Enabled = true;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else//如果记录不合格，则计算复检次数
                        {
                            int days = caculateDays(latestRecord.JCSJ, DateTime.Now);
                            if (days > 30)//如果超过复检规定时间，则视为初次检测，缴全款
                            {
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，已超过复检时间，确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    cjdatetime = DateTime.Now;
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    textBoxJCCS.Text = "1";
                                    groupBox1.Enabled = true;
                                    groupBox2.Enabled = true;
                                    groupBox3.Enabled = true;
                                    buttonSave.Enabled = true;
                                    buttonClear.Enabled = true;
                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                int jccs = int.Parse(latestRecord.JCCS) + 1;
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，此次为第" + (jccs - 1).ToString() + "次复检，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    if (latestcjRecord.CLID != "-2")
                                    {
                                        cjdatetime = latestcjRecord.JCSJ;
                                    }
                                    else
                                    {
                                        cjdatetime = DateTime.Now;
                                    }
                                    textBoxJCCS.Text = jccs.ToString();
                                    groupBox1.Enabled = true;
                                    groupBox2.Enabled = true;
                                    groupBox3.Enabled = true;
                                    buttonSave.Enabled = true;
                                    buttonClear.Enabled = true;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                }
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
                buttonSave.Enabled = true;
                buttonClear.Enabled = true;
            }
            else
            {
                MessageBox.Show("请选择年检项目", "系统提示");
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                buttonSave.Enabled = false;
                buttonClear.Enabled = false;
            }
        }

        private int caculateMonth(DateTime datetime1, DateTime datetime2)
        {
            int year1 = datetime1.Year;
            int year2 = datetime2.Year;
            int month1 = datetime1.Month;
            int month2 = datetime2.Month;
            int months = 12 * (year2 - year1) + (month2 - month1); 
            return months;
        }
        private int caculateDays(DateTime datetime1, DateTime datetime2)
        {
            int year1 = datetime1.Year;
            int year2 = datetime2.Year;
            int month1 = datetime1.Month;
            int month2 = datetime2.Month;
            int day1=datetime1.Day;
            int day2=datetime2.Day;
            int days = 365 * (year2 - year1) + 30*(month2 - month1)+day2-day1;
            return days;
        }

        private void dateZCRQ_ValueChanged(object sender, EventArgs e)
        {
            dateSCRQ.Value = dateZCRQ.Value;
        }

        private void comboBoxRLZL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mainPanel.isNetUsed || (mainPanel.netMode != mainPanel.NHNETMODE&& mainPanel.netMode != mainPanel.JXNETMODE && mainPanel.netMode != mainPanel.DALINETMODE&&mainPanel.netMode!=mainPanel.ORTNETMODE && mainPanel.netMode != mainPanel.EZNETMODE))
            {
                DataTable dt = null;
                comboBoxJHZZ.Items.Clear();
                if (comboBoxRLZL.Text.Contains("柴"))
                {
                    label45.Text = "排气后处理装置";
                    dt = logininfcontrol.getComBoBoxItemsInf("排气后处理装置");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comboBoxJHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                    }
                    comboBoxJHZZ.SelectedIndex = 0;
                }
                else
                {
                    label45.Text = "催化转化器情况";
                    dt = logininfcontrol.getComBoBoxItemsInf("催化转化器");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        comboBoxJHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                    }
                    comboBoxJHZZ.SelectedIndex = 0;
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void listBoxCarAtWait_DoubleClick(object sender, EventArgs e)
        {

            if (listBoxCarAtWait.Items.Count > 0)
            {
                CARATWAIT model = logininfcontrol.getCarInfatWaitList(listBoxCarAtWait.SelectedItem.ToString().Split(' ')[0]);
                comboBoxCLPH.Text = model.CLHP;
                comboBoxCPYS.Text = model.CPYS;
                comboBoxCLPH_Validated(sender, e);
            }
        }

        private void buttonJudgePfbz_Click(object sender, EventArgs e)
        {
            DateTime scrq = dateSCRQ.Value;
            int intvalue = 0;
            if (textBoxZZL.Text == "")
            {
                MessageBox.Show("总质量不能为空");
                return;
            }
            if (int.TryParse(textBoxZZL.Text, out intvalue) == false)
            {
                MessageBox.Show("总质量非有效整数");
                return;
            }
            int zzl = intvalue;
            if (textBoxHDZK.Text == "")
            {
                MessageBox.Show("载客人数不能为空");
                return;
            }
            if (int.TryParse(textBoxHDZK.Text, out intvalue) == false)
            {
                MessageBox.Show("载客人数非有效整数");
                return;
            }
            int zkrs = intvalue;
            if (comboBoxRLZL.Text == "")
            {
                MessageBox.Show("燃料种类不能为空");
                return;
            }
            string rlzl = comboBoxRLZL.Text;
            if (comboBoxOBD.Text == "")
            {
                MessageBox.Show("是否有OBD不能为空");
                return;
            }
            string cllx = comboBoxCLLX.Text;
            bool isQx = (zzl <= 3500);
            bool isZk = cllx.StartsWith("K");
            bool isM = isZk;//所有载客车都认为是M
            bool isYlQx = ((zzl <= 2500) && (zkrs <= 6));
            bool isOBD = ((comboBoxOBD.Text == "是") || (comboBoxOBD.Text == "有"));
            int zxbz = 1;
            if(isQx)
            {
                if(rlzl.Contains("柴"))
                {
                    if (isYlQx)
                    {
                        if (DateTime.Compare(scrq, Convert.ToDateTime("2000-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 0;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2005-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 1;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2013-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            if (isM)
                            {
                                if (DateTime.Compare(scrq, Convert.ToDateTime("2008-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                                {
                                    zxbz = 2;
                                }
                                else
                                    zxbz = 3;
                            }
                            else
                            {
                                if (DateTime.Compare(scrq, Convert.ToDateTime("2009-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                                {
                                    zxbz = 2;
                                }
                                else
                                    zxbz = 3;
                            }
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2018-01-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 4;
                        }
                        else
                            zxbz = 5;
                    }
                    else
                    {
                        if (DateTime.Compare(scrq, Convert.ToDateTime("2001-10-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 0;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2006-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 1;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2013-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            if (isM)
                            {
                                if (DateTime.Compare(scrq, Convert.ToDateTime("2008-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                                {
                                    zxbz = 2;
                                }
                                else
                                    zxbz = 3;
                            }
                            else
                            {
                                if (DateTime.Compare(scrq, Convert.ToDateTime("2009-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                                {
                                    zxbz = 2;
                                }
                                else
                                    zxbz = 3;
                            }
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2018-01-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 4;
                        }
                        else
                            zxbz = 5;
                    }
                }
                else
                {
                    if (isYlQx)
                    {
                        if (DateTime.Compare(scrq, Convert.ToDateTime("2000-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 0;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2005-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 1;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2011-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            if (isOBD)
                            {
                                if (DateTime.Compare(scrq, Convert.ToDateTime("2009-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                                {
                                    zxbz = 2;
                                }
                                else
                                    zxbz = 3;
                            }
                            else
                            {
                                if (DateTime.Compare(scrq, Convert.ToDateTime("2008-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                                {
                                    zxbz = 2;
                                }
                                else
                                    zxbz = 3;
                            }
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2017-01-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 4;
                        }
                        else
                            zxbz = 5;
                    }
                    else
                    {
                        if (DateTime.Compare(scrq, Convert.ToDateTime("2001-10-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 0;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2006-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 1;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2008-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 2;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2011-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 3;
                        }
                        else if (DateTime.Compare(scrq, Convert.ToDateTime("2017-01-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                        {
                            zxbz = 4;
                        }
                        else
                            zxbz = 5;
                    }
                }
            }
            else
            {
                if (rlzl.Contains("气"))
                {
                    if (DateTime.Compare(scrq, Convert.ToDateTime("2003-09-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 0;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2004-09-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 1;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2008-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 2;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2011-01-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 3;
                    }
                    else
                        zxbz = 4;
                }
                else if (rlzl.Contains("柴"))
                {
                    if (DateTime.Compare(scrq, Convert.ToDateTime("2001-09-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 0;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2004-09-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 1;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2008-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 2;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2013-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 3;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2017-01-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 4;
                    }
                    else
                        zxbz = 5;
                }
                else
                {
                    if (DateTime.Compare(scrq, Convert.ToDateTime("2003-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 0;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2004-09-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 1;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2010-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 2;
                    }
                    else if (DateTime.Compare(scrq, Convert.ToDateTime("2013-07-01")) < 0)    //2000年7月1日前生产的第一类轻型汽车
                    {
                        zxbz = 3;
                    }
                    else
                        zxbz = 4;
                }
            }
            comboBoxPFBZ.SelectedIndex = zxbz;
        }
    }
}
