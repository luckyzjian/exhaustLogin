using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using ini;
using System.Data;
using System.IO;
namespace HhzWebClient
{
    public class HhzQuery
    {
        public Dictionary<string, string> ncdl_USETYPE = new Dictionary<string, string>();
        public Dictionary<string, string> ncdl_FUELTYPE = new Dictionary<string, string>();
        public Dictionary<string, string> ncdlR_USETYPE = new Dictionary<string, string>();
        public Dictionary<string, string> ncdlR_FUELTYPE = new Dictionary<string, string>();
        OutAccess outaccess = null;
        //GAWebReference.TmriJaxRpcOutAccessService gainterface = null;
        string Jyjgbh = "";
        public HhzQuery(string url, string jyjgbh)
        {
            ncdl_USETYPE.Add("A", "非营运");
            ncdl_USETYPE.Add("B", "公路客运");
            ncdl_USETYPE.Add("C", "公交客运");
            ncdl_USETYPE.Add("D", "出租客运");
            ncdl_USETYPE.Add("E", "旅游客运");
            ncdl_USETYPE.Add("F", "货运");
            ncdl_USETYPE.Add("G", "租赁");
            ncdl_USETYPE.Add("H", "警用");
            ncdl_USETYPE.Add("I", "消防");
            ncdl_USETYPE.Add("J", "救护");
            ncdl_USETYPE.Add("K", "工程抢险");
            ncdl_USETYPE.Add("L", "营转非");
            ncdl_USETYPE.Add("M", "出租转非");
            ncdl_USETYPE.Add("Z", "其他");
            ncdl_FUELTYPE.Add("A", "汽油");
            ncdl_FUELTYPE.Add("B", "柴油");
            ncdl_FUELTYPE.Add("C", "电");
            ncdl_FUELTYPE.Add("D", "混合油");
            ncdl_FUELTYPE.Add("E", "天然气");
            ncdl_FUELTYPE.Add("F", "液化石油气");
            ncdl_FUELTYPE.Add("L", "甲醇");
            ncdl_FUELTYPE.Add("M", "乙醇");
            ncdl_FUELTYPE.Add("N", "太阳能");
            ncdl_FUELTYPE.Add("O", "混合动力");
            ncdl_FUELTYPE.Add("Y", "无");
            ncdl_FUELTYPE.Add("Z", "其他");
            ncdlR_USETYPE.Add("非营运", "A");
            ncdlR_USETYPE.Add("公路客运", "B");
            ncdlR_USETYPE.Add("公交客运", "C");
            ncdlR_USETYPE.Add("出租客运", "D");
            ncdlR_USETYPE.Add("旅游客运", "E");
            ncdlR_USETYPE.Add("货运", "F");
            ncdlR_USETYPE.Add("租赁", "G");
            ncdlR_USETYPE.Add("警用", "H");
            ncdlR_USETYPE.Add("消防", "I");
            ncdlR_USETYPE.Add("救护", "J");
            ncdlR_USETYPE.Add("工程抢险", "K");
            ncdlR_USETYPE.Add("营转非", "L");
            ncdlR_USETYPE.Add("出租转非", "M");
            ncdlR_USETYPE.Add("其他", "Z");
            ncdlR_FUELTYPE.Add("汽油", "A");
            ncdlR_FUELTYPE.Add("柴油", "B");
            ncdlR_FUELTYPE.Add("电", "C");
            ncdlR_FUELTYPE.Add("混合油", "D");
            ncdlR_FUELTYPE.Add("天然气", "E");
            ncdlR_FUELTYPE.Add("液化石油气", "F");
            ncdlR_FUELTYPE.Add("甲醇", "L");
            ncdlR_FUELTYPE.Add("乙醇", "M");
            ncdlR_FUELTYPE.Add("太阳能", "N");
            ncdlR_FUELTYPE.Add("混合动力", "O");
            ncdlR_FUELTYPE.Add("无", "Y");
            ncdlR_FUELTYPE.Add("其他", "Z");
            this.Jyjgbh = jyjgbh;
            outaccess = new OutAccess(url);

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
            xe2.InnerText = HttpUtility.UrlEncode(hphm);
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
            INIIO.saveSocketLogInf("【读取车辆信息】" + "-【车牌号】=" + hphm);
            INIIO.saveSocketLogInf("JKID:" + "18C49");
            string receiveXml = HttpUtility.UrlDecode(outaccess.queryObjectOut("18C49", ConvertXmlToStringNoEncoder(xmldoc)));
            INIIO.saveSocketLogInf("【返回】\r\n" + receiveXml);
            return ReadVehicleInfDatatable(receiveXml, out code, out message);
        }
        public DataTable ReadVehicleInfDatatable(string xmlstring, out string code, out string message)
        {
            DataSet ds = new DataSet();
            ds = XmlToData.CXmlToDataSet(xmlstring);
            code = ds.Tables["head"].Rows[0]["code"].ToString();
            message = "";
            if (code != "1")
                message = ds.Tables["head"].Rows[0]["message"].ToString();
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
