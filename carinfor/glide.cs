using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace carinfor
{
    public class glide
    {
        private string hxqj;

        public string Hxqj
        {
            get { return hxqj; }
            set { hxqj = value; }
        }
        private string qjmysd;

        public string Qjmysd
        {
            get { return qjmysd; }
            set { qjmysd = value; }
        }
        private string plhp;

        public string Plhp
        {
            get { return plhp; }
            set { plhp = value; }
        }
        private string ccdt;

        public string Ccdt
        {
            get { return ccdt; }
            set { ccdt = value; }
        }
        private string acdt;

        public string Acdt
        {
            get { return acdt; }
            set { acdt = value; }
        }
        private string wc;

        public string Wc
        {
            get { return wc; }
            set { wc = value; }
        }
        private string jzsdgl;

        public string Jzsdgl
        {
            get { return jzsdgl; }
            set { jzsdgl = value; }
        }
        private string bzsm;

        public string Bzsm
        {
            get { return bzsm; }
            set { bzsm = value; }
        }
        private string bdjg;

        public string Bdjg
        {
            get { return bdjg; }
            set { bdjg = value; }
        }
    }
    public class glideControl
    {
        public bool writeGlideIni(glide glidedata)
        {
            try
            {
                if (File.Exists("C:/jcdatatxt/Glide.ini"))
                {
                    File.Delete("C:/jcdatatxt/Glide.ini");
                }
                //configInfdata preConfigData = getConfigIni();
                ini.INIIO.WritePrivateProfileString("标定数据", "滑行区间", glidedata.Hxqj, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "区间名义速度", glidedata.Qjmysd, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "PLHP", glidedata.Plhp, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "CCDT", glidedata.Ccdt, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "ACDT", glidedata.Acdt, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "误差", glidedata.Wc, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "加载设定功率", glidedata.Jzsdgl, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "备注说明", glidedata.Bzsm, "C:/jcdatatxt/Glide.ini");
                ini.INIIO.WritePrivateProfileString("标定数据", "标定结果", glidedata.Bdjg, "C:/jcdatatxt/Glide.ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public glide readGlideData(string filePath)
        {
            glide vmas_data = new glide();
            try
            {
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                if (File.Exists(filePath))
                {
                    ini.INIIO.GetPrivateProfileString("标定数据", "滑行区间", "", temp, 2048, filePath);//、
                    vmas_data.Hxqj = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "区间名义速度", "", temp, 2048, filePath);
                    vmas_data.Qjmysd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "PLHP", "", temp, 2048, filePath);
                    vmas_data.Plhp = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "CCDT", "", temp, 2048, filePath);
                    vmas_data.Ccdt = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "ACDT", "", temp, 2048, filePath);
                    vmas_data.Acdt = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "误差", "", temp, 2048, filePath);
                    vmas_data.Wc = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "加载设定功率", "", temp, 2048, filePath);
                    vmas_data.Jzsdgl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "备注说明", "", temp, 2048, filePath);
                    vmas_data.Bzsm = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("标定数据", "标定结果", "", temp, 2048, filePath);
                    vmas_data.Bdjg = temp.ToString();
                }
                else
                {
                    vmas_data.Bdjg = "-1";
                }
                return vmas_data;
            }
            catch
            {
                vmas_data.Bdjg = "-1";
                return vmas_data;
            }
        }
    }
}
