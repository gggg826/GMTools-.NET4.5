using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.Model
{
    public class tbl_log
    {

        /// <summary>
        /// auto_increment
        /// </summary>		
        private int _record_id;
        public int record_id
        {
            get { return _record_id; }
            set { _record_id = value; }
        }
        /// <summary>
        /// adminid
        /// </summary>		
        private string _adminid;
        public string adminid
        {
            get { return _adminid; }
            set { _adminid = value; }
        }
        /// <summary>
        /// item_name
        /// </summary>		
        private string _item_name;
        public string item_name
        {
            get { return _item_name; }
            set { _item_name = value; }
        }
        /// <summary>
        /// comment
        /// </summary>		
        private string _comment;
        public string comment
        {
            get { return _comment; }
            set { _comment = value; }
        }
        /// <summary>
        /// datetime
        /// </summary>		
        private DateTime _datetime;
        public DateTime datetime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

    }
}
