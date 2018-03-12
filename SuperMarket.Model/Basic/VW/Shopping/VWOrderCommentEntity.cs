using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public class VWOrderCommentEntity
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
        public IList<VWOrderDetailEntity> OrderDetails;


    }
}
