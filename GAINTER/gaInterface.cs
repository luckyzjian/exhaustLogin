using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Data;
using System.IO;
using ini;

namespace GAINTER
{
    public class gaInterface
    {
        public Dictionary<string, string> GA_USETYPE = new Dictionary<string, string>();
        public Dictionary<string, string> GA_FUELTYPE = new Dictionary<string, string>();
        public Dictionary<string, string> RGA_USETYPE = new Dictionary<string, string>();
        public Dictionary<string, string> RGA_FUELTYPE = new Dictionary<string, string>();
        string Xtlb = "";
        string Jkxlh = "";
        string Jyjgbh = "";
        GAWebReference.TmriJaxRpcOutAccessService gainterface = null;
        public gaInterface(string url,string xtlb,string jkxlh,string jyjgbh)
        {
            GA_USETYPE.Add("A", "非营运");
            GA_USETYPE.Add("B", "公路客运");
            GA_USETYPE.Add("C", "公交客运");
            GA_USETYPE.Add("D", "出租客运");
            GA_USETYPE.Add("E", "旅游客运");
            GA_USETYPE.Add("F", "货运");
            GA_USETYPE.Add("G", "租赁");
            GA_USETYPE.Add("H", "警用");
            GA_USETYPE.Add("I", "消防");
            GA_USETYPE.Add("J", "救护");
            GA_USETYPE.Add("K", "工程抢险");
            GA_USETYPE.Add("L", "营转非");
            GA_USETYPE.Add("M", "出租转非");
            GA_USETYPE.Add("Z", "其他");
            GA_FUELTYPE.Add("A", "汽油");
            GA_FUELTYPE.Add("B", "柴油");
            GA_FUELTYPE.Add("C", "电");
            GA_FUELTYPE.Add("D", "混合油");
            GA_FUELTYPE.Add("E", "天然气");
            GA_FUELTYPE.Add("F", "液化石油气");
            GA_FUELTYPE.Add("L", "甲醇");
            GA_FUELTYPE.Add("M", "乙醇");
            GA_FUELTYPE.Add("N", "太阳能");
            GA_FUELTYPE.Add("O", "混合动力");
            GA_FUELTYPE.Add("Y", "无");
            GA_FUELTYPE.Add("Z", "其他");
            RGA_USETYPE.Add("非营运", "A");
            RGA_USETYPE.Add("公路客运", "B");
            RGA_USETYPE.Add("公交客运", "C");
            RGA_USETYPE.Add("出租客运", "D");
            RGA_USETYPE.Add("旅游客运", "E");
            RGA_USETYPE.Add("货运", "F");
            RGA_USETYPE.Add("租赁", "G");
            RGA_USETYPE.Add("警用", "H");
            RGA_USETYPE.Add("消防", "I");
            RGA_USETYPE.Add("救护", "J");
            RGA_USETYPE.Add("工程抢险", "K");
            RGA_USETYPE.Add("营转非", "L");
            RGA_USETYPE.Add("出租转非", "M");
            RGA_USETYPE.Add("其他", "Z");
            RGA_FUELTYPE.Add("汽油", "A");
            RGA_FUELTYPE.Add("柴油", "B");
            RGA_FUELTYPE.Add("电", "C");
            RGA_FUELTYPE.Add("混合油", "D");
            RGA_FUELTYPE.Add("天然气", "E");
            RGA_FUELTYPE.Add("液化石油气", "F");
            RGA_FUELTYPE.Add("甲醇", "L");
            RGA_FUELTYPE.Add("乙醇", "M");
            RGA_FUELTYPE.Add("太阳能", "N");
            RGA_FUELTYPE.Add("混合动力", "O");
            RGA_FUELTYPE.Add("无", "Y");
            RGA_FUELTYPE.Add("其他", "Z");
            this.Xtlb = xtlb;
            this.Jkxlh = jkxlh;
            this.Jyjgbh = jyjgbh;
            gainterface = new GAWebReference.TmriJaxRpcOutAccessService();
            gainterface.Url = url;
        }
        public static String encodeUTF8(string xmlDoc)
        {
            Encoding utf8 = Encoding.UTF8;
            string str = "";
            try
            {
                str = HttpUtility.UrlEncode(xmlDoc, utf8);

            }
            catch
            { }
            return str;
        }
        /// <summary>  
        /// 将XmlDocument转化为string  
        /// </summary>  
        /// <param name="xmlDoc"></param>  
        /// <returns></returns>  
        static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            //xmlString = encodeUTF8(xmlString);
            sr.Close();
            stream.Close();
            string newstring = xmlString.Replace("xml version=\"1.0\"", "xml version=\"1.0\" encoding=\"GBK\"");//.Replace(" ", "%20").Replace("\r\n", "%0d%0a") + "\r\n";

            INIIO.saveSocketLogInf("SEND:\r\n" + newstring);
            return newstring;
        }
        /// <summary>  
        /// 将XmlDocument转化为string  
        /// </summary>  
        /// <param name="xmlDoc"></param>  
        /// <returns></returns>  
        static string ConvertXmlToStringNoEncoder(XmlDocument xmlDoc)
        {
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            //xmlString = encodeUTF8(xmlString);
            sr.Close();
            stream.Close();
            string newstring = xmlString.Replace("xml version=\"1.0\"", "xml version=\"1.0\" encoding=\"GBK\"");//.Replace(" ", "%20").Replace("\r\n", "%0d%0a") + "\r\n";

            INIIO.saveSocketLogInf("SEND:\r\n" + newstring);
            return newstring;
        }
        public DataTable GetVehicleInf(string hphm, string hpzl, string clsbdh, out string code, out string message)
        {
            //socket.Connect(point);
            XmlDocument xmldoc, xlmrecivedoc;
            XmlNode xmlnode;
            XmlElement xmlelem;
            DataTable dt = null;
            xmldoc = new XmlDocument();
            xmlelem = xmldoc.CreateElement("", "root", "");
            xmldoc.AppendChild(xmlelem);
            XmlNode root = xmldoc.SelectSingleNode("root");//查找<Employees> 
            XmlElement xe1 = xmldoc.CreateElement("QueryCondition");//创建一个<Node>节点      
            XmlElement xe2 = xmldoc.CreateElement("hphm");//创建一个<Node>节点 
            xe2.InnerText = hphm;
            XmlElement xe3 = xmldoc.CreateElement("hpzl");//创建一个<Node>节点 
            xe3.InnerText = hpzl;
            XmlElement xe4 = xmldoc.CreateElement("clsbdh");//创建一个<Node>节点 
            xe4.InnerText = clsbdh;
            XmlElement xe5 = xmldoc.CreateElement("jyjgbh");//创建一个<Node>节点  
            xe5.InnerText = Jyjgbh;
            xe1.AppendChild(xe2);
            xe1.AppendChild(xe3);
            xe1.AppendChild(xe4);
            xe1.AppendChild(xe5);
            root.AppendChild(xe1);
            INIIO.saveSocketLogInf("【读取车辆信息】"+"-【车牌号】="+hphm);
            INIIO.saveSocketLogInf("XTLB:" + Xtlb);
            INIIO.saveSocketLogInf("JKXLH:" + Jkxlh);
            INIIO.saveSocketLogInf("JKID:" + "18C49");
            string receiveXml = HttpUtility.UrlDecode(gainterface.queryObjectOut(Xtlb, Jkxlh, "18C49", ConvertXmlToStringNoEncoder(xmldoc)));
            INIIO.saveSocketLogInf("【返回】\r\n" + receiveXml);
            return ReadVehicleInfDatatable(receiveXml, out code, out message);
        }
        public DataTable ReadVehicleInfDatatable(string xmlstring, out string code, out string message)
        {
            DataSet ds = new DataSet();
            ds = XmlToData.CXmlToDataSet(xmlstring);
            code = ds.Tables[0].Rows[0]["code"].ToString();
            message = "";
            if (code != "1")
                message = ds.Tables[0].Rows[0]["message"].ToString();
            Console.Write(code + "\r\n");
            Console.Write(message + "\r\n");
            if (code == "1")
            {
                string newxmlstring = GetbodyInfo(xmlstring);
                return XmlToData.CXmlToDatatTable(newxmlstring);
            }
            else
            {
                return null;
            }
        }
        public string GetbodyInfo(string xmlinf)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(xmlinf);
            int iBegin = str.ToString().IndexOf(@"<body>");
            int iEnd = str.ToString().IndexOf(@"</body>");
            string temp = str.ToString().Substring(iBegin, iEnd - iBegin + 7);
            return temp;
        }
    }
    
}
