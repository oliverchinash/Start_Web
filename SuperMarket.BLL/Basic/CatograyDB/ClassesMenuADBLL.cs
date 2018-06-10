using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.ProductDB;

/*****************************************
功能描述：表ClassesMenuAD的业务逻辑层。
创建时间：2017/6/15 15:25:46
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ClassesMenuAD的业务逻辑层。
	/// </summary>
	public class ClassesMenuADBLL
	{
	    #region 实例化
        public static ClassesMenuADBLL Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly ClassesMenuADBLL instance = new ClassesMenuADBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ClassesMenuAD，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classesMenuAD">要添加的ClassesMenuAD数据实体对象</param>
		public   int AddClassesMenuAD(ClassesMenuADEntity classesMenuAD)
		{
			  if (classesMenuAD.Id > 0)
            {
                return UpdateClassesMenuAD(classesMenuAD);
            }
          
            else if (ClassesMenuADBLL.Instance.IsExist(classesMenuAD))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ClassesMenuADDA.Instance.AddClassesMenuAD(classesMenuAD);
            }
	 	}

		/// <summary>
		/// 更新一条ClassesMenuAD记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classesMenuAD">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateClassesMenuAD(ClassesMenuADEntity classesMenuAD)
		{
			return ClassesMenuADDA.Instance.UpdateClassesMenuAD(classesMenuAD);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassesMenuADByKey(int id)
        {
            return ClassesMenuADDA.Instance.DeleteClassesMenuADByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassesMenuADDisabled()
        {
            return ClassesMenuADDA.Instance.DeleteClassesMenuADDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassesMenuADByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ClassesMenuADDA.Instance.DeleteClassesMenuADByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassesMenuADByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassesMenuADDA.Instance.DisableClassesMenuADByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ClassesMenuAD实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ClassesMenuAD实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ClassesMenuADEntity GetClassesMenuAD(int id)
		{
			return ClassesMenuADDA.Instance.GetClassesMenuAD(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ClassesMenuADEntity> GetClassesMenuADList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ClassesMenuADDA.Instance.GetClassesMenuADList(pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 根据分类Id获取对应品牌
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public IList<BrandEntity> GetBrandsByClassId(int classid)
        {
            IList<ClassesMenuADEntity> listmenu = ClassesMenuADDA.Instance.GetClassesMenuADAll(classid, (int)ClassesAdShowType.Brand);
            IList<BrandEntity> brandlist = new List<BrandEntity>();
            if (listmenu != null && listmenu.Count > 0)
            {
                for (int i =0;i < listmenu.Count; i++)
                {
                    ClassesMenuADEntity entity = listmenu[i];
                    BrandEntity brand = BrandBLL.Instance.GetBrand(entity.BrandOrProductId);
                    brandlist.Add(brand);
                }
            }
           
            return brandlist; 
        }
        public IList<VWProductEntity> GetProductsByClassId(int classid)
        {
            IList<VWProductEntity> productlist = new List<VWProductEntity>();
            IList<ClassesMenuADEntity> listmenu = ClassesMenuADDA.Instance.GetClassesMenuADAll(classid,(int)ClassesAdShowType.Product);
            foreach (ClassesMenuADEntity entity in listmenu)
            {
                VWProductEntity product = ProductBLL.Instance.GetProVWByDetailId(entity.BrandOrProductId);
                productlist.Add(product);
            }
            return productlist;
        }
      
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ClassesMenuADEntity classesMenuAD)
        {
            return ClassesMenuADDA.Instance.ExistNum(classesMenuAD)>0;
        }
		
	}
}

