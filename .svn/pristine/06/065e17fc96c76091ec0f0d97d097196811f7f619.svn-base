using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLib.BdPush
{
    public class Notice_IOS_Mod
    {
        public Dictionary<string, string> keyvalue1 { set; get; }
        public Dictionary<string, string> keyvalue2 { set; get; }
        public ApsModel aps { get; set; }

        public Notice_IOS_Mod(string alert)
        {
            aps.alert = alert;
        }
    }

    public class ApsModel
    {
        public string alert { set; get; }
        public string sound { set; get; }
        public string badge { set; get; }
    }
}
