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
//using PanGu;
//using Lucene.Net.Analysis.PanGu;
using System.Data.SqlClient;
using SuperMarket.Model;
using SuperMarket.Data.Search;
using Lucene.Net.Analysis.PanGu;

namespace SuperMarket.BLL.Search
{
    public class CreateIndex
    {
        #region 建立索引
        string Table="LuceneProduct";
        public void CreateLuceneProduct()
        {
            IList<ProductLuceneEntity> list =LuceneSearchBLL.Instance.GetLuceneList();

            Directory d = CreateDir(Table);
            IndexWriter writer = new IndexWriter(d, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), true, IndexWriter.MaxFieldLength.LIMITED);
            writer.MergeFactor=20;  //      调整segment合并的频率和大小
            //建立索引字段
            if (list.Count > 0)
            {
                foreach (ProductLuceneEntity model in list)
                {
                    //取得Doc对象
                    Document doc = GetDocumentObj(model);
                    //写入索引文档
                    writer.AddDocument(doc);
                }
            }
            //优化文档
            writer.Optimize();
            //关闭
            writer.Dispose();
            d.Dispose();
        }
        #endregion

        #region 更新索引 
        public void DelIndex(string StyleId)
        {
            Directory d = CreateDir(Table);
            //取得索引修改器
            //IndexWriter modifier = new IndexWriter(d, new PanGuAnalyzer(), false, IndexWriter.MaxFieldLength.LIMITED);
            IndexWriter modifier = new IndexWriter(d, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), false, IndexWriter.MaxFieldLength.LIMITED);
            //删除原索引
            modifier.DeleteDocuments(new Term("StyleCode", StyleId));
            //优化
            modifier.Optimize();
            //关闭
            modifier.Dispose();
            d.Dispose();
        }

       #endregion 

        #region 添加索引
        public void AddIndex(string StyleId)
        { 
            IList<ProductLuceneEntity> list = LuceneSearchBLL.Instance.GetLuceneList();
            if (list.Count == 0)     //无法查询,返回
            {
                return;
            }
            Directory d = CreateDir(Table);
            //取得索引修改器
            //IndexWriter modifier = new IndexWriter(d, new PanGuAnalyzer(), false, IndexWriter.MaxFieldLength.LIMITED);
            IndexWriter modifier = new IndexWriter(d, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), false, IndexWriter.MaxFieldLength.LIMITED);

            if (list.Count > 0)
            {
                foreach (ProductLuceneEntity model in list)
                {
                    //取得Doc对象
                    Document doc = GetDocumentObj(model);
                    //写入索引文档
                    modifier.AddDocument(doc);
                }
            }
            //优化
            modifier.Optimize();
            //关闭
            modifier.Dispose();
            d.Dispose();
        }
        #endregion

        #region 批量更新索引
        //public bool UpdateLucene()
        //{
        //      //商品信息
        //    IList<ProductLuceneEntity> list = SearchDA.Instance.GetLuceneList();
        //    if (list.Count == 0)
        //    {
        //        return true;
        //    }
        //    Directory d = CreateDir(Table);
        //    //IndexWriter modifier = new IndexWriter(d, new PanGuAnalyzer(), false, IndexWriter.MaxFieldLength.LIMITED);
        //    IndexWriter modifier = new IndexWriter(d, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), false, IndexWriter.MaxFieldLength.LIMITED);

        //    foreach (ProductLuceneEntity m in list)
        //    {
        //        try
        //        {
        //            modifier.DeleteDocuments(new Term("StyleCode", m.StyleCode));
        //        }
        //        catch (Exception ex)
        //        {
        //            Core.LogUtil.Log("更新索引失败", m.StyleCode + " - " + ex.Message);
        //        }
        //    }

        //    foreach (GY_StyleLuceneEntity m in list)
        //    {
        //        try
        //        {
        //            //IsKit为ISWEBSALE的值
        //            if (m.IsWebSale == 1)
        //            {
        //                Document doc = GetDocumentObj(m);
        //                //写入索引文档
        //                modifier.AddDocument(doc);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Core.LogUtil.Log("更新索引失败", m.StyleCode + " + " + ex.Message);
        //        }
        //    }
        //    modifier.Optimize();
        //    modifier.Commit();
        //    modifier.Close();
        //    d.Close();

        //    data.DeleteLuceneStyle(list.Max(c => c.MaxId));
        //    return true;
        //}
        #endregion
        #region  创建文件夹
        Document GetDocumentObj(ProductLuceneEntity productinfo)
        {
            Document doc = new Document();
            doc.Add(new Field("ProductId", productinfo.ProductId.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("ProductDetailId", productinfo.ProductDetailId.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Code", productinfo.Code.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Title", productinfo.Title.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("AdTitle", productinfo.AdTitle.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Name", productinfo.Name.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("OECode", productinfo.OECode.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("StyleId", productinfo.StyleId.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("BrandId", productinfo.BrandId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("ClassId", productinfo.ClassId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("CGMemId", productinfo.CGMemId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("IsAhmTake", productinfo.IsAhmTake.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("JiShiSong", productinfo.JiShiSong.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            //doc.Add(new NumericField("Cost", Field.Store.YES, true).SetFloatValue(float.Parse(productinfo.Cost.ToString()))); 
            doc.Add(new Field("Num", productinfo.Num.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Spec1", productinfo.Spec1.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Spec2", productinfo.Spec2.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Spec3", productinfo.Spec3.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("PicSuffix", productinfo.PicSuffix.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("CarTypeRelated", productinfo.CarTypeRelated.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Status", productinfo.Status.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Retail", productinfo.Retail.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Wholesale", productinfo.Wholesale.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Sort", productinfo.Sort.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("PlaceOrigin", productinfo.PlaceOrigin.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Unit", productinfo.Unit.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("PackUnit", productinfo.PackUnit.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IsOEM", productinfo.IsOEM.ToString(), Field.Store.YES, Field.Index.NO)); 
            doc.Add(new Field("PackNum", productinfo.PackNum.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("PicUrl", productinfo.PicUrl.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("ListShowMethod", productinfo.ListShowMethod.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("SiteId", productinfo.SiteId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("Price", productinfo.Price.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("TradePrice", productinfo.TradePrice.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("DealerPrice", productinfo.DealerPrice.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("IsBP", productinfo.IsBP.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("ProductType", productinfo.ProductType.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("SaleNum", productinfo.SaleNum.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("SpecModel", productinfo.SpecModel.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("Manufacturer", productinfo.Manufacturer.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("OEMPartNos", productinfo.OEMPartNos.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("SpecPacking", productinfo.SpecPacking.ToString(), Field.Store.YES, Field.Index.NO));
 
            doc.Add(new Field("SearchFor", productinfo.SearchFor , Field.Store.NO, Field.Index.ANALYZED)); 
 
            return doc;
        }


        public static Directory CreateDir(string Table)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["LuceneFileBootPath"].ToString()+ Table;
            
            //string path = (@"D:\\FieldIndex\\" + Table);
            //System.IO.FileInfo dirFile = new System.IO.FileInfo(path);
            Directory d = FSDirectory.Open(path);
            return d; 
        }

        #endregion
    }
}
