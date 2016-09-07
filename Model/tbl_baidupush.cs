using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.Model
{
    [Serializable]
    public class tbl_baidupush
    {
        public tbl_baidupush()
        { }

        #region     Model
        private int _record_id;
        private string _tile;
        private string _context;
        private DateTime? _startime;
        private DateTime? _stoptime;
        private DateTime? _pushtime;
        private string _mes_id;
        private string _timer_id;

        public int record_id
        {
            get
            {
                return _record_id;
            }

            set
            {
                _record_id = value;
            }
        }

        public string tile
        {
            get
            {
                return _tile;
            }

            set
            {
                _tile = value;
            }
        }

        public string context
        {
            get
            {
                return _context;
            }

            set
            {
                _context = value;
            }
        }

        public DateTime? startime
        {
            get
            {
                return _startime;
            }

            set
            {
                _startime = value;
            }
        }

        public DateTime? stoptime
        {
            get
            {
                return _stoptime;
            }

            set
            {
                _stoptime = value;
            }
        }

        public DateTime? pushtime
        {
            get
            {
                return _pushtime;
            }

            set
            {
                _pushtime = value;
            }
        }

        public string mes_id
        {
            get
            {
                return _mes_id;
            }

            set
            {
                _mes_id = value;
            }
        }

        public string timer_id
        {
            get
            {
                return _timer_id;
            }

            set
            {
                _timer_id = value;
            }
        }
        #endregion
    }
}
