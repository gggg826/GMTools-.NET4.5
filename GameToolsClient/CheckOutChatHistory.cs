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
using Newtonsoft.Json.Linq;

namespace GameToolsClient
{
    public partial class CheckOutChatHistory : FengNiao.GameTools.Util.BaseForm
    {
        FengNiao.GMTools.Database.Model.tbl_server currentServer;
        public CheckOutChatHistory()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        //private void InitList(List<FengNiao.GMTools.Database.Model.ChatMessage> dataList)
        //{
        //    gvDataGridView.Rows.Clear();
        //    foreach (FengNiao.GMTools.Database.Model.ChatMessage item in dataList)
        //    {
        //        int i = gvDataGridView.Rows.Add(item.PlayerName, item.Time,item.Kind, item.ToRole_id, item.Message);
        //        gvDataGridView.Rows[i].Tag = item;
        //    }
        //}

        private void InitList(List<Result> dataList)
        {
            gvDataGridView.Rows.Clear();
            foreach (Result item in dataList)
            {
                int i = gvDataGridView.Rows.Add(item.rolename, item.lv, item.vip_lv, item.time, item.type==0?"世界":"私聊", item.content);
                gvDataGridView.Rows[i].Tag = item;
            }
        }

        private void GetChatCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                if (requestResult.IsSuccess)
                {
                    try
                    {
                        MesResultModel mesRusultMod = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<MesResultModel>(requestResult.Content);
                        InitList(mesRusultMod.result);
                    }
                    catch
                    {
                        CustomMessageBox.Error(this, "暂无聊天数据");
                    }
                    
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

        private List<FengNiao.GMTools.Database.Model.ChatMessage> GetChatList(string str)
        {
            string[] strList = str.Split('\n');
            List<FengNiao.GMTools.Database.Model.ChatMessage> list = new List<FengNiao.GMTools.Database.Model.ChatMessage>();
            for (int i = 0; i < strList.Length-1; i++)
            {
                FengNiao.GMTools.Database.Model.ChatMessage message = new FengNiao.GMTools.Database.Model.ChatMessage(strList[i]);
                list.Add(message);
            }
            return list;
        }

        private void CheckOutChatHistory_Load(object sender, EventArgs e)
        {

        }

        private void tbServer_ButtonCustomClick(object sender, EventArgs e)
        {
            ServerManager frmServerManager = new ServerManager();
            frmServerManager.IsSelector = true;
            if (frmServerManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbServer.Text = frmServerManager.SelectedServer.fld_server_name;
                tbServer.Tag = frmServerManager.SelectedServer;
                currentServer = frmServerManager.SelectedServer;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }

            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.CheckChat, HttpMethodType.Add);
            string strServerIP = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(serverData.fld_server_ip.ToString());
            strArgs = string.Format("{0}&ServerIP={1}", strArgs, strServerIP);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetChatCallback);
        }


        private Result currentSelectedModel;
        private void gvDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex > -1&& e.ColumnIndex == 6)
            {
                currentSelectedModel = (Result)gvDataGridView.Rows[e.RowIndex].Tag;
                GetInfo();
            }
        }



        private void GetInfo()
        {
            Dictionary<string, string> paramters = new Dictionary<string, string>();
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Transfer, HttpMethodType.Transfer);
            paramters.Add("cmd", "locklist");
            List<string> IDList = new List<string>();
            IDList.Add(currentSelectedModel.serverid.ToString());
            string strServerIDs = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<string>>(IDList);
            string strParameters = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<Dictionary<string, string>>(paramters);
            strArgs = string.Format("{0}&ServerID={1}&Paramters={2}", strArgs, strServerIDs, strParameters);
            try
            {
                CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, QueryCallback);
            }
            catch
            {
                CustomMessageBox.Error(this, "地址无效");
            }
        }

        private void QueryCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                if (requestResult.IsSuccess)
                {
                    try
                    {
                        List<OperateResultModel> resultModelList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<OperateResultModel>(requestResult.Content);
                        if (resultModelList.Count > 0 && resultModelList[0].IsSuccess)
                        {
                            CheckIfLocked(resultModelList[0].Content);
                        }
                        else
                        {
                            CustomMessageBox.Error(this, "获取封停账号列表失败\r\n系统错误");
                        }
                    }
                    catch
                    {
                        CustomMessageBox.Error(this, "获取封停账号列表失败\r\n系统错误");
                    }
                }
                else
                {
                    CustomMessageBox.Error(this, string.Format("获取封停账号列表失败\r\n{0}", requestResult.Content));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
            btnQuery.Enabled = true;
        }


        private void CheckIfLocked(string strContent)
        {
            JObject contentObj = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject(strContent);
            int errorcode = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(contentObj, "errorcode");
            if (errorcode == 0)
            {
                JArray lockRoleInfoList = FengNiao.GameTools.Json.Serialize.GetJsonObject<JArray>(contentObj, "result");
                for (int i = 0; i < lockRoleInfoList.Count; i++)
                {
                    JObject lockRoleInfo = lockRoleInfoList[i] as JObject;
                    bool lockdevice = FengNiao.GameTools.Json.Serialize.GetJsonObject<bool>(lockRoleInfo, "lockdevice");
                    string locktime = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(lockRoleInfo, "locktime");
                    int locktype = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(lockRoleInfo, "locktype");
                    string reason = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(lockRoleInfo, "reason");
                    string roleid = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(lockRoleInfo, "roleid");
                    string rolename = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(lockRoleInfo, "rolename");
                    string unlocktime = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(lockRoleInfo, "unlocktime");
                    if(roleid == currentSelectedModel.roleid)
                    {
                        LockRoleSender frmLockRoleSender = new LockRoleSender(currentServer, roleid, reason);
                        frmLockRoleSender.ShowDialog();
                        //if(frmLockRoleSender.ShowDialog() == System.Windows.Forms.DialogResult.OK|| frmLockRoleSender.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                        //{
                        //    this.Show();
                        //}

                        return;
                    }
                }
                LockRoleSender frmLockRoleSender1 = new LockRoleSender(currentServer, currentSelectedModel.roleid);
                frmLockRoleSender1.ShowDialog();
                //if (frmLockRoleSender1.ShowDialog() == System.Windows.Forms.DialogResult.OK|| frmLockRoleSender1.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                //{
                //    this.Show();
                //}
            }
            else
            {
                CustomMessageBox.Error(this, string.Format("获取封停账号列表失败\r\nerrorcode{1}", errorcode));
            }
        }
    }

    public class MesResultModel
    {
        public int errorcode { get; set; }

        public List<Result> result { get; set; }
    }

    public class Result
    {
        public string content{ get; set; }
        public int lv { get; set; }
        public string roleid { get; set; }
        public string rolename { get; set; }
        public int serverid { get; set; }
        public string time { get; set; }
        public int type { get; set; }
        public int vip_lv { get; set; }
    }
}
