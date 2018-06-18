using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.Search;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;
using SuperMarket.BLL.CatograyDB;

/*****************************************
功能描述：表Product的业务逻辑层。
创建时间：2016/10/31 16:28:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.Search
{
	  
	/// <summary>
	/// 表Product的业务逻辑层。
	/// </summary>
	public class LuceneSearchBLL
    {
        public static LuceneSearchBLL Instance
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
            internal static readonly LuceneSearchBLL instance = new LuceneSearchBLL();
        }  
        public IList<ProductLuceneEntity> GetLuceneList()
        {
            IList<ProductLuceneEntity> list= LuceneSearchDA.Instance.GetLuceneList();
            if(list!=null&& list.Count > 0)
            {
                foreach(ProductLuceneEntity entity in list)
                {
                    entity.ClassName = ClassesFoundBLL.Instance.GetClassesFound(entity.ClassId,false).Name;
                    entity.BrandName = BrandBLL.Instance.GetBrand(entity.BrandId, false).Name;
                    entity.SearchFor = entity.ClassName+" "+entity.BrandName + " " + entity.AdTitle + " " + entity.OEMPartNos + " " + entity.Manufacturer + " " + entity.Spec1 + " " + entity.Spec2 + " " + entity.Code;
                }
            }

            return list;

        }
        
    }
}

