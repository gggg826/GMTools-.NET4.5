using Maticsoft.DBUtility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.DAL
{
    public partial class tbl_baidupush
    {
        public tbl_baidupush(){}

        #region  BasicMethod

        public bool Add(FengNiao.GMTools.Database.Model.tbl_baidupush model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_baidupush(");
            strSql.Append("title,context,startime,stoptime,pushtime,mes_id,timer_id)");
            strSql.Append(" values (");
            strSql.Append("@title,@context,@startime,@stoptime,@pushtime,@mes_id,@timer_id)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@title", MySqlDbType.VarChar,255),
                    new MySqlParameter("@context", MySqlDbType.VarChar,255),
                    new MySqlParameter("@startime", MySqlDbType.DateTime),
                    new MySqlParameter("@stoptime", MySqlDbType.DateTime),
                    new MySqlParameter("@pushtime", MySqlDbType.DateTime),
                    new MySqlParameter("@mes_id", MySqlDbType.VarChar,255),
                    new MySqlParameter("@timer_id", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.tile;
            parameters[1].Value = model.context;
            parameters[2].Value = model.startime;
            parameters[3].Value = model.stoptime;
            parameters[4].Value = model.pushtime;
            parameters[5].Value = model.mes_id;
            parameters[6].Value = model.timer_id;

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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int record_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_baidupush");
            strSql.Append(" where record_id=@record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FengNiao.GMTools.Database.Model.tbl_baidupush model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_baidupush set ");
            strSql.Append("title=@title,");
            strSql.Append("context=@context,");
            strSql.Append("startime=@startime,");
            strSql.Append("stoptime=@stoptime,");
            strSql.Append("pushtime=@pushtime");
            strSql.Append(" where record_id=@record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@title", MySqlDbType.VarChar,255),
                    new MySqlParameter("@context", MySqlDbType.VarChar,255),
                    new MySqlParameter("@startime", MySqlDbType.DateTime),
                    new MySqlParameter("@stoptime", MySqlDbType.DateTime),
                    new MySqlParameter("@pushtime", MySqlDbType.DateTime),
                    new MySqlParameter("@record_id", MySqlDbType.Int32,11)};
            parameters[0].Value = model.tile;
            parameters[1].Value = model.context;
            parameters[2].Value = model.startime;
            parameters[3].Value = model.stoptime;
            parameters[4].Value = model.pushtime;
            parameters[5].Value = model.record_id;

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
        /// 更新一条数据
        /// </summary>
        public bool UpdateMsg_ID(FengNiao.GMTools.Database.Model.tbl_baidupush model, string msg_id, string timer_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_baidupush set ");
            strSql.Append("mes_id=@mes_id,");
            strSql.Append("timer_id=@timer_id");
            strSql.Append(" where record_id=@record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@mes_id", MySqlDbType.VarChar,255),
                    new MySqlParameter("@timer_id", MySqlDbType.VarChar,255),
                    new MySqlParameter("@record_id", MySqlDbType.Int32,11)};
            parameters[0].Value = msg_id;
            parameters[1].Value = timer_id;
            parameters[2].Value = model.record_id;

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
        /// 得到一个对象实体
        /// </summary>
        public FengNiao.GMTools.Database.Model.tbl_baidupush GetModel(int record_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select record_id,title,context,startime,stoptime,pushtime,mes_id,timer_id from tbl_baidupush ");
            strSql.Append(" where record_id=@record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

            FengNiao.GMTools.Database.Model.tbl_baidupush model = new FengNiao.GMTools.Database.Model.tbl_baidupush();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select record_id,title,context,startime,stoptime,pushtime,mes_id,timer_id");
            strSql.Append(" FROM tbl_baidupush ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FengNiao.GMTools.Database.Model.tbl_baidupush DataRowToModel(DataRow row)
        {
            FengNiao.GMTools.Database.Model.tbl_baidupush model = new FengNiao.GMTools.Database.Model.tbl_baidupush();
            if (row != null)
            {
                if (row["record_id"] != null && row["record_id"].ToString() != "")
                {
                    model.record_id = int.Parse(row["record_id"].ToString());
                }
                if (row["title"] != null && row["title"].ToString() != "")
                {
                    model.tile = row["title"].ToString();
                }
                if (row["context"] != null && row["context"].ToString() != "")
                {
                    model.context = row["context"].ToString();
                }
                if (row["startime"] != null && row["startime"].ToString() != "")
                {
                    model.startime = DateTime.Parse(row["startime"].ToString());
                }
                if (row["stoptime"] != null && row["stoptime"].ToString() != "")
                {
                    model.stoptime = DateTime.Parse(row["stoptime"].ToString());
                }
                if (row["pushtime"] != null && row["pushtime"].ToString() != "")
                {
                    model.pushtime = DateTime.Parse(row["pushtime"].ToString());
                }

                if (row["mes_id"] != null && row["mes_id"].ToString() != "")
                {
                    model.mes_id = row["mes_id"].ToString();
                }
                if (row["timer_id"] != null && row["timer_id"].ToString() != "")
                {
                    model.timer_id = row["timer_id"].ToString();
                }
            }
            return model;
        }
        #endregion  BasicMethod

    }
}
