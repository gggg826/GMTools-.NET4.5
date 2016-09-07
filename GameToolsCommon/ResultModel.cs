using System;
using System.Collections.Generic;
using System.Text;

namespace GameToolsCommon
{
    public class OperateResultModel
    {
        public bool IsSuccess { set; get; }
        public string Guid { set; get; }
        public string Content { set; get; }
    }
    public class ResultModel
    {
        public bool IsSuccess { set; get; }
        public string Content { set; get; }
    }

    public class HttpResultModel
    {
        public int errorcode { set; get; }
        public string result { set; get; }
    }

    public class AsyncResult
    {
        private static string _content = string.Empty;

        public static string content
        {
            get
            {
                return _content;
            }

            set
            {
                _content = value;
            }
        }
    }

    public class PushNoticeResultMod
    {
        public string request_id { get; set; }
        public ResponseParams response_params { set; get; }
    }
    public class ResponseParams
    {
        public int total_num { get; set; }
        public string msg_id { get; set; }
        public string send_time { get; set; }
        public string timer_id { get; set; }
        public Result result { set; get; }
    }
    




    public class QueryNoticeResultModel
    {
        public int request_id { get; set; }
        public QueryResponseParams response_params { set; get; }
    }
    public class QueryResponseParams
    {
        public int total_num { get; set; }
        public Result result { set; get; }
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
