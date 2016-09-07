using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using FengNiao.GMTools.Database.Model;

namespace FengNiao.GMTools.Database.BLL
{
    public partial class tbl_log
    {

        private readonly FengNiao.GMTools.Database.DAL.tbl_log dal = new FengNiao.GMTools.Database.DAL.tbl_log();
        public tbl_log()
        { }

        #region  Method
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
        public int Add(FengNiao.GMTools.Database.Model.tbl_log model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FengNiao.GMTools.Database.Model.tbl_log model)
        {
            return dal.Update(model);
        }

        
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string record_idlist)
        {
            return dal.DeleteList(record_idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FengNiao.GMTools.Database.Model.tbl_log GetModel(int record_id)
        {

            return dal.GetModel(record_id);
        }

        
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FengNiao.GMTools.Database.Model.tbl_log> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FengNiao.GMTools.Database.Model.tbl_log> DataTableToList(DataTable dt)
        {
            List<FengNiao.GMTools.Database.Model.tbl_log> modelList = new List<FengNiao.GMTools.Database.Model.tbl_log>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FengNiao.GMTools.Database.Model.tbl_log model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new FengNiao.GMTools.Database.Model.tbl_log();
                    if (dt.Rows[n]["record_id"].ToString() != "")
                    {
                        model.record_id = int.Parse(dt.Rows[n]["record_id"].ToString());
                    }
                    model.adminid = dt.Rows[n]["adminid"].ToString();
                    model.item_name = dt.Rows[n]["item_name"].ToString();
                    model.comment = dt.Rows[n]["comment"].ToString();
                    if (dt.Rows[n]["datetime"].ToString() != "")
                    {
                        model.datetime = DateTime.Parse(dt.Rows[n]["datetime"].ToString());
                    }


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
