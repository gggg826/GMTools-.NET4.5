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
    public enum SELECTTYPE
    {
        SERVERGROUP,
        DEVICEGROUP,
        SERVER
    }
    public partial class SelectForm : FengNiao.GameTools.Util.BaseForm
    {
        private List<FengNiao.GMTools.Database.Model.tbl_server_group> serverGroup = new List<FengNiao.GMTools.Database.Model.tbl_server_group>();
        private List<FengNiao.GMTools.Database.Model.tbl_testerdevice> deviceGroup = new List<FengNiao.GMTools.Database.Model.tbl_testerdevice>();
        private string infos = string.Empty;
        public SelectForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public SelectForm(SELECTTYPE type, string infos)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.infos = infos;
            if (type == SELECTTYPE.SERVERGROUP)
            {
                this.lbTitle.Text = "选择服务器组";
                InitServerGroup();
            }
            else if(type == SELECTTYPE.SERVER)
            {
                this.lbTitle.Text = "选择服务器";
                //InitServer();
            }
            //else if (type == SELECTTYPE.DEVICEGROUP)
            //{
            //    this.lbTitle.Text = "选择设备";
            //    InitDeviceGroup();
            //}
        }

        public SelectForm(List<FengNiao.GMTools.Database.Model.tbl_server_group> dataList)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lbTitle.Text = "选择服务器组";
            if (dataList != null && dataList.Count > 0)
                serverGroup = dataList;
            InitServerGroup();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

                
        private void InitServerGroup()
        {
            loadingControl.Visible = true;
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Server_Group, HttpMethodType.GetList);
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
                        List<FengNiao.GMTools.Database.Model.tbl_server_group> PackageList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_server_group>(requestResult.Content);
                        GlobalObject.ServerGroupList = PackageList;
                        InitList(GlobalObject.ServerGroupList);
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
        private void InitList(List<FengNiao.GMTools.Database.Model.tbl_server_group> list)
        {

            gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.tbl_server_group data in list)
            {
                int rowIndex = gvDataList.Rows.Add(data.fld_record_id, data.fld_group_name, data.fld_group_id);
                gvDataList.Rows[rowIndex].Cells[0].Value = false;
                if (isContains(infos, data.fld_group_id.ToString()))
                    gvDataList.Rows[rowIndex].Cells[0].Value = true;
                gvDataList.Rows[rowIndex].Cells[3] = GlobalObject.GetCommonCell("启用", "禁用");
                gvDataList.Rows[rowIndex].Cells[3].Value = data.fld_deleted;
                gvDataList.Rows[rowIndex].Tag = data;
            }
        }

        private bool isContains(string info, string group_id)
        {
            string[] infos = ServerNameToID(info).Split(';');
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i] == group_id)
                {
                    return true;
                }
            }
            return false;
        }
        private string ServerNameToID(string names)
        {
            if (names == null || names == "")
                return "";

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
            return "获取失败";
        }
        public List<FengNiao.GMTools.Database.Model.tbl_server_group> GetServerGroups()
        {
            serverGroup.Clear();
            for (int i = 0; i < gvDataList.RowCount; i++)
            {
                bool isSelected = false;
                if (gvDataList.Rows[i].Cells[0].Value != null && bool.TryParse(gvDataList.Rows[i].Cells[0].Value.ToString(), out isSelected))
                {
                    if (isSelected)
                    {
                        FengNiao.GMTools.Database.Model.tbl_server_group selectedItem = gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_server_group;
                        serverGroup.Add(selectedItem);
                    }
                }
            }
            return serverGroup;
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
        //                //InitList(GlobalObject.TesterDeviceList);
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
        //private void InitList(List<FengNiao.GMTools.Database.Model.tbl_testerdevice> dataList)
        //{

        //    gvDataList.Rows.Clear();
        //    foreach (FengNiao.GMTools.Database.Model.tbl_testerdevice data in dataList)
        //    {
        //        int rowIndex = gvDataList.Rows.Add(data.fld_record_id, data.fld_declare, data.fld_value);
        //        gvDataList.Rows[rowIndex].Cells[0].Value = false;
        //        if (DeviceNameToID(infos).Contains(data.fld_value))
        //            gvDataList.Rows[rowIndex].Cells[0].Value = true;
        //        gvDataList.Rows[rowIndex].Cells[4] = GlobalObject.GetCommonCell("启用", "禁用");
        //        gvDataList.Rows[rowIndex].Cells[4].Value = data.fld_deleted;
        //        gvDataList.Rows[rowIndex].Tag = data;
        //    }
        //}
        //private string DeviceNameToID(string names)
        //{
        //    if (names == null || names == "")
        //        return "";

        //    else if (!names.Contains(";"))
        //    {
        //        foreach (var item in GlobalObject.TesterDeviceList)
        //        {
        //            if (item.fld_declare == names)
        //                return item.fld_value;
        //        }
        //    }

        //    else
        //    {
        //        string[] strnames = names.Split(';');
        //        string showNames = string.Empty;
        //        for (int i = 0; i < strnames.Length; i++)
        //        {
        //            foreach (var item in GlobalObject.TesterDeviceList)
        //            {
        //                if (item.fld_declare == strnames[i])
        //                    showNames += item.fld_value + ";";
        //            }

        //        }
        //        return showNames.Substring(0, showNames.LastIndexOf(';'));
        //    }
        //    return "获取失败";
        //}
        public List<FengNiao.GMTools.Database.Model.tbl_testerdevice> GetDeviceGroups()
        {
            deviceGroup.Clear();
            for (int i = 0; i < gvDataList.RowCount; i++)
            {
                bool isSelected = false;
                if (gvDataList.Rows[i].Cells[0].Value != null && bool.TryParse(gvDataList.Rows[i].Cells[0].Value.ToString(), out isSelected))
                {
                    if (isSelected)
                    {
                        FengNiao.GMTools.Database.Model.tbl_testerdevice selectedItem = gvDataList.Rows[i].Tag as FengNiao.GMTools.Database.Model.tbl_testerdevice;
                        deviceGroup.Add(selectedItem);
                    }
                }
            }
            return deviceGroup;
        }
    }
}
