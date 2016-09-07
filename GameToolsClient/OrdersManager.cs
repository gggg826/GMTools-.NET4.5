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
    public partial class OrdersManager : FengNiao.GameTools.Util.BaseForm
    {
        
        public OrdersManager()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.dtstartDate.Value = DateTime.Now.AddDays(-7);
            this.dtStartTime.Value = DateTime.Now;
            this.dtStopDate.Value = DateTime.Now.AddDays(1);
            this.dtStopTime.Value = DateTime.Now;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
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
            InitList();
        }

        private void InitList()
        {
            DateTime startTime = DateTime.Parse(dtstartDate.Value.ToString("yyyy-MM-dd") + " " + dtStartTime.Value.ToString("HH:mm:ss"));
            DateTime stopTime = DateTime.Parse(dtStopDate.Value.ToString("yyyy-MM-dd") + " " + dtStopTime.Value.ToString("HH:mm:ss"));
            if (DateTime.Compare(startTime, stopTime) > -1)
            {
                CustomMessageBox.Error(this, "首次充值结束时间必须大于开始时间");
                return;
            }

            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Orders, HttpMethodType.Add);
            string roleID = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(this.tbRoleID.Text.ToString());
            string beginTime = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(startTime.ToString());
            beginTime = beginTime.Replace('/', '-');
            string endTime = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(stopTime.ToString());
            endTime = endTime.Replace('/', '-'); 
            strArgs = string.Format("{0}&RoleID={1}&StartTime={2}&StopTime={3}", strArgs, roleID, beginTime, endTime);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetListCallback);
        }

        private void GetListCallback(object sender, UploadDataCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                HttpResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<HttpResultModel>(strContent);
                if (requestResult.errorcode == 0)
                {
                    List<FengNiao.GMTools.Database.Model.Orders> data = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.Orders>(requestResult.result);
                    InitList(data);
                }
                else
                {
                    CustomMessageBox.Error(this, string.Format("获取数据失败\r\n{0}", requestResult.result));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
        }
        
        private void InitList(List<FengNiao.GMTools.Database.Model.Orders> data)
        {
            gvDataList.Rows.Clear();
            foreach (FengNiao.GMTools.Database.Model.Orders order in data)
            {
                int i = gvDataList.Rows.Add(order.fld_channel, order.fld_itemid, order.fld_count, order.fld_price, order.fld_orderid,
                                            order.fld_createtime, GetStatusDir()[order.fld_status] + "-" + order.fld_status, order.fld_svrid);
                gvDataList.Rows[i].Tag = order;
            }
            
        }

        private void gvDataList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0 && e.ColumnIndex == 9)
            {
                //int status = int.Parse(gvDataList.Rows[e.RowIndex].Cells[6].Value.ToString());
                int status = ((FengNiao.GMTools.Database.Model.Orders)gvDataList.Rows[e.RowIndex].Tag).fld_status;
                if (status == 2 || status == 12 || status == 13)
                {
                    RetryOrder(gvDataList.Rows[e.RowIndex].Cells[4].Value.ToString());
                }
                else
                {
                    CustomMessageBox.Error(this, "订单已成功，切误再次下单！");
                }
            }
        }
        private void RetryOrder(string _orderID)
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Orders, HttpMethodType.Update);
            string orderID = FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<string>(_orderID);
            strArgs = string.Format("{0}&RoleID={1}", strArgs, orderID);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetRetryCallback);
        }

        private void GetRetryCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                HttpResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<HttpResultModel>(strContent);
                if (requestResult.errorcode == 0)
                {
                    CustomMessageBox.Confirm(this, string.Format("操作成功\r\n{0}", requestResult.result));
                }
                else
                {
                    CustomMessageBox.Error(this, string.Format("操作失败\r\n{0}", requestResult.result));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
        }

        private Dictionary<int, string> GetStatusDir()
        {
            Dictionary<int, string> statusDir = new Dictionary<int, string>();
            statusDir.Add(0, "订单初始状态");
            statusDir.Add(1, "订单核实成功，等待发货");
            statusDir.Add(2, "角色不在线");
            statusDir.Add(10, "订单核实失败");
            statusDir.Add(11, "订单完成");
            statusDir.Add(12, "没找到角色");
            statusDir.Add(13, "没这个商品");
            statusDir.Add(14, "等待继续确认");

            return statusDir;
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

        private void tbServer_ButtonCustomClick(object sender, EventArgs e)
        {
            ServerManager frmServerManager = new ServerManager();
            frmServerManager.IsSelector = true;
            if (frmServerManager.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbServer.Text = frmServerManager.SelectedServer.fld_server_name;
                tbServer.Tag = frmServerManager.SelectedServer;
            }
        }

        private void btnSupplement_Click(object sender, EventArgs e)
        {
            InitGlobalFirstRechargeList();
        }

        private void InitGlobalFirstRechargeList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.First_Recharge, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetFirstRechargeListCallback);
        }

        private void GetFirstRechargeListCallback(object sender, UploadDataCompletedEventArgs e)
        {
            //loadingControl.Visible = false;
            if (e.Error == null)
            {
                try
                {
                    string strContent = Encoding.UTF8.GetString(e.Result);
                    ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                    if (requestResult.IsSuccess)
                    {
                        List<FengNiao.GMTools.Database.Model.tbl_first_recharge> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_first_recharge>(requestResult.Content);
                        GlobalObject.FirstRechargeList = dataList;
                        InitGlobalPackageList();
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

        private void InitGlobalPackageList()
        {
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.Package, HttpMethodType.GetList);
            CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, GetPackageCallback);
        }

        private void GetPackageCallback(object sender, UploadDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                if (requestResult.IsSuccess)
                {
                    List<FengNiao.GMTools.Database.Model.tbl_package> dataList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_package>(requestResult.Content);
                    GlobalObject.PackageList = dataList;
                    SupplementOrders frmSupplementOrders = new SupplementOrders();
                    if(frmSupplementOrders.ShowDialog() == DialogResult.OK )
                        this.Show();
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
    }
}
