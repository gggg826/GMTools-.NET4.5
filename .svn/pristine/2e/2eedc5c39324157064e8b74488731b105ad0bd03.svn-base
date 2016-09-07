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
    public partial class SupplementOrders : FengNiao.GameTools.Util.BaseForm
    {
        private FengNiao.GMTools.Database.Model.tbl_server CurrentServer = new FengNiao.GMTools.Database.Model.tbl_server();
        public SupplementOrders()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            //cbIndex.DataSource = new BindingSource(GlobalObject.GetFirstRechargeData(), null);
            //cbIndex.DisplayMember = "Value";
            //cbIndex.ValueMember = "Key";
            //cbIndex.SelectedIndex = 0;

            this.cbChannel.DataSource = new BindingSource(GlobalObject.GetPackageData(), null);
            this.cbChannel.DisplayMember = "Value";
            this.cbChannel.ValueMember = "Key";
        }

        private void tbServer_ButtonCustomClick(object sender, EventArgs e)
        {
            ServerManager frmServerManager = new ServerManager();
            frmServerManager.IsSelector = true;
            if (frmServerManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbServer.Text = frmServerManager.SelectedServer.fld_server_name;
                tbServer.Tag = frmServerManager.SelectedServer;
                CurrentServer = frmServerManager.SelectedServer;
            }
        }

        private void tbRoleID_ButtonCustomClick(object sender, EventArgs e)
        {
            FengNiao.GMTools.Database.Model.tbl_server serverData = tbServer.Tag as FengNiao.GMTools.Database.Model.tbl_server;
            if (serverData == null)
            {
                CustomMessageBox.Error(this, "没有选择服务器");
                return;
            }

            QueryRole queryRoleInfo = new QueryRole(serverData);
            if (queryRoleInfo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbRoleID.Text = queryRoleInfo.SelectedRoleID;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        FengNiao.GMTools.Database.Model.SupplementOrders model = new FengNiao.GMTools.Database.Model.SupplementOrders();

        private void btnSupplement_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbOrderID.Text))
            {
                CustomMessageBox.Error(this, "请输入订单号");
                return;
            }

            if (string.IsNullOrEmpty(this.tbServer.Text))
            {
                CustomMessageBox.Error(this, "请选择服务器");
                return;
            }

            if (string.IsNullOrEmpty(this.tbRoleID.Text))
            {
                CustomMessageBox.Error(this, "请输入角色ID");
                return;
            }
            
            model.roleid = tbRoleID.Text.ToString();
            model.serverid = CurrentServer.fld_server_id.ToString();
            long record_id = GetSelectedValue();
            model.itemid = GetItemID(record_id);
            model.price = tbPrice.Text.ToString();
            model.channel = GetPackageNO((long)cbChannel.SelectedValue);
            model.orderno = tbOrderID.Text;

            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Orders, HttpMethodType.SendRequest);
            string strModel = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<FengNiao.GMTools.Database.Model.SupplementOrders>(model);
            strArgs = string.Format("{0}&Model={1}&User={2}", strArgs, System.Web.HttpUtility.UrlEncode(strModel, Encoding.UTF8), GlobalObject.user);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetSupplementCallback);
        }

        private void GetSupplementCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                HttpResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<HttpResultModel>(strContent);
                if (requestResult.errorcode == 0)
                {
                    CustomMessageBox.Info(this, "补单成功");
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    CustomMessageBox.Error(this, string.Format("补单失败\r\n{0}", requestResult.result));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
        }
        
        private string GetItemID(long record_id)
        {
            foreach (var item in GlobalObject.FirstRechargeList)
            {
                if (item.record_id == record_id)
                    return item.itemid;
            }
            return null;
        }
        private string GetItemPrice(long record_id)
        {
            foreach (var item in GlobalObject.FirstRechargeList)
            {
                if (item.record_id == record_id)
                    return item.RMBprice.ToString();
            }
            return null;
        }

        private string GetPackageNO(long record_id)
        {
            foreach (var item in GlobalObject.PackageList)
            {
                if (item.fld_record_id == record_id)
                    return item.fld_package_name;
            }
            return null;
        }

        private void cbIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPrice.Text = GetItemPrice(GetSelectedValue());
        }

        private long GetSelectedValue()
        {
            object obj = cbIndex.SelectedItem;
            KeyValuePair<long, string> value = (KeyValuePair<long, string>)obj;
            return (long)value.Key;
        }

        private void cbChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIndex.DataSource = new BindingSource(GetItemList(), null);
            cbIndex.DisplayMember = "Value";
            cbIndex.ValueMember = "Key";
            cbIndex.SelectedIndex = 0;
        }

        private Dictionary<long, string> GetItemList()
        {
            object obj = cbChannel.SelectedItem;
            KeyValuePair<long, string> value = (KeyValuePair<long, string>)obj;

            FengNiao.GMTools.Database.Model.tbl_package package = new FengNiao.GMTools.Database.Model.tbl_package();
            try
            {
                foreach (var item in GlobalObject.PackageList)
                {
                    if (item.fld_record_id == (long)value.Key)
                    {
                        package = item;
                        break;
                    }
                }
            }
            catch
            {

                CustomMessageBox.Error(this, "首充条目信息填写错误");
            }


            Dictionary<long, string> itemList = new Dictionary<long, string>();
            foreach (var item in GlobalObject.FirstRechargeList)
            {
                if(item.channel == package.fld_package_number)
                {
                    itemList.Add(item.record_id, item.name);
                }
            }

            if (itemList.Count != 0)
                return itemList;
            else
            {
                foreach (var item in GlobalObject.FirstRechargeList)
                {
                    if (string.IsNullOrEmpty(item.channel))
                        itemList.Add(item.record_id, item.name);
                }
                return itemList;
            }
        }
    }
}
