using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public class VWOrderDetailEntity
    {
        public int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public Int64 _OrderCode;
        public Int64 OrderCode
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

        public int _ReturnNum;
        public int ReturnNum
        {
            get
            {
                return _ReturnNum;
            }
            set
            {
                _ReturnNum = value;
            }
        }


        public decimal _Price;
        public decimal Price

        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }

        // 生成订单时的临时订单编码
        private string _AccepterName;
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
        // 生成订单时的临时订单编码
        private string _AccepterAddress;
        public string AccepterAddress
        {
            get
            {
                return _AccepterAddress;
            }
            set
            {
                _AccepterAddress = value;
            }
        }
        private string _AccepterPhone;
        public string AccepterPhone
        {
            get
            {
                return _AccepterPhone;
            }
            set
            {
                _AccepterPhone = value;
            }
        }
        private int _AccepterProvinceId;
        public int AccepterProvinceId
        {
            get
            {
                return _AccepterProvinceId;
            }
            set
            {
                _AccepterProvinceId = value;
            }
        }

        private int _AccepterCityId;
        public int AccepterCityId
        {
            get
            {
                return _AccepterCityId;
            }
            set
            {
                _AccepterCityId = value;
            }
        }
        private string _AccepterProvinceName;
        public string AccepterProvinceName
        {
            get
            {
                return _AccepterProvinceName;
            }
            set
            {
                _AccepterProvinceName = value;
            }
        }
        private string _AccepterCityName;
        public string AccepterCityName
        {
            get
            {
                return _AccepterCityName;
            }
            set
            {
                _AccepterCityName = value;
            }
        }

        public decimal _ActualPrice;
        public decimal ActualPrice
        {
            get
            {
                return _ActualPrice;
            }
            set
            {
                _ActualPrice = value;
            }
        }
        public decimal _TotalPrice;
        public decimal TotalPrice
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
        public int _ProductId;

        public int ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
            }
        }
        public int _ProductDetailId;
        public int ProductDetailId
        {
            get
            {
                return _ProductDetailId;
            }

            set
            {
                _ProductDetailId = value;
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
        public int _TransFeeType;
        public int TransFeeType
        {
            get
            {
                return _TransFeeType;
            }

            set
            {
                _TransFeeType = value;
            }
        }
        public decimal _TransFee ;
        public decimal TransFee 
        {
            get
            {
                return _TransFee ;
            }

            set
            {
                _TransFee  = value;
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
        public int _CanReturn;
        public int CanReturn
        {
            get
            {
                return _CanReturn;
            }

            set
            {
                _CanReturn = value;
            }
        }
        public string _Spec1;
        public string Spec1
        {
            get
            {
                return _Spec1;
            }

            set
            {
                _Spec1 = value;
            }
        }
        public string _Spec2;
        public string Spec2
        {
            get
            {
                return _Spec2;
            }

            set
            {
                _Spec2 = value;
            }
        }
        public string _Spec3;
        public string Spec3
        {
            get
            {
                return _Spec3;
            }

            set
            {
                _Spec3 = value;
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
        public string _ProductPicSuffix;
        public string ProductPicSuffix
        {
            get
            {
                return _ProductPicSuffix;
            }

            set
            {
                _ProductPicSuffix = value;
            }
        }

        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlOld
        {
            get
            {
                return string.IsNullOrEmpty(ProductPic) ? "" : ProductPic + "." + ProductPicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlBig
        {
            get
            {
                return string.IsNullOrEmpty(ProductPic) ? "" : ProductPic + "Big." + ProductPicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlNormal
        {
            get
            {
                return string.IsNullOrEmpty(ProductPic) ? "" : ProductPic + "Normal." + ProductPicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlSmall
        {
            get
            {
                return string.IsNullOrEmpty(ProductPic) ? "" : ProductPic + "Small." + ProductPicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlLittle
        {
            get
            {
                return string.IsNullOrEmpty(ProductPic) ? "" : ProductPic + "Little." + ProductPicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlList
        {
            get
            {
                return string.IsNullOrEmpty(ProductPic) ? "" : ProductPic + "List." + ProductPicSuffix;
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


        public int _PayType;
        public int PayType
        {
            get
            {
                return _PayType;
            }
            set
            {
                _PayType = value;
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
        public int _CGMemId;
        public int CGMemId
        {
            get
            {
                return _CGMemId;
            }

            set
            {
                _CGMemId = value;
            }
        }
        public int _IsAhmTake;
        public int IsAhmTake
        {
            get
            {
                return _IsAhmTake;
            } 
            set
            {
                _IsAhmTake = value;
            }
        }
    }
}
