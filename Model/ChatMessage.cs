using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengNiao.GMTools.Database.Model
{
    public class ChatMessage
    {
        private string time;
        public string Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
            }
        }


        private string kind;
        public string Kind
        {
            get
            {
                return kind;
            }

            set
            {
                kind = value;
            }
        }


        private string playerName;
        public string PlayerName
        {
            get
            {
                return playerName;
            }

            set
            {
                playerName = value;
            }
        }


        private string message;
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        
        private string server_id;

        public string Server_id
        {
            get
            {
                return server_id;
            }

            set
            {
                server_id = value;
            }
        }


        private string role_id;
        public string Role_id
        {
            get
            {
                return role_id;
            }

            set
            {
                role_id = value;
            }
        }


        private string toRole_id;
        public string ToRole_id
        {
            get
            {
                return toRole_id;
            }

            set
            {
                toRole_id = value;
            }
        }



        public ChatMessage(string strMessage)
        {
            string str = strMessage.Replace('[',',');
            str = str.Replace(']', ',');

            if (string.IsNullOrEmpty(strMessage)) return;
            string[] strArr = str.Split(',');


            time = strArr[0];
            kind = strArr[1];
            playerName = strArr[2];
            role_id = strArr[3];
            server_id = strArr[3].Substring(0, strArr[3].IndexOf('-'));
            message = strMessage.Substring(strMessage.LastIndexOf(':')+1);

            if(kind == "PRIVATE")
            {
                toRole_id = strMessage.Substring(strMessage.IndexOf('>') + 1);
                toRole_id = toRole_id.Substring(0, toRole_id.LastIndexOf(':'));
            }

        }
    }
}
