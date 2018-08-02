using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using ini;
using SYS.Model;
using System.Net;
using System.Collections;
using carinfor;

namespace JHWebClient
{
    public class JHdeviceInf
    {
        public string ID { set; get; }
        public string STMLINEID { set; get; }
        public string STMSTATIONCODE { set; get; }
        public string STMLINECODE { set; get; }
        public string CODE { set; get; }
        public string NAME { set; get; }
        public string TESTDEVICETYPE { set; get; }
        public string SERIES { set; get; }
        public string MODEL { set; get; }
        public string SPEC { set; get; }
        public string LOCKDESC { set; get; }
        public string MANUFCODE { set; get; }
        public string MANUFNAME { set; get; }
        public string MANUFDATE { set; get; }
        public string VENDORCODE { set; get; }
        public string VENDORNAME { set; get; }
        public string BOUGHTDATE { set; get; }
        public string KEEPER { set; get; }
        public string CERTCODE { set; get; }
        public string CERTDATE { set; get; }
        public string VALIDDATE { set; get; }
        public string STATUS { set; get; }
        public string SCORE { set; get; }
        public string RCREATETIME { set; get; }
        public string RUPDATETIME { set; get; }
        public string RMEMO { set; get; }
        public string RSTATUS { set; get; }
        public string IFCHECK { set; get; }
    }
    public class checkResultModel
    {
        public string testmethod { set; get; }
        public string testresult { set; get; }
        public string value01 { set; get; }
        public string value02 { set; get; }
        public string value03 { set; get; }
        public string value04 { set; get; }
        public string value05 { set; get; }
        public string value06 { set; get; }
        public string limit01 { set; get; }
        public string limit02 { set; get; }
        public string limit03 { set; get; }
        public string limit04 { set; get; }
        public string limit05 { set; get; }
        public string limit06 { set; get; }
        public string judge01 { set; get; }
        public string judge02 { set; get; }
        public string judge03 { set; get; }
        public string judge04 { set; get; }
        public string judge05 { set; get; }
        public string judge06 { set; get; }
        public string parame01 { set; get; }
        public string parame02 { set; get; }
        public string parame03 { set; get; }
        public string parame04 { set; get; }
        public string parame05 { set; get; }
        public string parame06 { set; get; }
    }
    
    public class JHvehicleInf
    {
        public string ID { set; get; }
        public string GAXH { set; get; }
        public string PLATENO { set; get; }
        public string PLATECLASS { set; get; }
        public string PLATECOLOR { set; get; }
        public string GAVTYPE { set; get; }
        public string EPVTYPE { set; get; }
        public string STVTYPE { set; get; }
        public string GAUSEC { set; get; }
        public string EPUSEC { set; get; }
        public string PREVDISTRICT { set; get; }
        public string CURDISTRICT { set; get; }
        public string FIRSTREGDATE { set; get; }
        public string SCRAPLIMITDATE { set; get; }
        public string EMISSION { set; get; }
        public string EPRANK { set; get; }
        public string VIN { set; get; }
        public string VIN8 { set; get; }
        public string BRAND { set; get; }
        public string MODEL { set; get; }
        public string SALETYPE { set; get; }
        public string MANUFCOUNTRY { set; get; }
        public string MANUFNAME { set; get; }
        public string ENGINENO { set; get; }
        public string ENGINEMODEL { set; get; }
        public string FUELTYPE { set; get; }
        public string DISPLACEMENT { set; get; }
        public string POWER { set; get; }
        public string REATEDPOWER { set; get; }
        public string REATEDSPEED { set; get; }
        public string AIRSUCTION { set; get; }
        public string BODYCOLOR { set; get; }
        public string BODYLENGTH { set; get; }
        public string BODYWIDTH { set; get; }
        public string BODYHEIGHT { set; get; }
        public string GROSSWEIGHT { set; get; }
        public string REATEDWEIGHT { set; get; }
        public string CURBWEIGHT { set; get; }
        public string PASSCAP { set; get; }
        public string TIRECOUNT { set; get; }
        public string TRAVELMILES { set; get; }
        public string DRIVEMODE { set; get; }
        public string GEARBOXTYPE { set; get; }
        public string EXFACTORYDATE { set; get; }
        public string GASTATUS { set; get; }
        public string EPSTATUS { set; get; }
        public string OWNERID { set; get; }
        public string OWNERNAME { set; get; }
        public string PROPRIGHT { set; get; }
        public string INSPLASTDATE { set; get; }
        public string INSPVALIDDATE { set; get; }
        public string TESTMETHOD { set; get; }
        public string ISOBD { set; get; }
        public string VEHICLEDESC { set; get; }
        public string TECHIDENTSTATUS { set; get; }
        public string CLINDYERNUM { set; get; }
        public string GEARNUM { set; get; }
        public string RMWEIGHT { set; get; }
        public string AXLEWEIGHT { set; get; }
        public string FUELWAY { set; get; }
        public string EXHAUSTDISPOSAL { set; get; }
        public string OXYGENSENSOR { set; get; }
        public string ENGINEMANUF { set; get; }

        public string OWNERPROPRIGHT { set; get; }
        public string OWNERCERTTYPE { set; get; }
        public string OWNERCERTNO { set; get; }
        public string OWNERTELNO { set; get; }
        public string OWNERADDRESS { set; get; }
        public string OWNERDISTRICT { set; get; }
        public string OWNERPOSTCODE { set; get; }

        public string REGID { set; get; }
        public string REGVEHICLEID { set; get; }
        public string REGPLATENO { set; get; }
        public string REGPLATECLASS { set; get; }
        public string REGPLATECOLOR { set; get; }
        public string REGVIN { set; get; }
        public string REGVIN8 { set; get; }
        public string REGEXFACTORYDATE { set; get; }
        public string REGVEHREGDATE { set; get; }
        public string REGVEHICLEMODEL { set; get; }
        public string REGFUELTYPE { set; get; }
        public string REGINSPFUELTYPE { set; get; }
        public string REGCLINDYERNUM { set; get; }
        public string REGTRAVELMILES { set; get; }
        public string REGVEHICLEDESC { set; get; }
        public string REGINSPTYPE { set; get; }
        public string REGINSPPERIOD { set; get; }
        public string REGINSPTIMES { set; get; }
        public string REGDISTRICT { set; get; }
        public string REGINSPSTATIONCODE { set; get; }
        public string REGINSPLINECODE { set; get; }
        public string REGTSNO { set; get; }
        public string REGTESTMETHOD { set; get; }
        public string REGREGUSERID { set; get; }
        public string REGREGTIME { set; get; }
        public string REGAPPOINTTESTTIME { set; get; }
        public string REGTESTNO { set; get; }
        public string REGRESULT { set; get; }
        public string REGAPPEARANCERESULT { set; get; }
        public string REGAPPEARANCECODE { set; get; }
        public string REGAPPEARANCEPERSON { set; get; }
        public string REGRECEIVABLES { set; get; }
        public string REGPROCEEDS { set; get; }
        public string REGINVOICENUM { set; get; }
        public string REGTOLLCOLLECTOR { set; get; }
        public string REGREGSTATUS { set; get; }


    }
    public class XmlToData
    {
        /**/
        /// <summary>
        /// 将Xml内容字符串转换成DataSet对象
        /// </summary>
        /// <param >Xml内容字符串</param>
        /// <returns>DataSet对象</returns>
        public static DataSet CXmlToDataSet(string xmlStr)
        {
            if (!string.IsNullOrEmpty(xmlStr))
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                try
                {
                    DataSet ds = new DataSet();
                    //读取字符串中的信息
                    StrStream = new StringReader(xmlStr);
                    //获取StrStream中的数据
                    Xmlrdr = new XmlTextReader(StrStream);
                    //ds获取Xmlrdr中的数据                
                    ds.ReadXml(Xmlrdr);
                    return ds;
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    //释放资源
                    if (Xmlrdr != null)
                    {
                        Xmlrdr.Close();
                        StrStream.Close();
                        StrStream.Dispose();
                    }
                }
            }
            else
            {
                return null;
            }
        }
        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param >Xml字符串</param>
        /// <param >Table表索引</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr, int tableIndex)
        {
            return CXmlToDataSet(xmlStr).Tables[tableIndex];
        }
        /**/
        /// <summary>
        /// 将Xml字符串转换成DataTable对象
        /// </summary>
        /// <param >Xml字符串</param>
        /// <returns>DataTable对象</returns>
        public static DataTable CXmlToDatatTable(string xmlStr)
        {
            return CXmlToDataSet(xmlStr).Tables[0];
        }

    }
    public class JHinterface
    {
        string securitycode = "";
        string inspstationcode = "";
        string insplinecode = "";
        IInspService outlineservice = null;
        public JHinterface(string url, string securitycode, string inspstationcode, string insplinecode)
        {
            outlineservice = new IInspService(url);
            this.securitycode = securitycode;
            this.inspstationcode = inspstationcode;
            this.insplinecode = insplinecode;
        }
        public JHinterface()
        { }
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
            sr.Close();
            stream.Close();
            string newstring = xmlString.Replace("xml version=\"1.0\"", "xml version=\"1.0\" encoding=\"UTF-8\"");//.Replace(" ", "%20").Replace("\r\n", "%0d%0a") + "\r\n";
            INIIO.saveSocketLogInf("SEND:\r\n" + newstring);
            return newstring;
        }
        public bool CheckPrintIsEffective(string webadd, string testingid, out string testno, out string resultno, out string resultdesc, out checkResultModel checkmodel)
        {
            try
            {
                bool checkresult = false;
                resultno = "";
                resultdesc = "";
                testno = "";
                checkmodel = new checkResultModel();
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://" + webadd + "/struts/intf/stationIntf!testingCheck.action?testingid=" + testingid);
                req.Method = "GET";
                string result = "";
                using (WebResponse wr = req.GetResponse())
                {
                    StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                    result = sr.ReadToEnd();
                    if (result != "")
                    {
                        try
                        {
                            DataSet ds = new DataSet();
                            ds = XmlToData.CXmlToDataSet(result);
                            DataTable checkinfo = ds.Tables["checkinfo"];
                            resultno = ds.Tables["resultno"].Rows[0]["resultno_Text"].ToString();
                            resultdesc = ds.Tables["resultdesc"].Rows[0]["resultdesc_Text"].ToString();
                            testno = ds.Tables["testno"].Rows[0]["testno_Text"].ToString();
                            if (int.Parse(resultno) > 0)
                            {
                                checkmodel.testmethod = checkinfo.Rows[0]["testmethod"].ToString();
                                checkmodel.testresult = checkinfo.Rows[0]["testresult"].ToString();
                                checkmodel.value01 = checkinfo.Rows[0]["value01"].ToString();
                                checkmodel.value02 = checkinfo.Rows[0]["value02"].ToString();
                                checkmodel.value03 = checkinfo.Rows[0]["value03"].ToString();
                                checkmodel.value04 = checkinfo.Rows[0]["value04"].ToString();
                                checkmodel.value05 = checkinfo.Rows[0]["value05"].ToString();
                                checkmodel.value06 = checkinfo.Rows[0]["value06"].ToString();
                                checkmodel.limit01 = checkinfo.Rows[0]["limit01"].ToString();
                                checkmodel.limit02 = checkinfo.Rows[0]["limit02"].ToString();
                                checkmodel.limit03 = checkinfo.Rows[0]["limit03"].ToString();
                                checkmodel.limit04 = checkinfo.Rows[0]["limit04"].ToString();
                                checkmodel.limit05 = checkinfo.Rows[0]["limit05"].ToString();
                                checkmodel.limit06 = checkinfo.Rows[0]["limit06"].ToString();
                                checkmodel.judge01 = checkinfo.Rows[0]["judge01"].ToString();
                                checkmodel.judge02 = checkinfo.Rows[0]["judge02"].ToString();
                                checkmodel.judge03 = checkinfo.Rows[0]["judge03"].ToString();
                                checkmodel.judge04 = checkinfo.Rows[0]["judge04"].ToString();
                                checkmodel.judge05 = checkinfo.Rows[0]["judge05"].ToString();
                                checkmodel.judge06 = checkinfo.Rows[0]["judge06"].ToString();
                                checkmodel.parame01 = checkinfo.Rows[0]["parame01"].ToString();
                                checkmodel.parame02 = checkinfo.Rows[0]["parame02"].ToString();
                                checkmodel.parame03 = checkinfo.Rows[0]["parame03"].ToString();
                                checkmodel.parame04 = checkinfo.Rows[0]["parame04"].ToString();
                                checkmodel.parame05 = checkinfo.Rows[0]["parame05"].ToString();
                                checkmodel.parame06 = checkinfo.Rows[0]["parame06"].ToString();
                            }
                            checkresult = true;
                        }
                        catch (Exception er)
                        {
                            resultno = "-1";
                            resultdesc = er.Message;
                        }
                    }
                    //在这里对接收到的页面内容进行处理
                }
                return checkresult;
            }
            catch(Exception er)
            {
                throw (new ApplicationException(er.Message));
            }
        }
        
        
    }
}
