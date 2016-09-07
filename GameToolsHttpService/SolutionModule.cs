using System;
using System.Collections.Generic;
using System.Web;
using GameToolsCommon;
using System.Text;
using System.Reflection;
using FengNiao.GameToolsCommon;
using Newtonsoft.Json.Linq;

namespace GameToolsHttpService
{
    public class SolutionModule : ProcessRequestBase
    {
        public SolutionModule(string tableName)
            : base(tableName)
        {
        }
        List<OperateResultModel> resultModelList = new List<OperateResultModel>();
        public override string Update(HttpContext context)
        {
            resultModelList.Clear();

            string strModel = context.Request["Model"];
            string strOperationType = context.Request["OperationType"];
            string strGuid = context.Request["guid"];
            ResultModel requestResult = new ResultModel();
            if (!CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }

            try
            {
                List<OperationType> operationTypeList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<OperationType>(strOperationType);
                List<string> guidList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<string>(strGuid);
                List<FengNiao.GMTools.Database.Model.tbl_activity_solution> modelList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_activity_solution>(strModel);

                FengNiao.GMTools.Database.BLL.tbl_activity_solution activityItemBLL = new FengNiao.GMTools.Database.BLL.tbl_activity_solution();

                if (operationTypeList != null && guidList != null && modelList.Count == guidList.Count && modelList.Count == operationTypeList.Count)
                {
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        OperationType operationType = operationTypeList[i];
                        if (operationType == OperationType.Add)
                        {
                            try
                            {
                                if (activityItemBLL.Add(modelList[i]))
                                {
                                        SendReload(modelList[i].fld_servers);
                                }
                            }
                            catch
                            {
                                OperateResultModel result = new OperateResultModel();
                                result.IsSuccess = true;
                                result.Content = modelList[i].fld_servers + " -> 新增失败";
                                resultModelList.Add(result);
                            }

                        }
                        else if (operationType == OperationType.Update)
                        {
                            try
                            {
                                if (activityItemBLL.Update(modelList[i]))
                                {
                                    SendReload(modelList[i].fld_servers);
                                }
                            }
                            catch
                            {
                                OperateResultModel result = new OperateResultModel();
                                result.IsSuccess = true;
                                result.Content = modelList[i].fld_servers + " -> 更新失败";
                                resultModelList.Add(result);
                            }
                        }
                    }
                }
                requestResult.IsSuccess = true;
                requestResult.Content = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<OperateResultModel>>(resultModelList);
            }
            catch
            {
                requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
        }

        private void SendReload(string severList)
        {
            string[] servers = severList.Split(';');
            FengNiao.GMTools.Database.BLL.tbl_server serverBLL = new FengNiao.GMTools.Database.BLL.tbl_server();
            for (int i = 0; i < servers.Length; i++)
            {
                List<FengNiao.GMTools.Database.Model.tbl_server> server = serverBLL.GetModelList("fld_server_id = " + servers[i]);
                OperateResultModel result = new OperateResultModel();

                if (server.Count > 0)
                {
                    try
                    {
                        string serverIP = server[i].fld_server_ip;
                        if (!string.IsNullOrEmpty(serverIP))
                        {
                            string strArgs = "cmd=reload_activity";
                            if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
                            {
                                serverIP = serverIP.ToUpper().Replace("HTTP://", "");
                            }
                            byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
                            string strContent = Encoding.UTF8.GetString(bytes);
                            JObject contentObj = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject(strContent);
                            int errorcode = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(contentObj, "errorcode");

                            if (errorcode == 0)
                            {
                                result.IsSuccess = true;
                                result.Content = servers[i] + " -> 新增成功";
                            }
                            else
                            {
                                result.IsSuccess = true;
                                result.Content = "活动已保存，但未能成功通知游戏服务器" + "(" + servers[i] + ")"; ;
                            }
                        }
                    }
                    catch
                    {
                        result.IsSuccess = true;
                        result.Content = "活动已保存，但未能成功通知游戏服务器" + "(" + servers[i] + ")";

                    }
                        //resultModelList.Add(result);
                 }
                else
                {
                    result.IsSuccess = true;
                    result.Content = "未查到相关服务器信息" + "(" + servers[i] + ")";
                    //resultModelList.Add(result);
                }
                resultModelList.Add(result);
            }
        }
    }
}