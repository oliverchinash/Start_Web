using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public partial class  ProductStyleProEntity
    { 
        /// <summary>
        /// 属性值
        /// <summary>
        private string _PropertyDetailName;
        public string PropertyDetailName
        {
            get
            {
                return _PropertyDetailName;
            }
            set
            {
                _PropertyDetailName = value;
            }
        } 
    }
}
