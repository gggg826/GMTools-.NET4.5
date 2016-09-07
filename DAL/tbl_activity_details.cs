using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;//Please add references

namespace FengNiao.GMTools.Database.DAL
{
    public partial class tbl_activity_details
    {
        public tbl_activity_details()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("fld_record_id", "tbl_activity_details");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int record_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tbl_activity_details");
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
        public bool Add(FengNiao.GMTools.Database.Model.tbl_activity_details model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tbl_activity_details(");
            strSql.Append("fld_solution_id,fld_open_type,fld_activity_id,fld_preview_time,fld_open_time,fld_close_time,fld_award_time,fld_dataid)");
            strSql.Append(" values (");
            strSql.Append("@fld_solution_id,@fld_open_type,@fld_activity_id,@fld_preview_time,@fld_open_time,@fld_close_time,@fld_award_time,@fld_dataid)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_solution_id", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_open_type", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_activity_id", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_preview_time", MySqlDbType.VarChar, 30),
                    new MySqlParameter("@fld_open_time", MySqlDbType.VarChar,30),
                    new MySqlParameter("@fld_close_time", MySqlDbType.VarChar,30),
                    new MySqlParameter("@fld_award_time", MySqlDbType.VarChar,30),
                    new MySqlParameter("@fld_dataid", MySqlDbType.Int32,11)};
            parameters[0].Value = model.fld_solution_id;
            parameters[1].Value = model.fld_open_type;
            parameters[2].Value = model.fld_activity_id;
            parameters[3].Value = model.fld_preview_time;
            parameters[4].Value = model.fld_open_time;
            parameters[5].Value = model.fld_close_time;
            parameters[6].Value = model.fld_award_time;
            parameters[7].Value = model.fld_dataid;

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
        public bool Update(FengNiao.GMTools.Database.Model.tbl_activity_details model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tbl_activity_details set ");
            strSql.Append("fld_solution_id=@fld_solution_id,");
            strSql.Append("fld_open_type=@fld_open_type,");
            strSql.Append("fld_activity_id=@fld_activity_id,");
            strSql.Append("fld_preview_time=@fld_preview_time,");
            strSql.Append("fld_open_time=@fld_open_time,");
            strSql.Append("fld_close_time=@fld_close_time,");
            strSql.Append("fld_award_time=@fld_award_time");
            strSql.Append("fld_dataid=@fld_dataid");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_solution_id", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_open_type", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_activity_id", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_preview_time", MySqlDbType.VarChar, 30),
                    new MySqlParameter("@fld_open_time", MySqlDbType.VarChar,30),
                    new MySqlParameter("@fld_close_time", MySqlDbType.VarChar,30),
                    new MySqlParameter("@fld_award_time", MySqlDbType.VarChar,30),
                    new MySqlParameter("@fld_dataid", MySqlDbType.Int32,11),
                    new MySqlParameter("@fld_record_id", MySqlDbType.VarChar,30)};
            parameters[0].Value = model.fld_solution_id;
            parameters[1].Value = model.fld_open_type;
            parameters[2].Value = model.fld_activity_id;
            parameters[3].Value = model.fld_preview_time;
            parameters[4].Value = model.fld_open_time;
            parameters[5].Value = model.fld_close_time;
            parameters[6].Value = model.fld_award_time;
            parameters[7].Value = model.fld_dataid;
            parameters[8].Value = model.fld_record_id;

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
        public bool Delete(int fld_record_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tbl_activity_details ");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = fld_record_id;

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
            strSql.Append("delete from tbl_activity_details ");
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
        public FengNiao.GMTools.Database.Model.tbl_activity_details GetModel(int record_id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fld_solution_id,fld_open_type,fld_activity_id,fld_preview_time,fld_open_time,fld_close_time,fld_award_time,fld_dataid from tbl_activity_details ");
            strSql.Append(" where fld_record_id=@fld_record_id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
            };
            parameters[0].Value = record_id;

            FengNiao.GMTools.Database.Model.tbl_activity_details model = new FengNiao.GMTools.Database.Model.tbl_activity_details();
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
        public FengNiao.GMTools.Database.Model.tbl_activity_details DataRowToModel(DataRow row)
        {
            FengNiao.GMTools.Database.Model.tbl_activity_details model = new FengNiao.GMTools.Database.Model.tbl_activity_details();
            if (row != null)
            {
                if (row["fld_record_id"] != null && row["fld_record_id"].ToString() != "")
                {
                    model.fld_record_id = int.Parse(row["fld_record_id"].ToString());
                }
                if (row["fld_solution_id"] != null && row["fld_solution_id"].ToString() != "")
                {
                    model.fld_solution_id = int.Parse(row["fld_solution_id"].ToString());
                }
                if (row["fld_open_type"] != null && row["fld_open_type"].ToString() != "")
                {
                    model.fld_open_type = int.Parse(row["fld_open_type"].ToString());
                }
                if (row["fld_activity_id"] != null && row["fld_activity_id"].ToString() != "")
                {
                    model.fld_activity_id = int.Parse(row["fld_activity_id"].ToString());
                }
                if (row["fld_preview_time"] != null && row["fld_preview_time"].ToString() != "")
                {
                    model.fld_preview_time = row["fld_preview_time"].ToString();
                }
                if (row["fld_open_time"] != null && row["fld_open_time"].ToString() != "")
                {
                    model.fld_open_time = row["fld_open_time"].ToString();
                }
                if (row["fld_close_time"] != null && row["fld_close_time"].ToString() != "")
                {
                    model.fld_close_time = row["fld_close_time"].ToString();
                }
                if (row["fld_award_time"] != null && row["fld_award_time"].ToString() != "")
                {
                    model.fld_award_time = row["fld_award_time"].ToString();
                }
                if (row["fld_dataid"] != null && row["fld_dataid"].ToString() != "")
                {
                    model.fld_dataid = int.Parse(row["fld_dataid"].ToString());
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
            strSql.Append("select fld_record_id,fld_solution_id,fld_open_type,fld_activity_id,fld_preview_time,fld_open_time,fld_close_time,fld_award_time,fld_dataid ");
            strSql.Append(" FROM tbl_activity_details ");
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
            strSql.Append("select count(1) FROM tbl_activity_details ");
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
            strSql.Append(")AS Row, T.*  from tbl_activity_details T ");
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
			parameters[0].Value = "tbl_activity_details";
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
