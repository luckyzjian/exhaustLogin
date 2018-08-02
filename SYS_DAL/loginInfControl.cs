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
    public class operatorMode
    {
        public string ID { set; get; }
        public string CLHP { set; get; }
        public string STATUS { set; get; }
        public string UPDATETIME { set; get; }
        public string LINEID { set; get; }
        public string YL1 { set; get; }
        public string YL2 { set; get; }
        public string YL3 { set; get; }
        public string YL4 { set; get; }

    }
    public partial class loginInfControl
    {
        #region 用车号插入一条运行记录数据
        /// <summary>
        /// 用检测编号和检测次数删除一条检测数据
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>bool</returns>
        public bool Delete_OperateRecord(string clhp)
        {
            string sql = "delete [运行状态] where CLHP=@CLHP";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLHP",clhp)
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

        #region 用检测编号和次数查询一条检测数据
        /// <summary>
        /// 用检测编号和次数查询一条检测数据
        /// </summary>
        /// <param name="jcbh">检测编号</param>
        /// <param name="jccs">检测次数</param>
        /// <returns>JZJS检测数据Model</returns>
        public bool Have_OperatorRecord(string CLHP)
        {
            string sql = "select * from [运行状态] where CLHP=@CLHP";
            SqlParameter[] spr ={
                                   new SqlParameter("@CLHP",CLHP)
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

        #region 用车辆号牌插入一条运行状态记录
        /// <summary>
        /// 用JZJS对象插入或更新条检测数据
        /// </summary>
        /// <param name="JZJS">JZJS</param>
        /// <returns>int 0为失败，1为插入成功，2为更新成功</returns>
        public int Save_OperateRecord(operatorMode model)
        {
            string sqli = "insert into [运行状态](ID,UPDATETIME,CLHP,LINEID,STATUS,YL1,YL2,YL3,YL4) values(@ID,@UPDATETIME,@CLHP,@LINEID,@STATUS,@YL1,@YL2,@YL3,@YL4)";
            string sqlu = "update [运行状态] set ID=@ID,UPDATETIME=@UPDATETIME,LINEID=@LINEID,STATUS=@STATUS,YL1=@YL1,YL2=@YL2,YL3=@YL3,YL4=@YL4 where CLHP=@CLHP";
            SqlParameter[] spr ={
                                   new SqlParameter("@ID",model.ID), //1
                                   new SqlParameter("@UPDATETIME",model.UPDATETIME),
                                   new SqlParameter("@CLHP",model.CLHP),
                                   new SqlParameter("@LINEID",model.LINEID),
                                   new SqlParameter("@STATUS",model.STATUS),
                                   new SqlParameter("@YL1",model.YL1),//6
                                   new SqlParameter("@YL2",model.YL2),
                                   new SqlParameter("@YL3",model.YL3),
                                   new SqlParameter("@YL4",model.YL4)
                               };
            try
            {
                if (Have_OperatorRecord(model.CLHP))
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
        #region 获取登陆界面的默认信息
        public loginInfModel getLoginDefaultInf()
        {
            DateTime a,b;
            loginInfModel model = new loginInfModel();
            string sql = "select * from [登录默认]";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.CLLX = dt.Rows[0]["CLLX"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                    model.SYXZ = dt.Rows[0]["SYXZ"].ToString();
                    model.PP = dt.Rows[0]["PP"].ToString();
                    model.XH = dt.Rows[0]["XH"].ToString();
                    model.CLSBM = dt.Rows[0]["CLSBM"].ToString();
                    model.FDJHM = dt.Rows[0]["FDJHM"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.SCQY = dt.Rows[0]["SCQY"].ToString();
                    model.HDZK = dt.Rows[0]["HDZK"].ToString();
                    model.JSSZK = dt.Rows[0]["JSSZK"].ToString();
                    model.ZZL = dt.Rows[0]["ZZL"].ToString();
                    model.HDZZL = dt.Rows[0]["HDZZL"].ToString();
                    model.ZBZL = dt.Rows[0]["ZBZL"].ToString();
                    model.JZZL = dt.Rows[0]["JZZL"].ToString();
                    DateTime.TryParse(dt.Rows[0]["ZCRQ"].ToString(),out a);
                    if (a != null)
                        model.ZCRQ = a;
                    else
                        model.ZCRQ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["SCRQ"].ToString(), out b);
                    if (b != null)
                        model.SCRQ = b;
                    else
                        model.SCRQ = DateTime.Today;
                    model.FDJPL = dt.Rows[0]["FDJPL"].ToString();
                    model.RLZL = dt.Rows[0]["RLZL"].ToString();
                    model.EDGL = dt.Rows[0]["EDGL"].ToString();
                    model.EDZS = dt.Rows[0]["EDZS"].ToString();
                    model.BSQXS = dt.Rows[0]["BSQXS"].ToString();
                    model.DWS = dt.Rows[0]["DWS"].ToString();
                    model.GYFS = dt.Rows[0]["GYFS"].ToString();
                    model.DPFS = dt.Rows[0]["DPFS"].ToString();
                    model.JQFS = dt.Rows[0]["JQFS"].ToString();
                    model.QGS = dt.Rows[0]["QGS"].ToString();
                    model.QDXS = dt.Rows[0]["QDXS"].ToString();
                    model.CHZZ = dt.Rows[0]["CHZZ"].ToString();
                    model.DLSP = dt.Rows[0]["DLSP"].ToString();
                    model.SFSRL = dt.Rows[0]["SFSRL"].ToString();
                    model.JHZZ = dt.Rows[0]["JHZZ"].ToString();
                    model.OBD = dt.Rows[0]["OBD"].ToString();
                    model.DKGYYB = dt.Rows[0]["DKGYYB"].ToString();
                    model.LXDH = dt.Rows[0]["LXDH"].ToString();
                }
                else
                    model.CLHP = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        public bool updatePlatePrefix(string newPlate)
        {
            string sql = "update [登录默认] set CLHP=" + "'" + newPlate + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        #region 获取comBoBox的Items值
        public DataTable getComBoBoxItemsInf(string comboboxName)
        {
            string sql = "select * from" + " [" + comboboxName + "]";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                    return null;       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        #endregion
        public DataTable getEnlishDatableTable(string comboboxName)
        {
            string sql = "select * from " + comboboxName;
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                    return null;       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }

        }
        #region 根据comBoBox的Items值获得对应的ID值
        public string  getComBoBoxItemsID(string comboboxName,string itemName)
        {
            string sql = "select * from" + " [" + comboboxName + "]"+" where MC='"+itemName+"'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["ID"].ToString();
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
        #region 根据comBoBox的ID值获得对应的items值
        public string getComBoBoxItemsNAME(string comboboxName, string itemID)
        {
            string sql = "select * from" + " [" + comboboxName + "]" + " where ID='" + itemID + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["MC"].ToString();
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
        #region 检查员工是否存在
        public string checkUserIsAlive(string userName, string password)
        {
            string userID = "";
            string sql = "select STAFFID from [员工] where NAME='" + userName + "' and PASSWORD='" + password + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["STAFFID"].ToString(); ;
                }
                else
                    return "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                return "-2";
                throw;
            }

        }
        #endregion
        #region 检查员工是否存在
        public bool checkUserIsAlive(string USERID)
        {
            string sql = "select * from [员工] where STAFFID='" + USERID + "'";
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
        #region 检查员工是否存在
        public DataTable getStaffByPost(string postID)
        {
            string sql = "select * from [员工] where postid='" + postID + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion
        #region 获取所有员工信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllStaff()
        {
            string sql = "select * from [员工]";
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
        public bool saveStaffInf(staffModel model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [员工] (");
            strSql.Append("STAFFID,");
            strSql.Append("NAME,");
            strSql.Append("POSTID,");
            strSql.Append("SEX,");
            strSql.Append("BIRTHDAY,");
            strSql.Append("AGE,");
            strSql.Append("ID,");
            strSql.Append("EDUCATION,");
            strSql.Append("MARRIED,");
            strSql.Append("ADDRESS,");
            strSql.Append("PHONE,");
            strSql.Append("QQ,");
            strSql.Append("EMAIL,");
            strSql.Append("PASSWORD)");
            strSql.Append("values (@STAFFID,@NAME,@POSTID,@SEX,@BIRTHDAY,@AGE,@ID,@EDUCATION,@MARRIED,@ADDRESS,@PHONE,@QQ,@EMAIL,@PASSWORD)");
            SqlParameter[] parameters =
            {
                new SqlParameter("@STAFFID", SqlDbType.VarChar,50),
                 new SqlParameter("@NAME", SqlDbType.VarChar,50),
                new SqlParameter("@POSTID", SqlDbType.VarChar,50),
                new SqlParameter("@SEX", SqlDbType.VarChar,50),
                new SqlParameter("@BIRTHDAY", SqlDbType.VarChar,50),
                 new SqlParameter("@AGE", SqlDbType.VarChar,50),
                new SqlParameter("@ID", SqlDbType.VarChar,50),
                new SqlParameter("@EDUCATION", SqlDbType.VarChar,50),
                new SqlParameter("@MARRIED", SqlDbType.VarChar,50),
                 new SqlParameter("@ADDRESS", SqlDbType.VarChar,50),
                new SqlParameter("@PHONE", SqlDbType.VarChar,50),
                new SqlParameter("@QQ", SqlDbType.VarChar,50),
                new SqlParameter("@EMAIL", SqlDbType.VarChar,50),
                 new SqlParameter("@PASSWORD", SqlDbType.VarChar,50)
                };
            parameters[i++].Value = model.STAFFID;
            parameters[i++].Value = model.NAME;
            parameters[i++].Value = model.POSTID;
            parameters[i++].Value = model.SEX;
            parameters[i++].Value = model.BIRTHDAY;
            parameters[i++].Value = model.AGE;
            parameters[i++].Value = model.ID;
            parameters[i++].Value = model.EDUCATION;
            parameters[i++].Value = model.MARRIED;
            parameters[i++].Value = model.ADDRESS;
            parameters[i++].Value = model.PHONE;
            parameters[i++].Value = model.QQ;
            parameters[i++].Value = model.EMAIL;
            parameters[i++].Value = model.PASSWORD;
            try
            {
                if (checkUserIsAlive(model.STAFFID))
                {
                    if (updateStaffInf(model))
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

        public bool updateStaffInf(staffModel model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [员工] set ");
            strSql.Append("NAME=@NAME,");
            strSql.Append("POSTID=@POSTID,");
            strSql.Append("SEX=@SEX,");
            strSql.Append("BIRTHDAY=@BIRTHDAY,");
            strSql.Append("AGE=@AGE,");
            strSql.Append("ID=@ID,");
            strSql.Append("EDUCATION=@EDUCATION,");
            strSql.Append("MARRIED=@MARRIED,");
            strSql.Append("ADDRESS=@ADDRESS,");
            strSql.Append("PHONE=@PHONE,");
            strSql.Append("QQ=@QQ,");
            strSql.Append("EMAIL=@EMAIL,");
            strSql.Append("PASSWORD=@PASSWORD");
            strSql.Append(" where STAFFID='" + model.STAFFID + "'");
            SqlParameter[] parameters =
            {
                //new SqlParameter("@STAFFID", SqlDbType.VarChar,50),
                 new SqlParameter("@NAME", SqlDbType.VarChar,50),
                new SqlParameter("@POSTID", SqlDbType.VarChar,50),
                new SqlParameter("@SEX", SqlDbType.VarChar,50),
                new SqlParameter("@BIRTHDAY", SqlDbType.VarChar,50),
                 new SqlParameter("@AGE", SqlDbType.VarChar,50),
                new SqlParameter("@ID", SqlDbType.VarChar,50),
                new SqlParameter("@EDUCATION", SqlDbType.VarChar,50),
                new SqlParameter("@MARRIED", SqlDbType.VarChar,50),
                 new SqlParameter("@ADDRESS", SqlDbType.VarChar,50),
                new SqlParameter("@PHONE", SqlDbType.VarChar,50),
                new SqlParameter("@QQ", SqlDbType.VarChar,50),
                new SqlParameter("@EMAIL", SqlDbType.VarChar,50),
                 new SqlParameter("@PASSWORD", SqlDbType.VarChar,50)
                };
            //parameters[i++].Value = model.STAFFID;
            parameters[i++].Value = model.NAME;
            parameters[i++].Value = model.POSTID;
            parameters[i++].Value = model.SEX;
            parameters[i++].Value = model.BIRTHDAY;
            parameters[i++].Value = model.AGE;
            parameters[i++].Value = model.ID;
            parameters[i++].Value = model.EDUCATION;
            parameters[i++].Value = model.MARRIED;
            parameters[i++].Value = model.ADDRESS;
            parameters[i++].Value = model.PHONE;
            parameters[i++].Value = model.QQ;
            parameters[i++].Value = model.EMAIL;
            parameters[i++].Value = model.PASSWORD;
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
        public bool deleteOnePerson(string carID)
        {
            string sql = "delete from [员工] where STAFFID='" + carID + "'";
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
        public staffModel GetStaffInf(string clid)
        {
            int i = 0;
            staffModel model = new staffModel();
            StringBuilder strSql = new StringBuilder();
            string sql = "select * from [员工] where STAFFID='" + clid + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.STAFFID = dt.Rows[0]["STAFFID"].ToString();
                    model.NAME = dt.Rows[0]["NAME"].ToString();
                    model.POSTID = dt.Rows[0]["POSTID"].ToString();
                    model.SEX = dt.Rows[0]["SEX"].ToString();
                    model.BIRTHDAY = dt.Rows[0]["BIRTHDAY"].ToString();
                    model.AGE = dt.Rows[0]["AGE"].ToString();
                    model.ID = dt.Rows[0]["ID"].ToString();
                    model.EDUCATION = dt.Rows[0]["EDUCATION"].ToString();
                    model.MARRIED = dt.Rows[0]["MARRIED"].ToString();
                    model.ADDRESS = dt.Rows[0]["ADDRESS"].ToString();
                    model.PHONE = dt.Rows[0]["PHONE"].ToString();
                    model.QQ = dt.Rows[0]["QQ"].ToString();
                    model.EMAIL = dt.Rows[0]["EMAIL"].ToString();
                    model.PASSWORD = dt.Rows[0]["PASSWORD"].ToString();
                }
                else
                    model.STAFFID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
                return model;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool reWritePassword(string clid, string newpassword)
        {
            string sql = "update [员工] set PASSWORD=" + "'" + newpassword + "'" + " where STAFFID='" + clid + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                int rows = DBHelperSQL.Execute(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        
        #region 获取职位对应的ID
        public string getPostIDbyPostName(string postName)
        {
            string sql = "select POSTID from [职位] where MC='" + postName + "'";
            string postid = "";
            try
            {

                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    postid = dt.Rows[0]["POSTID"].ToString();

                }
                else
                    postid = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return postid;
        }
        #endregion
        #region 获取职位对应的ID
        public string getPostNamebyPostID(string postID)
        {
            string sql = "select MC from [职位] where POSTID='" + postID + "'";
            string postid = "";
            try
            {

                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    postid = dt.Rows[0]["MC"].ToString();

                }
                else
                    postid = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return postid;
        }
        #endregion
        #region 获取职位对应的权限
        public postModel getPostQx(string postID)
        {
            SYS_MODEL.postModel model = new SYS_MODEL.postModel();
            string sql = "select * from [职位] where POSTID='"+postID+"'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.POSTID = dt.Rows[0]["POSTID"].ToString();
                    model.MC = dt.Rows[0]["MC"].ToString();
                    model.POSTRIGHT = dt.Rows[0]["POSTRIGHT"].ToString();
                    model.LOGINQX = dt.Rows[0]["LOGINQX"].ToString();
                    model.GONGWEIQX = dt.Rows[0]["GONGWEIQX"].ToString();
                    model.PRINTQX = dt.Rows[0]["PRINTQX"].ToString();
                    model.SETTINGSQX = dt.Rows[0]["SETTINGSQX"].ToString();
                    model.CHECKQX = dt.Rows[0]["CHECKQX"].ToString();
                    
                }
                else
                    model.POSTID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取某一品牌车的所有型号
        public DataTable getClxhbySB(string clsb)
        {
            string sql = "select CLXH from O_VEHICLES where CLSB like '*" + clsb + "*'" + " or CLSB LIKE '" + clsb + "'" + " or CLSB LIKE '" + clsb + "*'" + " or CLSB LIKE '*" + clsb + "'";
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #region 获取某一型号车的相关信息
        public vehicleInfModel getVehicleInfbyClxh(string clxh)
        {
            vehicleInfModel model = new vehicleInfModel();
            string sql = "select * from O_VEHICLES where clxh = '" + clxh + "'";
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.ID = int.Parse(dt.Rows[0]["ID"].ToString());
                    model.CLXH = dt.Rows[0]["CLXH"].ToString();
                    model.CLMC = dt.Rows[0]["CLMC"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.FDJSCC = dt.Rows[0]["FDJSCC"].ToString();
                    model.MANUF = dt.Rows[0]["MANUF"].ToString();
                    model.CLSB = dt.Rows[0]["CLSB"].ToString();
                    model.FILENAME = dt.Rows[0]["FILENAME"].ToString();
                    model.PF = dt.Rows[0]["PF"].ToString();
                    model.CLLB = dt.Rows[0]["CLLB"].ToString();
                    model.CLZL = dt.Rows[0]["CLZL"].ToString();
                }
                else
                {
                    model.ID = -2;
                }
            }
            catch (Exception)
            {
                model.ID = -2;
            }
            return model;
        }
        #endregion
        #region 根据车牌号检查该车信息是否存在
        public bool checkCarInfIsAlive(string plateNumber,string CPYS)
        {
            string sql = "select * from [车辆信息] where CLHP='" + plateNumber + "' and CPYS='"+CPYS+"'";
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
        #region 根据车牌号检查该车信息是否存在
        public bool checkCarInfIsAlive(string plateNumber)
        {
            string sql = "select * from [车辆信息] where CLHP='" + plateNumber + "'";
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
        #region 根据车牌号获取该车信息
        public CARINF getCarInfbyPlate(string plateNumber)
        {
            DateTime a, b;
            CARINF model = new CARINF();
            string sql = "select * from [车辆信息] where CLHP='" + plateNumber + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();//1
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.CLLX = dt.Rows[0]["CLLX"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                    model.SYXZ = dt.Rows[0]["SYXZ"].ToString();
                    model.PP = dt.Rows[0]["PP"].ToString();//5
                    model.XH = dt.Rows[0]["XH"].ToString();
                    model.CLSBM = dt.Rows[0]["CLSBM"].ToString();
                    model.FDJHM = dt.Rows[0]["FDJHM"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.SCQY = dt.Rows[0]["SCQY"].ToString();//10
                    model.HDZK = dt.Rows[0]["HDZK"].ToString();
                    model.JSSZK = dt.Rows[0]["JSSZK"].ToString();
                    model.ZZL = dt.Rows[0]["ZZL"].ToString();
                    model.HDZZL = dt.Rows[0]["HDZZL"].ToString();
                    model.ZBZL = dt.Rows[0]["ZBZL"].ToString();//15
                    model.JZZL = dt.Rows[0]["JZZL"].ToString();
                    DateTime.TryParse(dt.Rows[0]["ZCRQ"].ToString(), out a);
                    if (a != null)
                        model.ZCRQ = a;
                    else
                        model.ZCRQ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["SCRQ"].ToString(), out b);
                    if (a != null)
                        model.SCRQ = b;
                    else
                        model.SCRQ = DateTime.Today;
                    model.FDJPL = dt.Rows[0]["FDJPL"].ToString();
                    model.RLZL = dt.Rows[0]["RLZL"].ToString();//20
                    model.EDGL = dt.Rows[0]["EDGL"].ToString();
                    model.EDZS = dt.Rows[0]["EDZS"].ToString();
                    model.BSQXS = dt.Rows[0]["BSQXS"].ToString();
                    model.DWS = dt.Rows[0]["DWS"].ToString();
                    model.GYFS = dt.Rows[0]["GYFS"].ToString();//25
                    model.DPFS = dt.Rows[0]["DPFS"].ToString();
                    model.JQFS = dt.Rows[0]["JQFS"].ToString();
                    model.QGS = dt.Rows[0]["QGS"].ToString();
                    model.QDXS = dt.Rows[0]["QDXS"].ToString();
                    model.CHZZ = dt.Rows[0]["CHZZ"].ToString();//30
                    model.DLSP = dt.Rows[0]["DLSP"].ToString();
                    model.SFSRL = dt.Rows[0]["SFSRL"].ToString();
                    model.JHZZ = dt.Rows[0]["JHZZ"].ToString();
                    model.OBD = dt.Rows[0]["OBD"].ToString();
                    model.DKGYYB = dt.Rows[0]["DKGYYB"].ToString();//35
                    model.LXDH = dt.Rows[0]["LXDH"].ToString();
                    model.CZDZ = dt.Rows[0]["CZDZ"].ToString();
                    model.JCFS = dt.Rows[0]["JCFS"].ToString();
                    model.JCLB = dt.Rows[0]["JCLB"].ToString();
                    model.CLZL = dt.Rows[0]["CLZL"].ToString();
                    model.SSXQ = dt.Rows[0]["SSXQ"].ToString();
                    model.SFWDZR = dt.Rows[0]["SFWDZR"].ToString();
                    model.SFYQBF = dt.Rows[0]["SFYQBF"].ToString();
                    model.FDJSCQY = dt.Rows[0]["FDJSCQY"].ToString();
                    model.QDLTQY = dt.Rows[0]["QDLTQY"].ToString();
                    model.ZXBZ = dt.Rows[0]["ZXBZ"].ToString();
                    model.RYPH = dt.Rows[0]["RYPH"].ToString();
                    if (dt.Columns.Contains("CCS"))
                        model.CCS = dt.Rows[0]["CCS"].ToString();
                    else
                        model.CCS = "2";
                }
                else
                    model.CLHP="-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;

        }
        #endregion
        #region 根据车牌号获取该车信息
        public CARINF getCarInfbyPlate(string plateNumber,string CPYS)
        {
            DateTime a, b;
            CARINF model = new CARINF();
            string sql = "select * from [车辆信息] where CLHP='" + plateNumber + "' and CPYS='"+CPYS+"'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();//1
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.HPZL = dt.Rows[0]["HPZL"].ToString();
                    model.CLLX = dt.Rows[0]["CLLX"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                    model.SYXZ = dt.Rows[0]["SYXZ"].ToString();
                    model.PP = dt.Rows[0]["PP"].ToString();//5
                    model.XH = dt.Rows[0]["XH"].ToString();
                    model.CLSBM = dt.Rows[0]["CLSBM"].ToString();
                    model.FDJHM = dt.Rows[0]["FDJHM"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.SCQY = dt.Rows[0]["SCQY"].ToString();//10
                    model.HDZK = dt.Rows[0]["HDZK"].ToString();
                    model.JSSZK = dt.Rows[0]["JSSZK"].ToString();
                    model.ZZL = dt.Rows[0]["ZZL"].ToString();
                    model.HDZZL = dt.Rows[0]["HDZZL"].ToString();
                    model.ZBZL = dt.Rows[0]["ZBZL"].ToString();//15
                    model.JZZL = dt.Rows[0]["JZZL"].ToString();
                    DateTime.TryParse(dt.Rows[0]["ZCRQ"].ToString(), out a);
                    if (a != null)
                        model.ZCRQ = a;
                    else
                        model.ZCRQ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["SCRQ"].ToString(), out b);
                    if (a != null)
                        model.SCRQ = b;
                    else
                        model.SCRQ = DateTime.Today;
                    model.FDJPL = dt.Rows[0]["FDJPL"].ToString();
                    model.RLZL = dt.Rows[0]["RLZL"].ToString();//20
                    model.EDGL = dt.Rows[0]["EDGL"].ToString();
                    model.EDZS = dt.Rows[0]["EDZS"].ToString();
                    model.BSQXS = dt.Rows[0]["BSQXS"].ToString();
                    model.DWS = dt.Rows[0]["DWS"].ToString();
                    model.GYFS = dt.Rows[0]["GYFS"].ToString();//25
                    model.DPFS = dt.Rows[0]["DPFS"].ToString();
                    model.JQFS = dt.Rows[0]["JQFS"].ToString();
                    model.QGS = dt.Rows[0]["QGS"].ToString();
                    model.QDXS = dt.Rows[0]["QDXS"].ToString();
                    model.CHZZ = dt.Rows[0]["CHZZ"].ToString();//30
                    model.DLSP = dt.Rows[0]["DLSP"].ToString();
                    model.SFSRL = dt.Rows[0]["SFSRL"].ToString();
                    model.JHZZ = dt.Rows[0]["JHZZ"].ToString();
                    model.OBD = dt.Rows[0]["OBD"].ToString();
                    model.DKGYYB = dt.Rows[0]["DKGYYB"].ToString();//35
                    model.LXDH = dt.Rows[0]["LXDH"].ToString();
                    model.CZDZ = dt.Rows[0]["CZDZ"].ToString();
                    model.JCFS = dt.Rows[0]["JCFS"].ToString();
                    model.JCLB = dt.Rows[0]["JCLB"].ToString();
                    model.CLZL = dt.Rows[0]["CLZL"].ToString();
                    model.SSXQ = dt.Rows[0]["SSXQ"].ToString();
                    model.SFWDZR = dt.Rows[0]["SFWDZR"].ToString();
                    model.SFYQBF = dt.Rows[0]["SFYQBF"].ToString();
                    model.FDJSCQY = dt.Rows[0]["FDJSCQY"].ToString();
                    model.QDLTQY = dt.Rows[0]["QDLTQY"].ToString();
                    model.ZXBZ = dt.Rows[0]["ZXBZ"].ToString();
                    model.RYPH = dt.Rows[0]["RYPH"].ToString();
                    if (dt.Columns.Contains("CCS"))
                        model.CCS = dt.Rows[0]["CCS"].ToString();
                    else
                        model.CCS = "2";
                }
                else
                    model.CLHP = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;

        }
        #endregion
        #region 保存车辆信息
        public bool saveCarInfbyPlateHN(CARINF model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [车辆信息] (");
            strSql.Append("CLHP,");//=@PZLX,");1
            strSql.Append("CPYS,");//=@JCCLPH,");2
            strSql.Append("CLLX,");//=@CLXHBH,");3
            strSql.Append("CZ,");//=@CLXHBH,");3
            strSql.Append("SYXZ,");//=@JCCS,");4
            strSql.Append("PP,");//=@CCRQ,");5
            strSql.Append("XH,");//=@DJRQ,"); 6
            strSql.Append("CLSBM,");//=@FDJH,");7
            strSql.Append("FDJHM,");//=@CJH,");8
            strSql.Append("FDJXH,");//=@CZ,");9
            strSql.Append("SCQY,");//=@CZDH,");10
            strSql.Append("HDZK,");//=@CZDZ,");11
            strSql.Append("JSSZK,");//=@LCBDS,");12
            strSql.Append("ZZL,");//=@HBBZ,");13
            strSql.Append("HDZZL,");//=@SYQK,");14
            strSql.Append("ZBZL,");//=@JCBJ,");15
            strSql.Append("JZZL,");//=@JCZT,");16
            strSql.Append("ZCRQ,");//=@QRJCFF");17
            strSql.Append("SCRQ,");//=@RYLX,");18
            strSql.Append("FDJPL,");//=@GYFF,");19
            strSql.Append("RLZL,");//=@BSXLX,");20
            strSql.Append("EDGL,");//=@CLLX,");21
            strSql.Append("EDZS,");//=@ZBZL,");22
            strSql.Append("BSQXS,");//=@ZDZZL,");23
            strSql.Append("DWS,");//=@FDJPL,");24
            strSql.Append("GYFS,");//=@FDJSCS,");25
            strSql.Append("DPFS,");//=@JCZH,");26
            strSql.Append("JQFS,");//=@DCZZ,");27
            strSql.Append("QGS,");//=@FDJEDGL,");28
            strSql.Append("QDXS,");//=@FDJEDZS,");29
            strSql.Append("CHZZ,");//=@PQGSL,");30
            strSql.Append("DLSP,");//=@PFBZ,");31
            strSql.Append("SFSRL,");//=@ZZCS,");32
            strSql.Append("JHZZ,");//=@QGS,");33
            strSql.Append("OBD,");//=@QDLQY,");34
            strSql.Append("DKGYYB,");//=@QDLQY,");35

            strSql.Append("CZDZ,");//=@QDLQY,");35
            strSql.Append("JCFS,");//=@QDLQY,");35
            strSql.Append("JCLB,");//=@QDLQY,");35
            strSql.Append("CLZL,");//=@QDLQY,");35
            strSql.Append("SSXQ,");//=@QDLQY,");35
            strSql.Append("SFWDZR,");//=@QDLQY,");35
            strSql.Append("SFYQBF,");//=@QDLQY,");35
            strSql.Append("FDJSCQY,");//=@QDLQY,");35
            strSql.Append("QDLTQY,");//=@QDLQY,");35
            strSql.Append("RYPH,");//=@QDLQY,");35
            strSql.Append("CSYS,");//=@QDLQY,");35
            strSql.Append("HPZL,");//=@QDLQY,");35
            strSql.Append("ZXBZ,");//=@QDLQY,");35

            strSql.Append("LXDH)");//=@QDLQY,");36
            strSql.Append("values (@CLHP,@CPYS,@CLLX,@CZ,@SYXZ,@PP,@XH,@CLSBM,@FDJHM,@FDJXH,@SCQY,@HDZK,@JSSZK,@ZZL,@HDZZL,@ZBZL,@JZZL,@ZCRQ,@SCRQ,@FDJPL,@RLZL,@EDGL,@EDZS,@BSQXS,@DWS,@GYFS,@DPFS,@JQFS,@QGS,@QDXS,@CHZZ,@DLSP,@SFSRL,@JHZZ,@OBD,@DKGYYB,@CZDZ,@JCFS,@JCLB,@CLZL,@SSXQ,@SFWDZR,@SFYQBF,@FDJSCQY,@QDLTQY,@RYPH,@CSYS,@HPZL,@ZXBZ,@LXDH)");
            SqlParameter[] parameters = {
					new SqlParameter("@CLHP", SqlDbType.VarChar,50),
					new SqlParameter("@CPYS", SqlDbType.VarChar,50),
					new SqlParameter("@CLLX",SqlDbType.VarChar,50),
                    new SqlParameter("@CZ",SqlDbType.VarChar,100),
					new SqlParameter("@SYXZ", SqlDbType.VarChar,50),
					new SqlParameter("@PP", SqlDbType.VarChar,50),
                    new SqlParameter("@XH", SqlDbType.VarChar,50),
					new SqlParameter("@CLSBM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJHM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJXH", SqlDbType.VarChar,50),
					new SqlParameter("@SCQY", SqlDbType.VarChar,100),
					new SqlParameter("@HDZK", SqlDbType.VarChar,50),
					new SqlParameter("@JSSZK", SqlDbType.VarChar,50),
					new SqlParameter("@ZZL", SqlDbType.VarChar,50),
					new SqlParameter("@HDZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZCRQ", SqlDbType.DateTime),
                    new SqlParameter("@SCRQ", SqlDbType.DateTime),
                    new SqlParameter("@FDJPL", SqlDbType.VarChar,50),
					new SqlParameter("@RLZL", SqlDbType.VarChar,50),
					new SqlParameter("@EDGL", SqlDbType.VarChar,50),
					new SqlParameter("@EDZS", SqlDbType.VarChar,50),
					new SqlParameter("@BSQXS",SqlDbType.VarChar,50),
					new SqlParameter("@DWS", SqlDbType.VarChar,50),
					new SqlParameter("@GYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JQFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QGS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDXS", SqlDbType.VarChar,50),
                    new SqlParameter("@CHZZ", SqlDbType.VarChar,50),
                    new SqlParameter("@DLSP", SqlDbType.VarChar,50),
					new SqlParameter("@SFSRL", SqlDbType.VarChar,50),
					new SqlParameter("@JHZZ",SqlDbType.VarChar,50),
					new SqlParameter("@OBD",SqlDbType.VarChar,50),
                    new SqlParameter("@DKGYYB",SqlDbType.VarChar,50),
                    new SqlParameter("@CZDZ",SqlDbType.VarChar,100),
                    new SqlParameter("@JCFS",SqlDbType.VarChar,50),
                    new SqlParameter("@JCLB",SqlDbType.VarChar,50),
                    new SqlParameter("@CLZL",SqlDbType.VarChar,50),
                    new SqlParameter("@SSXQ",SqlDbType.VarChar,50),
                    new SqlParameter("@SFWDZR",SqlDbType.VarChar,50),
                    new SqlParameter("@SFYQBF",SqlDbType.VarChar,50),
                    new SqlParameter("@FDJSCQY",SqlDbType.VarChar,100),
                    new SqlParameter("@QDLTQY",SqlDbType.VarChar,50),
                    new SqlParameter("@RYPH",SqlDbType.VarChar,50),
                    new SqlParameter("@CSYS",SqlDbType.VarChar,50),
                    new SqlParameter("@HPZL",SqlDbType.VarChar,50),
                    new SqlParameter("@ZXBZ",SqlDbType.VarChar,50),
                    new SqlParameter("@LXDH",SqlDbType.VarChar,50)};
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.CPYS;
            parameters[i++].Value = model.CLLX;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SYXZ;
            parameters[i++].Value = model.PP;
            parameters[i++].Value = model.XH;
            parameters[i++].Value = model.CLSBM;
            parameters[i++].Value = model.FDJHM;
            parameters[i++].Value = model.FDJXH;
            parameters[i++].Value = model.SCQY;
            parameters[i++].Value = model.HDZK;
            parameters[i++].Value = model.JSSZK;
            parameters[i++].Value = model.ZZL;
            parameters[i++].Value = model.HDZZL;
            parameters[i++].Value = model.ZBZL;
            parameters[i++].Value = model.JZZL;
            parameters[i++].Value = model.ZCRQ;
            parameters[i++].Value = model.SCRQ;
            parameters[i++].Value = model.FDJPL;
            parameters[i++].Value = model.RLZL;
            parameters[i++].Value = model.EDGL;
            parameters[i++].Value = model.EDZS;
            parameters[i++].Value = model.BSQXS;
            parameters[i++].Value = model.DWS;
            parameters[i++].Value = model.GYFS;
            parameters[i++].Value = model.DPFS;
            parameters[i++].Value = model.JQFS;
            parameters[i++].Value = model.QGS;
            parameters[i++].Value = model.QDXS;
            parameters[i++].Value = model.CHZZ;
            parameters[i++].Value = model.DLSP;
            parameters[i++].Value = model.SFSRL;
            parameters[i++].Value = model.JHZZ;
            parameters[i++].Value = model.OBD;
            parameters[i++].Value = model.DKGYYB;
            parameters[i++].Value = model.CZDZ;
            parameters[i++].Value = model.JCFS;
            parameters[i++].Value = model.JCLB;
            parameters[i++].Value = model.CLZL;
            parameters[i++].Value = model.SSXQ;
            parameters[i++].Value = model.SFWDZR;
            parameters[i++].Value = model.SFYQBF;
            parameters[i++].Value = model.FDJSCQY;
            parameters[i++].Value = model.QDLTQY;
            parameters[i++].Value = model.RYPH;
            parameters[i++].Value = model.CSYS;
            parameters[i++].Value = model.HPZL;
            parameters[i++].Value = model.ZXBZ;
            parameters[i++].Value = model.LXDH;
            try
            {
                if (checkCarInfIsAlive(model.CLHP))
                {
                    if (UpdateHN(model))
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
        #endregion
        #region 更新车辆信息
        public bool UpdateHN(CARINF model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [车辆信息] set ");
            //strSql.Append("CLHP,");//=@PZLX,");
            strSql.Append("CPYS=@CPYS,");//=@JCCLPH,");2
            strSql.Append("CLLX=@CLLX,");//=@CLXHBH,");3
            strSql.Append("CZ=@CZ,");//=@CLXHBH,");3
            strSql.Append("SYXZ=@SYXZ,");//=@JCCS,");4
            strSql.Append("PP=@PP,");//=@CCRQ,");5
            strSql.Append("XH=@XH,");//=@DJRQ,"); 6
            strSql.Append("CLSBM=@CLSBM,");//=@FDJH,");7
            strSql.Append("FDJHM=@FDJHM,");//=@CJH,");8
            strSql.Append("FDJXH=@FDJXH,");//=@CZ,");9
            strSql.Append("SCQY=@SCQY,");//=@CZDH,");10
            strSql.Append("HDZK=@HDZK,");//=@CZDZ,");11
            strSql.Append("JSSZK=@JSSZK,");//=@LCBDS,");12
            strSql.Append("ZZL=@ZZL,");//=@HBBZ,");13
            strSql.Append("HDZZL=@HDZZL,");//=@SYQK,");14
            strSql.Append("ZBZL=@ZBZL,");//=@JCBJ,");15
            strSql.Append("JZZL=@JZZL,");//=@JCZT,");16
            strSql.Append("ZCRQ=@ZCRQ,");//=@QRJCFF");17
            strSql.Append("SCRQ=@SCRQ,");//=@RYLX,");18
            strSql.Append("FDJPL=@FDJPL,");//=@GYFF,");19
            strSql.Append("RLZL=@RLZL,");//=@BSXLX,");20
            strSql.Append("EDGL=@EDGL,");//=@CLLX,");21
            strSql.Append("EDZS=@EDZS,");//=@ZBZL,");22
            strSql.Append("BSQXS=@BSQXS,");//=@ZDZZL,");23
            strSql.Append("DWS=@DWS,");//=@FDJPL,");24
            strSql.Append("GYFS=@GYFS,");//=@FDJSCS,");25
            strSql.Append("DPFS=@DPFS,");//=@JCZH,");26
            strSql.Append("JQFS=@JQFS,");//=@DCZZ,");27
            strSql.Append("QGS=@QGS,");//=@FDJEDGL,");28
            strSql.Append("QDXS=@QDXS,");//=@FDJEDZS,");29
            strSql.Append("CHZZ=@CHZZ,");//=@PQGSL,");30
            strSql.Append("DLSP=@DLSP,");//=@PFBZ,");31
            strSql.Append("SFSRL=@SFSRL,");//=@ZZCS,");32
            strSql.Append("JHZZ=@JHZZ,");//=@QGS,");33
            strSql.Append("OBD=@OBD,");//=@QDLQY,");34
            strSql.Append("DKGYYB=@DKGYYB,");//=@QDLQY,");35

            strSql.Append("CZDZ=@CZDZ,");//=@QDLQY,");35
            strSql.Append("JCFS=@JCFS,");//=@QDLQY,");35
            strSql.Append("JCLB=@JCLB,");//=@QDLQY,");35
            strSql.Append("CLZL=@CLZL,");//=@QDLQY,");35
            strSql.Append("SSXQ=@SSXQ,");//=@QDLQY,");35
            strSql.Append("SFWDZR=@SFWDZR,");//=@QDLQY,");35
            strSql.Append("SFYQBF=@SFYQBF,");//=@QDLQY,");35
            strSql.Append("FDJSCQY=@FDJSCQY,");//=@QDLQY,");35
            strSql.Append("QDLTQY=@QDLTQY,");//=@QDLQY,");35
            strSql.Append("RYPH=@RYPH,");//=@QDLQY,");35
            strSql.Append("CSYS=@CSYS,");//=@QDLQY,");35
            strSql.Append("HPZL=@HPZL,");//=@QDLQY,");35
            strSql.Append("ZXBZ=@ZXBZ,");//=@QDLQY,");35

            strSql.Append("LXDH=@LXDH");//=@QDLQY,");36
            strSql.Append(" where CLHP='" + model.CLHP + "'");
            SqlParameter[] parameters = {
					new SqlParameter("@CPYS", SqlDbType.VarChar,50),
					new SqlParameter("@CLLX",SqlDbType.VarChar,50),
                    new SqlParameter("@CZ",SqlDbType.VarChar,100),
					new SqlParameter("@SYXZ", SqlDbType.VarChar,50),
					new SqlParameter("@PP", SqlDbType.VarChar,50),
                    new SqlParameter("@XH", SqlDbType.VarChar,50),
					new SqlParameter("@CLSBM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJHM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJXH", SqlDbType.VarChar,50),
					new SqlParameter("@SCQY", SqlDbType.VarChar,100),
					new SqlParameter("@HDZK", SqlDbType.VarChar,50),
					new SqlParameter("@JSSZK", SqlDbType.VarChar,50),
					new SqlParameter("@ZZL", SqlDbType.VarChar,50),
					new SqlParameter("@HDZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZCRQ", SqlDbType.DateTime),
                    new SqlParameter("@SCRQ", SqlDbType.DateTime),
                    new SqlParameter("@FDJPL", SqlDbType.VarChar,50),
					new SqlParameter("@RLZL", SqlDbType.VarChar,50),
					new SqlParameter("@EDGL", SqlDbType.VarChar,50),
					new SqlParameter("@EDZS", SqlDbType.VarChar,50),
					new SqlParameter("@BSQXS",SqlDbType.VarChar,50),
					new SqlParameter("@DWS", SqlDbType.VarChar,50),
					new SqlParameter("@GYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JQFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QGS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDXS", SqlDbType.VarChar,50),
                    new SqlParameter("@CHZZ", SqlDbType.VarChar,50),
                    new SqlParameter("@DLSP", SqlDbType.VarChar,50),
					new SqlParameter("@SFSRL", SqlDbType.VarChar,50),
					new SqlParameter("@JHZZ",SqlDbType.VarChar,50),
					new SqlParameter("@OBD",SqlDbType.VarChar,50),
                    new SqlParameter("@DKGYYB",SqlDbType.VarChar,50),
                    new SqlParameter("@CZDZ",SqlDbType.VarChar,100),
                    new SqlParameter("@JCFS",SqlDbType.VarChar,50),
                    new SqlParameter("@JCLB",SqlDbType.VarChar,50),
                    new SqlParameter("@CLZL",SqlDbType.VarChar,50),
                    new SqlParameter("@SSXQ",SqlDbType.VarChar,50),
                    new SqlParameter("@SFWDZR",SqlDbType.VarChar,50),
                    new SqlParameter("@SFYQBF",SqlDbType.VarChar,50),
                    new SqlParameter("@FDJSCQY",SqlDbType.VarChar,100),
                    new SqlParameter("@QDLTQY",SqlDbType.VarChar,50),
                    new SqlParameter("@RYPH",SqlDbType.VarChar,50),
                    new SqlParameter("@CSYS",SqlDbType.VarChar,50),
                    new SqlParameter("@HPZL",SqlDbType.VarChar,50),
                    new SqlParameter("@ZXBZ",SqlDbType.VarChar,50),
                    new SqlParameter("@LXDH",SqlDbType.VarChar,50)};
            parameters[i++].Value = model.CPYS;
            parameters[i++].Value = model.CLLX;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SYXZ;
            parameters[i++].Value = model.PP;
            parameters[i++].Value = model.XH;
            parameters[i++].Value = model.CLSBM;
            parameters[i++].Value = model.FDJHM;
            parameters[i++].Value = model.FDJXH;
            parameters[i++].Value = model.SCQY;
            parameters[i++].Value = model.HDZK;
            parameters[i++].Value = model.JSSZK;
            parameters[i++].Value = model.ZZL;
            parameters[i++].Value = model.HDZZL;
            parameters[i++].Value = model.ZBZL;
            parameters[i++].Value = model.JZZL;
            parameters[i++].Value = model.ZCRQ;
            parameters[i++].Value = model.SCRQ;
            parameters[i++].Value = model.FDJPL;
            parameters[i++].Value = model.RLZL;
            parameters[i++].Value = model.EDGL;
            parameters[i++].Value = model.EDZS;
            parameters[i++].Value = model.BSQXS;
            parameters[i++].Value = model.DWS;
            parameters[i++].Value = model.GYFS;
            parameters[i++].Value = model.DPFS;
            parameters[i++].Value = model.JQFS;
            parameters[i++].Value = model.QGS;
            parameters[i++].Value = model.QDXS;
            parameters[i++].Value = model.CHZZ;
            parameters[i++].Value = model.DLSP;
            parameters[i++].Value = model.SFSRL;
            parameters[i++].Value = model.JHZZ;
            parameters[i++].Value = model.OBD;
            parameters[i++].Value = model.DKGYYB;
            parameters[i++].Value = model.CZDZ;
            parameters[i++].Value = model.JCFS;
            parameters[i++].Value = model.JCLB;
            parameters[i++].Value = model.CLZL;
            parameters[i++].Value = model.SSXQ;
            parameters[i++].Value = model.SFWDZR;
            parameters[i++].Value = model.SFYQBF;
            parameters[i++].Value = model.FDJSCQY;
            parameters[i++].Value = model.QDLTQY;
            parameters[i++].Value = model.RYPH;
            parameters[i++].Value = model.CSYS;
            parameters[i++].Value = model.HPZL;
            parameters[i++].Value = model.ZXBZ;
            parameters[i++].Value = model.LXDH;
            try
            {
                int rows = DBHelperSQL.Execute(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 保存车辆信息
        public bool saveCarInfbyPlate(CARINF model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [车辆信息] (");
            strSql.Append("CLHP,");//=@PZLX,");1
            strSql.Append("CPYS,");//=@JCCLPH,");2
            strSql.Append("CLLX,");//=@CLXHBH,");3
            strSql.Append("CZ,");//=@CLXHBH,");3
            strSql.Append("SYXZ,");//=@JCCS,");4
            strSql.Append("PP,");//=@CCRQ,");5
            strSql.Append("XH,");//=@DJRQ,"); 6
            strSql.Append("CLSBM,");//=@FDJH,");7
            strSql.Append("FDJHM,");//=@CJH,");8
            strSql.Append("FDJXH,");//=@CZ,");9
            strSql.Append("SCQY,");//=@CZDH,");10
            strSql.Append("HDZK,");//=@CZDZ,");11
            strSql.Append("JSSZK,");//=@LCBDS,");12
            strSql.Append("ZZL,");//=@HBBZ,");13
            strSql.Append("HDZZL,");//=@SYQK,");14
            strSql.Append("ZBZL,");//=@JCBJ,");15
            strSql.Append("JZZL,");//=@JCZT,");16
            strSql.Append("ZCRQ,");//=@QRJCFF");17
            strSql.Append("SCRQ,");//=@RYLX,");18
            strSql.Append("FDJPL,");//=@GYFF,");19
            strSql.Append("RLZL,");//=@BSXLX,");20
            strSql.Append("EDGL,");//=@CLLX,");21
            strSql.Append("EDZS,");//=@ZBZL,");22
            strSql.Append("BSQXS,");//=@ZDZZL,");23
            strSql.Append("DWS,");//=@FDJPL,");24
            strSql.Append("GYFS,");//=@FDJSCS,");25
            strSql.Append("DPFS,");//=@JCZH,");26
            strSql.Append("JQFS,");//=@DCZZ,");27
            strSql.Append("QGS,");//=@FDJEDGL,");28
            strSql.Append("QDXS,");//=@FDJEDZS,");29
            strSql.Append("CHZZ,");//=@PQGSL,");30
            strSql.Append("DLSP,");//=@PFBZ,");31
            strSql.Append("SFSRL,");//=@ZZCS,");32
            strSql.Append("JHZZ,");//=@QGS,");33
            strSql.Append("OBD,");//=@QDLQY,");34
            strSql.Append("DKGYYB,");//=@QDLQY,");35

            strSql.Append("CZDZ,");//=@QDLQY,");35
            strSql.Append("JCFS,");//=@QDLQY,");35
            strSql.Append("JCLB,");//=@QDLQY,");35
            strSql.Append("CLZL,");//=@QDLQY,");35
            strSql.Append("SSXQ,");//=@QDLQY,");35
            strSql.Append("SFWDZR,");//=@QDLQY,");35
            strSql.Append("SFYQBF,");//=@QDLQY,");35
            strSql.Append("FDJSCQY,");//=@QDLQY,");35
            strSql.Append("QDLTQY,");//=@QDLQY,");35
            strSql.Append("RYPH,");//=@QDLQY,");35
            strSql.Append("ZXBZ,");//=@QDLQY,");35
            strSql.Append("HPZL,");//=@QDLQY,");35
            strSql.Append("CCS,");//=@QDLQY,");35

            strSql.Append("LXDH)");//=@QDLQY,");36
            strSql.Append("values (@CLHP,@CPYS,@CLLX,@CZ,@SYXZ,@PP,@XH,@CLSBM,@FDJHM,@FDJXH,@SCQY,@HDZK,@JSSZK,@ZZL,@HDZZL,@ZBZL,@JZZL,@ZCRQ,@SCRQ,@FDJPL,@RLZL,@EDGL,@EDZS,@BSQXS,@DWS,@GYFS,@DPFS,@JQFS,@QGS,@QDXS,@CHZZ,@DLSP,@SFSRL,@JHZZ,@OBD,@DKGYYB,@CZDZ,@JCFS,@JCLB,@CLZL,@SSXQ,@SFWDZR,@SFYQBF,@FDJSCQY,@QDLTQY,@RYPH,@ZXBZ,@HPZL,@CCS,@LXDH)");
            SqlParameter[] parameters = {
                    new SqlParameter("@CLHP", SqlDbType.VarChar,50),
                    new SqlParameter("@CPYS", SqlDbType.VarChar,50),
                    new SqlParameter("@CLLX",SqlDbType.VarChar,50),
                    new SqlParameter("@CZ",SqlDbType.VarChar,100),
                    new SqlParameter("@SYXZ", SqlDbType.VarChar,50),
                    new SqlParameter("@PP", SqlDbType.VarChar,50),
                    new SqlParameter("@XH", SqlDbType.VarChar,50),
                    new SqlParameter("@CLSBM", SqlDbType.VarChar,50),
                    new SqlParameter("@FDJHM", SqlDbType.VarChar,50),
                    new SqlParameter("@FDJXH", SqlDbType.VarChar,50),
                    new SqlParameter("@SCQY", SqlDbType.VarChar,100),
                    new SqlParameter("@HDZK", SqlDbType.VarChar,50),
                    new SqlParameter("@JSSZK", SqlDbType.VarChar,50),
                    new SqlParameter("@ZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@HDZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZCRQ", SqlDbType.DateTime),
                    new SqlParameter("@SCRQ", SqlDbType.DateTime),
                    new SqlParameter("@FDJPL", SqlDbType.VarChar,50),
                    new SqlParameter("@RLZL", SqlDbType.VarChar,50),
                    new SqlParameter("@EDGL", SqlDbType.VarChar,50),
                    new SqlParameter("@EDZS", SqlDbType.VarChar,50),
                    new SqlParameter("@BSQXS",SqlDbType.VarChar,50),
                    new SqlParameter("@DWS", SqlDbType.VarChar,50),
                    new SqlParameter("@GYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JQFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QGS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDXS", SqlDbType.VarChar,50),
                    new SqlParameter("@CHZZ", SqlDbType.VarChar,50),
                    new SqlParameter("@DLSP", SqlDbType.VarChar,50),
                    new SqlParameter("@SFSRL", SqlDbType.VarChar,50),
                    new SqlParameter("@JHZZ",SqlDbType.VarChar,50),
                    new SqlParameter("@OBD",SqlDbType.VarChar,50),
                    new SqlParameter("@DKGYYB",SqlDbType.VarChar,50),
                    new SqlParameter("@CZDZ",SqlDbType.VarChar,100),
                    new SqlParameter("@JCFS",SqlDbType.VarChar,50),
                    new SqlParameter("@JCLB",SqlDbType.VarChar,50),
                    new SqlParameter("@CLZL",SqlDbType.VarChar,50),
                    new SqlParameter("@SSXQ",SqlDbType.VarChar,50),
                    new SqlParameter("@SFWDZR",SqlDbType.VarChar,50),
                    new SqlParameter("@SFYQBF",SqlDbType.VarChar,50),
                    new SqlParameter("@FDJSCQY",SqlDbType.VarChar,100),
                    new SqlParameter("@QDLTQY",SqlDbType.VarChar,50),
                    new SqlParameter("@RYPH",SqlDbType.VarChar,50),
                    new SqlParameter("@ZXBZ",SqlDbType.VarChar,50),
                    new SqlParameter("@HPZL",SqlDbType.VarChar,50),
                    new SqlParameter("@CCS",SqlDbType.VarChar,50),
                    new SqlParameter("@LXDH",SqlDbType.VarChar,50)};
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.CPYS;
            parameters[i++].Value = model.CLLX;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SYXZ;
            parameters[i++].Value = model.PP;
            parameters[i++].Value = model.XH;
            parameters[i++].Value = model.CLSBM;
            parameters[i++].Value = model.FDJHM;
            parameters[i++].Value = model.FDJXH;
            parameters[i++].Value = model.SCQY;
            parameters[i++].Value = model.HDZK;
            parameters[i++].Value = model.JSSZK;
            parameters[i++].Value = model.ZZL;
            parameters[i++].Value = model.HDZZL;
            parameters[i++].Value = model.ZBZL;
            parameters[i++].Value = model.JZZL;
            parameters[i++].Value = model.ZCRQ;
            parameters[i++].Value = model.SCRQ;
            parameters[i++].Value = model.FDJPL;
            parameters[i++].Value = model.RLZL;
            parameters[i++].Value = model.EDGL;
            parameters[i++].Value = model.EDZS;
            parameters[i++].Value = model.BSQXS;
            parameters[i++].Value = model.DWS;
            parameters[i++].Value = model.GYFS;
            parameters[i++].Value = model.DPFS;
            parameters[i++].Value = model.JQFS;
            parameters[i++].Value = model.QGS;
            parameters[i++].Value = model.QDXS;
            parameters[i++].Value = model.CHZZ;
            parameters[i++].Value = model.DLSP;
            parameters[i++].Value = model.SFSRL;
            parameters[i++].Value = model.JHZZ;
            parameters[i++].Value = model.OBD;
            parameters[i++].Value = model.DKGYYB;
            parameters[i++].Value = model.CZDZ;
            parameters[i++].Value = model.JCFS;
            parameters[i++].Value = model.JCLB;
            parameters[i++].Value = model.CLZL;
            parameters[i++].Value = model.SSXQ;
            parameters[i++].Value = model.SFWDZR;
            parameters[i++].Value = model.SFYQBF;
            parameters[i++].Value = model.FDJSCQY;
            parameters[i++].Value = model.QDLTQY;
            parameters[i++].Value = model.RYPH;
            parameters[i++].Value = model.ZXBZ;
            parameters[i++].Value = model.HPZL;
            parameters[i++].Value = model.CCS;
            parameters[i++].Value = model.LXDH;
            try
            {
                if (checkCarInfIsAlive(model.CLHP))
                {
                    if (Update(model))
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
        #endregion
        #region 更新车辆信息
        public bool Update(CARINF model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [车辆信息] set ");
            //strSql.Append("CLHP,");//=@PZLX,");
            strSql.Append("CPYS=@CPYS,");//=@JCCLPH,");2
            strSql.Append("CLLX=@CLLX,");//=@CLXHBH,");3
            strSql.Append("CZ=@CZ,");//=@CLXHBH,");3
            strSql.Append("SYXZ=@SYXZ,");//=@JCCS,");4
            strSql.Append("PP=@PP,");//=@CCRQ,");5
            strSql.Append("XH=@XH,");//=@DJRQ,"); 6
            strSql.Append("CLSBM=@CLSBM,");//=@FDJH,");7
            strSql.Append("FDJHM=@FDJHM,");//=@CJH,");8
            strSql.Append("FDJXH=@FDJXH,");//=@CZ,");9
            strSql.Append("SCQY=@SCQY,");//=@CZDH,");10
            strSql.Append("HDZK=@HDZK,");//=@CZDZ,");11
            strSql.Append("JSSZK=@JSSZK,");//=@LCBDS,");12
            strSql.Append("ZZL=@ZZL,");//=@HBBZ,");13
            strSql.Append("HDZZL=@HDZZL,");//=@SYQK,");14
            strSql.Append("ZBZL=@ZBZL,");//=@JCBJ,");15
            strSql.Append("JZZL=@JZZL,");//=@JCZT,");16
            strSql.Append("ZCRQ=@ZCRQ,");//=@QRJCFF");17
            strSql.Append("SCRQ=@SCRQ,");//=@RYLX,");18
            strSql.Append("FDJPL=@FDJPL,");//=@GYFF,");19
            strSql.Append("RLZL=@RLZL,");//=@BSXLX,");20
            strSql.Append("EDGL=@EDGL,");//=@CLLX,");21
            strSql.Append("EDZS=@EDZS,");//=@ZBZL,");22
            strSql.Append("BSQXS=@BSQXS,");//=@ZDZZL,");23
            strSql.Append("DWS=@DWS,");//=@FDJPL,");24
            strSql.Append("GYFS=@GYFS,");//=@FDJSCS,");25
            strSql.Append("DPFS=@DPFS,");//=@JCZH,");26
            strSql.Append("JQFS=@JQFS,");//=@DCZZ,");27
            strSql.Append("QGS=@QGS,");//=@FDJEDGL,");28
            strSql.Append("QDXS=@QDXS,");//=@FDJEDZS,");29
            strSql.Append("CHZZ=@CHZZ,");//=@PQGSL,");30
            strSql.Append("DLSP=@DLSP,");//=@PFBZ,");31
            strSql.Append("SFSRL=@SFSRL,");//=@ZZCS,");32
            strSql.Append("JHZZ=@JHZZ,");//=@QGS,");33
            strSql.Append("OBD=@OBD,");//=@QDLQY,");34
            strSql.Append("DKGYYB=@DKGYYB,");//=@QDLQY,");35

            strSql.Append("CZDZ=@CZDZ,");//=@QDLQY,");35
            strSql.Append("JCFS=@JCFS,");//=@QDLQY,");35
            strSql.Append("JCLB=@JCLB,");//=@QDLQY,");35
            strSql.Append("CLZL=@CLZL,");//=@QDLQY,");35
            strSql.Append("SSXQ=@SSXQ,");//=@QDLQY,");35
            strSql.Append("SFWDZR=@SFWDZR,");//=@QDLQY,");35
            strSql.Append("SFYQBF=@SFYQBF,");//=@QDLQY,");35
            strSql.Append("FDJSCQY=@FDJSCQY,");//=@QDLQY,");35
            strSql.Append("QDLTQY=@QDLTQY,");//=@QDLQY,");35
            strSql.Append("RYPH=@RYPH,");//=@QDLQY,");35
            strSql.Append("ZXBZ=@ZXBZ,");//=@QDLQY,");35
            strSql.Append("HPZL=@HPZL,");//=@QDLQY,");35
            strSql.Append("CCS=@CCS,");//=@QDLQY,");35

            strSql.Append("LXDH=@LXDH");//=@QDLQY,");36
            strSql.Append(" where CLHP='" + model.CLHP + "'");
            SqlParameter[] parameters = {
                    new SqlParameter("@CPYS", SqlDbType.VarChar,50),
                    new SqlParameter("@CLLX",SqlDbType.VarChar,50),
                    new SqlParameter("@CZ",SqlDbType.VarChar,100),
                    new SqlParameter("@SYXZ", SqlDbType.VarChar,50),
                    new SqlParameter("@PP", SqlDbType.VarChar,50),
                    new SqlParameter("@XH", SqlDbType.VarChar,50),
                    new SqlParameter("@CLSBM", SqlDbType.VarChar,50),
                    new SqlParameter("@FDJHM", SqlDbType.VarChar,50),
                    new SqlParameter("@FDJXH", SqlDbType.VarChar,50),
                    new SqlParameter("@SCQY", SqlDbType.VarChar,100),
                    new SqlParameter("@HDZK", SqlDbType.VarChar,50),
                    new SqlParameter("@JSSZK", SqlDbType.VarChar,50),
                    new SqlParameter("@ZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@HDZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZCRQ", SqlDbType.DateTime),
                    new SqlParameter("@SCRQ", SqlDbType.DateTime),
                    new SqlParameter("@FDJPL", SqlDbType.VarChar,50),
                    new SqlParameter("@RLZL", SqlDbType.VarChar,50),
                    new SqlParameter("@EDGL", SqlDbType.VarChar,50),
                    new SqlParameter("@EDZS", SqlDbType.VarChar,50),
                    new SqlParameter("@BSQXS",SqlDbType.VarChar,50),
                    new SqlParameter("@DWS", SqlDbType.VarChar,50),
                    new SqlParameter("@GYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JQFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QGS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDXS", SqlDbType.VarChar,50),
                    new SqlParameter("@CHZZ", SqlDbType.VarChar,50),
                    new SqlParameter("@DLSP", SqlDbType.VarChar,50),
                    new SqlParameter("@SFSRL", SqlDbType.VarChar,50),
                    new SqlParameter("@JHZZ",SqlDbType.VarChar,50),
                    new SqlParameter("@OBD",SqlDbType.VarChar,50),
                    new SqlParameter("@DKGYYB",SqlDbType.VarChar,50),
                    new SqlParameter("@CZDZ",SqlDbType.VarChar,100),
                    new SqlParameter("@JCFS",SqlDbType.VarChar,50),
                    new SqlParameter("@JCLB",SqlDbType.VarChar,50),
                    new SqlParameter("@CLZL",SqlDbType.VarChar,50),
                    new SqlParameter("@SSXQ",SqlDbType.VarChar,50),
                    new SqlParameter("@SFWDZR",SqlDbType.VarChar,50),
                    new SqlParameter("@SFYQBF",SqlDbType.VarChar,50),
                    new SqlParameter("@FDJSCQY",SqlDbType.VarChar,100),
                    new SqlParameter("@QDLTQY",SqlDbType.VarChar,50),
                    new SqlParameter("@RYPH",SqlDbType.VarChar,50),
                    new SqlParameter("@ZXBZ",SqlDbType.VarChar,50),
                    new SqlParameter("@HPZL",SqlDbType.VarChar,50),
                    new SqlParameter("@CCS",SqlDbType.VarChar,50),
                    new SqlParameter("@LXDH",SqlDbType.VarChar,50)};
            parameters[i++].Value = model.CPYS;
            parameters[i++].Value = model.CLLX;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SYXZ;
            parameters[i++].Value = model.PP;
            parameters[i++].Value = model.XH;
            parameters[i++].Value = model.CLSBM;
            parameters[i++].Value = model.FDJHM;
            parameters[i++].Value = model.FDJXH;
            parameters[i++].Value = model.SCQY;
            parameters[i++].Value = model.HDZK;
            parameters[i++].Value = model.JSSZK;
            parameters[i++].Value = model.ZZL;
            parameters[i++].Value = model.HDZZL;
            parameters[i++].Value = model.ZBZL;
            parameters[i++].Value = model.JZZL;
            parameters[i++].Value = model.ZCRQ;
            parameters[i++].Value = model.SCRQ;
            parameters[i++].Value = model.FDJPL;
            parameters[i++].Value = model.RLZL;
            parameters[i++].Value = model.EDGL;
            parameters[i++].Value = model.EDZS;
            parameters[i++].Value = model.BSQXS;
            parameters[i++].Value = model.DWS;
            parameters[i++].Value = model.GYFS;
            parameters[i++].Value = model.DPFS;
            parameters[i++].Value = model.JQFS;
            parameters[i++].Value = model.QGS;
            parameters[i++].Value = model.QDXS;
            parameters[i++].Value = model.CHZZ;
            parameters[i++].Value = model.DLSP;
            parameters[i++].Value = model.SFSRL;
            parameters[i++].Value = model.JHZZ;
            parameters[i++].Value = model.OBD;
            parameters[i++].Value = model.DKGYYB;
            parameters[i++].Value = model.CZDZ;
            parameters[i++].Value = model.JCFS;
            parameters[i++].Value = model.JCLB;
            parameters[i++].Value = model.CLZL;
            parameters[i++].Value = model.SSXQ;
            parameters[i++].Value = model.SFWDZR;
            parameters[i++].Value = model.SFYQBF;
            parameters[i++].Value = model.FDJSCQY;
            parameters[i++].Value = model.QDLTQY;
            parameters[i++].Value = model.RYPH;
            parameters[i++].Value = model.ZXBZ;
            parameters[i++].Value = model.HPZL;
            parameters[i++].Value = model.CCS;
            parameters[i++].Value = model.LXDH;
            try
            {
                int rows = DBHelperSQL.Execute(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 更改某一检测是否缴费
        public int setCarbjPayed(string carID, string isPayed)
        {
            string sql = "update [已检车辆信息] set SFJF=" + "'" + isPayed + "'" + " where CLID='" + carID + "'";
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
        #region 保存车辆被检信息
        public bool saveCarbj(CARDETECTED model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [已检车辆信息] (");
            strSql.Append("CLID,");//=@PZLX,");1
            strSql.Append("STATIONID,");//=@PZLX,");1
            strSql.Append("LINEID,");//=@PZLX,");1
            strSql.Append("ZXBZ,");//=@PZLX,");1
            strSql.Append("DLSJ,");//=@PZLX,");1
            strSql.Append("JCSJ,");//=@PZLX,");1
            strSql.Append("JCFF,");//=@PZLX,");1
            strSql.Append("XSLC,");//=@PZLX,");1
            strSql.Append("JCJG,");//=@PZLX,");1
            strSql.Append("JCCS,");//=@PZLX,");1
            strSql.Append("LSH,");//=@PZLX,");1
            strSql.Append("CLHP,");//=@PZLX,");1
            strSql.Append("CPYS,");//=@JCCLPH,");2
            strSql.Append("CLLX,");//=@CLXHBH,");3
            strSql.Append("CZ,");//=@CLXHBH,");3
            strSql.Append("SYXZ,");//=@JCCS,");4
            strSql.Append("PP,");//=@CCRQ,");5
            strSql.Append("XH,");//=@DJRQ,"); 6
            strSql.Append("CLSBM,");//=@FDJH,");7
            strSql.Append("FDJHM,");//=@CJH,");8
            strSql.Append("FDJXH,");//=@CZ,");9
            strSql.Append("SCQY,");//=@CZDH,");10
            strSql.Append("HDZK,");//=@CZDZ,");11
            strSql.Append("JSSZK,");//=@LCBDS,");12
            strSql.Append("ZZL,");//=@HBBZ,");13
            strSql.Append("HDZZL,");//=@SYQK,");14
            strSql.Append("ZBZL,");//=@JCBJ,");15
            strSql.Append("JZZL,");//=@JCZT,");16
            strSql.Append("ZCRQ,");//=@QRJCFF");17
            strSql.Append("SCRQ,");//=@RYLX,");18
            strSql.Append("FDJPL,");//=@GYFF,");19
            strSql.Append("RLZL,");//=@BSXLX,");20
            strSql.Append("EDGL,");//=@CLLX,");21
            strSql.Append("EDZS,");//=@ZBZL,");22
            strSql.Append("BSQXS,");//=@ZDZZL,");23
            strSql.Append("DWS,");//=@FDJPL,");24
            strSql.Append("GYFS,");//=@FDJSCS,");25
            strSql.Append("DPFS,");//=@JCZH,");26
            strSql.Append("JQFS,");//=@DCZZ,");27
            strSql.Append("QGS,");//=@FDJEDGL,");28
            strSql.Append("QDXS,");//=@FDJEDZS,");29
            strSql.Append("CHZZ,");//=@PQGSL,");30
            strSql.Append("DLSP,");//=@PFBZ,");31
            strSql.Append("SFSRL,");//=@ZZCS,");32
            strSql.Append("JHZZ,");//=@QGS,");33
            strSql.Append("OBD,");//=@QDLQY,");34
            strSql.Append("DKGYYB,");//=@QDLQY,");35
            strSql.Append("LXDH,");//=@QDLQY,");36
            strSql.Append("CZY,");//=@PZLX,");1
            strSql.Append("JSY,");//=@PZLX,");1
            strSql.Append("DLY,");//=@PZLX,");1
            strSql.Append("JCFY,");//=@PZLX,");1
            strSql.Append("SFJF,");//=@PZLX,");1
            strSql.Append("TEST,");//=@PZLX,");1
            strSql.Append("JCZMC)");//=@PZLX,");1
            strSql.Append("values (@CLID,@STATIONID,@LINEID,@ZXBZ,@DLSJ,@JCSJ,@JCFF,@XSLC,@JCJG,@JCCS,@LSH,@CLHP,@CPYS,@CLLX,@CZ,@SYXZ,@PP,@XH,@CLSBM,@FDJHM,@FDJXH,@SCQY,@HDZK,@JSSZK,@ZZL,@HDZZL,@ZBZL,@JZZL,@ZCRQ,@SCRQ,@FDJPL,@RLZL,@EDGL,@EDZS,@BSQXS,@DWS,@GYFS,@DPFS,@JQFS,@QGS,@QDXS,@CHZZ,@DLSP,@SFSRL,@JHZZ,@OBD,@DKGYYB,@LXDH,@CZY,@JSY,@DLY,@JCFY,@SFJF,@TEST,@JCZMC)");
            SqlParameter[] parameters = {
                                            new SqlParameter("@CLID", SqlDbType.VarChar,50),
                                            new SqlParameter("@STATIONID", SqlDbType.VarChar,50),
                                            new SqlParameter("@LINEID", SqlDbType.VarChar,50),
                                            new SqlParameter("@ZXBZ", SqlDbType.VarChar,50),
                                            new SqlParameter("@DLSJ", SqlDbType.DateTime),
                                            new SqlParameter("@JCSJ", SqlDbType.DateTime),
                                            new SqlParameter("@JCFF", SqlDbType.VarChar,50),
                                            new SqlParameter("@XSLC", SqlDbType.VarChar,50),
                                            new SqlParameter("@JCJG", SqlDbType.VarChar,50),
                                            new SqlParameter("@JCCS", SqlDbType.VarChar,50),
                                            new SqlParameter("@LSH", SqlDbType.VarChar,50),
					new SqlParameter("@CLHP", SqlDbType.VarChar,50),
					new SqlParameter("@CPYS", SqlDbType.VarChar,50),
					new SqlParameter("@CLLX",SqlDbType.VarChar,50),
                    new SqlParameter("@CZ",SqlDbType.VarChar,100),
					new SqlParameter("@SYXZ", SqlDbType.VarChar,50),
					new SqlParameter("@PP", SqlDbType.VarChar,50),
                    new SqlParameter("@XH", SqlDbType.VarChar,50),
					new SqlParameter("@CLSBM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJHM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJXH", SqlDbType.VarChar,50),
					new SqlParameter("@SCQY", SqlDbType.VarChar,100),
					new SqlParameter("@HDZK", SqlDbType.VarChar,50),
					new SqlParameter("@JSSZK", SqlDbType.VarChar,50),
					new SqlParameter("@ZZL", SqlDbType.VarChar,50),
					new SqlParameter("@HDZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZCRQ", SqlDbType.DateTime),
                    new SqlParameter("@SCRQ", SqlDbType.DateTime),
                    new SqlParameter("@FDJPL", SqlDbType.VarChar,50),
					new SqlParameter("@RLZL", SqlDbType.VarChar,50),
					new SqlParameter("@EDGL", SqlDbType.VarChar,50),
					new SqlParameter("@EDZS", SqlDbType.VarChar,50),
					new SqlParameter("@BSQXS",SqlDbType.VarChar,50),
					new SqlParameter("@DWS", SqlDbType.VarChar,50),
					new SqlParameter("@GYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JQFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QGS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDXS", SqlDbType.VarChar,50),
                    new SqlParameter("@CHZZ", SqlDbType.VarChar,50),
                    new SqlParameter("@DLSP", SqlDbType.VarChar,50),
					new SqlParameter("@SFSRL", SqlDbType.VarChar,50),
					new SqlParameter("@JHZZ",SqlDbType.VarChar,50),
					new SqlParameter("@OBD",SqlDbType.VarChar,50),
                    new SqlParameter("@DKGYYB",SqlDbType.VarChar,50),
                    new SqlParameter("@LXDH",SqlDbType.VarChar,50),
                                        new SqlParameter("@CZY",SqlDbType.VarChar,50),
                                        new SqlParameter("@JSY",SqlDbType.VarChar,50),
                                        new SqlParameter("@DLY",SqlDbType.VarChar,50),
                                        new SqlParameter("@JCFY",SqlDbType.VarChar,50),
                                        new SqlParameter("@SFJF",SqlDbType.VarChar,50),
                                        new SqlParameter("@TEST",SqlDbType.VarChar,50),
                                        new SqlParameter("@JCZMC",SqlDbType.VarChar,50)};
            parameters[i++].Value = model.CLID;
            parameters[i++].Value = model.STATIONID;
            parameters[i++].Value = model.LINEID;
            parameters[i++].Value = model.ZXBZ;
            parameters[i++].Value = model.DLSJ;
            parameters[i++].Value = model.JCSJ;
            parameters[i++].Value = model.JCFF;
            parameters[i++].Value = model.XSLC;
            parameters[i++].Value = model.JCJG;
            parameters[i++].Value = model.JCCS;
            parameters[i++].Value = model.LSH;
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.CPYS;
            parameters[i++].Value = model.CLLX;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SYXZ;
            parameters[i++].Value = model.PP;
            parameters[i++].Value = model.XH;
            parameters[i++].Value = model.CLSBM;
            parameters[i++].Value = model.FDJHM;
            parameters[i++].Value = model.FDJXH;
            parameters[i++].Value = model.SCQY;
            parameters[i++].Value = model.HDZK;
            parameters[i++].Value = model.JSSZK;
            parameters[i++].Value = model.ZZL;
            parameters[i++].Value = model.HDZZL;
            parameters[i++].Value = model.ZBZL;
            parameters[i++].Value = model.JZZL;
            parameters[i++].Value = model.ZCRQ;
            parameters[i++].Value = model.SCRQ;
            parameters[i++].Value = model.FDJPL;
            parameters[i++].Value = model.RLZL;
            parameters[i++].Value = model.EDGL;
            parameters[i++].Value = model.EDZS;
            parameters[i++].Value = model.BSQXS;
            parameters[i++].Value = model.DWS;
            parameters[i++].Value = model.GYFS;
            parameters[i++].Value = model.DPFS;
            parameters[i++].Value = model.JQFS;
            parameters[i++].Value = model.QGS;
            parameters[i++].Value = model.QDXS;
            parameters[i++].Value = model.CHZZ;
            parameters[i++].Value = model.DLSP;
            parameters[i++].Value = model.SFSRL;
            parameters[i++].Value = model.JHZZ;
            parameters[i++].Value = model.OBD;
            parameters[i++].Value = model.DKGYYB;
            parameters[i++].Value = model.LXDH;
            parameters[i++].Value = model.CZY;
            parameters[i++].Value = model.JSY;
            parameters[i++].Value = model.DLY;
            parameters[i++].Value = model.JCFY;
            parameters[i++].Value = model.SFJF;
            parameters[i++].Value = model.TEST;
            parameters[i++].Value = model.JCZMC;
            try
            {
                if (checkIsAlive(model.CLID,"已检车辆信息"))
                {
                    if (UpdateCarBj(model))
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
        #endregion
        #region 更新车辆被检信息
        public bool UpdateCarBj(CARDETECTED model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [已检车辆信息] set ");
            strSql.Append("DLSJ=@DLSJ");
            strSql.Append("STATIONID=@STATIONID");
            strSql.Append("LINEID=@LINEID");
            strSql.Append("ZXBZ=@ZXBZ");
            strSql.Append("JCSJ=@JCSJ");
            strSql.Append("JCFF=@JCFF");
            strSql.Append("XSLC=@XSLC");
            strSql.Append("JCJG=@JCJG");
            strSql.Append("JCCS=@JCCS");
            strSql.Append("LSH=@LSH");
            strSql.Append("CLHP=@CLHP");//=@PZLX,");
            strSql.Append("CPYS=@CPYS,");//=@JCCLPH,");2
            strSql.Append("CLLX=@CLLX,");//=@CLXHBH,");3
            strSql.Append("CZ=@CZ,");//=@CLXHBH,");3
            strSql.Append("SYXZ=@SYXZ,");//=@JCCS,");4
            strSql.Append("PP=@PP,");//=@CCRQ,");5
            strSql.Append("XH=@XH,");//=@DJRQ,"); 6
            strSql.Append("CLSBM=@CLSBM,");//=@FDJH,");7
            strSql.Append("FDJHM=@FDJHM,");//=@CJH,");8
            strSql.Append("FDJXH=@FDJXH,");//=@CZ,");9
            strSql.Append("SCQY=@SCQY,");//=@CZDH,");10
            strSql.Append("HDZK=@HDZK,");//=@CZDZ,");11
            strSql.Append("JSSZK=@JSSZK,");//=@LCBDS,");12
            strSql.Append("ZZL=@ZZL,");//=@HBBZ,");13
            strSql.Append("HDZZL=@HDZZL,");//=@SYQK,");14
            strSql.Append("ZBZL=@ZBZL,");//=@JCBJ,");15
            strSql.Append("JZZL=@JZZL,");//=@JCZT,");16
            strSql.Append("ZCRQ=@ZCRQ,");//=@QRJCFF");17
            strSql.Append("SCRQ=@SCRQ,");//=@RYLX,");18
            strSql.Append("FDJPL=@FDJPL,");//=@GYFF,");19
            strSql.Append("RLZL=@RLZL,");//=@BSXLX,");20
            strSql.Append("EDGL=@EDGL,");//=@CLLX,");21
            strSql.Append("EDZS=@EDZS,");//=@ZBZL,");22
            strSql.Append("BSQXS=@BSQXS,");//=@ZDZZL,");23
            strSql.Append("DWS=@DWS,");//=@FDJPL,");24
            strSql.Append("GYFS=@GYFS,");//=@FDJSCS,");25
            strSql.Append("DPFS=@DPFS,");//=@JCZH,");26
            strSql.Append("JQFS=@JQFS,");//=@DCZZ,");27
            strSql.Append("QGS=@QGS,");//=@FDJEDGL,");28
            strSql.Append("QDXS=@QDXS,");//=@FDJEDZS,");29
            strSql.Append("CHZZ=@CHZZ,");//=@PQGSL,");30
            strSql.Append("DLSP=@DLSP,");//=@PFBZ,");31
            strSql.Append("SFSRL=@SFSRL,");//=@ZZCS,");32
            strSql.Append("JHZZ=@JHZZ,");//=@QGS,");33
            strSql.Append("OBD=@OBD,");//=@QDLQY,");34
            strSql.Append("DKGYYB=@DKGYYB,");//=@QDLQY,");35
            strSql.Append("LXDH=@LXDH,");//=@QDLQY,");36
            strSql.Append("CZY=@CZY,");//=@QDLQY,");36
            strSql.Append("JSY=@JSY,");//=@QDLQY,");36
            strSql.Append("DLY=@DLY,");//=@QDLQY,");36
            strSql.Append("JCFY=@JCFY,");//=@QDLQY,");36
            strSql.Append("SFJF=@SFJF,");//=@QDLQY,");36
            strSql.Append("TEST=@TEST,");//=@QDLQY,");36
            strSql.Append("JCZMC=@JCZMC");//=@QDLQY,");36
            strSql.Append(" where CLID='" + model.CLID + "'");
            SqlParameter[] parameters = {
                                            new SqlParameter("@DLSJ", SqlDbType.DateTime),
                                            new SqlParameter("@STATIONID", SqlDbType.VarChar,50),
                                            new SqlParameter("@LINEID", SqlDbType.VarChar,50),
                                            new SqlParameter("@ZXBZ", SqlDbType.VarChar,50),
                                            new SqlParameter("@JCSJ", SqlDbType.DateTime),
                                            new SqlParameter("@JCFF", SqlDbType.VarChar,50),
                                            new SqlParameter("@XSLC", SqlDbType.VarChar,50),
                                            new SqlParameter("@JCJG", SqlDbType.VarChar,50),
                                            new SqlParameter("@JCCS", SqlDbType.VarChar,50),
                                            new SqlParameter("@LSH", SqlDbType.VarChar,50),
					new SqlParameter("@CLHP", SqlDbType.VarChar,50),
					new SqlParameter("@CPYS", SqlDbType.VarChar,50),
					new SqlParameter("@CLLX",SqlDbType.VarChar,50),
                    new SqlParameter("@CZ",SqlDbType.VarChar,100),
					new SqlParameter("@SYXZ", SqlDbType.VarChar,50),
					new SqlParameter("@PP", SqlDbType.VarChar,50),
                    new SqlParameter("@XH", SqlDbType.VarChar,50),
					new SqlParameter("@CLSBM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJHM", SqlDbType.VarChar,50),
					new SqlParameter("@FDJXH", SqlDbType.VarChar,50),
					new SqlParameter("@SCQY", SqlDbType.VarChar,100),
					new SqlParameter("@HDZK", SqlDbType.VarChar,50),
					new SqlParameter("@JSSZK", SqlDbType.VarChar,50),
					new SqlParameter("@ZZL", SqlDbType.VarChar,50),
					new SqlParameter("@HDZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZBZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JZZL", SqlDbType.VarChar,50),
                    new SqlParameter("@ZCRQ", SqlDbType.DateTime),
                    new SqlParameter("@SCRQ", SqlDbType.DateTime),
                    new SqlParameter("@FDJPL", SqlDbType.VarChar,50),
					new SqlParameter("@RLZL", SqlDbType.VarChar,50),
					new SqlParameter("@EDGL", SqlDbType.VarChar,50),
					new SqlParameter("@EDZS", SqlDbType.VarChar,50),
					new SqlParameter("@BSQXS",SqlDbType.VarChar,50),
					new SqlParameter("@DWS", SqlDbType.VarChar,50),
					new SqlParameter("@GYFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@JQFS", SqlDbType.VarChar,50),
                    new SqlParameter("@QGS", SqlDbType.VarChar,50),
                    new SqlParameter("@QDXS", SqlDbType.VarChar,50),
                    new SqlParameter("@CHZZ", SqlDbType.VarChar,50),
                    new SqlParameter("@DLSP", SqlDbType.VarChar,50),
					new SqlParameter("@SFSRL", SqlDbType.VarChar,50),
					new SqlParameter("@JHZZ",SqlDbType.VarChar,50),
					new SqlParameter("@OBD",SqlDbType.VarChar,50),
                    new SqlParameter("@DKGYYB",SqlDbType.VarChar,50),
                    new SqlParameter("@LXDH",SqlDbType.VarChar,50),
                                        new SqlParameter("@CZY",SqlDbType.VarChar,50),
                                        new SqlParameter("@JSY",SqlDbType.VarChar,50),
                                        new SqlParameter("@DLY",SqlDbType.VarChar,50),
                                        new SqlParameter("@JCFY",SqlDbType.VarChar,50),
                                        new SqlParameter("@SFJF",SqlDbType.VarChar,50),
                                        new SqlParameter("@TEST",SqlDbType.VarChar,50),
                                        new SqlParameter("@JCZMC",SqlDbType.VarChar,50)};
            parameters[i++].Value = model.DLSJ;
            parameters[i++].Value = model.STATIONID;
            parameters[i++].Value = model.LINEID;
            parameters[i++].Value = model.ZXBZ;
            parameters[i++].Value = model.JCSJ;
            parameters[i++].Value = model.JCFF;
            parameters[i++].Value = model.XSLC;
            parameters[i++].Value = model.JCJG;
            parameters[i++].Value = model.JCCS;
            parameters[i++].Value = model.LSH;
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.CPYS;
            parameters[i++].Value = model.CLLX;
            parameters[i++].Value = model.CZ;
            parameters[i++].Value = model.SYXZ;
            parameters[i++].Value = model.PP;
            parameters[i++].Value = model.XH;
            parameters[i++].Value = model.CLSBM;
            parameters[i++].Value = model.FDJHM;
            parameters[i++].Value = model.FDJXH;
            parameters[i++].Value = model.SCQY;
            parameters[i++].Value = model.HDZK;
            parameters[i++].Value = model.JSSZK;
            parameters[i++].Value = model.ZZL;
            parameters[i++].Value = model.HDZZL;
            parameters[i++].Value = model.ZBZL;
            parameters[i++].Value = model.JZZL;
            parameters[i++].Value = model.ZCRQ;
            parameters[i++].Value = model.SCRQ;
            parameters[i++].Value = model.FDJPL;
            parameters[i++].Value = model.RLZL;
            parameters[i++].Value = model.EDGL;
            parameters[i++].Value = model.EDZS;
            parameters[i++].Value = model.BSQXS;
            parameters[i++].Value = model.DWS;
            parameters[i++].Value = model.GYFS;
            parameters[i++].Value = model.DPFS;
            parameters[i++].Value = model.JQFS;
            parameters[i++].Value = model.QGS;
            parameters[i++].Value = model.QDXS;
            parameters[i++].Value = model.CHZZ;
            parameters[i++].Value = model.DLSP;
            parameters[i++].Value = model.SFSRL;
            parameters[i++].Value = model.JHZZ;
            parameters[i++].Value = model.OBD;
            parameters[i++].Value = model.DKGYYB;
            parameters[i++].Value = model.LXDH;
            parameters[i++].Value = model.CZY;
            parameters[i++].Value = model.JSY;
            parameters[i++].Value = model.DLY;
            parameters[i++].Value = model.JCFY;
            parameters[i++].Value = model.SFJF;
            parameters[i++].Value = model.TEST;
            parameters[i++].Value = model.JCZMC;
            try
            {
                int rows = DBHelperSQL.Execute(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 获取所有被检车辆信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllCarDetected()
        {
            string sql = "select * from [已检车辆信息] order by JCSJ desc";
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
        #region 获取所有被检车辆信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataSet getAllCarDetectedDataSet()
        {
            string sql = "select * from [已检车辆信息] order by JCSJ desc";
            DataSet dt = null;
            try
            {
                dt = DBHelperSQL.GetDataSet(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 获取该检测站的已检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限,cllx:1-轻型车，2-中型车，3-重型车，0，不限
        /// </summary>
        public DataTable getCarDetectedHasNotPrint(DateTime starttime, DateTime endtime, string result, string plate, string cz)
        {
            string sql = "";
            sql = "select * from [已检车辆信息] where SFJF='N' and";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and PDJG='不合格'"; break;
                case "1": sql += " and PDJG='合格'"; break;
                default: break;
            }
            if (plate != "0")
                sql += " and CLHP like" + "'%" + plate + "%'";
            if (cz != "0")
            {
                sql += " and CZ like '%" + cz + "%'";
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
        #region 获取该检测站的已检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限,cllx:1-轻型车，2-中型车，3-重型车，0，不限
        /// </summary>
        public DataTable getCarDetectedByPlate( DateTime starttime, DateTime endtime, string result, string plate, string cz)
        {
            string sql = "";
            sql = "select * from [已检车辆信息] where";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and PDJG='不合格'"; break;
                case "1": sql += " and PDJG='合格'"; break;
                default: break;
            }
            if (plate != "0")
                sql += " and CLHP like" + "'%" + plate + "%'";
            if (cz != "0")
            {
                sql += " and CZ like '%" + cz + "%'";
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
        
        #region 获取某一辆车的所有检测信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getCarDetectedByPlate(string lx,string plate)
        {
            string sql="";
            switch(lx)
            {
                case "当日":
                    sql = "select * from [已检车辆信息] where CLHP=" + "'" + plate + "'" + " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE()) order by JCSJ desc";
                    break;
                case "所有":
                    sql = "select * from [已检车辆信息] where CLHP="+"'"+plate+"'"+" order by JCSJ desc";
                    break;
            }
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
        #region 获取所有已检车辆信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllCarDetected(string lx)
        {
            string sql="";
            switch(lx)
            {
                case "当日":
                    sql = "select * from [已检车辆信息] where DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    //sql = "select * from [已检车辆信息] where JCSJ>="+DateTime.Now.ToShortDateString()+"order by JCSJ desc";
                    break;
                case "当月":
                    sql = "select * from [已检车辆信息] where DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "当周":
                    sql = "select * from [已检车辆信息] where DATEPART(wk, JCSJ) = DATEPART(wk, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default:
                    sql = "select * from [已检车辆信息] where DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
            }
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
        #region 获取某一辆车的所有安检检测信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getCarSafeDetectedByPlate(string lx, string plate)
        {
            string sql = "";
            switch (lx)
            {
                case "当日":
                    sql = "select * from [安检记录] where CLHP=" + "'" + plate + "'" + " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE()) order by JCSJ desc";
                    break;
                case "所有":
                    sql = "select * from [安检记录] where CLHP=" + "'" + plate + "'" + " order by JCSJ desc";
                    break;
            }
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
        #region 获取某一辆安检待检车信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public CARSAFEDETECTED getCarSafeAtWaitByPlate( string plate)
        {
            CARSAFEDETECTED model = new CARSAFEDETECTED();
            string sql = "";
            sql = "select * from [安检记录] where CLHP=" + "'" + plate + "'" + " and SFJS='N'";                    
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.DLSJ = DateTime.Parse(dt.Rows[0]["DLSJ"].ToString());
                    model.JCSJ = DateTime.Parse(dt.Rows[0]["JCSJ"].ToString());
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.SFLQ = dt.Rows[0]["SFLQ"].ToString();
                    model.SFJS = dt.Rows[0]["SFJS"].ToString();
                    model.JCJG = dt.Rows[0]["JCJG"].ToString();

                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
                return model;
            }
            catch
            {
                model.CLID = "-2";
                return model;
            }
        }
        #endregion
        #region 用CLID获取某一辆安检待检车信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public CARSAFEDETECTED getCarSafeAtWaitByCLID(string CLID)
        {
            CARSAFEDETECTED model = new CARSAFEDETECTED();
            string sql = "";
            sql = "select * from [安检记录] where CLID=" + "'" + CLID + "'";
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.DLSJ = DateTime.Parse(dt.Rows[0]["DLSJ"].ToString());
                    model.JCSJ = DateTime.Parse(dt.Rows[0]["JCSJ"].ToString());
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.SFLQ = dt.Rows[0]["SFLQ"].ToString();
                    model.SFJS = dt.Rows[0]["SFJS"].ToString();
                    model.JCJG = dt.Rows[0]["JCJG"].ToString();

                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
                return model;
            }
            catch
            {
                model.CLID = "-2";
                return model;
            }
        }
        #endregion
        #region 删除该号牌的安检待检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public bool deleteCarSafeAtWaitbyPlate(string plate)
        {
            string sql = "delete from [安检记录] where CLHP='" + plate + "' and SFJS='N'";
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
        #endregion
        #region 获取所有安检待检车信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getallCarSafeAtWait()
        {
            string sql = "";
            sql = "select * from [安检记录] where SFJS='N'";
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
        
        #region 获取所有已检车辆安检信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllCarSafeDetected(string lx)
        {
            string sql = "";
            switch (lx)
            {
                case "当日":
                    sql = "select * from [安检记录] where SFJS='Y' and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    //sql = "select * from [已检车辆信息] where JCSJ>="+DateTime.Now.ToShortDateString()+"order by JCSJ desc";
                    break;
                case "当月":
                    sql = "select * from [安检记录] where SFJS='Y' and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "当周":
                    sql = "select * from [安检记录] where SFJS='Y' and DATEPART(wk, JCSJ) = DATEPART(wk, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default:
                    sql = "select * from [安检记录] where SFJS='Y' and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
            }
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
        #region 更改某一检测安检是否领取报告单
        public int setSafeCarbjPayed(string carID, string isPayed)
        {
            string sql = "update [安检记录] set SFLQ=" + "'" + isPayed + "'" + " where CLID='" + carID + "'";
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
        #region 更改某一检测安检是否检测结束
        public int setSafeCarIsFinished(string carID, string isFinished)
        {
            string sql = "update [安检记录] set SFJS=" + "'" + isFinished + "'" + " where CLID='" + carID + "'";
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
        #region 更改某一检测安检的结果
        public int setSafeCarIsOK(string carID, string result)
        {
            string sql = "update [安检记录] set JCJG=" + "'" + result + "'" + " where CLID='" + carID + "'";
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
        #region 更改某一检测安检的检测时间
        public int setSafeCarJCSJ(string carID, DateTime datetime)
        {
            string sql = "update [安检记录] set JCSJ=" + "'" + datetime + "'" + " where CLID='" + carID + "'";
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
        #region 根据车牌号检查该车信息是否存在
        public bool checkCarSafeIsAtWait(string plateNumber)
        {
            string sql = "select * from [安检记录] where CLHP='" + plateNumber + "' and SFJS='N'";
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
        #region 保存车辆安检被检信息
        public bool saveCarSafebj(CARSAFEDETECTED model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [安检记录] (");
            strSql.Append("CLID,");//=@PZLX,");1
            strSql.Append("DLSJ,");//=@PZLX,");1
            strSql.Append("JCSJ,");//=@PZLX,");1
            strSql.Append("JCJG,");//=@PZLX,");1
            strSql.Append("JCCS,");//=@PZLX,");1
            strSql.Append("CLHP,");//=@PZLX,");1
            strSql.Append("JCFY,");//=@PZLX,");1
            strSql.Append("SFJS,");//=@PZLX,");1
            strSql.Append("SFLQ)");//=@PZLX,");1
            strSql.Append("values (@CLID,@DLSJ,@JCSJ,@JCJG,@JCCS,@CLHP,@JCFY,@SFJS,@SFLQ)");
            SqlParameter[] parameters = {
                        new SqlParameter("@CLID", SqlDbType.VarChar,50),
                        new SqlParameter("@DLSJ", SqlDbType.DateTime),
                        new SqlParameter("@JCSJ", SqlDbType.DateTime),
                        new SqlParameter("@JCJG", SqlDbType.VarChar,50),
                        new SqlParameter("@JCCS", SqlDbType.VarChar,50),
                        new SqlParameter("@CLHP", SqlDbType.VarChar,50),
                        new SqlParameter("@JCFY", SqlDbType.VarChar,50),
                        new SqlParameter("@SFJS", SqlDbType.VarChar,50),
					    new SqlParameter("@SFLQ", SqlDbType.VarChar,50)};
            parameters[i++].Value = model.CLID;
            parameters[i++].Value = model.DLSJ;
            parameters[i++].Value = model.JCSJ;
            parameters[i++].Value = model.JCJG;
            parameters[i++].Value = model.JCCS;
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.JCFY;
            parameters[i++].Value = model.SFJS;
            parameters[i++].Value = model.SFLQ;
            try
            {
                if (checkIsAlive(model.CLID, "已检车辆信息"))
                {
                    if (UpdateCarSafeBj(model))
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
        #endregion
        #region 更新车辆安检被检信息
        public bool UpdateCarSafeBj(CARSAFEDETECTED model)
        {
            int i = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [安检记录] set ");
            strSql.Append("DLSJ=@DLSJ,");
            strSql.Append("JCSJ=@JCSJ,");
            strSql.Append("JCJG=@JCJG,");
            strSql.Append("JCCS=@JCCS,");
            strSql.Append("CLHP=@CLHP,");
            strSql.Append("JCFY=@JCFY,");//=@QDLQY,");36
            strSql.Append("SFJS=@SFJS,");//=@QDLQY,");36
            strSql.Append("SJLQ=@SJLQ");
            strSql.Append(" where CLID='" + model.CLID + "'");
            SqlParameter[] parameters = {
                        new SqlParameter("@DLSJ", SqlDbType.DateTime),
                        new SqlParameter("@JCSJ", SqlDbType.DateTime),
                        new SqlParameter("@JCJG", SqlDbType.VarChar,50),
                        new SqlParameter("@JCCS", SqlDbType.VarChar,50),
                        new SqlParameter("@CLHP", SqlDbType.VarChar,50),
                        new SqlParameter("@JCFY", SqlDbType.VarChar,50),
                        new SqlParameter("@SFJS", SqlDbType.VarChar,50),
					    new SqlParameter("@SFLQ", SqlDbType.VarChar,50)};
            parameters[i++].Value = model.DLSJ;
            parameters[i++].Value = model.JCSJ;
            parameters[i++].Value = model.JCJG;
            parameters[i++].Value = model.JCCS;
            parameters[i++].Value = model.CLHP;
            parameters[i++].Value = model.JCFY;
            parameters[i++].Value = model.SFJS;
            parameters[i++].Value = model.SFLQ;
            try
            {
                int rows = DBHelperSQL.Execute(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region 根据车辆ID获取该车被检信息
        public CARDETECTED getCarbjbycarID(string carID)
        {
            DateTime a, b;
            CARDETECTED model = new CARDETECTED();
            string sql = "select * from [已检车辆信息] where CLID='" + carID + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.STATIONID = dt.Rows[0]["STATIONID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.ZXBZ = dt.Rows[0]["ZXBZ"].ToString();
                    DateTime.TryParse(dt.Rows[0]["DLSJ"].ToString(), out a);
                    if (a != null)
                        model.DLSJ = a;
                    else
                        model.DLSJ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["JCSJ"].ToString(), out b);
                    if (b != null)
                        model.JCSJ = b;
                    else
                        model.JCSJ = DateTime.Today;
                    model.JCFF = dt.Rows[0]["JCFF"].ToString();
                    model.XSLC = dt.Rows[0]["XSLC"].ToString();
                    model.JCJG = dt.Rows[0]["JCJG"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.LSH = dt.Rows[0]["LSH"].ToString();
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();//1
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.CLLX = dt.Rows[0]["CLLX"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                    model.SYXZ = dt.Rows[0]["SYXZ"].ToString();
                    model.PP = dt.Rows[0]["PP"].ToString();//5
                    model.XH = dt.Rows[0]["XH"].ToString();
                    model.CLSBM = dt.Rows[0]["CLSBM"].ToString();
                    model.FDJHM = dt.Rows[0]["FDJHM"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.SCQY = dt.Rows[0]["SCQY"].ToString();//10
                    model.HDZK = dt.Rows[0]["HDZK"].ToString();
                    model.JSSZK = dt.Rows[0]["JSSZK"].ToString();
                    model.ZZL = dt.Rows[0]["ZZL"].ToString();
                    model.HDZZL = dt.Rows[0]["HDZZL"].ToString();
                    model.ZBZL = dt.Rows[0]["ZBZL"].ToString();//15
                    model.JZZL = dt.Rows[0]["JZZL"].ToString();
                    DateTime.TryParse(dt.Rows[0]["ZCRQ"].ToString(), out a);
                    if (a != null)
                        model.ZCRQ = a;
                    else
                        model.ZCRQ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["SCRQ"].ToString(), out b);
                    if (b != null)
                        model.SCRQ = b;
                    else
                        model.SCRQ = DateTime.Today;
                    model.FDJPL = dt.Rows[0]["FDJPL"].ToString();
                    model.RLZL = dt.Rows[0]["RLZL"].ToString();//20
                    model.EDGL = dt.Rows[0]["EDGL"].ToString();
                    model.EDZS = dt.Rows[0]["EDZS"].ToString();
                    model.BSQXS = dt.Rows[0]["BSQXS"].ToString();
                    model.DWS = dt.Rows[0]["DWS"].ToString();
                    model.GYFS = dt.Rows[0]["GYFS"].ToString();//25
                    model.DPFS = dt.Rows[0]["DPFS"].ToString();
                    model.JQFS = dt.Rows[0]["JQFS"].ToString();
                    model.QGS = dt.Rows[0]["QGS"].ToString();
                    model.QDXS = dt.Rows[0]["QDXS"].ToString();
                    model.CHZZ = dt.Rows[0]["CHZZ"].ToString();//30
                    model.DLSP = dt.Rows[0]["DLSP"].ToString();
                    model.SFSRL = dt.Rows[0]["SFSRL"].ToString();
                    model.JHZZ = dt.Rows[0]["JHZZ"].ToString();
                    model.OBD = dt.Rows[0]["OBD"].ToString();
                    model.DKGYYB = dt.Rows[0]["DKGYYB"].ToString();//35
                    model.LXDH = dt.Rows[0]["LXDH"].ToString();
                    model.CZY = dt.Rows[0]["CZY"].ToString();
                    model.JSY = dt.Rows[0]["JSY"].ToString();
                    model.DLY = dt.Rows[0]["DLY"].ToString();//35
                    model.JCZMC = dt.Rows[0]["JCZMC"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.SFJF = dt.Rows[0]["SFJF"].ToString();
                    model.TEST = dt.Rows[0]["TEST"].ToString();
                    model.QDLTQY = dt.Rows[0]["QDLTQY"].ToString();
                    model.RYPH = dt.Rows[0]["RYPH"].ToString();
                    model.jcgcsj = dt.Rows[0]["JCGCSJ"].ToString();
                    model.wjy = dt.Rows[0]["WJY"].ToString();
                    if(dt.Columns.Contains("BGFFYY"))
                        model.BGFFYY = dt.Rows[0]["BGFFYY"].ToString();
                    if (dt.Columns.Contains("CCS"))
                        model.CCS = dt.Rows[0]["CCS"].ToString();
                    else
                        model.CCS = "2";
                    if (dt.Columns.Contains("JYLSH"))
                        model.JYLSH = dt.Rows[0]["JYLSH"].ToString();
                    else
                        model.JYLSH = "";
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;

        }
        #endregion

        #region 删除被检数据
        public bool deleteDatabj(string carID)
        {
            string sql = "delete from [已检车辆信息] where CLID='" + carID + "'";
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
        #endregion
        #region 获取车辆的上次检测记录
        public CARDETECTED getPreTestDatebyPlate(string clph, string hpys)
        {
            DateTime a, b;
            CARDETECTED model = new CARDETECTED();
            string sql = "select * from [已检车辆信息] where CLHP='" + clph + "' and CPYS='" + hpys + "' order by JCSJ desc";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.STATIONID = dt.Rows[0]["STATIONID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.ZXBZ = dt.Rows[0]["ZXBZ"].ToString();
                    DateTime.TryParse(dt.Rows[0]["DLSJ"].ToString(), out a);
                    if (a != null)
                        model.DLSJ = a;
                    else
                        model.DLSJ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["JCSJ"].ToString(), out b);
                    if (b != null)
                        model.JCSJ = b;
                    else
                        model.JCSJ = DateTime.Today;
                    model.JCFF = dt.Rows[0]["JCFF"].ToString();
                    model.XSLC = dt.Rows[0]["XSLC"].ToString();
                    model.JCJG = dt.Rows[0]["JCJG"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.LSH = dt.Rows[0]["LSH"].ToString();
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();//1
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.CLLX = dt.Rows[0]["CLLX"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                    model.SYXZ = dt.Rows[0]["SYXZ"].ToString();
                    model.PP = dt.Rows[0]["PP"].ToString();//5
                    model.XH = dt.Rows[0]["XH"].ToString();
                    model.CLSBM = dt.Rows[0]["CLSBM"].ToString();
                    model.FDJHM = dt.Rows[0]["FDJHM"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.SCQY = dt.Rows[0]["SCQY"].ToString();//10
                    model.HDZK = dt.Rows[0]["HDZK"].ToString();
                    model.JSSZK = dt.Rows[0]["JSSZK"].ToString();
                    model.ZZL = dt.Rows[0]["ZZL"].ToString();
                    model.HDZZL = dt.Rows[0]["HDZZL"].ToString();
                    model.ZBZL = dt.Rows[0]["ZBZL"].ToString();//15
                    model.JZZL = dt.Rows[0]["JZZL"].ToString();
                    DateTime.TryParse(dt.Rows[0]["ZCRQ"].ToString(), out a);
                    if (a != null)
                        model.ZCRQ = a;
                    else
                        model.ZCRQ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["SCRQ"].ToString(), out b);
                    if (b != null)
                        model.SCRQ = b;
                    else
                        model.SCRQ = DateTime.Today;
                    model.FDJPL = dt.Rows[0]["FDJPL"].ToString();
                    model.RLZL = dt.Rows[0]["RLZL"].ToString();//20
                    model.EDGL = dt.Rows[0]["EDGL"].ToString();
                    model.EDZS = dt.Rows[0]["EDZS"].ToString();
                    model.BSQXS = dt.Rows[0]["BSQXS"].ToString();
                    model.DWS = dt.Rows[0]["DWS"].ToString();
                    model.GYFS = dt.Rows[0]["GYFS"].ToString();//25
                    model.DPFS = dt.Rows[0]["DPFS"].ToString();
                    model.JQFS = dt.Rows[0]["JQFS"].ToString();
                    model.QGS = dt.Rows[0]["QGS"].ToString();
                    model.QDXS = dt.Rows[0]["QDXS"].ToString();
                    model.CHZZ = dt.Rows[0]["CHZZ"].ToString();//30
                    model.DLSP = dt.Rows[0]["DLSP"].ToString();
                    model.SFSRL = dt.Rows[0]["SFSRL"].ToString();
                    model.JHZZ = dt.Rows[0]["JHZZ"].ToString();
                    model.OBD = dt.Rows[0]["OBD"].ToString();
                    model.DKGYYB = dt.Rows[0]["DKGYYB"].ToString();//35
                    model.LXDH = dt.Rows[0]["LXDH"].ToString();
                    model.CZY = dt.Rows[0]["CZY"].ToString();
                    model.JSY = dt.Rows[0]["JSY"].ToString();
                    model.DLY = dt.Rows[0]["DLY"].ToString();//35
                    model.JCZMC = dt.Rows[0]["JCZMC"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.SFJF = dt.Rows[0]["SFJF"].ToString();
                    model.TEST = dt.Rows[0]["TEST"].ToString();
                    model.QDLTQY = dt.Rows[0]["QDLTQY"].ToString();
                    model.RYPH = dt.Rows[0]["RYPH"].ToString();
                    if (dt.Columns.Contains("CCS"))
                        model.CCS = dt.Rows[0]["CCS"].ToString();
                    else
                        model.CCS = "2";
                    if (dt.Columns.Contains("JYLSH"))
                        model.JYLSH = dt.Rows[0]["JYLSH"].ToString();
                    else
                        model.JYLSH = "";
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取车辆的最近一次初检检测记录
        public CARDETECTED getPreCjTestDatebyPlate(string clph, string hpys)
        {
            DateTime a, b;
            CARDETECTED model = new CARDETECTED();
            string sql = "select * from [已检车辆信息] where CLHP='" + clph + "' and CPYS='" + hpys + "' and JCCS='1' order by JCSJ desc";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.STATIONID = dt.Rows[0]["STATIONID"].ToString();
                    model.LINEID = dt.Rows[0]["LINEID"].ToString();
                    model.ZXBZ = dt.Rows[0]["ZXBZ"].ToString();
                    DateTime.TryParse(dt.Rows[0]["DLSJ"].ToString(), out a);
                    if (a != null)
                        model.DLSJ = a;
                    else
                        model.DLSJ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["JCSJ"].ToString(), out b);
                    if (b != null)
                        model.JCSJ = b;
                    else
                        model.JCSJ = DateTime.Today;
                    model.JCFF = dt.Rows[0]["JCFF"].ToString();
                    model.XSLC = dt.Rows[0]["XSLC"].ToString();
                    model.JCJG = dt.Rows[0]["JCJG"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.LSH = dt.Rows[0]["LSH"].ToString();
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();//1
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.CLLX = dt.Rows[0]["CLLX"].ToString();
                    model.CZ = dt.Rows[0]["CZ"].ToString();
                    model.SYXZ = dt.Rows[0]["SYXZ"].ToString();
                    model.PP = dt.Rows[0]["PP"].ToString();//5
                    model.XH = dt.Rows[0]["XH"].ToString();
                    model.CLSBM = dt.Rows[0]["CLSBM"].ToString();
                    model.FDJHM = dt.Rows[0]["FDJHM"].ToString();
                    model.FDJXH = dt.Rows[0]["FDJXH"].ToString();
                    model.SCQY = dt.Rows[0]["SCQY"].ToString();//10
                    model.HDZK = dt.Rows[0]["HDZK"].ToString();
                    model.JSSZK = dt.Rows[0]["JSSZK"].ToString();
                    model.ZZL = dt.Rows[0]["ZZL"].ToString();
                    model.HDZZL = dt.Rows[0]["HDZZL"].ToString();
                    model.ZBZL = dt.Rows[0]["ZBZL"].ToString();//15
                    model.JZZL = dt.Rows[0]["JZZL"].ToString();
                    DateTime.TryParse(dt.Rows[0]["ZCRQ"].ToString(), out a);
                    if (a != null)
                        model.ZCRQ = a;
                    else
                        model.ZCRQ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["SCRQ"].ToString(), out b);
                    if (b != null)
                        model.SCRQ = b;
                    else
                        model.SCRQ = DateTime.Today;
                    model.FDJPL = dt.Rows[0]["FDJPL"].ToString();
                    model.RLZL = dt.Rows[0]["RLZL"].ToString();//20
                    model.EDGL = dt.Rows[0]["EDGL"].ToString();
                    model.EDZS = dt.Rows[0]["EDZS"].ToString();
                    model.BSQXS = dt.Rows[0]["BSQXS"].ToString();
                    model.DWS = dt.Rows[0]["DWS"].ToString();
                    model.GYFS = dt.Rows[0]["GYFS"].ToString();//25
                    model.DPFS = dt.Rows[0]["DPFS"].ToString();
                    model.JQFS = dt.Rows[0]["JQFS"].ToString();
                    model.QGS = dt.Rows[0]["QGS"].ToString();
                    model.QDXS = dt.Rows[0]["QDXS"].ToString();
                    model.CHZZ = dt.Rows[0]["CHZZ"].ToString();//30
                    model.DLSP = dt.Rows[0]["DLSP"].ToString();
                    model.SFSRL = dt.Rows[0]["SFSRL"].ToString();
                    model.JHZZ = dt.Rows[0]["JHZZ"].ToString();
                    model.OBD = dt.Rows[0]["OBD"].ToString();
                    model.DKGYYB = dt.Rows[0]["DKGYYB"].ToString();//35
                    model.LXDH = dt.Rows[0]["LXDH"].ToString();
                    model.CZY = dt.Rows[0]["CZY"].ToString();
                    model.JSY = dt.Rows[0]["JSY"].ToString();
                    model.DLY = dt.Rows[0]["DLY"].ToString();//35
                    model.JCZMC = dt.Rows[0]["JCZMC"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.SFJF = dt.Rows[0]["SFJF"].ToString();
                    model.TEST = dt.Rows[0]["TEST"].ToString();
                    model.QDLTQY = dt.Rows[0]["QDLTQY"].ToString();
                    model.RYPH = dt.Rows[0]["RYPH"].ToString();
                    if (dt.Columns.Contains("CCS"))
                        model.CCS = dt.Rows[0]["CCS"].ToString();
                    else
                        model.CCS = "2";
                    if (dt.Columns.Contains("JYLSH"))
                        model.JYLSH = dt.Rows[0]["JYLSH"].ToString();
                    else
                        model.JYLSH = "";
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取车辆的上次安检检测记录
        public CARSAFEDETECTED getPreSafeTestDatebyPlate(string clph)
        {
            DateTime a, b;
            CARSAFEDETECTED model = new CARSAFEDETECTED();
            string sql = "select * from [安检记录] where CLHP='" + clph + "' and SFJS='Y' order by JCSJ desc";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    DateTime.TryParse(dt.Rows[0]["DLSJ"].ToString(), out a);
                    if (a != null)
                        model.DLSJ = a;
                    else
                        model.DLSJ = DateTime.Today;
                    DateTime.TryParse(dt.Rows[0]["JCSJ"].ToString(), out b);
                    if (b != null)
                        model.JCSJ = b;
                    else
                        model.JCSJ = DateTime.Today;
                    
                    model.JCJG = dt.Rows[0]["JCJG"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();//1
                    
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.SFLQ = dt.Rows[0]["SFLQ"].ToString();
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
        #endregion
        #region 获取该车在今年的检测次数
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public int getJccsbyClph(string clph)
        {
            string year = DateTime.Now.Year.ToString();
            int count = 0;
            string sql = "select count(*) as number from [已检车辆信息] where CLHP='" + clph + "'" + " and JCSJ>" + "'" + year + "-01-01 00:00:00.000" + "'" + " and JCSJ<" + "'" + year + "-12-31 23:59:59.000'";
            try
            {
                count = DBHelperSQL.ExecuteCount(sql);
                return count;
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 用车牌号检测该车是否已经在待检序列中
        /// <summary>
        /// 用车牌号检测该车的信息是否已经存在
        /// </summary>
        /// <param name="jcbh">汽车车牌</param>
        /// <param name="jccs">检测线编号</param>
        /// <returns>bool</returns>
        public bool checkCarIsAtWait(string jcclph)
        {
            string sql = "select * from [待检车辆] where CLHP="+"'"+jcclph+"'";
            try
            {
                if (DBHelperSQL.GetDataTable(sql, CommandType.Text).Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        
        #region 添加车辆到待检列表
        public CARATWAIT getDataWaitbyCarID(string carID)
        {
            CARATWAIT model = new CARATWAIT();
            string sql = "select * from [待检车辆] where CLID='" + carID + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.DLSJ =DateTime.Parse(dt.Rows[0]["DLSJ"].ToString());
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.JCFF = dt.Rows[0]["JCFF"].ToString();
                    model.XSLC = dt.Rows[0]["XSLC"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.CZY = dt.Rows[0]["CZY"].ToString();
                    model.JSY = dt.Rows[0]["JSY"].ToString();
                    model.DLY = dt.Rows[0]["DLY"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.TEST = dt.Rows[0]["TEST"].ToString();
                    model.JCBGBH = dt.Rows[0]["JCBGBH"].ToString();
                    model.JCGWH = dt.Rows[0]["JCGWH"].ToString();
                    model.JCZBH = dt.Rows[0]["JCZBH"].ToString();
                    model.JCRQ = DateTime.Parse(dt.Rows[0]["JCRQ"].ToString());
                    model.BGJCFFYY = dt.Rows[0]["BGJCFFYY"].ToString();
                    model.SFCS = dt.Rows[0]["SFCS"].ToString();
                    model.ECRYPT = dt.Rows[0]["ECRYPT"].ToString();
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;

        }
        public string addCarToWaitList(CARATWAIT model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [待检车辆] (");
            strSql.Append("CLID,");//=@PZLX,");
            strSql.Append("DLSJ,");//=@JCCLPH,");
            strSql.Append("CLHP,");//=@CLXHBH,");
            strSql.Append("CPYS,");//=@JCCS,");
            strSql.Append("JCFF,");//=@JCCS,");
            strSql.Append("XSLC,");
            strSql.Append("JCCS,");
            strSql.Append("CZY,");//=@JCCS,");
            strSql.Append("JSY,");
            strSql.Append("JCFY,");//=@JCCS,");
            strSql.Append("TEST,");
            strSql.Append("JCBGBH,");
            strSql.Append("JCGWH,");
            strSql.Append("JCZBH,");
            strSql.Append("JCRQ,");
            strSql.Append("BGJCFFYY,");
            strSql.Append("SFCS,");
            strSql.Append("ECRYPT,");
            strSql.Append("SFCJ,");
            strSql.Append("HPZL,");
            strSql.Append("JYLSH,");
            strSql.Append("WXBJ,");
            strSql.Append("WXCD,");
            strSql.Append("WXSJ,");
            strSql.Append("WXFY,");
            strSql.Append("GYXTXS,");
            strSql.Append("SFLJ,");
            strSql.Append("FWLX,");
            strSql.Append("ICCARDNO,");
            strSql.Append("DPFS,");
            strSql.Append("DLY)");

            strSql.Append("values (@CLID,@DLSJ,@CLHP,@CPYS,@JCFF,@XSLC,@JCCS,@CZY,@JSY,@JCFY,@TEST,@JCBGBH,@JCGWH,@JCZBH,@JCRQ,@BGJCFFYY,@SFCS,@ECRYPT,@SFCJ,@HPZL,@JYLSH,@WXBJ,@WXCD,@WXSJ,@WXFY,@GYXTXS,@SFLJ,@FWLX,@ICCARDNO,@DPFS,@DLY)");
            SqlParameter[] parameters = {
					new SqlParameter("@CLID", SqlDbType.VarChar,50),
					new SqlParameter("@DLSJ", SqlDbType.DateTime),
					new SqlParameter("@CLHP",SqlDbType.VarChar,50),
					new SqlParameter("@CPYS", SqlDbType.VarChar,50),
                    new SqlParameter("@JCFF", SqlDbType.VarChar,50),
                    new SqlParameter("@XSLC", SqlDbType.VarChar,50),
                    new SqlParameter("@JCCS", SqlDbType.VarChar,50),
                    new SqlParameter("@CZY", SqlDbType.VarChar,50),
                    new SqlParameter("@JSY", SqlDbType.VarChar,50),
                    new SqlParameter("@JCFY", SqlDbType.VarChar,50),
                    new SqlParameter("@TEST", SqlDbType.VarChar,50),
                    new SqlParameter("@JCBGBH", SqlDbType.VarChar,50),
                    new SqlParameter("@JCGWH", SqlDbType.VarChar,50),
                    new SqlParameter("@JCZBH", SqlDbType.VarChar,50),
                    new SqlParameter("@JCRQ", SqlDbType.DateTime),
                    new SqlParameter("@BGJCFFYY", SqlDbType.VarChar,100),
                    new SqlParameter("@SFCS", SqlDbType.VarChar,50),
                    new SqlParameter("@ECRYPT", SqlDbType.VarChar,100),
                    new SqlParameter("@SFCJ", SqlDbType.VarChar,50),
                    new SqlParameter("@HPZL", SqlDbType.VarChar,50),
                    new SqlParameter("@JYLSH", SqlDbType.VarChar,50),
                    new SqlParameter("@WXBJ", SqlDbType.VarChar,50),
                    new SqlParameter("@WXCD", SqlDbType.VarChar,50),
                    new SqlParameter("@WXSJ", SqlDbType.VarChar,50),
                    new SqlParameter("@WXFY", SqlDbType.VarChar,50),
                    new SqlParameter("@GYXTXS", SqlDbType.VarChar,50),
                    new SqlParameter("@SFLJ", SqlDbType.VarChar,50),
                    new SqlParameter("@FWLX", SqlDbType.VarChar,50),
                    new SqlParameter("@ICCARDNO", SqlDbType.VarChar,50),
                    new SqlParameter("@DPFS", SqlDbType.VarChar,50),
                    new SqlParameter("@DLY", SqlDbType.VarChar,50)};
            parameters[0].Value = model.CLID;
            parameters[1].Value = model.DLSJ;
            parameters[2].Value = model.CLHP;
            parameters[3].Value = model.CPYS;
            parameters[4].Value = model.JCFF;
            parameters[5].Value = model.XSLC;
            parameters[6].Value = model.JCCS;
            parameters[7].Value = model.CZY;
            parameters[8].Value = model.JSY;
            parameters[9].Value = model.JCFY;
            parameters[10].Value = model.TEST;
            parameters[11].Value = model.JCBGBH;
            parameters[12].Value = model.JCGWH;
            parameters[13].Value = model.JCZBH;
            parameters[14].Value = model.JCRQ;
            parameters[15].Value = model.BGJCFFYY;
            parameters[16].Value = model.SFCS;
            parameters[17].Value = model.ECRYPT;
            parameters[18].Value = model.SFCJ;
            parameters[19].Value = model.HPZL;
            parameters[20].Value = model.JYLSH;
            parameters[21].Value = model.WXBJ;
            parameters[22].Value = model.WXCD;
            parameters[23].Value = model.WXSJ;
            parameters[24].Value = model.WXFY;
            parameters[25].Value = model.GYXTXS;
            parameters[26].Value = model.SFLJ;
            parameters[27].Value = model.FWLX;
            parameters[28].Value = model.ICCARDNO;
            parameters[29].Value = model.DPFS;
            parameters[30].Value = model.DLY;
            try
            {
                if (checkCarIsAtWait(model.CLHP))
                {
                    return "环检添加失败，该车已在待检序列中";
                }
                else
                {
                    if (DBHelperSQL.Execute(strSql.ToString(), parameters) > 0)
                        return "环检添加成功";
                    else
                        return "环检添加失败";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
        #region 获取所有待检车辆信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllCarAtWait()
        {
            string sql = "select * from [待检车辆] order by DLSJ desc";
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
        #region 获取所有待检车辆信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllCarAtWait(string isNet)
        {
            string sql = "select * from [待检车辆] where TEST='" + isNet + "' order by DLSJ desc";
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
        #region 获取待检列表中某一辆车的检测信息
        public CARATWAIT getCarInfatWaitList(string clhp)
        {
            CARATWAIT model = new CARATWAIT();
            string sql = "select * from [待检车辆] where CLHP='" + clhp + "'";
            try
            {
                DataTable dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    model.CLID = dt.Rows[0]["CLID"].ToString();
                    model.DLSJ = DateTime.Parse(dt.Rows[0]["DLSJ"].ToString());
                    model.CLHP = dt.Rows[0]["CLHP"].ToString();
                    model.CPYS = dt.Rows[0]["CPYS"].ToString();
                    model.JCFF = dt.Rows[0]["JCFF"].ToString();
                    model.XSLC = dt.Rows[0]["XSLC"].ToString();
                    model.JCCS = dt.Rows[0]["JCCS"].ToString();
                    model.CZY = dt.Rows[0]["CZY"].ToString();
                    model.JSY = dt.Rows[0]["JSY"].ToString();
                    model.DLY = dt.Rows[0]["DLY"].ToString();
                    model.JCFY = dt.Rows[0]["JCFY"].ToString();
                    model.TEST = dt.Rows[0]["TEST"].ToString();
                    model.JCBGBH = dt.Rows[0]["JCBGBH"].ToString();
                    model.JCGWH = dt.Rows[0]["JCGWH"].ToString();
                    model.JCZBH = dt.Rows[0]["JCZBH"].ToString();
                    model.JCRQ = DateTime.Parse(dt.Rows[0]["JCRQ"].ToString());
                    model.BGJCFFYY = dt.Rows[0]["BGJCFFYY"].ToString();
                    model.SFCS = dt.Rows[0]["SFCS"].ToString();
                    model.ECRYPT = dt.Rows[0]["ECRYPT"].ToString();
                }
                else
                    model.CLID = "-2";       //当服务器上没有找到本线时，本线编号置为-2，以免为0
            }
            catch (Exception)
            {
                throw;
            }
            return model;

        }
        #endregion
        #region 获取该号牌待检车的车辆ID
        public string getCarIDatWaitbyPlate(string plate)
        {
            string sql = "select CLID from [待检车辆] where CLHP='"+plate+"'";
            DataTable dt = null;
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["CLID"].ToString();
                }
                else
                {
                    return "-2";
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region 删除该号牌的待检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public bool deleteCarAtWaitbyPlate(string plate)
        {
            string sql = "delete from [待检车辆] where CLHP='"+plate+"'";
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
        #endregion
        #region 检测数据是否存在
        public bool checkIsAlive(string carID, string listName)
        {
            string sql = "select * from [" + listName + "] where CLID=" + "'" + carID + "'";
            try
            {
                if (DBHelperSQL.GetDataTable(sql, CommandType.Text).Rows.Count > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 获取该检测站的已检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天
        /// </summary>
        public int getStationLineDetectedCount(string stationid,string lineid,string lx)
        {
            string sql = "";
            if (lineid == "0")
                sql = "select count(*) as number from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";
            else
                sql = "select count(*) as number from [已检车辆信息]" +" where STATIONID=" + "'" + stationid + "' and LINEID='" + lineid + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, JCSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
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

        #region 获取该检测站的已检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getStationLineDetectedCount(string stationid, string lineid, string lx,string plate,string jcff,string result)
        {
            string sql = "";
            if (lineid == "0")
                sql = "select count(*) as number from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";//+ "' and CLHP LIKE '" + plate + "'";
            else
                sql = "select count(*) as number from [已检车辆信息]" + " where STATIONID='"  + stationid  +"' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, JCSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            if (jcff!="0")
            {
                sql += " and JCFF='"+jcff+"'";
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
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
        #region 获取该检测站的已检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getStationLineDetectedCount(string stationid, string lineid, DateTime starttime,DateTime endtime, string plate, string jcff, string result)
        {
            string sql = "";
            if (lineid == "0")
                sql = "select count(*) as number from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";//+ "' and CLHP LIKE '" + plate + "'";
            else
                sql = "select count(*) as number from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
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
        #region 获取该检测站的安检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getSafeDetectedCount(string lx, string plate, string result,string isFinished)
        {
            string sql = "";
            sql = "select count(*) as number from [安检记录]" + " where SFJS=" + "'" + isFinished + "'";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, JCSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
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
        #region 获取该检测站的安检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getSafeWaitCount(string lx, string plate, string result, string isFinished)
        {
            string sql = "";
            sql = "select count(*) as number from [安检记录]" + " where SFJS=" + "'" + isFinished + "'";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, DLSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, DLSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
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
        #region 获取该检测站的安检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// starttime:开始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getSafeDetectedCount(DateTime starttime,DateTime endtime, string plate, string result, string isFinished)
        {
            string sql = "";
            sql = "select count(*) as number from [安检记录]" + " where SFJS=" + "'" + isFinished + "'";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
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
        #region 获取该检测站的安检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// starttime:开始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getSafeWiatCount(DateTime starttime, DateTime endtime, string plate, string result, string isFinished)
        {
            string sql = "";
            sql = "select count(*) as number from [安检记录]" + " where SFJS=" + "'" + isFinished + "'";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " and DLSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and DLSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
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
        #region 获取所有参检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getDetectedCarMoney(string stationid, string lineid, string lx, string plate, string jcff)
        {
            DataTable dt = null;
            string sql = "";
            if (lineid == "0")
                sql = "select JCFY from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";//+ "' and CLHP LIKE '" + plate + "'";
            else
                sql = "select JCFY from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, JCSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
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
        #region 获取所有参检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getDetectedCarMoney(string stationid, string lineid, DateTime starttime,DateTime endtime, string plate, string jcff)
        {
            DataTable dt = null;
            string sql = "";
            if (lineid == "0")
                sql = "select JCFY from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";//+ "' and CLHP LIKE '" + plate + "'";
            else
                sql = "select JCFY from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
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
        public DataTable getWaitedCarMoney( string lx, string plate, string jcff)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select JCFY from [待检车辆]";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " where DATEPART(yy, DLSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " where DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " where DATEPART(dd, DLSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
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
        public DataTable getWaitedCarMoney(DateTime starttime,DateTime endtime, string plate, string jcff)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select JCFY from [待检车辆]";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " and DLSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and DLSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
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
        #region 获取所有安检参检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getSafeDetectedCarMoney(string lx, string plate)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select JCFY from [安检记录]" + " where SFJS='Y'";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, DLSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, DLSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
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
        #region 获取所有安检待检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getSafeWaitedCarMoney(string lx, string plate)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select JCFY from [安检记录] whrer SFJS='N'";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " where DATEPART(yy, DLSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " where DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " where DATEPART(dd, DLSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
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
        #region 获取所有安检参检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getSafeDetectedCarMoney(DateTime starttime,DateTime endtime, string plate)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select JCFY from [安检记录]" + " where SFJS='Y'";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " and DLSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and DLSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            
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
        #region 获取所有安检待检车的费用信息
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataTable getSafeWaitedCarMoney(DateTime starttime,DateTime endtime, string plate)
        {
            DataTable dt = null;
            string sql = "";
            sql = "select JCFY from [安检记录] whrer SFJS='N'";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " and DLSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and DLSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            
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
        #region 获取该检测站的待检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getStationAtWaitCount( string lx, string plate, string jcff)
        {
            string sql = "";
            sql = "select count(*) as number from [待检车辆]";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    if (jcff != "0")
                    {
                        sql += " where JCFF='" + jcff + "'";
                    }
                    break;
                case "1":
                    sql += " where DATEPART(yy, DLSJ)=DATEPART(yy, GETDATE())";
                    if (jcff != "0")
                    {
                        sql += " and JCFF='" + jcff + "'";
                    }
                    break;
                case "2":
                    sql += " where DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    if (jcff != "0")
                    {
                        sql += " and JCFF='" + jcff + "'";
                    }
                    break;
                case "3":
                    sql += " where DATEPART(dd, DLSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, DLSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, DLSJ) = DATEPART(yy, GETDATE())";
                    if (jcff != "0")
                    {
                        sql += " and JCFF='" + jcff + "'";
                    }
                    break;
                default: break;
            }
            
            
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
        #region 获取该检测站的待检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public int getStationAtWaitCount(DateTime starttime,DateTime endtime, string plate, string jcff)
        {
            string sql = "";
            sql = "select count(*) as number from [待检车辆]";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " where DLSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and DLSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
            }
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
        #region 获取该检测站的待检车辆数
        /// <summary>
        /// 获取该检测站的待检车辆数
        /// stationid:站号
        /// </summary>
        public int getStationAtWaitCount(string stationid)
        {
            string sql = "";
            sql = "select count(*) as number from [待检车辆]";
            
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

        #region 获取所有车辆信息
        /// <summary>
        /// 获取所有检测线的信息
        /// </summary>
        public DataTable getAllCarInf(string sqlname, DateTime starttime, DateTime endtime, bool up)
        {
            string sql = "";
            if (up)
                sql = "select lineid,clhp,jcsj,lsh,jcff,zxbz,cpys,hdzk,zzl,rlzl,zcrq,xh,xslc,cz,pp,syxz,scqy,clsbm,scrq,cllx,fdjhm from [" + sqlname + "] where jcsj>'" + starttime.ToShortDateString() + " 00:00:00" + "' and jcsj<'" + endtime.ToShortDateString() + " 23:59:59" + "' and jcjg='合格' order by jcsj";
            else
                sql = "select lineid,clhp,jcsj,lsh,jcff,zxbz,cpys,hdzk,zzl,rlzl,zcrq,xh,xslc,cz,pp,syxz,scqy,clsbm,scrq,cllx,fdjhm from [" + sqlname + "] where jcsj>'" + starttime.ToShortDateString() + " 00:00:00" + "' and jcsj<'" + endtime.ToShortDateString() + " 23:59:59" + "' and jcjg='合格' order by jcsj DESC";
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
        #region 获取该检测站的已检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataSet getStationLineDetectedDataset(string stationid, string lineid, string lx, string plate, string jcff, string result)
        {
            string sql = "";
            if (lineid == "0")
                sql = "select * from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";//+ "' and CLHP LIKE '" + plate + "'";
            else
                sql = "select * from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, JCSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
            
            DataSet dt = null;
            try
            {
                dt = DBHelperSQL.GetDataSet(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion
        #region 获取该检测站的已检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限,cllx:1-轻型车，2-中型车，3-重型车，0，不限
        /// </summary>
        public DataSet getStationLineDetectedDataset(string stationid, string lineid, DateTime starttime, DateTime endtime, string plate, string jcff, string result)
        {
            string sql = "";
            if (lineid == "0")
                sql = "select * from [已检车辆信息]" + " where STATIONID=" + "'" + stationid + "'";//+ "' and CLHP LIKE '" + plate + "'";
            else
                sql = "select * from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            if (jcff != "0")
            {
                sql += " and JCFF='" + jcff + "'";
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
            
            DataSet dt = null;
            try
            {
                dt = DBHelperSQL.GetDataSet(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion
        #region 获取该检测站的已检车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限,cllx:1-轻型车，2-中型车，3-重型车，0，不限
        /// </summary>
        public DataSet getStationLineAniDataset(DateTime starttime, DateTime endtime)
        {
            string sql = "";
            sql = "select * from [已检车辆信息]" + " where JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "' and JCCS='1'";//+ "' and CLHP LIKE '" + plate + "'";
            DataSet dt = null;
            try
            {
                dt = DBHelperSQL.GetDataSet(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion
        #region 获取某段时间内该来检的车辆
        /// <summary>
        /// 获取所有检测线的信息
        /// stationid:站号，lineid:线号，0表示所有线，startTime:起始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限,cllx:1-轻型车，2-中型车，3-重型车，0，不限
        /// </summary>
        public DataSet getAniDataset(DateTime starttime)
        {
            string sql = "";
            sql = "select * from [车辆信息]";//+ "' and CLHP LIKE '" + plate + "'";
            DataTable dt = null;
            DataTable dt_model = null;
            DataSet ds = new DataSet();
            try
            {
                dt = DBHelperSQL.GetDataTable(sql, CommandType.Text);
                dt_model = dt;
                int monthToReg=0;
                int monthToTest = 0;
                DataRow dr=null;
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    dr = dt.Rows[i];
                    DateTime regTime=DateTime.Parse(dr["ZCRQ"].ToString());
                    monthToReg = caculateMonth(regTime,starttime);
                    if (dr["SYXZ"].ToString().Contains("客运"))
                    {
                        if (monthToReg <= 60)
                        {
                            monthToTest = monthToReg % 12;
                            if (monthToTest == 0 || monthToTest == 11 || monthToTest == 10)
                            {
                                continue;
                            }
                            else
                            {
                                dt.Rows.RemoveAt(i--);
                            }
                        }
                        else
                        {
                            monthToTest = (monthToReg-60) % 6;
                            if (monthToTest == 0 || monthToTest == 4 || monthToTest == 5)
                            {
                                continue;
                            }
                            else
                            {
                                dt.Rows.RemoveAt(i--);
                            }
                        }
                    }
                    else if (dr["SYXZ"].ToString().Contains("非营运"))
                    {
                        if (dr["CLLX"].ToString().Contains("H1") || dr["CLLX"].ToString().Contains("H2"))
                        {
                            if (monthToReg <= 120)
                            {
                                monthToTest = monthToReg % 12;
                                if (monthToTest == 0 || monthToTest == 11 || monthToTest == 10)
                                {
                                    continue;
                                }
                                else
                                {
                                    dt.Rows.RemoveAt(i--);
                                }
                            }
                            else
                            {
                                monthToTest = (monthToReg - 120) % 6;
                                if (monthToTest == 0 || monthToTest == 4 || monthToTest == 5)
                                {
                                    continue;
                                }
                                else
                                {
                                    dt.Rows.RemoveAt(i--);
                                }
                            }
                        }
                        else if (dr["CLLX"].ToString().Contains("K3") || dr["CLLX"].ToString().Contains("K4"))
                        {
                            if (monthToReg <= 60)
                            {
                                monthToTest = monthToReg % 24;
                                if (monthToTest == 0 || monthToTest == 22 || monthToTest == 23)
                                {
                                    continue;
                                }
                                else
                                {
                                    dt.Rows.RemoveAt(i--);
                                }
                            }
                            else if (monthToTest <= 180)
                            {
                                monthToTest = (monthToReg - 60) % 12;
                                if (monthToTest == 0 || monthToTest == 11 || monthToTest == 10)
                                {
                                    continue;
                                }
                                else
                                {
                                    dt.Rows.RemoveAt(i--);
                                }
                            }
                            else
                            {
                                monthToTest = (monthToReg - 180) % 6;
                                if (monthToTest == 0 || monthToTest == 4 || monthToTest == 5)
                                {
                                    continue;
                                }
                                else
                                {
                                    dt.Rows.RemoveAt(i--);
                                }
                            }
                        }
                        else
                        {
                            monthToTest = monthToReg % 12;
                            if (monthToTest == 0 || monthToTest == 11 || monthToTest == 10)
                            {
                                continue;
                            }
                            else
                            {
                                dt.Rows.RemoveAt(i--);
                            }
                        }
                    }
                    else
                    {
                        monthToTest = monthToReg % 12;
                        if (monthToTest == 0 || monthToTest == 11 || monthToTest == 10)
                        {
                            continue;
                        }
                        else
                        {
                            dt.Rows.RemoveAt(i--);
                        }
                    }

                }
                ds.Tables.Add(dt); 
                return ds;
            }
            catch
            {
                return ds;
                throw;
            }
        }
        #endregion
        private int caculateMonth(DateTime datetime1, DateTime datetime2)
        {
            int year1 = datetime1.Year;
            int year2 = datetime2.Year;
            int month1 = datetime1.Month;
            int month2 = datetime2.Month;
            int months = 12 * (year2 - year1) + (month2 - month1);
            return months;
        }
        #region 获取该检测站的安检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// lx:0——全部，1——当年，2——当月，3——当天,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataSet getSafeDetectedDataset(string lx, string plate, string result, string isFinished)
        {
            string sql = "";
            sql = "select * from [安检记录]" + " where SFJS=" + "'" + isFinished + "'";//+ "' and CLHP LIKE '" + plate + "'";
            switch (lx)
            {
                case "0":
                    break;
                case "1":
                    sql += " and DATEPART(yy, JCSJ)=DATEPART(yy, GETDATE())";
                    break;
                case "2":
                    sql += " and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                case "3":
                    sql += " and DATEPART(dd, JCSJ)=DATEPART(dd, GETDATE()) and DATEPART(mm, JCSJ)=DATEPART(mm, GETDATE()) and DATEPART(yy, JCSJ) = DATEPART(yy, GETDATE())";
                    break;
                default: break;
            }
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
            
            DataSet dt = null;
            try
            {
                dt = DBHelperSQL.GetDataSet(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion
        #region 获取该检测站的安检车辆数
        /// <summary>
        /// 获取所有检测线的信息
        /// starttime:开始时间,endtime:终止时间,plate:车牌,jcff:0-不限,result:检测结果：-1—不合格，1-合格，0-不限
        /// </summary>
        public DataSet getSafeDetectedDataset(DateTime starttime, DateTime endtime, string plate, string result, string isFinished)
        {
            string sql = "";
            sql = "select * from [安检记录]" + " where SFJS=" + "'" + isFinished + "'";//+ "' and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            switch (result)
            {
                case "0": break;
                case "-1": sql += " and JCJG='不合格'"; break;
                case "1": sql += " and JCJG='合格'"; break;
                default: break;
            }
            
            DataSet dt = null;
            try
            {
                dt = DBHelperSQL.GetDataSet(sql, CommandType.Text);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion

        #region 获取该检测站的已检车辆
        /// <summary>
        /// 获取该检测站的上线次数
        /// </summary>
        /// <param name="stationid">检测站编号</param>
        /// <param name="lineid">线号</param>
        /// <param name="starttime">统计开始时间</param>
        /// <param name="endtime">统计结束时间</param>
        /// <param name="pdjg">判定结果</param>
        /// <param name="jcff">检测方法</param>
        /// <param name="jccs">检测次数</param>
        /// <returns></returns>
        public int getStationLineTestCount(string stationid, string lineid, DateTime starttime, DateTime endtime, string pdjg, string jcff, string jccs)
        {
            string sql = "";
            sql = "select count(distinct clhp) as number from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            sql += " and JCJG='" + pdjg + "'";
            sql += " and JCFF='" + jcff + "'";
            switch (jccs)
            {
                case "1":
                    sql += " and JCCS='1'"; break;
                case "2":
                    sql += " and JCCS='2'"; break;
                case "3":
                    sql += " and JCCS>'2'"; break;
                default: break;
            }
            int dt = 0;
            try
            {
                dt = DBHelperSQL.ExecuteCount(sql);
                return dt;
            }
            catch
            {
                return dt;
                throw;
            }
        }
        #endregion

        #region 获取该检测站的上线车辆数
        /// <summary>
        /// 获取该检测站的上线车辆数
        /// </summary>
        /// <param name="stationid">检测站编号</param>
        /// <param name="lineid">线号</param>
        /// <param name="starttime">统计开始时间</param>
        /// <param name="endtime">统计结束时间</param>
        /// <param name="pdjg">判定结果</param>
        /// <param name="jcff">检测方法</param>
        /// <param name="jccs">检测次数</param>
        /// <returns></returns>
        public int getStationLineCarCount(string stationid, string lineid, DateTime starttime, DateTime endtime, string pdjg, string jcff, string jccs)
        {
            string sql = "";
            sql = "select count(clhp) as number from [已检车辆信息]" + " where STATIONID='" + stationid + "' and LINEID='" + lineid + "'";// and CLHP LIKE '" + plate + "'";
            sql += " and JCSJ>='" + starttime.ToShortDateString() + " 00:00:00" + "' and JCSJ<='" + endtime.ToShortDateString() + " 23:59:59" + "'";
            sql += " and JCJG='" + pdjg + "'";
            sql += " and JCFF='" + jcff + "'";
            switch (jccs)
            {
                case "1":
                    sql += " and JCCS='1'"; break;
                case "2":
                    sql += " and JCCS='2'"; break;
                case "3":
                    sql += " and JCCS>'2'"; break;
                default: break;
            }
            int dt = 0;
            try
            {
                dt = DBHelperSQL.ExecuteCount(sql);
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


