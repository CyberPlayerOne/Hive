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
using System.Xml;

/*注意：
 * item.Tag中是SShareElementAttributes结构实例
 * ImageComboBox不仅作为一个改变当前目录的工具也作为一个显示当前目录的工具
 */

namespace Hive
{
    /// <summary><para>用于表示文件或文件夹属性集合的结构体</para>
    /// 对应MyShare.xml文件结构，作为一个共享的文件或文件夹的属性列表;
    /// 常常存放在ListView元素(ListViewItem)的Tag中
    /// </summary>
    struct SShareElementAttributes
    {
        public string ShareName;
        public bool IsFolder;
        public string LocalPath;
        public bool CanDelete;
        public bool CanCreate;
        public string Size;
        public string Info;
    }


    /// <summary>此窗体类用于显示本地共享管理器
    /// <para>本机上设置的共享目录，进行添加、删除等等操作实际为修改程序目录下的MyShare.xml文件</para>
    /// </summary>
    public partial class FormDocument : DockContentNew
    {
        /// <summary> 当前目录在alDir中的位置索引
        /// </summary>
        public int CurrentDirIndex;
        /// <summary>用于保存目录浏览历史的ArrayList
        /// <para>是为了能够进行前进、返回目录操作</para>
        /// </summary>
        public ArrayList alDir;

        /// <summary>保存在浏览过程中曾经遇到过的文件扩展名对应的Icon,第0个元素是文件夹类型图标
        /// <para>此后每遇到一个扩展名类型就搜索Key并返回其index，如果是新扩展名类型则添加之</para>
        /// 未知类型文件的图标在遇到它的时候添加并将其Key设为".?"
        /// <para>exe文件存储其(路径,icon)作为(key,image)</para>
        /// </summary>
        private ImageList imageListIcons;

        /// <summary> 可由外部访问的，指向本类中的ImageComboBoxShare控件。在Load事件中赋值
        /// </summary>
        public ImageComboBox.ImageComboBox pImageComboBox;
        


        /// <summary>窗体类FormDocument的公共构造函数。
        /// </summary>
        public FormDocument()
        {
            InitializeComponent(); 
            //初始化ImageListIcons,并添加文件夹图标作为第一个图标
            imageListIcons = new ImageList();
            imageListIcons.TransparentColor = System.Drawing.Color.Transparent;//32位下为什么图标有黑色阴影？
            Icon icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType("文件夹", true);
            imageListIcons.Images.Add(icon);//index==0//添加文件夹图标

            listViewShare.LargeImageList = imageListIcons;
            imageListIcons.ImageSize = new Size(32, 32);
            //还可以clone一个imageListIconsSmall 16*16
            ////---------------------------------------------------
            //imageComboBoxShare初始化放置于PaintListViewWithMyShare()
                       
        }

        private void FormDocument_Load(object sender, EventArgs e)
        {
            PaintListViewWithMyShare();

            alDir = new ArrayList();
            alDir.Add("\\");//提前添加首个item（代表共享根目录）
            CurrentDirIndex = 0;

            pImageComboBox = imageComboBoxShare;
            //禁用前进后退按钮
            buttonBack.Enabled = buttonForward.Enabled = false;
        }

        /// <summary><para>(显示根目录)读取MyShare.Xml文件，显示在ListView中；</para>
        /// 此函数可以由外部调用因为在改变我的共享时要再进行调用.
        /// <para>(注意：此方法会重绘ListView和ImageComboBox的项目，选择将不复存在，因此需要提前保存他们被选择的项目)</para>
        /// </summary>
        public void PaintListViewWithMyShare()
        {
            //因为imageComboBoxShare添加了SelectedIndexChanged事件响应方法，而在本方法内要修改其item故会导致事件发生，故需在之前之后取消订阅和添加订阅事件
            imageComboBoxShare.SelectedIndexChanged -= imageComboBoxShare_SelectedIndexChanged;

            listViewShare.Items.Clear();
            imageComboBoxShare.Items.Clear();//本方法顺带把ImageComboBox与ListViewShare保持一致
            //imageComboBoxShare初始化放置于此
            imageComboBoxShare.Indent = 10;
            imageComboBoxShare.Sorted = false;;
            ImageComboBox.ImageComboBoxItem icbi = new ImageComboBox.ImageComboBoxItem();
            icbi.Text = "\\";
            icbi.IndentLevel = 0;
            imageComboBoxShare.Items.Add(icbi);
            imageComboBoxShare.SelectedIndex = 0;
            //imageComboBoxShare.Text = "\\";
            imageComboBoxShare.MaxDropDownItems = 14;

            //读取MyShare.Xml文件，显示在ListView中(我这里应当用CXmlHandle.cs却没有用)
            XmlDocument document = new XmlDocument();
            try { document.Load(MainForm.WorkDirectory + "\\" + "MyShare.xml"); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
#warning 应当在这里对XML进行格式验证！！！
            XmlElement rootElement = document.DocumentElement;//<MyShare> element
            if (!rootElement.HasChildNodes)//没有共享文件夹
                return;

            #region 对每个XmlNode获取其属性值并存放在SShareElementAttributes中，最终添加到ListView和ImageComboBox控件中
            XmlNodeList dirNodes = rootElement.GetElementsByTagName("ShareElement");//为什么不行？其实行，XML文件位置！！！！MSDN建议使用SelectSingleNodes代替之
            //XmlNodeList dirNodes = rootElement.ChildNodes;
            SShareElementAttributes shElemAttributes;
            ListViewItem item;
            ImageComboBox.ImageComboBoxItem comboBoxItem;
            foreach (XmlNode node in dirNodes)
            {
                shElemAttributes.ShareName = node.Attributes["ShareName"].Value;
                shElemAttributes.IsFolder = bool.Parse(node.Attributes["IsFolder"].Value);
                shElemAttributes.LocalPath = node.Attributes["LocalPath"].Value;
                shElemAttributes.CanDelete = bool.Parse(node.Attributes["CanDelete"].Value);
                shElemAttributes.CanCreate = bool.Parse(node.Attributes["CanCreate"].Value);
                shElemAttributes.Size = node.Attributes["Size"].Value;
                shElemAttributes.Info = node.Attributes["Info"].Value;

                item = new ListViewItem();
                item.Tag = shElemAttributes;
                item.Text = shElemAttributes.ShareName;
                if (shElemAttributes.IsFolder)
                    item.ImageIndex = 0;
                else//是文件，要获取其文件类型图标
                {
                    string ext = Path.GetExtension(shElemAttributes.LocalPath);
                    if (ext.ToLower() == ".exe")
                    {
                        int i;
                        try
                        {
                            i = GetIconIndexOfExeFromImageList(shElemAttributes.LocalPath);
                            item.ImageIndex = i;
                        }
                        catch
                        {
                            item.ImageIndex = GetIconIndexFromImageList(".exe");
                        }
                    }
                    else
                    {
                        if (ext == string.Empty) ext = ".?";
                        item.ImageIndex = GetIconIndexFromImageList(ext);
                    }
                }
                     //!!!!!!TooltipText要在ListView的属性ShowItemToolTips为true时才行  
                item.ToolTipText = "共享名:  " + shElemAttributes.ShareName;
                item.ToolTipText += "\n大小:    " + shElemAttributes.Size;
                item.ToolTipText += "\n本地路径:" + shElemAttributes.LocalPath;
                item.ToolTipText += "\n说明:    " + shElemAttributes.Info;

                listViewShare.Items.Add(item);

               
                //comboBoxShareRoot.Items.Add(item);
                imageComboBoxShare.ImageList = this.imageListIcons;
                comboBoxItem = new ImageComboBox.ImageComboBoxItem();
                comboBoxItem.ImageIndex = item.ImageIndex;
                comboBoxItem.Text = item.Text;
                comboBoxItem.IndentLevel = 1;
               // comboBoxItem.Item 与原comboBox一样的属性
                imageComboBoxShare.Items.Add(comboBoxItem);
            }
            #endregion

            imageComboBoxShare.SelectedIndexChanged += imageComboBoxShare_SelectedIndexChanged;
        }
        /// <summary>输入扩展名，从维护的ImageList列表中查找并返回对应扩展名的图标索引;如果不存在于ImageLIst中，则添加之并返回索引；
        ///<para> 提供异常情况处理（如扩展名为空的情况,某些因为帮助类自身原因会发生异常）,推荐自行先判断</para>
        /// 若为exe文件，不应该使用此方法；本方法用于可根据扩展名分类的文件的图标
        /// </summary>
        /// <param name="extensionName">如果为空，要提前自己去处理！</param>
        /// <returns>Icon在ImageList中的索引</returns>
        /// <seealso cref="GetIconIndexOfExeFromImageList(string)"/>
        private int GetIconIndexFromImageList(string extensionName)
        {
            int i = imageListIcons.Images.IndexOfKey(extensionName);
            if (i != -1) return i;
            Icon icon;
            try
            { 
                icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(extensionName, true);
                if (icon == null) throw new Exception();//when extentionName like .resx can not get a proper icon it will throw an exception to get an icon for  .? file
            }
            catch { icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(".?", true); }
            imageListIcons.Images.Add(extensionName, icon);
            return imageListIcons.Images.Count - 1;
        }
        /// <summary>获取exe文件的图标，从ImageList中
        /// </summary>
        /// <param name="FullFileName">文件路径</param>
        /// <returns></returns>
        /// <seealso cref="GetIconIndexFromImageList(string)"/>
        private int GetIconIndexOfExeFromImageList(string FullFileName)
        {
            int a = imageListIcons.Images.IndexOfKey(FullFileName);
            if (a != -1)//找到了
                return a;
            else//尚未添加此ico
            {
                //Icon icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileName(FullFileName);
                Icon icon;
                try 
                { 
                    icon = System.Drawing.Icon.ExtractAssociatedIcon(FullFileName);
                    if (icon == null) throw new Exception();
                }
                catch { icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(".exe", true); }
                imageListIcons.Images.Add(FullFileName, icon);
                return imageListIcons.Images.Count - 1;
            }
        }

        /// <summary>
        /// ListView激活item事件响应方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewShare_ItemActivate(object sender, EventArgs e)
        {
            if(listViewShare.SelectedItems.Count!=0)
                ActivateItem(listViewShare.SelectedItems[0].Text); ;
        }

       /// <summary>
        /// 菜单“编辑共享属性”事件响应方法
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void 编辑共享属性PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SShareElementAttributes ShElemAttributes =
            (SShareElementAttributes)this.listViewShare.SelectedItems[0].Tag;//一次只能编辑一个共享的属性
            FrmProperty frmProperty = new FrmProperty();
            frmProperty.Text = ShElemAttributes.ShareName + " 共享属性";
            //初始化成员变量然后显示属性窗体
            frmProperty._labelShareName = frmProperty._textBoxShareName = ShElemAttributes.ShareName;
            frmProperty._labelType = ShElemAttributes.IsFolder ? "文件夹" : "文件";
            frmProperty._labelPath = frmProperty._textBoxPath = ShElemAttributes.LocalPath;
            frmProperty._checkBoxCanDelete = ShElemAttributes.CanDelete;
            frmProperty._checkBoxCanCreate = ShElemAttributes.CanCreate;
            frmProperty._labelSize = ShElemAttributes.Size;
            frmProperty._textBoxInfo = ShElemAttributes.Info;
            frmProperty.Show();
        }       

        private void contextMenuStripMyShare_Opening(object sender, CancelEventArgs e)
        {
            if (listViewShare.SelectedItems.Count == 0)
            {
                共享属性PToolStripMenuItem.Visible =
                    删除共享DToolStripMenuItem.Visible =
                    打开OToolStripMenuItem.Visible = false;
            }
            else
            {
                共享属性PToolStripMenuItem.Visible =
                    删除共享DToolStripMenuItem.Visible =
                    打开OToolStripMenuItem.Visible = true;
            }
            ListViewItem firstItem = listViewShare.Items[0];
            bool isRootDir = CXmlHandle.GetSingleShareElementAttribute(firstItem.Text).HasValue ? true : false;
            if (!isRootDir)//不是根目录，则应该去掉某些右键菜单
            {
                共享属性PToolStripMenuItem.Text = "共享属性(&R)...";
                删除共享DToolStripMenuItem.Visible = false;
            }
            else
            {
                共享属性PToolStripMenuItem.Text = "编辑共享属性(&E)...";
                删除共享DToolStripMenuItem.Visible = true;
            }
        }

        private void 添加共享AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm.PMainForm.frmMyComputer.Activate();
            //或者打开一个新属性窗体，由用户输入
        }

        /// <summary>从XML共享列表文件中,检查ShareName同名是否已经存在，如果存在则返回true;
        /// 那么这样就不能添加同名的ListViewItem
        /// </summary>
        /// <param name="ShareName"></param>
        /// <returns></returns>
        public bool ExistsShareName(String ShareName)
        {
            List<SShareElementAttributes> list = CXmlHandle.GetShareElementAttributes();
            foreach (SShareElementAttributes item in list)
            {
                if (ShareName.ToLower() == item.ShareName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void 删除共享DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewShare.SelectedItems.Count == 0)
                return;
            else if (listViewShare.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("您确定要删除" + listViewShare.SelectedItems[0].Text + "这个共享吗?\n删除共享不会删除您的硬盘上的文件。", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.No)
                    return;
            }
            else
            {
                if (MessageBox.Show("您确定要删除这" + listViewShare.SelectedItems.Count.ToString() + "个共享吗?\n删除共享不会删除您的硬盘上的文件。", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.No)
                    return;
            }
            //一次性删除多项
            foreach (ListViewItem item in listViewShare.SelectedItems)
            {
                CXmlHandle.DeleteShareElement(item.Text);
            }
            //刷新列表
            MainForm.PMainForm.frmDocument.PaintListViewWithMyShare();
            MainForm.PMainForm.SendShareListToServer();
        }

        /// <summary>用于外部调用，以选中指定Item（获取焦点）；
        /// <para>选中后就可以用这个选中的item做些其他事情</para>
        /// </summary>
        /// <param name="itemText">item.Text</param>
        public void SelectItemByText(string itemText)
        {
            foreach (ListViewItem item in listViewShare.Items)
            {
                if (item.Text.ToLower() == itemText.ToLower())
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActivateItem(listViewShare.SelectedItems[0].Text);
        }

        private void listViewShare_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {

        }

        /// <summary>使用回车键响应用户操作：打开用户输入在地址栏的资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imageComboBoxShare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (imageComboBoxShare.Text == "\\")//根目录
                {
                    PaintListViewWithMyShare();
                }
                else
                {//
                    string sourcePath = imageComboBoxShare.Text;
                    try
                    {                        
                       bool result = PaintListViewInDirectory(sourcePath);//虽然返回了false，但已经重绘了上一级文件夹来
                       if (!result)
                       {
                           //可能是个文件
                        string parentPath;
                        string filePath;
                        filePath = GetLocalPathByVirtualPath(sourcePath, out parentPath);
                        if (filePath == null )//文件也不存在则这个路径就是根本没有的
                        {
                            throw new Exception();//输入了错误的路径
                        }
                        System.Diagnostics.Process.Start(filePath);//filePath不为空时
                       }
                   }
                   catch
                   {
                       //输入了错误的路径    或    filePath无法启动    
                       MessageBox.Show("网络路径\""+sourcePath+"\"不存在！");
                   }
                }
            }            
        }

        /// <summary>在当前ListView(不一定根目录)的Items中，根据text找到item,
        /// 根据其属性itemAttributes，按照要激活的item的类型（文件/文件夹）激活之。
        /// 并在路径栏显示当前路径。。。<para>
        ///1. 在非直接输入ImageComboBox或非ImageComboBox的SelectedIndexChanged事件响应方法中，
        /// 要在本方法执行后,先取消订阅ImageComboBox的SelectedIndexChanged事件响应方法，
        /// 再修改ImageComboBox的Text值，再订阅selectedIndexChanged事件(这样使ListView的激活
        /// 与ImageComboBox的Text一致)</para>
        ///2. 可以为ImageComboBox的SelectedIndexChanged事件直接使用本方法。
        /// </summary>
        /// <param name="itemText">当前ListView(不一定根目录)的Item的Text</param>
        /// <returns>是否成功激活（打开）</returns>
        private bool ActivateItem(string itemText)
        {
            ListViewItem item =
            listViewShare.FindItemWithText(itemText);
                                                //foreach (ListViewItem i in listViewShare.Items)
                                                //{
                                                //    if (i.Text==itemText)
                                                //    {
                                                //        item = i;
                                                //        continue;
                                                //    }
                                                //}
            if (item == null)//没有这一项
                return false;
                                                    //在ImageComboBox中显示此项目
                                                   // imageComboBoxShare.Text = "\\"+itemText;//会引起Select操作！！！！！！！！！！！！！

            SShareElementAttributes itemAttributes = (SShareElementAttributes)item.Tag;
            if (!itemAttributes.IsFolder)//是文件（不管根目录上的共享文件还是孩子文件都同一种处理方式）
            {
                try
                {
                    System.Diagnostics.Process.Start(itemAttributes.LocalPath);
                    //如果是文件夹则显示全虚拟网络路径，如果是文件则显示父文件夹的虚拟网络路径
                    if (CXmlHandle.GetSingleShareElementAttribute(itemText).HasValue)//如果是根目录上的共享文件，则其属性中的Info不为网络路径
                    { }//不在imageComboBox显示任何东西
                    else//是子文件,则其Info含有网络路径,从中提取父文件夹网络路径，在imageComboBox显示
                    {
                        imageComboBoxShare.SelectedIndexChanged -= imageComboBoxShare_SelectedIndexChanged;
                        imageComboBoxShare.Text = itemAttributes.Info.Substring(0, itemAttributes.Info.IndexOf(itemText));                        
                        imageComboBoxShare.SelectedIndexChanged += imageComboBoxShare_SelectedIndexChanged;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }                
            }
            else//文件夹
            {              

                //共享文件夹目录显示为：
                //"\"表示根目录
                //"share\"或"share"表示根目录下的share文件夹
                //类推,"\share\music\" etc.

                //网络用户的资源目录（以及收藏夹中）使用这样的形式：
                //"恶魔陛下\infernal share\"

                if (CXmlHandle.GetSingleShareElementAttribute(itemText).HasValue)//是根目录上的共享文件夹
                {
                    imageComboBoxShare.SelectedIndexChanged -= imageComboBoxShare_SelectedIndexChanged;
                    imageComboBoxShare.Text = itemText;
                    imageComboBoxShare.SelectedIndexChanged += imageComboBoxShare_SelectedIndexChanged;

                    //--------------------------------
                    //-----------------
                    /* 目录的前进与后退用ArrayList保存
                    当CurrentDirectory<alDir.Count-1即曾经回溯过,在Active事件发生时,如果新进入的路径不同于alDir[CurrentDirectory+1]
                    位置的路径(路径发生转折),则先在ArrayList中删除当前路径以后的路径元素,再保存新路径----即只保持一条历史轨迹(新的);
                    否则,即新路径与ArrayList中保存的CurrentDirectory后面的路径相同,则不删除而只进行CurrentDirectory++
                    */
                    if (CurrentDirIndex < alDir.Count - 1)//曾经回溯过,不是一路Active过来的
                    {
                        if (itemAttributes.ShareName != alDir[CurrentDirIndex + 1].ToString())//路径发生转折.!=左应该为网络虚拟路径
                        {
                            alDir.RemoveRange(CurrentDirIndex + 1, alDir.Count - CurrentDirIndex - 1);
                            alDir.Add(itemAttributes.ShareName);
                        }
                        CurrentDirIndex++;
                        PaintListViewInDirectory(itemAttributes.ShareName);
                    }
                    else//新目录
                    {
                        alDir.Add(itemAttributes.ShareName);
                        CurrentDirIndex++;
                        PaintListViewInDirectory(itemAttributes.ShareName);
                    }

                    //更改button
                    if (CurrentDirIndex == 0)//已经到头,禁用
                    {
                        //MainForm.PMainForm.btnBack.Enabled = false;//原来使用的是主窗体上的按钮，
                        buttonBack.Enabled = false;//现在改为ForDocuement窗体内的按钮
                    }
                    else//还没到头
                    {
                        //MainForm.PMainForm.btnBack.Enabled = true;
                        buttonBack.Enabled = true;
                    }
                    if (alDir.Count <= CurrentDirIndex + 1)//已经到尾
                    {
                        //MainForm.PMainForm.btnForward.Enabled = false;
                        buttonForward.Enabled = false;
                    }
                    else
                    {
                        //MainForm.PMainForm.btnForward.Enabled = true;
                        buttonForward.Enabled = true;
                    }
                     //-------------------------------
                    return PaintListViewInDirectory(itemText);//itemText也是根目录文件夹的虚拟网络路径
                }
                else//是子文件夹------>在子文件夹的属性中的Info字段存储了其虚拟网络路径(这样子文件的属性页不可修改)
                {
                    imageComboBoxShare.SelectedIndexChanged -= imageComboBoxShare_SelectedIndexChanged;
                    imageComboBoxShare.Text = itemAttributes.Info;
                    imageComboBoxShare.SelectedIndexChanged += imageComboBoxShare_SelectedIndexChanged;
                    //-----------------
                    /* 目录的前进与后退用ArrayList保存
                    当CurrentDirectory<alDir.Count-1即曾经回溯过,在Active事件发生时,如果新进入的路径不同于alDir[CurrentDirectory+1]
                    位置的路径(路径发生转折),则先在ArrayList中删除当前路径以后的路径元素,再保存新路径----即只保持一条历史轨迹(新的);
                    否则,即新路径与ArrayList中保存的CurrentDirectory后面的路径相同,则不删除而只进行CurrentDirectory++
                    */
                    if (CurrentDirIndex < alDir.Count - 1)//曾经回溯过,不是一路Active过来的
                    {
                        if (itemAttributes.Info != alDir[CurrentDirIndex + 1].ToString())//路径发生转折.!=左应该为网络虚拟路径
                        {
                            alDir.RemoveRange(CurrentDirIndex + 1, alDir.Count - CurrentDirIndex - 1);
                            alDir.Add(itemAttributes.Info);
                        }
                        CurrentDirIndex++;
                        PaintListViewInDirectory(itemAttributes.Info);
                    }
                    else//新目录
                    {
                        alDir.Add(itemAttributes.Info);
                        CurrentDirIndex++;
                        PaintListViewInDirectory(itemAttributes.Info);
                    }

                    //更改button
                    if (CurrentDirIndex == 0)//已经到头,禁用
                    {
                        //MainForm.PMainForm.btnBack.Enabled = false;
                        buttonBack.Enabled = false;
                    }
                    else//还没到头
                    {
                        //MainForm.PMainForm.btnBack.Enabled = true;
                        buttonBack.Enabled = true;
                    }
                    if (alDir.Count <= CurrentDirIndex + 1)//已经到尾
                    {
                        //MainForm.PMainForm.btnForward.Enabled = false;
                        buttonForward.Enabled = false;
                    }
                    else
                    {
                        //MainForm.PMainForm.btnForward.Enabled = true;
                        buttonForward.Enabled = true;
                    }
                    //MessageBox.Show(CurrentDirIndex.ToString()+"&&"+alDir.Count.ToString());//test only
                    //-------------------------------
                    return PaintListViewInDirectory(itemAttributes.Info);
                }
            }
//还不知道在哪里：反正back和forward要同时修改ImageComboBox!!!!-------->就在按钮响应方法里！
        }

        /// <summary>绘制指定的虚拟路径到ListView中，本方法不进行路径检查，因此必须确保虚拟路径是存在的！
        /// <para>根目录下每个共享文件夹的子文件夹要有一个属性来存储虚拟路径(相对共享根目录的路径)，
        /// 以知道其共享属性.</para>
        /// ------------------>本方法没有修改imageComboBox的Text，调用时记得自行修改！！！（应该加上！）-----------------------
        /// </summary>
        /// <param name="virtualPath"> 文件夹 的虚拟路径(相对共享根目录路径)</param>
        /// <returns>操作是否成功</returns>
        public bool PaintListViewInDirectory(string virtualPath)
        {
            if (virtualPath.StartsWith("\\"))
            {
                if (virtualPath != "\\")
                    return false;
                else//就是根目录
                {
                    PaintListViewWithMyShare();
                    return true;
                }
            }

            //如果virtualPath是单独根共享文件夹，则直接获取其共享属性的LocalPath并paint---》同时对每个子文件夹/文件
            //item的Tag要设置属性结构体（LocalPath本地路径，Info设为虚拟全路径）

            if (!virtualPath.Contains("\\") || virtualPath.IndexOf("\\") == virtualPath.Length - 1)//examples:share || share\（第一个斜杠就是最后一个char）
            {
                try
                {
                    string text = virtualPath;//共享名
                    if (virtualPath.Contains("\\"))
                        text = virtualPath.Substring(0, virtualPath.Length - 1);//去掉"\"

                    string localPath = CXmlHandle.GetSingleShareElementAttribute(text).Value.LocalPath;

                    SShareElementAttributes attributesOfParent = (SShareElementAttributes)CXmlHandle.GetSingleShareElementAttribute(text);

                    DirectoryInfo di = new DirectoryInfo(localPath);
                    DirectoryInfo[] dirs = di.GetDirectories();
                    FileInfo[] files = di.GetFiles();
                    listViewShare.Items.Clear();
                    listViewShare.BeginUpdate();

                    ListViewItem item;

                    foreach (DirectoryInfo dir in dirs)
                    {
                        SShareElementAttributes attributes = attributesOfParent;//复制一个结构体
                        //三个布尔值属性（可删除可建立和是否文件夹），与父文件夹一致
                        attributes.Info = attributesOfParent.ShareName + "\\" + dir.Name;//用Info属性存储子文件夹的虚拟路径.文件夹末尾不加斜杠
                        attributes.LocalPath = attributesOfParent.LocalPath + "\\" + dir.Name;
                        attributes.ShareName = dir.Name;
                        attributes.Size = CToolClass.GetSizeOfFolder(attributes.LocalPath);
                        

                        item = new ListViewItem();
                        item.Text = dir.Name;
                        item.ImageIndex = 0;
                        item.Tag = attributes;

                        //!!!!!!TooltipText要在ListView的属性ShowItemToolTips为true时才行  
                        item.ToolTipText = "共享名:  " + attributes.ShareName;
                        item.ToolTipText += "\n大小:    " + attributes.Size;
                        item.ToolTipText += "\n本地路径:" + attributes.LocalPath;
                        item.ToolTipText += "\n网络路径:    " + attributes.Info;

                        listViewShare.Items.Add(item);
                    }
                    foreach (FileInfo file in files)
                    {
                        SShareElementAttributes attributes = attributesOfParent;//复制一个结构体
                        //布尔值属性（可删除），与父文件夹一致
                        attributes.CanCreate = false;
                        attributes.IsFolder = false;
                        attributes.Info = attributesOfParent.ShareName + "\\" + file.Name;//用Info属性存储子文件夹的虚拟路径.文件夹末尾不加斜杠
                        attributes.LocalPath = attributesOfParent.LocalPath + "\\" + file.Name;
                        attributes.ShareName = file.Name;
                        attributes.Size = CToolClass.GetSizeOfFile(attributes.LocalPath);
                        

                        item = new ListViewItem();
                        item.Text = file.Name;
                        //item.ImageIndex
                        string ext = Path.GetExtension(attributes.LocalPath);
                        if (ext.ToLower() == ".exe")
                        {
                            int i;
                            try
                            {
                                i = GetIconIndexOfExeFromImageList(attributes.LocalPath);
                                item.ImageIndex = i;
                            }
                            catch
                            {
                                item.ImageIndex = GetIconIndexFromImageList(".exe");
                            }
                        }
                        else
                        {
                            if (ext == string.Empty) ext = ".?";
                            item.ImageIndex = GetIconIndexFromImageList(ext);
                        }

                        //!!!!!!TooltipText要在ListView的属性ShowItemToolTips为true时才行  
                        item.ToolTipText = "共享名:  " + attributes.ShareName;
                        item.ToolTipText += "\n大小:    " + attributes.Size;
                        item.ToolTipText += "\n本地路径:" + attributes.LocalPath;
                        item.ToolTipText += "\n网络路径:    " + attributes.Info;

                        item.Tag = attributes;

                        listViewShare.Items.Add(item);
                    }
                    listViewShare.EndUpdate();
                    return true;
                }
                catch
                {
                    PaintListViewWithMyShare();//根目录下的非法文件夹！则返回根目录，返回false
                    //PaintListViewWithMyShare()方法已经包办了ImageComboBox 的修改
                    return false;
                }
            }//一级if over
            else
            {
                //如果virtualPath是二级及以上子文件夹，无法从XML直接获取其共享属性的LocalPath，因此，
                  //当前问题：根据virtualPath获取LocalPath
                //share是共享根目录的      share\sharedMusic\nightwish可以先分割为两段，根目录+后段；
                //      获取根目录的LocalPath，并于后段组合，获得其LocalPath
                //但共享根目录不一定为物理根目录，如：
                // software\keys\是共享根目录的     software\keys\new\new2,上方法不可行
                //或者software\keys\new\new2的共享根目录有可能是software\keys\也有可能是software\keys\new\
                //因此需要查找其共享根目录。
                //
                
                    try
                    {
                        string parentLocalPath;//不是virtualPath的上一级目录，而是他对应的共享根目录
                        string localPath = GetLocalPathByVirtualPath(virtualPath,out parentLocalPath);
                        if (localPath == null) //不存在此路径的文件或文件夹
                            throw new Exception();

                        listViewShare.Items.Clear();
                        listViewShare.BeginUpdate();
                        //children......................
                        DirectoryInfo di = new DirectoryInfo(localPath);
                        DirectoryInfo[] dirs = di.GetDirectories();
                        FileInfo[] files = di.GetFiles();

                        ListViewItem item;
                        foreach (DirectoryInfo dir in dirs)
                        {
                            item = new ListViewItem();
                            item.Text = dir.Name;
                            item.ImageIndex = 0;
                            SShareElementAttributes attri;
                            string parentShareName = GetShareNameByLocalPath(parentLocalPath);
                            attri = (SShareElementAttributes)CXmlHandle.GetSingleShareElementAttribute(parentShareName);//深拷贝,继承根共享目录之属性
                            attri.Info = virtualPath.EndsWith("\\") ? virtualPath + dir.Name : virtualPath + "\\" + dir.Name;//网络相对路径
                            attri.LocalPath = localPath.EndsWith("\\") ? localPath + dir.Name : localPath + "\\" + dir.Name;
                            attri.ShareName = dir.Name;
                            attri.Size = CToolClass.GetSizeOfFolder(attri.LocalPath);
                            item.Tag = attri;
                            listViewShare.Items.Add(item);
                        }
                        foreach (FileInfo file in files)
                        {
                            item = new ListViewItem();
                            item.Text = file.Name;
                            
                            SShareElementAttributes attri;
                            string parentShareName = GetShareNameByLocalPath(parentLocalPath);
                            attri = (SShareElementAttributes)CXmlHandle.GetSingleShareElementAttribute(parentShareName);//深拷贝,继承根共享目录之属性
                            attri.Info = virtualPath.EndsWith("\\") ? virtualPath + file.Name : virtualPath + "\\" + file.Name;//网络相对路径
                            attri.LocalPath = localPath.EndsWith("\\") ? localPath + file.Name : localPath + "\\" + file.Name;
                            attri.ShareName = file.Name;
                            attri.Size = CToolClass.GetSizeOfFile(attri.LocalPath);
                            attri.CanCreate = false;
                            attri.IsFolder = false;
                            item.Tag = attri;

                            string ext = Path.GetExtension(attri.LocalPath);
                            if (ext.ToLower() == ".exe")
                            {
                                int i;
                                try
                                {
                                    i = GetIconIndexOfExeFromImageList(attri.LocalPath);
                                    item.ImageIndex = i;
                                }
                                catch
                                {
                                    item.ImageIndex = GetIconIndexFromImageList(".exe");
                                }
                            }
                            else
                            {
                                if (ext == string.Empty) ext = ".?";
                                item.ImageIndex = GetIconIndexFromImageList(ext);
                            }

                            item.ToolTipText = "共享名:  " + attri.ShareName;
                            item.ToolTipText += "\n大小:    " + attri.Size;
                            item.ToolTipText += "\n本地路径:" + attri.LocalPath;
                            item.ToolTipText += "\n共享路径:    " + attri.Info;

                            listViewShare.Items.Add(item);
                        }
                        listViewShare.EndUpdate();

                        return true;
                    }
                    catch 
                    {
                        // //回到上一目录
                        string parentVirtualPath;
                        if (virtualPath.EndsWith("\\"))
                            virtualPath = virtualPath.Remove(virtualPath.Length - 1);
                        parentVirtualPath = virtualPath.Substring(0, virtualPath.LastIndexOf("\\"));
                        //MessageBox.Show(parentVirtualPath);
                        PaintListViewInDirectory(parentVirtualPath);
                        imageComboBoxShare.SelectedIndexChanged -= imageComboBoxShare_SelectedIndexChanged;
                        imageComboBoxShare.Text = parentVirtualPath;
                        imageComboBoxShare.SelectedIndexChanged += imageComboBoxShare_SelectedIndexChanged;
                        return false;//可能是文件或非法路径，由其他方法处理！
                    }
            }//一级else
        }

        /// <summary>根据虚拟路径返回对应本地物理路径，带出虚拟路径的第一个父共享目录的本地路径。
        /// <para>如果虚拟路径不存在，或者获取的物理路径(文件/文件夹)不存在，则返回值和ParentLocalPath皆为null</para>
        /// </summary>
        /// <param name="virtualPath">虚拟路径（共享相对路径）</param>
        /// <param name="ParentLocalPath">(输出参数)其找到的第一个父共享根目录（在XML中存在）</param>
        /// <returns>虚拟路径对应的本地物理路径</returns>
        public string GetLocalPathByVirtualPath(string virtualPath,out string ParentLocalPath)
        {
            //原理：
            //根据virtualPath与XML中所有的共享名进行比对，相似性最大的那一个是其共享根目录。
            //但为了简便，目标是找到其LocalPath，只要在virtualPath中找到一个共享名即可
            foreach (SShareElementAttributes attributes in CXmlHandle.GetShareElementAttributes())
            {
                if(virtualPath.Contains(attributes.ShareName))
                {
                    ParentLocalPath = attributes.LocalPath;
                    if (!ParentLocalPath.EndsWith("\\"))
                        ParentLocalPath += "\\";//加上
                    string part2 = virtualPath.Remove(0, attributes.ShareName.Length);
                    if (part2.StartsWith("\\"))
                        part2 = part2.Remove(0,1);
                    if (Directory.Exists(ParentLocalPath + part2) || File.Exists(ParentLocalPath + part2))
                        return (ParentLocalPath + part2);
                }
            }
            ParentLocalPath = null;
            return null;
        }
        /// <summary>
        /// 根据本地路径获取共享名（必须为共享根路径）
        /// </summary>
        /// <param name="localPath">必须为共享根路径</param>
        /// <returns></returns>
        /// <seealso cref="GetLocalPathByVirtualPath(string,out string)"/>
        private string GetShareNameByLocalPath(string localPath)
        {
            foreach (SShareElementAttributes attrib in CXmlHandle.GetShareElementAttributes())
            {
                //如果末尾有'\'，就不能比较，必须先去掉才行
                string lp1 = attrib.LocalPath.ToLower();
                string lp2 = localPath.ToLower();
                if (lp1.EndsWith("\\"))
                    lp1 = lp1.Substring(0, lp1.Length - 1);
                if (lp2.EndsWith("\\"))
                    lp2 = lp2.Substring(0, lp2.Length - 1);
                if (lp1 == lp2)
                    return attrib.ShareName;
            }
            return null;
        }

        /// <summary>使用ImageComboBox进行目录浏览(仅根目录下的目录浏览）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void imageComboBoxShare_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ActivateItem(imageComboBoxShare.Text);
            //不能直接调用ActivateItem因为在这里的事件中它需要保证当前目录为根目录，那么可以这样：
            string imageComboBoxText = imageComboBoxShare.Text;//先保存
            PaintListViewWithMyShare();//再重绘  //在重绘中已经使用代码不会导致本事件响应而自循环调用
            ActivateItem(imageComboBoxText);//后激活
        }
        /// <summary>
        /// 使用“转到”按钮，类似于用路径打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGo_Click(object sender, EventArgs e)
        {
            //代码段同ImageComboBox的回车键响应方法
            if (imageComboBoxShare.Text == "\\")//根目录
            {
                PaintListViewWithMyShare();
            }
            else
            {//
                string sourcePath = imageComboBoxShare.Text;
                try
                {
                    bool result = PaintListViewInDirectory(sourcePath);//虽然返回了false，但已经重绘了上一级文件夹来
                    if (!result)
                    {
                        //可能是个文件
                        string parentPath;
                        string filePath;
                        filePath = GetLocalPathByVirtualPath(sourcePath, out parentPath);
                        if (filePath == null)//文件也不存在则这个路径就是根本没有的
                        {
                            throw new Exception();//输入了错误的路径
                        }
                        System.Diagnostics.Process.Start(filePath);//filePath不为空时
                    }
                }
                catch
                {
                    //输入了错误的路径    或    filePath无法启动    
                    MessageBox.Show("网络路径\"" + sourcePath + "\"不存在！");
                }
            }
        }

        private void backgroundImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "jpg image|*.jpg|bmp files|*.bmp";
            ofd.FileName = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try { this.listViewShare.BackgroundImage = Image.FromFile(ofd.FileName); }
                catch { }
            }
            ofd.Dispose();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            listViewShare.Focus();
            //路径后退
            string dir = this.alDir[--this.CurrentDirIndex].ToString();
            //判断并设置是否禁用前进后退按钮
            if (this.CurrentDirIndex == 0)//已经到头,禁用
            {
                buttonBack.Enabled = false;
            }
            else//还没到头
            {
                buttonBack.Enabled = true;
            }
            if (this.alDir.Count == this.CurrentDirIndex + 1)//已经到尾
            {
                buttonForward.Enabled = false;
            }
            else
            {
                buttonForward.Enabled = true;
            }
            //--------------进行前进或后退操作---------------------------
            if (dir == "\\")
            {
                this.PaintListViewWithMyShare();
                return;
            }
            else
                this.PaintListViewInDirectory(dir);
            this.pImageComboBox.SelectedIndexChanged -= this.imageComboBoxShare_SelectedIndexChanged;
            this.pImageComboBox.Text = dir;
            this.pImageComboBox.SelectedIndexChanged += this.imageComboBoxShare_SelectedIndexChanged;
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            listViewShare.Focus();
            ++this.CurrentDirIndex;
            if (this.CurrentDirIndex == 0)//已经到头,禁用
            {
                buttonBack.Enabled = false;
            }
            else//还没到头
            {
                buttonBack.Enabled = true;
            }
            if (this.alDir.Count - 1 <= this.CurrentDirIndex)//当前路径 已经是尾，因此禁用后返回
            {
               buttonForward.Enabled = false;
                //return;
            }
            else
            {
               buttonForward.Enabled = true;
            }
            //-----------------
            string dir = this.alDir[this.CurrentDirIndex].ToString();

            if (dir == "\\")
            {
                this.PaintListViewWithMyShare();
                return;
            }
            else
                this.PaintListViewInDirectory(dir);

            this.pImageComboBox.SelectedIndexChanged -= this.imageComboBoxShare_SelectedIndexChanged;
            this.pImageComboBox.Text = dir;
            this.pImageComboBox.SelectedIndexChanged += this.imageComboBoxShare_SelectedIndexChanged;
        }

        private void buttonRoot_Click(object sender, EventArgs e)
        {
            listViewShare.Focus();
            PaintListViewWithMyShare();
        }
       
    }
    

///// <summary> Win32 API Test
///// </summary>
//    public class Win32{
//        /// <summary>
//        /// Win32 API函数MsgBox
//        /// </summary>
//        /// <param name="hWnd"></param>
//        /// <param name="text"></param>
//        /// <param name="caption"></param>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        [DllImport("user32.dll", EntryPoint = "MessageBox")]
//        public static extern int MsgBox(int hWnd, String text, String caption, uint type);
//    }





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

