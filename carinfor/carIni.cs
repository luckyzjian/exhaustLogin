using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ini;

namespace carinfor
{
    public class GAWebInf
    {
        public string weburl;
        public string xtlb;
        public string jkxlh;
        public string jyjgbh;
       
    }
    public class JXWebInf
    {
        public string user;	//webserive url
        public string password;
        public string lineid;
        public string url;
        public string socketip;
        public string socketport;
    }
    public class HNHYWebInf
    {
        public string weburl;	//webserive url
        public string stationid;	//检测站代码
        public string lineid;	//检测站代码
    }
    public class NHWebInf
    {
        public string weburl;	//webserive url
        public string lineid;
    }

    public class DALIWebInf
    {
        public string LINEID;
        public string SERVERIP;
        public string SERVERPORT;
    }

    public class EZWebInf
    {
        public string weburl;	//webserive url
        public string stationid;	//检测站代码
        public string lineid;	//检测站代码
    }
    public class HHZWebInf
    {
        public string weburl;	//webserive url
        public string queryUrl;
        public bool enableQuery;
    }
    public class carInidata
    {
        private string carID;//车辆ID

        public string CarID
        {
            get { return carID; }
            set { carID = value; }
        }
        private string carPH;//车辆牌号

        public string CarPH
        {
            get { return carPH; }
            set { carPH = value; }
        }
        private float carJzzl;//基准质量

        public float CarJzzl
        {
            get { return carJzzl; }
            set { carJzzl = value; }
        }
        private string carRlzl;//燃料类型

        public string CarRlzl
        {
            get { return carRlzl; }
            set { carRlzl = value; }
        }
        private float carEdgl;//额定功率

        public float CarEdgl
        {
            get { return carEdgl; }
            set { carEdgl = value; }
        }
        private float carEdzs;//额定转速

        public float CarEdzs
        {
            get { return carEdzs; }
            set { carEdzs = value; }
        }
        private string carBsxlx;//变速箱类型

        public string CarBsxlx
        {
            get { return carBsxlx; }
            set { carBsxlx = value; }
        }
        private float carLxcc;//连续超差时限

        public float CarLxcc
        {
            get { return carLxcc; }
            set { carLxcc = value; }
        }
        private float carLjcc;//累计超差时限

        public float CarLjcc
        {
            get { return carLjcc; }
            set { carLjcc = value; }
        }
        private float carNdz;//浓度限值

        public float CarNdz
        {
            get { return carNdz; }
            set { carNdz = value; }
        }
        private string carCc;//冲程

        public string CarCc
        {
            get { return carCc; }
            set { carCc = value; }
        }

    }
    public class carIni
    {
        public carInidata getCarIni()
        {
            float a=0;
            carInidata carinidata=new carInidata();
            StringBuilder temp = new StringBuilder();
            temp.Length = 2048;
            ini.INIIO.GetPrivateProfileString("检测信息", "车辆ID", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            carinidata.CarID = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("检测信息", "车辆牌照号", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            carinidata.CarPH = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("检测信息", "基准质量", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            if(float.TryParse(temp.ToString().Trim(),out a))
                carinidata.CarJzzl=a;
            else
                carinidata.CarJzzl=0;
            ini.INIIO.GetPrivateProfileString("检测信息", "燃料种类", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            carinidata.CarRlzl = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("检测信息", "额定功率", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            if(float.TryParse(temp.ToString().Trim(),out a))
                carinidata.CarEdgl=a;
            else
                carinidata.CarEdgl=0;
            ini.INIIO.GetPrivateProfileString("检测信息", "额定转速", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            if(float.TryParse(temp.ToString().Trim(),out a))
                carinidata.CarEdzs=a;
            else
                carinidata.CarEdzs=0;
            ini.INIIO.GetPrivateProfileString("检测信息", "变速箱", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            carinidata.CarBsxlx = temp.ToString().Trim();
            ini.INIIO.GetPrivateProfileString("检测信息", "连续超差", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            if(float.TryParse(temp.ToString().Trim(),out a))
                carinidata.CarLxcc=a;
            else
                carinidata.CarLxcc=0;
            ini.INIIO.GetPrivateProfileString("检测信息", "累计超差", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            if(float.TryParse(temp.ToString().Trim(),out a))
                carinidata.CarLjcc=a;
            else
                carinidata.CarLjcc=0;
            ini.INIIO.GetPrivateProfileString("检测信息", "浓度值", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            if(float.TryParse(temp.ToString().Trim(),out a))
                carinidata.CarNdz=a;
            else
                carinidata.CarNdz=0;
            ini.INIIO.GetPrivateProfileString("检测信息", "冲程", "", temp, 2048, @"C:\jcdatatxt\carinfo.ini");
            carinidata.CarCc = temp.ToString().Trim();
            return carinidata;
        }
        public bool writeCarIni(carInidata carinidata)
        {
            try
            {
                //configInfdata preConfigData = getConfigIni();
                ini.INIIO.WritePrivateProfileString("检测信息", "车辆ID", carinidata.CarID, @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "车辆牌照号", carinidata.CarPH, @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "基准质量", carinidata.CarJzzl.ToString("0"), @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "燃料种类", carinidata.CarRlzl, @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "额定功率", carinidata.CarEdgl.ToString("0"), @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "额定转速", carinidata.CarEdzs.ToString("0"), @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "变速箱", carinidata.CarBsxlx, @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "连续超差", carinidata.CarLxcc.ToString("0.0"), @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "累计超差", carinidata.CarLjcc.ToString("0.0"), @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "浓度值", carinidata.CarNdz.ToString("0.0"), @"C:\jcdatatxt\carinfo.ini");
                ini.INIIO.WritePrivateProfileString("检测信息", "冲程", carinidata.CarCc, @"C:\jcdatatxt\carinfo.ini");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
