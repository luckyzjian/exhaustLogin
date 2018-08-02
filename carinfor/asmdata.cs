using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace carinfor
{
    public class asmdata
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
    public class asmdataControl
    {
        public bool writeAsmData(asmdata asm_data)
        {
            try
            {
                if (File.Exists("C:/jcdatatxt/" + asm_data.CarID + ".ini"))
                {
                    File.Delete("C:/jcdatatxt/" + asm_data.CarID + ".ini");
                }
                ini.INIIO.WritePrivateProfileString("检测结果", "车辆ID", asm_data.CarID, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "相对湿度", asm_data.Sd, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "环境温度", asm_data.Wd, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "大气压力", asm_data.Dqy, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值1", asm_data.Cozl, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值2", asm_data.Hczl, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "数值3", asm_data.Noxzl, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                ini.INIIO.WritePrivateProfileString("检测结果", "累计超差", asm_data.Ljcc, "C:/jcdatatxt/" + asm_data.CarID + ".ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public asmdata readAsmData(string filePath)
        {
            asmdata asm_data = new asmdata();
            try
            {
                StringBuilder temp = new StringBuilder();
                temp.Length = 2048;
                if (File.Exists(filePath))
                {
                    ini.INIIO.GetPrivateProfileString("检测结果", "车辆ID", "", temp, 2048, filePath);
                    asm_data.CarID = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "相对湿度", "", temp, 2048, filePath);
                    asm_data.Sd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "环境温度", "", temp, 2048, filePath);
                    asm_data.Wd = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "大气压力", "", temp, 2048, filePath);
                    asm_data.Dqy = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值1", "", temp, 2048, filePath);
                    asm_data.Cozl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值2", "", temp, 2048, filePath);
                    asm_data.Hczl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "数值3", "", temp, 2048, filePath);
                    asm_data.Noxzl = temp.ToString();
                    ini.INIIO.GetPrivateProfileString("检测结果", "累计超差", "", temp, 2048, filePath);
                    asm_data.Ljcc = temp.ToString();
                }
                else
                {
                    asm_data.CarID = "-1";
                }
                return asm_data;
            }
            catch
            {
                asm_data.CarID = "-1";
                return asm_data;
            }
        }
    }
}
