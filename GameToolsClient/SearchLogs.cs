using FengNiao.GameToolsCommon;
using GameToolsCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GameToolsClient
{
    public partial class SearchLogs : FengNiao.GameTools.Util.BaseForm
    {
        public SearchLogs()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            dtStartDate.Value = DateTime.Now.AddDays(-7);
            dtStartTime.Value = DateTime.Now;
            dtStopDate.Value = DateTime.Now.AddDays(1);
            dtStopTime.Value = DateTime.Now;

            InitList();
        }

        private void InitList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Logs, HttpMethodType.GetList);
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
                        List<FengNiao.GMTools.Database.Model.tbl_log> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_log>(requestResult.Content);
                        GlobalObject.LogsList = dataList;
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

        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_log> dataList)
        {

            gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.tbl_log data in dataList)
            {
                gvDataList.Rows.Add(data.item_name,
                                    data.datetime,
                                    data.adminid,
                                    data.comment );
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitList();
        }

        private void btnQueryByTime_Click(object sender, EventArgs e)
        {
            List<FengNiao.GMTools.Database.Model.tbl_log> dataList = new List<FengNiao.GMTools.Database.Model.tbl_log>();

            DateTime starttime = DateTime.Parse(dtStartDate.Value.ToString("yyyy-MM-dd") + " " + dtStartTime.Value.ToString("HH:mm:ss"));
            DateTime stoptime = DateTime.Parse(dtStopDate.Value.ToString("yyyy-MM-dd") + " " + dtStopTime.Value.ToString("HH:mm:ss"));

            foreach (var item in GlobalObject.LogsList)
            {
                if(DateTime.Compare(DateTime.Now, starttime) > 0 && DateTime.Compare(DateTime.Now, stoptime) < 0)
                {
                    dataList.Add(item);
                }
            }
            InitList(dataList);
        }

        private void btnQueryByUser_Click(object sender, EventArgs e)
        {
            List<FengNiao.GMTools.Database.Model.tbl_log> dataList = new List<FengNiao.GMTools.Database.Model.tbl_log>();

            DateTime starttime = DateTime.Parse(dtStartDate.Value.ToString("yyyy-MM-dd") + " " + dtStartTime.Value.ToString("HH:mm:ss"));
            DateTime stoptime = DateTime.Parse(dtStopDate.Value.ToString("yyyy-MM-dd") + " " + dtStopTime.Value.ToString("HH:mm:ss"));

            foreach (var item in GlobalObject.LogsList)
            {
                if (item.adminid == tbUser.Text)
                {
                    dataList.Add(item);
                }
            }
            InitList(dataList);
        }
    }
}
