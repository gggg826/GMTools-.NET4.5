using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace CustomWebClient
{
    public class WebClientEx
    {

        public static byte[] GetData(string url, string args, Encoding encoding, string cookie = "")
        {
            TcpClient tcpClient = new TcpClient();
            tcpClient = new TcpClient();
            Uri tUri = new Uri(url);
            tcpClient.Connect(tUri.Host, tUri.Port);
            StringBuilder requestHeaders = new StringBuilder();
            string getUrl = string.IsNullOrEmpty(args) ? tUri.PathAndQuery : tUri.PathAndQuery + "?" + args;
            requestHeaders.Append("GET" + " " + getUrl + " HTTP/1.1\r\n");
            requestHeaders.Append("Content-Type:application/x-www-form-urlencoded\r\n");
            requestHeaders.Append("User-Agent:Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.64 Safari/537.11\r\n");
            if (!string.IsNullOrEmpty(cookie))
            {
                requestHeaders.Append("Cookie:" + cookie + "\r\n");
            }
            requestHeaders.Append("Accept:*/*\r\n");
            requestHeaders.Append("Host:" + tUri.Host + "\r\n");
            //requestHeaders.Append("Connection:close\r\n\r\n");
            string strHeaders = requestHeaders.ToString();
            byte[] request = Encoding.UTF8.GetBytes(strHeaders);
            int sendLength =tcpClient.Client.Send(request);
            List<byte> buffer = new List<byte>();
            byte[] responseByte = new byte[4096];
            try
            {
                int len = tcpClient.Client.Receive(responseByte);
                //if (len >= 4096)
                //{
                    while (len > 0)
                    {
                        byte[] tempBytes = new byte[len];
                        Array.Copy(responseByte, 0, tempBytes, 0, len);
                        buffer.AddRange(tempBytes);
                        len = tcpClient.Client.Receive(responseByte);
                    }
                //}
                //else
                //{
                //    byte[] tempBytes = new byte[len];
                //    Array.Copy(responseByte, 0, tempBytes, 0, len);
                //    buffer.AddRange(tempBytes);
                //}
                
            }
            catch (System.Exception ex)
            {

            }
            byte[] bytes = buffer.ToArray();
            string strContent = encoding.GetString(bytes);
            /*
            Match match = Regex.Match(strContent, "Content-Length: (?<length>\\d+)");
            if (match.Success)
            {
                int contentLength = int.Parse(match.Groups["length"].Value);
                byte[] contentBytes = new byte[contentLength];
                Array.Copy(bytes, bytes.Length - contentLength, contentBytes, 0, contentLength);
                return contentBytes;
            }
            else
            {*/
                int start = strContent.IndexOf("\r\n\r\n") + 4;//找到空行位置
                if (start != -1)
                {
                    strContent = strContent.Substring(start);
                    return encoding.GetBytes(strContent);
                }
                else
                {
                    return new byte[0];
                }
            //}

        }
    }
}
