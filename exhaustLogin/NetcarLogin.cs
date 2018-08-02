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

namespace exhaustLogin
{
    public partial class NetcarLogin : Form
    {
        private loginInfModel logininfmodel = new loginInfModel();
        private loginInfControl logininfcontrol = new loginInfControl();
        private stationControl stationcontrol = new stationControl();
        public struct carAtWaitInf
        {
            public string plate;
            public string loginTime;
            public string jccs;
        };
        private List<carAtWaitInf> carAtWaitlist = new List<carAtWaitInf>();

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

        public NetcarLogin()
        {
            InitializeComponent();
        }

       /* private void carLogin_Load(object sender, EventArgs e)
        {
            
            init_loginDefault();
            init_waitList();
            init_safewaitList();
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            buttonSave.Enabled = false;
            buttonClear.Enabled = false;
            checkBoxExhaust.Checked = true;
            checkBoxSafe.Checked = false;

        }
        private void init_loginDefault()
        {
            logininfmodel = logininfcontrol.getLoginDefaultInf();
            DataTable dt = null;
            dt = logininfcontrol.getComBoBoxItemsInf("车辆牌号");
            //comboBoxCLPH.Text = logininfmodel.CLHP;
            //textBoxPlateAtWait.Text = logininfmodel.CLHP;
            dt = logininfcontrol.getComBoBoxItemsInf("车牌颜色");
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                comboBoxCPYS.Items.Add(dt.Rows[i]["MC"].ToString());
            }
            //comboBoxCPYS.Text = logininfmodel.CPYS;
            dt = logininfcontrol.getComBoBoxItemsInf("车辆类型");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if(dt.Rows[i]["Display"].ToString()=="Y")
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
            dt = logininfcontrol.getComBoBoxItemsInf("是否选择");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxDLSP.Items.Add(dt.Rows[i]["MC"].ToString());
                comboBoxSRL.Items.Add(dt.Rows[i]["MC"].ToString());
                comboBoxJHZZ.Items.Add(dt.Rows[i]["MC"].ToString());
                comboBoxOBD.Items.Add(dt.Rows[i]["MC"].ToString());
                comboBoxDKGY.Items.Add(dt.Rows[i]["MC"].ToString());
            }
            clear_loginInf();
            //comboBoxCPYS.Text = logininfmodel.CPYS;
            
        }

        private void init_waitList()
        {
            listBoxCarAtWait.Items.Clear();
            DataTable waitTable = null;
            waitTable = logininfcontrol.getAllCarAtWait();
            for (int i = 0; i < waitTable.Rows.Count; i++)
            {
                listBoxCarAtWait.Items.Add(waitTable.Rows[i]["CLHP"]);
            }
            listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
        }

        private void init_safewaitList()
        {
            listBoxCarSafeWait.Items.Clear();
            DataTable waitTable = null;
            waitTable = logininfcontrol.getallCarSafeAtWait();
            for (int i = 0; i < waitTable.Rows.Count; i++)
            {
                listBoxCarSafeWait.Items.Add(waitTable.Rows[i]["CLHP"]);
            }
            listBoxCarSafeWait.SelectedIndex = listBoxCarSafeWait.Items.Count - 1;
        }

        private void comboBoxCLPH_Validated(object sender, EventArgs e)
        {
            int jccs=0;
            comboBoxCLPH.Text = comboBoxCLPH.Text.ToUpper();
            if (!logininfcontrol.checkCarInfIsAlive(comboBoxCLPH.Text))
            {
                //MessageBox.Show("该车信息未在信息库中找到,请按要求输入");
                labelClhp.Text = "(未找到信息)";
                //comboBoxCLPH.Text = logininfmodel.CLHP;
                comboBoxCPYS.Text = logininfmodel.CPYS;
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
                textBoxJCCS.Text = "1";
            }
            else
            {
                labelClhp.Text = "(信息已找到)";
                CARINF model = new CARINF();
                model = logininfcontrol.getCarInfbyPlate(comboBoxCLPH.Text);
                if (model.CLHP != "-2")
                {
                    //comboBoxCLPH.Text = model.CLHP;
                    comboBoxCPYS.Text = model.CPYS;
                    comboBoxCLLX.Text = model.CLLX;
                    textBoxCZ.Text = model.CZ;
                    comboBoxSYXZ.Text= model.SYXZ;
                    textBoxPP.Text = model.PP;
                    comboBoxXH.Text= model.XH;
                    textBoxDPHM.Text = model.CLSBM;
                    textBoxFDJH.Text= model.FDJHM;
                    textBoxFDJXH.Text= model.FDJXH;
                    textBoxSCS.Text = model.SCQY;
                    textBoxHDZK.Text= model.HDZK;
                    textBoxJSSZK.Text= model.JSSZK;
                    textBoxZZL.Text = model.ZZL;
                    textBoxHDZZL.Text = model.HDZZL;
                    textBoxZBZL.Text = model.ZBZL;
                    textBoxJZZL.Text = model.JZZL;
                    dateZCRQ.Value= model.ZCRQ;
                    dateSCRQ.Value = model.SCRQ;
                    textBoxFDJPL.Text= model.FDJPL;
                    comboBoxRLZL.Text = model.RLZL;
                    textBoxEDGL.Text= model.EDGL;
                    textBoxEDZS.Text= model.EDZS;
                    comboBoxBSQXS.Text= model.BSQXS;
                    textBoxDWS.Text = model.DWS;
                    comboBoxGYFS.Text = model.GYFS;
                    comboBoxDPXS.Text = model.DPFS;
                    comboBoxJQFS.Text= model.JQFS;
                    textBoxQGS.Text = model.QGS;
                    comboBoxQDXS.Text = model.QDXS;
                    comboBoxCHZZ.Text = model.CHZZ;
                    comboBoxDLSP.Text = model.DLSP;
                    comboBoxSRL.Text = model.SFSRL;
                    comboBoxJHZZ.Text = model.JHZZ;
                    comboBoxOBD.Text = model.OBD;
                    comboBoxDKGY.Text = model.DKGYYB;
                    textBoxLXDH.Text = model.LXDH;
                    
                }
            }
            checkBoxExhaust.Checked = true;
            checkBoxSafe.Checked = false;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            buttonSave.Enabled = false;
            buttonClear.Enabled = false;
        }

        private void clear_loginInf()
        {
            comboBoxCLPH.Text = logininfmodel.CLHP;
            comboBoxCPYS.Text = logininfmodel.CPYS;
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
            textBoxXSLC.Text = "";
            
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            clear_loginInf();
            
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
            if (checkBoxExhaust.Checked == false && checkBoxSafe.Checked == false)
            {
                MessageBox.Show("请先选择参检项目再保存");
                return; 
            }
            if (checkBoxSafe.Checked)
            {
                if (logininfcontrol.checkCarSafeIsAtWait(clhp))
                {
                    MessageBox.Show("安检保存失败，该车已经在安检待检序列中");
                    return;
                }
            }
            if (checkBoxExhaust.Checked)
            {
                if (logininfcontrol.checkCarIsAtWait(clhp))
                {
                    MessageBox.Show("环检保存失败，该车已经在环检待检序列中");
                    return;
                }
            }

            if (checkBoxSafe.Checked == true && checkBoxExhaust.Checked == false)
            {
                DateTime loginTime = DateTime.Now;
                int safeJccs = int.Parse(labelSafeJccs.Text);
                exhaustBZ = "未参检";
                if (safeJccs % 2 == 1)
                {
                    safeFy = 100;
                }
                else
                {
                    safeFy = 0;
                    safeBZ = "复检";
                }
                totalFy = exhaustFy + safeFy;
                MessageBox.Show("环保:未参检，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                CARSAFEDETECTED safemodel = new CARSAFEDETECTED();
                safemodel.CLHP = comboBoxCLPH.Text;
                safemodel.CLID = safemodel.CLHP + "T" + loginTime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                safemodel.DLSJ = loginTime;
                safemodel.JCSJ = loginTime;
                safemodel.JCCS = labelSafeJccs.Text;
                safemodel.JCJG = "--";
                safemodel.SFJS = "N";
                safemodel.SFLQ = "N";
                safemodel.JCFY = safeFy.ToString("0.0");
                if (logininfcontrol.saveCarSafebj(safemodel) == true)
                {
                    listBoxCarSafeWait.Items.Add(safemodel.CLHP);
                    listBoxCarSafeWait.SelectedIndex = listBoxCarSafeWait.Items.Count - 1;
                    moneymodel.CLID = safemodel.CLID;
                    moneymodel.CLHP = safemodel.CLHP;
                    moneymodel.DLSJ = safemodel.DLSJ;
                    moneymodel.SAFE = "1";
                    moneymodel.EXHAUST = "0";
                    moneymodel.SAFESTANDARDFY = safeFyStandard.ToString("0.0");
                    moneymodel.SAFEFY = safeFy.ToString("0.0");
                    moneymodel.SAFEBZ = safeBZ;
                    moneymodel.EXHAUSTSTANDARDFY = exhaustFyStandard.ToString("0.0");
                    moneymodel.EXHAUSTFY = exhaustFy.ToString("0.0");
                    moneymodel.EXHAUSTBZ = exhaustBZ;
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
                    MessageBox.Show("安检添加失败", "系统提示");
                    return;
                }
            }
            else if (checkBoxExhaust.Checked==true)//如果参检环保
            {
                if (checkBoxSafe.Checked == true)
                {
                    int safeJccs = int.Parse(labelSafeJccs.Text);
                    if (safeJccs % 2 == 1)
                    {
                        safeFy = 100;
                    }
                    else
                    {
                        safeFy = 0;
                        safeBZ = "复检";
                    }
                }
                else
                {
                    safeBZ = "未参检";
                }
                foreach (Control control in panel2.Controls)
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
                model.SCRQ = dateZCRQ.Value;
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
                logininfcontrol.saveCarInfbyPlate(model);
                waitmodel.CLHP = comboBoxCLPH.Text;
                waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                waitmodel.DLSJ = loginTime;
                waitmodel.JCCS = textBoxJCCS.Text.Trim();
                waitmodel.CPYS = comboBoxCPYS.Text;
                waitmodel.DLY = mainPanel.nowUser.userName;
                waitmodel.JSY = "";
                waitmodel.CZY = "";
                waitmodel.TEST = "N";
                if (checkBoxSafe.Checked)
                {
                    safemodel.CLID = waitmodel.CLID;
                    safemodel.DLSJ = loginTime;
                    safemodel.JCSJ = loginTime;
                    safemodel.CLHP = comboBoxCLPH.Text;
                    safemodel.JCCS = labelSafeJccs.Text;
                    safemodel.JCJG = "--";
                    safemodel.SFJS = "N";
                    safemodel.SFLQ = "N";
                    safemodel.JCFY = safeFy.ToString("0.0");
                }
                //waitmodel.XSLC = textBoxXSLC.Text;
                if (int.Parse(model.ZZL) <= 3500)
                {
                    exhaustFy = double.Parse(stationcontrol.getFYbyjcff("LIGHT"));
                    cxlb = "轻型车";
                }
                else if (int.Parse(model.ZZL) > 3500 && int.Parse(model.ZZL) <= 10000)
                {
                    exhaustFy = double.Parse(stationcontrol.getFYbyjcff( "MIDDLE"));
                    cxlb = "中型车";
                }
                else
                {
                    exhaustFy = double.Parse(stationcontrol.getFYbyjcff("HEAVY"));
                    cxlb = "重型车";
                }
                exhaustFyStandard = exhaustFy;//标准费用
                if (model.RLZL.Contains("柴油"))
                {
                    if (model.QDXS != "全时四驱" && model.CHZZ != "不可摘除" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
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
                                if(!checkBoxSafe.Checked) 
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                else
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                    if (!checkBoxSafe.Checked)
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    else
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                if (!checkBoxSafe.Checked)
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                else
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    
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
                                    if (!checkBoxSafe.Checked)
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    else
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                if (!checkBoxSafe.Checked)
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                else
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                            if (!checkBoxSafe.Checked)
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                            else
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                    if (model.QDXS != "全时四驱" && model.CHZZ != "不可摘除")
                    {
                        if (int.Parse(model.ZZL) > 3500)
                        {
                            linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
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
                                exhaustBZ += "(" + linestring + "双)";
                                if (!checkBoxSafe.Checked)
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                else
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                if (mainPanel.stationinfmodel.STATIONJCFF == "ASM")
                                {
                                    exhaustBZ += "(" + linestring + "稳)";
                                    if (!checkBoxSafe.Checked)
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    else
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                }
                                else
                                {
                                    exhaustBZ += "(" + linestring + "瞬)";
                                    if (!checkBoxSafe.Checked)
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    else
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                            exhaustBZ += "(" + linestring + "双)";
                            if (!checkBoxSafe.Checked)
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                            else
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                if (checkBoxSafe.Checked)
                {
                    if (logininfcontrol.saveCarSafebj(safemodel) == true)
                    {
                        listBoxCarSafeWait.Items.Add(safemodel.CLHP);
                        listBoxCarSafeWait.SelectedIndex = listBoxCarSafeWait.Items.Count - 1;
                        addmsg += "安检添加成功.";
                    }
                    else
                    {
                        addmsg += "未知原因导致安检添加失败.";
                        addSuccess = false;
                    }
                }
                if (checkBoxExhaust.Checked)
                {
                    string addmessage = logininfcontrol.addCarToWaitList(waitmodel);
                    if (addmessage == "环检添加成功")
                    {
                        addmsg += "环检添加成功.";
                        listBoxCarAtWait.Items.Add(model.CLHP);
                        listBoxCarAtWait.SelectedIndex = listBoxCarAtWait.Items.Count - 1;
                    }
                    else
                    {
                        addmsg += "未知原因导致环检添加失败.";
                        addSuccess = false;
                    }
                }
                if(addSuccess)
                {
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                    buttonSave.Enabled = false;
                    buttonClear.Enabled = false;
                    moneymodel.CLID = waitmodel.CLID;
                    moneymodel.CLHP = waitmodel.CLHP;
                    moneymodel.DLSJ = waitmodel.DLSJ;
                    moneymodel.SAFE = checkBoxSafe.Checked?"1":"0";
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
            }
        }*/
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
    /*
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
            CARATWAIT model = logininfcontrol.getCarInfatWaitList(listBoxCarAtWait.SelectedItem.ToString());
            if (model.CLID != "-2")
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
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);
                        MessageBox.Show("删除成功，退费：" + model.JCFY + "元，请至收费台进行退费", "系统提示");
                    }
                    else
                    {
                        MessageBox.Show("删除成功，未产生退费费用", "系统提示");
                    }
                }
                //MessageBox.Show("删除成功，退费："+model.JCFY+"元","系统提示");
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
            foreach (Control control in groupBox4.Controls)
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
            string cjclph = comboBoxCLPH.Text;
            CARDETECTED latestRecord = logininfcontrol.getPreTestDatebyPlate(cjclph);
            CARSAFEDETECTED latestSafeRecord = logininfcontrol.getPreSafeTestDatebyPlate(cjclph);
            if (checkBoxExhaust.Checked == true && checkBoxSafe.Checked == false)//只参加环保
            {
                if (latestRecord.CLID == "-2")//如果没有记录，则提示为初检
                {
                    if (MessageBox.Show("该车为初次环保检测，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                    {
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
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
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
            else if (checkBoxExhaust.Checked == false && checkBoxSafe.Checked == true)//如果只参加安检检查
            {
                if (latestSafeRecord.CLID == "-2")//如果没有记录，则提示为初检
                {
                    if (MessageBox.Show("该车为初次安全性能检测，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                    {
                        //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                        labelSafeJccs.Text = "1";
                        //textBoxJCCS.Text = "1";
                        //groupBox1.Enabled = true;
                        //groupBox2.Enabled = true;
                        //groupBox3.Enabled = true;
                        buttonSave.Enabled = true;
                        buttonClear.Enabled = true;
                    }
                    else
                    {
                        return;
                    }
                }
                else//如果有记录，则判定记录内容
                {
                    int latestMonths = caculateMonth(latestSafeRecord.JCSJ, DateTime.Now);//计算最近的检测记录距离此次的时间
                    if (latestMonths > 9)//如果距离超过9个月，则认为达到检测时间
                    {
                        if (MessageBox.Show("该车为年检车辆，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                        {
                            //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                            labelSafeJccs.Text = "1";
                            //textBoxJCCS.Text = "1";
                            //groupBox1.Enabled = true;
                            //groupBox2.Enabled = true;
                            //groupBox3.Enabled = true;
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
                        if (latestSafeRecord.JCJG == "合格")//如果有记录上次检测已经合格，则提示相关信息
                        {
                            if (MessageBox.Show("该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测合格，确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                            {
                                //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                labelSafeJccs.Text = "1";
                                //textBoxJCCS.Text = "1";
                                //groupBox1.Enabled = true;
                                //groupBox2.Enabled = true;
                                //groupBox3.Enabled = true;
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
                            int days = caculateDays(latestSafeRecord.JCSJ, DateTime.Now);
                            if (days > 30)//如果超过复检规定时间，则视为初次检测，缴全款
                            {
                                if (MessageBox.Show("该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格，已超过复检时间，确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = "1";
                                    //textBoxJCCS.Text = "1";
                                    //groupBox1.Enabled = true;
                                    //groupBox2.Enabled = true;
                                    //groupBox3.Enabled = true;
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
                                int jccs = int.Parse(latestSafeRecord.JCCS) + 1;
                                if (MessageBox.Show("该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格，此次为第" + (jccs - 1).ToString() + "次复检，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = jccs.ToString();
                                    //textBoxJCCS.Text = "1";
                                    //groupBox1.Enabled = true;
                                    //groupBox2.Enabled = true;
                                    //groupBox3.Enabled = true;
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
                buttonSave.Enabled = true;
                buttonClear.Enabled = true;
            }
            else if (checkBoxExhaust.Checked == true && checkBoxSafe.Checked == true)//如果同时参检环保和安检
            {
                if (latestSafeRecord.CLID == "-2" && latestRecord.CLID == "-2")//如果环保和安检均没有记录，则提示为初检
                {
                    if (MessageBox.Show("该车为初次检测，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                    {
                        //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                        labelSafeJccs.Text = "1";
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
                else//如果有记录，则判定记录内容
                {
                    int latestMonths = caculateMonth(latestRecord.JCSJ, DateTime.Now);//计算最近的检测记录距离此次的时间
                    int latestSafeMonths = caculateMonth(latestSafeRecord.JCSJ, DateTime.Now);//计算最近的安检检测记录距离此次的时间
                    if (latestMonths > 9 || latestSafeMonths > 9)//如果有一项距离超过9个月，则认为达到检测时间
                    {
                        if (MessageBox.Show("该车为年检车辆，确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                        {
                            //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                            labelSafeJccs.Text = "1";
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
                        if (latestRecord.JCJG == "合格" && latestSafeRecord.JCJG == "合格")//如果上次记录两者均合格
                        {
                            if (MessageBox.Show("该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测合格\n" + "该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测合格\n确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                            {
                                //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                labelSafeJccs.Text = "1";
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
                        else if (latestRecord.JCJG == "合格" && latestSafeRecord.JCJG == "不合格")//如果安全性能检测不合格
                        {
                            int days = caculateDays(latestSafeRecord.JCSJ, DateTime.Now);
                            if (days > 30)//如果超过复检规定时间，则视为初次检测，缴全款
                            {
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测合格\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格，已超过复检时间\n确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = "1";
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
                                int jccs = int.Parse(latestSafeRecord.JCCS) + 1;
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测合格\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格，此次为第" + (jccs - 1).ToString() + "次复检\n确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = jccs.ToString();
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

                        }
                        else if (latestRecord.JCJG == "不合格" && latestSafeRecord.JCJG == "合格")//如果环保不合格
                        {
                            int days = caculateDays(latestRecord.JCSJ, DateTime.Now);
                            if (days > 30)//如果超过复检规定时间，则视为初次检测，缴全款
                            {
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，已超过复检时间\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测合格\n确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = "1";
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
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，此次为第" + (jccs - 1).ToString() + "次复检\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测合格\n确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = "1";
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
                        else
                        {
                            int days = caculateDays(latestRecord.JCSJ, DateTime.Now);
                            int safeDays = caculateDays(latestSafeRecord.JCSJ, DateTime.Now);
                            if (days > 30 && safeDays > 30)//如果超过复检规定时间，则视为初次检测，缴全款
                            {
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，已超过复检时间\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格，已超过复检时间\n确认再次参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = "1";
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
                            else if (days > 30 && safeDays <= 30)
                            {
                                int jccs = int.Parse(latestSafeRecord.JCCS) + 1;
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，已超过复检时间\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格,此次为第" + (jccs - 1).ToString() + "次复检\n确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = jccs.ToString();
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
                            else if (days <= 30 && safeDays > 30)
                            {
                                int jccs = int.Parse(latestRecord.JCCS) + 1;
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，此次为第" + (jccs - 1).ToString() + "次复检\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格,已超过复检时间\n确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = "1";
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
                            else
                            {
                                int jccs = int.Parse(latestRecord.JCCS) + 1;
                                int safeJccs = int.Parse(latestSafeRecord.JCCS) + 1;
                                if (MessageBox.Show("该车于" + latestRecord.JCSJ.ToShortDateString() + "环保检测不合格，此次为第" + (jccs - 1).ToString() + "次复检\n" + "该车于" + latestSafeRecord.JCSJ.ToShortDateString() + "安全性能检测不合格,此次为第" + (safeJccs - 1).ToString() + "次复检\n确认参检？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)//如果确认参检
                                {
                                    //jccs = logininfcontrol.getJccsbyClph(model.CLHP);
                                    labelSafeJccs.Text = safeJccs.ToString();
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CARSAFEDETECTED model = logininfcontrol.getCarSafeAtWaitByPlate(listBoxCarSafeWait.SelectedItem.ToString());
            if (model.CLID != "-2")
            {
                logininfcontrol.deleteCarSafeAtWaitbyPlate(listBoxCarSafeWait.SelectedItem.ToString());
                listBoxCarSafeWait.Items.Remove(listBoxCarSafeWait.SelectedItem.ToString());
                listBoxCarSafeWait.SelectedIndex = listBoxCarSafeWait.Items.Count - 1;
                if (mainPanel.moneyinfcontrol.checkMoneyIsAlive(model.CLID))//未缴费,更改费用信息
                {

                    double exhaustfy = 0.0;
                    double safefy = 0.0;
                    MONEY moneymodel = mainPanel.moneyinfcontrol.GetMoneyInf(model.CLID);
                    if (moneymodel.EXHAUST == "0")//如果不检环检，则直接删除费用信息
                    {
                        mainPanel.moneyinfcontrol.deleteOneMoney(model.CLID);
                        MessageBox.Show("删除成功", "系统提示");
                    }
                    else
                    {
                        moneymodel.SAFE = "0";
                        moneymodel.SAFEFY = "0.0";
                        moneymodel.SAFEBZ = "未参检";
                        moneymodel.SAFESTANDARDFY = "0.0";
                        exhaustfy = double.Parse(moneymodel.EXHAUSTFY);
                        moneymodel.FY = (exhaustfy + safefy).ToString("0.0");
                        mainPanel.moneyinfcontrol.updateMoneyInf(moneymodel);
                        MessageBox.Show("删除成功，该车环检还未缴费，请前往收费台缴费", "系统提示");

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
                    tffy = -double.Parse(moneymodel.SAFEFY);
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
                        moneymodel.CZ = textBoxCZ.Text;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);
                        MessageBox.Show("删除成功，退费：" + model.JCFY + "元，请至收费台进行退费", "系统提示");
                    }
                    else
                    {
                        MessageBox.Show("删除成功，未产生退费费用", "系统提示");
                    }
                }
                //MessageBox.Show("删除成功，退费：" + model.JCFY + "元", "系统提示");
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            init_safewaitList();
        }
      */
    }
}
