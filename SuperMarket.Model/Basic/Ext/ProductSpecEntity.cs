using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{ 

    public partial class ProductSpecEntity
    {
        /// <summary>
        /// 规格值的Id
        /// <summary>
        private string _SpecDetailName;
        public string SpecDetailName
        {
            get
            {
                return _SpecDetailName;
            }
            set
            {
                _SpecDetailName = value;
            }
        }

    }
}
