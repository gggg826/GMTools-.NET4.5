using GameToolsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GameToolsHttpService
{
    public class AsyncToOtherUtil
    {
        public class AsyncToOther
        {
            private System.Threading.Thread send;
            FengNiao.GMTools.Database.Model.tbl_server server;
            List<FengNiao.GMTools.Database.Model.tbl_recommend> models;
            public AsyncToOther(FengNiao.GMTools.Database.Model.tbl_server server, List<FengNiao.GMTools.Database.Model.tbl_recommend> models)
            {
                this.server = server;
                this.models = models;
                send = new System.Threading.Thread(Send);
                send.Start();
            }
            private void Send()
            {
                FengNiao.GMTools.Database.BLL.tbl_recommend ConfigBLL = new FengNiao.GMTools.Database.BLL.tbl_recommend();

                ConfigBLL.DeleteByServer(server.fld_server_id);
                if (models != null)
                {
                    foreach (FengNiao.GMTools.Database.Model.tbl_recommend item in models)
                    {
                        FengNiao.GMTools.Database.Model.tbl_recommend model = item;
                        model.server_id = server.fld_server_id;
                        ConfigBLL.Add(model);
                    }


                }
                try
                {
                    string serverIP = server.fld_server_ip;
                    if (!string.IsNullOrEmpty(serverIP))
                    {
                        string strArgs = "cmd=reload_activity_task";
                        if (serverIP.ToUpper().IndexOf("HTTP://") != -1)
                        {
                            serverIP = serverIP.ToUpper().Replace("HTTP://", "");
                        }
                        byte[] bytes = CustomWebRequest.Request(string.Format("http://{0}/gm", serverIP), strArgs, Encoding.UTF8);
                        string strContent = Encoding.UTF8.GetString(bytes);
                    }
                    data.asyncResult.IsSuccess = true;
                    data.asyncResult.Content += string.Format("{0}配置成功\r\n", server.fld_server_name);
                }
                catch
                {
                    data.asyncResult.IsSuccess = true;
                    data.asyncResult.Content += string.Format("{0}活动已生效，但未能成功通知游戏服务器\r\n", server.fld_server_name);
                }
            }
        }

        public class data
        {
            private static ResultModel _asyncResult = new ResultModel();

            public static ResultModel asyncResult
            {
                get
                {
                    return _asyncResult;
                }

                set
                {
                    _asyncResult = value;
                }
            }
        }
    }
}