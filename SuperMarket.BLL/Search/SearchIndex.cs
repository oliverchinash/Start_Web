using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;
using PanGu;
//using Lucene.Net.Analysis.PanGu;
using System.Data.SqlClient;
using System.Diagnostics;
using SuperMarket.Model;
using SuperMarket.Core;
using System.Collections;
using Lucene.Net.Highlight;
using SuperMarket.Core.Util;
using Lucene.Net.Analysis.PanGu; 

namespace SuperMarket.BLL.Search
{
    public class SearchIndex
    {
        string Table = "LuceneProduct";
        #region LuceneSearch
        /// 根据keyword检索数据,反回已分页的List
        /// </summary>
        /// <param name="keyword">查找的关键字</param>
        /// <param name="pageSize">分页每页的大小</param>
        /// <param name="pageIndex">反回第几页的数据</param>
        /// <returns>结果数据集,Tables[0]是结果数据,Tables[1]是存放总记录数(total数据)以及总页数的表</returns>
        public GyLuceneEntity LuceneSearch(string keyword, int websiteid, int brandid, int classid,int jishi, int pageSize, int pageIndex)
        { 
            if (keyword != "")
            {
                keyword = keyword.ToLower();
                keyword = keyword.Replace("(", " ");
                keyword = keyword.Replace("?", " ");
                keyword = keyword.Replace(")", " ");
                keyword = keyword.Replace("*", " ");
                keyword = keyword.Replace("{", " ");
                keyword = keyword.Replace("}", " ");
                keyword = keyword.Replace("[", " ");
                keyword = keyword.Replace("]", " ");
                keyword = keyword.Replace("or", " ");
                keyword = keyword.Replace("and", " ");
                keyword = keyword.Replace("not", " ");
                keyword = keyword.Trim();
            }
            GyLuceneEntity lucelist = new GyLuceneEntity();
 
            List<ProductLuceneEntity> list = new List<ProductLuceneEntity>();
            List<int> listclass = new List<int>();
            List<int> listbrand = new List<int>();
            int result = 0;     //结果 
            int startPost = pageSize * (pageIndex - 1);
            List<Document> doc;

            //取得搜索结果
            doc = seacher(keyword, websiteid, brandid, classid, jishi, out result);

            if (doc != null)
            { 
                //for (int i = 0; i < pageSize && startPost < result && startPost < startPost + pageSize; i++, startPost++)
                //{
                //添加信息
                //AppendList(list, doc[startPost], startPost, keyword);
                //}

                for (int i = 0; i < result; i++)
                {
                    AppendList(list, doc[i], i, keyword);
                     
                } 
                result = list.Count;
            }
            else
            {
                list = null;
            }

            lucelist.ProductList = list;
            foreach(ProductLuceneEntity entity in list)
            {
                listclass.Add(entity.ClassId);
                listbrand.Add(entity.BrandId);
            }
            lucelist.Count = list.Count;
            listclass= listclass.Distinct().ToList<int>();
            listbrand = listbrand.Distinct().ToList<int>();


            int count = pageSize;
            int start = (pageIndex - 1) * pageSize;
            if (count > (lucelist.Count - start))
            {
                count = (lucelist.Count - start);
            }
            if (count < 0)
            {
                start = 0;
                count = pageSize; 
                if (count > (lucelist.Count - start))
                {
                    count = (lucelist.Count - start);
                }
            } 
            lucelist.ProductList = lucelist.ProductList.GetRange(start, count);
            lucelist.ClassList = listclass;
            lucelist.BrandList = listbrand;
              
            return lucelist;
        }
      
        /// <summary>
        /// 取得搜索信息
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public List<Document> seacher(string strqueryString, int websiteid,int brandid,int  classid  ,int jishi, out int TotalCount)
        { 
            string queryString = strqueryString; 
            Directory d = CreateIndex.CreateDir(Table);
            TopDocs docs;
            BooleanQuery query;
            List<Document> doc = new List<Document>();
            TotalCount = 0;
            try
            {
                query = new BooleanQuery(); 
                //IndexSearcher mysea = new IndexSearcher(d, true);  
                IndexSearcher mysea = new IndexSearcher(IndexReader.Open(d,true)); 
                if (queryString != "")
                {
                    IList<string> querystrattr = StringUtils.GetLuceneChinaStrAttr(queryString);
                    foreach (string querystr in querystrattr)
                    {
                        Query query1;
                        QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "SearchFor", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
                        query1 = qp.Parse(querystr);
                        query.Add(query1, Occur.MUST); 
                    } 
                }
                //if (websiteid > 0)
                //{
                //    //不能用BrandId来索引,会有重复数据.
                //    Query query2 = new TermQuery(new Term("SiteId", websiteid.ToString()));
                //    query.Add(query2, Occur.MUST);
                //}
                //else
                //{
                //    Query query2 = new TermQuery(new Term("SiteId", "1"));
                //    query.Add(query2,  Occur.MUST);
                //}
                if (brandid > 0)
                {
                    //不能用BrandId来索引,会有重复数据.
                    Query query3 = new TermQuery(new Term("BrandId", brandid.ToString()));
                    query.Add(query3, Occur.MUST);
                }
                if (classid > 0)
                {
                    //不能用BrandId来索引,会有重复数据.
                    Query query4 = new TermQuery(new Term("ClassId", classid.ToString()));
                    query.Add(query4, Occur.MUST);
                }
                if (jishi > 0)
                {
                    //不能用BrandId来索引,会有重复数据.
                    Query queryjishisong = new TermQuery(new Term("JiShiSong", jishi.ToString()));
                    query.Add(queryjishisong, Occur.MUST);
                }
                docs = mysea.Search(query, null,500);
                ScoreDoc[] hits = docs.ScoreDocs;  //获取命中的文档信息对象 
                //TotalCount = docs.totalHits;
                TotalCount = hits.Length;

                for (int i = 0; i < hits.Length; i++)
                {
                    Document hitDoc = mysea.Doc(hits[i].Doc); //根据命中的文档的内部编号获取该文档 
                    doc.Add(hitDoc);
                }
                mysea.Dispose();
            }
            catch (Exception e)
            {
                LogUtil.Log("搜索失败",e.Message);
                query = null;
                docs = null;
            }
            return doc;
        }
        public List<Document> SeacherForClass(string strqueryString, int websiteid, out int TotalCount)
        { 
            string queryString = strqueryString; 
            Directory d = CreateIndex.CreateDir(Table);
            TopDocs docs;
            BooleanQuery query;
            List<Document> doc = new List<Document>();
            TotalCount = 0;
            try
            {
                query = new BooleanQuery(); 
                IndexSearcher mysea = new IndexSearcher(IndexReader.Open(d, true));


                if (queryString != "")
                {
                    Query query1;
                    QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "SearchFor", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
                    query1 = qp.Parse(queryString + "*");
                    query.Add(query1, Occur.MUST);
                    
                }
                docs = mysea.Search(query, null, 500);
                ScoreDoc[] hits = docs.ScoreDocs;  //获取命中的文档信息对象 
                //TotalCount = docs.totalHits;
                TotalCount = hits.Length; 
                for (int i = 0; i < hits.Length; i++)
                {
                    Document hitDoc = mysea.Doc(hits[i].Doc); //根据命中的文档的内部编号获取该文档 
                    doc.Add(hitDoc);
                }
                mysea.Dispose();
            }
            catch (Exception e)
            {
                LogUtil.Log("搜索失败", e.Message);
                query = null;
                docs = null;
            }
            return doc;
        }


        void AppendList(IList<ProductLuceneEntity> list, Document doc, int startPost, string keywords )
        {
            ProductLuceneEntity model = new ProductLuceneEntity();
                
            model.PicUrl = StringUtils.SafeStr(doc.GetField("PicUrl").StringValue);
            model.PicSuffix = StringUtils.SafeStr(doc.GetField("PicSuffix").StringValue);
            model.Code = StringUtils.SafeStr(doc.GetField("Code").StringValue);
            model.ProductId = StringUtils.GetDbInt(doc.GetField("ProductId").StringValue);
            model.ProductDetailId = StringUtils.GetDbInt(doc.GetField("ProductDetailId").StringValue);
            model.ClassId = StringUtils.GetDbInt(doc.GetField("ClassId").StringValue);
            model.BrandId = StringUtils.GetDbInt(doc.GetField("BrandId").StringValue);
            model.AdTitle = doc.GetField("AdTitle").StringValue;
            model.Spec1 =  doc.GetField("Spec1").StringValue;
            model.Spec2=  doc.GetField("Spec2").StringValue;
            model.Price = StringUtils.GetDbDecimal(doc.GetField("Price").StringValue);
            model.TradePrice = StringUtils.GetDbDecimal(doc.GetField("TradePrice").StringValue);
            model.DealerPrice = StringUtils.GetDbDecimal(doc.GetField("DealerPrice").StringValue);
            model.IsBP = StringUtils.GetDbInt(doc.GetField("IsBP").StringValue);
            model.IsOEM = StringUtils.GetDbInt(doc.GetField("IsOEM").StringValue);
            model.Manufacturer =  doc.GetField("Manufacturer").StringValue;
            if (keywords != "")
            {
                Lucene.Net.Analysis.Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "SearchFor", analyzer);
                Query query1 = qp.Parse(keywords); 



                //PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter = new PanGu.HighLight.SimpleHTMLFormatter("<font class=detailprice>", "</font>");
                //PanGu.HighLight.Highlighter highlighter = new PanGu.HighLight.Highlighter(simpleHTMLFormatter, new Segment());
                //highlighter.FragmentSize = 50;
                //model.HLStyleName = highlighter.GetBestFragment(keywords, Mola.Core.StringUtils.SafeStr(doc.GetField("StyleName").StringValue()));
            }
            //model.Cost =  StringUtils.SafeDecimal(doc.GetField("Cost").StringValue(), 0);
            //model.MarketPrice = StringUtils.SafeDecimal(doc.GetField("MarketPrice").StringValue(), 0);

            list.Add(model);

        }
        #endregion
    }
}
