using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Basic.VW.Shopping
{
    [Serializable]
    public class VWReturnEntity
    {
        public string _OrderCode;
        public string OrderCode
        {
            get
            {
                return _OrderCode;
            }
            set
            {
                _OrderCode = value;
            }
        }

        public int _OrderDetailId;
        public int OrderDetailId
        {
            get
            {
                return _OrderDetailId;
            }

            set
            {
                _OrderDetailId = value;
            }
        }

        public DateTime _CreateTime;
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }

            set
            {
                _CreateTime = value;
            }
        }

        public string _ProductPic;
        public string ProductPic
        {
            get
            {
                return _ProductPic;
            }
            set
            {
                _ProductPic = value;
            }
        }

        public string _ProductName;
        public string ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                _ProductName = value;
            }
        }


   
    }

}
