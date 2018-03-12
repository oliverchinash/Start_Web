using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Basic.VW.Shopping
{
    [Serializable]
    public class VWOrderListEntity
    {
        public int _OrderId;
        public int OrderId
        {
            get
            {
                return _OrderId;
            }

            set
            {
                _OrderId = value;
            }
        }
        public int _OrderCode;
        public int OrderCode
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

        public string _ProductStyleName;
        public string ProductStyleName
        {
            get
            {
                return _ProductStyleName;
            }

            set
            {
                _ProductStyleName = value;
            }
        }

        public int _Num;
        public int Num
        {
            get
            {
                return _Num;
            }

            set
            {
                _Num = value;
            }
        }

        public string _AccepterName;
        public string AccepterName
        {
            get
            {
                return _AccepterName;
            }

            set
            {
                _AccepterName = value;
            }
        }

        public float _TotalPrice;
        public float TotalPrice
        {
            get
            {
                return _TotalPrice;
            }

            set
            {
                _TotalPrice = value;
            }
        }

        public int _OrderType;
        public int OrderType
        {
            get
            {
                return _OrderType;
            }

            set
            {
                _OrderType = value;
            }
        }

        public int _HasComment;
        public int HasComment
        {
            get
            {
                return _HasComment;
            }
            set
            {
                _HasComment = value;
            }
        }

        public int _Status;
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        public int _ProductStyleId;
        public int ProductStyleId
        {
            get
            {
                return _ProductStyleId;
            }
            set
            {
                _ProductStyleId = value;
            }
        }

        public int _HasReturn;
        public int HasReturn
        {
            get
            {
                return _HasReturn;
            }
            set
            {
                _HasReturn = value;
            }
        }


    }
}
