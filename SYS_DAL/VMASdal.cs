using System;
using System.Data;
using System.Data.SqlClient;
using SYS.Model;

namespace SYS_DAL
{
    public class VMASdal
    {
        #region 用检测编号和检测次数判断一条检测数据是否存在
        /// <summary>
        /// 用检测编号和检测次数判断一条检测数据是否存在
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>bool</returns>
        public bool Have_VMAS(string CARID)
        {
            string sql = "select * from vmas where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CARID)
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
        public bool Delete_ASM(string CARID)
        {
            string sql = "delete vmas where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CARID)
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

        #region 用ASM对象插入或更新条检测数据
        /// <summary>
        /// 用ASM对象插入或更新条检测数据
        /// </summary>
        /// <param name="asm">ASM</param>
        /// <returns>int 0为失败，1为插入成功，2为更新成功</returns>
        public int Save_VMAS(VMAS vmas)
        {
            string sqli = "insert into vmas(CLID,CLPH,COZL,COXZ,COPD,NOXZL,NOXXZ,NOXPD,HCZL,HCXZ,HCPD,ZHPD,JCRQ,WD,SD,DQY,SBMC,SBXH,SBZZC,CGJXH,CGJBH,CGJZZC,FXYXH,FXYBH,FXYZZC,LLJXH,LLJBH,LLJZZC,BEFORE) values(@CLID,@CLPH,@COZL,@COXZ,@COPD,@NOXZL,@NOXXZ,@NOXPD,@HCZL,@HCXZ,@HCPD,@ZHPD,@JCRQ,@WD,@SD,@DQY,@SBMC,@SBXH,@SBZZC,@CGJXH,@CGJBH,@CGJZZC,@FXYXH,@FXYBH,@FXYZZC,@LLJXH,@LLJBH,@LLJZZC,@BEFORE)";
            string sqlu = "update vmas set CLPH=@CLPH,COZL=@COZL,COXZ=@COXZ,COPD=@COPD,NOXZL=@NOXZL,NOXXZ=@NOXXZ,NOXPD=@NOXPD,HCZL=@HCZL,HCXZ=@HCXZ,HCPD=@HCPD,ZHPD=@ZHPD,JCRQ=@JCRQ,WD=@WD,SD=@SD,DQY=@DQY,SBMC=@SBMC,SBXH=@SBXH,SBZZC=@SBZZC,CGJXH=@CGJXH,CGJBH=@CGJBH,CGJZZC=@CGJZZC,FXYXH=@FXYXH,FXYBH=@FXYBH,FXYZZC=@FXYZZC,LLJXH=@LLJXH,LLJBH=@LLJBH,LLJZZC=@LLJZZC,BEFORE=@BEFORE where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",vmas.CLID), //1
                                   new SqlParameter("@CLPH",vmas.CLPH),
                                   new SqlParameter("@COZL",vmas.COZL),
                                   new SqlParameter("@COXZ",vmas.COXZ),
                                   new SqlParameter("@COPD",vmas.COPD),
                                   new SqlParameter("@NOXZL",vmas.NOXZL),//6
                                   new SqlParameter("@NOXXZ",vmas.NOXXZ),
                                   new SqlParameter("@NOXPD",vmas.NOXPD),
                                   new SqlParameter("@HCZL",vmas.HCZL),
                                   new SqlParameter("@HCXZ",vmas.HCXZ),
                                   new SqlParameter("@HCPD",vmas.HCPD),//11
                                   new SqlParameter("@ZHPD",vmas.ZHPD),
                                   new SqlParameter("@JCRQ",vmas.JCRQ),
                                   new SqlParameter("@WD",vmas.WD),
                                   new SqlParameter("@SD",vmas.SD),
                                   new SqlParameter("@DQY",vmas.DQY),//16
                                   new SqlParameter("@SBMC",vmas.SBMC),
                                   new SqlParameter("@SBXH",vmas.SBXH),
                                   new SqlParameter("@SBZZC",vmas.SBZZC),
                                   new SqlParameter("@CGJXH",vmas.CGJXH),
                                   new SqlParameter("@CGJBH",vmas.CGJBH),//21
                                   new SqlParameter("@CGJZZC",vmas.CGJZZC),
                                   new SqlParameter("@FXYXH",vmas.FXYXH),
                                   new SqlParameter("@FXYBH",vmas.FXYBH),
                                   new SqlParameter("@FXYZZC",vmas.FXYZZC),//47
                                   new SqlParameter("@LLJXH",vmas.FXYXH),
                                   new SqlParameter("@LLJBH",vmas.FXYBH),
                                   new SqlParameter("@LLJZZC",vmas.FXYZZC),//47
                                   new SqlParameter("@BEFORE",vmas.BEFORE)
                                                                   
                               };
            try
            {
                if (Have_VMAS(vmas.CLID))
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
        /// <returns>ASM检测数据Model</returns>
        public VMAS Get_VMAS(string CLID)
        {
            DateTime a;
            string sql = "select * from vmas where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CLID)
                               };
            try
            {
                VMAS vmas = new VMAS();
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text, spr);
                if (dt.Rows.Count > 0)
                {
                    vmas.CLID = dt.Rows[0]["CLID"].ToString();//1
                    vmas.CLPH = dt.Rows[0]["CLPH"].ToString();
                    vmas.COZL = dt.Rows[0]["COZL"].ToString();
                    vmas.COXZ = dt.Rows[0]["COXZ"].ToString();
                    vmas.COPD = dt.Rows[0]["COPD"].ToString();
                    vmas.NOXZL = dt.Rows[0]["NOXZL"].ToString();//6
                    vmas.NOXXZ = dt.Rows[0]["NOXXZ"].ToString();
                    vmas.NOXPD = dt.Rows[0]["NOXPD"].ToString();
                    vmas.HCZL = dt.Rows[0]["HCZL"].ToString();
                    vmas.HCXZ = dt.Rows[0]["HCXZ"].ToString();
                    vmas.HCPD = dt.Rows[0]["HCPD"].ToString();//11
                    vmas.ZHPD = dt.Rows[0]["ZHPD"].ToString();
                    DateTime.TryParse(dt.Rows[0]["JCRQ"].ToString(), out a);
                    if (a != null)
                        vmas.JCRQ = a;
                    else
                        vmas.JCRQ = DateTime.Today;
                    vmas.WD = dt.Rows[0]["WD"].ToString();
                    vmas.SD = dt.Rows[0]["SD"].ToString();
                    vmas.DQY = dt.Rows[0]["DQY"].ToString();//16
                    vmas.SBMC = dt.Rows[0]["SBMC"].ToString();
                    vmas.SBXH = dt.Rows[0]["SBXH"].ToString();
                    vmas.SBZZC = dt.Rows[0]["SBZZC"].ToString();
                    vmas.CGJXH = dt.Rows[0]["CGJXH"].ToString();
                    vmas.CGJBH = dt.Rows[0]["CGJBH"].ToString();//21
                    vmas.CGJZZC = dt.Rows[0]["CGJZZC"].ToString();
                    vmas.FXYXH = dt.Rows[0]["FXYXH"].ToString();
                    vmas.FXYBH = dt.Rows[0]["FXYBH"].ToString();
                    vmas.FXYZZC = dt.Rows[0]["FXYZZC"].ToString();
                    vmas.LLJXH = dt.Rows[0]["LLJXH"].ToString();
                    vmas.LLJBH = dt.Rows[0]["LLJBH"].ToString();
                    vmas.LLJZZC = dt.Rows[0]["LLJZZC"].ToString();
                    vmas.BEFORE = dt.Rows[0]["BEFORE"].ToString();
                }
                else 
                {
                    vmas.CLID= "-2";
                }
                return vmas;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
