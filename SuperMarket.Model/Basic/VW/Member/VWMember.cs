using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Basic.VW.Member
{
    [Serializable]

    /// <summary>
    /// 用于个人用户审核的视图
    /// </summary>
    public class VWMemberEntity2
    {

        /// <summary>
        /// 个人信息Id
        /// </summary>
        private int _Id;
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string _MemCode;
        public string MemCode
        {
            get;
            set;
        }

        /// <summary>
        /// 身份证正面图片路径
        /// </summary>
        private string _IDPre;
        public string IDPre
        {
            get;
            set;
        }

        /// <summary>
        /// 身份证反面图片路径
        /// </summary>
        private string _IDBeh;
        public string IDBeh
        {
            get;
            set;
        }


        /// <summary>
        /// 个人用户审核状态
        /// </summary>
        private int _Status;
        public int Status
        {
            get;
            set;
        }



    }
}
