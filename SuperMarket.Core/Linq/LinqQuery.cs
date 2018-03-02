using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core 
{
   public class LinqQuery
    {
        /// <summary>
        /// 输入分页字符页码
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="objs"></param>
        /// <returns></returns>
        public string GetQueryPagesMenu<T>(int PageSize, IList<T> objs)
        {
            int Count;
            var db = objs;
            var query = from cms_contents in db select cms_contents;
            Count = (query.Count() / PageSize + 1);//不足一页按一页算
            string PageMenu = "";
            //拼接分页目录
            for (int i = 1; i <= Count; i++)
            {
                PageMenu += "<a href='?CurPage=" + i.ToString() + "'>" + i.ToString() + "</a>&nbsp;|&nbsp;";
            }
            return PageMenu;
        }
        /// <summary>
        /// 获取分页后的数据集
        /// </summary>
        /// <param name="PageSize">每页显示的记录数</param>
        /// <param name="CurPage">页数</param>
        /// <param name="objs">数据总集合</param>
        /// <returns></returns>
        public static List<T> QueryByPage<T>(int PageSize, int CurPage, IList<T> objs)
        {
            var query = from cms_contents in objs select cms_contents;
            return query.Take(PageSize * CurPage).Skip(PageSize * (CurPage - 1)).ToList();
        
        }
      
    }
}
