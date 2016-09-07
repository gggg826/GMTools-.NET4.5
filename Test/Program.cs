using System;
using System.Collections.Generic;
using System.Text;
using FengNiao.GameToolsCommon;
using GameToolsCommon;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Drawing;

namespace Test
{
    class Program
    {
        static void JustTest()
        {
            List<tbl_recommend> list = new List<tbl_recommend>();
            tbl_recommend model = new tbl_recommend();
            model.record_id = 5;
            model.id = 1;
            model.isopen = 1;
            model.starttime = System.DateTime.Now;
            model.stoptime = System.DateTime.Now;
            model.server_id = 1002;
            model.rank = 1;
            model.queue = 11;

            tbl_recommend mod = new tbl_recommend();
            mod.record_id = 5;
            mod.id = 1;
            mod.isopen = 1;
            mod.starttime = System.DateTime.Now;
            mod.stoptime = System.DateTime.Now;
            mod.server_id = 1002;
            mod.rank = 1;
            mod.queue = 11;

            list.Add(model);
            list.Add(mod);

            string str = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<tbl_recommend>>(list);

            List<tbl_recommend> configModels = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<tbl_recommend>(str);
        }


        public partial class tbl_recommend
        {
            public tbl_recommend()
            { }
            #region Model
            private int _record_id;
            private int _id;
            private int? _isopen;
            private DateTime? _starttime;
            private DateTime? _stoptime;
            private int? _server_id;
            private int? _rank;
            private int? _queue;
            /// <summary>
            /// auto_increment
            /// </summary>
            public int record_id
            {
                set { _record_id = value; }
                get { return _record_id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int id
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? isopen
            {
                set { _isopen = value; }
                get { return _isopen; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? starttime
            {
                set { _starttime = value; }
                get { return _starttime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? stoptime
            {
                set { _stoptime = value; }
                get { return _stoptime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? server_id
            {
                set { _server_id = value; }
                get { return _server_id; }
            }

            public int? rank
            {
                get { return _rank; }
                set { _rank = value; }
            }

            public int? queue
            {
                get { return _queue; }
                set { _queue = value; }
            }

            #endregion Model

        }

        static void Main(string[] args)
        {
            JustTest();
            //byte[] bytes = CustomWebRequest.Request("http://192.168.2.58:8080/gm", "cmd=locklist", Encoding.UTF8);
            //string strContent = Encoding.UTF8.GetString(bytes);

            //JObject contentObj = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject(strContent);
            //int errorcode = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(contentObj, "errorcode");
            //JArray resultObj = FengNiao.GameTools.Json.Serialize.GetJsonObject<JArray>(contentObj, "result");
            //int bunkerlv = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(resultObj, "bunkerlv");
            //string channel = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "channel");
            //string createtime = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "createtime");
            //string deviceid = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "deviceid");
            //int diamond = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(resultObj, "diamond");
            //int gold = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(resultObj, "gold");
            //string logintime = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "logintime");
            //string logoutime = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "logoutime");
            //JArray mercenarys = FengNiao.GameTools.Json.Serialize.GetJsonObject<JArray>(resultObj, "mercenarys");
            //for (int i = 0; i < mercenarys.Count; i++)
            //{
            //    JObject mercenaryObj = mercenarys[i] as JObject;
            //    int level = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(mercenaryObj, "level");
            //    int star = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(mercenaryObj, "star");
            //    int stdid = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(mercenaryObj, "stdid");
            //}
            //string phone = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "phone");
            //string platform_userid = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "platform_userid");
            //string role_name = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "role_name");
            //string roleid = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "roleid");
            //int stamina = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(resultObj, "stamina");
            //string userid = FengNiao.GameTools.Json.Serialize.GetJsonObject<string>(resultObj, "userid");
            //int vip_lv = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(resultObj, "vip_lv");
            //JArray weapons = FengNiao.GameTools.Json.Serialize.GetJsonObject<JArray>(resultObj, "weapons");
            //for (int i = 0; i < weapons.Count; i++)
            //{
            //    JObject weaponObj = weapons[i] as JObject;
            //    int level = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(weaponObj, "level");
            //    int star = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(weaponObj, "star");
            //    int stdid = FengNiao.GameTools.Json.Serialize.GetJsonObject<int>(weaponObj, "stdid");
            //}
            //Console.Read();

        }
    }
}
