using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Text;
using System.Windows.Forms;
using FengNiao.GameToolsCommon;
using GameToolsCommon;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameToolsClient
{
    public partial class BaiduPushManager : FengNiao.GameTools.Util.BaseForm
    {
        public BaiduPushManager()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            InitList();
        }


        private void InitList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.PushNotice, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallback);
        }

        private void GetListCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_baidupush> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_baidupush>(requestResult.Content);
                        InitList(dataList);
                    }
                    else
                    {
                        CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResult.Content));
                    }
                }
                catch
                {
                    CustomMessageBox.Error(this, "服务器返回的数据无效");
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
        }

        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_baidupush> dataList)
        {
            this.gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.tbl_baidupush item in dataList)
            {
                int i = gvDataList.Rows.Add(item.record_id, item.tile, item.context, item.startime, item.stoptime, item.pushtime.Value.ToString("HH:mm:ss"));
                gvDataList.Rows[i].Tag = item;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            BaiduPushDetail frmBaiduPushDedail = new BaiduPushDetail();
            if (frmBaiduPushDedail.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                InitList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitList();
        }

        private void gvDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.ColumnIndex == 6)
            {
                BaiduPushDetail frmBaiduPushDetail = new BaiduPushDetail(gvDataList.Rows[e.RowIndex].Tag as FengNiao.GMTools.Database.Model.tbl_baidupush);
                if (frmBaiduPushDetail.ShowDialog() == DialogResult.OK)
                {
                    InitList();
                }
            }

            if (e.ColumnIndex > -1 && e.ColumnIndex == 8)
            {
                FengNiao.GMTools.Database.Model.tbl_baidupush model = (FengNiao.GMTools.Database.Model.tbl_baidupush)(gvDataList.Rows[e.RowIndex].Tag);
                if (string.IsNullOrEmpty(model.mes_id))
                {
                    CustomMessageBox.Info(this, "任务还未开始，请明天再查");
                    return;
                }
                string strTeamID = model.record_id.ToString();
                string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.PushNotice, HttpMethodType.AsyncToOthers);
                strArgs = string.Format("{0}&Record_id={1}", strArgs, strTeamID);
                CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetQueryCallback);
            }
        }
        private void GetQueryCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        string resultText = string.Empty;

                        ////FengNiao.GMTools.Database.Model.QueryNoticeResultModel mod = new FengNiao.GMTools.Database.Model.QueryNoticeResultModel();
                        //Newtonsoft.Json.Linq.JObject contentObj = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject(requestResult.Content);
                        //int request_id = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(contentObj, "request_id");
                        //JToken response_params = contentObj["response_params"] as JToken;
                        ////mod.request_id = request_id;

                        //int total_num = response_params["total_num"].Value<int>();
                        ////mod.response_params.total_num = total_num;

                        //JArray resultList = response_params["result"].Value<JArray>();
                        //for (int i = 0; i < resultList.Count; i++)
                        //{
                        //    //FengNiao.GMTools.Database.Model.Result resultObj = new FengNiao.GMTools.Database.Model.Result();
                        //    JToken result = resultList[i];
                        //    JToken resultObj1 = result["send_time"];
                        //    int send_time = result["send_time"].Value<int>();
                        //    int success = result["success"].Value<int>();
                        //    int t_total_num = result["total_num"].Value<int>();
                        //    string msg_id = result["msg_id"].Value<string>();
                        //    int stutas = result["status"].Value<int>();
                        //    //resultObj.send_time = send_time;
                        //    //resultObj.success = success;
                        //    //resultObj.total_num = t_total_num;
                        //    //resultObj.msg_id = msg_id;
                        //    //mod.response_params.result.Add(resultObj);

                        //    resultText += string.Format("状态:{2}  总共:{0}  成功:{1}  实际发送时间:{3}\r\n",
                        //                                t_total_num, success, GetStatus(stutas), ConvertIntDateTime(send_time));
                        //}
                        

                        if(requestResult.Content.Contains("Data Required Not Found"))
                        {
                            CustomMessageBox.Error(this, "该任务已过时或取消！\r\n请更新任务");
                            return;
                        }
                        FengNiao.GMTools.Database.Model.QueryNoticeResultModel mod1 = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<FengNiao.GMTools.Database.Model.QueryNoticeResultModel>(requestResult.Content);
                        //FengNiao.GMTools.Database.Model.QueryNoticeResultModel mod1 = JsonConvert.DeserializeObject<FengNiao.GMTools.Database.Model.QueryNoticeResultModel>(requestResult.Content);

                        var response = requestResult.Content;


                        foreach (var item in mod1.response_params.result)
                        {
                            resultText += string.Format("状态:{2}   发送:{0}   接收:{1}   实际发送时间:{3}\r\n",
                                                        item.total_num, item.success, GetStatus(item.status), FengNiao.GameTools.Util.TimeStamp.ConvertIntDateTime(item.send_time));
                        }
                        CustomMessageBox.Info(this, resultText);
                    } 
                    else
                    {
                        CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResult.Content));
                    }
                }
                catch
                {
                    CustomMessageBox.Error(this, string.Format("暂无数据,请稍后查询\r\n"));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
        }

       private string GetStatus(int status)
        {
            switch(status)
            {
                case 0:
                    return "已发送";
                case 1:
                    return "未发送";
                case 2:
                    return "正在发送";
                case 3:
                    return "失败";
            }
            return "错误";
        }

        public string ConvertIntDateTime(int d)
        {
            System.DateTime time;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
