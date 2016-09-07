using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references

namespace FengNiao.GMTools.Database.DAL
{
    public partial class tbl_log
    {

        public bool Exists(int record_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_log");
            strSql.Append(" where ");
            strSql.Append(" record_id = @record_id  ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(FengNiao.GMTools.Database.Model.tbl_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_log(");
            strSql.Append("adminid,item_name,comment,datetime");
            strSql.Append(") values (");
            strSql.Append("@adminid,@item_name,@comment,@datetime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
                        new MySqlParameter("@adminid", MySqlDbType.VarChar,255) ,
                        new MySqlParameter("@item_name", MySqlDbType.VarChar,255) ,
                        new MySqlParameter("@comment", MySqlDbType.VarChar,255) ,
                        new MySqlParameter("@datetime", MySqlDbType.DateTime)

            };

            parameters[0].Value = model.adminid;
            parameters[1].Value = model.item_name;
            parameters[2].Value = model.comment;
            parameters[3].Value = model.datetime;

            object obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FengNiao.GMTools.Database.Model.tbl_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_log set ");

            strSql.Append(" adminid = @adminid , ");
            strSql.Append(" item_name = @item_name , ");
            strSql.Append(" comment = @comment , ");
            strSql.Append(" datetime = @datetime  ");
            strSql.Append(" where record_id=@record_id ");

            MySqlParameter[] parameters = {
                        new MySqlParameter("@record_id", MySqlDbType.Int32,20) ,
                        new MySqlParameter("@adminid", MySqlDbType.VarChar,255) ,
                        new MySqlParameter("@item_name", MySqlDbType.VarChar,255) ,
                        new MySqlParameter("@comment", MySqlDbType.VarChar,255) ,
                        new MySqlParameter("@datetime", MySqlDbType.DateTime)

            };

            parameters[0].Value = model.record_id;
            parameters[1].Value = model.adminid;
            parameters[2].Value = model.item_name;
            parameters[3].Value = model.comment;
            parameters[4].Value = model.datetime;
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string record_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_log ");
            strSql.Append(" where ID in (" + record_idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FengNiao.GMTools.Database.Model.tbl_log GetModel(int record_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select record_id, adminid, item_name, comment, datetime  ");
            strSql.Append("  from tbl_log ");
            strSql.Append(" where record_id=@record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@record_id",MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;


            FengNiao.GMTools.Database.Model.tbl_log model = new FengNiao.GMTools.Database.Model.tbl_log();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["record_id"].ToString() != "")
                {
                    model.record_id = int.Parse(ds.Tables[0].Rows[0]["record_id"].ToString());
                }
                model.adminid = ds.Tables[0].Rows[0]["adminid"].ToString();
                model.item_name = ds.Tables[0].Rows[0]["item_name"].ToString();
                model.comment = ds.Tables[0].Rows[0]["comment"].ToString();
                if (ds.Tables[0].Rows[0]["datetime"].ToString() != "")
                {
                    model.datetime = DateTime.Parse(ds.Tables[0].Rows[0]["datetime"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tbl_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM tbl_log ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySQL.Query(strSql.ToString());
        }


    }
}
