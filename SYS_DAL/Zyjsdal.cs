using System;
using System.Data;
using System.Data.SqlClient;
using SYS.Model;

namespace SYS_DAL
{
     public class Zyjsdal
    {
        #region 用检测编号和检测次数判断一条检测数据是否存在
        /// <summary>
        /// 用检测编号和检测次数判断一条检测数据是否存在
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>bool</returns>
        public bool Have_Zyjs_Btg(string CLID)
        {
            string sql = "select * from zyjs_btg where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CLID)
                               };
            try
            {
                if (DBHelperSQL.GetDataTable(sql, CommandType.Text, spr).Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 用检测编号和检测次数删除一条检测数据
        /// <summary>
        /// 用检测编号和检测次数删除一条检测数据
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>bool</returns>
        public bool Delete_Zyjs_Btg(string CLID)
        {
            string sql = "delete zyjs_btg where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CLID)
                               };
            try
            {
                if (DBHelperSQL.Execute(sql, spr) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 用Zyjs_Btg对象插入或更新条检测数据
        /// <summary>
        /// 用Zyjs_Btg对象插入或更新条检测数据
        /// </summary>
        /// <param name="zyjs">Zyjs_Btg</param>
        /// <returns>int 0为失败，1为插入成功，2为更新成功</returns>
        public int Save_Zyjs_Btg(Zyjs_Btg zyjs)
        {
            string sqli = "insert into zyjs_btg(CLID,CLPH,FIRSTDATA,SECONDDATA,THIRDDATA,AVERAGEDATA,YDXZ,YDPD,DSZS,ZHPD,JCRQ,WD,SD,DQY,SBMC,SBXH,SBZZC,YDJXH,YDJBH,YDJZZC,ZSJXH,ZSJBH,ZSJZZC) values(@CLID,@CLPH,@FIRSTDATA,@SECONDDATA,@THIRDDATA,@AVERAGEDATA,@YDXZ,@YDPD,@DSZS,@ZHPD,@JCRQ,@WD,@SD,@DQY,@SBMC,@SBXH,@SBZZC,@YDJXH,@YDJBH,@YDJZZC,@ZSJXH,@ZSJBH,@ZSJZZC)";
            string sqlu = "update zyjs_btg set CLPH=@CLPH,FIRSTDATA=@FIRSTDATA,SECONDDATA=@SECONDDATA,THIRDDATA=@THIRDDATA,AVERAGEDATA=@AVERAGEDATA,YDXZ=@YDXZ,YDPD=@YDPD,DSZS=@DSZS,ZHPD=@ZHPD,JCRQ=@JCRQ,WD=@WD,SD=@SD,DQY=@DQY,SBMC=@SBMC,SBXH=@SBXH,SBZZC=@SBZZC,YDJXH=@YDJXH,YDJBH=@YDJBH,YDJZZC=@YDJZZC,ZSJXH=@ZSJXH,ZSJBH=@ZSJBH,ZSJZZC=@ZSJZZC where CLID=@CLID";
            SqlParameter[] spr={
                                   new SqlParameter("@CLID",zyjs.CLID), //1
                                   new SqlParameter("@CLPH",zyjs.CLPH),
                                   new SqlParameter("@FIRSTDATA",zyjs.FIRSTDATA),
                                   new SqlParameter("@SECONDDATA",zyjs.SECONDDATA),
                                   new SqlParameter("@THIRDDATA",zyjs.THIRDDATA),
                                   new SqlParameter("@AVERAGEDATA",zyjs.AVERAGEDATA),//6zyjs
                                   new SqlParameter("@YDXZ",zyjs.YDXZ),
                                   new SqlParameter("@YDPD",zyjs.YDPD),
                                   new SqlParameter("@DSZS",zyjs.DSZS),
                                   new SqlParameter("@ZHPD",zyjs.ZHPD),
                                   new SqlParameter("@JCRQ",zyjs.JCRQ),
                                   new SqlParameter("@WD",zyjs.WD),
                                   new SqlParameter("@SD",zyjs.SD),
                                   new SqlParameter("@DQY",zyjs.DQY),//16
                                   new SqlParameter("@SBMC",zyjs.SBMC),
                                   new SqlParameter("@SBXH",zyjs.SBXH),
                                   new SqlParameter("@SBZZC",zyjs.SBZZC),
                                   new SqlParameter("@ZSJXH",zyjs.ZSJXH),
                                   new SqlParameter("@ZSJBH",zyjs.ZSJBH),//21
                                   new SqlParameter("@ZSJZZC",zyjs.ZSJZZC),
                                   new SqlParameter("@YDJXH",zyjs.YDJXH),
                                   new SqlParameter("@YDJBH",zyjs.YDJBH),
                                   new SqlParameter("@YDJZZC",zyjs.YDJZZC)//47
                               };
            try
            {
                if (Have_Zyjs_Btg(zyjs.CLID))
                {
                    if (DBHelperSQL.Execute(sqlu, spr) > 0)
                        return 2;
                    else
                        return 0;
                }
                else
                {
                    if (DBHelperSQL.Execute(sqli, spr) > 0)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 用检测编号和次数查询一条检测数据
        /// <summary>
        /// 用检测编号和次数查询一条检测数据
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>Zyjs_Btg检测数据Model</returns>
        public Zyjs_Btg Get_Zyjs(string CLID)
        {
            DateTime a;
            string sql = "select * from zyjs_btg where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CLID)
                               };
            try
            {
                Zyjs_Btg zyjs = new Zyjs_Btg();
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text, spr);
                if (dt.Rows.Count > 0)
                {
                    zyjs.CLID = dt.Rows[0]["CLID"].ToString();//1
                    zyjs.CLPH = dt.Rows[0]["CLPH"].ToString();
                    zyjs.FIRSTDATA = dt.Rows[0]["FIRSTDATA"].ToString();
                    zyjs.SECONDDATA = dt.Rows[0]["SECONDDATA"].ToString();
                    zyjs.THIRDDATA = dt.Rows[0]["THIRDDATA"].ToString();
                    zyjs.AVERAGEDATA = dt.Rows[0]["AVERAGEDATA"].ToString();//6
                    if (dt.Columns.Contains("FOURTHDATA"))
                    {
                        zyjs.FOURTHDATA = dt.Rows[0]["FOURTHDATA"].ToString();
                    }
                    else
                        zyjs.FOURTHDATA = "";
                    zyjs.YDXZ = dt.Rows[0]["YDXZ"].ToString();//6
                    zyjs.YDPD = dt.Rows[0]["YDPD"].ToString();
                    zyjs.DSZS = dt.Rows[0]["DSZS"].ToString();
                    zyjs.ZHPD = dt.Rows[0]["ZHPD"].ToString();
                    DateTime.TryParse(dt.Rows[0]["JCRQ"].ToString(), out a);
                    if (a != null)
                        zyjs.JCRQ = a;
                    else
                        zyjs.JCRQ = DateTime.Today;
                    zyjs.WD = dt.Rows[0]["WD"].ToString();
                    zyjs.SD = dt.Rows[0]["SD"].ToString();
                    zyjs.DQY = dt.Rows[0]["DQY"].ToString();//16
                    zyjs.SBMC = dt.Rows[0]["SBMC"].ToString();
                    zyjs.SBXH = dt.Rows[0]["SBXH"].ToString();
                    zyjs.SBZZC = dt.Rows[0]["SBZZC"].ToString();
                    zyjs.ZSJXH = dt.Rows[0]["ZSJXH"].ToString();
                    zyjs.ZSJBH = dt.Rows[0]["ZSJBH"].ToString();//21
                    zyjs.ZSJZZC = dt.Rows[0]["ZSJZZC"].ToString();
                    zyjs.YDJXH = dt.Rows[0]["YDJXH"].ToString();
                    zyjs.YDJBH = dt.Rows[0]["YDJBH"].ToString();
                    zyjs.YDJZZC = dt.Rows[0]["YDJZZC"].ToString();
                }
                else
                {
                    zyjs.CLID = "-2";
                }
                return zyjs;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
