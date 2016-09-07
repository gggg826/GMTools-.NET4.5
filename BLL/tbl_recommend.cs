﻿using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using FengNiao.GMTools.Database.Model;
namespace FengNiao.GMTools.Database.BLL
{
	/// <summary>
	/// tbl_recommend
	/// </summary>
	public partial class tbl_recommend
	{
		private readonly FengNiao.GMTools.Database.DAL.tbl_recommend dal=new FengNiao.GMTools.Database.DAL.tbl_recommend();
		public tbl_recommend()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int record_id)
		{
			return dal.Exists(record_id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(FengNiao.GMTools.Database.Model.tbl_recommend model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(FengNiao.GMTools.Database.Model.tbl_recommend model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int record_id)
		{
			
			return dal.Delete(record_id);
		}



        public bool DeleteByServer(int server_id)
        {

            return dal.DeleteByServer(server_id);
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string record_idlist )
		{
			return dal.DeleteList(record_idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public FengNiao.GMTools.Database.Model.tbl_recommend GetModel(int record_id)
		{
			
			return dal.GetModel(record_id);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public FengNiao.GMTools.Database.Model.tbl_recommend GetModelByCache(int record_id)
		{
			
			string CacheKey = "tbl_recommendModel-" + record_id;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(record_id);
					if (objModel != null)
					{
						int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (FengNiao.GMTools.Database.Model.tbl_recommend)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<FengNiao.GMTools.Database.Model.tbl_recommend> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<FengNiao.GMTools.Database.Model.tbl_recommend> DataTableToList(DataTable dt)
		{
			List<FengNiao.GMTools.Database.Model.tbl_recommend> modelList = new List<FengNiao.GMTools.Database.Model.tbl_recommend>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				FengNiao.GMTools.Database.Model.tbl_recommend model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
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

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

