using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
   public class WeiXinAccessEntity
    {
        public string access_token;
        public string expires_in;
    }
    public class WeiXinFailEntity
    {
        public string errcode;
        public string errmsg;
        public string msgid;
    }
    public class WeiXinTicketEntity
    {
        public string errcode;
        public string errmsg;
        public string ticket;
        public string expires_in;   
    }
}
