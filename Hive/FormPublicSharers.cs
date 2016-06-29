using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Reflection;

namespace Hive
{
    public partial class FormPublicSharers : DockContentNew
    {
        /// <summary>
        /// 代替PanelSearch属性visiable
        /// <para>并在set时，相应地设置PanelSearch和listViewOnlineUsers和listViewFind的显示与隐藏以及Location等</para>
        /// <para>一劳永逸?</para>
        /// </summary>
        public bool PanelSearchVisiable
        {
            get { return this.panelSearch.Visible; }
            set
            { 
                this.panelSearch.Visible = value;//设置visiable
                if (value)//为true,则显示this.panelSearch和listViewFind
                {
                    panelSearch.Location = new Point(0, 0);
                    panelSearch.Width = this.Width;

                    listViewFind.Visible = true;
                    listViewFind.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
                    listViewFind.Location = new Point(0, panelSearch.Location.Y + panelSearch.Height);//ListView位置下移
                    listViewFind.Size = new Size(this.Width, this.Height - panelSearch.Location.Y - panelSearch.Height);
                    listViewOnlineUsers.Visible = false;

                    this.textBoxSearch.Focus();

                    textBoxSearch_TextChanged(null, null);//调用此方法以显示ListView中Items
                }
                else
                {
                    listViewFind.Visible = false;

                    listViewOnlineUsers.Visible = true;
                    listViewOnlineUsers.Location = new Point(0, 0);//上移
                    listViewOnlineUsers.Width = this.Width;
                    listViewOnlineUsers.Height = this.Height;
                }
            }
        }


        public FormPublicSharers()
        {
            InitializeComponent();
            IsShowed = true;
        }

        private void FormPublicShare_Load(object sender, EventArgs e)
        {
            //设置控件属性
            PanelSearchVisiable = false;
            listViewOnlineUsers.LargeImageList =
                listViewOnlineUsers.SmallImageList =
                listViewFind.LargeImageList =
                listViewFind.SmallImageList = imageListDisplayPic;
            //给两个ListView添加ColumnHeader
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Text = "昵称";
            ColumnHeader ch2 = new ColumnHeader();
            ch2.Text = "状态"; 
            ch1.Width = this.Width - 13;
            ch2.Width = this.Width;
            listViewOnlineUsers.Columns.AddRange(new ColumnHeader[] { ch1, ch2 });
            listViewFind.Columns.AddRange(new ColumnHeader[] { (ColumnHeader)ch1.Clone(), (ColumnHeader)ch2.Clone() });

            this.listViewOnlineUsers.DoubleClick += new EventHandler(聊天CToolStripMenuItem_Click);
            //添加右键菜单
            listViewFind.ContextMenuStrip = listViewOnlineUsers.ContextMenuStrip = contextMenuStripUsers;
        }

        private void buttonClosePanelSearch_Click(object sender, EventArgs e)
        {
            PanelSearchVisiable = false;
        }

        private delegate ListViewItem GetItemFromListViewOnlineUsersCallback(string IpString);
        /// <summary>
        /// [允许跨线程调用]检查指定IP的用户是否已经在ListViewOnlineUsers里有了，如果有，则返回此ListViewitem的引用，否则返回null。
        /// <para>使用IP作为不同用户的区别，以字符串的形式，保存在ListViewItem的Tag里。</para>
        /// </summary>
        /// <param name="IpString">作为不同用户（item）唯一的区别</param>
        /// <returns></returns>
        public ListViewItem GetItemFromListViewOnlineUsers(string IpString)
        {
            if (this.InvokeRequired)
            {
                return (ListViewItem)this.Invoke(new GetItemFromListViewOnlineUsersCallback(GetItemFromListViewOnlineUsers), IpString);
            }
            else
            {
                foreach (ListViewItem item in listViewOnlineUsers.Items)
                {
                    if (item.Tag.ToString().Equals(IpString))
                    {
                        return item;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// 搜索包含指定text的item是否已经在ListViewOnlineUsers里有了。如有则添加找到的item到ListViewFind的item集合中去,并且返回集合引用
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ListView.ListViewItemCollection GetItemsFromListViewOnlineUsersByText(string text)
        {
            //×------>MSDN：应避免使用ListViewItemCollection构造函数而应该使用ListView.Items属性来访问他！
            ListView.ListViewItemCollection itemCollection = new ListView.ListViewItemCollection(listViewFind);//直接也是只能在这里关联了ListViewFind
            foreach (ListViewItem item in listViewOnlineUsers.Items)
            {
                if (item.Text.ToLower().Contains(text.ToLower()))//不区分大小写(Contains方法默认区分大小写)_
                    itemCollection.Add((ListViewItem)item.Clone());
            }
            return itemCollection;//返回他其实无用
        }

        private delegate void SetUserOnlineStatusCallback(string ipString, string name, string statusInfo, bool isOnline);
        private SetUserOnlineStatusCallback setUserOnlineStatusCallback;
        /// <summary>
        /// 设置用户图标在线下线状态(允许跨线程调用，invoke)：如果
        /// <para>//下线、已知用户--------------》删除用户图标</para>
        /// //上线、未知用户（未添加到ListView中的）情况-----------》添加用户图标
        /// <para> //上线、已知用户----------->更新其信息</para>
        /// ------下线未知用户不做响应
        /// </summary>
        /// <param name="ipString"></param>
        /// <param name="name"></param>
        /// <param name="statusInfo"></param>
        /// <param name="isOnline"></param>
        public void SetUserOnlineStatus(string ipString,string name,string statusInfo, bool isOnline)
        {
            if (this.InvokeRequired)
            {
                setUserOnlineStatusCallback = new SetUserOnlineStatusCallback(SetUserOnlineStatus);
                this.Invoke(setUserOnlineStatusCallback, ipString, name, statusInfo, isOnline);
            }
            else
            {
                ListViewItem item = GetItemFromListViewOnlineUsers(ipString);
                if (item == null && isOnline)//上线、未知用户
                {
                    item = new ListViewItem();
                    item.Text = name;
                    item.ImageIndex = 0;
                    item.Tag = ipString;

                    ListViewItem.ListViewSubItem lvsiStatusInfo = new ListViewItem.ListViewSubItem();
                    lvsiStatusInfo.ForeColor = Color.Gray;
                    lvsiStatusInfo.Text = statusInfo;

                    item.SubItems.Add(lvsiStatusInfo);

                    listViewOnlineUsers.Items.Add(item);
                }
                else if (item != null && !isOnline) //下线、已知用户    //应该全面更新信息，因为已知用户也有可能改名什么的
                {
                    listViewOnlineUsers.Items.Remove(item);
                }
                else if (item != null && isOnline)
                {
                    item.Text = name;//maybe it's a new name
                    item.SubItems[1].Text = statusInfo;
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            //为ListViewFind准备items
            ListViewItem[] items = new ListViewItem[listViewOnlineUsers.Items.Count];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = (ListViewItem)listViewOnlineUsers.Items[i].Clone();
            }
            
            if (textBoxSearch.Text != string.Empty)//有搜索文本
            {
                listViewFind.Items.Clear();             
                //-------------添加item到listviewfind
                //listViewFind.Items.AddRange(GetItemsFromListViewOnlineUsersByText(textBoxSearch.Text));
                GetItemsFromListViewOnlineUsersByText(textBoxSearch.Text);
            }
            else//没有搜索文本
            {
                
                //显示所有
                listViewFind.Items.Clear();
                listViewFind.Items.AddRange(items);
            }
        }

        private void 聊天CToolStripMenuItem_Click(object sender, EventArgs e)
        {
#warning 如果聊天对方中途改名那就会出现问题
            //if (((ListView)(PanelSearchVisiable ? listViewFind : listViewOnlineUsers)).SelectedItems.Count == 0)
            //    return;
            string chatTitle = "与" + ((ListView)(PanelSearchVisiable ? listViewFind : listViewOnlineUsers)).SelectedItems[0].Text + "聊天中";
            WeifenLuo.WinFormsUI.Docking.IDockContent content = MainForm.PMainForm.FindDocument(chatTitle);
            FormChat fc ;
            if (content == null)
            {
                fc = new FormChat();
                fc.TabText = chatTitle;
                fc.DockAreas = DockAreas.Document;
                fc.Show(MainForm.PMainForm.dockPanelMain);
                MainForm.PMainForm.dockPanelMain.Refresh();
            }
            else
            {
                fc = (FormChat)content.DockHandler.Form;
            }
            fc.Activate();
        }

        private void contextMenuStripUsers_Opening(object sender, CancelEventArgs e)
        {
            ListView lv = (ListView)(PanelSearchVisiable ? listViewFind : listViewOnlineUsers);
            if (lv.SelectedItems.Count == 0)
                this.contextMenuStripUsers.Items[0].Visible = false;
            else
                this.contextMenuStripUsers.Items[0].Visible = true;
        }
    }
}