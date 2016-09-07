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
    public class BaiduPushNotice
    {
        private static List<tbl_baidupush> models = new List<tbl_baidupush>();
        private static FengNiao.GMTools.Database.BLL.tbl_baidupush modelBLL = new FengNiao.GMTools.Database.BLL.tbl_baidupush();
        private static string APIKEY_ANDROID = ConfigurationManager.AppSettings["APIKEY_ANDROID"];
        private static string SECRETKEY_ANDROID = ConfigurationManager.AppSettings["SECRETKEY_ANDROID"];
        private static string APIKEY_IOS = ConfigurationManager.AppSettings["APIKEY_IOS"];
        private static string SECRETKEY_IOS = ConfigurationManager.AppSettings["SECRETKEY_IOS"];
        private static uint Msg_expires = uint.Parse(ConfigurationManager.AppSettings["PushNoticeRemaindTime"]);

        public static void LoadTask()
        {
            //tbl_baidupush model = new tbl_baidupush();
            models = modelBLL.GetModelList("");
            //CancleAllTask();
            //PushAllTask();
        }
        public static void BeginDailyTask()
        {
            while (true)
            {
                if (DateTime.Now.Hour == 1)
                {
                    //System.Debug.Log("推送正在运行");
                    LoadTask();
                    //CancleAllTask();
                    BeginNewDayTasks();
                }
                System.Threading.Thread.Sleep(3600000);
            }
            
        }
        public static void BeginNewDayTasks()
        {
            PushAllTask();
        }
        public static void PushAllTask()
        {
            foreach (tbl_baidupush model in models)
            {
                PushSigleTask(model);
            }
        }
        public static void PushSigleTask(tbl_baidupush model)
        {
            if (CanSend(model))
            {
                PushAndroidTask(model);
            }
        }
        public static bool CanSend(tbl_baidupush model)
        {
            if (DateTime.Compare((DateTime)model.startime, DateTime.Now) < 0 && DateTime.Compare((DateTime)model.stoptime, DateTime.Now) > 0)
            {
                DateTime sendTime = (DateTime)model.pushtime;
                if (sendTime.Hour - DateTime.Now.Hour > 0)
                {
                    return true;
                }
                else if (sendTime.Hour - DateTime.Now.Hour == 0 && sendTime.Minute - DateTime.Now.Minute > 2)
                {
                    return true;
                }
            }
            return false;
        }
        private static void CancleAllTask()
        {
            foreach (tbl_baidupush model in models)
            {
                CancleSigleTask(model.timer_id);
            }
        }
        public static string CancleSigleTask(string android_timer_id)
        {
            Timer_Cancel_Mod mod = new Timer_Cancel_Mod(APIKEY_ANDROID, android_timer_id);
            Timer_Cancel tCancle = new Timer_Cancel(SECRETKEY_ANDROID, mod);
            string result = tCancle.PushMessage();
            return result;

        }
        

        private static void PushAndroidTask(tbl_baidupush item)
        {
            Notice_Android_Mod mod = new Notice_Android_Mod(item.tile, item.context, "2");
            string messsage = Newtonsoft.Json.JsonConvert.SerializeObject(mod);
            Push_All_Mod pmod = new Push_All_Mod(APIKEY_ANDROID, messsage, (int)Baidu_Helper.Message_Type.Notice, GetSendTime(item));
            pmod.msg_expires = Msg_expires;
            Push_All push = new Push_All(SECRETKEY_ANDROID, pmod);
            string result = push.PushMessage();
            SaveMes_id(result, item);
            System.Threading.Thread.Sleep(2000);
        }
        public static FengNiao.GMTools.Database.Model.tbl_baidupush PushNewAndroidTask(tbl_baidupush item)
        {
            FengNiao.GMTools.Database.Model.tbl_baidupush model = item;
            Notice_Android_Mod mod = new Notice_Android_Mod(item.tile, item.context, "2");
            string messsage = Newtonsoft.Json.JsonConvert.SerializeObject(mod);
            Push_All_Mod pmod = new Push_All_Mod(APIKEY_ANDROID, messsage, (int)Baidu_Helper.Message_Type.Notice, GetSendTime(item));
            pmod.msg_expires = Msg_expires;
            Push_All push = new Push_All(SECRETKEY_ANDROID, pmod);
            string result = push.PushMessage();

            try
            {
                PushNoticeResultMod pnrmod = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<PushNoticeResultMod>(result);
                item.mes_id = pnrmod.response_params.msg_id;
                item.timer_id = pnrmod.response_params.timer_id;
            }
            catch
            {
            }
            return item;
        }
        private static void SaveMes_id(string result, tbl_baidupush item)
        {
            try
            {
                PushNoticeResultMod mod = FengNiao.GameTools.Json.Serialize.ConvertJsonToObject<PushNoticeResultMod>(result);
                modelBLL.UpdateMsg_ID(item, mod.response_params.msg_id, mod.response_params.timer_id);
            }
            catch
            {
            }
        }
        private static void UpdateLastMes_id(string mes_id)
        {

        }
        public static string QueryMesResult(string msg_id)
        {
            Report_Query_Msg_Status_Mod qmsm = new Report_Query_Msg_Status_Mod(APIKEY_ANDROID, msg_id);
            Report_Query_Msg_Status qms = new Report_Query_Msg_Status(SECRETKEY_ANDROID, qmsm);
            string result = qms.PushMessage();
            return result;
        }
        public static string QueryTimerMesResult(string timer_id)
        {
            Timer_Query_List_Mod tqmod = new Timer_Query_List_Mod(APIKEY_ANDROID);
            Timer_Query_List tql = new Timer_Query_List(SECRETKEY_ANDROID, tqmod);
            string result = tql.PushMessage();
            return result;
        }
        public static uint GetSendTime(tbl_baidupush model)
        {
            string time = string.Format(DateTime.Today.ToString("yyyy-MM-dd") + " " + model.pushtime.Value.ToString("HH:mm:ss"));
            return (uint)FengNiao.GameTools.Util.TimeStamp.ConvertDateTimeInt(DateTime.Parse(time));
        }
    }
}