using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.Model
{
    public class QueryNoticeResultModel
    {
        //public QueryNoticeResultModel()
        //{
        //    response_params = new QueryResponseParams();
        //}
        public int request_id { get; set; }
        public QueryResponseParams response_params { set; get; }
    }
    public class QueryResponseParams
    {
        //public QueryResponseParams()
        //{
        //    result = new List<Result>();
        //}
        public int total_num { get; set; } 
        public List<Result> result { set; get; }
    }

    public class Result
    {
        public int send_time { set; get; }
        public int success { set; get; }
        public int total_num { set; get; }
        public int status { set; get; }
        public string msg_id { set; get; }
    }
}
