﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.Model
{
    [Serializable]
    public partial class tbl_activity_details
    {
        public tbl_activity_details()
        { }

        private int _fld_record_id;
        private int _fld_solution_id;
        private int _fld_open_type;
        private int _fld_activity_id;
        private string _fld_preview_time;
        private string _fld_open_time;
        private string _fld_close_time;
        private string _fld_award_time;
        private int _fld_dataid;

        public int fld_solution_id
        {
            get
            {
                return _fld_solution_id;
            }

            set
            {
                _fld_solution_id = value;
            }
        }

        public int fld_open_type
        {
            get
            {
                return _fld_open_type;
            }

            set
            {
                _fld_open_type = value;
            }
        }

        public int fld_activity_id
        {
            get
            {
                return _fld_activity_id;
            }

            set
            {
                _fld_activity_id = value;
            }
        }

        public string fld_preview_time
        {
            get
            {
                return _fld_preview_time;
            }

            set
            {
                _fld_preview_time = value;
            }
        }

        public string fld_open_time
        {
            get
            {
                return _fld_open_time;
            }

            set
            {
                _fld_open_time = value;
            }
        }

        public string fld_close_time
        {
            get
            {
                return _fld_close_time;
            }

            set
            {
                _fld_close_time = value;
            }
        }

        public string fld_award_time
        {
            get
            {
                return _fld_award_time;
            }

            set
            {
                _fld_award_time = value;
            }
        }

        public int fld_record_id
        {
            get
            {
                return _fld_record_id;
            }

            set
            {
                _fld_record_id = value;
            }
        }
        

        public int fld_dataid
        {
            get
            {
                return _fld_dataid;
            }

            set
            {
                _fld_dataid = value;
            }
        }
    }
}
