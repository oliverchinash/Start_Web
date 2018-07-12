using SuperMarket.BLL;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;
using SuperMarket.Core;
using System.Threading;
using SuperMarket.Core.Picture;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;

namespace SuPerMarket.BackgroundTask
{   
    public class ComPressPic
    {
        public static void ComPressProductPic()
        {
            bool IsFtpServer = bool.Parse(ConfigurationManager.AppSettings["IsFtpServer"]);
            int Num_i = Convert.ToInt16(ConfigurationManager.AppSettings["NumOnceDeal"]);

            int SleepTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SleepTime"]);
            IList<ConfigPicComPressEntity> picconfig = ConfigPicComPressBLL.Instance.GetConfigListByPicSource((int)SourcePicTypeEnum.Product);
            while (true)
            {
                IList<ProductStylePicsEntity> _piclist = new List<ProductStylePicsEntity>();
                _piclist = ProductStylePicsBLL.Instance.GetStylePicsNoComPress(Num_i);
                if (_piclist != null && _piclist.Count > 0)
                {

                    //ParallelOptions po = new ParallelOptions();
                    //po.MaxDegreeOfParallelism = 15; //最多并发50个任务
                    string FtpServerPath = "";
                    string ImagePath = "";
                    if (IsFtpServer)
                    {
                        FtpServerPath = ConfigurationManager.AppSettings["FtpServerPathPrefix"].ToString();
                    }
                    ImagePath = ConfigurationManager.AppSettings["ImagePath"].ToString();

                    if (picconfig != null && picconfig.Count > 0)
                    {
                        string allids = "0";
                        string successids = "0";
                        string failids = "0";
                        string error = "1";
                        foreach (ProductStylePicsEntity o in _piclist)
                        {
                            allids = allids + "," + o.Id.ToString();
                            string filepath = FtpServerPath + ImagePath.Replace("/", "\\") + o.PicUrl.Replace("/", "\\") + "." + o.PicSuffix;
                            try
                            {
                                if (IsFtpServer)
                                {
                                  
                            
                                    foreach (ConfigPicComPressEntity en in picconfig)
                                    {
                                        Image sourceImg = Image.FromFile(filepath);
                                        ImageFormat _imgf = sourceImg.RawFormat;
                                        string savepath = FtpServerPath + ImagePath.Replace("/", "\\") + o.PicUrl.Replace("/", "\\") +  en.PicName + "." + o.PicSuffix;

                                        int Width = en.Width;
                                        int Height = en.Height;
                                        using (Image ThumbImgl = PicTool.GetImageThumb(sourceImg, Width, Height))
                                        {
                                            ThumbImgl.Save(savepath, _imgf);
                                            ThumbImgl.Dispose();
                                        }
                                        sourceImg.Dispose();
                                    }
                                }
                                else
                                {
                                    FtpUtil _ftp = new FtpUtil();
                                    string downloadpath = ImagePath + o.PicUrl + "." + o.PicSuffix;
                                    byte[] _byt = _ftp.DownloadFile(downloadpath);
                                    Image sourceImg = PicTool.GetImageByBytes(_byt);   
                                       


                                    foreach (ConfigPicComPressEntity en in picconfig)
                                    {
                                        int Width = en.Width;
                                        int Height = en.Height;
                                        string uploadpath = ImagePath + o.PicUrl + "/" + en.PicName + "." + o.PicSuffix;
                                        using (Image ThumbImgl = PicTool.GetImageThumb(sourceImg, Width, Height))
                                        {
                                            byte[] _byts = PicTool.GetByteByImage(ThumbImgl);
                                            _ftp.UploadFile(uploadpath, _byts, false);
                                            ThumbImgl.Dispose();
                                        }
                                    }
                                    sourceImg.Dispose();
                                }
                                successids = successids + "," + o.Id.ToString();
                            }
                            catch (Exception ex)
                            {
                                LogUtil.Log(ex);

                                failids = failids + "," + o.Id.ToString();
                            }
                        }
                        ProductStylePicsBLL.Instance.ComPressComplete(allids, successids, failids);

                    }

                    Thread.Sleep(SleepTime);
                }
            }
        }
      
    }
}
