using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hive
{

#warning 帮助类CToolClass计算文件夹/文件大小的方法要修改，将要修改为：需要查看或者获取其大小的时候才获取

    /// <summary>工具类,给出本地路径，计算文件/文件夹的大小
    /// <para>
    /// <remarks>由于目前程序窗体调用本类中的方法时使用一个线程，
    /// 因此（特别是）在调用GetSizeOfFolder方法时常导致主窗体停止响应！
    /// </remarks>
    /// </para>
    /// </summary>
    class CToolClass
    {        
        /// <summary>私有方法成员:计算文件夹要用单独一个线程计算
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        private static long getSizeOfFolder(string path)
        {
            if (!Directory.Exists(path))//先查错
            {
                System.Windows.Forms.MessageBox.Show("文件夹路径" + path + "不存在!");
                return 0;
            }
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();
            FileInfo[] files = dirInfo.GetFiles();
            if (dirs.Length == 0 && files.Length == 0)//空文件夹
            {
                return 0;
            }

            long sizeSum = 0;

            if (files.Length > 0)//包含至少一个文件
            {
                foreach (FileInfo file in files)
                {
                    sizeSum += file.Length;
                }
            }
            if (dirs.Length > 0)//包含至少一个文件夹
            {
                foreach (DirectoryInfo dir in dirs)
                {
                    if (dir.Name.ToLower() == "Recycled".ToLower() || dir.Name.ToLower() == "System Volume Information".ToLower())
                    { continue; }//此类系统文件夹要跳过，计算才正确
                    sizeSum += getSizeOfFolder(dir.FullName);
                }
            }
            return sizeSum;
        }
        /// <summary>以友好的字符串的形式，返回路径参数中的文件夹的大小
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>string</returns>
        public static string GetSizeOfFolder(string path)
        {
            //try
            //{
            //    if (!Directory.Exists(path))
            //        return "路径不存在";

            //    long size;
            //    string SizeString;
            //    size = getSizeOfFolder(path);//也包含路径不存在的异常处理

            //    if (size / 1000 == 0)//**K
            //    {
            //        SizeString = size.ToString() + " B";
            //    }
            //    else if (size / (1000 * 1000) == 0)//**M
            //    {
            //        SizeString = (size / 1000).ToString() + "." + (size % 1000).ToString() + " KB";
            //    }
            //    else if (size / (1000 * 1000 * 1000) == 0)//**G
            //    {
            //        SizeString = (size / (1000 * 1000)).ToString() + "." + (size % (1000 * 1000)).ToString() + " MB";
            //    }
            //    else//大于1G
            //    {
            //        SizeString = (size / (1000 * 1000 * 1000)).ToString() + "." + (size % (1000 * 1000 * 1000)).ToString() + " GB";
            //    }
            //    return SizeString;
            //}
            //catch 
            //{
            //    return "";
            //}
            return "待修改！";
        }
        /// <summary>以友好的字符串的形式，返回路径参数中的文件的大小
        /// <example>这个例子演示了如何使用本方法：
        /// <code>
        /// string FilePath="C:\myfile.exe";
        /// GetSizeOfFile(FilePath);
        /// </code>
        /// </example>
        /// </summary>
        /// <exception cref="System.ArgumentNullException">fileName 为空引用</exception>
        /// <exception cref="ArgumentException">文件名为空，只包含空白，或包含无效字符。</exception>
        /// <exception cref="System.Security.SecurityException">调用方没有所要求的权限。</exception>
        /// <exception cref="NotSupportedException">fileName 字符串中间有一个冒号 (:)。 </exception>
        /// <param name="FilePath">文件路径</param>
        /// <returns>代表文件大小的string</returns>
        public static string GetSizeOfFile(string FilePath)
        {
            //try
            //{
            //    if (!File.Exists(FilePath))
            //        return "文件不存在";

            //    FileInfo fi = new FileInfo(FilePath);
            //    long size = fi.Length;
            //    string SizeString;

            //    if (size / 1000 == 0)//**K
            //    {
            //        SizeString = size.ToString() + " B";
            //    }
            //    else if (size / (1000 * 1000) == 0)//**M
            //    {
            //        SizeString = (size / 1000).ToString() + "." + (size % 1000).ToString() + " KB";
            //    }
            //    else if (size / (1000 * 1000 * 1000) == 0)//**G
            //    {
            //        SizeString = (size / (1000 * 1000)).ToString() + "." + (size % (1000 * 1000)).ToString() + " MB";
            //    }
            //    else//大于1G
            //    {
            //        SizeString = (size / (1000 * 1000 * 1000)).ToString() + "." + (size % (1000 * 1000 * 1000)).ToString() + " GB";
            //    }
            //    return SizeString;
            //}
            //catch (Exception)
            //{
            //    return "";
            //}
            return "待修改！";
        }

        /// <summary>
        /// 记录错误到日志文件中
        /// </summary>
        /// <param name="error">string类型的错误内容</param>
        public static void LogError(string error)
        {
            //File.AppendAllText(MainForm.WorkDirectory + @"\log\" + "error.log", "[" + DateTime.Now.ToString() + "]" + error + "\n");
            StreamWriter sw = File.AppendText(MainForm.WorkDirectory + @"\log\" + "error.log");
            sw.WriteLine("[" + DateTime.Now.ToString() + "]" + error);
            sw.Close();
        }
    }
}
