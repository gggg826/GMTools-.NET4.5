using System;
using System.Collections.Generic;
using System.Web;
using GameToolsCommon;
using CommLib.BdPush;

namespace GameToolsHttpService
{
    public class PushNoticeModule : ProcessRequestBase
    {
        public PushNoticeModule(string tableName)
            : base(tableName)
        {
        }

        public override string Add(HttpContext context)
        {
            ResultModel requestResult = new ResultModel();
            string strModel = context.Request["Model"];

            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            try
            {
                FengNiao.GMTools.Database.Model.tbl_baidupush configModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<FengNiao.GMTools.Database.Model.tbl_baidupush>(strModel);
                if (configModel != null)
                {
                    FengNiao.GMTools.Database.DAL.tbl_baidupush ConfigDAL = new FengNiao.GMTools.Database.DAL.tbl_baidupush();
                    FengNiao.GMTools.Database.Model.tbl_baidupush model = BaiduPushNotice.PushNewAndroidTask(configModel);
                    ConfigDAL.Add(model);
                    requestResult.IsSuccess = true;
                    requestResult.Content = "";
                }
            }
            catch
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
        }

        public override string Update(HttpContext context)
        {
            ResultModel requestResult = new ResultModel();
            string strModel = context.Request["Model"];

            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            try
            {
                FengNiao.GMTools.Database.Model.tbl_baidupush configModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<FengNiao.GMTools.Database.Model.tbl_baidupush>(strModel);
                if (configModel != null)
                {
                    FengNiao.GMTools.Database.DAL.tbl_baidupush activityConfigDAL = new FengNiao.GMTools.Database.DAL.tbl_baidupush();
                    if (activityConfigDAL.Update(configModel))
                    {
                        BaiduPushNotice.CancleSigleTask(configModel.timer_id);
                        BaiduPushNotice.PushSigleTask(configModel);
                        requestResult.IsSuccess = true;
                        requestResult.Content = "";
                    }
                }
            }
            catch
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
        }

        
    public override string AsyncToOthers(HttpContext context)
    {
            string strRecord_id = context.Request["Record_id"];
            ResultModel requestResult = new ResultModel();
            try
            {
                FengNiao.GMTools.Database.BLL.tbl_baidupush configBLL = new FengNiao.GMTools.Database.BLL.tbl_baidupush();
                FengNiao.GMTools.Database.Model.tbl_baidupush model = configBLL.GetModel(int.Parse(strRecord_id));
                if(model.mes_id == "0")
                {
                    requestResult.IsSuccess = true;
                    requestResult.Content = "暂无数据，请稍后查询";
                }
                else
                {
                    string result = BaiduPushNotice.QueryMesResult(model.mes_id);

                    requestResult.IsSuccess = true;
                    requestResult.Content = result;
                }
            }
            catch
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
        }
    }
}
