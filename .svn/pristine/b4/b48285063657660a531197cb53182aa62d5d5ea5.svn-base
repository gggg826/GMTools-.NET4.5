using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Maticsoft.DBUtility;

namespace FengNiao.GMTools.Database.DAL  
{
	 	//tbl_baidupush_detail
		public partial class tbl_baidupush_detail
	{
   		     
		public bool Exists(int fld_record_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tbl_baidupush_detail");
			strSql.Append(" where ");
			                                       strSql.Append(" fld_record_id = @fld_record_id  ");
                            			MySqlParameter[] parameters = {
					new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
			};
			parameters[0].Value = fld_record_id;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(FengNiao.GMTools.Database.Model.tbl_baidupush_detail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tbl_baidupush_detail(");			
            strSql.Append("fld_teams_id,fld_send_time,fld_mes_id");
			strSql.Append(") values (");
            strSql.Append("@fld_teams_id,@fld_send_time,@fld_mes_id");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			MySqlParameter[] parameters = {
			            new MySqlParameter("@fld_teams_id", MySqlDbType.Int32,11) ,            
                        new MySqlParameter("@fld_send_time", MySqlDbType.DateTime) ,            
                        new MySqlParameter("@fld_mes_id", MySqlDbType.VarChar,255)             
              
            };
			            
            parameters[0].Value = model.fld_teams_id;                        
            parameters[1].Value = model.fld_send_time;                        
            parameters[2].Value = model.fld_mes_id;                        
			   
			object obj = DbHelperMySQL.GetSingle(strSql.ToString(),parameters);			
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
		public bool Update(FengNiao.GMTools.Database.Model.tbl_baidupush_detail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tbl_baidupush_detail set ");
			                                                
            strSql.Append(" fld_teams_id = @fld_teams_id , ");                                    
            strSql.Append(" fld_send_time = @fld_send_time , ");                                    
            strSql.Append(" fld_mes_id = @fld_mes_id  ");            			
			strSql.Append(" where fld_record_id=@fld_record_id ");
						
MySqlParameter[] parameters = {
			            new MySqlParameter("@fld_record_id", MySqlDbType.Int32,11) ,            
                        new MySqlParameter("@fld_teams_id", MySqlDbType.Int32,11) ,            
                        new MySqlParameter("@fld_send_time", MySqlDbType.DateTime) ,            
                        new MySqlParameter("@fld_mes_id", MySqlDbType.VarChar,255)             
              
            };
						            
            parameters[0].Value = model.fld_record_id;                        
            parameters[1].Value = model.fld_teams_id;                        
            parameters[2].Value = model.fld_send_time;                        
            parameters[3].Value = model.fld_mes_id;                        
            int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tbl_baidupush_detail ");
			strSql.Append(" where fld_record_id=@fld_record_id");
						MySqlParameter[] parameters = {
					new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
			};
			parameters[0].Value = fld_record_id;


			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string fld_record_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tbl_baidupush_detail ");
			strSql.Append(" where ID in ("+fld_record_idlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public FengNiao.GMTools.Database.Model.tbl_baidupush_detail GetModel(int fld_record_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select fld_record_id, fld_teams_id, fld_send_time, fld_mes_id  ");			
			strSql.Append("  from tbl_baidupush_detail ");
			strSql.Append(" where fld_record_id=@fld_record_id");
						MySqlParameter[] parameters = {
					new MySqlParameter("@fld_record_id", MySqlDbType.Int32)
			};
			parameters[0].Value = fld_record_id;

			
			FengNiao.GMTools.Database.Model.tbl_baidupush_detail model=new FengNiao.GMTools.Database.Model.tbl_baidupush_detail();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["fld_record_id"].ToString()!="")
				{
					model.fld_record_id=int.Parse(ds.Tables[0].Rows[0]["fld_record_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fld_teams_id"].ToString()!="")
				{
					model.fld_teams_id=int.Parse(ds.Tables[0].Rows[0]["fld_teams_id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["fld_send_time"].ToString()!="")
				{
					model.fld_send_time=DateTime.Parse(ds.Tables[0].Rows[0]["fld_send_time"].ToString());
				}
					model.fld_mes_id= ds.Tables[0].Rows[0]["fld_mes_id"].ToString();
																										
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM tbl_baidupush_detail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM tbl_baidupush_detail ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperMySQL.Query(strSql.ToString());
		}

   
	}
}

