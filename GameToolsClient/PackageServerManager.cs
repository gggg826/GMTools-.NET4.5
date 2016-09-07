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
    public partial class PackageServerManager : FengNiao.GameTools.Util.BaseForm
    {
        Color errorColor = Color.DarkGray;
        Color successColor = Color.GreenYellow;
        public PackageServerManager()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.IsAcceptResize = true;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.TopMostBox = false;
            this.Image = Properties.Resources.TK_2icon;
            btnSave.Image = Properties.Resources.document_save_3;
            btnNew.Image = Properties.Resources.user_new;
            btnRefresh.Image = Properties.Resources.view_refresh_3;
            InitServerGroup();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            string strErrorMsg = string.Empty;

            List<OperationType> operationTypes = new List<OperationType>();
            List<FengNiao.GMTools.Database.Model.tbl_package_server> dataList = new List<FengNiao.GMTools.Database.Model.tbl_package_server>();
            List<string> guidList = new List<string>();
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                string packageName = string.Empty;
                int cellIndex = 1;
                strErrorMsg = "包名无效";
                if (gvDataList.Rows[i].Cells[cellIndex].Value == null)
                {
                    isError = true;
                    gvDataList.Rows[i].DefaultCellStyle.BackColor = errorColor;
                    gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                }
                else
                {
                    string Name = string.Empty;
                    foreach (var item in ChannelModleDir)
                    {
                        if (gvDataList.Rows[i].Cells[cellIndex].Value.ToString() == item.Value)
                        {
                            Name = item.Key;
                            break;
                        }
                    }

                    packageName = Name;
                    if (string.IsNullOrEmpty(packageName))
                    {
                        isError = true;
                        gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                    }
                }
                cellIndex = 2;
                strErrorMsg = "版本无效";
                string version = string.Empty;
                if (gvDataList.Rows[i].Cells[cellIndex].Value == null)
                {
                    isError = true;
                    gvDataList.Rows[i].DefaultCellStyle.BackColor = errorColor;
                    gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                }
                else
                {
                    version = gvDataList.Rows[i].Cells[cellIndex].Value.ToString();
                    if (string.IsNullOrEmpty(version))
                    {
                        isError = true;
                        gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                    }
                }
                cellIndex = 3;
                strErrorMsg = "服务器组无效";
                string serverGroup = string.Empty;
                if (gvDataList.Rows[i].Cells[cellIndex].Value == null)
                {
                    isError = true;
                    gvDataList.Rows[i].DefaultCellStyle.BackColor = errorColor;
                    gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                }
                else
                {
                    serverGroup = ServerNameToID(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                    if (string.IsNullOrEmpty(serverGroup))
                    {
                        isError = true;
                        gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                    }
                }

                cellIndex = 4;
                strErrorMsg = "测试机无效";
                string deviceGroup = string.Empty;
                if (gvDataList.Rows[i].Cells[cellIndex].Value == null)
                {
                    isError = true;
                    gvDataList.Rows[i].DefaultCellStyle.BackColor = errorColor;
                    gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                }
                else
                {
                    deviceGroup = ServerNameToID(gvDataList.Rows[i].Cells[cellIndex].Value.ToString());
                    if (string.IsNullOrEmpty(deviceGroup))
                    {
                        isError = true;
                        gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                    }
                }
                cellIndex = 7;
                strErrorMsg = "是否启用无效";
                int deleted = -1;
                if (gvDataList.Rows[i].Cells[cellIndex].Value == null || !int.TryParse(gvDataList.Rows[i].Cells[cellIndex].Value.ToString(), out deleted))
                {
                    isError = true;
                    gvDataList.Rows[i].Cells[cellIndex].ErrorText = strErrorMsg;
                }

                cellIndex = 9;
                string guid = gvDataList.Rows[i].Cells[cellIndex].Value.ToString();
                if (gvDataList.Rows[i].Tag == null)
                {
                    FengNiao.GMTools.Database.Model.tbl_package_server data = new FengNiao.GMTools.Database.Model.tbl_package_server();
                    data.fld_package_name = packageName;
                    data.fld_package_version = version;
                    data.fld_visible_group = serverGroup;
                    data.fld_tester_visible_group = deviceGroup;
                    data.fld_create_time = DateTime.Now;
                    data.fld_modif_time = DateTime.Now;
                    data.fld_deleted = deleted;
                    dataList.Add(data);
                    operationTypes.Add(OperationType.Add);
                    guidList.Add(guid);
                }
                else
                {
                    FengNiao.GMTools.Database.Model.tbl_package_server data = gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_package_server;
                    data.fld_package_name = packageName;
                    data.fld_package_version = version;
                    data.fld_visible_group = serverGroup;
                    data.fld_tester_visible_group = deviceGroup;
                    data.fld_create_time = DateTime.Now;
                    data.fld_modif_time = DateTime.Now;
                    data.fld_deleted = deleted;
                    dataList.Add(data);
                    operationTypes.Add(OperationType.Update);
                    guidList.Add(guid);
                }
            }
            if (isError)
            {
                CustomMessageBox.Error(this, "保存失败，请根据提示检查数据");
            }
            else
            {
                if (dataList.Count == operationTypes.Count && dataList.Count == guidList.Count)
                {
                    loadingControl.Visible = true;
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        gvDataList.Rows[i].Tag = dataList[i];
                    }

                    string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Package_Server, HttpMethodType.Update);
                    string strModel = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<FengNiao.GMTools.Database.Model.tbl_package_server>>(dataList);
                    string strOperationTypes = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<OperationType>>(operationTypes);
                    string strGuids = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<string>>(guidList);

                    strArgs = string.Format("{0}&Model={1}&OperationType={2}&guid={3}", strArgs, System.Web.HttpUtility.UrlEncode(strModel, Encoding.UTF8), strOperationTypes, strGuids);
                    CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, UpdateCallback);
                }
            }
        }
        private void UpdateCallback(object sender, UploadDataCompletedEventArgs e)
        {
            loadingControl.Visible = false;
            if (e.Error == null)
            {

                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResultModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResultModel.IsSuccess)
                    {
                        List<OperateResultModel> resultModelList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<OperateResultModel>(requestResultModel.Content);
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
                        InitList();
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
                if (gvDataList.Rows[i].Cells[9].Value.ToString().Equals(guid))
                {
                    return i;
                }
            }
            return -1;
        }
        

        private void btnNew_Click(object sender, EventArgs e)
        {
            gvDataList.Rows.Add();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            InitList();
        }


        Dictionary<int, int> ItemCountCache = new Dictionary<int, int>();
        private void gvDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > (gvDataList.RowCount - 1) || e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == 8)
            {
                if (gvDataList.Rows[e.RowIndex].Tag != null)
                {
                    CustomMessageBox.Error(this, "不能删除已经生效的数据");
                    return;
                }
                if (DialogResult.Yes == CustomMessageBox.Confirm(this, "确定要删除记录么?"))
                {
                    gvDataList.Rows.RemoveAt(e.RowIndex);
                }
            }

            if(e.ColumnIndex == 3|| e.ColumnIndex==4)
            {
                ItemCountCache.Clear();
                //string infos = gvDataList.Rows[e.RowIndex].Cells[3].Value.ToString();
                SelectForm frmSelectForm = new SelectForm(SELECTTYPE.SERVERGROUP, gvDataList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value==null ? "": gvDataList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                if (frmSelectForm.ShowDialog() == DialogResult.OK)
                {
                    List<FengNiao.GMTools.Database.Model.tbl_server_group> selectedItems = frmSelectForm.GetServerGroups();

                    string serverlist = string.Empty;
                    foreach (var item in selectedItems)
                    {
                           serverlist = serverlist + item.fld_group_name + ";";
                    }
                    if (serverlist.Contains(";"))
                        gvDataList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = serverlist.Substring(0, serverlist.LastIndexOf(';'));
                    else
                        gvDataList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
                //gvDataList.Rows[e.RowIndex].Cells[3].Tag = frmSelectForm.GetServerGroups();
            }

            //if (e.ColumnIndex == 4)
            //{
            //    ItemCountCache.Clear();
            //    SelectForm frmSelectForm = new SelectForm(SELECTTYPE.DEVICEGROUP, gvDataList.Rows[e.RowIndex].Cells[4].Value == null ? "" : gvDataList.Rows[e.RowIndex].Cells[4].Value.ToString());
            //    if (frmSelectForm.ShowDialog() == DialogResult.OK)
            //    {
            //        List<FengNiao.GMTools.Database.Model.tbl_testerdevice> selectedItems = frmSelectForm.GetDeviceGroups();
            //        string devicelist = string.Empty;
            //        foreach (var item in selectedItems)
            //        {
            //            devicelist = devicelist + item.fld_declare + ";";
            //        }
            //        if (devicelist.Contains(";"))
            //            gvDataList.Rows[e.RowIndex].Cells[4].Value = devicelist.Substring(0, devicelist.LastIndexOf(';'));
            //        else
            //            gvDataList.Rows[e.RowIndex].Cells[4].Value = "";
            //    }
            //    //gvDataList.Rows[e.RowIndex].Cells[3].Tag = frmSelectForm.GetDeviceGroups();
            //}
        }
        private List<FengNiao.GMTools.Database.Model.tbl_item> GetSelectedItems()
        {
            List<FengNiao.GMTools.Database.Model.tbl_item> selectedItems = new List<FengNiao.GMTools.Database.Model.tbl_item>();
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                FengNiao.GMTools.Database.Model.tbl_item item = gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_item;
                if (item != null)
                {
                    selectedItems.Add(item);
                }
            }
            return selectedItems;
        }

        private void gvDataList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvDataList.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.White;
            gvDataList.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
        }

        private void gvDataList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            gvDataList.Rows[e.RowIndex].Cells[7] = GlobalObject.GetCommonCell("是","否");
            gvDataList.Rows[e.RowIndex].Cells[9].Value = Guid.NewGuid();
        }

        private void PackageServerManager_Load(object sender, EventArgs e)
        {
            RefreshLayout();
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RefreshLayout();
        }
        private void RefreshLayout()
        {
            if (this.IsHandleCreated && gvDataList.IsHandleCreated)
            {
                panelParent.Left = base.ClientBounds.X+10;
                panelParent.Top = base.ClientBounds.Y;
                panelParent.Width = base.ClientBounds.Width-20;
                panelParent.Height = base.ClientBounds.Height-20;
            }
        }

        private void InitList()
        {
            loadingControl.Visible = true;
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Package_Server, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallback);
        }

        private void GetListCallback(object sender, UploadDataCompletedEventArgs e)
        {
            loadingControl.Visible = false;
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_package_server> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_package_server>(requestResult.Content);
                        GlobalObject.PackageServerList = dataList;
                        InitList(GlobalObject.PackageServerList);
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
        
        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_package_server> list)
        {
            gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.tbl_package_server data in list)
            {
                string name = string.Empty;
                if (ChannelModleDir.TryGetValue(data.fld_package_name, out name))
                {
                    int rowIndex = gvDataList.Rows.Add(data.fld_record_id, name, data.fld_package_version,
                                                      GetServerGroupNames(data.fld_visible_group),
                                                      GetServerGroupNames(data.fld_tester_visible_group),
                                                      data.fld_create_time, data.fld_modif_time);
                    gvDataList.Rows[rowIndex].Cells[7] = GlobalObject.GetCommonCell( "是", "否");
                    gvDataList.Rows[rowIndex].Cells[7].Value = data.fld_deleted;
                    gvDataList.Rows[rowIndex].Tag = data;
                    gvDataList.Rows[rowIndex].Cells[9].Value = Guid.NewGuid();

                }

                    
            }
        }


        private string GetServerGroupNames(string visible_group)
        {


            if (visible_group == null || visible_group == "")
                return null;

            else if (!visible_group.Contains(";"))
            {
                foreach (var item in GlobalObject.ServerGroupList)
                {
                    if (item.fld_group_id == int.Parse(visible_group))
                        return item.fld_group_name;
                }
            }

            else
            {
                string[] names = visible_group.Split(';');
                string showNames = string.Empty;
                for (int i = 0; i < names.Length; i++)
                {
                    foreach (var item in GlobalObject.ServerGroupList)
                    {
                        if (item.fld_group_id == int.Parse(names[i]))
                            showNames += item.fld_group_name + ";";
                    }

                }
                return showNames.Substring(0, showNames.LastIndexOf(';'));
            }
            return "获取失败";
        }
        private string GetDeviceGroupName(string tester_visible_group)
        {
            if (tester_visible_group == null || tester_visible_group == "")
                return null;

            else if (!tester_visible_group.Contains(";"))
            {
                foreach (var item in GlobalObject.TesterDeviceList)
                {
                    if (item.fld_value == tester_visible_group)
                        return item.fld_declare;
                }
            }

            else
            {
                string[] names = tester_visible_group.Split(';');
                string showNames = string.Empty;
                for (int i = 0; i < names.Length; i++)
                {
                    foreach (var item in GlobalObject.TesterDeviceList)
                    {
                        if (item.fld_value == names[i])
                            showNames += item.fld_declare + ";";
                    }

                }
                return showNames.Substring(0, showNames.LastIndexOf(';'));
            }
            return "获取失败";
        }



        private void InitServerGroup()
        {
            loadingControl.Visible = true;
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Server_Group, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListServerGroupCallback);
        }
        private void GetListServerGroupCallback(object sender, UploadDataCompletedEventArgs e)
        {
            loadingControl.Visible = false;
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_server_group> PackageList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_server_group>(requestResult.Content);
                        GlobalObject.ServerGroupList = PackageList;
                        //InitDeviceGroup();
                        InitChannel();
                        //BindingChannelResources();
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

        Dictionary<string, string> ChannelModleDir = new Dictionary<string, string>();
        private void BindingChannelResources()
        {
            DataGridViewComboBoxColumn channelShow = new DataGridViewComboBoxColumn();
            channelShow.Name = "item_name";
            channelShow.HeaderText = "活动名称";
            channelShow.Width = 100;
            foreach (var item in GlobalObject.PackageList)
            {
                //channelShow.Items.Add(item.fld_package_name + "-" + item.fld_declare);
                ChannelModleDir.Add(item.fld_package_name, item.fld_declare);
                channelShow.Items.Add(item.fld_declare);
            }

            //gvDataList.DataSource = new BindingSource(ChannelModleDir, null);
            //channelShow.DisplayMember = "Key";
            //channelShow.ValueMember = "Value";

            channelShow.DisplayIndex = 0;
            gvDataList.Columns.Insert(1, channelShow);

            InitList();
        }




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
                    BindingChannelResources();
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


        //private void InitDeviceGroup()
        //{
        //    loadingControl.Visible = true;
        //    string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.TesterDevice, HttpMethodType.GetList);
        //    CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListDeviceGroupCallback);
        //}
        //private void GetListDeviceGroupCallback(object sender, UploadDataCompletedEventArgs e)
        //{
        //    loadingControl.Visible = false;
        //    if (e.Error == null)
        //    {
        //        try
        //        {
        //            string strContent = Encoding.UTF8.GetString(e.Result);
        //            ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
        //            if (requestResult.IsSuccess)
        //            {
        //                List<FengNiao.GMTools.Database.Model.tbl_testerdevice> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_testerdevice>(requestResult.Content);
        //                GlobalObject.TesterDeviceList = dataList;
        //                InitList();
        //            }
        //            else
        //            {
        //                CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResult.Content));
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



        private string ServerNameToID(string names)
        {
            if (names == null || names == "")
                return null;

            else if (!names.Contains(";"))
            {
                foreach (var item in GlobalObject.ServerGroupList)
                {
                    if (item.fld_group_name == names)
                        return item.fld_group_id.ToString();
                }
            }

            else
            {
                string[] strnames = names.Split(';');
                string showNames = string.Empty;
                for (int i = 0; i < strnames.Length; i++)
                {
                    foreach (var item in GlobalObject.ServerGroupList)
                    {
                        if (item.fld_group_name == strnames[i])
                            showNames += item.fld_group_id.ToString() + ";";
                    }

                }
                return showNames.Substring(0, showNames.LastIndexOf(';'));
            }
            return string.Empty;
        }
        private string DeviceNameToID(string names)
        {
            if (names == null || names == "")
                return null;

            else if (!names.Contains(";"))
            {
                foreach (var item in GlobalObject.TesterDeviceList)
                {
                    if (item.fld_declare == names)
                        return item.fld_value;
                }
            }

            else
            {
                string[] strnames = names.Split(';');
                string showNames = string.Empty;
                for (int i = 0; i < strnames.Length; i++)
                {
                    foreach (var item in GlobalObject.TesterDeviceList)
                    {
                        if (item.fld_declare == strnames[i])
                            showNames += item.fld_value + ";";
                    }

                }
                return showNames.Substring(0, showNames.LastIndexOf(';'));
            }
            return string.Empty;
        }
    }
}
