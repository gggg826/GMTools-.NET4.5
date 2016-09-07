/**  版本信息模板在安装目录下，可自行修改。
* tbl_gift_code.cs
*
* 功 能： N/A
* 类 名： tbl_gift_code
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/6/26 14:32:54   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace FengNiao.GMTools.Database.Model
{
    /// <summary>
    /// tbl_gift_code:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class tbl_gift_code
    {
        public tbl_gift_code()
        { }
        /// <summary>
        /// fld_gift_code
        /// </summary>		
        private string _fld_gift_code;
        public string fld_gift_code
        {
            get { return _fld_gift_code; }
            set { _fld_gift_code = value; }
        }
        /// <summary>
        /// fld_gift_package
        /// </summary>		
        private int _fld_gift_package;
        public int fld_gift_package
        {
            get { return _fld_gift_package; }
            set { _fld_gift_package = value; }
        }
        /// <summary>
        /// fld_createtime
        /// </summary>		
        private DateTime _fld_createtime;
        public DateTime fld_createtime
        {
            get { return _fld_createtime; }
            set { _fld_createtime = value; }
        }
        /// <summary>
        /// fld_expiretime
        /// </summary>		
        private DateTime _fld_expiretime;
        public DateTime fld_expiretime
        {
            get { return _fld_expiretime; }
            set { _fld_expiretime = value; }
        }
        /// <summary>
        /// fld_channel
        /// </summary>		
        private string _fld_channel;
        public string fld_channel
        {
            get { return _fld_channel; }
            set { _fld_channel = value; }
        }
        /// <summary>
        /// fld_exchange_svrid
        /// </summary>		
        private int _fld_exchange_svrid;
        public int fld_exchange_svrid
        {
            get { return _fld_exchange_svrid; }
            set { _fld_exchange_svrid = value; }
        }
        /// <summary>
        /// fld_exchange_roleid
        /// </summary>		
        private string _fld_exchange_roleid;
        public string fld_exchange_roleid
        {
            get { return _fld_exchange_roleid; }
            set { _fld_exchange_roleid = value; }
        }
        /// <summary>
        /// fld_exchange_time
        /// </summary>		
        private DateTime _fld_exchange_time;
        public DateTime fld_exchange_time
        {
            get { return _fld_exchange_time; }
            set { _fld_exchange_time = value; }
        }
        /// <summary>
        /// fld_multi_use
        /// </summary>		
        private int _fld_multi_use;
        public int fld_multi_use
        {
            get { return _fld_multi_use; }
            set { _fld_multi_use = value; }
        }
        /// <summary>
        /// fld_starttime
        /// </summary>		
        private DateTime _fld_starttime;
        public DateTime fld_starttime
        {
            get { return _fld_starttime; }
            set { _fld_starttime = value; }
        }
        /// <summary>
        /// fld_user
        /// </summary>		
        private string _fld_user;
        public string fld_user
        {
            get { return _fld_user; }
            set { _fld_user = value; }
        }

    }
}

