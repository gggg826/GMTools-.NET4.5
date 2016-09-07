using System;
using System.Collections.Generic;
using System.Web;
using GameToolsCommon;
using System.Text;
using System.Reflection;

namespace GameToolsHttpService
{
    public class NoticeConfigModule : ProcessRequestBase
    {
        public NoticeConfigModule(string tableName)
            : base(tableName)
        {
        }

        public override string Add(HttpContext context)
        {
            return CommitNotice(context, "Add");
        }

        public override string Update(HttpContext context)
        {
            return CommitNotice(context, "Update");
        }


        private string CommitNotice(HttpContext context, string method)
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
                FengNiao.GMTools.Database.Model.tbl_notice_config configModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<FengNiao.GMTools.Database.Model.tbl_notice_config>(strModel);
                if (configModel != null)
                {
                    FengNiao.GMTools.Database.BLL.tbl_notice_config noticeConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_notice_config();
                    MethodInfo methodInfo = noticeConfigBLL.GetType().GetMethod(method);
                    if (methodInfo != null)
                    {
                        object[] param = new object[] { configModel };
                        object invokeResult = methodInfo.Invoke(noticeConfigBLL, param);
                        if ((bool)invokeResult)
                        {
                            requestResult.IsSuccess = true;
                            requestResult.Content = "";
                            try
                            {
                                FengNiao.GMTools.Database.BLL.tbl_server serverBLL = new FengNiao.GMTools.Database.BLL.tbl_server();
                                List<FengNiao.GMTools.Database.Model.tbl_server> serverList = serverBLL.GetModelList("fld_server_id=" + configModel.server_id.ToString());
                                if (serverList.Count > 0)
                                {
                                    string serverIP = serverList[0].fld_server_ip;
                                    if (!string.IsNullOrEmpty(serverIP))
                                    {

                                        string strArgs = "cmd=reload_notice";
                                        if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
                                        {
                                            serverIP = serverIP.ToUpper().Replace("HTTP://", "");
                                        }
                                        byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
                                        string strContent = Encoding.UTF8.GetString(bytes);
                                    }
                                }
                            }
                            catch
                            {
                                requestResult.IsSuccess = false;
                                requestResult.Content = "公告已生效，但未能成功通知游戏服务器";
                            }
                        }
                    }
                    else
                    {
                        requestResult.IsSuccess = false;
                        requestResult.Content = "不支持的操作";
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
            ResultModel requestResult = new ResultModel();
            string strModel = context.Request["Model"];
            string strServerList = context.Request["ServerList"];
            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }

            try
            {
                List<FengNiao.GMTools.Database.Model.tbl_notice_config> configModels = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_notice_config>(strModel);
                List<FengNiao.GMTools.Database.Model.tbl_server> serverList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_server>(strServerList);

                AsyncResult.content = string.Empty;
                foreach (var item in serverList)
                {

                    Send(item, configModels);
                }
            }
            catch
            {
                AsyncResult.content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(AsyncResult.content);
        }

        private void Send(FengNiao.GMTools.Database.Model.tbl_server server, List<FengNiao.GMTools.Database.Model.tbl_notice_config> models)
        {

            FengNiao.GMTools.Database.BLL.tbl_notice_config ConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_notice_config();
            ConfigBLL.DeleteByServer(server.fld_server_id, models[0].type);
            if (models != null)
            {
                foreach (FengNiao.GMTools.Database.Model.tbl_notice_config item in models)
                {
                    FengNiao.GMTools.Database.Model.tbl_notice_config model = item;
                    model.server_id = server.fld_server_id;
                    ConfigBLL.Add(model);
                }
            }
            try
            {
                string serverIP = server.fld_server_ip;
                if (!string.IsNullOrEmpty(serverIP))
                {
                    string strArgs = "cmd=reload_notice";
                    if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
                    {
                        serverIP = serverIP.ToUpper().Replace("HTTP://", "");
                    }
                    byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
                    string strContent = Encoding.UTF8.GetString(bytes);
                }
                AsyncResult.content += string.Format("{0}配置成功\r\n", server.fld_server_name);
            }
            catch
            {
                AsyncResult.content += string.Format("{0}活动已生效，但未能成功通知游戏服务器\r\n", server.fld_server_name);
            }
        }







        //public override string AsyncToOthers(HttpContext context)
        //{
        //    ResultModel requestResult = new ResultModel();
        //    string strModel = context.Request["Model"];
        //    string strServerid = context.Request["serverid"];
        //    string strServerip = context.Request["serverip"];
        //    string strType = context.Request["type"];
        //    if (!base.CheckUserSession(context))
        //    {
        //        requestResult.IsSuccess = false;
        //        requestResult.Content = "请登陆后操作";
        //        return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
        //    }
        //    string serverid = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strServerid);
        //    string serverip = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strServerip);

        //    try
        //    {
        //        List<FengNiao.GMTools.Database.Model.tbl_notice_config> configModels = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_notice_config>(strModel);

        //        FengNiao.GMTools.Database.BLL.tbl_notice_config ConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_notice_config();

        //        ConfigBLL.DeleteByServer(int.Parse(serverid), int.Parse(strType));
        //        if (configModels != null)
        //        {
        //            foreach (FengNiao.GMTools.Database.Model.tbl_notice_config item in configModels)
        //                ConfigBLL.Add(item);

        //        }
        //        requestResult.IsSuccess = true;
        //        requestResult.Content = serverid;


        //        try
        //        {
        //            string serverIP = serverip;
        //            if (!string.IsNullOrEmpty(serverIP))
        //            {
        //                string strArgs = "cmd=reload_activity_task";
        //                if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
        //                {
        //                    serverIP = serverIP.ToUpper().Replace("HTTP://", "");
        //                }
        //                byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
        //                string strContent = Encoding.UTF8.GetString(bytes);
        //            }
        //        }
        //        catch
        //        {
        //            requestResult.IsSuccess = false;
        //            requestResult.Content = "活动已生效，但未能成功通知游戏服务器" + "(" + serverid + ")";
        //        }
        //    }
        //    catch
        //    {
        //        requestResult.IsSuccess = false;
        //        requestResult.Content = "(" + serverid + ")";
        //    }
        //    return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);

        //}
    }
}