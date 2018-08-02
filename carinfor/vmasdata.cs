using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace carinfor
{
    public class vmasdata
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
        private string cozl;

        public string Cozl
        {
            get { return cozl; }
            set { cozl = value; }
        }
        private string hczl;

        public string Hczl
        {
            get { return hczl; }
            set { hczl = value; }
        }
        private string noxzl;

        public string Noxzl
        {
            get { return noxzl; }
            set { noxzl = value; }
        }
        private string ljcc;

        public string Ljcc
        {
            get { return ljcc; }
            set { ljcc = value; }
        }
    }
    public class vmasdataControl
    {
        public bool writeVmasData(vmasdata vmas_data)
        {
            try
            {
                if (File.Exists("C:/jcdatatxt/" + vmas_data.CarID + ".ini"))
                {
                    File.Delete("C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                }
                ini.INIIO.WritePrivateProfileString("检测结果", "车辆ID", vmas_data.CarID, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "相对湿度", vmas_data.Sd, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "环境温度", vmas_data.Wd, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "大气压力", vmas_data.Dqy, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值1", vmas_data.Cozl, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值2", vmas_data.Hczl, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值3", vmas_data.Noxzl, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "累计超差", vmas_data.Ljcc, "C:/jcdatatxt/" + vmas_data.CarID + ".ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public vmasdata readVmasData(string filePath)
        {
            vmasdata vmas_data = new vmasdata();
            try
            {
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                if (File.Exists(filePath))
                {
                    ini.INIIO.GetPrivateProfileString("检测结果", "车辆ID", "", temp, 2048, filePath);//、
                    vmas_data.CarID = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "相对湿度", "", temp, 2048, filePath);
                    vmas_data.Sd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "环境温度", "", temp, 2048, filePath);
                    vmas_data.Wd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "大气压力", "", temp, 2048, filePath);
                    vmas_data.Dqy = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值1", "", temp, 2048, filePath);
                    vmas_data.Cozl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值2", "", temp, 2048, filePath);
                    vmas_data.Hczl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值3", "", temp, 2048, filePath);
                    vmas_data.Noxzl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "累计超差", "", temp, 2048, filePath);
                    vmas_data.Ljcc = temp.ToString();
                }
                else
                {
                    vmas_data.CarID = "-1";
                }
                return vmas_data;
            }
            catch
            {
                vmas_data.CarID = "-1";
                return vmas_data;
            }
        }
    }
}
