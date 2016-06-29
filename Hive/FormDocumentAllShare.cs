using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Io = System.IO;
using System.Xml;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Hive
{
    public partial class FormDocumentAllShare : DockContentNew
    {
        private ImageList imageList = new ImageList();

        /// <summary>
        /// 表示当前请求的目录
        /// <para>在调用PaintDirectoryInListView(string directoryListXml)时要进行验证。如果directoryListXml
        /// 中的文件（夹）列表的父文件夹为RequestingDirectory，则此函数继续执行；</para>
        /// 否则,说明用户已经将当前目录更改，此函数return。
        /// <para>在ActivateItem事件响应方法（以及需要通过网络请求目录时）中，设置RequestingDirectory的值；在更改目录时，设置此字段的新值。</para>
        /// 主要目的是为了保持用户所请求目录与通过网络返回并为其显示的目录一致。
        /// </summary>
        public string RequestingDirectory = null;


        //用于前进后退按钮的字段----------------
        /// <summary>保存浏览历史的列表
        /// </summary>
        private System.Collections.ArrayList browseHistoryArrayList = new System.Collections.ArrayList();
        private int browseCurrentIndex;
        //控件引用变量-------------------------
        /// <summary>指向控件listViewShare
        /// </summary>
        public ListView listViewAllShare;
        //\/\/\/\/\/\-------------------------TCP服务器端----------/\/\/\/\/\/\/*/-----
        /// <summary>
        /// 表示本客户端已经退出
        /// </summary>
        private bool isExit = false;
        private TcpClient client;
        private NetworkStream networkStream;
        /// <summary>
        /// 线程同步事件,初始状态为非终止，使用手动重置
        /// </summary>
        private System.Threading.EventWaitHandle allDone = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset);
        //-------------
        //显示文件传输进度窗体 的列表
        /// <summary>
        ///显示文件传输进度窗体 的列表。。&lt;string,FrmDownloading&gt; 分别为传输文件的 "IP"+"ShareElement.LocalPath" 与 对应窗体
        /// </summary>
        Dictionary<string, FrmDownloading> DictFrmDownloading = new Dictionary<string, FrmDownloading>();

        //---------------------------------------------------------------------------------------

        /// <summary>
        /// 构造函数
        /// </summary>
        public FormDocumentAllShare()
        {
            InitializeComponent();

            IsShowed = true;//我的，表示已经显示了，没有隐藏或关闭
            

            //设置浏览初始情况
            browseHistoryArrayList.Add("\\");
            browseCurrentIndex = 0;//必须保证只有第一个即此index为0的item（在browseHistoryArrayList里面）是根目录（为字符串值“\”），其他的是ShareElement结构，与其不一致
            buttonBack.Enabled = false;

            listViewAllShare = this.listViewShare;
        }
        /// <summary>
        /// Activate事件响应函数：在第一次点击本窗体标签时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormDocumentAllShare_Activated(object sender, EventArgs e)
        {
            //MessageBox.Show("Activated!");//测试得知，在构造函数执行时Activate一次，点击此页标签时Activate一次！，Load函数执行时不会Activate。（因此我在Load函数内关联时间响应函数，就只在点击此页标签时Activate）
            PaintAllShareInListView();//在第一次Activate时执行
            this.Activated -= FormDocumentAllShare_Activated;//第一次执行之后就不再关联此事件响应函数！！
        }

        private void FormDocumentAllShare_Load(object sender, EventArgs e)
        {
            imageList.ImageSize = new Size(32, 32);
            //先存储文件夹的图标
            Icon icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType("文件夹", true);
            imageList.Images.Add(icon);
            listViewShare.LargeImageList = listViewShare.SmallImageList = imageList;

            //绘制ListView
            PaintAllShareInListView();
            //禁用前进后退按钮
            buttonBack.Enabled = buttonForward.Enabled = false;
            //Activate事件响应函数：在第一次点击本窗体标签时执行。
            this.Activated += new EventHandler(FormDocumentAllShare_Activated);
        }

        /// <summary>
        /// 向ListView中填充所有人的所有根目录共享
        /// </summary>
        public void PaintAllShareInListView()
        {
            //try
            //{
                //imageComboBoxShare-------------------------------------
                imageComboBoxShare.Items.Clear();//本方法顺带把ImageComboBox与ListViewShare保持一致
                //imageComboBoxShare初始化放置于此
                imageComboBoxShare.Indent = 10;
                imageComboBoxShare.Sorted = false; ;
                ImageComboBox.ImageComboBoxItem icbi = new ImageComboBox.ImageComboBoxItem();
                icbi.Text = "\\";
                icbi.IndentLevel = 0;
                imageComboBoxShare.Items.Add(icbi);
                imageComboBoxShare.SelectedIndex = 0;
                //imageComboBoxShare.Text = "\\";
                imageComboBoxShare.MaxDropDownItems = 14;
                //listViewShare------------------------------------------
                listViewShare.Items.Clear();
                listViewShare.BeginUpdate();
                //在本Form的Load事件中输入所有ListViewItem
                ListViewItem item, itemTmp;
                foreach (UserShareList usl in MainForm.PMainForm.UserLists.Values)//每一个人
                {
                    if (usl.ShareElements == null) continue;//还没有接收到共享列表,则跳过
                    foreach (ShareElement elem in usl.ShareElements)//每一个共享资源
                    {
                        item = new ListViewItem();
                        string userName = "未知用户";
                        itemTmp = MainForm.PMainForm.frmPublicShare.GetItemFromListViewOnlineUsers(elem.IP);
                        if (itemTmp != null) userName = itemTmp.Text;

                        //item.Text = elem.ShareName + " (" + userName + ")";
                        item.Text = elem.ShareName;
#warning 未确定如何命名为好
                        //item.ImageIndex 
                        if (elem.IsFolder) item.ImageIndex = 0;
                        else item.ImageIndex = GetIndexOfIconByExtension(Io.Path.GetExtension(elem.LocalPath));
                        item.Tag = elem;
                        listViewShare.Items.Add(item);
                    }
                }
                listViewShare.EndUpdate();
#warning 这里有时会异常
//            }
//            catch 
//            {
//#warning test only
//                MessageBox.Show("test only!");
//            }
        }

        private delegate void PaintDirectoryInListViewCallBack(string directoryListXml, string hostIp);
        PaintDirectoryInListViewCallBack paintDirectoryInListViewCallBack;
        /// <summary>
        /// 根据请求得到的目录列表xml，在ListView中显示出来
        /// <para>注意：</para>
        /// 设立了一个公共字段
        /// </summary>
        /// <param name="directoryListXml">子目录/文件列表XML</param>
        public void PaintDirectoryInListView(string directoryListXml,string hostIp)
        {
            if (this.InvokeRequired)
            {
                paintDirectoryInListViewCallBack = new PaintDirectoryInListViewCallBack(PaintDirectoryInListView);
                this.listViewShare.Invoke(paintDirectoryInListViewCallBack, directoryListXml, hostIp);
            }
            else
            {
                if (RequestingDirectory != null)
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(directoryListXml);
                    XmlNode root = document.DocumentElement;//<ResponseOfListDirectory>
                    XmlNode node = root.FirstChild;
                    if (node == null)
                    {
                        //没有子文件（夹）的情况！！......
                        //MessageBox.Show("这是一个空文件夹！");//test only
                        listViewShare.Items.Clear();
                        RequestingDirectory = null;
                        return;
                    }
                    string childDirectory = node.Attributes["LocalPath"].Value;
                    if (childDirectory.EndsWith("\\"))
                        childDirectory = childDirectory.Substring(0, childDirectory.Length - 1);//去掉此路径末尾的"\"
                    string parentDirectory = childDirectory.Remove(childDirectory.LastIndexOf("\\"));
                    if (RequestingDirectory.EndsWith("\\"))
                        RequestingDirectory = RequestingDirectory.Substring(0, RequestingDirectory.Length - 1);//去掉请求路径末尾的\
                    if (parentDirectory.ToLower() == RequestingDirectory.ToLower())//与刚才请求的目录相符，因此应当解析XML并且显示在ListView中
                    {
                        listViewShare.Items.Clear();
                        listViewShare.BeginUpdate();
                        ListViewItem item;
                        while (node != null)
                        {
                            //收集每一个node的信息并且添加到ListView中
                            ShareElement se;
                            se.IP = hostIp;
                            se.ShareName = node.Attributes["ShareName"].Value;
                            se.IsFolder = bool.Parse(node.Attributes["IsFolder"].Value);
                            se.LocalPath = node.Attributes["LocalPath"].Value;
                            se.CanDelete = bool.Parse(node.Attributes["CanDelete"].Value);
                            se.CanCreate = bool.Parse(node.Attributes["CanCreate"].Value);
                            se.Size = node.Attributes["Size"].Value;
                            se.Info = node.Attributes["Info"].Value;
                            //ShareElement准备完毕
                            item = new ListViewItem();
                            item.Text = se.ShareName;
                            if (se.IsFolder) item.ImageIndex = 0;
                            else item.ImageIndex = GetIndexOfIconByExtension(Io.Path.GetExtension(se.LocalPath));
                            item.Tag = se;
                            listViewShare.Items.Add(item);
                            node = node.NextSibling;
                        }
                        listViewShare.EndUpdate();
                    }
                    else//test only
                        MessageBox.Show("您已经更改当前网络资源目录！");

                    document = null;
                }//if
            }//else
        }
        /// <summary>
        /// 获取对应扩展名的图标索引
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private int GetIndexOfIconByExtension(string extension)
        {
            int idx = imageList.Images.IndexOfKey(extension);
            if (idx != -1) return idx;
            else
            {
                Icon icon;
                try
                {
                    icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(extension, true);
                    if (icon == null)
                        throw new Exception();
                }
                catch { icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(".?", true); }
                imageList.Images.Add(extension, icon);
                return imageList.Images.Count - 1;
            }

        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (browseCurrentIndex == 0)//根目录的browseHistoryArrayList[browseCurrentIndex]不能转换为ShareElement，因为他是一个string类型的"\"
            {
                RequestingDirectory = null;
                PaintAllShareInListView();
            }
            else
            {
                ShareElement se = (ShareElement)browseHistoryArrayList[browseCurrentIndex];
                RequestingDirectory = se.LocalPath;
                MainForm.PMainForm.SendRequestForListDirectoryToAClient(se);//发送目录结构列举请求，在接收到udp通信时，再继续绘制ListView
            }
            listViewShare.Focus();
#warning 刷新时，应当重新从服务器获取所有共享列表，重新填充UserLists，再重新填充ListView（当修改了本地共享属性、删除共享，另一方刷新无效！！！当添加共享，另一方刷新有效）
        }       

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (browseCurrentIndex == 1)//此时再后退就为ArrayList中的第一个，是字符串“\”，他不是ShareElement类型
            {
                browseCurrentIndex--;//此时为0
                RequestingDirectory = null;
                PaintAllShareInListView();

                buttonBack.Enabled = false;
            }
            else
            {
                ShareElement se = (ShareElement)browseHistoryArrayList[--browseCurrentIndex];
                RequestingDirectory = se.LocalPath;
                MainForm.PMainForm.SendRequestForListDirectoryToAClient(se);//发送目录结构列举请求，在接收到udp通信时，再继续绘制ListView
                //填充ImageComboBox
                imageComboBoxShare.Text = "\\" + MainForm.PMainForm.frmPublicShare.GetItemFromListViewOnlineUsers(se.IP).Text + ":\\" + se.LocalPath;
            }

            if (browseCurrentIndex != browseHistoryArrayList.Count - 1) buttonForward.Enabled = true;

            listViewShare.Focus();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            ShareElement se = (ShareElement)browseHistoryArrayList[++browseCurrentIndex];
            RequestingDirectory = se.LocalPath;
            MainForm.PMainForm.SendRequestForListDirectoryToAClient(se);
            if (browseCurrentIndex == browseHistoryArrayList.Count - 1)//browseCurrentIndex此时已经到了浏览历史最右边的末尾的头了,不能再++了
            {
                buttonForward.Enabled = false;
            }
            if (browseCurrentIndex != 0)
            {
                buttonBack.Enabled = true;
            }

            //填充ImageComboBox
            imageComboBoxShare.Text = "\\" + MainForm.PMainForm.frmPublicShare.GetItemFromListViewOnlineUsers(se.IP).Text + ":\\" + se.LocalPath;
            
            listViewShare.Focus();
        }

        private void buttonRoot_Click(object sender, EventArgs e)
        {
            RequestingDirectory = null;//这样刷新时才不会跑到别的目录（记录中的）中去
            PaintAllShareInListView();//上面一句与本句应该在一块用
            //添加根目录到历史路径中去------->会分麻烦，不如直接到一开始的地方
            browseCurrentIndex = 0;
            buttonBack.Enabled = false;//此二句应在一块
            listViewShare.Focus();
        }

        private void buttonAddToFavorite_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {

        }

        private void listViewShare_ItemActivate(object sender, EventArgs e)
        {
            //在这里向服务器发送文件列表请求
            //要告诉是哪一个窗体发送的请求，---------不需要。因为发送后返回前主窗体阻塞；如果不阻塞的话则需要记录。
            ListViewItem item = listViewShare.SelectedItems[0];
            if (((ShareElement)item.Tag).IsFolder)
            {
                RequestingDirectory = ((ShareElement)item.Tag).LocalPath;
                MainForm.PMainForm.SendRequestForListDirectoryToAClient((ShareElement)item.Tag);
                browseCurrentIndex++;
                if (browseHistoryArrayList.Count - 1 < browseCurrentIndex)//更深一层,新的
                {
                    browseHistoryArrayList.Add(item.Tag);//这样不装箱，速度提升
                }
                else//在历史浏览路径中前进
                {
                    if (browseHistoryArrayList[browseCurrentIndex] != item.Tag)//历史路径方向已经改变
                    {
                        browseHistoryArrayList.RemoveRange(browseCurrentIndex, browseHistoryArrayList.Count - browseCurrentIndex);
                        browseHistoryArrayList.Add(item.Tag);//这样不装箱，速度提升
                    }//else 历史路径方向没有改变
                }
                if (browseCurrentIndex != 0) buttonBack.Enabled = true;
                else buttonBack.Enabled = false;
                if (browseCurrentIndex != browseHistoryArrayList.Count - 1) buttonForward.Enabled = true;
                else buttonForward.Enabled = false;
                imageComboBoxShare.Text = "\\" + MainForm.PMainForm.frmPublicShare.GetItemFromListViewOnlineUsers(((ShareElement)item.Tag).IP).Text + ":\\" + ((ShareElement)item.Tag).LocalPath;
#warning 必须保证有网络用户的图标才行！
            }
            else
            {
                MessageBox.Show("您请求的是文件！");
            }
        }
        

        private void 共享属性SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShareElement se =
           (ShareElement)this.listViewShare.SelectedItems[0].Tag;//一次只能编辑一个共享的属性
            FrmProperty frmProperty = new FrmProperty();
            frmProperty.Text = se.ShareName + " 共享属性";
            //初始化成员变量然后显示属性窗体
            frmProperty._labelShareName = frmProperty._textBoxShareName = se.ShareName;
            frmProperty._labelType = se.IsFolder ? "文件夹" : "文件";
            frmProperty._labelPath = frmProperty._textBoxPath = se.LocalPath;
            frmProperty._checkBoxCanDelete = se.CanDelete;
            frmProperty._checkBoxCanCreate = se.CanCreate;
            frmProperty._labelSize = se.Size;
            frmProperty._textBoxInfo = se.Info;
            
            frmProperty.Show();
        }

        /// <summary>
        /// 控制右键菜单的item的显示与否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripListView_Opening(object sender, CancelEventArgs e)
        {
            if (listViewAllShare.SelectedItems.Count == 0)
            {
                共享属性SToolStripMenuItem.Visible = false;
                另存为SToolStripMenuItem.Visible = false;
            }
            else
            {
                共享属性SToolStripMenuItem.Visible = true;
                另存为SToolStripMenuItem.Visible = true;
            }
        }

        private void 另存为SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShareElement se = (ShareElement)listViewAllShare.SelectedItems[0].Tag;
            string fileName = se.ShareName;
            char[] illegalChars = new char[9] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };//Windows文件命名的非法字符
            for (int i = 0; i < illegalChars.Length; i++)
            {
                fileName = fileName.Replace(illegalChars[i], '_');//把所有非法字符替换为下划线
            }
            saveFileDialog1.FileName = fileName;//在这里赋值时纯文件名(fileName)，但选择路径后他会自动变成全路径文件名(saveFileDialog1.FileName)
            //saveFileDialog1.DefaultExt
            //saveFileDialog1.OpenFile()
            //saveFileDialog1.Title
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //\/\/\/\/--------------------------TCP客户端-------------\/\/\/\/\-------------------
                /*作为TCP客户端向服务器端发送连接请求，连接建立后发送文件全路径(文件请求指令)以请求文件.
                 * 接收时，先检查是否为指令。
                 取消时，向服务器发送指令以通知他取消发送线程*/

                client = new TcpClient(AddressFamily.InterNetwork);
                IPAddress serverIp = IPAddress.Parse(se.IP);
                //线程同步事件设为非终止状态
                allDone.Reset();
                //回调参数
                ClientNFileName cnn = new ClientNFileName();
                cnn.client = client;
                cnn.FileName = se.LocalPath;//远程文件全路径
                cnn.shareElement = se;
                cnn.SaveToPath = saveFileDialog1.FileName;//存储本地存储路径
                Io.FileStream fs = (Io.FileStream)saveFileDialog1.OpenFile();
                cnn.fileStream = fs;
                //开始一个对远程主机的异步连接请求
                client.BeginConnect(serverIp, MainForm.PMainForm.PORT_TCP_SERVER_RECEIVE, new AsyncCallback(ConnectTcpServerCallback), cnn);
                //阻塞当前线程等待BeginConnect完成；当BeginConnect完成时自动调用ConnectTcpServerCallback，并由他调用Set方法解除阻塞
                allDone.WaitOne();
#warning 用单独一个线程执行，因为这里有线程阻塞代码!
                //----/\/\/\/\/\/\/\/\/---------------------------------------------------------------


                
            }
        }
        /// <summary>
        /// 连接服务器回调方法 
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectTcpServerCallback(IAsyncResult ar)
        {
            //异步操作执行到此，说明BeginConnect已经完成
            //解除阻塞(可以继续连接其他服务器)
            allDone.Set();
            //try
            //{
                client = ((ClientNFileName)ar.AsyncState).client;
                string fileName = ((ClientNFileName)ar.AsyncState).FileName;
                Io.FileStream fs = ((ClientNFileName)ar.AsyncState).fileStream;
                //异步接受传入的连接尝试，使BeginConnect正常结束
                client.EndConnect(ar);
                networkStream = client.GetStream();
                //发送文件请求指令
                string stringFileRequest = "FILEREQUEST:" + fileName;
                SendRequestToServer(stringFileRequest);
                //异步接收服务器发送过来的（文件）数据
                ReadObject readObject = new ReadObject(networkStream, client.ReceiveBufferSize, fs, fileName, ((ClientNFileName)ar.AsyncState).shareElement);
                readObject.SaveToPath = ((ClientNFileName)ar.AsyncState).SaveToPath;//传递本地存储路径
                networkStream.BeginRead(readObject.bytes, 0, readObject.bytes.Length, new AsyncCallback(ReadFileLength), readObject);
            //}
            //catch (Exception ex)
            //{
            //    CToolClass.LogError(ex.Message);
            //    MessageBox.Show(ex.Message);
            //}
        }
        /// <summary>
        /// 回调方法，读取文件长度值。
        /// <para>由于服务器是把文件大小（字符串）和文件数据（可能为二进制数据）分两次发送/写入，因此客户端这里分两次接收/读取。</para>
        /// 两次数据的内容格式不相同，而且服务器端分两次发送的。。。。
        /// </summary>
        /// <param name="ar"></param>
        private void ReadFileLength(IAsyncResult ar)
        {
            ReadObject readObject = (ReadObject)ar.AsyncState;
            readObject.networkStream.EndRead(ar);//结束读取文件长度数据的异步操作
#warning 【未处理异常】如果远程服务器上其实没有这个请求的文件,或者另存为的是个文件夹，则：“无法从传输连接中读取数据: 远程主机强迫关闭了一个现有的连接。。”
            string length = Encoding.UTF8.GetString(readObject.bytes).Substring("FILELENGTH:".Length);
            readObject.FileLength = long.Parse(length);//存储长度值
            //MessageBox.Show(readObject.FileLength.ToString());//test only
            readObject.networkStream.BeginRead(readObject.bytes, 0, readObject.bytes.Length, new AsyncCallback(ReadCallback), readObject);//开始读取文件数据的异步操作
        }
        /// <summary>
        /// 回调方法，读取文件数据，并写入本地文件流
        /// </summary>
        /// <param name="ar"></param>
        private void ReadCallback(IAsyncResult ar)
        {
            //异步操作执行到此，说明BeginRead已经完成
            //在这里写入文件流
            //try
            //{
                ReadObject readObject = (ReadObject)ar.AsyncState;
                //完成一次异步读取操作
                int count = readObject.networkStream.EndRead(ar);
//#warning 无法从传输连接中读取数据: 远程主机强迫关闭了一个现有的连接(it still happens when I changed the reading method of file to send)

                if (readObject.offset == 0)//第一次异步读取时
                {
                    NewFrmDownloading(
                        readObject.fileName,
                        "从\'\\\\" + MainForm.PMainForm.frmPublicShare.GetItemFromListViewOnlineUsers(readObject.shareElement.IP).Text + "\\" + Io.Path.GetDirectoryName(readObject.shareElement.LocalPath) + "\' 到 \'" + Io.Path.GetDirectoryName(readObject.SaveToPath) + "\'",
                        (int)readObject.FileLength,
                        readObject.shareElement.IP + readObject.shareElement.LocalPath,//这样应该比较唯一了。。。。
                        readObject
                    );
                }

                readObject.offset += count;
                if (DictFrmDownloading.ContainsKey(readObject.shareElement.IP + readObject.shareElement.LocalPath))//存在进度条窗体
                {
                    SetFrmDownloading(readObject.shareElement.IP + readObject.shareElement.LocalPath, readObject.offset);//更改进度条当前值<线程间操作无效>
                }
                else
                {
                    count = 0;
                }
                
                //写入文件流
                try { readObject.fileStream.Write(readObject.bytes, 0, count); }//readObject.offset->0//exceptions:磁盘空间不足。
                catch (ObjectDisposedException) { count = 0; }//文件流已经关闭（只能是终止文件传输,如果传输完毕则不会到这里）
                catch (Exception ex) { MessageBox.Show(ex.Message, "在写入文件时错误"); count = 0; }//count置0以关闭相关对象

               
                //else if(//最后一次异步读取时关闭窗体和文件流)
                if (count == 0)
                {
                    //frmDownloading.Close();
                    readObject.fileStream.Close();
                    readObject.networkStream.Close();
                    //关闭进度窗体
                    CloseFrmDownloading(readObject.shareElement.IP + readObject.shareElement.LocalPath);
                    //DictFrmDownloading[readObject.shareElement.IP + readObject.shareElement.LocalPath].Close();
                    //线程间操作无效: 从不是创建控件“FrmDownloading”的线程访问它-->解决办法：使用委托在主线程中调用！
                }

                if (!isExit && count != 0)
                {
                    //重新调用BeginRead进行异步读取
                    networkStream.BeginRead(readObject.bytes, 0, readObject.bytes.Length, new AsyncCallback(ReadCallback), readObject);
//#warning 无法从传输连接中读取数据: 远程主机强迫关闭了一个现有的连接
                }
            //}
            //catch (Exception ex)
            //{
            //    CToolClass.LogError(ex.Message);
            //    MessageBox.Show(ex.Message);
            //}
        }
        /// <summary>
        /// 发送客户端请求到服务器
        /// </summary>
        /// <param name="stringRequest"></param>
        private void SendRequestToServer(string stringRequest)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(stringRequest);
            networkStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(SendRequestCallback), networkStream);
            networkStream.Flush();
        }
        private void SendRequestCallback(IAsyncResult ar)
        {
            //try
            //{
                networkStream.EndWrite(ar);
            //}
            //catch (Exception ex)
            //{
            //    CToolClass.LogError(ex.Message);
            //    MessageBox.Show(ex.Message);
            //}
        }


        private delegate void NewFrmDownloadingHandler(string labelFileName, string labelDownloading, int maximum, string keyIpNPath, ReadObject readObject);
        /// <summary>
        ///[跨线程]用于子线程跨线程在主线程中创建进度条窗体
        /// </summary>
        /// <param name="labelFileName"></param>
        /// <param name="labelDownloading"></param>
        /// <param name="maximum"></param>
        /// <param name="keyIpNPath">在DicFrmDownloading中的Key值,是目标IP与其文件来自ShareElement的LocalPath</param>
        /// <param name="readObject">用于Cancel对应窗体时候能找到对应于窗体的文件流、网络流等</param>
        private void NewFrmDownloading(string labelFileName, string labelDownloading, int maximum,string keyIpNPath,ReadObject readObject)
        {
            if (this.InvokeRequired)//在子线程中调用,希望在主线程创建窗体
            {
                this.Invoke(new NewFrmDownloadingHandler(NewFrmDownloading), labelFileName, labelDownloading, maximum, keyIpNPath, readObject);//在主线程执行委托
            }
            else
            {
                if (!DictFrmDownloading.ContainsKey(keyIpNPath))//不存在此进度条窗体
                {
                    //显示文件传输进度窗体
                    FrmDownloading frmDownloading = new FrmDownloading(labelFileName, labelDownloading, maximum);
                    frmDownloading.KeyIpNPath=keyIpNPath;//在CancelFrmDownloading中会用到
                    frmDownloading.ReadObject = readObject;
                    DictFrmDownloading.Add(keyIpNPath, frmDownloading);
                    //frmDownloading.LabelFileName = readObject.fileName;
                    //frmDownloading.LabelDownloading = "从\'\\\\" + MainForm.PMainForm.frmPublicShare.GetItemFromListViewOnlineUsers(readObject.shareElement.IP).Text + "\\" + Io.Path.GetDirectoryName(readObject.shareElement.LocalPath) + "\' 到 \'" + Io.Path.GetDirectoryName(saveFileDialog1.FileName) + "\'";//windows里面的不包含文件名，这里包含了
                    frmDownloading.Show();
                }
                else
                {
                    DictFrmDownloading[keyIpNPath].Focus();//置此窗体于前端
                }
            }
        }
        private delegate void SetFrmDownloadingHandler(string keyIpNPath, int value);
        /// <summary>
        /// [跨线程]设置当前进度条窗体的值，进度条窗体在DicFrmDownloading中使用keyIpNPath定位，设置进度值为value
        /// <para>已经处理的异常情况:</para>
        /// </summary>
        /// <exception cref="[此异常已经预防]FrmDownloading窗体在字典中不存在：则不进行处理">可能的异常</exception>
        /// <param name="keyIpNPath"></param>
        /// <param name="value"></param>
        private void SetFrmDownloading(string keyIpNPath, int value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetFrmDownloadingHandler(SetFrmDownloading), keyIpNPath, value);
                //exception:"给定关键字不在字典中。"
            }
            else
            {
                if (DictFrmDownloading.ContainsKey(keyIpNPath))
                {
                    //首先定位到这个窗体
                    FrmDownloading fd = DictFrmDownloading[keyIpNPath];
                    fd.ValueProperty = value;//然后设置进度值
                }                
            }
        }
        private delegate void CloseFrmDownloadingHandler(string keyIpNPath);
        /// <summary>
        /// [跨线程]关闭指定key的进度条窗体
        /// </summary>
        /// <param name="keyIpNPath"></param>
        private void CloseFrmDownloading(string keyIpNPath)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CloseFrmDownloadingHandler(CloseFrmDownloading), keyIpNPath);
            }
            else
            {
                if (DictFrmDownloading.ContainsKey(keyIpNPath))
                {
                    DictFrmDownloading[keyIpNPath].Close();
                    DictFrmDownloading.Remove(keyIpNPath);//一定要删除干净
                }
            }
        }
        public void CancelFrmDownloading(string keyIpNPath)
        {
            
            //在FrmDownloading的取消按钮响应方法中自己完成窗体的关闭；在这里完成文件流、网络流的关闭以及传输过来的文件的删除操作
            FrmDownloading fd = DictFrmDownloading[keyIpNPath];
            fd.ReadObject.networkStream.Close();
            fd.ReadObject.fileStream.Close();
            //关闭窗体，移除窗体引用
            CloseFrmDownloading(keyIpNPath);
            //MessageBox.Show(fd.ReadObject.fileName);
            Io.File.Delete(fd.ReadObject.SaveToPath);//这里不要删错了文件！！！readObject.FileName是远程路径readObject.SaveToPath是本地路径

        }
    }
    ///// <summary>
    ///// 用于显示与设置进度条窗体的类
    ///// </summary>
    //public class ProcessForm
    //{
    //    FrmDownloading frmDownloading = new FrmDownloading();
    //    /// <summary>
    //    /// 初始化FrmDownloading窗体上的各个控件值
    //    /// </summary>
    //    /// <param name="labelFileName">正在传输的文件名字</param>
    //    /// <param name="labelDownloading">例如从什么到什么</param>
    //    /// <param name="maximum">进度条的最大值</param>
    //    public ProcessForm(string labelFileName,string labelDownloading,int maximum)
    //    {
    //        frmDownloading.LabelFileName = labelFileName;
    //        frmDownloading.LabelDownloading = labelDownloading;
    //        frmDownloading.MaximumProperty = maximum;
    //    }
    //    /// <summary>
    //    /// 显示进度窗体
    //    /// </summary>
    //    public void Show()
    //    {
    //        frmDownloading.Show();
    //    }
    //    /// <summary>
    //    /// 主要用于设置窗体中进度值
    //    /// </summary>
    //    public int CurrentValue
    //    {
    //        get { return frmDownloading.ValueProperty; }
    //        set { frmDownloading.ValueProperty = value; }
    //    }
    //}
}