using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace carinfor
{
    public class sdsdata
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
        private string λ;

        public string λ_value
        {
            get { return λ; }
            set { λ = value; }
        }
        private string co_low;

        public string Co_low
        {
            get { return co_low; }
            set { co_low = value; }
        }
        private string hc_low;

        public string Hc_low
        {
            get { return hc_low; }
            set { hc_low = value; }
        }
        private string co_high;

        public string Co_high
        {
            get { return co_high; }
            set { co_high = value; }
        }
        private string hc_high;

        public string Hc_high
        {
            get { return hc_high; }
            set { hc_high = value; }
        }
    }
    public class sdsdataControl
    {
        public bool writeSdsData(sdsdata sds_data)
        {

            try
            {
                if (File.Exists("C:/jcdatatxt/" + sds_data.CarID + ".ini"))
                {
                    File.Delete("C:/jcdatatxt/" + sds_data.CarID + ".ini");
                }
                ini.INIIO.WritePrivateProfileString("检测结果", "车辆ID", sds_data.CarID, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "相对湿度", sds_data.Sd, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "环境温度", sds_data.Wd, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "大气压力", sds_data.Dqy, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值1", sds_data.λ_value, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值2", sds_data.Co_low, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值3", sds_data.Hc_low, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值4", sds_data.Co_high, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值5", sds_data.Hc_high, "C:/jcdatatxt/" + sds_data.CarID + ".ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public sdsdata readSdsData(string filePath)
        {
            sdsdata sds_data = new sdsdata();
            try
            {
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                if (File.Exists(filePath))
                {
                    ini.INIIO.GetPrivateProfileString("检测结果", "车辆ID", "", temp, 2048, filePath);
                    sds_data.CarID = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "相对湿度", "", temp, 2048, filePath);
                    sds_data.Sd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "环境温度", "", temp, 2048, filePath);
                    sds_data.Wd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "大气压力", "", temp, 2048, filePath);
                    sds_data.Dqy = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值1", "", temp, 2048, filePath);
                    sds_data.λ_value = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值2", "", temp, 2048, filePath);
                    sds_data.Co_low = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值3", "", temp, 2048, filePath);
                    sds_data.Hc_low = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值4", "", temp, 2048, filePath);
                    sds_data.Co_high = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值5", "", temp, 2048, filePath);
                    sds_data.Hc_high = temp.ToString();
                }
                else
                {
                    sds_data.CarID = "-1";
                }
                return sds_data;
            }
            catch
            {
                sds_data.CarID = "-1";
                return sds_data;
            }
        }
    }
}
