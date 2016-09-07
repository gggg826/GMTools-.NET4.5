using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using FengNiao.GMTools.Database.Model;
namespace FengNiao.GMTools.Database.BLL
{
	 	//tbl_baidupush_detail
		public partial class tbl_baidupush_detail
	{
   		     
		private readonly FengNiao.GMTools.Database.DAL.tbl_baidupush_detail dal=new FengNiao.GMTools.Database.DAL.tbl_baidupush_detail();
		public tbl_baidupush_detail()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int fld_record_id)
		{
			return dal.Exists(fld_record_id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(FengNiao.GMTools.Database.Model.tbl_baidupush_detail model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(FengNiao.GMTools.Database.Model.tbl_baidupush_detail model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int fld_record_id)
		{
			
			return dal.Delete(fld_record_id);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string fld_record_idlist )
		{
			return dal.DeleteList(fld_record_idlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public FengNiao.GMTools.Database.Model.tbl_baidupush_detail GetModel(int fld_record_id)
		{
			
			return dal.GetModel(fld_record_id);
		}

		///// <summary>
		///// 得到一个对象实体，从缓存中
		///// </summary>
		//public FengNiao.GMTools.Database.Model.tbl_baidupush_detail GetModelByCache(int fld_record_id)
		//{
			
		//	string CacheKey = "tbl_baidupush_detailModel-" + fld_record_id;
		//	object objModel = FengNiao.GMTools.Database.Common.DataCache.GetCache(CacheKey);
		//	if (objModel == null)
		//	{
		//		try
		//		{
		//			objModel = dal.GetModel(fld_record_id);
		//			if (objModel != null)
		//			{
		//				int ModelCache = FengNiao.GMTools.Database.Common.ConfigHelper.GetConfigInt("ModelCache");
		//				FengNiao.GMTools.Database.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
		//			}
		//		}
		//		catch{}
		//	}
		//	return (FengNiao.GMTools.Database.Model.tbl_baidupush_detail)objModel;
		//}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<FengNiao.GMTools.Database.Model.tbl_baidupush_detail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<FengNiao.GMTools.Database.Model.tbl_baidupush_detail> DataTableToList(DataTable dt)
		{
			List<FengNiao.GMTools.Database.Model.tbl_baidupush_detail> modelList = new List<FengNiao.GMTools.Database.Model.tbl_baidupush_detail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				FengNiao.GMTools.Database.Model.tbl_baidupush_detail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new FengNiao.GMTools.Database.Model.tbl_baidupush_detail();					
													if(dt.Rows[n]["fld_record_id"].ToString()!="")
				{
					model.fld_record_id=int.Parse(dt.Rows[n]["fld_record_id"].ToString());
				}
				if(dt.Rows[n]["fld_teams_id"].ToString()!="")
				{
					model.fld_teams_id=int.Parse(dt.Rows[n]["fld_teams_id"].ToString());
				}
				if(dt.Rows[n]["fld_send_time"].ToString()!="")
				{
					model.fld_send_time=DateTime.Parse(dt.Rows[n]["fld_send_time"].ToString());
				}
				model.fld_mes_id= dt.Rows[n]["fld_mes_id"].ToString();
																						
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}
#endregion
   
	}
}