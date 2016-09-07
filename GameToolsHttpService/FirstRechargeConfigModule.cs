﻿using System;
using System.Collections.Generic;
using System.Web;
using GameToolsCommon;
using System.Text;

namespace GameToolsHttpService
{
    public class FirstRechargeConfigModule : ProcessRequestBase
    {

        public FirstRechargeConfigModule(string tableName)
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
                FengNiao.GMTools.Database.Model.tbl_first_recharge_config configModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<FengNiao.GMTools.Database.Model.tbl_first_recharge_config>(strModel);
                if (configModel != null)
                {
                    FengNiao.GMTools.Database.BLL.tbl_first_recharge_config first_rechargeConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_first_recharge_config();
                    List<FengNiao.GMTools.Database.Model.tbl_first_recharge_config> configs = first_rechargeConfigBLL.GetModelList("server_id = " + configModel.server_id + " and recharge_record = " + configModel.recharge_record);
                    if (configs.Count == 0)
                    {
                        if (first_rechargeConfigBLL.Add(configModel))
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

                                        string strArgs = "cmd=reload_first_recharge";
                                        if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
                                        {
                                            serverIP = serverIP.ToUpper().Replace("HTTP://", "");
                                        }
                                        byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
                                        string strContent = Encoding.UTF8.GetString(bytes);

                                        HttpResultModel result = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<HttpResultModel>(strContent);
                                        if (result.errorcode != 0)
                                        {
                                            requestResult.IsSuccess = false;
                                            requestResult.Content = "活动已生效，但未能成功通知游戏服务器";
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                requestResult.IsSuccess = false;
                                requestResult.Content = "活动已生效，但未能成功通知游戏服务器";
                            }
                        }
                    }
                    else
                    {
                        requestResult.IsSuccess = false;
                        requestResult.Content = "该服务器已经配置过该活动";
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

        public string AddAll(HttpContext context)
        {
            ResultModel requestResult = new ResultModel();
            string strServerID = context.Request["ServerID"];
            int iServerID = -1;
            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            if (!int.TryParse(strServerID, out iServerID))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "参数错误";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            try
            {
                FengNiao.GMTools.Database.BLL.tbl_first_recharge firstrechargeBLL = new FengNiao.GMTools.Database.BLL.tbl_first_recharge();

                FengNiao.GMTools.Database.BLL.tbl_first_recharge_config FirstRechargeConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_first_recharge_config();

                List<FengNiao.GMTools.Database.Model.tbl_first_recharge> FirstRecharge = firstrechargeBLL.GetModelList("");

                List<OperateResultModel> resultModelList = new List<OperateResultModel>();
                for (int i = 0; i < FirstRecharge.Count; i++)
                {
                    List<FengNiao.GMTools.Database.Model.tbl_first_recharge_config> configs = FirstRechargeConfigBLL.GetModelList("server_id = " + strServerID + " and id = " + FirstRecharge[i].itemid);
                    if (configs.Count == 0)
                    {
                        FengNiao.GMTools.Database.Model.tbl_first_recharge_config configModel = new FengNiao.GMTools.Database.Model.tbl_first_recharge_config();
                        configModel.id = FirstRecharge[i].itemid.ToString();
                        configModel.server_id = iServerID;
                        configModel.isopen = 0;
                        configModel.starttime = DateTime.Now;
                        configModel.stoptime = DateTime.Now.AddDays(1);
                        if (FirstRechargeConfigBLL.Add(configModel))
                        {
                            OperateResultModel resultModel = new OperateResultModel();
                            resultModel.IsSuccess = true;
                            resultModel.Content = FirstRecharge[i].name.Trim() + "(" + FirstRecharge[i].itemid + ")" + "导入成功";
                            resultModelList.Add(resultModel);
                        }
                    }
                }
                requestResult.IsSuccess = true;
                requestResult.Content = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<OperateResultModel>>(resultModelList);
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
            string strServer = context.Request["server"];
            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            try
            {
                List<FengNiao.GMTools.Database.Model.tbl_first_recharge_config> newConfigList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<List<FengNiao.GMTools.Database.Model.tbl_first_recharge_config>>(strModel);
                string server_id = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strServer);
                FengNiao.GMTools.Database.BLL.tbl_first_recharge_config firstConfigConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_first_recharge_config();
                int server = int.Parse(server_id);
                firstConfigConfigBLL.DeleteByServer(server);
                if(newConfigList.Count != 0)
                {
                    foreach (FengNiao.GMTools.Database.Model.tbl_first_recharge_config configModel in newConfigList)
                    {
                        firstConfigConfigBLL.Add(configModel);
                    }
                }
                

                requestResult.IsSuccess = true;
                requestResult.Content = "";
                try
                {
                    FengNiao.GMTools.Database.BLL.tbl_server serverBLL = new FengNiao.GMTools.Database.BLL.tbl_server();
                    List<FengNiao.GMTools.Database.Model.tbl_server> serverList = serverBLL.GetModelList("fld_server_id="+ server_id);
                    if (serverList.Count > 0)
                    {
                        string serverIP = serverList[0].fld_server_ip;
                        if (!string.IsNullOrEmpty(serverIP))
                        {

                            //string strArgs = "cmd=reload_activity";
                            string strArgs = "cmd=reload_first_recharge";
                            if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
                            {
                                serverIP = serverIP.ToUpper().Replace("HTTP://", "");
                            }
                            byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
                            string strContent = Encoding.UTF8.GetString(bytes);

                            HttpResultModel result = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<HttpResultModel>(strContent);

                            if (result.errorcode != 0)
                            {
                                requestResult.IsSuccess = false;
                                requestResult.Content = "活动已生效，但未能成功通知游戏服务器";
                            }
                        }
                    }
                }
                catch
                {
                    requestResult.IsSuccess = false;
                    requestResult.Content = "活动已生效，但未能成功通知游戏服务器";
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
                List<FengNiao.GMTools.Database.Model.tbl_first_recharge_config> configModels = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_first_recharge_config>(strModel);
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

        private void Send(FengNiao.GMTools.Database.Model.tbl_server server, List<FengNiao.GMTools.Database.Model.tbl_first_recharge_config> models)
        {
            FengNiao.GMTools.Database.BLL.tbl_first_recharge_config ConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_first_recharge_config();

            ConfigBLL.DeleteByServer(server.fld_server_id);
            if (models != null)
            {
                foreach (FengNiao.GMTools.Database.Model.tbl_first_recharge_config item in models)
                {
                    FengNiao.GMTools.Database.Model.tbl_first_recharge_config model = item;
                    model.server_id = server.fld_server_id;
                    ConfigBLL.Add(model);
                }
            }
            try
            {
                string serverIP = server.fld_server_ip;
                if (!string.IsNullOrEmpty(serverIP))
                {
                    string strArgs = "cmd=reload_first_recharge";
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
    }
}