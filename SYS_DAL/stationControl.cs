using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using SYS_DAL;
using SYS.Model;
using SYS_MODEL;
using System.Data.SqlClient;
using System.Data;
using SYS;

namespace SYS_DAL
{
    public partial class stationControl
    {
        #region 获取该站的stationID
        public string getStationID()
        {
            string sql = "select * from thisStation";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["STATIONID"].ToString();
                }
                else
                    return "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 获取该站的信息
        public stationInfModel   getStationInf(string stationid)
        {
            stationInfModel model = new stationInfModel();
            string sql = "select * from stationNormalInf";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.STATIONID = dt.Rows[0]["STATIONID"].ToString();
                    model.STATIONADD = dt.Rows[0]["STATIONADD"].ToString();
                    model.STATIONNAME = dt.Rows[0]["STATIONNAME"].ToString();
                    model.STATIONPERSON = dt.Rows[0]["STATIONPERSON"].ToString();
                    model.STATIONPHONE = dt.Rows[0]["STATIONPHONE"].ToString();
                    model.STATIONDATE = dt.Rows[0]["STATIONDATE"].ToString();
                    model.STATIONJCFF = dt.Rows[0]["STATIONJCFF"].ToString();
                    model.STANDARD = dt.Rows[0]["STANDARD"].ToString();
                    model.STATIONNET = dt.Rows[0]["STATIONNET"].ToString();
                    model.StationCompany = dt.Rows[0]["STATIONCOMPANY"].ToString();
                }
                else
                    model.STATIONID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取该站的认证信息
        public  stationRzxx getStationRzInf(string stationid)
        {
            stationRzxx model = new stationRzxx();
            string sql = "select * from [认证信息] where ID='" + stationid + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.RZBH = dt.Rows[0]["RZBH"].ToString();
                    model.RZYXQ = dt.Rows[0]["RZYXQ"].ToString();
                    model.ID = dt.Rows[0]["ID"].ToString();
                    
                }
                else
                    model.ID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取其他的信息
        public othersModel getOtherInf()
        {
            othersModel model = new othersModel();
            string sql = "select * from [其他信息]";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.JDDH = dt.Rows[0]["JDDH"].ToString();
                    model.FWDH = dt.Rows[0]["FWDH"].ToString();
                }
                else
                    model.JDDH = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取该检测站的检测线数
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public int getStationLineCount(string stationid)
        {
            string sql = "select count(*) as number from stationNormalInf" + " where STATIONID=" + "'" + stationid + "'";
            int count = 0;
            try
            {
                count = DBHelperSQL.ExecuteCount(sql);
                return count;
            }
            catch
            {
                return 0;
                throw;
            }
        }
        
        #endregion
        #region 获取检测站线的配置
        public DataTable getStationLineInf(string station)
        {
            DataTable model = null;
            string sql = "select * from stationLine where STATIONID='" + station + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取某一条线的信息
        public lineModel getLineInf(string station, string line)
        {
            lineModel model = new lineModel();
            string sql = "select * from stationLine where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.StationID = dt.Rows[0]["STATIONID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.ASM = dt.Rows[0]["ASM"].ToString();
                    model.VMAS = dt.Rows[0]["VMAS"].ToString();
                    model.SDS = dt.Rows[0]["SDS"].ToString();
                    model.JZJS_LIGHT = dt.Rows[0]["JZJS_LIGHT"].ToString();
                    model.JZJS_HEAVY = dt.Rows[0]["JZJS_HEAVY"].ToString();
                    model.ZYJS = dt.Rows[0]["ZYJS"].ToString();
                    model.LZ = dt.Rows[0]["LZ"].ToString();
                    model.PRINTER = dt.Rows[0]["PRINTER"].ToString();
                    model.AUTOPRINT = dt.Rows[0]["AUTOPRINT"].ToString();
                    
                }
                else
                    model.StationID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取某一条线的设备信息
        public equipmentModel getLineEquipInf(string station, string line)
        {
            equipmentModel model = new equipmentModel();
            string sql = "select * from [检测线设备] where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.StationID = dt.Rows[0]["STATIONID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.SBMC = dt.Rows[0]["SBMC"].ToString();
                    model.SBXH = dt.Rows[0]["SBXH"].ToString();
                    model.SBZZC = dt.Rows[0]["SBZZC"].ToString();
                    model.CGJXH = dt.Rows[0]["CGJXH"].ToString();
                    model.CGJBH = dt.Rows[0]["CGJBH"].ToString();//21
                    model.CGJZZC = dt.Rows[0]["CGJZZC"].ToString();
                    model.FXYXH = dt.Rows[0]["FXYXH"].ToString();
                    model.FXYBH = dt.Rows[0]["FXYBH"].ToString();
                    model.FXYZZC = dt.Rows[0]["FXYZZC"].ToString();
                    model.LLJXH = dt.Rows[0]["LLJXH"].ToString();
                    model.LLJBH = dt.Rows[0]["LLJBH"].ToString();
                    model.LLJZZC = dt.Rows[0]["LLJZZC"].ToString();
                    model.YDJXH = dt.Rows[0]["YDJXH"].ToString();
                    model.YDJBH = dt.Rows[0]["YDJBH"].ToString();
                    model.YDJZZC = dt.Rows[0]["YDJZZC"].ToString();
                    model.ZSJXH = dt.Rows[0]["ZSJXH"].ToString();
                    model.ZSJBH = dt.Rows[0]["ZSJBH"].ToString();
                    model.ZSJZZC = dt.Rows[0]["ZSJZZC"].ToString();

                }
                else
                    model.StationID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取某一条线的标定信息
        public LINESBBD getLineDemarcateInf(string station, string line)
        {
            LINESBBD model = new LINESBBD();
            string sql = "select * from [设备标定] where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.STATIONID = dt.Rows[0]["STATIONID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.HXBD = dt.Rows[0]["HXBD"].ToString();
                    model.HXENABLE = dt.Rows[0]["HXENABLE"].ToString();
                    model.JSGLBD = dt.Rows[0]["JSGLBD"].ToString();
                    model.JSGLENABLE = dt.Rows[0]["JSGLENABLE"].ToString();
                    model.GLBD = dt.Rows[0]["GLBD"].ToString();
                    model.GLENABLE = dt.Rows[0]["GLENABLE"].ToString();
                    model.YRDATE =DateTime.Parse(dt.Rows[0]["YRDATE"].ToString());
                    model.YRENABLE = dt.Rows[0]["YRENABLE"].ToString();
                    model.FXYBD = dt.Rows[0]["FXYBD"].ToString();
                    model.FXYENALBE = dt.Rows[0]["FXYENABLE"].ToString();
                    model.LLJBD = dt.Rows[0]["LLJBD"].ToString();
                    model.LLJENABLE = dt.Rows[0]["LLJENABLE"].ToString();

                }
                else
                    model.STATIONID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 更改某一条线的某一项标定项的时间
        public int setDemarcateTimebyName(string station, string line,string item, string time)
        {
            string sql = "update [设备标定] set "+item+"="+ "'" + time + "'" + " where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region 更改某一条线的的预热时间
        public int setlineYureTime(string station, string line, DateTime time)
        {
            string sql = "update [设备标定] set YRDATE=" + "'" + time.ToShortDateString() + "'" + " where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region 获取标定时间限值信息
        public BDSX getDemarcateInf()
        {
            BDSX model = new BDSX();
            string sql = "select * from [设备标定时限]";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.HXSX = dt.Rows[0]["HXSX"].ToString();
                    model.JSGLSX = dt.Rows[0]["JSGLSX"].ToString();
                    model.GLSX = dt.Rows[0]["GLSX"].ToString();
                    model.FXYSX = dt.Rows[0]["FXYSX"].ToString();
                    model.LLJSX = dt.Rows[0]["LLJSX"].ToString();
                    
                }
                else
                    model.HXSX = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 更改某一条线的打印机
        public int setLinePrinter(string station, string line, string printer)
        {
            string sql = "update stationLine set PRINTER=" + "'" + printer + "'" + " where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region 更改某一条线的自动打印机
        public int setLineAutoPrint(string station, string line, string autoprint)
        {
            string sql = "update stationLine set autoprint=" + "'" + autoprint + "'" + " where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region 获取某一条线的流水号信息
        public lshModel getLineLshInf(string station, string line)
        {
            lshModel model = new lshModel();
            string sql = "select * from [流水号信息] where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.StationID = dt.Rows[0]["StationID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.DATE = DateTime.Parse(dt.Rows[0]["DATE"].ToString());
                    model.COUNT = dt.Rows[0]["COUNT"].ToString();
                }
                else
                    model.StationID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 更改某一条线的检测数
        public int setLineLshCount(string station, string line,string count)
        {
            string sql = "update [流水号信息] set count="+"'"+count+"'"+" where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region 更改某一条线的日期
        public int setLineLshDate(string station, string line, string datestring)
        {
            string sql = "update [流水号信息] set DATE=" + "'" + datestring + "'" + " where STATIONID='" + station + "' and LINEID='" + line + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
        #region 获取某一条线的信息
        public string getLineByJCFF(string station, string jcff)
        {
            string linestring = "";
            string sql = "select * from stationLine where STATIONID='" + station + "' and "+jcff+"='Y'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        linestring += dt.Rows[i]["LINEID"].ToString() + ",";
                    }
                    return linestring;
                }
                else
                    return "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 获取某一种方法的费用
        public string getFYbyjcff(string jcff)
        {
            string sql = "select * from [费用] where MC='" + jcff + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["FY"].ToString();
                }
                else
                    return "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 根据工位ID获取该工位名称
        public string getGongweiNamef(int gongweiid)
        {
            string sql = "select * from gongweiInf where GONGWEIID='" + gongweiid.ToString() +"'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["GONGWEINAME"].ToString();
                }
                else
                    return "";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 根据台体获取串口配置信息
        public comModel getComInf(string machinename)
        {
            string sql = "select * from [串口配置] where MC='" + machinename + "'";
            try
            {
                comModel model = new comModel();
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.Comname = dt.Rows[0]["COM"].ToString();
                    model.Comstring = dt.Rows[0]["COMCONFIG"].ToString();
                    model.IsEnable = (dt.Rows[0]["ENABLE"].ToString() == "1") ? true : false;
                }
                else
                    model.Comname = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public int getStationLoginLshInf(string station)
        {
            string sql = "select * from stationNormalInf where STATIONID='" + station + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DJLSH"].ToString() == "")
                        return 1;
                    else
                        return int.Parse(dt.Rows[0]["DJLSH"].ToString());
                }
                else
                    return 0;       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception er)
            {
                throw (new Exception(er.Message));
            }
        }

        public DateTime getStationLoginLshDate(string station)
        {
            string sql = "select * from stationNormalInf where STATIONID='" + station + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["LOGINCOUNTDATE"].ToString() == "")
                        return DateTime.Now.AddDays(-1);
                    else
                        return DateTime.Parse(dt.Rows[0]["LOGINCOUNTDATE"].ToString());
                }
                else
                    return DateTime.Now.AddDays(-1);       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception er)
            {
                throw (new Exception(er.Message));
            }
        }

        public int setStationLoginLshDate(string station)
        {
            string sql = "update stationNormalInf set LOGINCOUNTDATE=" + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " where STATIONID='" + station + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public DataTable requerydata(string sql)
        {
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                throw;
            }
        }

        #region 更改总站的流水号信息
        public int setStationLshCount(string station, string count)
        {
            string sql = "update stationNormalInf set YWLSH=" + "'" + count + "'" + " where STATIONID='" + station + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int setStationLoginLshCount(string station, string count)
        {
            string sql = "update stationNormalInf set DJLSH=" + "'" + count + "'" + " where STATIONID='" + station + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return rows;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
    }
}


