using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Account
{
    public   class AdminLoginEntity
    {
        public string ResultCode;
        public string ResultMsg;
        public SysUserEntity Member;
        public IList<GYRoleEntity> Roles; 

    }
}
