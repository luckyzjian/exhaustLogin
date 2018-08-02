using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace carinfor
{
    public class configInfdata
    {
        private string jcxh;

        public string Jcxh
        {
            get { return jcxh; }
            set { jcxh = value; }
        }
        private string zb_mode;

        public string Zb_mode
        {
            get { return zb_mode; }
            set { zb_mode = value; }
        }
        private string ifSongPin;

        public string IfSongPin
        {
            get { return ifSongPin; }
            set { ifSongPin = value; }
        }

        private bool ifFqyTl;

        public bool IfFqyTl
        {
            get { return ifFqyTl; }
            set { ifFqyTl = value; }
        }
        private float ndz;

        public float Ndz
        {
            get { return ndz; }
            set { ndz = value; }
        }
        private float lljll;

        public float Lljll
        {
            get { return lljll; }
            set { lljll = value; }
        }
        private float wqll;

        public float Wqll
        {
            get { return wqll; }
            set { wqll = value; }
        }
        private float xsb;

        public float Xsb
        {
            get { return xsb; }
            set { xsb = value; }
        }
        private float gljz;

        public float Gljz
        {
            get { return gljz; }
            set { gljz = value; }
        }
        private string cgjck;

        public string Cgjck
        {
            get { return cgjck; }
            set { cgjck = value; }
        }
        private string fqyck;

        public string Fqyck
        {
            get { return fqyck; }
            set { fqyck = value; }
        }
        private string lljck;

        public string Lljck
        {
            get { return lljck; }
            set { lljck = value; }
        }
        private string ydjck;

        public string Ydjck
        {
            get { return ydjck; }
            set { ydjck = value; }
        }
        private float lxcc;

        private string ledck;

        public string Ledck
        {
            get { return ledck; }
            set { ledck = value; }
        }

        public float Lxcc
        {
            get { return lxcc; }
            set { lxcc = value; }
        }
        private float ljcc;

        public float Ljcc
        {
            get { return ljcc; }
            set { ljcc = value; }
        }
        private int cccs;

        public int Cccs
        {
            get { return cccs; }
            set { cccs = value; }
        }
        private int dssj;

        public int Dssj
        {
            get { return dssj; }
            set { dssj = value; }
        }


        private bool speedMonitor;

        public bool SpeedMonitor
        {
            get { return speedMonitor; }
            set { speedMonitor = value; }
        }
        private bool powerMonitor;

        public bool PowerMonitor
        {
            get { return powerMonitor; }
            set { powerMonitor = value; }
        }
        private bool concentrationMonitor;

        public bool ConcentrationMonitor
        {
            get { return concentrationMonitor; }
            set { concentrationMonitor = value; }
        }
        private bool flowMonitorr;

        public bool FlowMonitorr
        {
            get { return flowMonitorr; }
            set { flowMonitorr = value; }
        }
        private bool thinnerratioMonitor;

        public bool ThinnerratioMonitor
        {
            get { return thinnerratioMonitor; }
            set { thinnerratioMonitor = value; }
        }
        private bool huanjingo2Monitor;

        public bool Huanjingo2Monitor
        {
            get { return huanjingo2Monitor; }
            set { huanjingo2Monitor = value; }
        }
        private bool remainedMonitor;

        public bool RemainedMonitor
        {
            get { return remainedMonitor; }
            set { remainedMonitor = value; }
        }

        private bool cgjifpz;

        public bool Cgjifpz
        {
            get { return cgjifpz; }
            set { cgjifpz = value; }
        }
        private bool fqyifpz;

        public bool Fqyifpz
        {
            get { return fqyifpz; }
            set { fqyifpz = value; }
        }
        private bool lljifpz;

        public bool Lljifpz
        {
            get { return lljifpz; }
            set { lljifpz = value; }
        }
        private bool ydjifpz;

        public bool Ydjifpz
        {
            get { return ydjifpz; }
            set { ydjifpz = value; }
        }
        private bool ledifpz;

        public bool Ledifpz
        {
            get { return ledifpz; }
            set { ledifpz = value; }
        }
        private string cgjxh;

        public string Cgjxh
        {
            get { return cgjxh; }
            set { cgjxh = value; }
        }
        private string fqyxh;

        public string Fqyxh
        {
            get { return fqyxh; }
            set { fqyxh = value; }
        }
        private string lljxh;

        public string Lljxh
        {
            get { return lljxh; }
            set { lljxh = value; }
        }
        private string ydjxh;

        public string Ydjxh
        {
            get { return ydjxh; }
            set { ydjxh = value; }
        }

        private string[] powerSet;

        public string[] PowerSet
        {
            get { return powerSet; }
            set { powerSet = value; }
        }
        private int sdscc;

        public int Sdscc
        {
            get { return sdscc; }
            set { sdscc = value; }
        }

    }
    public class configIni
    {
        public configInfdata getConfigIni()
        {
            float a = 0;
            int b = 0;
            configInfdata configinidata = new configInfdata();
            StringBuilder temp = new StringBuilder();
            temp.Length = 2048;
            ini.INIIO.GetPrivateProfileString("检测线配置", "检测线号", "", temp, 2048, @".\Config.ini");
            configinidata.Jcxh = temp.ToString();
            ini.INIIO.GetPrivateProfileString("检测线配置", "是否送屏", "", temp, 2048, @".\Config.ini");
            configinidata.IfSongPin = temp.ToString();
            ini.INIIO.GetPrivateProfileString("工作配置", "工作模式", "", temp, 2048, @".\Config.ini");
            configinidata.Zb_mode = temp.ToString();
            ini.INIIO.GetPrivateProfileString("配置参数", "是否调零", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.IfFqyTl = true;
            else
                configinidata.IfFqyTl = false;
            ini.INIIO.GetPrivateProfileString("配置参数", "浓度值", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Ndz = a;
            else
                configinidata.Ndz = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "流量计流量值", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Lljll = a;
            else
                configinidata.Lljll = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "尾气流量值", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Wqll = a;
            else
                configinidata.Wqll = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "稀释比", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Xsb = a;
            else
                configinidata.Xsb = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "功率加载", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Gljz = a;
            else
                configinidata.Gljz = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "测功机串口", "", temp, 2048, @".\Config.ini");
            configinidata.Cgjck = temp.ToString().Trim().Split('/')[0];
            configinidata.Cgjxh = temp.ToString().Trim().Split('/')[1];
            ini.INIIO.GetPrivateProfileString("配置参数", "废气仪串口", "", temp, 2048, @".\Config.ini");
            configinidata.Fqyck = temp.ToString().Trim().Split('/')[0];
            configinidata.Fqyxh = temp.ToString().Trim().Split('/')[1];
            ini.INIIO.GetPrivateProfileString("配置参数", "流量计串口", "", temp, 2048, @".\Config.ini");
            configinidata.Lljck = temp.ToString().Trim().Split('/')[0];
            configinidata.Lljxh = temp.ToString().Trim().Split('/')[1];
            ini.INIIO.GetPrivateProfileString("配置参数", "烟度计串口", "", temp, 2048, @".\Config.ini");
            configinidata.Ydjck = temp.ToString().Trim().Split('/')[0];
            configinidata.Ydjxh = temp.ToString().Trim().Split('/')[1];
            ini.INIIO.GetPrivateProfileString("配置参数", "LED串口", "", temp, 2048, @".\Config.ini");
            configinidata.Ledck = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("配置参数", "连续超差", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Lxcc = a;
            else
                configinidata.Lxcc = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "累计超差", "", temp, 2048, @".\Config.ini");
            if (float.TryParse(temp.ToString().Trim(), out a))
                configinidata.Ljcc = a;
            else
                configinidata.Ljcc = 3f;
            ini.INIIO.GetPrivateProfileString("配置参数", "重测次数", "", temp, 2048, @".\Config.ini");
            if (int.TryParse(temp.ToString().Trim(), out b))
                configinidata.Cccs = b;
            else
                configinidata.Cccs = 3;
            ini.INIIO.GetPrivateProfileString("配置参数", "怠速时间", "", temp, 2048, @".\Config.ini");
            if (int.TryParse(temp.ToString().Trim(), out b))
                configinidata.Dssj = b;
            else
                configinidata.Dssj = 40;
            ini.INIIO.GetPrivateProfileString("配置参数", "双怠速超差", "", temp, 2048, @".\Config.ini");
            if (int.TryParse(temp.ToString().Trim(), out b))
                configinidata.Sdscc = b;
            else
                configinidata.Sdscc = 100;

            ini.INIIO.GetPrivateProfileString("监控项目", "速度监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim()=="true")
                configinidata.SpeedMonitor = true;
            else
                configinidata.SpeedMonitor = false;
            ini.INIIO.GetPrivateProfileString("监控项目", "加载功率监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.PowerMonitor = true;
            else
                configinidata.PowerMonitor = false;
            ini.INIIO.GetPrivateProfileString("监控项目", "浓度监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.ConcentrationMonitor = true;
            else
                configinidata.ConcentrationMonitor = false;
            ini.INIIO.GetPrivateProfileString("监控项目", "流量监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.FlowMonitorr = true;
            else
                configinidata.FlowMonitorr = false;
            ini.INIIO.GetPrivateProfileString("监控项目", "稀释比监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.ThinnerratioMonitor = true;
            else
                configinidata.ThinnerratioMonitor = false;
            ini.INIIO.GetPrivateProfileString("监控项目", "环境氧监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.Huanjingo2Monitor = true;
            else
                configinidata.Huanjingo2Monitor = false;
            ini.INIIO.GetPrivateProfileString("监控项目", "残余量监测", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.RemainedMonitor = true;
            else
                configinidata.RemainedMonitor = false;

            ini.INIIO.GetPrivateProfileString("仪器配置", "测功机", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.Cgjifpz = true;
            else
                configinidata.Cgjifpz = false;
            ini.INIIO.GetPrivateProfileString("仪器配置", "废气仪", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.Fqyifpz = true;
            else
                configinidata.Fqyifpz = false;
            ini.INIIO.GetPrivateProfileString("仪器配置", "流量计", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.Lljifpz = true;
            else
                configinidata.Lljifpz = false;
            ini.INIIO.GetPrivateProfileString("仪器配置", "烟度计", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.Ydjifpz = true;
            else
                configinidata.Ydjifpz = false;
            ini.INIIO.GetPrivateProfileString("仪器配置", "LED屏", "", temp, 2048, @".\Config.ini");
            if (temp.ToString().Trim() == "true")
                configinidata.Ledifpz = true;
            else
                configinidata.Ledifpz = false;

            ini.INIIO.GetPrivateProfileString("吸收功率", "功率", "", temp, 2048, @".\Config.ini");
            configinidata.PowerSet = temp.ToString().Trim().Split(',');
            return configinidata;
        }

        public bool writeConfigIni(configInfdata configinidata)
        {
            try
            {
                //configInfdata preConfigData = getConfigIni();
                ini.INIIO.WritePrivateProfileString("配置参数", "浓度值", configinidata.Ndz.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "流量计流量值", configinidata.Lljll.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "尾气流量值", configinidata.Wqll.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "稀释比", configinidata.Xsb.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "功率加载", configinidata.Gljz.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "测功机串口", configinidata.Cgjck + "/" + configinidata.Cgjxh, @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "废气仪串口", configinidata.Fqyck + "/" + configinidata.Fqyxh, @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "流量计串口", configinidata.Lljck + "/" + configinidata.Lljxh, @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "烟度计串口", configinidata.Ydjck + "/" + configinidata.Ydjxh, @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "LED串口", configinidata.Ledck.ToString(), @".\Config.ini"); 
                ini.INIIO.WritePrivateProfileString("配置参数", "连续超差", configinidata.Lxcc.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "累计超差", configinidata.Ljcc.ToString("0.0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "重测次数", configinidata.Cccs.ToString("0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("配置参数", "怠速时间", configinidata.Dssj.ToString("0"), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "速度监测", configinidata.SpeedMonitor.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "加载功率监测", configinidata.PowerMonitor.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "浓度监测", configinidata.ConcentrationMonitor.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "流量监测", configinidata.FlowMonitorr.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "稀释比监测", configinidata.ThinnerratioMonitor.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "环境氧监测", configinidata.Huanjingo2Monitor.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("监控项目", "残余量监测", configinidata.RemainedMonitor.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("仪器配置", "测功机", configinidata.Cgjifpz.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("仪器配置", "废气仪", configinidata.Fqyifpz.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("仪器配置", "流量计", configinidata.Lljifpz.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("仪器配置", "烟度计", configinidata.Ydjifpz.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("仪器配置", "LED屏", configinidata.Ledifpz.ToString().ToLower(), @".\Config.ini");
                ini.INIIO.WritePrivateProfileString("吸收功率", "功率", configinidata.PowerSet[0] + "," + configinidata.PowerSet[1] + "," + configinidata.PowerSet[2] + "," + configinidata.PowerSet[3] + "," +
                configinidata.PowerSet[4] + "," + configinidata.PowerSet[5] + "," + configinidata.PowerSet[6] + "," + configinidata.PowerSet[7] + "," + configinidata.PowerSet[8] + "," + configinidata.PowerSet[9] + "," + configinidata.PowerSet[10], @".\Config.ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
