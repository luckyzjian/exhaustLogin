using System;
using System.Data;
using System.Data.SqlClient;
using SYS.Model;

namespace SYS_DAL
{
    public class SDSdal
    {
        #region 用检测编号判断一条检测数据是否存在
        /// <summary>
        /// 用检测编号判断一条检测数据是否存在
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <returns>bool</returns>
        public bool Have_SDS(string CLID)
        {
            string sql = "select * from SDS where CLID=@CLID";
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

        #region 用检测编号删除一条检测数据
        /// <summary>
        /// 用检测编号删除一条检测数据
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <returns>bool</returns>
        public bool Delete_SDS(string CLID)
        {
            string sql = "delete SDS where CLID=@CLID";
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

        #region 用SDS对象插入或更新条检测数据
        /// <summary>
        /// 用SDS对象插入或更新条检测数据
        /// </summary>
        /// <param name="SDS">SDS</param>
        /// <returns>int 0为失败，1为插入成功，2为更新成功</returns>
        public int Save_SDS(SDS SDS)
        {
            string sqli = "insert into SDS(CLID,CLPH,COLOWCLZ,COLOWXZ,HCLOWCLZ,HCLOWXZ,COHIGHCLZ,COHIGHXZ,HCHIGHCLZ,HCHIGHXZ,LAMDAHIGHCLZ,LAMDAHIGHXZ,LAMDAHIGHPD,LOWPD,HIGHPD,ZHPD,JCRQ,WD,SD,DQY,SBMC,SBXH,SBZZC,FXYXH,FXYBH,FXYZZC,ZSJXH,ZSJBH,ZSJZZC) values(@CLID,@CLPH,@COLOWCLZ,@COLOWXZ,@HCLOWCLZ,@HCLOWXZ,@COHIGHCLZ,@COHIGHXZ,@HCHIGHCLZ,@HCHIGHXZ,@LAMDAHIGHCLZ,@LAMDAHIGHXZ,@LAMDAHIGHPD,@LOWPD,@HIGHPD,@ZHPD,@JCRQ,@WD,@SD,@DQY,@SBMC,@SBXH,@SBZZC,@FXYXH,@FXYBH,@FXYZZC,@ZSJXH,@ZSJBH,@ZSJZZC)";
            string sqlu = "update SDS set CLPH=@CLPH,COLOWCLZ=@COLOWCLZ,COLOWXZ=@COLOWXZ,HCLOWCLZ=@HCLOWCLZ,HCLOWXZ=@HCLOWXZ,COHIGHCLZ=@COHIGHCLZ,COHIGHXZ=@COHIGHXZ,HCHIGHCLZ=@HCHIGHCLZ,HCHIGHXZ=@HCHIGHXZ,LAMDAHIGHCLZ=@LAMDAHIGHCLZ,LAMDAHIGHXZ=@LAMDAHIGHXZ,LOWPD=@LOWPD,HIGHPD=@HIGHPD,ZHPD=@ZHPD,JCRQ=@JCRQ,WD=@WD,SD=@SD,DQY=@DQY,SBMC=@SBMC,SBXH=@SBXH,SBZZC=@SBZZC,FXYXH=@FXYXH,FXYBH=@FXYBH,FXYZZC=@FXYZZC,ZSJXH=@ZSJXH,ZSJBH=@ZSJBH,ZSJZZC=@ZSJZZC where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",SDS.CLID), //1
                                   new SqlParameter("@CLPH",SDS.CLPH),
                                   new SqlParameter("@COLOWCLZ",SDS.COLOWCLZ),
                                   new SqlParameter("@COLOWXZ",SDS.COLOWXZ),
                                   new SqlParameter("@HCLOWXZ",SDS.HCLOWXZ),
                                   new SqlParameter("@COHIGHCLZ",SDS.COHIGHCLZ),//6
                                   new SqlParameter("@COHIGHXZ",SDS.COHIGHXZ),
                                   new SqlParameter("@HCHIGHCLZ",SDS.HCHIGHCLZ),
                                   new SqlParameter("@HCHIGHXZ",SDS.HCHIGHXZ),
                                   new SqlParameter("@LAMDAHIGHCLZ",SDS.LAMDAHIGHCLZ),
                                   new SqlParameter("@LAMDAHIGHXZ",SDS.LAMDAHIGHXZ),//11
                                   new SqlParameter("@LAMDAHIGHPD",SDS.LAMDAHIGHPD),//11                                   
                                   new SqlParameter("@LOWPD",SDS.LOWPD),
                                   new SqlParameter("@HIGHPD",SDS.HIGHPD),//11
                                   new SqlParameter("@ZHPD",SDS.ZHPD),
                                   new SqlParameter("@JCRQ",SDS.JCRQ),
                                   new SqlParameter("@WD",SDS.WD),
                                   new SqlParameter("@SD",SDS.SD),
                                   new SqlParameter("@DQY",SDS.DQY),//16
                                   new SqlParameter("@SBMC",SDS.SBMC),
                                   new SqlParameter("@SBXH",SDS.SBXH),
                                   new SqlParameter("@SBZZC",SDS.SBZZC),
                                   new SqlParameter("@ZSJXH",SDS.ZSJXH),
                                   new SqlParameter("@ZSJBH",SDS.ZSJBH),//21
                                   new SqlParameter("@ZSJZZC",SDS.ZSJZZC),
                                   new SqlParameter("@FXYXH",SDS.FXYXH),
                                   new SqlParameter("@FXYBH",SDS.FXYBH),
                                   new SqlParameter("@FXYZZC",SDS.FXYZZC)//47
                               };
            try
            {
                if (Have_SDS(SDS.CLID))
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
        /// <returns>SDS检测数据Model</returns>
        public SDS Get_SDS(string CLID)
        {
            DateTime a;
            string sql = "select * from SDS where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CLID)
                               };
            try
            {
                SDS SDS = new SDS();
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text, spr);
                if (dt.Rows.Count > 0)
                {
                    SDS.CLID = dt.Rows[0]["CLID"].ToString();//1
                    SDS.CLPH = dt.Rows[0]["CLPH"].ToString();
                    SDS.COLOWCLZ = dt.Rows[0]["COLOWCLZ"].ToString();
                    SDS.COLOWXZ = dt.Rows[0]["COLOWXZ"].ToString();
                    SDS.HCLOWCLZ = dt.Rows[0]["HCLOWCLZ"].ToString();
                    SDS.HCLOWXZ = dt.Rows[0]["HCLOWXZ"].ToString();//6
                    SDS.COHIGHCLZ = dt.Rows[0]["COHIGHCLZ"].ToString();
                    SDS.COHIGHXZ = dt.Rows[0]["COHIGHXZ"].ToString();
                    SDS.HCHIGHCLZ = dt.Rows[0]["HCHIGHCLZ"].ToString();
                    SDS.HCHIGHXZ = dt.Rows[0]["HCHIGHXZ"].ToString();
                    SDS.LAMDAHIGHCLZ = dt.Rows[0]["LAMDAHIGHCLZ"].ToString();//11
                    SDS.LAMDAHIGHXZ = dt.Rows[0]["LAMDAHIGHXZ"].ToString();//11
                    SDS.LAMDAHIGHPD = dt.Rows[0]["LAMDAHIGHPD"].ToString();//11
                    SDS.LOWPD = dt.Rows[0]["LOWPD"].ToString();//11
                    SDS.HIGHPD = dt.Rows[0]["HIGHPD"].ToString();//11

                    SDS.COLOWPD = dt.Rows[0]["COLOWPD"].ToString();//11
                    SDS.COHIGHPD = dt.Rows[0]["COHIGHPD"].ToString();//11
                    SDS.HCLOWPD = dt.Rows[0]["HCLOWPD"].ToString();//11
                    SDS.HCHIGHPD = dt.Rows[0]["HCHIGHPD"].ToString();//11

                    SDS.ZHPD = dt.Rows[0]["ZHPD"].ToString();
                    DateTime.TryParse(dt.Rows[0]["JCRQ"].ToString(), out a);
                    if (a != null)
                        SDS.JCRQ = a;
                    else
                        SDS.JCRQ = DateTime.Today;
                    SDS.WD = dt.Rows[0]["WD"].ToString();
                    SDS.SD = dt.Rows[0]["SD"].ToString();
                    SDS.DQY = dt.Rows[0]["DQY"].ToString();//16
                    SDS.SBMC = dt.Rows[0]["SBMC"].ToString();
                    SDS.SBXH = dt.Rows[0]["SBXH"].ToString();
                    SDS.SBZZC = dt.Rows[0]["SBZZC"].ToString();
                    SDS.ZSJXH = dt.Rows[0]["ZSJXH"].ToString();
                    SDS.ZSJBH = dt.Rows[0]["ZSJBH"].ToString();//21
                    SDS.ZSJZZC = dt.Rows[0]["ZSJZZC"].ToString();
                    SDS.FXYXH = dt.Rows[0]["FXYXH"].ToString();
                    SDS.FXYBH = dt.Rows[0]["FXYBH"].ToString();
                    SDS.FXYZZC = dt.Rows[0]["FXYZZC"].ToString();
                    SDS.ZSLOW = dt.Rows[0]["ZSLOW"].ToString();
                    SDS.ZSHIGH = dt.Rows[0]["ZSHIGH"].ToString();
                    SDS.CO2HIGH = dt.Rows[0]["CO2HIGH"].ToString();
                    SDS.O2HIGH = dt.Rows[0]["O2HIGH"].ToString();
                    SDS.CO2LOW = dt.Rows[0]["CO2LOW"].ToString();
                    SDS.O2LOW = dt.Rows[0]["O2LOW"].ToString();
                    if (dt.Columns.Contains("COLOWXXZ"))
                    {
                        SDS.COLOWXXZ = dt.Rows[0]["COLOWXXZ"].ToString();
                        SDS.COHIGHXXZ = dt.Rows[0]["COHIGHXXZ"].ToString();
                        SDS.COLOWXYZ = dt.Rows[0]["COLOWXYZ"].ToString();
                        SDS.COHIGHXYZ = dt.Rows[0]["COHIGHXYZ"].ToString();
                        SDS.CO2LOWXYZ = dt.Rows[0]["CO2LOWXYZ"].ToString();
                        SDS.CO2HIGHXYZ = dt.Rows[0]["CO2HIGHXYZ"].ToString();
                        SDS.HCLOWXYZ = dt.Rows[0]["HCLOWXYZ"].ToString();
                        SDS.HCHIGHXYZ = dt.Rows[0]["HCHIGHXYZ"].ToString();
                    }
                }
                else
                {
                    SDS.CLID = "-2";
                }
                return SDS;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


    }
}
