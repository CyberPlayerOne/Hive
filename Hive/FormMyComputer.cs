using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
//node.Tag中是文件/文件夹的路径
namespace Hive
{
    /// <summary>
    /// 子窗体“我的电脑”，用于添加共享
    /// </summary>
    public partial class FormMyComputer : DockContentNew
    {
        //imageListRoot(设计时)存储"我的电脑"和硬盘图标，应该改为动态获取用户电脑上真正图标
        //一个TreeView控件只能用一个ImageList/...

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormMyComputer()
        {
            InitializeComponent();
            IsShowed = true;
        }

        private void FormMyComputer_Load(object sender, EventArgs e)
        {
            //设置控件属性
            treeViewMyComputer.Width = this.Width;
            treeViewMyComputer.Location = new Point(1, 51);
            treeViewMyComputer.Height = this.Height - treeViewMyComputer.Location.Y;
            //--------------------------------------
            #region 显示“我的电脑”和所有磁盘等到TreeView中

            treeViewMyComputer.ImageList = imageListRoot;
            treeViewMyComputer.Nodes.Add("我的电脑", "我的电脑", 0);
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            int imgIndex;//软盘/硬盘/光盘驱动器图标索引
            string driveName = "";
            TreeNode node;
            bool hasChildren = true;//是否要加子节点，以显示左侧的加号（IsReady?属性）软盘光盘要判断
            foreach (DriveInfo di in driveInfos)
            {
                switch (di.DriveType)
                {
                    case DriveType.CDRom://光盘
                        {
                            imgIndex = 3;
                            if (di.IsReady)
                            {
                                driveName = di.VolumeLabel + " (" + di.Name.Remove(2) + ")";//删除斜杠,斜杠在本系统用于作为文件夹路径的分割
                                hasChildren = true;
                            }
                            else
                            {
                                driveName = "CD 驱动器 (" + di.Name.Remove(2) + ")";
                                hasChildren = false;
                            }
                        }
                        break;
                    case DriveType.Fixed: //硬盘
                        {
                            imgIndex = 2;
                            if (di.VolumeLabel != string.Empty)
                                driveName = di.VolumeLabel + " (" + di.Name.Remove(2) + ")";
                            else
                                driveName = "本地磁盘 (" + di.Name.Remove(2) + ")";
                            hasChildren = true;//?????????????????????????????????不加怎么不行？应该行!？？？？？？？？？？
                        }
                        break;
                    case DriveType.Removable://软盘或U盘
                        {
                            imgIndex = 1;
                            if (di.IsReady)
                            {
                                driveName = di.VolumeLabel + " (" + di.Name.Remove(2) + ")";
                                hasChildren = true;
                            }
                            else
                            {
                                driveName = "3.5软盘 (" + di.Name.Remove(2) + ")";
                                hasChildren = false;
                            }
                        }
                        break;
                    default: imgIndex = 1; hasChildren = false;
                        break;
                }
                node = new TreeNode(driveName, imgIndex, imgIndex);
                node.Tag = di.Name;//一致使用Tag存储驱动器/文件/文件夹的全路径
                treeViewMyComputer.Nodes["我的电脑"].Nodes.Add(node);//select不会改变其图标
                //运行时不显示节点，仅用于使驱动器/文件夹有个可展开的加号(如果不添加子节点就没有加号).
                //当加实际子节点时要remove或clear之
                if(hasChildren)
                    node.Nodes.Add("运行时不显示节点");
                node.ContextMenuStrip = contextMenuStripTreeView;//右键菜单

                this.treeViewMyComputer.Nodes[0].Expand();//展开“我的电脑”节点
            }

            #endregion
            ////-----------添加文件夹的图标到imageList中，作为第0 个图标-----------------
            Icon icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType("文件夹", false);
            imageListRoot.Images.Add(icon);//index==4
            ////---------------------------------------------------
        }

        /// <summary>输入扩展名，从维护的ImageList列表中查找并返回对应扩展名的图标索引;如果不存在于ImageLIst中，则添加之并返回索引
        /// ;
        /// <para>提供异常情况处理（如扩展名为空的情况）,宜自行先判断</para>
        /// </summary>
        /// <param name="extensionName"></param>
        /// <returns></returns>
        private int GetIconIndexFromImageList(string extensionName)
        {
            int a = imageListRoot.Images.IndexOfKey(extensionName);
            if (a != -1)
                return a;
            else//没有找到
            {
                Icon icon;
                try
                {
                    icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(extensionName, true);
                }
                catch 
                {
                    icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(".?", true);
                }
                imageListRoot.Images.Add(extensionName, icon);
                return imageListRoot.Images.Count - 1;//新添加到总是最后一个
            }

        }
        private int GetIconIndexOfExeFromImageList(string FullFileName)
        {
            int a =imageListRoot.Images.IndexOfKey(FullFileName);
            if(a!=-1)//找到了
                return a;
            else//没有添加此ico
            {
                Icon icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileName(FullFileName);
                imageListRoot.Images.Add(FullFileName, icon);
                return imageListRoot.Images.Count - 1;
            }
        }

        /// <summary>给出路径，返回子节点数组；使用可以foreach add
        /// </summary>
        /// <param name="path">物理路径</param>
        /// <returns>节点集合</returns>
        private TreeNodeCollection GetSubTree(string path)
        {
            TreeNodeCollection nodes;//最后返回他
            TreeNode node = new TreeNode();//这个node引用在后面用；这个新TreeNode实例只为用其Nodes属性（TreeNodeCollection）
            nodes = node.Nodes;//-|上面...
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] dirs = di.GetDirectories();
            FileInfo[] files = di.GetFiles();
            //ImageList imageListThis = new ImageList();
            foreach (DirectoryInfo dir in dirs)
            {
                node = new TreeNode(dir.Name);//加图标！！！！！！！！！！！！！！！！！！！
                node.Tag = dir.FullName;                
                nodes.Add(node);
                //找图标并给node
                node.ImageIndex = 4;
                node.SelectedImageIndex = 4;
                node.ContextMenuStrip = contextMenuStripTreeView;//右键菜单
                //加上一个无意义子节点以用于显示加号可以展开,会在BeforeExpand时被clear
                node.Nodes.Add("zz"); 
            }
            foreach (FileInfo file in files)
            {
                node = new TreeNode(file.Name);
                node.Tag = file.FullName;
                nodes.Add(node);
                node.ContextMenuStrip = contextMenuStripTreeView;//右键菜单
                //找图标并给node
                try
                {
                    string extName = Path.GetExtension(node.Tag.ToString());
                    if (extName == string.Empty)
                        extName = ".?";
                    else if (extName.ToLower() == ".exe")
                    {
                        node.ImageIndex = node.SelectedImageIndex = GetIconIndexOfExeFromImageList(node.Tag.ToString());
                        continue;//exe文件的icon找到并赋值，到此结束
                    }
                    node.ImageIndex = node.SelectedImageIndex = GetIconIndexFromImageList(extName);//".?"和非exe的情况
                }
                catch (Exception)
                {
                    node.ImageIndex = node.SelectedImageIndex = GetIconIndexFromImageList(".?");
                }
            }
            return nodes;
        }

        /// <summary>节点展开事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void treeViewMyComputer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level == 0) return;//是"我的电脑"
            if (e.Node.Level == 1)//展开的是驱动器
            {
                if (!(new DriveInfo(e.Node.Tag.ToString())).IsReady)
                { 
                    MessageBox.Show("请将磁盘插入驱动器 " + e.Node.Tag.ToString() + "。");
                    return;
                }//似乎没有了加号后就不会发生了
            }
            //不管是驱动器还是文件夹都要GetSubTree()--------------
            e.Node.Nodes.Clear();
            foreach (TreeNode node in GetSubTree(e.Node.Tag.ToString()))
            {
                e.Node.Nodes.Add(node);
            }
        }

        /// <summary>
        /// 节点双击事件响应方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewMyComputer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (File.Exists(e.Node.Tag.ToString()) && e.Button==MouseButtons.Left)//是文件&&鼠标左键//用e.Node.Nodes.Count == 0判断不行！！！！！例如没有子文件夹的文件夹!!!!!!!!!!!!!
                System.Diagnostics.Process.Start(e.Node.Tag.ToString());
            }
            catch// (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(treeViewMyComputer.SelectedNode.Tag.ToString());//210行行为什么这里不行？又行了。。。怪了
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        /// <summary>
        /// 此事件响应方法使右键也可以选中节点（使节点获取焦点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewMyComputer_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//重要！右键也可以选中节点
            { treeViewMyComputer.SelectedNode = e.Node; }
        }

        /// <summary>
        /// 使用添加共享的默认属性来添加共享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 加入共享SToolStripMenuItem_Click(object sender, EventArgs e)
        {
           //不经过输入，直接修改XML（添加节点）
            string FullName = treeViewMyComputer.SelectedNode.Tag.ToString();
            string ShareName = treeViewMyComputer.SelectedNode.Text;//不包含"\"////直接用SelectedNode.Text就行(与FullName.Substring(FullName.LastIndexOf("\\") + 1)有时候不同例如磁盘根目录)
            if (ShareName == string.Empty) ShareName = FullName;//此情况见于磁盘根目录

            if (MainForm.PMainForm.frmDocument.ExistsShareName(ShareName))//已经有了同名的了
            {
                MessageBox.Show("已有同名共享，请修改共享名后添加或添加其他资源！");
                return;
            }

            FileInfo fi = new FileInfo(FullName);
            //使用默认的方式添加共享
            CXmlHandle.AddNewShareElement(
                ShareName,
                Directory.Exists(FullName),
                FullName,
                false,
                false,
                Directory.Exists(FullName) ? CToolClass.GetSizeOfFolder(FullName) : CToolClass.GetSizeOfFile(FullName),
                "");
            //设置焦点到"共享管理器" document 子窗体
            WeifenLuo.WinFormsUI.Docking.IDockContent content = MainForm.PMainForm.FindDocument("共享管理器");
            FormDocument frm;

            if (content == null)//该文档窗口已经被关闭了
            {
                frm = new FormDocument();
                frm.DockAreas = DockAreas.Document;
                frm.Show(MainForm.PMainForm.dockPanelMain);
            }
            else
            {
                frm = (FormDocument)content.DockHandler.Form;
            }
             
            frm.Activate();
            //刷新其中的listView控件
            frm.PaintListViewWithMyShare();
            //向服务器发送新的共享列表
            MainForm.PMainForm.SendShareListToServer();
        }

        /// <summary>
        /// 编辑共享属性，添加共享
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 共享并编辑属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ShareName = treeViewMyComputer.SelectedNode.Text;
            string LocalPath = treeViewMyComputer.SelectedNode.Tag.ToString();

            if (MainForm.PMainForm.frmDocument.ExistsShareName(ShareName))//已经有了同名的了
            {
                MessageBox.Show("已有同名共享，请修改共享名或更改资源路径后添加！");
                return;
            }

            加入共享SToolStripMenuItem_Click(sender, e);//先默认方式添加上共享
            
            //再打开编辑属性的窗体-------------------------------

            //首先选中
            MainForm.PMainForm.frmDocument.SelectItemByText(ShareName);
   
            FrmProperty frmProperty = new FrmProperty();
            frmProperty.Text = "添加共享: " + ShareName;
            //一切属性显示默认添加共享时的属性设置
            frmProperty._labelShareName = frmProperty._textBoxShareName = ShareName;
            frmProperty._labelType = Directory.Exists(LocalPath) ? "文件夹" : "文件";
            frmProperty._labelPath = frmProperty._textBoxPath = LocalPath;
            frmProperty._checkBoxCanDelete = false;
            frmProperty._checkBoxCanCreate = false;
            frmProperty._labelSize = Directory.Exists(LocalPath) ? CToolClass.GetSizeOfFolder(LocalPath) : CToolClass.GetSizeOfFile(LocalPath);
            frmProperty._textBoxInfo = "";
            frmProperty.Show();
            //Level3FormProperty的修改方式是正确的（修改时在XML里定位再修改）
        }

        private void contextMenuStripTreeView_Opening(object sender, CancelEventArgs e)
        {
        }

        private void 收缩ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in treeViewMyComputer.Nodes[0].Nodes)
            {
                node.Collapse();
            }
        }

        private void 展开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in treeViewMyComputer.Nodes[0].Nodes)
            {
                node.Expand();
            }
        }
    }
}
//*.ico文件图标显示仍然不正常!
//TreeView节点右键点击就选中-OK了
// GetIcon.cs的类有错误：如.resx扩展名会发生异常退出-Ok了