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
    /// <summary>������ܣ�
    /// GetIcon.cs�����д�����.resx��չ���ᷢ���쳣�˳�
    /// [�Ѿ����,ԭ����#warning]����Recycled��ϵͳ���ļ��У��ᷢ���쳣�˳�
    /// ��һЩ�ļ����͵�ͼ����ȡ������
    /// [�Ѿ����]�ļ���������ļ�̫�࣬��ȡͼ��̫������ͻ�����------------------������ȫ��ά��һ��ImageList,(key,value)Ϊ(��չ��,Image),�õ�ʱ����������չ����û�������
    /// </summary>
    public partial class FormDocument : DockContentNew
    {
        /// <summary>
        /// ��ǰĿ¼��CurrentDirIndex�е�λ��
        /// </summary>
        private int CurrentDirIndex;
        /// <summary>
        /// ���ڱ���Ŀ¼�����ʷ��ArrayList
        /// </summary>
        private ArrayList alDir;
        /// <summary>
        /// ����������������������������ļ���չ����Ӧ��Icon,��0��Ԫ����Ŀ¼ͼ��
        /// �˺�ÿ����һ����չ�����;�����Key��������index�����������չ�����������֮
        /// δ֪�����ļ���ͼ������������ʱ����Ӳ�����Key��Ϊ".?"
        /// </summary>
        private ImageList ImageListIcons;
        private Icon icon;

        /// <summary>
        /// �ڹ���ListViewItemʱ��ʶ���ļ��л����ļ�
        /// </summary>
        enum DirOrFile
        {
            Directory,
            File
        }
        /// <summary>�洢DirOrFile���ͺͶ�Ӧ�ļ�/�ļ��е�ȫ·��������ListViewItem��Tag��
        /// </summary>
        struct DofNPath//Ĭ��˽�У��ڱ�����ʹ�õ�����
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
            icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType("?", true);//���ȡһ���ļ��е�ͼ��

            //Image img = Bitmap.FromHbitmap((icon.ToBitmap()).GetHbitmap(Color.Blue));
            //Image img = Bitmap.FromHicon(icon.Handle);

            ImageListIcons.Images.Add("directory", icon);//KeyΪ"directory",indexΪ0

            ListRootDirectoryInListView();
        }
        /// <summary>ListView��ʾ�ҵĵ����е����д���
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
                item.ImageIndex = 0;//ʹ���ļ���ͼ��

                DofNPath dnp;
                dnp.dof = DirOrFile.Directory;
                dnp.Path = di.Name;
                item.Tag = dnp;
                listViewFiles.Items.Add(item);
            }
            listViewFiles.EndUpdate();
        }

        /// <summary> ��ListView���г��ض�Ŀ¼,���������ļ����ļ���
        /// </summary>
        /// <param name="directory">��string��ʾ��Ŀ¼</param>
        private void ListDirectoryInListView(string directory)
        {
            DirectoryInfo di = new DirectoryInfo(directory);
            if ((new DriveInfo(directory)).IsReady == false)
            {
                MessageBox.Show("�豸δ����!");
                return;
            }
            DirectoryInfo[] dirs = di.GetDirectories();
            FileInfo[] files = di.GetFiles();
            listViewFiles.Items.Clear();//��պ��ػ�
            listViewFiles.BeginUpdate();

            ListViewItem lvi;
            foreach (DirectoryInfo d in dirs)
            {
                lvi = new ListViewItem(d.Name);
                lvi.ImageIndex = 0;//�ļ���ͼ��

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
#warning [����������취]����C:\��һЩϵͳ�ļ�û����չ������ͼ����ʾΪδ֪�ļ�
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
        /// <summary>������չ������ά����ImageList�б��в��Ҳ����ض�Ӧ��չ����ͼ������;�����������ImageLIst�У������֮����������
        /// ;
        /// ���ṩ�쳣�����������չ��Ϊ�յ������,Ҫ�������ж�
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        private int GetIconIndexFromImageList(string extensionName)
        {
            int a = ImageListIcons.Images.IndexOfKey(extensionName);
            if (a != -1)
                return a;
            else//û���ҵ�
            {
                icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(extensionName, true);
                ImageListIcons.Images.Add(extensionName, icon);
                return ImageListIcons.Images.Count - 1;
            }

        }
        /// <summary>ͼ�꼤���¼���Ӧ����
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
                    System.Diagnostics.Process.Start(dir);//�����ļ�
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else//directory
            {
                /* Ŀ¼��ǰ���������ArrayList����
                ��CurrentDirectory<alDir.Count-1���������ݹ�,��Active�¼�����ʱ,����½����·����ͬ��alDir[CurrentDirectory+1]
                λ�õ�·��(·������ת��),������ArrayList��ɾ����ǰ·���Ժ��·��Ԫ��,�ٱ�����·��----��ֻ����һ����ʷ�켣(�µ�);
                ����,����·����ArrayList�б����CurrentDirectory�����·����ͬ,��ɾ����ֻ����CurrentDirectory++
                */
                if (CurrentDirIndex < alDir.Count - 1)//�������ݹ�,����һ·Active������
                {
                    if (dir != alDir[CurrentDirIndex + 1].ToString())//·������ת��
                    {
                        alDir.RemoveRange(CurrentDirIndex + 1, alDir.Count - CurrentDirIndex - 1);
                        alDir.Add(dir);
                    }
                    CurrentDirIndex++;
                    ListDirectoryInListView(dir);
                }
                else//��Ŀ¼
                {
                    alDir.Add(dir);
                    CurrentDirIndex++;
                    ListDirectoryInListView(dir);
                }

                //����button
                if (CurrentDirIndex == 0)//�Ѿ���ͷ,����
                {
                    simpleButton1.Enabled = false;
                }
                else//��û��ͷ
                {
                    simpleButton1.Enabled = true;
                }
                if (alDir.Count <= CurrentDirIndex + 1)//�Ѿ���β
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

            //for (int i=alDir.Count-1;i>=0;i--)//��β������
            //{
            //    if (((sDirElem)alDir[CurrentDirIndex]).LastDir == ((sDirElem)alDir[i]).CurrDir)
            //    {
            //        CurrentDirIndex = i; labelIndex.Text = CurrentDirIndex.ToString(); labelCurrentDirectory.Text = ((sDirElem)alDir[i]).CurrDir;
            //        break;//���ҵ�������ֹͣ����(��ֻ�����һ��������)
            //    }
            //}
            //string dir=((sDirElem)alDir[CurrentDirIndex]).CurrDir;

            if (CurrentDirIndex == 0)//�Ѿ���ͷ,����
            {
                simpleButton1.Enabled = false;
            }
            else//��û��ͷ
            {
                simpleButton1.Enabled = true;
            }
            if (alDir.Count == CurrentDirIndex + 1)//�Ѿ���β
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
            if (CurrentDirIndex == 0)//�Ѿ���ͷ,����
            {
                simpleButton1.Enabled = false;
            }
            else//��û��ͷ
            {
                simpleButton1.Enabled = true;
            }
            if (alDir.Count - 1 <= CurrentDirIndex)//��ǰ·�� �Ѿ���β����˽��ú󷵻�
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
            Win32.MsgBox(0, "text:��MsgBoxͨ������Windows API����.", "caption", 0);
            //System.Drawing.Design.IconEditor ie = new System.Drawing.Design.IconEditor();
            //ie.getex
        }
    }

    public class Win32
    {
        /// <summary>
        /// Win32 API����MsgBox
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
 * Ŀ¼��ǰ���������ArrayList����
��CurrentDirectory<alDir.Count-1���������ݹ�,��Active�¼�����ʱ,����½����·����ͬ��alDir[CurrentDirectory+1]λ�õ�·��(·������ת��),������ArrayList��ɾ����ǰ·���Ժ��·��Ԫ��,�ٱ�����·��----��ֻ����һ����ʷ�켣(�µ�);
����,����·����ArrayList�б����CurrentDirectory�����·����ͬ,��ɾ����ֻ����CurrentDirectory++
*/
/*ע�⣺
�ļ���������չ��Ҳ����û��
�ļ��п�������չ��Ҳ����û��
*/