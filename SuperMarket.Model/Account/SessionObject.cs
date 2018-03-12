using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public class SessionObject
    {
        public string SessionKey { get; set; }
        public User LogonUser { get; set; }
    }

    public class User : IUser<int>
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }
        public bool IsActive { get;   set; }
    }

    public class UserDevice
    {
        public int UserId { get;   set; }
        public DateTime ActiveTime { get;   set; }
        public DateTime ExpiredTime { get;   set; }
        public DateTime CreateTime { get;   set; }
        public int DeviceType { get;   set; }
        public string SessionKey { get;   set; }
    }
}
