using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public partial class ComProDetailsEntity
    {  /// <summary>
       /// 属性Id
       /// <summary>
        private string _PropertyName;
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                _PropertyName = value;
            }
        }
    }
}
