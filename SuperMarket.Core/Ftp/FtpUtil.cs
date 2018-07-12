using SuperMarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace SuperMarket.Core.Ftp
{
    /// <summary>
    /// Ftp工具类
    /// 作者 严敏春
    /// 日期 2015-12-28
    /// </summary>
    public class FtpUtil
    {
        private string _ip;
        private string _password;
        private string _userName;
        private string _root;  
        /// <summary>
        /// Ftp操作的根目录，默认为ftp://ip/
        /// 例如，对于ftp://root/child/folder1，可以设置为ftp://root/child/，则所有操作都会针对此目录
        /// </summary>
        public string Root
        {
            get
            {
                return _root;
            }
            set
            {
                _root = value;
            }
        }
        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="ip">IP地址，例如 (local)或(local):其他端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public FtpUtil()
        {
            _ip = ConfigCore.Instance.ConfigCommonEntity.FtpServer;
            _userName = ConfigCore.Instance.ConfigCommonEntity.FtpUserName;
            _password = ConfigCore.Instance.ConfigCommonEntity.FtpPassWord;
            _root = "ftp://" + _ip + "/";
        }
        /// <summary>
        /// 构造实例
        /// </summary>
        /// <param name="ip">IP地址，例如 (local)或(local):其他端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public FtpUtil(string ip, string userName, string password)
        {
            _ip = ip;
            _userName = userName;
            _password = password;
            _root = "ftp://" + ip + "/";
        }

        /// <summary>
        /// 修正路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private string FixPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // 替换斜杠
            path = path.Replace("\\", "/").Trim();
            if (path.StartsWith("/"))
            {
                // 去掉最前面的斜杠
                path = path.Substring(1);
            }

            return path;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/</param>
        /// <param name="localPath">本地文件路径，例如 D:\\file.txt</param>
        /// <param name="createDirectory">true：如果ftp目录不存在则创建，false：不检测目录是否存在，不存在抛异常</param>
        public void UploadFile(string ftpPath, string localPath, bool createDirectory = false)
        {
            using (FileStream fileStream = new FileStream(localPath, FileMode.Open))
            {
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                UploadFile(ftpPath, buffer, createDirectory);
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/</param>
        /// <param name="buffer">字节数组</param>
        /// <param name="createDirectory">true：如果ftp目录不存在则创建，false：不检测目录是否存在，不存在抛异常</param>
        public void UploadFile(string ftpPath, byte[] buffer, bool createDirectory = false)
        {
            Stream stream = null;
            try
            {
                // 生成目录，如果此目录不存在
                if (createDirectory)
                    CreateDirectory(Path.GetDirectoryName(ftpPath));

                string requestUriString = Root + FixPath(ftpPath);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.ContentLength = buffer.Length;
                stream = ftpWebRequest.GetRequestStream();
                stream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/file.txt</param>
        /// <returns>字节数组</returns>
        public byte[] DownloadFile(string ftpPath)
        {
            MemoryStream memoryStream = null;
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            byte[] result;
            try
            {
                string requestUriString = Root + FixPath(ftpPath);
                memoryStream = new MemoryStream();
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpWebRequest.UseBinary = true;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
                long contentLength = ftpWebResponse.ContentLength;
                int num = 2048;
                byte[] buffer = new byte[num];
                for (int i = stream.Read(buffer, 0, num); i > 0; i = stream.Read(buffer, 0, num))
                {
                    memoryStream.Write(buffer, 0, i);
                }
                memoryStream.Position = 0;
                result = memoryStream.ToArray();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                if (memoryStream != null)
                    memoryStream.Close();

                if (ftpWebResponse != null)
                    ftpWebResponse.Close();
            }
            return result;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/file.txt</param>
        /// <param name="savePath">本地文件路径，例如 D:\\file.txt</param>
        public void DownloadFile(string ftpPath, string savePath)
        {
            FileStream fileStream = null;
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            try
            {
                string folder = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string requestUriString = Root + FixPath(ftpPath);
                fileStream = new FileStream(savePath, FileMode.Create);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpWebRequest.UseBinary = true;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
                long contentLength = ftpWebResponse.ContentLength;
                int num = 2048;
                byte[] buffer = new byte[num];
                for (int i = stream.Read(buffer, 0, num); i > 0; i = stream.Read(buffer, 0, num))
                {
                    fileStream.Write(buffer, 0, i);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();

                if (stream != null)
                    stream.Close();

                if (ftpWebResponse != null)
                    ftpWebResponse.Close();
            }
        }

        /// <summary>
        /// 创建ftp目录
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/，可递归一级一级创建目录</param>
        public void CreateDirectory(string ftpDirectory)
        {
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            try
            {
                string uri = Root;
                string tmp = string.Empty;

                string[] dirs = FixPath(ftpDirectory)
                    .Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < dirs.Length; i++)
                {
                    List<string> directoryList = GetDirectoryList(tmp);
                    uri = uri + dirs[i] + "/";
                    tmp = tmp + dirs[i] + "/";
                    if (!directoryList.Exists((string x) => string.Equals(x, dirs[i], StringComparison.OrdinalIgnoreCase)))
                    {
                        FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(uri);
                        ftpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                        ftpWebRequest.UseBinary = true;
                        ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                        ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                        stream = ftpWebResponse.GetResponseStream();
                        stream.Close();
                        ftpWebResponse.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// 移除文件
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/file.txt</param>
        public void RemoveFile(string ftpPath)
        {
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            StreamReader streamReader = null;
            try
            {
                string requestUriString = Root + FixPath(ftpPath);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpWebRequest.KeepAlive = false;
                string text = string.Empty;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                long contentLength = ftpWebResponse.ContentLength;
                stream = ftpWebResponse.GetResponseStream();
                streamReader = new StreamReader(stream);
                text = streamReader.ReadToEnd();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (stream != null)
                    stream.Close();

                if (streamReader != null)
                    streamReader.Close();
            }
        }

        /// <summary>
        /// 移除文件夹
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/，可递归一级一级移除目录并且删除文件</param>
        public void RemoveDirectory(string ftpDirectory)
        {
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            try
            {
                List<string> fileList = GetFileList(ftpDirectory);

                for (int i = 0; i < fileList.Count; i++)
                {
                    RemoveFile(ftpDirectory + "/" + fileList[i]);
                }

                List<string> directoryList = GetDirectoryList(ftpDirectory);
                for (int j = 0; j < directoryList.Count; j++)
                {
                    RemoveDirectory(ftpDirectory + "/" + directoryList[j]);
                }

                string requestUriString = Root + FixPath(ftpDirectory);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (stream != null)
                    stream.Close();
            }
        }

        /// <summary>
        /// 获取ListDirectoryDetails协议列出的内容
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/</param>
        /// <returns>ListDirectoryDetails协议列出的内容</returns>
        public List<string> ListDirectoryDetails(string ftpDirectory)
        {
            FtpWebResponse ftpWebResponse = null;
            StreamReader streamReader = null;
            List<string> result;
            try
            {
                string uriString = Root + FixPath(ftpDirectory);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(uriString));
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                streamReader = new StreamReader(ftpWebResponse.GetResponseStream());
                string text = streamReader.ReadToEnd();
                result = new List<string>(text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (streamReader != null)
                    streamReader.Close();
            }
            return result;
        }

        /// <summary>
        /// 获取ListDirectory协议列出的内容
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/</param>
        /// <returns>ListDirectory协议列出的内容</returns>
        public List<string> ListDirectory(string ftpDirectory)
        {
            FtpWebResponse ftpWebResponse = null;
            StreamReader streamReader = null;
            List<string> result;
            try
            {
                string uriString = Root + FixPath(ftpDirectory);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(uriString));
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                streamReader = new StreamReader(ftpWebResponse.GetResponseStream());
                string text = streamReader.ReadToEnd();
                result = new List<string>(text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (streamReader != null)
                    streamReader.Close();
            }
            return result;
        }

        /// <summary>
        /// 获取ftp目录下所有的文件
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/</param>
        /// <returns>ftp目录下所有的文件</returns>
        public List<string> GetFileList(string ftpDirectory)
        {
            try
            {
                List<string> list = new List<string>();
                List<string> all = ListDirectoryDetails(ftpDirectory);
                Regex regex = new Regex("([\\S]*\\s*){3}(?<name>.*)$");
                Regex regex2 = new Regex("([\\S]*\\s*){8}(?<name>.*)$");
                for (int i = 0; i < all.Count; i++)
                {
                    string line = all[i];
                    int index = line.IndexOf("<DIR>", StringComparison.OrdinalIgnoreCase);
                    bool flag = line.StartsWith("d", StringComparison.OrdinalIgnoreCase);
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (index > -1 || flag)
                            continue;
                        Match match = null;
                        if (line.StartsWith("-r", StringComparison.OrdinalIgnoreCase))
                            match = regex2.Match(line);
                        else
                            match = regex.Match(line);
                        if (match != null && match.Groups.Count > 0)
                        {
                            list.Add(match.Groups["name"].Value);
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }
        /// <summary>
        /// 获取ftp目录下所有的文件
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/</param>
        /// <returns>ftp目录下所有的文件</returns>
        public List<string> GetFileListTree(string ftpDirectory,string rootpath="")
        {
            try
            {
                if (rootpath != "")
                {
                    ftpDirectory = FilePath.PathCombine(ftpDirectory, rootpath);
                }
                List<string> list = new List<string>();
                List<string> all = ListDirectoryDetails(ftpDirectory);
                Regex regex = new Regex("([\\S]*\\s*){3}(?<name>.*)$");
                Regex regex2 = new Regex("([\\S]*\\s*){8}(?<name>.*)$");
                for (int i = 0; i < all.Count; i++)
                {
                    string line = all[i];
                    int index = line.IndexOf("<DIR>", StringComparison.OrdinalIgnoreCase);
                    bool flag = line.StartsWith("d", StringComparison.OrdinalIgnoreCase);
                    if (!string.IsNullOrEmpty(line))
                    {
                        if (index > -1 || flag)
                        {
                            Match match = null;
                            if (line.StartsWith("-r", StringComparison.OrdinalIgnoreCase))
                                match = regex2.Match(line);
                            else
                                match = regex.Match(line);
                            if (match != null && match.Groups.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(rootpath))
                                    list.AddRange(GetFileListTree(ftpDirectory, FilePath.PathCombine(rootpath + match.Groups["name"].Value)));
                                else list.AddRange(GetFileListTree(ftpDirectory,  match.Groups["name"].Value));

                            }
                            continue;
                        }
                        else
                        {
                            Match match = null;
                            if (line.StartsWith("-r", StringComparison.OrdinalIgnoreCase))
                                match = regex2.Match(line);
                            else
                                match = regex.Match(line);
                            if (match != null && match.Groups.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(rootpath))
                                    list.Add(FilePath.PathCombine(rootpath, match.Groups["name"].Value));
                                else list.Add(FilePath.PathCombine(match.Groups["name"].Value));

                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        /// <summary>
        /// 获取ftp目录下所有的子目录
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/</param>
        /// <returns>ftp目录下所有的子目录</returns>
        public List<string> GetDirectoryList(string ftpDirectory)
        {
            try
            {
                List<string> list = new List<string>();
                List<string> all = ListDirectoryDetails(ftpDirectory);
                Regex regex = new Regex("([\\S]*\\s*){3}(?<name>.*)$");
                Regex regex2 = new Regex("([\\S]*\\s*){8}(?<name>.*)$");
                for (int i = 0; i < all.Count; i++)
                {
                    string line = all[i];
                    if (!string.IsNullOrEmpty(line))
                    {
                        int index = line.IndexOf("<DIR>", StringComparison.OrdinalIgnoreCase);
                        bool flag = line.StartsWith("d", StringComparison.OrdinalIgnoreCase);
                        if (index > -1 || flag)
                        {
                            Match match = null;
                            if (flag)
                                match = regex2.Match(line);
                            else
                                match = regex.Match(line);
                            if (match != null && match.Groups.Count > 0)
                            {
                                list.Add(match.Groups["name"].Value);
                            }
                        }
                    }
                }
                return list;
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/file.txt</param>
        /// <returns>文件大小</returns>
        public long GetFileSize(string ftpPath)
        {
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            long result = 0;
            try
            {
                string requestUriString = Root + FixPath(ftpPath);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
                result = ftpWebResponse.ContentLength;
                stream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (stream != null)
                    stream.Close();
            }
            return result;
        }

        /// <summary>
        /// 判断Ftp上文件是否存在
        /// </summary>
        /// <param name="ftpPath">ftp路径，例如 root/child/folder1/file.txt</param>
        /// <returns>true：存在，false：不存在</returns>
        public bool FileExist(string ftpPath)
        {
            string dir = string.Empty;
            string[] dirs = ftpPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dirs.Length; i++)
            {
                if (i != dirs.Length - 1)
                {
                    List<string> directoryList = GetDirectoryList(dir);
                    dir = dir + dirs[i] + "/";

                    if (!directoryList.Exists((string x) => string.Equals(x, dirs[i], StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }
                else
                {
                    List<string> fileList = GetFileList(dir);

                    if (fileList.Exists((string x) => string.Equals(x, dirs[i], StringComparison.OrdinalIgnoreCase)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 判断Ftp上目录是否存在
        /// </summary>
        /// <param name="ftpDirectory">ftp目录，例如 root/child/folder1/</param>
        /// <returns>true：存在，false：不存在</returns>
        public bool DirectoryExist(string ftpDirectory)
        {
            string dir = string.Empty;
            string[] dirs = ftpDirectory.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < dirs.Length; i++)
            {
                List<string> directoryList = GetDirectoryList(dir);
                dir = dir + dirs[i] + "/";

                if (directoryList.Exists((string x) => string.Equals(x, dirs[i], StringComparison.OrdinalIgnoreCase)))
                {
                    if (i == dirs.Length - 1)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            return false;
        }

        /// <summary>
        /// 文件更名，若新的位置已存在同名文件，将抛出异常
        /// </summary>
        /// <param name="currentFtpPath">当前ftp路径，例如 root/child/file.txt</param>
        /// <param name="newRelativePath">新的相对ftp路径，例如 ../child2/file.txt，folder1/file.txt</param>
        public void Rename(string currentFtpPath, string newRelativePath)
        {
            FtpWebResponse ftpWebResponse = null;
            Stream stream = null;
            try
            {
                string requestUriString = Root + FixPath(currentFtpPath);
                FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUriString);
                ftpWebRequest.Method = WebRequestMethods.Ftp.Rename;
                ftpWebRequest.RenameTo = FixPath(newRelativePath);
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
                stream = ftpWebResponse.GetResponseStream();
                stream.Close();
                ftpWebResponse.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (ftpWebResponse != null)
                    ftpWebResponse.Close();

                if (stream != null)
                    stream.Close();
            }
        }
    }
}
