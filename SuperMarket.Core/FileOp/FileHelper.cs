using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SuperMarket.Core.FileOp
{
    public class FileHelper
    {    
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool FileExits(String filepath)
        {
            try
            {
                return File.Exists(filepath);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool FolderExits(String filepath)
        {
            try
            {
                return Directory.Exists(filepath);
            }
            catch
            {
                return false;
            }
        }
        public static void CreatFolder(string forderpath)
        {
            if (!FolderExits(forderpath))
            {
                Directory.CreateDirectory(forderpath);
            }
        }
        /// <summary>
        /// 遍历文件夹得到子文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string[] GetFiles(String filepath)
        {
           
            try
            {
                if (FolderExits(filepath))
                    return Directory.GetFiles(filepath);
                else
                    return null;
            }
            catch
            {
                return null;
            } 
            
        }
        /// <summary>
        /// 遍历文件夹得到子文件夹
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string[] GetFolders(String filepath)
        {

            try
            {
                if (FolderExits(filepath))
                    return Directory.GetDirectories(filepath);
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// 遍历文件夹得到子文件夹和子文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string[] GetFolderAndFile(String filepath)
        {
            try
            {

                if (FolderExits(filepath))
                {
                    string[] str1 = Directory.GetDirectories(filepath);
                    string[] str2 = Directory.GetFiles(filepath);
                    string[] str = new string[str1.Length + str2.Length];
                    str = (from t1 in str1 select t1).Union(from t2 in str2 select t2).ToArray();
                    return str;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }
        public static void MoveFile(string frompath, string topath)
        {
            MoveFile(frompath, topath, true);
        }
        public static void MoveFile(string frompath, string topath, bool canCover)
        {
            if (FileExits(topath) && canCover)
                File.Delete(topath);
            if (FileExits(topath) && canCover)
                File.Delete(topath);
            File.Move(frompath, topath);
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="frompath"></param>
        /// <param name="topath"></param>
        /// <param name="canCover"></param>
        public static void CopyFile(string frompath, string topath, bool canCover)
        {
            File.Copy(frompath,   topath,   canCover);
        }

        public static void DeleteFile(string  path)
        {
            if (FileExits(path))
            File.Delete(path);
        }


        /// <summary>
        /// 设置路径
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string GetControlPath(string url)
        {
            if (ConfigCore.Instance.ConfigEntity.CreateHtml == 0)
            {
                return "";
            }
            else
            {
                string path = "";

                int siteid = Globals.WebSiteId;
                string filepath =  BuildPath("\\" + url + ".cshtml");
                if ( FileHelper.FileExits(filepath))
                {
                    path = filepath; 
                }

                return path;
            }
        }
        /// <summary>
        /// 并合路径
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static string BuildPath(string Path)
        {
            string _Service =  ConfigCore.Instance.ConfigCommonEntity.HtmlService;
            string _Path = ConfigCore.Instance.ConfigCommonEntity.Site_HtmlPath;
            string HtmlDirPath = _Service +   _Path;
             
            return HtmlDirPath + Path;
        }
        /// <summary> 
        /// 设置路径
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static void CreateHtmlFile(string Dir, string url, string content)
        {
            try
            {
                CreateDir(Dir);
                CreateFile( Dir + "\\" + url + ".cshtml", content);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool CreateDir(String dir)
        {
            try
            { 
                dir = BuildPath(dir);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <param name="content"></param>
        public static void CreateFile(String filepath, String content)
        {
            try
            {  
                CreateDir(""); 
                filepath = BuildPath(filepath); 
                StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.UTF8);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                LogUtil.Log("CreateFile" + filepath, ex.ToString());
            }
        }
        public static string ReadHtmlFile(string filepath)
        { 
            return File.ReadAllText(filepath, Encoding.Default);
        }
    }
}
