using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace carinfor
{
    public class zyjsBtgdata
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
        private string dszs;

        public string Dszs
        {
            get { return dszs; }
            set { dszs = value; }
        }
        private string firstData;

        public string FirstData
        {
            get { return firstData; }
            set { firstData = value; }
        }
        private string secondData;

        public string SecondData
        {
            get { return secondData; }
            set { secondData = value; }
        }
        private string thirdData;

        public string ThirdData
        {
            get { return thirdData; }
            set { thirdData = value; }
        }
        
    }
    public class zyjsBtgdataControl
    {
        public bool writeJzjsData(zyjsBtgdata jzjs_data)
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
                ini.INIIO.WritePrivateProfileString("检测结果", "数值1", jzjs_data.Dszs, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值2", jzjs_data.FirstData, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值3", jzjs_data.SecondData, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值4", jzjs_data.ThirdData, "C:/jcdatatxt/" + jzjs_data.CarID + ".ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public zyjsBtgdata readZyjsData(string filePath)
        {
            zyjsBtgdata zyjs_data = new zyjsBtgdata();
            try
            {
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                if (File.Exists(filePath))
                {
                    ini.INIIO.GetPrivateProfileString("检测结果", "车辆ID", "", temp, 2048, filePath);
                    zyjs_data.CarID = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "相对湿度", "", temp, 2048, filePath);
                    zyjs_data.Sd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "环境温度", "", temp, 2048, filePath);
                    zyjs_data.Wd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "大气压力", "", temp, 2048, filePath);
                    zyjs_data.Dqy = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值1", "", temp, 2048, filePath);
                    zyjs_data.Dszs = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值2", "", temp, 2048, filePath);
                    zyjs_data.FirstData = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值3", "", temp, 2048, filePath);
                    zyjs_data.SecondData = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值4", "", temp, 2048, filePath);
                    zyjs_data.ThirdData = temp.ToString();
                }
                else
                {
                    zyjs_data.CarID = "-1";
                }
                return zyjs_data;
            }
            catch
            {
                zyjs_data.CarID = "-1";
                return zyjs_data;
            }
        }
    }
}
