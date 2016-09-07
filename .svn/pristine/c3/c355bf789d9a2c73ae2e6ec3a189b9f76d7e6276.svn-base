using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FengNiao.GameTools.Util;
using FengNiao.GameToolsCommon;
using GameToolsCommon;
using System.Net;
using System.Collections;

namespace GameToolsClient
{
    public partial class RecommendManager : BaseForm
    {
        public RecommendManager()
        {
            InitializeComponent();
            this.Text = "活动管理";
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }
            //InitActivityList();
            RecommendDetails frmRecommendDetails = new RecommendDetails(serverData);
            if (frmRecommendDetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                GetList();
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetList();
        }

        private void GetList()
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }
            CurrentServer = serverData;
            InitActivityList();
        }

        private void InitList(FengNiao.GMTools.Database.Model.tbl_server serverData)
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Recommend, HttpMethodType.GetList);
            List<HttpInterfaceSqlParameter> requestParameters = new List<HttpInterfaceSqlParameter>();
            requestParameters.Add(new HttpInterfaceSqlParameter("server_id", serverData.fld_server_id, SqlCompareType.Equal));
            if (requestParameters.Count > 0)
            {
                string strParamter = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<HttpInterfaceSqlParameter>>(requestParameters);
                strArgs = string.Format("{0}&Parameter={1}", strArgs, strParamter);
            }
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallback);
            CurrentServer = serverData;
            btnQuery.Enabled = false;
        }


        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_recommend> dataList)
        {
            gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.tbl_recommend data in dataList)
            {
                int rowIndex = gvDataList.Rows.Add(data.record_id, data.id, GlobalObject.Recommends.ContainsKey(data.id) ? GlobalObject.Recommends[data.id] : "无效推荐", data.isopen == 1 ? "开启" : "未开启", data.starttime, data.stoptime, data.rank, data.queue);
                gvDataList.Rows[rowIndex].Tag = data;
                gvDataList.Rows[rowIndex].Cells[9].Value = Guid.NewGuid();
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
                        List<FengNiao.GMTools.Database.Model.tbl_recommend> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_recommend>(requestResult.Content);
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
            if (e.RowIndex > -1 && e.ColumnIndex == 8)
            {
                FengNiao.GMTools.Database.Model.tbl_recommend data = gvDataList.Rows[e.RowIndex].Tag as FengNiao.GMTools.Database.Model.tbl_recommend;
                if (data != null)
                {
                    RecommendDetails frmRecommendDetails = new RecommendDetails(CurrentServer, data);
                    if (frmRecommendDetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        GetList();
                    }
                }
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

        private void RecommendManager_Load(object sender, EventArgs e)
        {
            RefreshLayout();
        }


        
        private void InitActivityList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Activity, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetActivityListCallBack);
        }

        private void GetActivityListCallBack(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_activity> list = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_activity>(requestResult.Content);
                        GlobalObject.ActivityList = list;
                        InitRecommadList();
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
        
        
        
        
        
        
        
        
        //初始化GameObject.Recommad
        private void InitRecommadList()
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;

            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Activity_Config, HttpMethodType.GetList);
            List<HttpInterfaceSqlParameter> requestParameters = new List<HttpInterfaceSqlParameter>();
            requestParameters.Add(new HttpInterfaceSqlParameter("server_id", serverData.fld_server_id, SqlCompareType.Equal));
            if (requestParameters.Count > 0)
            {
                string strParamter = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<HttpInterfaceSqlParameter>>(requestParameters);
                strArgs = string.Format("{0}&Parameter={1}", strArgs, strParamter);
            }
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetRecommendListCallback);
        }
        private void GetRecommendListCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_activity_config> list = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_activity_config>(requestResult.Content);
                        List<FengNiao.GMTools.Database.Model.tbl_activity_config> temp = new List<FengNiao.GMTools.Database.Model.tbl_activity_config>();
                        foreach (FengNiao.GMTools.Database.Model.tbl_activity_config item in list)
                        {
                            if (item.isopen == 1)
                               temp.Add(item);
                        }
                        GlobalObject.ActivityConfigList = temp;


                        InitList(CurrentServer);


                        //FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
                        //RecommendDetails frmRecommendDetails = new RecommendDetails(serverData);
                        //if (frmRecommendDetails.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        //{
                        //    GetList();
                        //}



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
            string strArgs = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_recommend>>(GetGridViewTags());
            string strMoudle = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Recommend, HttpMethodType.AsyncToOthers);
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

        private List<FengNiao.GMTools.Database.Model.tbl_recommend> GetGridViewTags()
        {
            List<FengNiao.GMTools.Database.Model.tbl_recommend> data = new List<FengNiao.GMTools.Database.Model.tbl_recommend>();
            for (int i = 0; i < gvDataList.RowCount; i++)
            {
                data.Add(gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_recommend);
            }
            return data;
        }
    }
}
