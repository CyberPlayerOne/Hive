using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;

namespace Hive
{
    /// <summary>问题汇总：
    /// GetIcon.cs的类有错误：如.resx扩展名会发生异常退出
    /// [已经解决,原因在#warning]进入Recycled（系统）文件夹，会发生异常退出
    /// 有一些文件类型的图标提取不出来
    /// [已经解决]文件夹下如果文件太多，提取图标太慢程序就会死掉------------------可以在全局维持一个ImageList,(key,value)为(扩展名,Image),用的时候再搜索扩展名，没有则添加
    /// </summary>
    public partial class FormDocument : DockContentNew
    {
        /// <summary>
        /// 当前目录在CurrentDirIndex中的位置
        /// </summary>
        private int CurrentDirIndex;
        /// <summary>
        /// 用于保存目录浏览历史的ArrayList
        /// </summary>
        private ArrayList alDir;
        /// <summary>
        /// 保存在浏览过程中曾经遇到过的文件扩展名对应的Icon,第0个元素是目录图标
        /// 此后每遇到一个扩展名类型就搜索Key并返回其index，如果是新扩展名类型则添加之
        /// 未知类型文件的图标在遇到它的时候添加并将其Key设为".?"
        /// </summary>
        private ImageList ImageListIcons;
        private Icon icon;

        /// <summary>
        /// 在构建ListViewItem时标识是文件夹还是文件
        /// </summary>
        enum DirOrFile
        {
            Directory,
            File
        }
        /// <summary>存储DirOrFile类型和对应文件/文件夹的全路径，放在ListViewItem的Tag里
        /// </summary>
        struct DofNPath//默认私有，在本类中使用的类型
        {
            public DirOrFile dof;
            public string Path;
        }

        public FormDocument()
        {
            InitializeComponent();

            simpleButton1.Enabled = simpleButton2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            alDir = new ArrayList();
            alDir.Add("root");
            CurrentDirIndex = 0;

            ImageListIcons = new ImageList();
            ImageListIcons.ImageSize = new Size(32, 32);
            listViewFiles.LargeImageList = ImageListIcons;
            icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType("?", true);//随便取一个文件夹的图标

            //Image img = Bitmap.FromHbitmap((icon.ToBitmap()).GetHbitmap(Color.Blue));
            //Image img = Bitmap.FromHicon(icon.Handle);

            ImageListIcons.Images.Add("directory", icon);//Key为"directory",index为0

            ListRootDirectoryInListView();
        }
        /// <summary>ListView显示我的电脑中的所有磁盘
        /// 
        /// </summary>
        private void ListRootDirectoryInListView()
        {
            DriveInfo[] DriveInfos = DriveInfo.GetDrives();

            listViewFiles.Items.Clear();
            listViewFiles.BeginUpdate();



            listViewFiles.View = View.LargeIcon;

            foreach (DriveInfo di in DriveInfos)
            {
                ListViewItem item = new ListViewItem(di.Name);
                item.ImageIndex = 0;//使用文件夹图标

                DofNPath dnp;
                dnp.dof = DirOrFile.Directory;
                dnp.Path = di.Name;
                item.Tag = dnp;
                listViewFiles.Items.Add(item);
            }
            listViewFiles.EndUpdate();
        }

        /// <summary> 在ListView中列出特定目录,包括所有文件和文件夹
        /// </summary>
        /// <param name="directory">用string表示的目录</param>
        private void ListDirectoryInListView(string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            if ((new DriveInfo(directory)).IsReady == false)
            {
                MessageBox.Show("设备未就绪!");
                return;
            }
            DirectoryInfo[] dirs = di.GetDirectories();
            FileInfo[] files = di.GetFiles();
            listViewFiles.Items.Clear();//清空后重绘
            listViewFiles.BeginUpdate();

            ListViewItem lvi;
            foreach (DirectoryInfo d in dirs)
            {
                lvi = new ListViewItem(d.Name);
                lvi.ImageIndex = 0;//文件夹图标

                DofNPath dnp;
                dnp.dof = DirOrFile.Directory;
                dnp.Path = d.FullName;

                lvi.Tag = dnp;
                listViewFiles.Items.Add(lvi);
            }
            foreach (FileInfo f in files)
            {
                int idx;
                try
                {
#warning [非真正解决办法]例如C:\下一些系统文件没有扩展名，则图标显示为未知文件
                    string ext = Path.GetExtension(f.FullName);
                    if (ext == "") ext = ".?";
                    idx = GetIconIndexFromImageList(ext);

                }
                catch (Exception)
                {
                    idx = GetIconIndexFromImageList(".?");
                }

                lvi = new ListViewItem(f.Name);
                lvi.ImageIndex = idx;

                DofNPath dnp;
                dnp.dof = DirOrFile.File;
                dnp.Path = f.FullName;

                lvi.Tag = dnp;
                listViewFiles.Items.Add(lvi);
            }
            listViewFiles.EndUpdate();

        }
        /// <summary>输入扩展名，从维护的ImageList列表中查找并返回对应扩展名的图标索引;如果不存在于ImageLIst中，则添加之并返回索引
        /// ;
        /// 不提供异常情况处理（如扩展名为空的情况）,要自行先判断
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        private int GetIconIndexFromImageList(string extensionName)
        {
            int a = ImageListIcons.Images.IndexOfKey(extensionName);
            if (a != -1)
                return a;
            else//没有找到
            {
                icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(extensionName, true);
                ImageListIcons.Images.Add(extensionName, icon);
                return ImageListIcons.Images.Count - 1;
            }

        }
        /// <summary>图标激活事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewFiles_ItemActivate(object sender, EventArgs e)
        {
            ListView lv = (ListView)sender;
            string dir = ((DofNPath)lv.SelectedItems[0].Tag).Path;
            DirOrFile dof = ((DofNPath)lv.SelectedItems[0].Tag).dof;

            if (dof == DirOrFile.File)//file
            {
                try
                {
                    System.Diagnostics.Process.Start(dir);//运行文件
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else//directory
            {
                /* 目录的前进与后退用ArrayList保存
                当CurrentDirectory<alDir.Count-1即曾经回溯过,在Active事件发生时,如果新进入的路径不同于alDir[CurrentDirectory+1]
                位置的路径(路径发生转折),则先在ArrayList中删除当前路径以后的路径元素,再保存新路径----即只保持一条历史轨迹(新的);
                否则,即新路径与ArrayList中保存的CurrentDirectory后面的路径相同,则不删除而只进行CurrentDirectory++
                */
                if (CurrentDirIndex < alDir.Count - 1)//曾经回溯过,不是一路Active过来的
                {
                    if (dir != alDir[CurrentDirIndex + 1].ToString())//路径发生转折
                    {
                        alDir.RemoveRange(CurrentDirIndex + 1, alDir.Count - CurrentDirIndex - 1);
                        alDir.Add(dir);
                    }
                    CurrentDirIndex++;
                    ListDirectoryInListView(dir);
                }
                else//新目录
                {
                    alDir.Add(dir);
                    CurrentDirIndex++;
                    ListDirectoryInListView(dir);
                }

                //更改button
                if (CurrentDirIndex == 0)//已经到头,禁用
                {
                    simpleButton1.Enabled = false;
                }
                else//还没到头
                {
                    simpleButton1.Enabled = true;
                }
                if (alDir.Count <= CurrentDirIndex + 1)//已经到尾
                {
                    simpleButton2.Enabled = false;
                }
                else
                {
                    simpleButton2.Enabled = true;
                }

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string dir = alDir[--CurrentDirIndex].ToString(); labelIndex.Text = CurrentDirIndex.ToString();
            labelCurrentDirectory.Text = dir;

            //for (int i=alDir.Count-1;i>=0;i--)//从尾部查找
            //{
            //    if (((sDirElem)alDir[CurrentDirIndex]).LastDir == ((sDirElem)alDir[i]).CurrDir)
            //    {
            //        CurrentDirIndex = i; labelIndex.Text = CurrentDirIndex.ToString(); labelCurrentDirectory.Text = ((sDirElem)alDir[i]).CurrDir;
            //        break;//查找到后立即停止查找(即只用最后一个相符结果)
            //    }
            //}
            //string dir=((sDirElem)alDir[CurrentDirIndex]).CurrDir;

            if (CurrentDirIndex == 0)//已经到头,禁用
            {
                simpleButton1.Enabled = false;
            }
            else//还没到头
            {
                simpleButton1.Enabled = true;
            }
            if (alDir.Count == CurrentDirIndex + 1)//已经到尾
            {
                simpleButton2.Enabled = false;
            }
            else
            {
                simpleButton2.Enabled = true;
            }

            if (dir == "root")
            {
                ListRootDirectoryInListView();
                return;
            }
            else
                ListDirectoryInListView(dir);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ++CurrentDirIndex;
            if (CurrentDirIndex == 0)//已经到头,禁用
            {
                simpleButton1.Enabled = false;
            }
            else//还没到头
            {
                simpleButton1.Enabled = true;
            }
            if (alDir.Count - 1 <= CurrentDirIndex)//当前路径 已经是尾，因此禁用后返回
            {
                simpleButton2.Enabled = false;
                //return;
            }
            else
            {
                simpleButton2.Enabled = true;
            }

            string dir = alDir[CurrentDirIndex].ToString(); labelIndex.Text = CurrentDirIndex.ToString();
            labelCurrentDirectory.Text = dir;

            if (dir == "root")
            {
                ListRootDirectoryInListView();
                return;
            }
            else
                ListDirectoryInListView(dir);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Win32.MsgBox(0, "text:此MsgBox通过调用Windows API产生.", "caption", 0);
            //System.Drawing.Design.IconEditor ie = new System.Drawing.Design.IconEditor();
            //ie.getex
        }
    }

    public class Win32
    {
        /// <summary>
        /// Win32 API函数MsgBox
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "MessageBox")]
        public static extern int MsgBox(int hWnd, String text, String caption, uint type);
    }
}
/*
 * 目录的前进与后退用ArrayList保存
当CurrentDirectory<alDir.Count-1即曾经回溯过,在Active事件发生时,如果新进入的路径不同于alDir[CurrentDirectory+1]位置的路径(路径发生转折),则先在ArrayList中删除当前路径以后的路径元素,再保存新路径----即只保持一条历史轨迹(新的);
否则,即新路径与ArrayList中保存的CurrentDirectory后面的路径相同,则不删除而只进行CurrentDirectory++
*/
/*注意：
文件可以有扩展名也可以没有
文件夹可以有扩展名也可以没有
*/