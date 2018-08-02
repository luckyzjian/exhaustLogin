using System;
using System.Data;
using System.Data.SqlClient;
using SYS.Model;

namespace SYS_DAL
{
    public class ASMdal
    {
        #region 用检测编号和检测次数判断一条检测数据是否存在
        /// <summary>
        /// 用检测编号和检测次数判断一条检测数据是否存在
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>bool</returns>
        public bool Have_ASM(string CARID)
        {
            string sql = "select * from asm where CLID=@CLID";
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
            string sql = "delete asm where CLID=@CLID";
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
        public int Save_ASM(ASM asm)
        {
            string sqli = "insert into asm(CLID,CLPH,CO25CLZ,CO25XZ,CO25PD,HC25CLZ,HC25XZ,HC25PD,NOX25CLZ,NOX25XZ,NOX25PD,CO40CLZ,CO40XZ,CO40PD,HC40CLZ,HC40XZ,HC40PD,NOX40CLZ,NOX40XZ,NOX40PD,ZHPD,JCRQ,WD,SD,DQY,SBMC,SBXH,SBZZC,CGJXH,CGJBH,CGJZZC,FXYXH,FXYBH,FXYZZC) values(@CLID,@CLPH,@CO25CLZ,@CO25XZ,@CO25PD,@HC25CLZ,@HC25XZ,@HC25PD,@NOX25CLZ,@NOX25XZ,@NOX25PD,@CO40CLZ,@CO40XZ,@CO40PD,@HC40CLZ,@HC40XZ,@HC40PD,@NOX40CLZ,@NOX40XZ,@NOX40PD,@ZHPD,@JCRQ,@WD,@SD,@DQY,@SBMC,@SBXH,@SBZZC,@CGJXH,@CGJBH,@CGJZZC,@FXYXH,@FXYBH,@FXYZZC)";
            string sqlu = "update asm set CLPH=@CLPH,CO25CLZ=@CO25CLZ,CO25XZ=@CO25XZ,CO25PD=@CO25PD,HC25CLZ=@HC25CLZ,HC25XZ=@HC25XZ,HC25PD=@HC25PD,NOX25CLZ=@NOX25CLZ,NOX25XZ=@NOX25XZ,NOX25PD=@NOX25PD,CO40CLZ=@CO40CLZ,CO40XZ=@CO40XZ,CO40PD=@CO40PD,HC40CLZ=@HC40CLZ,HC40XZ=@HC40XZ,HC40PD=@HC40PD,NOX40CLZ=@NOX40CLZ,NOX40XZ=@NOX40XZ,NOX40PD=@NOX40PD,ZHPD=@ZHPD,JCRQ=@JCRQ,WD=@WD,SD=@SD,DQY=@DQY,SBMC=@SBMC,SBXH=@SBXH,SBZZC=@SBZZC,CGJXH=@CGJXH,CGJBH=@CGJBH,CGJZZC=@CGJZZC,FXYXH=@FXYXH,FXYBH=@FXYBH,FXYZZC=@FXYZZC where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",asm.CLID), //1
                                   new SqlParameter("@CLPH",asm.CLPH), //1
                                   new SqlParameter("@CO25CLZ",asm.CO25CLZ),
                                   new SqlParameter("@CO25XZ",asm.CO25XZ),
                                   new SqlParameter("@CO25PD",asm.CO25PD),
                                   new SqlParameter("@HC25CLZ",asm.HC25CLZ),//6
                                   new SqlParameter("@HC25XZ",asm.HC25XZ),
                                   new SqlParameter("@HC25PD",asm.HC25PD),
                                   new SqlParameter("@NOX25CLZ",asm.NOX25CLZ),
                                   new SqlParameter("@NOX25XZ",asm.NOX25XZ),
                                   new SqlParameter("@NOX25PD",asm.NOX25PD),//11
                                   new SqlParameter("@CO40CLZ",asm.CO40CLZ),
                                   new SqlParameter("@CO40XZ",asm.CO40XZ),
                                   new SqlParameter("@CO40PD",asm.CO40PD),
                                   new SqlParameter("@HC40CLZ",asm.HC40CLZ),//6
                                   new SqlParameter("@HC40XZ",asm.HC40XZ),
                                   new SqlParameter("@HC40PD",asm.HC40PD),
                                   new SqlParameter("@NOX40CLZ",asm.NOX40CLZ),
                                   new SqlParameter("@NOX40XZ",asm.NOX40XZ),
                                   new SqlParameter("@NOX40PD",asm.NOX40PD),//11
                                   new SqlParameter("@ZHPD",asm.ZHPD),
                                   new SqlParameter("@JCRQ",asm.JCRQ),
                                   new SqlParameter("@WD",asm.WD),
                                   new SqlParameter("@SD",asm.SD),
                                   new SqlParameter("@DQY",asm.DQY),//16
                                   new SqlParameter("@SBMC",asm.SBMC),
                                   new SqlParameter("@SBXH",asm.SBXH),
                                   new SqlParameter("@SBZZC",asm.SBZZC),
                                   new SqlParameter("@CGJXH",asm.CGJXH),
                                   new SqlParameter("@CGJBH",asm.CGJBH),//21
                                   new SqlParameter("@CGJZZC",asm.CGJZZC),
                                   new SqlParameter("@FXYXH",asm.FXYXH),
                                   new SqlParameter("@FXYBH",asm.FXYBH),
                                   new SqlParameter("@FXYZZC",asm.FXYZZC)
                                                                   
                               };
            try
            {
                if (Have_ASM(asm.CLID))
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
        public ASM Get_ASM(string CLID)
        {
            DateTime a;
            string sql = "select * from asm where CLID=@CLID";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLID",CLID)
                               };
            try
            {
                ASM asm = new ASM();
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text, spr);
                if (dt.Rows.Count > 0)
                {
                    asm.CLID = dt.Rows[0]["CLID"].ToString();//1
                    asm.CLPH = dt.Rows[0]["CLPH"].ToString();
                    asm.CO25CLZ = dt.Rows[0]["CO25CLZ"].ToString();
                    asm.CO25XZ = dt.Rows[0]["CO25XZ"].ToString();
                    asm.CO25PD = dt.Rows[0]["CO25PD"].ToString();
                    asm.HC25CLZ = dt.Rows[0]["HC25CLZ"].ToString();//6
                    asm.HC25XZ = dt.Rows[0]["HC25XZ"].ToString();
                    asm.HC25PD = dt.Rows[0]["HC25PD"].ToString();
                    asm.NOX25CLZ = dt.Rows[0]["NOX25CLZ"].ToString();
                    asm.NOX25XZ = dt.Rows[0]["NOX25XZ"].ToString();
                    asm.NOX25PD = dt.Rows[0]["NOX25PD"].ToString();//11
                    asm.CO40CLZ = dt.Rows[0]["CO40CLZ"].ToString();
                    asm.CO40XZ = dt.Rows[0]["CO40XZ"].ToString();
                    asm.CO40PD = dt.Rows[0]["CO40PD"].ToString();
                    asm.HC40CLZ = dt.Rows[0]["HC40CLZ"].ToString();//6
                    asm.HC40XZ = dt.Rows[0]["HC40XZ"].ToString();
                    asm.HC40PD = dt.Rows[0]["HC40PD"].ToString();
                    asm.NOX40CLZ = dt.Rows[0]["NOX40CLZ"].ToString();
                    asm.NOX40XZ = dt.Rows[0]["NOX40XZ"].ToString();
                    asm.NOX40PD = dt.Rows[0]["NOX40PD"].ToString();//11
                    asm.ZHPD = dt.Rows[0]["ZHPD"].ToString();
                    DateTime.TryParse(dt.Rows[0]["JCRQ"].ToString(), out a);
                    if (a != null)
                        asm.JCRQ = a;
                    else
                        asm.JCRQ = DateTime.Today;
                    asm.WD = dt.Rows[0]["WD"].ToString();
                    asm.SD = dt.Rows[0]["SD"].ToString();
                    asm.DQY = dt.Rows[0]["DQY"].ToString();//16
                    asm.SBMC = dt.Rows[0]["SBMC"].ToString();
                    asm.SBXH = dt.Rows[0]["SBXH"].ToString();
                    asm.SBZZC = dt.Rows[0]["SBZZC"].ToString();
                    asm.CGJXH = dt.Rows[0]["CGJXH"].ToString();
                    asm.CGJBH = dt.Rows[0]["CGJBH"].ToString();//21
                    asm.CGJZZC = dt.Rows[0]["CGJZZC"].ToString();
                    asm.FXYXH = dt.Rows[0]["FXYXH"].ToString();
                    asm.FXYBH = dt.Rows[0]["FXYBH"].ToString();
                    asm.FXYZZC = dt.Rows[0]["FXYZZC"].ToString();
                    asm.CO25025 = dt.Rows[0]["CO25025"].ToString();
                    asm.O25025 = dt.Rows[0]["O25025"].ToString();
                    asm.CO22540 = dt.Rows[0]["CO22540"].ToString();
                    asm.O22540 = dt.Rows[0]["O22540"].ToString();
                    if(dt.Columns.Contains("LAMBDA5025"))
                    {
                        asm.LAMBDA5025= dt.Rows[0]["LAMBDA5025"].ToString();
                        asm.LAMBDA2540 = dt.Rows[0]["LAMBDA2540"].ToString();
                    }
                }
                else
                {
                    asm.CLID = "-2";
                }
                return asm;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
