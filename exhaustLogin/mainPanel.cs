using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SYS_DAL;
using SYS_MODEL;
using carinfor;
using NhWebClient;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using JHWebClient;
using System.Linq;
using JxWebClient;
using ini;
using DevComponents.DotNetBar;
using HnWebClient;
using System.Collections;
using System.Drawing;
using DlWebClient;
using GAINTER;
using EzWebClient;
using HhzWebClient;

namespace exhaustLogin
{
    public partial class mainPanel : Office2007Form
    {
        public static gaInterface gainterface = null;
        public static stationControl stationcontrol = new stationControl();
        public static stationInfModel stationinfmodel = new stationInfModel();
        public static loginInfControl logininfcontrol = new loginInfControl();
        public static moneyInfControl moneyinfcontrol = new moneyInfControl();
        public static moneyRecordInfControl moneyrecordinfcontrol = new moneyRecordInfControl();
        public static stationModel stationmodel = new stationModel();
        public static othersModel otherinf = new othersModel();
        public static demarcateControl demarcatecontrol = new demarcateControl();
        public static stationRzxx stationrzxx = new stationRzxx();
        public static CcSocketInf ccsocketinf = new CcSocketInf();
        public static chenChuangSocketControl ccsocket = null;
        public static carinfor.GAWebInf gawebinf = new GAWebInf();
        public static carinfor.NHWebInf nhwebinf = new carinfor.NHWebInf();
        public static carinfor.JXWebInf jxwebinf = new JXWebInf();
        public static carinfor.HNHYWebInf hnhywebinf = new HNHYWebInf();
        public static carinfor.DALIWebInf daliwebinf = new DALIWebInf();
        public static carinfor.OrtSocketInf ortsocketinf = new OrtSocketInf();
        public static carinfor.ortSocketControl ortsocket = new ortSocketControl();
        public static carinfor.HHZWebInf hhzwebinf = new HHZWebInf();
        public static int linesCount = 0;
        public static string stationid = "";
        public static string lineid = "";
        public static string printerName = "";
        public static bool isdisplayCMA = false;
        public static bool isdisplayCMANo = false;
        public static string CMABH = "";
        public static string CMAYXQ = "";
        public static string XKZH = "";
        public static string XKZYXQ = "";
        public static int copies=1;
        public static string jyy = "";
        public static bool isNetUsed = false;
        private int carDetectedCount = 0;
        private int carIsOkCount = 0;
        private int carIsNotOkCount = 0;
        private int carIsOkRate = 0;
        private int carAtWaitCount = 0;
        private int refreshCount = 300;
        public static bool isPrintPay=false;
        public static bool isRegistered = false;
        public static workLogData worklogdata = new workLogData();
        public static string addname="";
        public static string reportStyle = "";
        public static bool lz = false;
        public static bool isdisplayResult = true;
        public static bool printJccs = false;
        public static string localIP = "127.0.0.1";

        public static string zjurl;
        public static bool startAutoPrint = false;
        public static int autoPrintScanSeconds = 30;
        public static DateTime autoPrintStarttime = DateTime.Now;
        public static DateTime autoPrintEndtime = DateTime.Now;


        public const string acArea_NN = "辽宁";
        public const string acArea_Other = "其他";


        public struct NetInformation
        {
            public string MOE_ip;
            public string StationServer_ip;
            public string StationNumber;
            public string InterfaceNumber;
            public string PlatePrefix;
            public string Area;
        }
        public struct user
        {
            public string userName;
            public string postID;
            public string userID;
            public string postName;
            public string password;
        }
        public static NetInformation netInf=new NetInformation();
        public static user nowUser = new user();
        public static bool userLoginSuccess=false;

        public static string wgjcy = "";
        public static string bjy = "";

        public static string netMode="";
        public static bool isQueryInfFromGA = false;
        public static double fontSize = 10.5;
        public enum LoginDefaultMode {DEFAULT_SQL,DEFAULT_INI};
        public static LoginDefaultMode loginmode = LoginDefaultMode.DEFAULT_SQL;
        public static string ACNETMODE = "安车联网";
        public static string CCNETMODE = "诚创联网";
        public static string NHNETMODE = "南华联网";
        public static string ZJNETMODE = "浙江联网";
        public static string JXNETMODE = "江西联网";
        public static string HNNETMODE = "湖南联网";
        public static string DALINETMODE = "大理联网";
        public static string ORTNETMODE = "欧润特联网";
        public const string EZNETMODE = "鄂州联网";
        public const string HHZNNETMODE = "红河州联网";

        public static NhWebControl nhinterface = null;//南华接口
        public static jxWebControl jxinterface = null;
        public static hnWebControl hninterface = null;
        public static daliInterface daliinterface = null;
        /*鄂州联网*/
        public static EzWebClient.Ezclient ezinterface = null;
        public static EZWebInf ezwebinf = new EZWebInf();
        public static Hhzclient hhzinterface = null;

        public mainPanel()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool init_stationinf()
        {
            StringBuilder temp = new StringBuilder();
            temp.Length = 2048;
            ini.INIIO.GetPrivateProfileString("检测线", "地区", "", temp, 2048, @".\appConfig.ini");
            addname = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("检测线", "报表样式", "", temp, 2048, @".\appConfig.ini");
            reportStyle = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("检测线", "滤纸", "", temp, 2048, @".\appConfig.ini");
            lz = (temp.ToString().Trim() == "Y");
            ini.INIIO.GetPrivateProfileString("检测线", "显示检测次数", "", temp, 2048, @".\appConfig.ini");
            printJccs = (temp.ToString().Trim() == "Y");
            ini.INIIO.GetPrivateProfileString("数据", "服务器", "", temp, 2048, @".\appConfig.ini");
            string severIP = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("数据", "本机IP", "", temp, 2048, @".\appConfig.ini");
            localIP = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("界面设置", "字体大小", "9", temp, 2048, @".\appConfig.ini");
            fontSize =double.Parse( temp.ToString().Trim());
            ribbonBar1.Font =new Font("宋体",(float)fontSize);
            ini.INIIO.GetPrivateProfileString("界面设置", "登录默认", "0", temp, 2048, @".\appConfig.ini");
            loginmode = (LoginDefaultMode)(int.Parse(temp.ToString().Trim()));
            //if (!ping(severIP))
            //{
            //    MessageBox.Show("连接到本地服务器失败");
            //    return false;
            //}
            stationid = stationcontrol.getStationID();
            stationinfmodel = stationcontrol.getStationInf(stationid);
            linesCount = stationcontrol.getStationLineCount(stationid);
            otherinf = stationcontrol.getOtherInf();
            stationrzxx = stationcontrol.getStationRzInf(stationid);
            
            ini.INIIO.GetPrivateProfileString("设备", "printer", "", temp, 2048, @".\appConfig.ini");
            printerName = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("设备", "份数", "", temp, 2048, @".\appConfig.ini");
            copies = int.Parse(temp.ToString().Trim());
            ini.INIIO.GetPrivateProfileString("设备", "是否打印票据", "", temp, 2048, @".\appConfig.ini");
            if (temp.ToString().Trim() == "是")
                isPrintPay = true;
            else
                isPrintPay = false;
            ini.INIIO.GetPrivateProfileString("设备", "CMA标志显示", "", temp, 2048, @".\appConfig.ini");
            if (temp.ToString().Trim() == "是")
                isdisplayCMA = true;
            else
                isdisplayCMA = false;
            ini.INIIO.GetPrivateProfileString("设备", "CMA编号显示", "", temp, 2048, @".\appConfig.ini");
            if (temp.ToString().Trim() == "是")
                isdisplayCMANo = true;
            else
                isdisplayCMANo = false;
            //ini.INIIO.GetPrivateProfileString("工作模式", "模式", "", temp, 2048, @".\appConfig.ini");
            ini.INIIO.GetPrivateProfileString("工作模式", "联网运行", "N", temp, 2048, @".\appConfig.ini");
            if (temp.ToString() == "Y")
                isNetUsed = true;
            else
                isNetUsed = false;
            ini.INIIO.GetPrivateProfileString("工作模式", "读取公安信息", "N", temp, 2048, @".\appConfig.ini");
            if (temp.ToString() == "Y")
                isQueryInfFromGA = true;
            else
                isQueryInfFromGA = false;
            ini.INIIO.GetPrivateProfileString("工作模式", "联网模式", "诚创联网", temp, 2048, @".\appConfig.ini");
            netMode = temp.ToString();

            ini.INIIO.GetPrivateProfileString("南华联网", "WEBURL", "http://localhost:50581/NHDSWebService.asmx", temp, 2048, @".\appConfig.ini");
            nhwebinf.weburl = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("南华联网", "LINEID", "01", temp, 2048, @".\appConfig.ini");
            nhwebinf.lineid = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("公安联网", "WEBURL", "http://ip:port/pnweb/services/TmriOutAccess?wsdl", temp, 2048, @".\appConfig.ini");
            gawebinf.weburl = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("公安联网", "XTLB", "18", temp, 2048, @".\appConfig.ini");
            gawebinf.xtlb = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("公安联网", "JKXLH", "", temp, 2048, @".\appConfig.ini");
            gawebinf.jkxlh = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("公安联网", "JYJGBH", "", temp, 2048, @".\appConfig.ini");
            gawebinf.jyjgbh = temp.ToString().Trim();


            ini.INIIO.GetPrivateProfileString("欧润特联网", "服务器IP", "192.168.1.188", temp, 2048, @".\appConfig.ini");
            ortsocketinf.IP = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("欧润特联网", "服务器PORT", "6005", temp, 2048, @".\appConfig.ini");
            ortsocketinf.PORT = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("浙江联网", "WEBURL", "127.0.0.1", temp, 2048, @".\appConfig.ini");
            zjurl = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("江西联网", "账号", "", temp, 2048, @".\appConfig.ini");
            jxwebinf.user = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("江西联网", "密码", "", temp, 2048, @".\appConfig.ini");
            jxwebinf.password = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("江西联网", "检测线ID", "", temp, 2048, @".\appConfig.ini");
            jxwebinf.lineid = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("江西联网", "url", "", temp, 2048, @".\appConfig.ini");
            jxwebinf.url = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("江西联网", "SOCKETIP", "", temp, 2048, @".\appConfig.ini");
            jxwebinf.socketip = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("江西联网", "SOCKETPORT", "", temp, 2048, @".\appConfig.ini");
            jxwebinf.socketport = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("湖南衡阳联网", "WEBURL", "http://192.168.1.230/jcz_jk/ExternalAccess.asmx", temp, 2048, @".\appConfig.ini");
            hnhywebinf.weburl = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("湖南衡阳联网", "STATIONID", "43042111", temp, 2048, @".\appConfig.ini");
            hnhywebinf.stationid = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("湖南衡阳联网", "LINEID", "4304211101", temp, 2048, @".\appConfig.ini");
            hnhywebinf.lineid = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("大理联网", "LINEID", "532923001", temp, 2048, @".\appConfig.ini");
            daliwebinf.LINEID = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("大理联网", "SERVERIP", "192.168.100.100", temp, 2048, @".\appConfig.ini");
            daliwebinf.SERVERIP = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("大理联网", "SERVERPORT", "9800", temp, 2048, @".\appConfig.ini");
            daliwebinf.SERVERPORT = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("红河州联网", "WEBURL", "http://172.16.1.1:8088/hhapi/unit_task/", temp, 2048, @".\appConfig.ini");
            hhzwebinf.weburl = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("红河州联网", "QUERYURL", "http://127.0.0.1:88/webservice/outaccess.asmx", temp, 2048, @".\appConfig.ini");
            hhzwebinf.queryUrl = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("红河州联网", "ENABLEQUERY", "N", temp, 2048, @".\appConfig.ini");
            hhzwebinf.enableQuery =( temp.ToString().Trim()=="Y");

            ini.INIIO.GetPrivateProfileString("联网信息", "环保局服务器IP", "", temp, 2048, @".\appConfig.ini");
            netInf.MOE_ip = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("联网信息", "检测站服务器IP", "", temp, 2048, @".\appConfig.ini");
             netInf.StationServer_ip  = temp.ToString().Trim();

            //ini.INIIO.GetPrivateProfileString("联网信息", "检测站编号", "", temp, 2048, @".\appConfig.ini");
             netInf.StationNumber  = stationid;

            ini.INIIO.GetPrivateProfileString("联网信息", "接口序列号", "", temp, 2048, @".\appConfig.ini");
             netInf.InterfaceNumber  = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("联网信息", "号牌前缀", "", temp, 2048, @".\appConfig.ini");
             netInf.PlatePrefix  = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("联网信息", "安车联网地区", "", temp, 2048, @".\appConfig.ini");
            netInf.Area = temp.ToString().Trim();

            ini.INIIO.GetPrivateProfileString("设备", "检验员", "", temp, 2048, @".\appConfig.ini");
             wgjcy = temp.ToString().Trim();
             ini.INIIO.GetPrivateProfileString("设备", "报检员", "", temp, 2048, @".\appConfig.ini");
             bjy = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("报表数据", "CMABH", "", temp, 2048, @".\appConfig.ini");
            CMABH = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("报表数据", "CMAYXQ", "", temp, 2048, @".\appConfig.ini");
            CMAYXQ = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("报表数据", "XKZH", "", temp, 2048, @".\appConfig.ini");
            XKZH = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("报表数据", "XKZYXQ", "", temp, 2048, @".\appConfig.ini");
            XKZYXQ = temp.ToString().Trim();
            //stationmodel = stationcontrol.getLineInf(stationid, lineid);
            labelStationName.Text = stationinfmodel.STATIONNAME ;
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "车辆登录")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            if (!isNetUsed)
            {
                if (isQueryInfFromGA)
                {
                    if (gainterface == null)
                    {
                        try
                        {
                            gainterface = new gaInterface(gawebinf.weburl, gawebinf.xtlb, gawebinf.jkxlh, gawebinf.jyjgbh);
                            string code, message;
                            DataTable dttime = gainterface.GetVehicleInf("", "", "", out code, out message);
                            ini.INIIO.saveLogInf("连接公安信息平台成功");
                        }
                        catch
                        {
                            MessageBox.Show("公安信息平台连接失败，不能联网查询车辆信息", "警告");
                            ini.INIIO.saveLogInf("连接公安信息平台失败");
                            isQueryInfFromGA = false;
                        }
                    }
                }
                if (isQueryInfFromGA)
                {
                    carLoginGA childcarlogin = new carLoginGA();
                    TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                    childtabitem.Name = "车辆登录";
                    childcarlogin.TopLevel = false;
                    childtabitem.AttachedControl.Controls.Add(childcarlogin);
                    childcarlogin.FormBorderStyle = FormBorderStyle.None;
                    childcarlogin.Dock = DockStyle.Fill;
                    childcarlogin.Show();
                    tabControl1.SelectedTab = childtabitem;
                }
                else
                {
                    carLoginNew childcarlogin = new carLoginNew();
                    TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                    childtabitem.Name = "车辆登录";
                    childcarlogin.TopLevel = false;
                    childtabitem.AttachedControl.Controls.Add(childcarlogin);
                    childcarlogin.FormBorderStyle = FormBorderStyle.None;
                    childcarlogin.Dock = DockStyle.Fill;
                    childcarlogin.Show();
                    tabControl1.SelectedTab = childtabitem;
                }
            }
            else if (netMode == mainPanel.NHNETMODE||netMode==mainPanel.JXNETMODE||netMode==mainPanel.ORTNETMODE || netMode == mainPanel.EZNETMODE)
            {
                carLoginNew childcarlogin = new carLoginNew();
                TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                childtabitem.Name = "车辆登录";
                childcarlogin.TopLevel = false;
                childtabitem.AttachedControl.Controls.Add(childcarlogin);
                childcarlogin.FormBorderStyle = FormBorderStyle.None;
                childcarlogin.Dock = DockStyle.Fill;
                childcarlogin.Show();
                tabControl1.SelectedTab = childtabitem;
            }
            else if (netMode == mainPanel.DALINETMODE)
            {
                carLoginDali childcarlogin = new carLoginDali();
                TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                childtabitem.Name = "车辆登录";
                childcarlogin.TopLevel = false;
                childtabitem.AttachedControl.Controls.Add(childcarlogin);
                childcarlogin.FormBorderStyle = FormBorderStyle.None;
                childcarlogin.Dock = DockStyle.Fill;
                childcarlogin.Show();
                tabControl1.SelectedTab = childtabitem;
            }
            else if (netMode == mainPanel.HHZNNETMODE)
            {
                carLoginHHZ childcarlogin = new carLoginHHZ();
                TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                childtabitem.Name = "车辆登录";
                childcarlogin.TopLevel = false;
                childtabitem.AttachedControl.Controls.Add(childcarlogin);
                childcarlogin.FormBorderStyle = FormBorderStyle.None;
                childcarlogin.Dock = DockStyle.Fill;
                childcarlogin.Show();
                tabControl1.SelectedTab = childtabitem;
            }
            else if (netMode == mainPanel.HNNETMODE)
            {
                carLoginHN childcarlogin = new carLoginHN();
                TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                childtabitem.Name = "车辆登录";
                childcarlogin.TopLevel = false;
                childtabitem.AttachedControl.Controls.Add(childcarlogin);
                childcarlogin.FormBorderStyle = FormBorderStyle.None;
                childcarlogin.Dock = DockStyle.Fill;
                childcarlogin.Show();
                tabControl1.SelectedTab = childtabitem;
            }
            else if (netMode == mainPanel.ACNETMODE)
            {
                /*if (mainPanel.netInf.Area == mainPanel.acArea_NN)
                {
                    netLoginAcNn childcarlogin = new netLoginAcNn();
                    TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                    childtabitem.Name = "车辆登录";
                    childcarlogin.TopLevel = false;
                    childtabitem.AttachedControl.Controls.Add(childcarlogin);
                    childcarlogin.FormBorderStyle = FormBorderStyle.None;
                    childcarlogin.Dock = DockStyle.Fill;
                    childcarlogin.Show();
                    tabControl1.SelectedTab = childtabitem;
                }*/
                /*
                else
                {
                    netLogin childcarlogin = new netLogin();
                    TabItem childtabitem = this.tabControl1.CreateTab("车辆登录");
                    childtabitem.Name = "车辆登录";
                    childcarlogin.TopLevel = false;
                    childtabitem.AttachedControl.Controls.Add(childcarlogin);
                    childcarlogin.FormBorderStyle = FormBorderStyle.None;
                    childcarlogin.Dock = DockStyle.Fill;
                    childcarlogin.Show();
                    tabControl1.SelectedTab = childtabitem;
                }*/
            }
        }
        private void buttonQuery_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "统计查询")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            dataQuery childcarlogin = new dataQuery();
            TabItem childtabitem = this.tabControl1.CreateTab("统计查询");
            childtabitem.Name = "统计查询";
            childcarlogin.TopLevel = false;
            childtabitem.AttachedControl.Controls.Add(childcarlogin);
            childcarlogin.FormBorderStyle = FormBorderStyle.None;
            childcarlogin.Dock = DockStyle.Fill;
            childcarlogin.Show();
            tabControl1.SelectedTab = childtabitem;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "个人信息")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            personelInf childcarlogin = new personelInf();
            TabItem childtabitem = this.tabControl1.CreateTab("个人信息");
            childtabitem.Name = "个人信息";
            childcarlogin.TopLevel = false;
            childtabitem.AttachedControl.Controls.Add(childcarlogin);
            childcarlogin.FormBorderStyle = FormBorderStyle.None;
            childcarlogin.Dock = DockStyle.Fill;
            childcarlogin.Show();
            tabControl1.SelectedTab = childtabitem;
        }

        private bool init_ccinf()
        {
            StringBuilder temp = new StringBuilder();
            temp.Length = 2048;
            ini.INIIO.GetPrivateProfileString("诚创联网", "服务器IP", "127.0.0.1", temp, 2048, @".\appConfig.ini");
            ccsocketinf.IP = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("诚创联网", "服务器PORT", "7801", temp, 2048, @".\appConfig.ini");
            ccsocketinf.PORT = temp.ToString().Trim();
            return true;
        }

        private void mainPanel_Load(object sender, EventArgs e)
        {
            nowUser.userName = "";
            disable_function("0");//登录前未获得权限
            if (!init_stationinf())
                return;
            staffLogin stafflogin = new staffLogin();
            //stafflogin.FormBorderStyle = FormBorderStyle.None;
            
            stafflogin.ShowDialog();
            if (userLoginSuccess)
            {
                if (isQueryInfFromGA)
                {
                        try
                        {
                            gainterface = new gaInterface(gawebinf.weburl,gawebinf.xtlb,gawebinf.jkxlh,gawebinf.jyjgbh);
                            string code,message;
                            DataTable dttime = gainterface.GetVehicleInf("","","",out code,out message);
                        ini.INIIO.saveLogInf("连接公安信息平台成功");
                    }
                        catch
                        {
                            MessageBox.Show("公安信息平台连接失败，不能联网查询车辆信息", "警告");
                        ini.INIIO.saveLogInf("连接公安信息平台失败");
                        isQueryInfFromGA = false;
                        }
                }
                if (isNetUsed)
                {
                    if (mainPanel.netMode == CCNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        ccsocket = new chenChuangSocketControl();
                        ccsocket.initSocket(ccsocketinf.IP, ccsocketinf.PORT);
                        string socketMsg = ccsocket.init_equipment();
                        if (socketMsg == "连接成功")
                        {
                            isNetUsed = true;
                            labelWorkMode.Text = "工作状态：连接诚创联网服务器成功";
                            ini.INIIO.saveLogInf("工作状态：连接诚创联网服务器成功");

                        }
                        else
                        {
                            isNetUsed = false;
                            labelWorkMode.Text = "工作状态：连接诚创联网服务器失败,将工作在单机模式";
                            ini.INIIO.saveLogInf("工作状态：连接诚创联网服务器失败,将工作在单机模式");
                        }
                    }
                    else if (mainPanel.netMode == ACNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                    }
                    else if (mainPanel.netMode == ZJNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                    }
                    else if (mainPanel.netMode == DALINETMODE)
                    {
                        daliinterface = new daliInterface();
                        isNetUsed = true;
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        labelWorkMode.Text = "工作状态：联网模式(大理联网)";
                    }
                    else if (mainPanel.netMode == NHNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        try
                        {
                            nhinterface = new NhWebControl(nhwebinf.weburl, stationid, nhwebinf.lineid);
                            isNetUsed = true;
                            ini.INIIO.saveLogInf("工作状态：连接南华联网服务器成功");
                            int nhresult, nhexceptionCode;
                            string nhErrMsg = "", nhExpMsg = "";
                            DateTime syntime = nhinterface.GetTimeRequest(out nhresult, out nhErrMsg, out nhexceptionCode, out nhExpMsg);
                            if (nhresult == 0 && nhexceptionCode == 0)
                            {
                                labelWorkMode.Text = "工作状态：连接南华联网服务器成功,将工作在联网模式";
                                ini.INIIO.saveLogInf("同步南华联网时间成功:" + syntime.ToString("yyyy-MM-dd HH:mm:ss"));
                            }

                        }
                        catch
                        {
                            isNetUsed = false;
                            labelWorkMode.Text = "工作状态：连接南华联网服务器失败,将工作在单机模式";
                            ini.INIIO.saveLogInf("工作状态：连接南华联网服务器失败,将工作在单机模式");
                        }
                    }
                    else if (mainPanel.netMode == JXNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        try
                        {
                            jxinterface = new jxWebControl(jxwebinf.url, jxwebinf.user, jxwebinf.password);
                            bool jxstatus;
                            string code, msg;
                            //string jserrMsg = "";
                            jxstatus = jxinterface.sendAuthorizationDataWithVersion();
                            if (jxinterface.fetchServerTime(out code, out msg))
                            {
                                string[] datetimearray = DateTime.Parse(msg).ToString("yyyy-MM-dd HH:mm:ss.fff").Split(' ');
                                string datetimereceive = datetimearray[0] + " " + datetimearray[1];
                                SetSystemDateTime.SetLocalTimeByStr(datetimereceive);
                                labelWorkMode.Text = "工作状态：连接江西联网服务器成功,将工作在联网模式";
                                ini.INIIO.saveLogInf("同步江西联网时间成功:" + msg);
                            }
                            else
                            {
                                MessageBox.Show("fetchServerTime失败，code:" + code + "\r\nmessage:" + msg);
                            }
                               

                        }
                        catch
                        {
                            isNetUsed = false;
                            labelWorkMode.Text = "工作状态：连接江西联网服务器失败,将工作在单机模式";
                            ini.INIIO.saveLogInf("工作状态：连接江西联网服务器失败,将工作在单机模式");
                        }
                    }
                    else if (mainPanel.netMode == mainPanel.HNNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        try
                        {
                            hninterface = new hnWebControl(hnhywebinf.weburl, mainPanel.nowUser.password);
                            bool hnstatus;
                            string code, msg;

                            hnstatus = hninterface.testConnect(out code, out msg);
                            if (!hnstatus)
                            {
                                MessageBox.Show("连接测试失败，code:" + code + "\r\nmessage:" + msg);
                            }
                            else
                            {
                                Hashtable ht = new Hashtable();
                                ht.Add("jczbh", hnhywebinf.stationid);
                                ht.Add("jcxbh", hnhywebinf.lineid);
                                ht.Add("dlzh", nowUser.userName);
                                ht.Add("dlkl", hninterface.md5key);
                                string forcekey = "";
                                if (hninterface.loginIn(ht, out forcekey, out code, out msg))
                                {
                                    string datetimestring = "";
                                    if (hninterface.queryTime(out datetimestring, out code, out msg))
                                    {
                                        string[] datetimearray = DateTime.Parse(datetimestring).ToString("yyyy-MM-dd HH:mm:ss.fff").Split(' ');
                                        string datetimereceive = datetimearray[0] + " " + datetimearray[1];
                                        SetSystemDateTime.SetLocalTimeByStr(datetimereceive);
                                        ini.INIIO.saveLogInf("同步时间成功");
                                    }
                                    else
                                    {
                                        ini.INIIO.saveLogInf("同步时间失败,code" + code + ",msg:" + msg);
                                    }
                                    labelWorkMode.Text = "工作状态：连接平台服务器成功，将工作在联网模式";
                                }
                                else if (msg.Contains("强制登录"))
                                {
                                    if (MessageBox.Show(msg + "\r\n是否进行强制登录？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                                    {
                                        Hashtable ht2 = new Hashtable();
                                        ht2.Add("jczbh", hnhywebinf.stationid);
                                        ht2.Add("jcxbh", hnhywebinf.lineid);
                                        ht2.Add("dlzh", nowUser.userName);
                                        ht2.Add("dlkl", hninterface.md5key);
                                        ht2.Add("qzdlsqm", hninterface.autoriseForceKey);
                                        if (hninterface.loginInForce(ht2, out code, out msg))
                                        {
                                            string datetimestring = "";
                                            if (hninterface.queryTime(out datetimestring, out code, out msg))
                                            {
                                                string[] datetimearray = DateTime.Parse(datetimestring).ToString("yyyy-MM-dd HH:mm:ss.fff").Split(' ');
                                                string datetimereceive = datetimearray[0] + " " + datetimearray[1];
                                                SetSystemDateTime.SetLocalTimeByStr(datetimereceive);
                                                ini.INIIO.saveLogInf("同步时间成功");
                                            }
                                            else
                                            {
                                                ini.INIIO.saveLogInf("同步时间失败,code" + code + ",msg:" + msg);
                                            }
                                            labelWorkMode.Text = "工作状态：连接平台服务器成功，将工作在联网模式";
                                        }
                                        else
                                        {
                                            MessageBox.Show("从业人员强制登录失败，code:" + code + "\r\nmessage:" + msg);
                                            isNetUsed = false;
                                            labelWorkMode.Text = "工作状态：登录平台失败，将工作在单机模式";
                                            ini.INIIO.saveLogInf("工作状态：登录平台失败，将工作在单机模式");
                                        }
                                    }
                                    else
                                    {
                                        isNetUsed = false;
                                        labelWorkMode.Text = "工作状态：登录平台失败，将工作在单机模式";
                                        ini.INIIO.saveLogInf("工作状态：登录平台失败，将工作在单机模式");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("从业人员登录失败，code:" + code + "\r\nmessage:" + msg);
                                    isNetUsed = false;
                                    labelWorkMode.Text = "工作状态：登录平台失败，将工作在单机模式";
                                    ini.INIIO.saveLogInf("工作状态：登录平台失败，将工作在单机模式");
                                }
                            }

                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("连接到联网服务器失败", er.Message);
                            isNetUsed = false;
                            labelWorkMode.Text = "工作状态：连接联网服务器失败，将工作在单机模式";
                            ini.INIIO.saveLogInf("工作状态：连接联网服务器失败，将工作在单机模式");
                        }
                    }
                    else if(mainPanel.netMode==mainPanel.ORTNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        try
                        {
                            ortsocket = new ortSocketControl();
                            ortsocket.initSocket(ortsocketinf.IP, ortsocketinf.PORT);
                            string socketMsg = ortsocket.init_equipment();
                            if (socketMsg == "连接成功")
                            {
                                ortsocket.close_socket();
                                isNetUsed = true;
                                string msg = "";
                                msg += "工作状态：连接欧润特联网服务器成功";
                                isNetUsed = true;
                                string result, err, datetimeSyc, version;
                                if (ortsocket.SendConnectionTest(stationid, stationinfmodel.StationCompany+"01", out result, out err, out version, out datetimeSyc))
                                {
                                    if (result == "TRUE")
                                    {
                                        msg += ",获取版本号成功：" + version;                                        
                                    }
                                    else
                                    {
                                        msg += ",获取版本号失败：" + err;
                                    }
                                }
                                else
                                {
                                    msg += ",获取版本号失败：" + err;
                                }
                                if (ortsocket.SendGetDatatime(out result, out err, out datetimeSyc))
                                {
                                    if (result == "TRUE")
                                    {
                                        string[] datetimearray = DateTime.Parse(datetimeSyc).ToString("yyyy-MM-dd HH:mm:ss.fff").Split(' ');
                                        string datetimereceive = datetimearray[0] + " " + datetimearray[1];
                                        SetSystemDateTime.SetLocalTimeByStr(datetimereceive);
                                        msg += ",获取服务器同步时间成功：" + datetimeSyc;
                                    }
                                    else
                                    {
                                        msg += ",获取服务器同步时间失败：" + err;
                                    }
                                }
                                else
                                {
                                    msg += ",获取服务器同步时间失败：" + err;
                                }
                                ini.INIIO.saveLogInf(msg);
                                labelWorkMode.Text = "工作状态：连接欧润特联网服务器成功，工作在联网模式";
                                ini.INIIO.saveLogInf("工作状态：连接欧润特联网服务器成功，工作在联网模式");
                            }
                            else
                            {
                                isNetUsed = false;
                                labelWorkMode.Text = "工作状态：连接欧润特联网服务器失败,将工作在单机模式";
                                ini.INIIO.saveLogInf("工作状态：连接欧润特联网服务器失败,将工作在单机模式");
                            }


                        }
                        catch(Exception er)
                        {
                            isNetUsed = false;
                            labelWorkMode.Text = "工作状态：连接江西联网服务器失败,将工作在单机模式";
                            ini.INIIO.saveLogInf("工作状态：连接江西联网服务器发生异常,将工作在单机模式，异常信息;"+er.Message);
                        }
                    }
                    else if (mainPanel.netMode == mainPanel.EZNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        isNetUsed = true;
                        ezinterface = new Ezclient("");
                        labelWorkMode.Text = "工作状态：工作在鄂州联网模式";
                        ini.INIIO.saveLogInf("工作状态：工作在鄂州联网模式");
                    }
                    else if (mainPanel.netMode == mainPanel.HHZNNETMODE)
                    {
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "Y", @".\appConfig.ini");
                        isNetUsed = true;
                        hhzinterface = new Hhzclient(hhzwebinf.weburl);
                        labelWorkMode.Text = "工作状态：工作在红河州联网模式";
                        ini.INIIO.saveLogInf("工作状态：工作在红河州联网模式");
                    }
                    else
                    {
                        labelWorkMode.Text = "工作状态：单机工作模式";
                        ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "N", @".\appConfig.ini");
                    }
                }
                else
                {
                    labelWorkMode.Text = "工作状态：单机工作模式";
                    ini.INIIO.WritePrivateProfileString("工作模式", "联网运行", "N", @".\appConfig.ini");

                }
                labelUserName.Text = nowUser.userName;
                disable_function(nowUser.postID);

                worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                worklogdata.ProjectName = "操作日志";
                worklogdata.Stationid = mainPanel.stationid;
                worklogdata.Lineid = "00";
                worklogdata.Czy = mainPanel.nowUser.userName;
                worklogdata.Data = "登录员" + nowUser.userName + "登录";
                worklogdata.State = "成功";
                worklogdata.Result = "";
                worklogdata.Date = DateTime.Now;
                worklogdata.Bzsm = "";
                demarcatecontrol.saveWordLogByIni(worklogdata);
            }
            else
            {
                labelUserName.Text ="未登录";
                disable_function("0");
                worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                worklogdata.ProjectName = "操作日志";
                worklogdata.Stationid = mainPanel.stationid;
                worklogdata.Lineid = "00";
                worklogdata.Czy = mainPanel.nowUser.userName;
                worklogdata.Data = "登录员" + nowUser.userName + "登录";
                worklogdata.State = "失败";
                worklogdata.Result = "";
                worklogdata.Date = DateTime.Now;
                worklogdata.Bzsm = "";
                demarcatecontrol.saveWordLogByIni(worklogdata);
            }
            timer1.Start();
            
        }
        private void disable_function(string quanxian)
        {
            if (quanxian != "0")
            {
                postModel postmodel = logininfcontrol.getPostQx(quanxian);
                buttonItemLoginCar.Enabled = (postmodel.LOGINQX == "1") ? true : false;
                buttonItemPrint.Enabled = (postmodel.PRINTQX == "1") ? true : false;
                buttonItemStastic.Enabled = (postmodel.CHECKQX == "1") ? true : false;
                buttonItemSettings.Enabled = (postmodel.SETTINGSQX == "1") ? true : false;
                buttonRegister.Enabled = (postmodel.CHECKQX == "1") ? true : false;
                buttonItemPersonelInf.Enabled = true;
            }
            else
            {
                buttonItemLoginCar.Enabled = false;
                buttonItemPrint.Enabled = false;
                buttonItemStastic.Enabled = false;
                buttonItemSettings.Enabled = false;
                buttonRegister.Enabled = false;
                buttonItemPersonelInf.Enabled = false;
            }
            
        }
        private void disable_button(Button button,string pictureName)
        {
            //Image image=Image.FromFile("./png/"+pictureName+"2h.png");
            //button.ForeColor=Color.Gray;
            button.Enabled = false;
            //button.BackgroundImage = image;
        }
        private void enable_button(Button button, string pictureName)
        {
            //Image image = Image.FromFile("./png/" + pictureName + "2.png");
            //button.ForeColor = Color.RoyalBlue;
            button.Enabled = true;
            //button.BackgroundImage = image;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString();
            refreshCount++;
            if (refreshCount >= 300)
            {
                refreshCount = 0;
                carDetectedCount=logininfcontrol.getStationLineDetectedCount(stationid, "0", "3");
                carAtWaitCount = logininfcontrol.getStationAtWaitCount(stationid);
                labelCarDetectedCount.Text ="今日已检车辆：" +carDetectedCount.ToString();
                labelCarAtWaitCount.Text = "待检车辆：" + carAtWaitCount.ToString();
            }
            if (startAutoPrint)
                labelAutoPrint.Text = "自动打印已开启";
            else
                labelAutoPrint.Text = "自动打印已关闭";
        }

        private void buttonReLogin_Click(object sender, EventArgs e)
        {
            staffLogin stafflogin = new staffLogin();
            stafflogin.ShowDialog();
            if (userLoginSuccess)
            {
                labelUserName.Text = nowUser.userName;
                disable_function(nowUser.postID);
                worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                worklogdata.ProjectName = "操作日志";
                worklogdata.Stationid = mainPanel.stationid;
                worklogdata.Lineid = "00";
                worklogdata.Czy = mainPanel.nowUser.userName;
                worklogdata.Data = "登录员" + nowUser.userName + "登录";
                worklogdata.State = "成功";
                worklogdata.Result = "";
                worklogdata.Date = DateTime.Now;
                worklogdata.Bzsm = "";
                demarcatecontrol.saveWordLogByIni(worklogdata);
            }
            else
            {
                labelUserName.Text = "未登录";
                disable_function("0");
                worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
                worklogdata.ProjectName = "操作日志";
                worklogdata.Stationid = mainPanel.stationid;
                worklogdata.Lineid = "00";
                worklogdata.Czy = mainPanel.nowUser.userName;
                worklogdata.Data = "登录员" + nowUser.userName + "登录";
                worklogdata.State = "失败";
                worklogdata.Result = "";
                worklogdata.Date = DateTime.Now;
                worklogdata.Bzsm = "";
                demarcatecontrol.saveWordLogByIni(worklogdata);
            }
        }

        private void buttonSysSettings_Click(object sender, EventArgs e)
        {

            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "系统设置")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            systemConfig childcarlogin = new systemConfig();
            TabItem childtabitem = this.tabControl1.CreateTab("系统设置");
            childtabitem.Name = "系统设置";
            childcarlogin.TopLevel = false;
            childtabitem.AttachedControl.Controls.Add(childcarlogin);
            childcarlogin.FormBorderStyle = FormBorderStyle.None;
            childcarlogin.Dock = DockStyle.Fill;
            childcarlogin.Show();
            tabControl1.SelectedTab = childtabitem;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "报表打印")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            printer childcarlogin = new printer();
            TabItem childtabitem = this.tabControl1.CreateTab("报表打印");
            childtabitem.Name = "报表打印";
            childcarlogin.TopLevel = false;
            childtabitem.AttachedControl.Controls.Add(childcarlogin);
            childcarlogin.FormBorderStyle = FormBorderStyle.None;
            childcarlogin.Dock = DockStyle.Fill;
            childcarlogin.Show();
            tabControl1.SelectedTab = childtabitem;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "人员管理")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            registerPersonel childcarlogin = new registerPersonel();
            TabItem childtabitem = this.tabControl1.CreateTab("人员管理");
            childtabitem.Name = "人员管理";
            childcarlogin.TopLevel = false;
            childtabitem.AttachedControl.Controls.Add(childcarlogin);
            childcarlogin.FormBorderStyle = FormBorderStyle.None;
            childcarlogin.Dock = DockStyle.Fill;
            childcarlogin.Show();
            tabControl1.SelectedTab = childtabitem;
        }

        public bool ping(string ip)
        {
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
            options.DontFragment = true;
            string data = "Test data!";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 1000;
            System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
            if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else
                return false;
        }

        private void mainPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isNetUsed)
            {
                if (mainPanel.netMode == mainPanel.HNNETMODE)
                {
                    string code, msg;
                    if (!mainPanel.hninterface.loginOut(out code, out msg))
                    {
                        MessageBox.Show("发送注销命令失败\r\ncode:" + code + "\r\nmsg:" + msg);
                        ini.INIIO.saveLogInf("发送注销命令失败,code" + code + ",msg:" + msg);
                    }
                }
                if(mainPanel.netMode==mainPanel.ORTNETMODE)
                {
                    //mainPanel.ortsocket.close_socket();
                }
            }

            mainPanel.worklogdata.ProjectID = mainPanel.stationid + "00" + DateTime.Now.ToString("yyMMddHHmmss");//线号“00”代表为登录机进行的操作
            mainPanel.worklogdata.ProjectName = "操作日志";
            mainPanel.worklogdata.Stationid = mainPanel.stationid;
            mainPanel.worklogdata.Lineid = "00";
            mainPanel.worklogdata.Czy = mainPanel.nowUser.userName;
            mainPanel.worklogdata.Data = "退出系统";
            mainPanel.worklogdata.State = "成功";
            mainPanel.worklogdata.Result = "";
            mainPanel.worklogdata.Date = DateTime.Now;
            mainPanel.worklogdata.Bzsm = "";
            mainPanel.demarcatecontrol.saveWordLogByIni(mainPanel.worklogdata);
        }
        int printTimerCount = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (startAutoPrint)
            {
                labelAutoPrint.Text = "已开启";
                /*if (printTimerCount++ >= autoPrintScanSeconds)
                {
                    printTimerCount = 0;
                    DataTable dt = mainPanel.logininfcontrol.getCarDetectedHasNotPrint(autoPrintStarttime, autoPrintEndtime, "0", "0", "0");
                    DataRow dr = null;
                    if (dt != null)
                    {
                        int lengthnow = richTextBoxPrintMsg.Text.Length;
                        string info = "扫描到共有" + dt.Rows.Count.ToString() + "辆车等待打印";
                        richTextBoxPrintMsg.AppendText(info + "\r\n");
                        richTextBoxPrintMsg.Select(lengthnow, lengthnow + info.Length);
                        richTextBoxPrintMsg.SelectionColor = System.Drawing.Color.Black;
                        foreach (DataRow dR in dt.Rows)
                        {
                            string msg = "";
                            if (printReportByClid(dR["CLID"].ToString(), out msg))
                            {
                                lengthnow = richTextBoxPrintMsg.Text.Length;
                                msg = dR["CLHP"].ToString() + "打印完成";
                                richTextBoxPrintMsg.AppendText(msg + "\r\n");
                                richTextBoxPrintMsg.Select(lengthnow, lengthnow + msg.Length);
                                richTextBoxPrintMsg.SelectionColor = System.Drawing.Color.Lime;

                            }
                            else
                            {
                                lengthnow = richTextBoxPrintMsg.Text.Length;
                                msg = dR["CLHP"].ToString() + "打印失败：" + msg;
                                richTextBoxPrintMsg.AppendText(msg + "\r\n");
                                richTextBoxPrintMsg.Select(lengthnow, lengthnow + msg.Length);
                                richTextBoxPrintMsg.SelectionColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    else
                    {
                        int lengthnow = richTextBoxPrintMsg.Text.Length;
                        string info = "没有车辆等待打印";
                        richTextBoxPrintMsg.AppendText(info + "\r\n");
                        richTextBoxPrintMsg.Select(lengthnow, lengthnow + info.Length);
                        richTextBoxPrintMsg.SelectionColor = System.Drawing.Color.Black;
                    }
                }*/
            }
            else
            {
                labelAutoPrint.Text = "未开启";
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DevComponents.DotNetBar.TabItem tabitem in tabControl1.Tabs)
            {
                if (tabitem.Name == "自动打印")
                {
                    tabControl1.SelectedTab = tabitem;
                    return;
                }
            }
            autoPrint childcarlogin = new autoPrint();
            TabItem childtabitem = this.tabControl1.CreateTab("自动打印");
            childtabitem.Name = "自动打印";
            childcarlogin.TopLevel = false;
            childtabitem.AttachedControl.Controls.Add(childcarlogin);
            childcarlogin.FormBorderStyle = FormBorderStyle.None;
            childcarlogin.Dock = DockStyle.Fill;
            childcarlogin.Show();
            tabControl1.SelectedTab = childtabitem;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            richTextBoxPrintMsg.Clear();

        }




        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.tabControl1.SelectedTab.Name=="自动打印")
            {
                startAutoPrint = false;
            }
            this.tabControl1.Tabs.Remove(this.tabControl1.SelectedTab);

        }
    }
}
