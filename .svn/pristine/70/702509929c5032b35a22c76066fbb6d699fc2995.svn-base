using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FengNiao.GameTools.Util;
using FengNiao.GameToolsCommon;
using System.Net;
using GameToolsCommon;

namespace GameToolsClient
{
    public partial class RollingNoticeConfigManager : BaseForm
    {
        public RollingNoticeConfigManager()
        {
            InitializeComponent();
            this.Text = "跑马灯公告管理";
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsAcceptResize = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.TopMostBox = false;
            this.IsShowCaptionImage = false;
            this.IsShowCaptionText = false;
            this.IsTitleSplitLine = false;
            // loadingControl.Dock = DockStyle.Fill;
            this.Image = Properties.Resources.TK_2icon;
        }

        private FengNiao.GMTools.Database.Model.tbl_server CurrentServer;

        private void tbServer_ButtonCustomClick(object sender, EventArgs e)
        {
            ServerManager frmServerManager = new ServerManager();
            frmServerManager.IsSelector = true;
            if (frmServerManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbServer.Text = frmServerManager.SelectedServer.fld_server_name;
                tbServer.Tag = frmServerManager.SelectedServer;
                gvDataList.Rows.Clear();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //GetList();
            InitChannel();
        }


        private void GetList()
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }
            InitList(serverData);
        }

        private void InitList(FengNiao.GMTools.Database.Model.tbl_server serverData)
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Notice_Config, HttpMethodType.GetList);
            List<HttpInterfaceSqlParameter> requestParameters = new List<HttpInterfaceSqlParameter>();
            requestParameters.Add(new HttpInterfaceSqlParameter("server_id", serverData.fld_server_id, SqlCompareType.Equal));
            requestParameters.Add(new HttpInterfaceSqlParameter("`type`", 1, SqlCompareType.Equal));
            if (requestParameters.Count > 0)
            {
                string strParamter = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<HttpInterfaceSqlParameter>>(requestParameters);
                strArgs = string.Format("{0}&Parameter={1}", strArgs, strParamter);
            }
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallback);
            CurrentServer = serverData;
            btnQuery.Enabled = false;
        }


        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_notice_config> dataList)
        {
            gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.tbl_notice_config data in dataList)
            {
                string strContent = string.Empty;
                strContent = System.Text.RegularExpressions.Regex.Replace(data.content, "\\[[0-9a-f]+\\]", "");
                int rowIndex = gvDataList.Rows.Add(data.id, data.title, strContent,GetPackageName(data.package_record_id) ,data.interval, data.starttime, data.stoptime, data.deleted == 0 ? "启用" : "未启用", data.package_record_id);
                gvDataList.Rows[rowIndex].Tag = data;
                gvDataList.Rows[rowIndex].Cells[10].Value = Guid.NewGuid();
            }
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
                        List<FengNiao.GMTools.Database.Model.tbl_notice_config> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_notice_config>(requestResult.Content);
                        InitList(dataList);
                    }
                    else
                    {
                        CurrentServer = null;
                        CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResult.Content));
                    }
                }
                catch
                {
                    CurrentServer = null;
                    CustomMessageBox.Error(this, "服务器返回的数据无效");
                }
            }
            else
            {
                CurrentServer = null;
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
            btnQuery.Enabled = true;
        }

        private void gvDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 9)
            {
                FengNiao.GMTools.Database.Model.tbl_notice_config data = gvDataList.Rows[e.RowIndex].Tag as FengNiao.GMTools.Database.Model.tbl_notice_config;
                if (data != null)
                {
                    tbChannel frmNoticeConfigDetails = new tbChannel(CurrentServer, data);
                    if (frmNoticeConfigDetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        GetList();
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }
            tbChannel frmNoticeConfigDetails = new tbChannel(serverData);
            if (frmNoticeConfigDetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetList();
            }
        }

        private void RefreshLayout()
        {
            if (this.IsHandleCreated && gvDataList.IsHandleCreated)
            {
                panelParent.Left = base.ClientBounds.X;
                panelParent.Top = base.ClientBounds.Y;
                panelParent.Width = base.ClientBounds.Width;
                panelParent.Height = base.ClientBounds.Height;
            }
        }

        private void RollingNoticeConfigManager_Load(object sender, EventArgs e)
        {
            RefreshLayout();
        }

       

        //初始化Global.Channel
        private void InitChannel()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Package, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetChannelCallback);
        }

        private void GetChannelCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                if (requestResult.IsSuccess)
                {
                    List<FengNiao.GMTools.Database.Model.tbl_package> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_package>(requestResult.Content);
                    GlobalObject.PackageList = dataList;
                    GetList();
                }
                else
                {
                    CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResult.Content));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
        }

        private string GetPackageName(long value)
        {
            if (value == -1)
                return "全部";
            foreach (var item in GlobalObject.ChannelDir)
            {
                if (item.Key == value)
                {
                    return item.Value;
                }
            }
            return "错误";
            //string channel = "";
            //GlobalObject.ChannelDir.TryGetValue(key, out channel);
            //return channel;
        }

        private void labelX2_Click(object sender, EventArgs e)
        {

        }

        private void applytoothers_Click(object sender, EventArgs e)
        {
            AsyncToOtherServers frmAsyncToOtherServers = new AsyncToOtherServers(CurrentServer);
            if (frmAsyncToOtherServers.ShowDialog() == DialogResult.OK)
            {
                AsyncToOthers(frmAsyncToOtherServers);
                this.btnNew.Enabled = false;
                this.btnQuery.Enabled = false;
                this.applytoothers.Enabled = false;
            }
        }

        private void AsyncToOthers(AsyncToOtherServers async)
        {
            string strServers = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_server>>(async.serverList); ;
            string strArgs = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_notice_config>>(GetGridViewTags());
            string strMoudle = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Notice_Config, HttpMethodType.AsyncToOthers);
            strArgs = string.Format("{0}&Model={1}&ServerList={2}", strMoudle, System.Web.HttpUtility.UrlEncode(strArgs, Encoding.UTF8), System.Web.HttpUtility.UrlEncode(strServers, Encoding.UTF8));
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetCopyCallBack);
        }

        private void GetCopyCallBack(object sender, UploadDataCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                strContent = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<string>(strContent);
                CustomMessageBox.Info(this, strContent);
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }

            this.btnNew.Enabled = true;
            this.btnQuery.Enabled = true;
            this.applytoothers.Enabled = true;
        }

        private List<FengNiao.GMTools.Database.Model.tbl_notice_config> GetGridViewTags()
        {
            List<FengNiao.GMTools.Database.Model.tbl_notice_config> data = new List<FengNiao.GMTools.Database.Model.tbl_notice_config>();
            for (int i = 0; i < gvDataList.RowCount; i++)
            {
                data.Add(gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_notice_config);
            }
            return data;
        }




        //private void AsyncToOthers(AsyncToOtherServers async)
        //{
        //    List<FengNiao.GMTools.Database.Model.tbl_notice_config> otherList = new List<FengNiao.GMTools.Database.Model.tbl_notice_config>();
        //    foreach (FengNiao.GMTools.Database.Model.tbl_server other in async.serverList)
        //    {
        //        otherList.Clear();
        //        string strServerid = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(other.fld_server_id.ToString());
        //        string strServerIP = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(other.fld_server_ip.ToString());
        //        string strType = "1";

        //        for (int i = 0; i < gvDataList.RowCount; i++)
        //        {
        //            FengNiao.GMTools.Database.Model.tbl_notice_config config = gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_notice_config;
        //            config.server_id = other.fld_server_id;
        //            otherList.Add(config);
        //        }
        //        string strArgs = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_notice_config>>(otherList);
        //        string strMoudle = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Notice_Config, HttpMethodType.AsyncToOthers);
        //        strArgs = string.Format("{0}&Model={1}&serverid={2}&serverip={3}&type={4}", strMoudle, System.Web.HttpUtility.UrlEncode(strArgs, Encoding.UTF8), strServerid, strServerIP, strType);
        //        CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetCopyCallBack);

        //    }
        //}

        //private void GetCopyCallBack(object sender, UploadDataCompletedEventArgs e)
        //{

        //    if (e.Error == null)
        //    {
        //        try
        //        {
        //            string strContent = Encoding.UTF8.GetString(e.Result);
        //            ResultModel requestResultModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
        //            if (requestResultModel.IsSuccess)
        //            {
        //                CustomMessageBox.Info(this, requestResultModel.Content + "保存完毕");
        //            }
        //            else
        //            {
        //                CustomMessageBox.Error(this, string.Format("保存失败\r\n{0}", requestResultModel.Content));
        //            }
        //        }
        //        catch
        //        {
        //            CustomMessageBox.Error(this, "服务器返回的数据无效");
        //        }
        //    }
        //    else
        //    {
        //        CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
        //    }
        //}


    }
}
