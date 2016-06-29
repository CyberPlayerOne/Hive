using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hive
{
    class CToolClass
    {
        //static Array CollectionToArray()
        /// <summary>计算文件夹要用单独一个线程计算
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

        public static string SizeString;

        public static string GetSizeOfFolder(string path)
        {
            long size;
            size = getSizeOfFolder(path);

            if (size / 1000 == 0)//**K
            {
                SizeString = size.ToString() + " B"; 
                return SizeString;
            }
            else if (size / (1000 * 1000) == 0)//**M
            {
                SizeString = (size / 1000).ToString() + "." + (size % 1000).ToString() + " K"; 
                return SizeString; 
            }
            else if (size / (1000 * 1000 * 1000) == 0)//**G
            {
                SizeString = (size / (1000 * 1000)).ToString() + "." + (size % (1000 * 1000)).ToString() + " M";
                return SizeString;
            }
            else//大于1G
            {
                SizeString = (size / (1000 * 1000 * 1000)).ToString() + "." + (size % (1000 * 1000 * 1000)).ToString() + " G";
                return SizeString;
            }
        }

        public static string GetSizeOfFolderThread(string path)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(CToolClass.GetSizeOfFolder));
            thread.Start(path);
            return SizeString;
        }
    }
}
