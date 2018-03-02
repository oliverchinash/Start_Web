using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.Util
{
 public    class FilePath
    {
        public static string PathCombine(params string[] paths)
        {
            if (paths == null || paths.Length == 0)
                return "";
            if (  paths.Length == 1)
                return paths[0];
            string pathstr ="/"+ paths[0].Trim('/');
            for  (int i=1;i< paths.Length;i++)
            {
                pathstr = pathstr + "/" + paths[i].Trim('/');
            }
            return pathstr;
        }
    }
}
