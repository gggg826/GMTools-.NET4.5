using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FengNiao.GameToolsCommon;
using GameToolsCommon;
using System.Net;

namespace GameToolsClient
{
    public partial class BaiduPushDetail : FengNiao.GameTools.Util.BaseForm
    {
        private bool isNew = true;
        private FengNiao.GMTools.Database.Model.tbl_baidupush currentBaiduPush = new FengNiao.GMTools.Database.Model.tbl_baidupush();
        public BaiduPushDetail()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.labelX2.Text = "新建推送通知";
            this.tbTitle.Text = "";
            this.tbContext.Text = "";
            this.dtStartDate.Value = DateTime.Now.Date;
            this.dtStopDate.Value = DateTime.Now.Date;
        }

        public BaiduPushDetail(FengNiao.GMTools.Database.Model.tbl_baidupush model)
        :this()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.labelX2.Text = "编辑推送通知";
            this.tbTitle.Text = model.tile;
            this.tbContext.Text = model.context;
            this.dtStartDate.Value = (DateTime)model.startime;
            this.dtStopDate.Value = (DateTime)model.stoptime;
            this.dtStartTime.Value = (DateTime)model.pushtime;
            this.currentBaiduPush = model;
            this.isNew = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_baidupush model = new FengNiao.GMTools.Database.Model.tbl_baidupush();

            model.tile = this.tbTitle.Text;

            if(this.tbContext.Text == null)
            {
                CustomMessageBox.Error(this, "请输入通知的内容");
                return;
            }
            model.context = this.tbContext.Text;

            DateTime startdate = DateTime.Parse(dtStartDate.Value.ToString("yyyy-MM-dd"));
            DateTime stopdate = DateTime.Parse(dtStopDate.Value.ToString("yyyy-MM-dd"));
            if (DateTime.Compare(startdate, stopdate) > -1)
            {
                CustomMessageBox.Error(this, "活动结束时间必须大于开始时间");
                return;
            }
            model.startime = startdate;
            model.stoptime = stopdate;

            DateTime sendTime = DateTime.Parse(dtStartTime.Value.ToString("HH:mm:ss"));

            model.pushtime = sendTime;

            if(currentBaiduPush != null)
            {
                model.record_id = currentBaiduPush.record_id;
                model.mes_id = currentBaiduPush.mes_id;
                model.timer_id = currentBaiduPush.timer_id;
            }
            string strModel = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson(model);

            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.PushNotice, isNew?HttpMethodType.Add:HttpMethodType.Update);
            strArgs = string.Format("{0}&Model={1}", strArgs, System.Web.HttpUtility.UrlEncode(strModel, Encoding.UTF8));
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetPushCallback);
            this.btnSend.Enabled = false;
        }

        private void GetPushCallback(object sender, UploadDataCompletedEventArgs e)
        {
            //CustomMessageBox.Info(this, e.Result.ToString());
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResultModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResultModel.IsSuccess)
                    {
                        CustomMessageBox.Info(this, "保存成功");
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResultModel.Content));
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
    }
}
