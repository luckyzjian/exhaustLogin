using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SYS.Model;
using SYS_DAL;
using SYS_MODEL;
using DevComponents.DotNetBar;

namespace exhaustLogin
{
    public partial class netLogin : Office2007Form
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
        List<string> asmNotNeedControl;// = { "comboBoxDLSP", "textBoxDWS", "textBoxQGS", "comboBoxOBD", "comboBoxSRL", "textBoxEDZS", "textBoxRYPH", "comboBoxDKGY", "textBoxQDLTQY", "textBoxSCS", "textBoxFDJSCQY" };
        
        public netLogin()
        {
            InitializeComponent();
        }

        private void netLogin_Load(object sender, EventArgs e)
        {
            if (!mainPanel.isRegistered)
            {
                if (axAC_Vehicle1.AC_INIT(mainPanel.netInf.MOE_ip,mainPanel.netInf.StationServer_ip,mainPanel.netInf.StationNumber,mainPanel.netInf.InterfaceNumber,mainPanel.netInf.PlatePrefix) == 0)
                    mainPanel.isRegistered = true;
                else
                {
                    mainPanel.isRegistered = false;
                    MessageBox.Show("连接环保局服务器失败","提示");
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try//如果参检环保
            {
                MONEY moneymodel = new MONEY();
                double safeFyStandard = 100;
                double exhaustFyStandard = 0;
                string safeBZ = "";
                string exhaustBZ = "";
                double safeFy = 0;
                double exhaustFy = 0;
                double totalFy = 0;
                RMBCapitalization rmbcapitalization = new RMBCapitalization();
                string linestring = "";
                string fy = "";
                string cxlb = "";
                CARINF model = new CARINF();
                CARATWAIT waitmodel = new CARATWAIT();
                CARSAFEDETECTED safemodel = new CARSAFEDETECTED();
                DateTime loginTime = DateTime.Now;//.Replace(':', ' ').Replace('/', ' ').Replace('-', ' ').Replace(" ","");
                model.CLHP = axAC_Vehicle1.CPHM==null?"": axAC_Vehicle1.CPHM;
                model.CPYS = axAC_Vehicle1.CPYS == null ? "" : axAC_Vehicle1.CPYS;
                model.CLLX = axAC_Vehicle1.CLLX == null ? "" : axAC_Vehicle1.CPYS;
                model.CZ = axAC_Vehicle1.CZMC == null ? "" : axAC_Vehicle1.CPHM;
                model.SYXZ = axAC_Vehicle1.SYXZ == null ? "" : axAC_Vehicle1.CPHM;
                model.PP = axAC_Vehicle1.CPMC == null ? "" : axAC_Vehicle1.CPMC;
                model.XH = axAC_Vehicle1.CPXH == null ? "" : axAC_Vehicle1.CPXH;
                model.CLSBM = axAC_Vehicle1.CLSBM == null ? "" : axAC_Vehicle1.CLSBM;
                model.FDJHM = "";
                model.FDJXH = axAC_Vehicle1.FDJXH == null ? "" : axAC_Vehicle1.FDJXH;
                model.SCQY = axAC_Vehicle1.CLSCQY == null ? "" : axAC_Vehicle1.CLSCQY;
                model.HDZK = axAC_Vehicle1.SJCYS == null ? "" : axAC_Vehicle1.SJCYS;
                model.JSSZK = "";
                model.ZZL = axAC_Vehicle1.ZDZZL == null ? "" : axAC_Vehicle1.ZDZZL;
                model.HDZZL = "";
                model.JZZL = axAC_Vehicle1.JZZL == null ? "2000" : axAC_Vehicle1.JZZL;
                model.ZBZL = (int.Parse(model.JZZL)-100).ToString();
                model.ZCRQ =axAC_Vehicle1.CCDJRQ==null? DateTime.Parse("2001-10-10"): DateTime.Parse(axAC_Vehicle1.CCDJRQ.Substring(0,4)+"-"+ axAC_Vehicle1.CCDJRQ.Substring(4, 2) + "-"+axAC_Vehicle1.CCDJRQ.Substring(6, 2));
                model.SCRQ = axAC_Vehicle1.CCRQ == null ? DateTime.Parse("2001-10-10") : DateTime.Parse(axAC_Vehicle1.CCRQ.Substring(0, 4) + "-" + axAC_Vehicle1.CCRQ.Substring(4, 2) + "-" + axAC_Vehicle1.CCRQ.Substring(6, 2));
                model.FDJPL = axAC_Vehicle1.FDJPL == null ? "" : axAC_Vehicle1.FDJPL;
                model.RLZL = axAC_Vehicle1.RYGG == null ? "" : axAC_Vehicle1.RYGG;
                model.EDGL = axAC_Vehicle1.FDJEDGL == null ? "" : axAC_Vehicle1.FDJEDGL;
                model.EDZS = axAC_Vehicle1.FDJEDZS == null ? "" : axAC_Vehicle1.FDJEDZS;
                model.BSQXS = axAC_Vehicle1.BSQXS == null ? "" : axAC_Vehicle1.BSQXS;
                model.DWS = axAC_Vehicle1.DWS == null ? "" : axAC_Vehicle1.DWS;
                model.GYFS = axAC_Vehicle1.RYXS == null ? "" : axAC_Vehicle1.RYXS;
                model.DPFS = "";
                model.JQFS = axAC_Vehicle1.JQFS == null ? "" : axAC_Vehicle1.JQFS;
                model.QGS = axAC_Vehicle1.QGS == null ? "" : axAC_Vehicle1.QGS;
                model.QDXS = axAC_Vehicle1.QDFS == null ? "" : axAC_Vehicle1.QDFS;
                model.CHZZ = "";
                model.DLSP = "";
                model.SFSRL = "";
                model.JHZZ = axAC_Vehicle1.CHZHQ == null ? "" : axAC_Vehicle1.CHZHQ;
                model.OBD = "";
                model.DKGYYB = "";
                model.LXDH = axAC_Vehicle1.LXDH == null ? "" : axAC_Vehicle1.LXDH;
                model.CZDZ = axAC_Vehicle1.LXDZ == null ? "" : axAC_Vehicle1.LXDZ;
                model.JCFS = axAC_Vehicle1.JCFS == null ? "" : axAC_Vehicle1.JCFS;
                model.JCLB = axAC_Vehicle1.JCLB == null ? "" : axAC_Vehicle1.JCLB;
                model.CLZL = axAC_Vehicle1.CLZL == null ? "" : axAC_Vehicle1.CPHM;
                model.SSXQ = axAC_Vehicle1.SSXQ == null ? "" : axAC_Vehicle1.SSXQ;
                model.SFWDZR = axAC_Vehicle1.SFWDZR == null ? "" : axAC_Vehicle1.SFWDZR;
                model.SFYQBF = axAC_Vehicle1.SFYQBF == null ? "" : axAC_Vehicle1.SFYQBF;
                model.FDJSCQY = axAC_Vehicle1.FDJSCQY == null ? "" : axAC_Vehicle1.FDJSCQY;
                model.QDLTQY = axAC_Vehicle1.QDLTQY == null ? "" : axAC_Vehicle1.QDLTQY;
                model.RYPH = axAC_Vehicle1.RYPH == null ? "" : axAC_Vehicle1.RYPH;
                model.ZXBZ = "";
                model.HPZL = axAC_Vehicle1.RYPH == null ? "" : axAC_Vehicle1.RYPH;

                logininfcontrol.saveCarInfbyPlate(model);
                string ecrypt = "";
                if (axAC_Vehicle1.AC_RECV(out ecrypt) == 0)
                {
                    if (!ecrypt.Contains("|"))
                    {
                        MessageBox.Show(ecrypt, "提示");
                        return;                        
                    }

                    waitmodel.CLHP = model.CLHP;
                    waitmodel.CLID = waitmodel.CLHP + "T" + loginTime.ToString().Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "");
                    waitmodel.DLSJ = loginTime;
                    waitmodel.JCCS = textBoxJCCS.Text.Trim();
                    waitmodel.CPYS = model.CPYS;
                    waitmodel.DLY = mainPanel.nowUser.userName;
                    waitmodel.JSY = "";
                    waitmodel.CZY = "";
                    waitmodel.TEST = mainPanel.isNetUsed ? "Y" : "N";
                    waitmodel.ECRYPT = ecrypt.Split('|')[0].Split('=')[1];
                    waitmodel.JCBGBH = "--";
                    waitmodel.JCGWH = "--";
                    waitmodel.JCZBH = mainPanel.stationinfmodel.STATIONID;
                    waitmodel.JCRQ = DateTime.Now;
                    waitmodel.BGJCFFYY = "--";
                    waitmodel.SFCS = "0";
                    
                    string[] carnetinf = ecrypt.Split('|');
                    int getinfcount = 0;
                    foreach (string netinf in carnetinf)
                    {
                        if (netinf.Contains("JYLSH"))
                        {
                            waitmodel.JYLSH = netinf.Split('=')[1];
                            getinfcount++;
                        }
                        if (netinf.Contains("JCCS"))
                        {
                            waitmodel.JCCS = netinf.Split('=')[1];
                            getinfcount++;
                            textBoxJCCS.Text = waitmodel.JCCS.ToString();
                        }
                        if (netinf.Contains("SFCJ"))
                        {
                            waitmodel.SFCJ = netinf.Split('=')[1];
                            getinfcount++;
                        }
                        if (getinfcount >= 3)
                            break;
                    }
                    if (getinfcount < 3)
                    {
                        MessageBox.Show("车辆注册返回信息有误", "提示");
                        return;
                    }
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
                    string qdxs = logininfcontrol.getComBoBoxItemsNAME("驱动形式", model.QDXS);
                    string syxz = logininfcontrol.getComBoBoxItemsNAME("使用性质", model.SYXZ);
                    if (logininfcontrol.getComBoBoxItemsNAME("燃料种类", model.RLZL).Contains("柴油"))
                    {

                        if (qdxs != "四驱" && DateTime.Compare(model.SCRQ, Convert.ToDateTime("2001-10-01")) >= 0)//2001年10月1日 起生产的汽车，轻型车额定功率不超过150KW，重型车额定功率不超过350KW
                        {
                            if (int.Parse(model.ZZL) > 3500 && int.Parse(model.EDGL) <= 350)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "JZJS_HEAVY");
                                if (linestring != "-2")
                                {
                                    if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                    if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                    MessageBox.Show("环保:请到" + linestring + "进行加载减速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    waitmodel.JCFF = "JZJS";
                                }
                                else
                                {
                                    linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "ZYJS");
                                    if (linestring != "-2")
                                    {
                                        if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                        MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                    if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                    MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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

                                if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                MessageBox.Show("环保:请到" + linestring + "进行自由加速不透光检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                        if (qdxs != "四驱")
                        {
                            if (int.Parse(model.ZZL) > 3500)
                            {
                                linestring = stationcontrol.getLineByJCFF(mainPanel.stationid, "SDS");
                                if (linestring != "-2")
                                {
                                    if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                    MessageBox.Show("环保:请到" + linestring + "进行双怠速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                    if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                        MessageBox.Show("环保:请到" + linestring + "进行稳态工况检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                    }
                                    else
                                    {
                                        exhaustBZ += "(" + linestring + "瞬)";
                                        MessageBox.Show("环保:请到" + linestring + "进行简易瞬态工况检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
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
                                if (syxz.Contains("出租") || syxz.Contains("公交"))
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
                                MessageBox.Show("环保:请到" + linestring + "进行双怠速检测，费用：" + exhaustFy.ToString("0.0") + "元\n" + "安检:未参检,费用：" + safeFy.ToString("0.0") + "元\n" + "总计:" + totalFy.ToString("0.0") + "元\n");
                                waitmodel.JCFF = "SDS";
                            }
                            else
                            {
                                MessageBox.Show("该检测站未配置双怠速线，该车不能参检");
                                return;
                            }
                        }
                    }

                    waitmodel.XSLC = axAC_Vehicle1.LJXSLC==null?"": axAC_Vehicle1.LJXSLC;
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
                        }
                        else
                        {
                            addmsg += addmessage;
                            addSuccess = false;
                        }
                    }
                    if (addSuccess)
                    {

                        //buttonSave.Enabled = false;
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
                        moneymodel.CZ = axAC_Vehicle1.CZMC;
                        mainPanel.moneyinfcontrol.savemoneyInf(moneymodel);

                    }
                    else
                    {
                        MessageBox.Show(addmsg);
                    }
                }
                else
                {
                    MessageBox.Show("提交车辆信息失败", "提示");
                    return;
                }
            }
            catch(Exception er)
            {
                MessageBoxEx.Show("保存过程中出现异常:" + er.Message);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CARATWAIT model = logininfcontrol.getCarInfatWaitList(listBoxCarAtWait.SelectedItem.ToString());
            CARINF modelcarinf = logininfcontrol.getCarInfbyPlate(listBoxCarAtWait.SelectedItem.ToString());
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
                        moneymodel.CZ = modelcarinf.CZ;
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
        private void init_waitList()
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

        private void button2_Click(object sender, EventArgs e)
        {
            string plateSearch =textBoxX1.Text.ToUpper();
            for (int i = 0; i < listBoxCarAtWait.Items.Count; i++)
            {
                if (listBoxCarAtWait.Items[i].ToString() == plateSearch)
                {
                    listBoxCarAtWait.SelectedIndex = i;
                    return;
                }
            }
            MessageBox.Show("未找到该车辆,请先登录");
        }

    }
}
