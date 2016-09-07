﻿using System;
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
using System.Xml;

namespace GameToolsClient
{
    public partial class Login : BaseForm
    {
        public Login()
        {
            InitializeComponent();
            this.Text = "抢滩GM管理工具";
            this.TopMostBox = false;
            this.IsShowCaptionImage = false;
            this.IsShowCaptionText = false;
            this.IsTitleSplitLine = false;
            this.BackColor = System.Drawing.Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TitleAlign = TitleAlignment.Left;
            this.Image = Properties.Resources.TK_2icon;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                CustomMessageBox.Error(this, "用户名不能为空");
                return;
            }
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                CustomMessageBox.Error(this, "密码不能为空");
                return;
            }
            if (tbPassword.Text.Length < GlobalObject.PasswrodLength)
            {
                CustomMessageBox.Error(this, string.Format("密码不能小于{0}位", GlobalObject.PasswrodLength));
                return;
            }


            GlobalObject.user = tbName.Text;
            string strArgs = GlobalObject.GetModuleAndMethodArgs(HttpModuleType.User, HttpMethodType.Login);
            List<HttpInterfaceSqlParameter> requestParamters = new List<HttpInterfaceSqlParameter>();
            strArgs = string.Format("{0}&user={1}&password={2}", strArgs, tbName.Text, tbPassword.Text);
            try
            {
                CustomWebRequest.Request(GlobalObject.HttpServerIP, strArgs, Encoding.UTF8, LoginCallback);
                btnLogin.Enabled = false;
                cbNetworkType.Enabled = false;
            }
            catch
            {
                CustomMessageBox.Error(this, "地址无效");
            }
        }


        private void LoginCallback(object sender, UploadDataCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                string strContent = Encoding.UTF8.GetString(e.Result);
                ResultModel requestResult = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<ResultModel>(strContent);
                if (requestResult.IsSuccess)
                {
                    try
                    {
                        LoginUserModel loginUserModel = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<LoginUserModel>(requestResult.Content);
                        if (loginUserModel != null)
                        {
                            GlobalObject.CurrentLoginUserModel = loginUserModel;
                            CustomMessageBox.Info(this, "登陆成功");
                            this.Hide();
                            GlobalObject.frmLogin = this;
                            Main frmMain = new Main();
                            frmMain.Show();
                        }
                    }
                    catch
                    {
                        CustomMessageBox.Error(this, "登陆失败\r\n系统错误");
                    }
                }
                else
                {
                    CustomMessageBox.Error(this, string.Format("登陆失败\r\n{0}", requestResult.Content));
                }
            }
            else
            {
                CustomMessageBox.Error(this, "连接服务器异常，请确认是否已经联网");
            }
            btnLogin.Enabled = true;
            cbNetworkType.Enabled = true;
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            tbName.Text = "";
            tbPassword.Text = "";
#if DEBUG
            tbName.Text = "admin";
            tbPassword.Text = "123456";
#endif
            tbName.Focus();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //    int maxErrorCount = 1;
            //    int errorCounter = 0;
            //Retry:
            try
            {
                GlobalObject.HttpServerDataList = new List<ServerInfoData>();
                XmlConfig xmlConfig = new XmlConfig(Runpath.AppPath + "system.config");
                XmlNodeList nodes = xmlConfig.GetXmlNodes("config", "serverIP");
                int temp;
#if DEBUG
                temp = 0;
#else
                temp = 1;
#endif
                for (int i = temp; i < nodes.Count; i++)
                {
                    ServerInfoData sid = new ServerInfoData();
                    sid.Title = nodes[i].Attributes["title"].InnerText;
                    sid.HttpInterface = nodes[i].InnerText;
                    GlobalObject.HttpServerDataList.Add(sid);
                }
                cbNetworkType.DataSource = GlobalObject.HttpServerDataList;
                cbNetworkType.DisplayMember = "Title";
                cbNetworkType.ValueMember = "HttpInterface";


                XmlNodeList dirs = xmlConfig.GetXmlNodes("config", "destinationDirectory");
                for (int i = 0; i < dirs.Count; i++)
                {
                    DirectoryInfoData did = new DirectoryInfoData();
                    did.Title = dirs[i].Attributes["title"].InnerText;
                    did.DestinationDirectory = dirs[i].InnerText;
                    GlobalObject.DirectoryDataList.Add(did);
                }
                if (cbNetworkType.Items.Count > 0)
                {
                    cbNetworkType.SelectedIndex = 0;
                }
                //string serverIP1 = xmlConfig.GetXmlReader("config", "serverIP1");
                //GlobalObject.HttpServerIP1 = serverIP1;
                //string serverIP2 = xmlConfig.GetXmlReader("config", "serverIP2");
                //GlobalObject.HttpServerIP2 = serverIP2;
                //GlobalObject.HttpServerIP = serverIP1;
            }
            catch
            {
                CustomMessageBox.Error(this, "加载配置失败，请重新安装");
                //if (errorCounter < maxErrorCount)
                //{
                //    errorCounter++;
                //Retry1:
                //    if (CustomMessageBox.Confirm(this, "加载配置失败，请重新设置配置文件") == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        Config frmConfig = new Config();
                //        if (frmConfig.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //        {
                //            goto Retry;
                //        }
                //        else
                //        {
                //            goto Retry1;
                //        }
                //    }
                //}
                //else
                //{
                //    CustomMessageBox.Error(this, "加载配置失败");
                //}
            }
        }

        private void cbNetworkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServerInfoData sid = cbNetworkType.SelectedValue as ServerInfoData;
            if (sid != null)
            {
                GlobalObject.HttpServerIP = sid.HttpInterface;
            }
            else
            {
                string serverIP = cbNetworkType.SelectedValue as string;
                if (!string.IsNullOrEmpty(serverIP))
                {
                    GlobalObject.HttpServerIP = serverIP;
                }
            }
        }
    }
}
