﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FengNiao.GameTools.Util;
using System.Net;
using FengNiao.GameToolsCommon;
using DevComponents.DotNetBar;
using GameToolsCommon;
using FengNiao.GameTools.Json;
using System.Web;

namespace GameToolsClient
{
    public partial class CountManagers : FengNiao.GameTools.Util.BaseForm
    {


        Dictionary<int, string> CountsModleDir = new Dictionary<int, string>();
        FengNiao.GMTools.Database.Model.tbl_server CurrentServer = new FengNiao.GMTools.Database.Model.tbl_server();
        //bool isGetCountsDir = false;

        public CountManagers()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lbSaving.BringToFront();
            this.lbSaving.Visible = false;
            SetButtonStatus(false);
            loadingControl.Visible = false;
        }




        //选择服务器
        //====================================================================================================================
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
        //====================================================================================================================




        //查询
        //--------------------------------------------------------------------------------------------//

        #region
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
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
            InitList(serverData);
        }

        #endregion


        //--------------------------------------------------------------------------------------------//






        //初始化
        //====================================================================================================================
        #region
        private void CountManagers_Load(object sender, EventArgs e)
        {
            InitModleDir();
        }


        //初始化CountsModleDir字典
        private void InitModleDir()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.ProtoCount, HttpMethodType.GetList);

            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetCountModelCallBack);
        }


        //初始化CountsModleDir字典
        private void GetCountModelCallBack(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_proto_cout> list = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_proto_cout>(requestResult.Content);
                        foreach (FengNiao.GMTools.Database.Model.tbl_proto_cout item in list)
                        {
                            CountsModleDir.Add(item.proteo_id, item.button_name);
                        }
                        GlobalObject.ProtoList = list;

                        InitComboBoxColumn();
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


        //初始化DataGridView
        private void InitList(FengNiao.GMTools.Database.Model.tbl_server server)
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.CountsConfig, HttpMethodType.GetList);
            List<HttpInterfaceSqlParameter> requestParameters = new List<HttpInterfaceSqlParameter>();
            requestParameters.Add(new HttpInterfaceSqlParameter("serverID", server.fld_server_id, SqlCompareType.Equal));
            if (requestParameters.Count > 0)
            {
                string strParamter = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<HttpInterfaceSqlParameter>>(requestParameters);
                strArgs = string.Format("{0}&Parameter={1}", strArgs, strParamter);
            }
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallback);
            CurrentServer = server;
        }


        //初始化DataGridView
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
                        List<FengNiao.GMTools.Database.Model.tbl_counts_config> list = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_counts_config>(requestResult.Content);
                        GlobalObject.CountsList = list;
                        InitList(GlobalObject.CountsList);
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


        //初始化DataGridView
        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_counts_config> countList)
        {
            this.btnSave.Enabled = true;
            gvDataList.Rows.Clear();
            setDeleteButton(true);
            gvDataList.Columns[1].ReadOnly = true;
            if (countList.Count != 0)
            {
                foreach (FengNiao.GMTools.Database.Model.tbl_counts_config counts in countList)
                {
                    string kind = string.Empty;
                    if (counts.kindofrewards == 0)
                        kind = "全部领取";
                    else if (counts.kindofrewards == 1)
                        kind = "四选一";

                    string name = string.Empty;
                    if (CountsModleDir.TryGetValue(counts.item_id, out name))
                    {
                        int rowIndex = gvDataList.Rows.Add(name + "-" + counts.item_id, counts.record_id, counts.count, counts.dec, kind, counts.reward1, counts.count1, counts.reward2, counts.count2, counts.reward3, counts.count3, counts.reward4, counts.count4, counts.dataid);
                        //gvDataList.Rows[rowIndex].Cells[1].Value = name;
                        gvDataList.Rows[rowIndex].Tag = counts;
                        gvDataList.Rows[rowIndex].Cells[15].Value = Guid.NewGuid();
                    }
                    else
                    {
                        CustomMessageBox.Error(this, "初始化数据失败");
                        return;
                    }
                }
            }
            btnSave.Enabled = true;
            btnNew.Enabled = true;
            //InitComboBoxColumn();

        }


        //初始化活动名称列
        private void InitComboBoxColumn()
        {
            DataGridViewComboBoxColumn colShow = new DataGridViewComboBoxColumn();
            colShow.Name = "item_name";
            colShow.HeaderText = "活动名称";
            colShow.Width = 200;
            foreach (var item in CountsModleDir)
            {
                colShow.Items.Add(item.Value + "-" + item.Key);
            }
            colShow.DisplayIndex = 0;
            colShow.Width = 120;
            gvDataList.Columns.Insert(0, colShow);

            DataGridViewComboBoxColumn kindShow = new DataGridViewComboBoxColumn();
            kindShow.Name = "kindofReward";
            kindShow.HeaderText = "奖励类型";
            kindShow.Width = 200;
            kindShow.Items.Add("全部领取");
            kindShow.Items.Add("四选一");
            kindShow.Width = 80;
            gvDataList.Columns.Insert(4, kindShow);

        }

        #endregion
        //====================================================================================================================



        //添加活动按钮
        //====================================================================================================================
        private void btnNew_Click(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }
            int index = gvDataList.Rows.Add();
            gvDataList.Rows[index].Cells[15].Value = Guid.NewGuid();
        }
        //====================================================================================================================
        
        //保存活动按钮
        //====================================================================================================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }
            string strErrorMsg = string.Empty;
            
            List<string> guidList = new List<string>();
            List<OperationType> operationTypes = new List<OperationType>();
            List<FengNiao.GMTools.Database.Model.tbl_counts_config> dataList = new List<FengNiao.GMTools.Database.Model.tbl_counts_config>();
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                //string str_item_name;
                //isError |= CheckAndGetData(i, 0, "活动ID无效", out str_item_name, false);
                //int item_name = int.Parse(str_item_name);

                //string str_record_id;
                //isError |= CheckAndGetData(i, 1, "record_id无效", out str_record_id, false);
                //int record_id = int.Parse(str_record_id);

                //string str_count;
                //isError |= CheckAndGetData(i, 2, "目标数量无效", out str_count, false);
                //int count = int.Parse(str_count);

                //string str_dec;
                //isError |= CheckAndGetData(i, 3, "描述无效", out str_dec, false);
                //int dec = int.Parse(str_dec);
                

                int cellIndex = 0;
                int item_name = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    string tempname = gvDataList.Rows[i].Cells[cellIndex].Value.ToString();
                    item_name = int.Parse(tempname.Substring(tempname.LastIndexOf("-") + 1));
                }
                else
                {
                    CustomMessageBox.Error(this, "请选择活动ID");
                    return;
                }

                cellIndex++;
                int record_id = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    record_id = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }

                cellIndex++;
                int count = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    count = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                else
                {
                    CustomMessageBox.Error(this, "请填写目标数量");
                    return;
                }

                cellIndex++;
                string dec = string.Empty;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    dec = gvDataList.Rows[i].Cells[cellIndex].Value.ToString();
                }
                else
                {
                    CustomMessageBox.Error(this, "请填写描述");
                    return;
                }

                cellIndex++;
                int kindofReward = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    if (gvDataList.Rows[i].Cells[cellIndex].Value.ToString() == "全部领取")
                        kindofReward = 0;
                    else if (gvDataList.Rows[i].Cells[cellIndex].Value.ToString() == "四选一")
                        kindofReward = 1;
                }
                else
                {
                    CustomMessageBox.Error(this, "请选择奖励类型");
                    return;
                }

                cellIndex++;
                int reward1 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    reward1 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }

                cellIndex++;
                int count1 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    count1 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                cellIndex++;
                int reward2 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    reward2 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                cellIndex++;
                int count2 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    count2 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }

                cellIndex++;
                int reward3 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    reward3 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                cellIndex++;
                int count3 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    count3 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                cellIndex++;
                int reward4 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    reward4 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                cellIndex++;
                int count4 = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    count4 = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }
                cellIndex++;
                int dataid = 0;
                if (gvDataList.Rows[i].Cells[cellIndex].Value != null)
                {
                    dataid = int.Parse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                }

                string guid = gvDataList.Rows[i].Cells[15].Value.ToString();

                int serverID = CurrentServer.fld_server_id;
                
                if(gvDataList.Rows[i].Tag == null)
                {
                    FengNiao.GMTools.Database.Model.tbl_counts_config data = new FengNiao.GMTools.Database.Model.tbl_counts_config();
                    data.item_id = item_name;
                    data.count = count;
                    data.dec = dec;
                    data.kindofrewards = kindofReward;
                    data.reward1 = reward1;
                    data.count1 = count1;
                    data.reward2 = reward2;
                    data.count2 = count2;
                    data.reward3 = reward3;
                    data.count3 = count3;
                    data.reward4 = reward4;
                    data.count4 = count4;
                    data.dataid = dataid;
                    data.serverID = CurrentServer.fld_server_id;
                    dataList.Add(data);
                    operationTypes.Add(OperationType.Add);
                    guidList.Add(guid);
                }
                else
                {
                    FengNiao.GMTools.Database.Model.tbl_counts_config data = gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_counts_config;
                    data.item_id = item_name;
                    data.count = count;
                    data.dec = dec;
                    data.kindofrewards = kindofReward;
                    data.reward1 = reward1;
                    data.count1 = count1;
                    data.reward2 = reward2;
                    data.count2 = count2;
                    data.reward3 = reward3;
                    data.count3 = count3;
                    data.reward4 = reward4;
                    data.count4 = count4;
                    data.dataid = dataid;
                    data.serverID = CurrentServer.fld_server_id;
                    dataList.Add(data);
                    operationTypes.Add(OperationType.Update);
                    guidList.Add(guid);
                }
            }
            if (dataList.Count == operationTypes.Count && dataList.Count == guidList.Count)
            {
                loadingControl.Visible = true;
                for (int i = 0; i < dataList.Count; i++)
                {
                    gvDataList.Rows[i].Tag = dataList[i];
                }

                string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.CountsConfig, HttpMethodType.Update);
                string strModel = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_counts_config>>(dataList);
                string strOperationTypes = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<OperationType>>(operationTypes);
                string strGuids = Serialize.ConvertObjectToJsonList(guidList);
                string strServer = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(CurrentServer.fld_server_id.ToString());
                strArgs = string.Format("{0}&Model={1}&OperationType={2}&guid={3}&server={4}", strArgs, System.Web.HttpUtility.UrlEncode(strModel, Encoding.UTF8), strOperationTypes, strGuids, strServer);
                CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, UpdateCallback);
            }
            
        }

        Color errorColor = Color.DarkGray;
        Color successColor = Color.GreenYellow;
        private bool CheckAndGetData(int rowIndex, int cellIndex, string errorMsg, out string data, bool canNull)
        {
            bool isError = false;
            data = string.Empty;
            if(gvDataList.Rows[rowIndex].Cells[cellIndex].Value == null)
            {
                isError = true;
                gvDataList.Rows[rowIndex].DefaultCellStyle.BackColor = errorColor;
                gvDataList.Rows[rowIndex].Cells[cellIndex].ErrorText = errorMsg;
            }
            else
            {
                data = gvDataList.Rows[rowIndex].Cells[cellIndex].Value.ToString();
                if(!canNull && string.IsNullOrEmpty(data))
                {
                    isError = true;
                    gvDataList.Rows[rowIndex].Cells[cellIndex].ErrorText = errorMsg;
                }
            }
            return isError;
        }

        private void UpdateCallback(object sender, UploadDataCompletedEventArgs e)
        {

            loadingControl.Visible = false;
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResultModel = Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResultModel.IsSuccess)
                    {
                        List<OperateResultModel> resultModelList = Serialize.ConvertJsonToObjectList<OperateResultModel>(requestResultModel.Content);
                        foreach (OperateResultModel resultModel in resultModelList)
                        {
                            int rowIndex = GetRowIndexByGuid(resultModel.Guid);
                            if (rowIndex != -1)
                            {
                                if (resultModel.IsSuccess)
                                {
                                    gvDataList.Rows[rowIndex].Cells[1].Style.BackColor = successColor;
                                }
                                else
                                {
                                    gvDataList.Rows[rowIndex].Tag = null;
                                    gvDataList.Rows[rowIndex].Cells[1].Style.BackColor = errorColor;
                                    gvDataList.Rows[rowIndex].Cells[1].ErrorText = resultModel.Content;
                                }
                            }
                        }
                        CustomMessageBox.Info(this, "保存完毕");
                        GetList();
                    }
                    else
                    {
                        CustomMessageBox.Error(this, string.Format("保存失败\r\n{0}", requestResultModel.Content));
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

        private int GetRowIndexByGuid(string guid)
        {
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                if (gvDataList.Rows[i].Cells[8].Value.ToString().Equals(guid))
                {
                    return i;
                }
            }
            return -1;
        }
        //====================================================================================================================




        //删除活动按钮
        //====================================================================================================================
        private void gvDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 16)
            {
                if (DialogResult.Yes == CustomMessageBox.Confirm(this, "确定要删除记录么?"))
                {
                    if (gvDataList.Rows[e.RowIndex].Tag == null)
                    {
                        gvDataList.Rows.RemoveAt(e.RowIndex);
                    }
                    else
                    {
                        FengNiao.GMTools.Database.Model.tbl_counts_config model = gvDataList.Rows[e.RowIndex].Tag as FengNiao.GMTools.Database.Model.tbl_counts_config;
                        List<FengNiao.GMTools.Database.Model.tbl_counts_config> models = new List<FengNiao.GMTools.Database.Model.tbl_counts_config>();
                        models.Add(model);
                        string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.CountsConfig, HttpMethodType.Remove);
                        string strModel = Serialize.ConvertObjectToJsonList(models);
                        strArgs = string.Format("{0}&Model={1}", strArgs, HttpUtility.UrlEncode(strModel, Encoding.UTF8));
                        CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, RemoveCallback);
                    }
                }
            }
        }

        private void RemoveCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        CustomMessageBox.Error(this, string.Format("{0}", requestResult.Content));
                        GetList();
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

        //====================================================================================================================





        //筛选查询
        //====================================================================================================================
        private void btnCustomQuery_click(object sender, EventArgs e)
        {
            InitGlobalObjectList();
            if (string.IsNullOrEmpty(tbItemID.Text))
            {
                gvDataList.Rows.Clear();
                setDeleteButton(true);
                gvDataList.Columns[1].ReadOnly = true;
                foreach (FengNiao.GMTools.Database.Model.tbl_counts_config counts in GlobalObject.CountsList)
                {
                    if (counts.serverID != CurrentServer.fld_server_id)
                        continue;
                    string kind = string.Empty;
                    if (counts.kindofrewards == 0)
                        kind = "全部领取";
                    else if (counts.kindofrewards == 1)
                        kind = "四选一";

                    string name = string.Empty;
                    if (CountsModleDir.TryGetValue(counts.item_id, out name))
                    {
                        int rowIndex = gvDataList.Rows.Add(name + "-" + counts.item_id, counts.record_id, counts.count, counts.dec, kind, counts.reward1, counts.count1, counts.reward2, counts.reward2, counts.reward3, counts.count3, counts.reward4, counts.count4, counts.dataid);
                    }
                    else
                    {
                        CustomMessageBox.Error(this, "初始化数据失败");
                        return;
                    }
                    btnSave.Enabled = true;
                }
            }
            else
            {
                btnSave.Enabled = false;
                int id = int.Parse(tbItemID.Text.ToString());
                gvDataList.Rows.Clear();
                setDeleteButton(false);
                gvDataList.Columns[1].ReadOnly = true;
                foreach (FengNiao.GMTools.Database.Model.tbl_counts_config counts in GlobalObject.CountsList)
                {
                    string kind = string.Empty;
                    if (counts.kindofrewards == 0)
                        kind = "全部领取";
                    else if (counts.kindofrewards == 1)
                        kind = "四选一";

                    if (counts.dataid == id)
                    {
                        string name = string.Empty;

                        if (CountsModleDir.TryGetValue(counts.item_id, out name))
                        {
                            int rowIndex = gvDataList.Rows.Add(name + "-" + counts.item_id, counts.record_id, counts.count, counts.dec, kind, counts.reward1, counts.count1, counts.reward2, counts.reward2, counts.reward3, counts.count3, counts.reward4, counts.count4, counts.dataid);
                        }
                        else
                        {
                            CustomMessageBox.Error(this, "初始化数据失败，请检查资料编号");
                            return;
                        }
                    }
                }

            }
        }
        //====================================================================================================================




        //重新初始化GlobalObject.CountsList
        //====================================================================================================================
        #region
        private void InitGlobalObjectList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.CountsConfig, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetGlobalListCallback);
        }



        private void GetGlobalListCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_counts_config> list = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_counts_config>(requestResult.Content);
                        GlobalObject.CountsList = list;
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
        #endregion
        //====================================================================================================================


            
      
        private void applytoothers_Click(object sender, EventArgs e)
        {
            AsyncToOtherServers frmAsyncToOtherServers = new AsyncToOtherServers(CurrentServer);
            if (frmAsyncToOtherServers.ShowDialog() == DialogResult.OK)
            {
                AsyncToOthers(frmAsyncToOtherServers);

                this.btnNew.Enabled = false;
                this.btnQuery.Enabled = false;
                this.btnSave.Enabled = false;
                this.applytoothers.Enabled = false;

            }
        }

        private void AsyncToOthers(AsyncToOtherServers async)
        {
            string strServers = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_server>>(async.serverList); ;
            string strArgs = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_counts_config>>(GetGridViewTags());
            string strMoudle = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.CountsConfig, HttpMethodType.AsyncToOthers);
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
            this.btnSave.Enabled = true;
            this.applytoothers.Enabled = true;
        }

        private List<FengNiao.GMTools.Database.Model.tbl_counts_config> GetGridViewTags()
        {
            List<FengNiao.GMTools.Database.Model.tbl_counts_config> data = new List<FengNiao.GMTools.Database.Model.tbl_counts_config>();
            for (int i = 0; i < gvDataList.RowCount; i++)
            {
                data.Add(gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_counts_config);
            }
            return data;
        }


        private void SetButtonStatus(bool isTrue)
        {
            this.btnSave.Enabled = isTrue;
            this.btnNew.Enabled = isTrue;
            this.applytoothers.Enabled = isTrue;
        }



        private bool isCanSave()
        {
            for (int i = 0; i < gvDataList.RowCount-1; i++)
            {
                for (int j = i + 1; j < gvDataList.RowCount; j++ )
                {
                    if (gvDataList.Rows[i].Cells[1].Value.ToString() == gvDataList.Rows[j].Cells[1].Value.ToString())
                        return false;
                }
            }
            return true;
        }

        private int GetNewRecordID()
        {
            int id = 0;
            if (gvDataList.RowCount == 1)
                return id;


            while (true)
            {
                bool isContance = false;
                for (int i = 0; i < gvDataList.RowCount-1; i++)
                {
                    if (int.Parse(gvDataList.Rows[i].Cells[1].Value.ToString()) == id)
                    {
                        isContance = true;
                    }
                }
                if (!isContance)
                    return id;
                id++;
            }
            return -1;
        }

        private void setDeleteButton(bool isShow)
        {
                gvDataList.Columns[16].Visible = isShow;
        }

    }
}
