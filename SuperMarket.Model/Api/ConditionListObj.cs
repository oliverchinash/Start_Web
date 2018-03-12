using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    /// <summary>
    /// 分页查询参数传递对象
    /// </summary>
    public class ConditionListObj
    {
        public ConditionListObj()
        {
            ConditionList = new List<ConditionUnit>();
        }
        /// <summary>
        /// 查询页码
        /// </summary>
        public int PageIndex;
        /// <summary>
        /// 当前显示页数
        /// </summary>
        public int PageSize;
        /// <summary>
        /// 查询条件
        /// </summary>
        public IList<ConditionUnit> ConditionList;
    }
    public class ConditionUnit
    {
        /// <summary>
        /// 字段名-"$"为搜索查询关键词
        /// </summary>
        public string FieldName;
        /// <summary>
        /// 比较值
        /// </summary>
        public object CompareValue; 
    } 
}
