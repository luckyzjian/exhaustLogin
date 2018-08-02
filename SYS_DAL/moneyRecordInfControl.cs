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
    public partial class moneyRecordInfControl
    {
        #region 检查该收费信息是否存在
        public bool checkMoneyRecordIsAlive(string clid)
        {
            string sql = "select * from [收费记录] where CLID='" + clid + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        #endregion
        #region 获取所有员工信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllMoneyRecord()
        {
            string sql = "select * from [收费记录]";
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
        #endregion
        public bool savemoneyRecordInf(MONEY model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [收费记录] (");
            strSql.Append("CLID,");
            strSql.Append("CLHP,");
            strSql.Append("DLSJ,");
            strSql.Append("EXHAUST,");
            strSql.Append("SAFE,");
            strSql.Append("EXHAUSTSTANDARDFY,");
            strSql.Append("SAFESTANDARDFY,");
            strSql.Append("EXHAUSTFY,");
            strSql.Append("SAFEFY,");
            strSql.Append("EXHAUSTBZ,");
            strSql.Append("SAFEBZ,");
            strSql.Append("FY,");
            strSql.Append("SKR,");
            strSql.Append("TF,");
            strSql.Append("TFFY,");
            strSql.Append("CZ,");
            strSql.Append("SFSJ)");
            strSql.Append("values (@CLID,@CLHP,@DLSJ,@EXHAUST,@SAFE,@EXHAUSTSTANDARDFY,@SAFESTANDARDFY,@EXHAUSTFY,@SAFEFY,@EXHAUSTBZ,@SAFEBZ,@FY,@SKR,@TF,@TFFY,@CZ,@SFSJ)");
            SqlParameter[] parameters =
            {
                new SqlParameter("@CLID", SqlDbType.VarChar,50),
                new SqlParameter("@CLHP", SqlDbType.VarChar,50),
                 new SqlParameter("@DLSJ", SqlDbType.DateTime),
                new SqlParameter("@EXHAUST", SqlDbType.VarChar,50),
                new SqlParameter("@SAFE", SqlDbType.VarChar,50),
                new SqlParameter("@EXHAUSTSTANDARDFY", SqlDbType.VarChar,50),
                 new SqlParameter("@SAFESTANDARDFY", SqlDbType.VarChar,50),
                new SqlParameter("@EXHAUSTFY", SqlDbType.VarChar,50),
                new SqlParameter("@SAFEFY", SqlDbType.VarChar,50),
                new SqlParameter("@EXHAUSTBZ", SqlDbType.VarChar,50),
                 new SqlParameter("@SAFEBZ", SqlDbType.VarChar,50),
                new SqlParameter("@FY", SqlDbType.VarChar,50),
                new SqlParameter("@SKR", SqlDbType.VarChar,50),
                new SqlParameter("@TF", SqlDbType.VarChar,50),
                new SqlParameter("@TFFY", SqlDbType.VarChar,50),
                new SqlParameter("@CZ", SqlDbType.VarChar,50),
                 new SqlParameter("@SFSJ", SqlDbType.DateTime)
                };
            parameters[i++].Value = model.CLID;
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.DLSJ;
            parameters[i++].Value = model.EXHAUST;
            parameters[i++].Value = model.SAFE;
            parameters[i++].Value = model.EXHAUSTSTANDARDFY;
            parameters[i++].Value = model.SAFESTANDARDFY;
            parameters[i++].Value = model.EXHAUSTFY;
            parameters[i++].Value = model.SAFEFY;
            parameters[i++].Value = model.EXHAUSTBZ;
            parameters[i++].Value = model.SAFEBZ;
            parameters[i++].Value = model.FY;
            parameters[i++].Value = model.SKR;
            parameters[i++].Value = model.TF;
            parameters[i++].Value = model.TFFY;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SFSJ;
            try
            {
                if (checkMoneyRecordIsAlive(model.CLID))
                {
                    if (updateMoneyRecordInf(model))
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (DBHelperSQL.Execute(strSql.ToString(), parameters) > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool updateMoneyRecordInf(MONEY model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [收费记录] set ");
            strSql.Append("CLHP=@CLHP,");
            strSql.Append("DLSJ=@DLSJ,");

            strSql.Append("EXHAUST=@EXHAUST,");
            strSql.Append("SAFE=@SAFE,");
            strSql.Append("EXHAUSTSTANDARDFY=@EXHAUSTSTANDARDFY,");
            strSql.Append("SAFESTANDARDFY=@SAFESTANDARDFY,");
            strSql.Append("EXHAUSTFY=@EXHAUSTFY,");
            strSql.Append("SAFEFY=@SAFEFY,");
            strSql.Append("EXHAUSTBZ=@EXHAUSTBZ,");
            strSql.Append("SAFEBZ=@SAFEBZ,");
            strSql.Append("FY=@FY,");
            strSql.Append("SKR=@SKR,");
            strSql.Append("TF=@TF,");
            strSql.Append("TFFY=@TFFY,");
            strSql.Append("CZ=@CZ,");
            strSql.Append("SFSJ=@SFSJ");
            strSql.Append(" where CLID='" + model.CLID + "'");
            SqlParameter[] parameters =
            {
                new SqlParameter("@CLHP", SqlDbType.VarChar,50),
                new SqlParameter("@DLSJ", SqlDbType.DateTime),
                new SqlParameter("@EXHAUST", SqlDbType.VarChar,50),
                new SqlParameter("@SAFE", SqlDbType.VarChar,50),
                new SqlParameter("@EXHAUSTSTANDARDFY", SqlDbType.VarChar,50),
                 new SqlParameter("@SAFESTANDARDFY", SqlDbType.VarChar,50),
                new SqlParameter("@EXHAUSTFY", SqlDbType.VarChar,50),
                new SqlParameter("@SAFEFY", SqlDbType.VarChar,50),
                new SqlParameter("@EXHAUSTBZ", SqlDbType.VarChar,50),
                 new SqlParameter("@SAFEBZ", SqlDbType.VarChar,50),
                new SqlParameter("@FY", SqlDbType.VarChar,50),
                new SqlParameter("@SKR", SqlDbType.VarChar,50),
                new SqlParameter("@TF", SqlDbType.VarChar,50),
                new SqlParameter("@TFFY", SqlDbType.VarChar,50),
                new SqlParameter("@CZ", SqlDbType.VarChar,50),
                 new SqlParameter("@SFSJ", SqlDbType.DateTime)
                };
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.DLSJ;
            parameters[i++].Value = model.EXHAUST;
            parameters[i++].Value = model.SAFE;
            parameters[i++].Value = model.EXHAUSTSTANDARDFY;
            parameters[i++].Value = model.SAFESTANDARDFY;
            parameters[i++].Value = model.EXHAUSTFY;
            parameters[i++].Value = model.SAFEFY;
            parameters[i++].Value = model.EXHAUSTBZ;
            parameters[i++].Value = model.SAFEBZ;
            parameters[i++].Value = model.FY;
            parameters[i++].Value = model.SKR;
            parameters[i++].Value = model.TF;
            parameters[i++].Value = model.TFFY;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SFSJ;
            try
            {
                if (DBHelperSQL.Execute(strSql.ToString(), parameters) > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool deleteOneMoneyRecord(string CLID)
        {
            string sql = "delete from [收费记录] where CLID='" + CLID + "'";
            try
            {
                if (DBHelperSQL.GetDataTable(sql, CommandType.Text).Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
        public MONEY GetMoneyRecordInf(string clid)
        {
            int i = 0;
            MONEY model = new MONEY();
            StringBuilder strSql = new StringBuilder();
            string sql = "select * from [收费记录] where CLID='" + clid + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = clid;
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();
                    model.DLSJ = DateTime.Parse(dt.Rows[0]["DLSJ"].ToString());
                    model.EXHAUST = dt.Rows[0]["EXHAUST"].ToString();
                    model.SAFE = dt.Rows[0]["SAFE"].ToString();
                    model.EXHAUSTSTANDARDFY = dt.Rows[0]["EXHAUSTSTANDARDFY"].ToString();
                    model.SAFESTANDARDFY = dt.Rows[0]["SAFESTANDARDFY"].ToString();
                    model.EXHAUSTFY = dt.Rows[0]["EXHAUSTFY"].ToString();
                    model.SAFEFY = dt.Rows[0]["SAFEFY"].ToString();
                    model.EXHAUSTBZ = dt.Rows[0]["EXHAUSTBZ"].ToString();
                    model.SAFEBZ = dt.Rows[0]["SAFEBZ"].ToString();
                    model.FY = dt.Rows[0]["FY"].ToString();
                    model.SKR = dt.Rows[0]["SKR"].ToString();
                    model.SFSJ = DateTime.Parse(dt.Rows[0]["SFSJ"].ToString());
                    model.TF = dt.Rows[0]["TF"].ToString();
                    model.TFFY = dt.Rows[0]["TFFY"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
                return model;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #region 获取期间内所有未缴费车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限,cllx:1-轻型车，2-中型车，3-重型车，0，不限
        /// </summary>
        public DataTable GetMoneyRecordInf(string plate, string cz, DateTime starttime, DateTime endtime)
        {
            string sql = "";
            sql = "select * from [收费记录]" + " where DLSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and DLSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            //    sql = "select * from [收费信息]" + " where CLPH=" + "'" + plate + "'";//+ "' and CLHP LIKE '" + plate + "'";
            if (plate != "0")
                sql += " and CLHP=" + "'" + plate + "'";
            if (cz != "0")
            {
                sql += " and CZ='" + cz + "'";
            }
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion
        #region 获取所有参检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getMoneyStatistic(string lx)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select FY from [收费记录]";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " where DATEPART(yy, SFSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " where DATEPART(mm, SFSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, SFSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " where DATEPART(dd, SFSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, SFSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, SFSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion
        #region 获取所有待检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getMoneyStatistic(DateTime starttime, DateTime endtime)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select FY from [收费记录]";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " where SFSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and SFSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion

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
    }
}


