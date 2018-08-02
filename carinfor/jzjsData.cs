using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace carinfor
{
    public class jzjsdata
    {
        private string carID;

        public string CarID
        {
            get { return carID; }
            set { carID = value; }
        }
        private string sd;

        public string Sd
        {
            get { return sd; }
            set { sd = value; }
        }
        private string wd;

        public string Wd
        {
            get { return wd; }
            set { wd = value; }
        }
        private string dqy;

        public string Dqy
        {
            get { return dqy; }
            set { dqy = value; }
        }
        private string gxsxs_100;

        public string Gxsxs_100
        {
            get { return gxsxs_100; }
            set { gxsxs_100 = value; }
        }
        private string gxsxs_90;

        public string Gxsxs_90
        {
            get { return gxsxs_90; }
            set { gxsxs_90 = value; }
        }
        private string gxsxs_80;

        public string Gxsxs_80
        {
            get { return gxsxs_80; }
            set { gxsxs_80 = value; }
        }
        private string lbgl;

        public string Lbgl
        {
            get { return lbgl; }
            set { lbgl = value; }
        }
    }
    public class jzjsdataControl
    {
        public bool writeJzjsData(jzjsdata jzjs_data)
        {
            
            try
            {
                if (File.Exists("C:/jcdatatxt/" + jzjs_data.CarID + ".ini"))
                {
                    File.Delete("C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                }
                ini.INIIO.WritePrivateProfileString("检测结果", "车辆ID", jzjs_data.CarID, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "相对湿度", jzjs_data.Sd, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "环境温度", jzjs_data.Wd, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "大气压力", jzjs_data.Dqy, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值1", jzjs_data.Gxsxs_100, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值2", jzjs_data.Gxsxs_90, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值3", jzjs_data.Gxsxs_80, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值4", jzjs_data.Lbgl, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public jzjsdata readJzjsData(string filePath)
        {
            jzjsdata jzjs_data = new jzjsdata();
            try
            {
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                if (File.Exists(filePath))
                {
                    ini.INIIO.GetPrivateProfileString("检测结果", "车辆ID", "", temp, 2048, filePath);
                    jzjs_data.CarID = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "相对湿度", "", temp, 2048, filePath);
                    jzjs_data.Sd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "环境温度", "", temp, 2048, filePath);
                    jzjs_data.Wd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "大气压力", "", temp, 2048, filePath);
                    jzjs_data.Dqy = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值1", "", temp, 2048, filePath);
                    jzjs_data.Gxsxs_100 = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值2", "", temp, 2048, filePath);
                    jzjs_data.Gxsxs_90 = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值3", "", temp, 2048, filePath);
                    jzjs_data.Gxsxs_80 = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值4", "", temp, 2048, filePath);
                    jzjs_data.Lbgl = temp.ToString();
                }
                else
                {
                    jzjs_data.CarID = "-1";
                }
                return jzjs_data;
            }
            catch
            {
                jzjs_data.CarID = "-1";
                return jzjs_data;
            }
        }
    }
    
}
