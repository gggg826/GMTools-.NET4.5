using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FengNiao.GMTools.Database.Model;
using CommLib.BdPush;
using System.Configuration;
using GameToolsCommon;

namespace GameToolsHttpService
{
    public class BaiduPushNotice_old
    {
        private static List<tbl_baidupush> models = new List<tbl_baidupush>();

        private static FengNiao.GMTools.Database.BLL.tbl_baidupush modelBLL = new FengNiao.GMTools.Database.BLL.tbl_baidupush();
        
        private static string APIKEY_ANDROID = ConfigurationManager.AppSettings["APIKEY_ANDROID"];
        private static string SECRETKEY_ANDROID = ConfigurationManager.AppSettings["SECRETKEY_ANDROID"];

        private static string APIKEY_IOS = ConfigurationManager.AppSettings["APIKEY_IOS"];
        private static string SECRETKEY_IOS = ConfigurationManager.AppSettings["SECRETKEY_IOS"];

        private static uint Msg_expires = uint.Parse(ConfigurationManager.AppSettings["PushNoticeRemaindTime"]);

        //public static void reloadInfo(tbl_baidupush model)
        //{
        //model = _model;
        //apikey = model.apiKey;
        //secretKey = model.secretKey;
        //title = model.tile;
        //context = model.context;
        //startdate = (DateTime)model.startime;
        //stopdate = (DateTime)model.stoptime;
        //pushtime = (DateTime)model.pushtime;
        //}

        public static void reloadInfo()
        {
            tbl_baidupush model = new tbl_baidupush();
            models = modelBLL.GetModelList("");
        }

        public static void PushBaiduNotice()
        {
            while (true)
            {
                foreach (tbl_baidupush model in models)
                {
                    AddAndroidPushTask(model);
                }
                System.Threading.Thread.Sleep(60000);
            }
        }
        private static void AddAndroidPushTask(tbl_baidupush item)
        {
            //if (DateTime.Compare((DateTime)item.startime, DateTime.Now) < 0 && DateTime.Compare((DateTime)item.stoptime, DateTime.Now) > 0)
            //{
            //    if (DateTime.Now.Hour == ((DateTime)item.pushtime).Hour)
            //        if (DateTime.Now.Minute == ((DateTime)item.pushtime).Minute)
            //        {
            Notice_Android_Mod mod = new Notice_Android_Mod(item.tile, item.context, "2");
            string messsage = Newtonsoft.Json.JsonConvert.SerializeObject(mod);
            Push_All_Mod pmod = new Push_All_Mod(APIKEY_ANDROID, messsage, (uint)Baidu_Helper.Device_Type.Android, (int)Baidu_Helper.Message_Type.Notice);
            pmod.msg_expires = Msg_expires;
            Push_All push = new Push_All(SECRETKEY_ANDROID, pmod);
            string result = push.PushMessage();
            SaveMes_id(result, item);
            //        }
            //}
        }

        private static void SaveMes_id(string result, tbl_baidupush item)
        {
            try
            {
                PushNoticeResultMod mod = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<PushNoticeResultMod>(result);
                //modelBLL.UpdateMsg_ID(item, mod.response_params.msg_id);
            }
            catch
            {
            	
            }
           
        }

        public static string QueryMesResult(string msg_ids)
        {
            Report_Query_Msg_Status_Mod qmsm = new Report_Query_Msg_Status_Mod(APIKEY_ANDROID, msg_ids);
            Report_Query_Msg_Status qms = new Report_Query_Msg_Status(SECRETKEY_ANDROID, qmsm);
            string result = qms.PushMessage();
            return result;
        }
    }
}