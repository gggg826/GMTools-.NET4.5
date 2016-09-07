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
    public partial class AsyncToOtherServers : FengNiao.GameTools.Util.BaseForm
    {
        public List<FengNiao.GMTools.Database.Model.tbl_server> serverList = new List<FengNiao.GMTools.Database.Model.tbl_server>();

        private FengNiao.GMTools.Database.Model.tbl_server currentServer ;
        private FengNiao.GMTools.Database.Model.tbl_activity_solution currentSolution;
        public AsyncToOtherServers(FengNiao.GMTools.Database.Model.tbl_server server)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            currentServer = server;
        }

        public AsyncToOtherServers(FengNiao.GMTools.Database.Model.tbl_server server, FengNiao.GMTools.Database.Model.tbl_activity_solution solution)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            currentSolution = solution;
            currentServer = server;
        }

        private void AsyncToOtherServers_Load(object sender, EventArgs e)
        {
            GetList();
        }

        private void GetList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Server, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallBack);
        }

        private void GetListCallBack(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_server_group> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_server_group>(requestResult.Content);
                        InitServerList(GlobalObject.ServerList, dataList);
                    }
                    else
                        CustomMessageBox.Error(this, string.Format("获取数据失败\r\rn{0}", requestResult.Content));
                }
                catch
                {
                    CustomMessageBox.Error(this, "服务器返回的数据无效");
                }
            }
            else
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
        }

        private void InitServerList(List<FengNiao.GMTools.Database.Model.tbl_server> list, List<FengNiao.GMTools.Database.Model.tbl_server_group> groupList)
        {
            GlobalObject.ServerList = list;
            GlobalObject.ServerGroupList = groupList;
            if(groupList!=null)
            {
                gvDataList.Rows.Clear();
                foreach (FengNiao.GMTools.Database.Model.tbl_server data in GlobalObject.ServerList)
                {
                    if (data.fld_server_ip == currentServer.fld_server_ip || data.fld_isvisible == 0)
                        continue;
                    InitGridView(data);
                }
            }
            else
            {
                string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Server_Group, HttpMethodType.GetList);
                CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetServerGroupListCallback);
            }
        }

        private void InitGridView(FengNiao.GMTools.Database.Model.tbl_server data)
        {
            int rowIndex = gvDataList.Rows.Add(data.fld_record_id, data.fld_server_name, data.fld_server_id);
            gvDataList.Rows[rowIndex].Cells[3] = GlobalObject.GetServerGroupCell();
            if (GlobalObject.IsExistsServerGroup(data.fld_group_id))
            {
                gvDataList.Rows[rowIndex].Cells[3].Value = data.fld_group_id;
            }
            gvDataList.Rows[rowIndex].Cells[1].Value = data.fld_server_name;
            gvDataList.Rows[rowIndex].Cells[2].Value = data.fld_server_id;
            gvDataList.Rows[rowIndex].Tag = data;
            if(currentSolution != null)
            {
                if (isSelected(data))
                {
                    gvDataList.Rows[rowIndex].Cells[5].Value = true;
                }
            }
        }

        private bool isSelected( FengNiao.GMTools.Database.Model.tbl_server server)
        {
            string[] servers = currentSolution.fld_servers.Split(';');
            for (int i = 0; i < servers.Length; i++)
            {
                if (servers[i] == server.fld_server_id.ToString())
                    return true;
            }
            return false;
        }

        private void GetServerGroupListCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_server_group> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_server_group>(requestResult.Content);
                        InitServerList(GlobalObject.ServerList, dataList);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            AddServerSelected();
            if(serverList.Count < 1)
            {
                CustomMessageBox.Error(this, "请选择至少一个服务器");
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void AddServerSelected()
        {
            for (int i = 0; i < gvDataList.RowCount; i++)
            {
                if ((bool)gvDataList.Rows[i].Cells[5].EditedFormattedValue)
                {
                    serverList.Add(gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_server);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
