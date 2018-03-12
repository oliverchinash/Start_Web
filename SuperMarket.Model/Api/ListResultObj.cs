using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public  class  ListResultObj
    {
        public int Total;
        public string Listjson;
    }
    public class ListObj
    {
        public int Total;
        public object List ;
    }
    public class ResultObj
    {
        public int Status;
        public object Obj;
    }
}
