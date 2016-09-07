using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Configuration;

namespace GameToolsCommon
{
    public class CustomWebRequest
    {
        //Encoding requestEncoding;

        public static void Request(string strHttpUrl, string strArgs, Encoding encoding, UploadDataCompletedEventHandler requestCallback)
        {

            string strTempHttpUrl = StandardizationHttpUrl(strHttpUrl);
            WebClient webClient = new WebClient();
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            webClient.UploadDataCompleted += requestCallback;
            webClient.Encoding = encoding;
            //strArgs = System.Web.HttpUtility.HtmlEncode(strArgs);
            byte[] bytes = encoding.GetBytes(strArgs);

            webClient.UploadDataAsync(new Uri(strTempHttpUrl), "POST", bytes);
        }

        public static byte[] Request(string strHttpUrl, string strArgs, Encoding encoding,bool isPostData = false)
        {
            string gmKey = ConfigurationManager.AppSettings["gmKey"];
            string strSorted = SortArgs(strArgs);
            string strSignStrArg = FengNiao.GameToolsCommon.MD5.GetMD5String(strSorted + gmKey);
            strSignStrArg = string.Format("{0}&sign={1}", strSorted, strSignStrArg);
            //byte[] bytes = encoding.GetBytes(strSignStrArg);

            string strTempHttpUrl = StandardizationHttpUrl(strHttpUrl);
            WebClient webClient = new WebClient();
            webClient.Encoding = encoding;

            byte[] bytes = encoding.GetBytes(strSignStrArg);
            if (!isPostData)
            {
                byte[] datas = webClient.DownloadData(new Uri(strTempHttpUrl + "?" + strSignStrArg));
                return datas;
            }
            else
            {
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                byte[] datas = webClient.UploadData(new Uri(strTempHttpUrl), "POST", bytes);
                return datas;
            }   
            
        }


        // 排序要发送的key value
        static string SortArgs(string strArgs)
        {
            SortedDictionary<string, string> argsDic = new SortedDictionary<string, string>();
            string[] strArr = strArgs.Split('&');
            for (int i = 0; i < strArr.Length; i++)
            {
                string[] itemArr = strArr[i].Split('=');
                argsDic.Add(itemArr[0], itemArr[1]);
            }

            //Dictionary<string, string> sortDic = new Dictionary<string, string>();
            //foreach (KeyValuePair<string, string> item in argsDic)
            //{
            //    sortDic.Add(item.Key, item.Value);
            //}
            string strSortArgs = string.Empty;
            foreach (var item in argsDic)
            {
                strSortArgs += string.Format("{0}={1}&", item.Key, item.Value);
            }

            return strSortArgs.Substring(0, strSortArgs.Length - 1);
        }

        /// <summary>
        /// 标准化URL
        /// </summary>
        /// <param name="strHttpUrl"></param>
        /// <returns></returns>
        public static string StandardizationHttpUrl(string strHttpUrl)
        {
            //判断地址是否指定了http协议前缀
            if (strHttpUrl.ToLower().IndexOf("http://") == -1)
            {
                strHttpUrl = string.Format("{0}{1}", "Http://", strHttpUrl);
            }
            return strHttpUrl;
        }
    }

    public class WebDownload : WebClient
    {
        private int _timeout;
        /// <summary>
        /// 超时时间(毫秒)
        /// </summary>
        public int Timeout
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public WebDownload()
        {
            this._timeout = 60000;
        }

        public WebDownload(int timeout)
        {
            this._timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var result = base.GetWebRequest(address);
            result.Timeout = this._timeout;
            return result;
        }


    }
}
