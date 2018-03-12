using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    [Serializable]
    public class VWXunJiaObj
    {
        #region Public Properties	
        public VWProductEntity Product;
        public VWProductNomalParamEntity ProductExt;
        public string MobilePhone  ;
        public int MemId;
        #endregion
    }
}
