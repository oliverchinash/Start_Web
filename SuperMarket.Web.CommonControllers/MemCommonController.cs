using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Core.WebService;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.CommonControllers
{
    public class MemCommonController : BaseMemberController
    {
        public string MemUploadImg()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            int aa = files.Count;
            int _pictype = FormString.IntSafeQ("pictype");
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            CertificateType _certype = (CertificateType)_pictype;
            if (file != null)
            {
                byte[] bytes = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    bytes = binaryReader.ReadBytes(file.ContentLength);
                }
                FtpUtil _ftp = new FtpUtil();
                Random _rd = new Random();
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName, _certype.ToString(), memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
                string dicpathfull = dicpath + file.FileName.Substring(file.FileName.LastIndexOf("."));
                string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                _ftp.UploadFile(certifapath, bytes, true);
                return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"pic_raw\" : \"" + dicpathfull + "\"}";
            }

            return "";
        }

        public string MemUploadImgBit()
        {
            int _pictype = QueryString.IntSafeQ("pictype");
            string filename = QueryString.SafeQ("name");
            using (Stream filesStream = System.Web.HttpContext.Current.Request.InputStream)
            {

                CertificateType _certype = (CertificateType)_pictype;

                byte[] bytes = new byte[filesStream.Length];
                filesStream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                filesStream.Seek(0, SeekOrigin.Begin);
                FtpUtil _ftp = new FtpUtil();
                Random _rd = new Random();
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName, _certype.ToString(), memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
                string dicpathfull = dicpath + filename.Substring(filename.LastIndexOf("."));
                string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                _ftp.UploadFile(certifapath, bytes, true);
                return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"pic_raw\" : \"" + dicpathfull + "\"}";
            }

            return "";
        }
    }
}
