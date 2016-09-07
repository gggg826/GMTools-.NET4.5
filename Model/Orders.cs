using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.Model
{
    public partial class Orders
    {
        public Orders()
        { }

        private string _fld_ChannelIndex;
        private string _fld_CurrencyType;
        private string _fld_ModesOfPayment;
        private string _fld_channel;
        private string _fld_itemid;
        private string _fld_orderid;
        private string _fld_roleid;
        private int _fld_price;
        private int _fld_count;
        private int _fld_status;
        private int _fld_svrid;
        
        private DateTime _fld_createtime;

        public string fld_ChannelIndex
        {
            get
            {
                return _fld_ChannelIndex;
            }

            set
            {
                _fld_ChannelIndex = value;
            }
        }

        public string fld_CurrencyType
        {
            get
            {
                return _fld_CurrencyType;
            }

            set
            {
                _fld_CurrencyType = value;
            }
        }

        public string fld_ModesOfPayment
        {
            get
            {
                return _fld_ModesOfPayment;
            }

            set
            {
                _fld_ModesOfPayment = value;
            }
        }

        public string fld_channel
        {
            get
            {
                return _fld_channel;
            }

            set
            {
                _fld_channel = value;
            }
        }

        public string fld_itemid
        {
            get
            {
                return _fld_itemid;
            }

            set
            {
                _fld_itemid = value;
            }
        }

        public string fld_orderid
        {
            get
            {
                return _fld_orderid;
            }

            set
            {
                _fld_orderid = value;
            }
        }

        public string fld_roleid
        {
            get
            {
                return _fld_roleid;
            }

            set
            {
                _fld_roleid = value;
            }
        }

        public int fld_price
        {
            get
            {
                return _fld_price;
            }

            set
            {
                _fld_price = value;
            }
        }

        public int fld_count
        {
            get
            {
                return _fld_count;
            }

            set
            {
                _fld_count = value;
            }
        }

        public int fld_status
        {
            get
            {
                return _fld_status;
            }

            set
            {
                _fld_status = value;
            }
        }
        
        public DateTime fld_createtime
        {
            get
            {
                return _fld_createtime;
            }

            set
            {
                _fld_createtime = value;
            }
        }

        public int fld_svrid
        {
            get
            {
                return _fld_svrid;
            }

            set
            {
                _fld_svrid = value;
            }
        }
    }
}
