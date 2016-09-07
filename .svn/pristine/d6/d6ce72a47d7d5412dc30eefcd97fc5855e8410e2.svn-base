using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameToolsCommon;
using System.Configuration;
using System.Text;

namespace GameToolsHttpService
{
    public class OrderModule :ProcessRequestBase
    {
        public OrderModule(string tableName):base(tableName)
        {

        }

        public override string Add(HttpContext context)
        {
            HttpResultModel httpResultModel = new HttpResultModel();

            ResultModel requestResult = new ResultModel();
            string strRoleID = context.Request["RoleID"];
            string strStartTime = context.Request["StartTime"];
            string strStopTime = context.Request["StopTime"];

            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            try
            {
                string roleID = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strRoleID);
                string beginTime = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strStartTime);
                string endTime = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strStopTime);
                if (!string.IsNullOrEmpty(roleID)&& !string.IsNullOrEmpty(beginTime)&& !string.IsNullOrEmpty(endTime))
                {
                    try
                    {
                        string orderServerIP = ConfigurationManager.AppSettings["OrdersServerIP"];
                        if (!string.IsNullOrEmpty(orderServerIP))
                        {

                            string strArgs = "cmd=query_role_orders";
                            strArgs = string.Format("{0}&roleid={1}&begintime={2}&endtime={3}", strArgs, roleID, beginTime, endTime);
                            if (orderServerIP.ToUpper().IndexOf("HTTP://") != -1)
                            {
                                orderServerIP = orderServerIP.ToUpper().Replace("HTTP://", "");
                            }
                            byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", orderServerIP), strArgs, Encoding.UTF8);
                            string strContent = Encoding.UTF8.GetString(bytes);
                            return strContent;
                        }
                    }
                    catch
                    {
                        httpResultModel.errorcode = -1;
                        httpResultModel.result = "查询失败";
                        //requestResult.IsSuccess = false;
                        //requestResult.Content = "活动已生效，但未能成功通知游戏服务器" + "(" + serverid + ")";
                    }
                }
            }
            catch
            {
                httpResultModel.errorcode = -1;
                httpResultModel.result = "查询失败";
                //requestResult.IsSuccess = false;
                //requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<HttpResultModel>(httpResultModel);
        }

        public override string Update(HttpContext context)
        {
            HttpResultModel httpResultModel = new HttpResultModel();

            ResultModel requestResult = new ResultModel();
            string strRoleID = context.Request["RoleID"];

            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            try
            {
                string roleID = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strRoleID);
                if (!string.IsNullOrEmpty(roleID))
                {
                    try
                    {
                        string orderServerIP = ConfigurationManager.AppSettings["OrdersServerIP"];
                        if (!string.IsNullOrEmpty(orderServerIP))
                        {

                            string strArgs = "cmd=retry_order";
                            strArgs = string.Format("{0}&roleid={1}", strArgs, roleID);
                            if (orderServerIP.ToUpper().IndexOf("HTTP://") != -1)
                            {
                                orderServerIP = orderServerIP.ToUpper().Replace("HTTP://", "");
                            }
                            byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", orderServerIP), strArgs, Encoding.UTF8);
                            string strContent = Encoding.UTF8.GetString(bytes);
                            return strContent;
                        }
                    }
                    catch
                    {
                        httpResultModel.errorcode = -1;
                        httpResultModel.result = "查询失败";
                        //requestResult.IsSuccess = false;
                        //requestResult.Content = "活动已生效，但未能成功通知游戏服务器" + "(" + serverid + ")";
                    }
                }
            }
            catch
            {
                httpResultModel.errorcode = -1;
                httpResultModel.result = "查询失败";
                //requestResult.IsSuccess = false;
                //requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<HttpResultModel>(httpResultModel);
        }

        public override string SendRequest(HttpContext context)
        {
            HttpResultModel httpResultModel = new HttpResultModel();

            ResultModel requestResult = new ResultModel();
            string strModel = context.Request["Model"];
            string strUser = context.Request["User"];

            if (!base.CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }


            

            try
            {
                FengNiao.GMTools.Database.Model.SupplementOrders model = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<FengNiao.GMTools.Database.Model.SupplementOrders>(strModel);
                FengNiao.GMTools.Database.BLL.tbl_server serverBLL = new FengNiao.GMTools.Database.BLL.tbl_server();
                

                List<FengNiao.GMTools.Database.Model.tbl_server> server = serverBLL.GetModelList("fld_server_id = " + model.serverid);
                if (model != null)
                {
                    try
                    {
                        string ServerIP = ConfigurationManager.AppSettings["OrdersServerIP"];
                        //string ServerIP = server[0].fld_server_ip;
                        if (!string.IsNullOrEmpty(ServerIP))
                        {

                            string strArgs = "cmd=recharge";
                            strArgs = string.Format("{0}&roleid={1}&itemid={2}&price={3}&channel={4}&serverid={5}&orderno={6}", 
                                                    strArgs, 
                                                    model.roleid,
                                                    model.itemid,
                                                    model.price,
                                                    model.channel,
                                                    model.serverid,
                                                    model.orderno);
                            if (ServerIP.ToUpper().IndexOf("HTTP://") != -1)
                            {
                                ServerIP = ServerIP.ToUpper().Replace("HTTP://", "");
                            }
                            byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", ServerIP), strArgs, Encoding.UTF8);
                            string strContent = Encoding.UTF8.GetString(bytes);


                            FengNiao.GMTools.Database.Model.tbl_log logModel = new FengNiao.GMTools.Database.Model.tbl_log();
                            logModel.adminid = strUser;
                            logModel.item_name = "补单";
                            logModel.comment = string.Format("补单号:{0}", model.orderno);
                            logModel.datetime = System.DateTime.Now;


                            FengNiao.GMTools.Database.BLL.tbl_log logBLL = new FengNiao.GMTools.Database.BLL.tbl_log();
                            if (FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<HttpResultModel>(strContent).errorcode == 0)
                                logBLL.Add(logModel);
                            return strContent;
                        }
                    }
                    catch
                    {
                        httpResultModel.errorcode = -1;
                        httpResultModel.result = "补单失败";
                    }
                }
            }
            catch
            {
                httpResultModel.errorcode = -1;
                httpResultModel.result = "补单失败";
            }
            
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<HttpResultModel>(httpResultModel);
        }

    }
}