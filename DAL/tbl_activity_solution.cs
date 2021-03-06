﻿using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references

namespace FengNiao.GMTools.Database.DAL
{
    public partial class tbl_activity_solution
    {
        public tbl_activity_solution()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("fld_record_id", "tbl_activity_solution");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int record_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_activity_solution");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FengNiao.GMTools.Database.Model.tbl_activity_solution model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_activity_solution(");
            strSql.Append("fld_id,fld_name,fld_servers)");
            strSql.Append(" values (");
            strSql.Append("@fld_id,@fld_name,@fld_servers)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_id", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_name", MySqlDbType.VarChar,50),
                    new MySqlParameter("@fld_servers", MySqlDbType.VarChar,2000),};
            parameters[0].Value = model.fld_id;
            parameters[1].Value = model.fld_name;
            parameters[2].Value = model.fld_servers;

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
        public bool Update(FengNiao.GMTools.Database.Model.tbl_activity_solution model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_activity_solution set ");
            strSql.Append("fld_id=@fld_id,");
            strSql.Append("fld_name=@fld_name,");
            strSql.Append("fld_servers=@fld_servers");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_id", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_name", MySqlDbType.VarChar,50),
                    new MySqlParameter("@fld_servers", MySqlDbType.VarChar,2000),
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32,11) };
            parameters[0].Value = model.fld_id;
            parameters[1].Value = model.fld_name;
            parameters[2].Value = model.fld_servers;
            parameters[3].Value = model.fld_record_id;
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
        /// 删除一条数据
        /// </summary>
        public bool Remove(FengNiao.GMTools.Database.Model.tbl_activity_solution model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_activity_solution ");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = model.fld_record_id;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int record_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_activity_solution ");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string record_idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_activity_solution ");
            strSql.Append(" where fld_record_id in (" + record_idlist + ")  ");
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
        public FengNiao.GMTools.Database.Model.tbl_activity_solution GetModel(int record_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fld_id,fld_name,fld_servers ");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

            FengNiao.GMTools.Database.Model.tbl_activity_solution model = new FengNiao.GMTools.Database.Model.tbl_activity_solution();
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


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FengNiao.GMTools.Database.Model.tbl_activity_solution DataRowToModel(DataRow row)
        {
            FengNiao.GMTools.Database.Model.tbl_activity_solution model = new FengNiao.GMTools.Database.Model.tbl_activity_solution();
            if (row != null)
            {
                if (row["fld_record_id"] != null && row["fld_record_id"].ToString() != "")
                {
                    model.fld_record_id = int.Parse(row["fld_record_id"].ToString());
                }
                if (row["fld_id"] != null && row["fld_id"].ToString() != "")
                {
                    model.fld_id = int.Parse(row["fld_id"].ToString());
                }
                if (row["fld_name"] != null && row["fld_name"].ToString() != "")
                {
                    model.fld_name = row["fld_name"].ToString();
                }
                if (row["fld_servers"] != null && row["fld_servers"].ToString() != "")
                {
                    model.fld_servers = row["fld_servers"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fld_record_id,fld_id,fld_name,fld_servers ");
            strSql.Append(" FROM tbl_activity_solution ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tbl_activity_solution ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.record_id desc");
            }
            strSql.Append(")AS Row, T.*  from tbl_activity_solution T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "tbl_activity_solution";
			parameters[1].Value = "record_id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
    }
}
