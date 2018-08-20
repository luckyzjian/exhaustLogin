﻿using System;
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
using Newtonsoft;

namespace exhaustLogin
{
    public partial class carLoginHHZ : Form
    {
        private loginInfModel logininfmodel = new loginInfModel();
        private loginInfControl logininfcontrol = new loginInfControl();
        private stationControl stationcontrol = new stationControl();
        private bool isCarLoginOkOnNet = false;
        private HhzWebClient.HhzQuery hhzquery = null;
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

        public carLoginHHZ()
        {
            InitializeComponent();
        }
        private void init_notNotNeededControl()
        {
            // "comboBoxDLSP", "textBoxDWS", "textBoxQGS", "comboBoxOBD", "comboBoxSRL", "textBoxEDZS", "textBoxRYPH", "comboBoxDKGY", "textBoxQDLTQY", "textBoxSCS", "textBoxFDJSCQY"
            asmNotNeedControl.Add( "comboBoxDLSP");

        }
        private void initQuery()
        {
            if(mainPanel.hhzwebinf.enableQuery)
            {
                hhzquery = new HhzWebClient.HhzQuery(mainPanel.hhzwebinf.queryUrl,mainPanel.gawebinf.jyjgbh);
            }
        }
        private void carLogin_Load(object sender, EventArgs e)
        {
            this.Font = new Font("宋体", (float)mainPanel.fontSize);
            initQuery();
            init_loginDefault();
            init_waitList();
            //init_safewaitList();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            buttonSave.Enabled = false;
            buttonClear.Enabled = false;
            //buttonPlate.Visible = mainPanel.hhzwebinf.enableQuery;


        }
        private void init_loginDefault()
        {
            logininfmodel = logininfcontrol.getLoginDefaultInf();
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
                comboBoxPFBZ.Items.Add("国0");
                comboBoxPFBZ.Items.Add("国I");
                comboBoxPFBZ.Items.Add("国II");
                comboBoxPFBZ.Items.Add("国III");
                comboBoxPFBZ.Items.Add("国IV");
                comboBoxPFBZ.Items.Add("国V");
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
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.EZNETMODE)
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
            if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.HHZNNETMODE)
            {
                panelExNHINF.Visible = false;
                DataTable dt = null;
                dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
                IDictionaryEnumerator myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_CPYS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCPYS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_HPZL.GetEnumerator();
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
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_SYXZ.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSYXZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_RLZL.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxRLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_BSQXS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxBSQXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_GYFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxGYFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_DPFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxDPXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_JQFS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJQFS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_QDXS.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxQDXS.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_CHZZ.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCHZZ.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_RESULT_JCLX.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJCLB.Items.Add((string)(myEnumerator.Value));
                }
                dt = logininfcontrol.getComBoBoxItemsInf("检测方式");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    comboBoxJCFS.Items.Add(dt.Rows[i]["MC"].ToString());
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_CLZL.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxCLZL.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_HCLTYPE.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxSFYQBF.Items.Add((string)(myEnumerator.Value));
                }

                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetEnumerator();
                while (myEnumerator.MoveNext())//遍历哈希表的值,生成xml节点
                {
                    comboBoxJHZZ.Items.Add((string)(myEnumerator.Value));
                    comboBoxSFWDZR.Items.Add((string)(myEnumerator.Value));
                    //comboBoxSFYQBF.Items.Add((string)(myEnumerator.Value));
                    comboBoxSRL.Items.Add((string)(myEnumerator.Value));
                    comboBoxDLSP.Items.Add((string)(myEnumerator.Value));
                    comboBoxOBD.Items.Add((string)(myEnumerator.Value));
                    comboBoxDKGY.Items.Add((string)(myEnumerator.Value));
                }
                myEnumerator = mainPanel.hhzinterface.Hhz_VEHICLE_PFBZ.GetEnumerator();
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
            clear_loginInf();
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
            int jccs = 0;
            comboBoxCLPH.Text = comboBoxCLPH.Text.ToUpper();
            comboBoxHPZL.SelectedIndex =1;
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

        private void clear_loginInf()
        {
            comboBoxCLPH.Text = logininfmodel.CLHP;
            comboBoxHPZL.SelectedIndex = 1;
            comboBoxCPYS.SelectedIndex = 0;
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

        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clear_loginInf();
            
        }
        private string String2Unicode(string source)
        {
            var bytes = Encoding.Unicode.GetBytes(source);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0:x2}{1:x2}", bytes[i + 1], bytes[i]);
            }
            return stringBuilder.ToString();
        }
        /// <summary>    
        /// Unicode字符串转为正常字符串    
        /// </summary>    
        /// <param name="srcText"></param>    
        /// <returns></returns>    
        private string UnicodeToString(string srcText)
        {
            string dst = "";
            string src = srcText;
            int len = srcText.Length / 6;
            for (int i = 0; i <= len - 1; i++)
            {
                string str = "";
                str = src.Substring(0, 6).Substring(2);
                src = src.Substring(6);
                byte[] bytes = new byte[2];
                bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), System.Globalization.NumberStyles.HexNumber).ToString());
                dst += Encoding.Unicode.GetString(bytes);
            }
            return dst;
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
                        hs_v.Add("EmissionStandard","");
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
                else if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.HHZNNETMODE)
                {
                    #region 红河州
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
                    model.SFYQBF = comboBoxSFYQBF.Text;//用作处理装置类型
                    model.FDJSCQY = textBoxFDJSCQY.Text;
                    model.QDLTQY = textBoxQDLTQY.Text;
                    model.RYPH = textBoxRYPH.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    model.ZXBZ = comboBoxPFBZ.Text;
                    model.HPZL = comboBoxHPZL.Text;
                    logininfcontrol.saveCarInfbyPlate(model);

                    waitmodel.CLHP = comboBoxCLPH.Text;
                    System.Collections.Hashtable hthhz = new System.Collections.Hashtable();
                    System.Collections.Hashtable hthhz2 = new System.Collections.Hashtable();

                    #region 先判断车是否可以检测           

                    hthhz.Add("vin", model.CLSBM);
                    hthhz2.Add("type", "QUERY");
                    hthhz2.Add("filter", hthhz);
                    try
                    {
                        string code, msg;
                        string jsonReturnString = "";
                        if (mainPanel.hhzinterface.queryData(hthhz2, HhzWebClient.Hhzclient.DATALX.CLXXCX, out code, out msg, out jsonReturnString))
                        {
                            try
                            {
                                List<HhzWebClient.hhclxx> clxxlist = new List<HhzWebClient.hhclxx>();
                                clxxlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HhzWebClient.hhclxx>>(jsonReturnString);
                                if (clxxlist.Count > 0)//如果该 vin 有记录则使用已存在carno作为clid
                                {
                                    try
                                    {
                                        HhzWebClient.hhclxx clxx = clxxlist[0];                                        
                                        waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString("yyyyMMddHHmmss");
                                        waitmodel.JYLSH = clxx.carno;
                                    }
                                    catch (Exception er)
                                    {
                                        ini.INIIO.saveLogInf("解析车辆信息出现异常：" + er.Message);
                                        MessageBox.Show("解析车辆信息出现异常：" + er.Message);
                                        return;
                                    }
                                }
                                else//如果没有记录,则使用生成的clid作为carno
                                {
                                    DateTime logindatetime = mainPanel.stationcontrol.getStationLoginLshDate(mainPanel.stationid);
                                    if (logindatetime.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                                    {
                                        mainPanel.stationcontrol.setStationLoginLshDate(mainPanel.stationid);
                                        mainPanel.stationcontrol.setStationLoginLshCount(mainPanel.stationid, "1");

                                    }
                                    int stationcount = mainPanel.stationcontrol.getStationLoginLshInf(mainPanel.stationid);
                                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString("yyyyMMddHHmmss");
                                    waitmodel.JYLSH = mainPanel.stationid + DateTime.Now.ToString("yyyyMMdd") + stationcount.ToString("0000000000");
                                    stationcount++;
                                    mainPanel.stationcontrol.setStationLoginLshCount(mainPanel.stationid, stationcount.ToString());
                                }
                            }
                            catch (Exception er)
                            {
                                ini.INIIO.saveLogInf("解析车辆检测记录出现异常：" + er.Message);
                                MessageBox.Show("解析车辆检测记录出现异常：" + er.Message);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("查询车辆检测记录出现失败：\r\ncode=" + code + "\r\nmsg=" + msg);
                            return;
                        }
                    }
                    catch (Exception er)
                    {
                        ini.INIIO.saveLogInf("查询车辆检测记录出现异常：" + er.Message);
                        MessageBox.Show("查询车辆检测记录出现异常：" + er.Message);
                        return;
                    }
                    #endregion

                    
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = comboBoxCPYS.Text;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "N";
                    waitmodel.ECRYPT = "";
                    waitmodel.XSLC = textBoxXSLC.Text;
                    waitmodel.SFCJ = waitmodel.JCCS == "1" ? "初检" : "复检";
                    waitmodel.JCBGBH = "";
                    if (mainPanel.isNetUsed && mainPanel.netMode == mainPanel.HHZNNETMODE)
                    {
                        #region 红河州联网上传检测线信息
                        hthhz.Clear();
                        hthhz2.Clear();

                        #region 先判断车是否可以检测
                        hthhz.Add("vin", model.CLSBM);
                        hthhz2.Add("type", "QUERY");
                        hthhz2.Add("filter", hthhz);
                        try
                        {
                            string code, msg;
                            string jsonReturnString = "";
                            if (mainPanel.hhzinterface.queryData(hthhz2, HhzWebClient.Hhzclient.DATALX.JCBHGCLXX, out code, out msg, out jsonReturnString))
                            {
                                try
                                {
                                    List<HhzWebClient.hhzbhgjl> bhglist = new List<HhzWebClient.hhzbhgjl>();
                                    bhglist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HhzWebClient.hhzbhgjl>>(jsonReturnString);
                                    if(bhglist.Count>0)
                                    {
                                        if(bhglist[0].tsno!=mainPanel.stationid)
                                        {
                                            MessageBox.Show("该车辆于"+bhglist[0].exchangetime+"在"+bhglist[0].teststation+"检测未通过，请回原检测站进行检测","通知");
                                            return;
                                        }
                                    }
                                }
                                catch (Exception er)
                                {
                                    ini.INIIO.saveLogInf("解析车辆检测记录出现异常：" + er.Message);
                                    MessageBox.Show("解析车辆检测记录出现异常：" + er.Message);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("查询车辆检测记录出现失败：\r\ncode=" + code + "\r\nmsg=" + msg);
                                return;
                            }
                        }
                        catch (Exception er)
                        {
                            ini.INIIO.saveLogInf("查询车辆检测记录出现异常：" + er.Message);
                            MessageBox.Show("查询车辆检测记录出现异常：" + er.Message);
                            return;
                        }
                        #endregion
                        #region 车型信息
                        /*
                        hthhz.Clear();
                        hthhz2.Clear();
                        hthhz.Add("CXBM", model.PP + model.XH);
                        hthhz.Add("CP", model.PP);
                        hthhz.Add("PFBZ", mainPanel.hhzinterface.HhzR_VEHICLE_PFBZ.GetValue(model.ZXBZ, "0"));
                        hthhz.Add("CLLX", model.CLLX.Split('_')[0]);
                        hthhz.Add("ZDZZL", model.ZZL);
                        hthhz.Add("BZZL", model.JZZL);
                        hthhz.Add("QDXS", mainPanel.hhzinterface.HhzR_VEHICLE_QDXS.GetValue(model.QDXS, "0"));
                        hthhz.Add("BSQLX", mainPanel.hhzinterface.HhzR_VEHICLE_BSQXS.GetValue(model.BSQXS, "0"));
                        hthhz.Add("SFODB", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.OBD, "0"));
                        hthhz.Add("EGRXH", "000000");
                        hthhz.Add("KH", model.CLLX.StartsWith("K") ? "K" : "H");
                        hthhz.Add("ZWS", model.HDZK);
                        hthhz.Add("SFJHZZ", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.JHZZ, "0"));
                        hthhz.Add("JWJHQBH1", "0");
                        hthhz.Add("JWJHQBH2", "0");
                        hthhz.Add("YCGQXH1", "0");
                        hthhz.Add("YCGQXH2", "0");
                        hthhz.Add("SFLS", "0");
                        hthhz.Add("CHQBH", "1");
                        hthhz.Add("CLSCC", model.SCQY);
                        hthhz.Add("ZZ", model.HDZZL);
                        hthhz.Add("SCSJ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        hthhz2.Clear();
                        hthhz2.Add("type", "UPLOAD");
                        hthhz2.Add("data", hthhz);
                        try
                        {
                            string code, msg;
                            if (mainPanel.hhzinterface.uploadJsonArray(hthhz2, HhzWebClient.Hhzclient.DATALX.CXXX, out code, out msg))
                            {
                                ini.INIIO.saveLogInf("上传车型信息至联网平台成功");
                            }
                            else
                            {
                                if (msg.Contains("already exists"))
                                {
                                    hthhz2["type"] = "UPDATE";
                                    if (mainPanel.hhzinterface.uploadJsonArray(hthhz2, HhzWebClient.Hhzclient.DATALX.CXXX, out code, out msg))
                                    {
                                        ini.INIIO.saveLogInf("更新车型信息至联网平台成功");
                                    }
                                    else
                                    {
                                        MessageBox.Show("更新车型信息至联网平台失败:code=" + code + ",msg=" + msg);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("上传车型信息至联网平台失败:code=" + code + ",msg=" + msg);
                                    return;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("上传或更新车型信息至联网平台发生异常:" + er.Message);
                            return;
                        }*/
                        #endregion
                        #region 发动机信息
                        /*
                        hthhz.Clear();
                        hthhz2.Clear();
                        hthhz.Add("FDJBM", model.FDJHM);
                        hthhz.Add("CXBM", model.PP + model.XH);
                        hthhz.Add("FDJXH", model.FDJXH);
                        hthhz.Add("FDJEDZS", model.EDZS);
                        hthhz.Add("FDJPL", model.FDJPL);
                        hthhz.Add("FDJEDGL", model.EDGL);
                        hthhz.Add("JQFS", mainPanel.hhzinterface.HhzR_VEHICLE_JQFS.GetValue(model.JQFS, "0"));
                        hthhz.Add("QGSL", model.QGS);
                        hthhz.Add("FDJSCC", model.FDJSCQY);
                        hthhz.Add("RYZL", mainPanel.hhzinterface.HhzR_VEHICLE_RLZL.GetValue(model.RLZL, "0"));
                        hthhz.Add("DPFS", mainPanel.hhzinterface.HhzR_VEHICLE_DPFS.GetValue(model.DPFS, "0"));
                        hthhz.Add("GYFS", mainPanel.hhzinterface.HhzR_VEHICLE_GYFS.GetValue(model.GYFS, "0"));
                        hthhz.Add("SFDK", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.DKGYYB, "0"));
                        hthhz.Add("SCSJ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        hthhz2.Clear();
                        hthhz2.Add("type", "UPLOAD");
                        hthhz2.Add("data", hthhz);
                        try
                        {
                            string code, msg;
                            if (mainPanel.hhzinterface.uploadJsonArray(hthhz2, HhzWebClient.Hhzclient.DATALX.FDJXX, out code, out msg))
                            {
                                ini.INIIO.saveLogInf("上传发动机信息至联网平台成功");
                            }
                            else
                            {
                                if (msg.Contains("already exists"))
                                {
                                    hthhz2["type"] = "UPDATE";
                                    if (mainPanel.hhzinterface.uploadJsonArray(hthhz2, HhzWebClient.Hhzclient.DATALX.FDJXX, out code, out msg))
                                    {
                                        ini.INIIO.saveLogInf("更新发动机信息至联网平台成功");
                                    }
                                    else
                                    {
                                        MessageBox.Show("更新发动机信息至联网平台失败:code=" + code + ",msg=" + msg);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("上传发动机信息至联网平台失败:code=" + code + ",msg=" + msg);
                                    return;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("上传或更新发动机信息至联网平台发生异常:" + er.Message);
                            return;
                        }
                        */
                        #endregion
                        #region 车辆信息
                        hthhz.Clear();
                        hthhz2.Clear();
                        /*
                        hthhz.Add("CLBM", waitmodel.JCBGBH);
                        hthhz.Add("CLZL", mainPanel.hhzinterface.HhzR_VEHICLE_CLZL.GetValue(model.CLZL, "0"));
                        hthhz.Add("CPHM", waitmodel.CLHP);
                        hthhz.Add("CPLX", mainPanel.hhzinterface.HhzR_VEHICLE_CPYS.GetValue(model.CPYS, "0"));
                        hthhz.Add("CLLX", model.CLLX.Split('_')[0]);//5
                        hthhz.Add("VIN", model.CLSBM);
                        hthhz.Add("CDRQ", model.ZCRQ.ToString("yyyy-MM-dd"));// mainPanel.hhzinterface.HhzR_JCZ_JJLX.GetValue(model.JJLX, ""));//5
                        hthhz.Add("CZDW", model.CZ);
                        hthhz.Add("LXDZ", model.CZDZ);
                        hthhz.Add("LXDH", model.LXDH);//10
                        hthhz.Add("HBXXKH", "000000");
                        hthhz.Add("FDJH", model.FDJHM);
                        hthhz.Add("LCBDS", waitmodel.XSLC);
                        hthhz.Add("CPXH", model.PP + model.XH);
                        hthhz.Add("CP", model.PP);//15
                        hthhz.Add("PFBZ", mainPanel.hhzinterface.HhzR_VEHICLE_PFBZ.GetValue(model.ZXBZ, "0"));
                        hthhz.Add("ZDZL", model.ZZL);
                        hthhz.Add("JZZL", model.JZZL);
                        hthhz.Add("QDXS", mainPanel.hhzinterface.HhzR_VEHICLE_QDXS.GetValue(model.QDXS, "0"));
                        hthhz.Add("BSQXS", mainPanel.hhzinterface.HhzR_VEHICLE_BSQXS.GetValue(model.BSQXS, "0"));//20
                        hthhz.Add("RYZL", mainPanel.hhzinterface.HhzR_VEHICLE_RLZL.GetValue(model.RLZL, "0"));
                        hthhz.Add("GYFS", mainPanel.hhzinterface.HhzR_VEHICLE_GYFS.GetValue(model.GYFS, "0"));
                        hthhz.Add("SFYOBD", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.OBD, "0"));
                        hthhz.Add("SFYJHZZ", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.JHZZ, "0"));
                        hthhz.Add("SFDK", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.DKGYYB, "0"));//25
                        hthhz.Add("EGRXH", "2");
                        hthhz.Add("KH", model.CLLX.StartsWith("K") ? "K" : "H");
                        hthhz.Add("ZW", model.HDZK);
                        hthhz.Add("JWJHQBH1", "2");
                        hthhz.Add("JWJHQBH2", "2");//30
                        hthhz.Add("YCGXH1", "2");
                        hthhz.Add("YCGXH2", "2");
                        hthhz.Add("PYBXH", "2");
                        hthhz.Add("PYQXH", "2");
                        hthhz.Add("ZYQXH", "2");//35
                        hthhz.Add("FDJXH", model.FDJXH);
                        hthhz.Add("FDJEDZS", model.EDZS);
                        hthhz.Add("FDJPL", model.FDJPL);
                        hthhz.Add("FDJEDGL", model.EDGL);
                        hthhz.Add("QGSL", model.QGS);
                        hthhz.Add("JQFS", mainPanel.hhzinterface.HhzR_VEHICLE_JQFS.GetValue(model.JQFS, "0"));//41
                        hthhz.Add("CLYTBS", "2");
                        hthhz.Add("SFGZ", "2");
                        hthhz.Add("SFYYC", "2");
                        hthhz.Add("SFYHBF", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.SFYQBF, "0"));
                        hthhz.Add("SFMJ", "0");//46
                        hthhz.Add("BFNX", "2");
                        hthhz.Add("QZBFNX", "2");
                        hthhz.Add("BTGCS", (int.Parse(waitmodel.JCCS) - 1).ToString());
                        hthhz.Add("SFHG", "2");
                        hthhz.Add("SFYJFB", "2");//51
                        hthhz.Add("BZFFRQ", DateTime.Now.ToString("yyyy-MM-dd"));
                        hthhz.Add("BZYXQ", "2");
                        hthhz.Add("DJYBM", "2");
                        hthhz.Add("JCCBHDJ", "2");
                        hthhz.Add("JCCBHXG", "2");//56
                        hthhz.Add("JCCBHFK", "2");
                        hthhz.Add("DPFS", mainPanel.hhzinterface.HhzR_VEHICLE_DPFS.GetValue(model.DPFS, "0"));
                        hthhz.Add("XZBM", "1");
                        hthhz.Add("FDJSCC", model.FDJSCQY);
                        hthhz.Add("CLSCC", model.SCQY);//61
                        hthhz.Add("ZZ", model.HDZZL);
                        hthhz.Add("QX", model.SSXQ);//协议 里面限值为varchar(6)
                        hthhz.Add("WQJCRQ", DateTime.Now.ToString("yyyy-MM-dd"));
                        hthhz.Add("JCZQS", "1");
                        hthhz.Add("BZQNJCCS", waitmodel.JCCS);//66
                        hthhz.Add("CHZZ", mainPanel.hhzinterface.HhzR_VEHICLE_CHZZ.GetValue(model.CHZZ, "0"));
                        hthhz.Add("SCSJ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        hthhz2.Clear();
                        hthhz2.Add("type", "UPLOAD");
                        hthhz2.Add("data", hthhz);
                        */
                        hthhz.Add("carno", waitmodel.JYLSH);
                        hthhz.Add("license", waitmodel.CLHP);
                        hthhz.Add("licensetype", mainPanel.hhzinterface.HhzR_VEHICLE_CPYS.GetValue(model.CPYS, "1"));
                        hthhz.Add("licensecode", mainPanel.hhzinterface.HhzR_VEHICLE_HPZL.GetValue(model.HPZL, "01"));
                        hthhz.Add("vin", model.CLSBM);
                        hthhz.Add("vehicletype", model.CLLX.Split('_')[0]);//5
                        hthhz.Add("usetype", mainPanel.hhzinterface.HhzR_VEHICLE_SYXZ.GetValue(model.SYXZ, "A"));
                        hthhz.Add("registerdate", model.ZCRQ.ToString("yyyy-MM-dd"));// mainPanel.hhzinterface.HhzR_JCZ_JJLX.GetValue(model.JJLX, ""));//5
                        hthhz.Add("mdate", model.ZCRQ.ToString("yyyy-MM-dd"));// mainPanel.hhzinterface.HhzR_JCZ_JJLX.GetValue(model.JJLX, ""));//5
                        hthhz.Add("brand", model.PP);//15
                        hthhz.Add("vehiclemodel", model.XH);
                        hthhz.Add("engine", model.FDJXH);
                        hthhz.Add("enginemanuf", model.FDJSCQY);
                        hthhz.Add("carfield", "01");
                        hthhz.Add("hdzk", model.HDZK);
                        hthhz.Add("fueltype", mainPanel.hhzinterface.HhzR_VEHICLE_RLZL.GetValue(model.RLZL, "A"));
                        hthhz.Add("gear", mainPanel.hhzinterface.HhzR_VEHICLE_BSQXS.GetValue(model.BSQXS, "01"));//20
                        hthhz.Add("airin", mainPanel.hhzinterface.HhzR_VEHICLE_JQFS.GetValue(model.JQFS, "01"));//41
                        hthhz.Add("ed", model.FDJPL);
                        hthhz.Add("fuelsupply", mainPanel.hhzinterface.HhzR_VEHICLE_GYFS.GetValue(model.GYFS, "01"));
                        hthhz.Add("enginespeed", model.EDZS);
                        hthhz.Add("enginepower", model.EDGL);
                        hthhz.Add("drivemode", mainPanel.hhzinterface.HhzR_VEHICLE_QDXS.GetValue(model.QDXS, "01"));
                        hthhz.Add("gvm", model.ZZL);
                        hthhz.Add("rm", model.JZZL);
                        hthhz.Add("odometer", waitmodel.XSLC);
                        hthhz.Add("cylinders", model.QGS);
                        hthhz.Add("egr", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.OBD, "N"));
                        hthhz.Add("tg", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.DKGYYB, "N"));
                        hthhz.Add("hcl", mainPanel.hhzinterface.HhzR_VEHICLE_YESORNO.GetValue(model.JHZZ, "N"));
                        hthhz.Add("hcltype", mainPanel.hhzinterface.HhzR_VEHICLE_HCLTYPE.GetValue(model.SFYQBF, "1"));
                        hthhz.Add("exchangetime", DateTime.Now.ToString("yyyy-MM-dd"));
                        hthhz2.Clear();
                        hthhz2.Add("type", "POST");
                        hthhz2.Add("data", hthhz);
                        try
                        {
                            string code, msg;
                            Newtonsoft.Json.Linq.JObject ackobject;
                            if (mainPanel.hhzinterface.uploadJsonArray("",hthhz2, HhzWebClient.Hhzclient.DATALX.CLXX, out code, out msg,out ackobject))
                            {
                                string ackmsg = "";
                                try
                                {
                                    ackmsg = ackobject["message"].ToString();
                                }
                                catch
                                {
                                    ackmsg = "";

                                }
                                if (code == "0")
                                {
                                    ini.INIIO.saveLogInf("上传检测信息至联网平台成功");
                                }
                                else if (ackmsg.Contains("already exists"))
                                {
                                    hthhz2["type"] = "PUT";
                                    if (mainPanel.hhzinterface.uploadJsonArray("", hthhz2, HhzWebClient.Hhzclient.DATALX.CLXX, out code, out msg, out ackobject))
                                    {
                                        try
                                        {
                                            ackmsg = ackobject["message"].ToString();
                                        }
                                        catch
                                        {
                                            ackmsg = "";
                                        }
                                        if (code == "0")
                                        {
                                            ini.INIIO.saveLogInf("更新检测信息至联网平台成功");
                                        }
                                        else
                                        {
                                            MessageBox.Show("更新检测信息至联网平台失败:code=" + code + ",msg=" + ackmsg);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("更新检测信息至联网平台失败:code=" + code + ",msg=" + msg);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("上传检测信息至联网平台失败:code=" + code + ",msg=" + ackmsg);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("上传检测信息至联网平台失败:code=" + code + ",msg=" + msg);
                                return;
                            }
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("上传或更新检测信息至联网平台发生异常:" + er.Message);
                            return;
                        }
                        #endregion
                        #endregion
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
                        else if (model.CPYS == "农用" && mainPanel.lz)//如果为低速货车或者拖拉机也为滤纸式烟度法
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
                        else if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不能解锁")//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
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
                        if (!comboBoxQDXS.Text.Contains("四驱") && comboBoxCHZZ.Text != "不能解锁")
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
            bool isquerysuccessGA = false;
            bool isquerysuccessLocal = false;
            bool isquerysuccessHB = false;
            string vin = "";
            string clhp = "";
            string hpzl = "";
            string carno = "";
            clhp = comboBoxCLPH.Text;
            hpzl = mainPanel.hhzinterface.HhzR_VEHICLE_HPZL.GetValue(comboBoxHPZL.Text, "");
            if (mainPanel.hhzwebinf.enableQuery && hhzquery != null)
            {
                try
                {
                    string code, message;
                    string gaclhp = comboBoxCLPH.Text.ToUpper();
                    string gahpzl =mainPanel.hhzinterface.HhzR_VEHICLE_HPZL.GetValue(  comboBoxHPZL.Text,"01");
                    string gavin = textBoxXGAVIN.Text.ToUpper();
                    DataTable testedvehicle = hhzquery.GetVehicleInf(gaclhp, gahpzl, gavin, out code, out message);
                    if (testedvehicle != null && testedvehicle.Rows.Count > 0)
                    {
                        ini.INIIO.saveLogInf("安检平台查询车辆" + gaclhp + "信息成功");
                        //textBoxHDZK.Text= testedvehicle.Rows[0]["jycs"].ToString();
                        if (testedvehicle.Columns.Contains("cllx"))
                            comboBoxCLLX.Text =testedvehicle.Rows[0]["cllx"].ToString() + "_" + logininfcontrol.getComBoBoxItemsNAME("车辆类型", testedvehicle.Rows[0]["cllx"].ToString());
                        if (testedvehicle.Columns.Contains("syr"))
                            textBoxCZ.Text = testedvehicle.Rows[0]["syr"].ToString();
                        if (testedvehicle.Columns.Contains("syxz"))
                            comboBoxSYXZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_SYXZ.GetValue(testedvehicle.Rows[0]["syxz"].ToString(), "");
                        if (testedvehicle.Columns.Contains("clpp1"))
                            textBoxPP.Text = testedvehicle.Rows[0]["clpp1"].ToString();
                        if (testedvehicle.Columns.Contains("clxh"))
                            comboBoxXH.Text = testedvehicle.Rows[0]["clxh"].ToString();
                        if (testedvehicle.Columns.Contains("clsbdh"))
                        {
                            textBoxDPHM.Text = testedvehicle.Rows[0]["clsbdh"].ToString();
                            vin = testedvehicle.Rows[0]["clsbdh"].ToString();
                        }
                        if (testedvehicle.Columns.Contains("fdjh"))
                            textBoxFDJH.Text = testedvehicle.Rows[0]["fdjh"].ToString();
                        //textBoxFDJXH.Text = testedvehicle.Rows[0]["jycs"].ToString();
                        if (testedvehicle.Columns.Contains("zzcmc"))
                        {
                            textBoxSCS.Text = testedvehicle.Rows[0]["zzcmc"].ToString();
                        }
                        if (testedvehicle.Columns.Contains("hdzk"))
                            textBoxHDZK.Text = testedvehicle.Rows[0]["hdzk"].ToString();
                        if (testedvehicle.Columns.Contains("qpzk"))
                            textBoxJSSZK.Text = testedvehicle.Rows[0]["qpzk"].ToString();
                        if (testedvehicle.Columns.Contains("zzl"))
                            textBoxZZL.Text = testedvehicle.Rows[0]["zzl"].ToString();
                        if (testedvehicle.Columns.Contains("hdzzl"))
                            textBoxHDZZL.Text = testedvehicle.Rows[0]["hdzzl"].ToString();
                        if (testedvehicle.Columns.Contains("zbzl"))
                            textBoxZBZL.Text = testedvehicle.Rows[0]["zbzl"].ToString();
                        //textBoxJZZL.Text = testedvehicle.Rows[0]["jycs"].ToString();
                        if (testedvehicle.Columns.Contains("ccdjrq"))
                            dateZCRQ.Value = DateTime.Parse(testedvehicle.Rows[0]["ccdjrq"].ToString());
                        if (testedvehicle.Columns.Contains("ccrq"))
                            dateSCRQ.Value = DateTime.Parse(testedvehicle.Rows[0]["ccrq"].ToString());
                        if (testedvehicle.Columns.Contains("pl"))
                        {
                            string gapl = testedvehicle.Rows[0]["pl"].ToString();
                            double gapld = 0;
                            if (double.TryParse(gapl, out gapld))
                                textBoxFDJPL.Text = (gapld * 0.001).ToString("0.0");
                            else
                                textBoxFDJPL.Text = "";
                        }
                        if (testedvehicle.Columns.Contains("rlzl"))
                            comboBoxRLZL.Text = mainPanel.hhzinterface.Hhz_VEHICLE_RLZL.GetValue(testedvehicle.Rows[0]["rlzl"].ToString(), "");
                        if (testedvehicle.Columns.Contains("gl"))
                        {
                            string gapl = testedvehicle.Rows[0]["gl"].ToString();
                            double gapld = 0;
                            if (double.TryParse(gapl, out gapld))
                                textBoxEDGL.Text = ((int)(gapld)).ToString("0");
                            else
                                textBoxEDGL.Text = "";
                        }
                        //textBoxEDGL.Text = testedvehicle.Rows[0]["gl"].ToString();
                        if (testedvehicle.Columns.Contains("xzqh"))
                            textBoxSSXQ.Text = testedvehicle.Rows[0]["xzqh"].ToString();
                        isquerysuccessGA = true;
                    }
                    else
                    {
                        MessageBox.Show("安检平台查询车辆" + gaclhp + "信息失败\r\ncode=" + code + "|message=" + message, "警告");
                        ini.INIIO.saveLogInf("安检平台查询车辆" + gaclhp + "信息失败\r\ncode=" + code + "|message=" + message);
                    }
                }
                catch (Exception er)
                {
                    MessageBox.Show("安检平台查询车辆信息出现异常:" + er.Message, "警告");
                    ini.INIIO.saveLogInf("安检平台查询车辆信息出现异常:" + er.Message);
                }
            }
            if(true)
            {
                CARINF model = new CARINF();
                model = logininfcontrol.getCarInfbyPlate(comboBoxCLPH.Text, comboBoxCPYS.Text);
                if (model.CLHP != "-2")
                {
                        //comboBoxCPYS.Text = model.CPYS;
                        //comboBoxHPZL.Text = model.HPZL;
                    if(!isquerysuccessGA)
                    {
                        comboBoxCLLX.Text = model.CLLX;
                        textBoxCZ.Text = model.CZ;
                        comboBoxSYXZ.Text = model.SYXZ;
                        textBoxPP.Text = model.PP;
                        comboBoxXH.Text = model.XH;
                        textBoxDPHM.Text = model.CLSBM;
                        vin = model.CLSBM;
                        textBoxFDJH.Text = model.FDJHM;
                        textBoxSCS.Text = model.SCQY;
                        textBoxHDZK.Text = model.HDZK;
                        textBoxJSSZK.Text = model.JSSZK;
                        textBoxZZL.Text = model.ZZL;
                        textBoxHDZZL.Text = model.HDZZL;
                        textBoxZBZL.Text = model.ZBZL;
                        dateZCRQ.Value = model.ZCRQ;
                        dateSCRQ.Value = model.SCRQ;
                        textBoxFDJPL.Text = model.FDJPL;
                        comboBoxRLZL.Text = model.RLZL;
                        textBoxEDGL.Text = model.EDGL;
                        textBoxSSXQ.Text = model.SSXQ;
                    } 

                        textBoxFDJXH.Text = model.FDJXH;
                        textBoxJZZL.Text = model.JZZL;
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
                        comboBoxSFWDZR.Text = model.SFWDZR;
                        comboBoxSFYQBF.Text = model.SFYQBF;
                        textBoxFDJSCQY.Text = model.FDJSCQY;
                        textBoxQDLTQY.Text = model.QDLTQY;
                        textBoxRYPH.Text = model.RYPH;
                        comboBoxPFBZ.Text = model.ZXBZ;
                    isquerysuccessLocal = true;                
                }
            }
            if (isquerysuccessGA || isquerysuccessLocal)
            {

                System.Collections.Hashtable hthhz = new System.Collections.Hashtable();
                System.Collections.Hashtable hthhz2 = new System.Collections.Hashtable();

                #region 先判断车是否可以检测           

                hthhz.Add("vin", vin);
                hthhz2.Add("type", "QUERY");
                hthhz2.Add("filter", hthhz);
                try
                {
                    string code, msg;
                    string jsonReturnString = "";
                    if (mainPanel.hhzinterface.queryData(hthhz2, HhzWebClient.Hhzclient.DATALX.CLXXCX, out code, out msg, out jsonReturnString))
                    {
                        try
                        {
                            List<HhzWebClient.hhclxx> clxxlist = new List<HhzWebClient.hhclxx>();
                            clxxlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HhzWebClient.hhclxx>>(jsonReturnString);
                            if (clxxlist.Count > 0)
                            {
                                try
                                {
                                    HhzWebClient.hhclxx clxx = clxxlist[0];
                                    comboBoxCPYS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_CPYS.GetValue(clxx.licensetype, "");
                                    textBoxDPHM.Text = clxx.vin;
                                    string cllx = clxx.vehicletype;
                                    cllx = cllx + "_" + mainPanel.logininfcontrol.getComBoBoxItemsNAME("车辆类型", cllx);
                                    comboBoxCLLX.Text = cllx;
                                    comboBoxSYXZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_SYXZ.GetValue(clxx.usetype, "");
                                    dateZCRQ.Value = DateTime.Parse(clxx.registerdate);
                                    dateSCRQ.Value = DateTime.Parse(clxx.mdate);
                                    textBoxPP.Text = clxx.brand;
                                    comboBoxXH.Text = clxx.vehiclemodel;
                                    textBoxFDJXH.Text = clxx.engine;
                                    textBoxFDJSCQY.Text = clxx.enginemanuf;
                                    textBoxHDZK.Text = clxx.hdzk;
                                    comboBoxRLZL.Text = mainPanel.hhzinterface.Hhz_VEHICLE_RLZL.GetValue(clxx.fueltype, "");
                                    comboBoxBSQXS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_BSQXS.GetValue(clxx.gear, "");
                                    comboBoxJQFS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_JQFS.GetValue(clxx.airin, "");
                                    textBoxFDJPL.Text = clxx.ed;
                                    comboBoxGYFS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_GYFS.GetValue(clxx.fuelsupply, "");
                                    textBoxEDGL.Text = clxx.enginepower;
                                    textBoxEDZS.Text = clxx.enginespeed;
                                    comboBoxQDXS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_QDXS.GetValue(clxx.drivemode, "");
                                    textBoxZZL.Text = clxx.gvm;
                                    textBoxZBZL.Text = (int.Parse(clxx.rm) - 100).ToString();
                                    textBoxXSLC.Text = clxx.odometer;
                                    textBoxQGS.Text = clxx.cylinders;
                                    comboBoxOBD.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.egr, "");
                                    comboBoxDKGY.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.tg, "");
                                    comboBoxJHZZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.hcl, "");
                                    comboBoxSFYQBF.Text = mainPanel.hhzinterface.Hhz_VEHICLE_HCLTYPE.GetValue(clxx.hcltype, "");
                                    carno = clxx.carno;
                                    isquerysuccessHB = true;
                                }
                                catch (Exception er)
                                {
                                    ini.INIIO.saveLogInf("解析车辆信息出现异常：" + er.Message);
                                    MessageBox.Show("解析车辆信息出现异常：" + er.Message);
                                    return;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            ini.INIIO.saveLogInf("解析车辆检测记录出现异常：" + er.Message);
                            MessageBox.Show("解析车辆检测记录出现异常：" + er.Message);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("查询车辆检测记录出现失败：\r\ncode=" + code + "\r\nmsg=" + msg);
                        return;
                    }
                }
                catch (Exception er)
                {
                    ini.INIIO.saveLogInf("查询车辆检测记录出现异常：" + er.Message);
                    MessageBox.Show("查询车辆检测记录出现异常：" + er.Message);
                    return;
                }
                #endregion
            }
            else
            {
                System.Collections.Hashtable hthhz = new System.Collections.Hashtable();
                System.Collections.Hashtable hthhz2 = new System.Collections.Hashtable();

                #region 先判断车是否可以检测           

                hthhz.Add("license", clhp);
                hthhz.Add("licensecode", hpzl);
                hthhz2.Add("type", "QUERY");
                hthhz2.Add("filter", hthhz);
                try
                {
                    string code, msg;
                    string jsonReturnString = "";
                    if (mainPanel.hhzinterface.queryData(hthhz2, HhzWebClient.Hhzclient.DATALX.CLXXCX, out code, out msg, out jsonReturnString))
                    {
                        try
                        {
                            List<HhzWebClient.hhclxx> clxxlist = new List<HhzWebClient.hhclxx>();
                            clxxlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HhzWebClient.hhclxx>>(jsonReturnString);
                            if (clxxlist.Count > 0)
                            {
                                try
                                {
                                    HhzWebClient.hhclxx clxx = clxxlist[0];
                                    comboBoxCPYS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_CPYS.GetValue(clxx.licensetype, "");
                                    textBoxDPHM.Text = clxx.vin;
                                    string cllx = clxx.vehicletype;
                                    cllx = cllx + "_" + mainPanel.logininfcontrol.getComBoBoxItemsNAME("车辆类型", cllx);
                                    comboBoxCLLX.Text = cllx;
                                    comboBoxSYXZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_SYXZ.GetValue(clxx.usetype, "");
                                    dateZCRQ.Value = DateTime.Parse(clxx.registerdate);
                                    dateSCRQ.Value = DateTime.Parse(clxx.mdate);
                                    textBoxPP.Text = clxx.brand;
                                    comboBoxXH.Text = clxx.vehiclemodel;
                                    textBoxFDJXH.Text = clxx.engine;
                                    textBoxFDJSCQY.Text = clxx.enginemanuf;
                                    textBoxHDZK.Text = clxx.hdzk;
                                    comboBoxRLZL.Text = mainPanel.hhzinterface.Hhz_VEHICLE_RLZL.GetValue(clxx.fueltype, "");
                                    comboBoxBSQXS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_BSQXS.GetValue(clxx.gear, "");
                                    comboBoxJQFS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_JQFS.GetValue(clxx.airin, "");
                                    textBoxFDJPL.Text = clxx.ed;
                                    comboBoxGYFS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_GYFS.GetValue(clxx.fuelsupply, "");
                                    textBoxEDGL.Text = clxx.enginepower;
                                    textBoxEDZS.Text = clxx.enginespeed;
                                    comboBoxQDXS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_QDXS.GetValue(clxx.drivemode, "");
                                    textBoxZZL.Text = clxx.gvm;
                                    textBoxZBZL.Text = (int.Parse(clxx.rm) - 100).ToString();
                                    textBoxXSLC.Text = clxx.odometer;
                                    textBoxQGS.Text = clxx.cylinders;
                                    comboBoxOBD.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.egr, "");
                                    comboBoxDKGY.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.tg, "");
                                    comboBoxJHZZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.hcl, "");
                                    comboBoxSFYQBF.Text = mainPanel.hhzinterface.Hhz_VEHICLE_HCLTYPE.GetValue(clxx.hcltype, "");
                                    carno = clxx.carno;
                                    isquerysuccessHB = true;
                                }
                                catch (Exception er)
                                {
                                    ini.INIIO.saveLogInf("解析车辆信息出现异常：" + er.Message);
                                    MessageBox.Show("解析车辆信息出现异常：" + er.Message);
                                    return;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            ini.INIIO.saveLogInf("解析车辆检测记录出现异常：" + er.Message);
                            MessageBox.Show("解析车辆检测记录出现异常：" + er.Message);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("查询车辆检测记录出现失败：\r\ncode=" + code + "\r\nmsg=" + msg);
                        return;
                    }
                }
                catch (Exception er)
                {
                    ini.INIIO.saveLogInf("查询车辆检测记录出现异常：" + er.Message);
                    MessageBox.Show("查询车辆检测记录出现异常：" + er.Message);
                    return;
                }
                #endregion
            }
            labelQueryInf.Text = "公安:" + (isquerysuccessGA ? "成功" : "失败") + "|本地:" + (isquerysuccessLocal ? "成功" : "失败") + "|环保:" + (isquerysuccessHB ? "成功" : "失败") + "carno:" + carno;
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
            if (!mainPanel.isNetUsed || (mainPanel.netMode != mainPanel.NHNETMODE&& mainPanel.netMode != mainPanel.JXNETMODE && mainPanel.netMode != mainPanel.DALINETMODE&&mainPanel.netMode!=mainPanel.ORTNETMODE && mainPanel.netMode != mainPanel.EZNETMODE && mainPanel.netMode != mainPanel.HHZNNETMODE))
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

        private void buttonX2_Click(object sender, EventArgs e)
        {
                       
            bool isquerysuccess = false;
            if (true)
            {
                System.Collections.Hashtable hthhz = new System.Collections.Hashtable();
                System.Collections.Hashtable hthhz2 = new System.Collections.Hashtable();

                #region 先判断车是否可以检测
                string clhp = "";
                string hpzl = "";
                string vin = "";
                clhp = comboBoxCLPH.Text;
                hpzl = mainPanel.hhzinterface.HhzR_VEHICLE_HPZL.GetValue(comboBoxHPZL.Text, "");
                vin = textBoxXGAVIN.Text;
                if (clhp != "" && hpzl != "")
                {
                    hthhz.Add("license", clhp);
                    hthhz.Add("licensecode", hpzl);
                }
                else if (vin != "")
                {
                    hthhz.Add("vin", vin);
                }
                else
                {
                    MessageBox.Show("查询条件：号牌+号牌种类 或者 车架号" + "\r\n请至少输入一项" );
                    return;
                }
                hthhz2.Add("type", "QUERY");
                hthhz2.Add("filter", hthhz);
                try
                {
                    string code, msg;
                    string jsonReturnString = "";
                    if (mainPanel.hhzinterface.queryData(hthhz2, HhzWebClient.Hhzclient.DATALX.CLXXCX, out code, out msg, out jsonReturnString))
                    {
                        try
                        {
                            List<HhzWebClient.hhclxx> clxxlist = new List<HhzWebClient.hhclxx>();
                            clxxlist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HhzWebClient.hhclxx>>(jsonReturnString);
                            if (clxxlist.Count > 0)
                            {
                                try
                                {
                                    HhzWebClient.hhclxx clxx = clxxlist[0];
                                    comboBoxCPYS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_CPYS.GetValue(clxx.licensetype, "");
                                    textBoxDPHM.Text = clxx.vin;
                                    string cllx = clxx.vehicletype;
                                    cllx = cllx + "_" + mainPanel.logininfcontrol.getComBoBoxItemsNAME("车辆类型", cllx);
                                    comboBoxCLLX.Text = cllx;
                                    comboBoxSYXZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_SYXZ.GetValue(clxx.usetype, "");
                                    dateZCRQ.Value = DateTime.Parse(clxx.registerdate);
                                    dateSCRQ.Value= DateTime.Parse(clxx.mdate);
                                    textBoxPP.Text = clxx.brand;
                                    comboBoxXH.Text = clxx.vehiclemodel;
                                    textBoxFDJXH.Text = clxx.engine;
                                    textBoxFDJSCQY.Text = clxx.enginemanuf;
                                    textBoxHDZK.Text = clxx.hdzk;
                                    comboBoxRLZL.Text= mainPanel.hhzinterface.Hhz_VEHICLE_RLZL.GetValue(clxx.fueltype, "");
                                    comboBoxBSQXS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_BSQXS.GetValue(clxx.gear, "");
                                    comboBoxJQFS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_JQFS.GetValue(clxx.airin, "");
                                    textBoxFDJPL.Text = clxx.ed;
                                    comboBoxGYFS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_GYFS.GetValue(clxx.fuelsupply, "");
                                    textBoxEDGL.Text = clxx.enginepower;
                                    textBoxEDZS.Text = clxx.enginespeed;
                                    comboBoxQDXS.Text = mainPanel.hhzinterface.Hhz_VEHICLE_QDXS.GetValue(clxx.drivemode, "");
                                    textBoxZZL.Text = clxx.gvm;
                                    textBoxZBZL.Text = (int.Parse(clxx.rm) - 100).ToString();
                                    textBoxXSLC.Text = clxx.odometer;
                                    textBoxQGS.Text = clxx.cylinders;
                                    comboBoxOBD.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.egr, "");
                                    comboBoxDKGY.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.tg, "");
                                    comboBoxJHZZ.Text = mainPanel.hhzinterface.Hhz_VEHICLE_YESORNO.GetValue(clxx.hcl, "");
                                    comboBoxSFYQBF.Text = mainPanel.hhzinterface.Hhz_VEHICLE_HCLTYPE.GetValue(clxx.hcltype, "");
                                    isquerysuccess = true;
                                }
                                catch(Exception er)
                                {
                                    ini.INIIO.saveLogInf("解析车辆信息出现异常：" + er.Message);
                                    MessageBox.Show("解析车辆信息出现异常：" + er.Message);
                                    return;
                                }
                            }
                        }
                        catch (Exception er)
                        {
                            ini.INIIO.saveLogInf("解析车辆检测记录出现异常：" + er.Message);
                            MessageBox.Show("解析车辆检测记录出现异常：" + er.Message);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("查询车辆检测记录出现失败：\r\ncode=" + code + "\r\nmsg=" + msg);
                        return;
                    }
                }
                catch (Exception er)
                {
                    ini.INIIO.saveLogInf("查询车辆检测记录出现异常：" + er.Message);
                    MessageBox.Show("查询车辆检测记录出现异常：" + er.Message);
                    return;
                }
                #endregion
            }
            if(!isquerysuccess)
            {
                CARINF model = new CARINF();
                model = logininfcontrol.getCarInfbyPlate(comboBoxCLPH.Text, comboBoxCPYS.Text);
                if (model.CLHP != "-2")
                {
                    //comboBoxCPYS.Text = model.CPYS;
                    //comboBoxHPZL.Text = model.HPZL;
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
            }
            string cjclph = comboBoxCLPH.Text;
            string cjcpys = comboBoxCPYS.Text;
            CARDETECTED latestRecord = logininfcontrol.getPreTestDatebyPlate(cjclph, cjcpys);
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

        private void comboBoxDKGY_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
