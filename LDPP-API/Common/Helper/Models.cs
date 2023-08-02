using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDPP_API.Common.Helper;

namespace LDPP_API.Common.Helper
{
    public class Models
        //internal class Models
    {
        public class LdppJson
        {
            //  {"ResponseCode" : "0", "Message" : "CREATE_AP_OK", "Payload":"{"Status": 0, "SSID": "WIFID-H" , "PWD": "12345678"}"} 

            public string ResponseCode  { get;set; }
            public string Message { get; set; }
            public Payload Payload { get; set; }

        }
        public class Payload
        {
            public int Status { get; set; }
            public string SSID { get; set; }
            public string PWD { get; set; }
        }
    }

}
