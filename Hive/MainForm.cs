using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Collections;

namespace Hive
{

    //在公共字段UserLists中保存用户列表,元素类型为：
    /// <summary>
    /// 每个其他网上用户的IP（用作唯一标识）及其共享列表
    /// </summary>
    public struct UserShareList
    {
        public string IP;
        /// <summary>
        /// 此类型实例要在接收到共享列表时进行实例化,因此在使用前应该先确保其已经实例化（要验证）
        /// </summary>
        public List<ShareElement> ShareElements;
    }
    /// <summary>
    /// 对应于MyShare.XML文件里的一个共享资源元素,并且不仅包括SShareElementAttributes的所有字段，另外又加上了IP字段，以标志其用户。
    /// <para>！！本struct专用于（网络资源浏览器）FormDocuementAllShare窗体的ListViewItem！！</para>
    /// </summary>
    public struct ShareElement
    {        
        /// <summary>
        /// 此字段主要用于在ListViewItem中的Tag，当用户进行其他操作如请求、删除、修改此共享资源等操作时，可以知道其目标
        /// </summary>
        public string IP;

        public string ShareName;
        public bool IsFolder;
        public string LocalPath;
        public bool CanDelete;
        public bool CanCreate;
        public string Size;
        public string Info;
    }




    //------------------------------------------------------------------
    /// <summary>
    /// 主窗体
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>指向MainForm实例对象(静态字段成员)
        /// </summary>
        public static MainForm PMainForm;
        /// <summary>我的共享（共享管理器）子窗体(document form)
        /// </summary>
        public FormDocument frmDocument;
        /// <summary>网络资源浏览器子窗体(document form)
        /// </summary>
        public FormDocumentAllShare frmDocAllShare;
        /// <summary>显示其他在线的人的子窗体
        /// </summary>
        public FormPublicSharers frmPublicShare;
        /// <summary>个人信息子窗体
        /// </summary>
        private FormPersonalProfile frmPersonalProfile;
        /// <summary>我的电脑(添加共享)子窗体
        /// </summary>
        public FormMyComputer frmMyComputer;
        //private FormTask frmTask;
        //private FormProperty frmProperty;

        /// <summary>访问公开的工具栏按钮的引用
        /// </summary>
        public ToolStripButton btnBack, btnForward;

        /// <summary>在构造时保存工作目录（程序可执行文件所在目录）
        /// </summary>
        static public readonly string WorkDirectory = Application.StartupPath;
       

        //-/\/\/\/\/\/--------------------------UDP 网络方面的字段-------/\/\/\/\/\/\//\/\-------
        
        
        /// <summary>
        /// UDP接受对象(from server)接收组播
        /// </summary>
        private UdpClient udpClientReceive;
        /// <summary>
        /// 组播地址
        /// </summary>
        private const string MULTICAST_IP = "224.0.0.9";
        /// <summary>
        /// 本地端口8899：
        /// 1.用于本客户端接收服务器组播(与服务器上的要发送到的目标端口一致)
        /// <para>2.用于各个客户端之间进行P2P指令的发送</para>
        /// 
        /// </summary>
        private const int PORT_UDP_RECEIVE = 8899;
        //--------
        /// <summary>
        /// 本地端口9999：用于本客户端接收服务器单播到本机上的在本机上线之前就已经上线的其他用户的状态信息
        /// <para>与服务器上的要发送到的目标端口一致</para>
        /// </summary>
        private const int PORT_UDP_RECV_OTHERUSERINFOS_N_P2P_CMD = 9999;
        /// <summary>
        /// UDP发送对象(to server)-------------------------------
        /// <para>程序运行期间一直使用</para>
        /// </summary>
        private UdpClient udpClientSender = new UdpClient();
        /// <summary>
        /// 服务器的IP string
        /// </summary>
        private const string IP_OF_SERVER = "192.168.135.171";
        /// <summary>
        /// 服务器的IPEndPoint
        /// <para>已经在构造函数中初始化</para>
        /// </summary>
        private readonly IPEndPoint IPEndPointOfServer = null;
        
        
        //--------------
        /// <summary>
        /// 网络用户列表（重要！）
        /// <para>&lt;string, UserShareList&gt; == &lt;IP, UserShareList&gt;</para>
        /// </summary>
        public Dictionary<string, UserShareList> UserLists = new Dictionary<string, UserShareList>();
        
        
        //\/\/\/\/\/\-------------------------TCP服务器端----------/\/\/\/\/\/\/*/-----
        /*TCP服务器端监听客户端的连接请求，收到后读取网络流以获取客户端发过来的文件全路径，确认此文件存在后向客户端发送文件，
         * 否则发送XML格式指令说明文件未找到*/
        /// <summary>
        /// 程序是否已退出
        /// </summary>
        private bool isExit = false;
        /// <summary>
        /// 连接的客户端列表
        /// </summary>
        ArrayList clientList = new ArrayList();
        /// <summary>
        /// TcpListener对象
        /// </summary>
        TcpListener listener;
        /// <summary>
        /// 线程同步事件类，非终止状态、手动重置
        /// </summary>
        private EventWaitHandle allDone = new EventWaitHandle(false, EventResetMode.ManualReset);
        /// <summary>
        /// 服务器端接收端口
        /// </summary>
        public int PORT_TCP_SERVER_RECEIVE = 19870;
        /// <summary>
        /// 客户端接收端口
        /// </summary>
        public int PORT_TCP_CLIENT_RECEIVE = 19871;


        //-----------------------------------------------------------------------------------
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {            
            InitializeComponent();
            skinEngine1.Active = false;//系统默认皮肤
#warning 放在这里，第一次运行时，默认的菜单栏工具栏的背景颜色就是正常的了
            PMainForm = this;
            //MessageBox.Show(Application.CommonAppDataPath);//
            frmDocument = new FormDocument();
            frmDocument.DockAreas = DockAreas.Document;
            frmDocument.Show(dockPanelMain);
            dockPanelMain.Refresh();

            frmDocAllShare = new FormDocumentAllShare();
            frmDocAllShare.DockAreas = DockAreas.Document;
            frmDocAllShare.Show(dockPanelMain);
            dockPanelMain.Refresh();

            frmPublicShare = new FormPublicSharers();
            //frmPublicShare.DockAreas = DockAreas.DockLeft;            
            frmPublicShare.Show(dockPanelMain, DockState.DockBottom);
            frmPublicShare.Show(dockPanelMain, DockState.DockLeft);
            dockPanelMain.Refresh();

            frmPersonalProfile = new FormPersonalProfile();
            frmPersonalProfile.Show(dockPanelMain, DockState.DockTop);
            frmPersonalProfile.Show(dockPanelMain, DockState.DockLeft);
            dockPanelMain.Refresh();

            frmMyComputer = new FormMyComputer();
            frmMyComputer.DockAreas = DockAreas.DockLeft;
            frmMyComputer.Show(dockPanelMain, DockState.DockLeftAutoHide);
            //frmMyComputer.Show(dockPanelMain, DockState.DockLeft);
            dockPanelMain.Refresh();

            //frmTask = new FormTask();
            //frmTask.DockAreas = DockAreas.DockBottom;
            //frmTask.Show(dockPanelMain, DockState.DockBottomAutoHide);//单独此行，使只在下载时可见//关闭并保存前要隐藏
            //frmTask.Show(dockPanelMain);

            //frmProperty = new FormProperty();
            //frmProperty.Show(dockPanelMain, DockState.DockRightAutoHide);//点击文件夹/文件时显示

            //控件引用初始化-----------------
            btnBack = toolStripButtonBack;
            btnForward = toolStripButtonForward;
            //-----------变量初始化------------------------------            
            //服务器的IPEndPoint
            IPEndPointOfServer = new IPEndPoint(IPAddress.Parse(IP_OF_SERVER), 8890);
        }

        /// <summary>找到文档窗口并返回之!
        /// </summary>
        /// <param name="text">文档窗口的TabText</param>
        /// <returns>IDockContent</returns>
        public IDockContent FindDocument(string text)
        {
            if (dockPanelMain.DocumentStyle == DocumentStyle.SystemMdi)//系统多文档模型
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)//找到第一个就OK
                        return form as IDockContent;

                return null;//没有找到
            }
            else//其他：如默认为DockingMdi
            {
                foreach (IDockContent content in dockPanelMain.Documents)
                    if (content.DockHandler.TabText == text)//作为Docuement Window的唯一识别？？ 这里可以用来修改Document的Text属性！！
                        return content;

                return null;
            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            DockContentNew dockContent;//这个类被所有子窗体继承并且含有字段IsShowed

            if (menuItem == 在线用户OToolStripMenuItem)
                dockContent = frmPublicShare;
            else if (menuItem == 个人信息PToolStripMenuItem)
                dockContent = frmPersonalProfile;
            else if (menuItem == 我的电脑MToolStripMenuItem)
                dockContent = frmMyComputer;
            //else if (menuItem == 任务信息TToolStripMenuItem)
            //    dockContent = frmTask;
            //else if (menuItem == 资源属性PToolStripMenuItem)
            //    dockContent = frmProperty;
            else
                return;

            if (dockContent.IsShowed)
            {
#warning        //if (dockContent == null) MessageBox.Show("already closed!->need Modifying here");
                dockContent.Hide();
                dockContent.IsShowed = false;
            }
            else
            {
                dockContent.Show();
                dockContent.IsShowed = true;
            }
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 我的所有共享WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeifenLuo.WinFormsUI.Docking.IDockContent content = MainForm.PMainForm.FindDocument("共享管理器");

            if (content == null)//该文档窗口已经被关闭了
            {
                frmDocument = new FormDocument();
                frmDocument.DockAreas = DockAreas.Document;
                frmDocument.Show(MainForm.PMainForm.dockPanelMain);
            }
            else//没有关闭
            {
                frmDocument = (FormDocument)content.DockHandler.Form;
            }

            frmDocument.Activate();
            //frmDocument.PaintListViewWithMyShare();
        }

        
        /// <summary>
        /// 后台线程函数:接收来自服务器的UDP组播,获取新上线的（单个）在线用户状态信息、共享列表。
        /// 在后台运行进行接收.
        /// <para>!!!Server to Local Client</para>
        /// </summary>
        private void ReceiveUserStatusProc()
        {
            try
            {
                //在本地指定端口接收状态信息UDP广播
                udpClientReceive = new UdpClient(PORT_UDP_RECEIVE);
                udpClientReceive.JoinMulticastGroup(IPAddress.Parse(MULTICAST_IP));
#warning 在其上下文中，该请求的地址无效。
                udpClientReceive.Ttl = 50;
                IPEndPoint remoteIpEndPoint = null;

                while (true)
                {
                    try
                    {
                        //读取
                        byte[] bytes = udpClientReceive.Receive(ref remoteIpEndPoint);//udpClient关闭时此句抛出异常
                        string strings = Encoding.UTF8.GetString(bytes);//strings应该为包含Name、StatusInfo、IsOnline值的XML格式字符串

                        //解析XML字符串
                        string Name = "", StatusInfo = "", ip = "";
                        bool IsOnline = false;
                        XmlDocument document = new XmlDocument();
                        try { document.LoadXml(strings); }
                        catch (Exception ex) { CToolClass.LogError(ex.Message); MessageBox.Show(ex.Message); }
                        XmlNode root = document.DocumentElement;//UdpBroadcast
                        XmlNodeList nodes = root.ChildNodes;

                        if (root.Name == "UdpUser")//确认是UdpUser状态信息，而不是其他
                        {
                            foreach (XmlNode node in nodes)
                            {
                                if (node.Name == "Ip")
                                {
                                    ip = node.InnerText;
                                    continue;
                                }
                                if (node.Name == "Name")
                                {
                                    Name = node.InnerText; //MessageBox.Show(node.InnerText);
                                    continue;
                                }
                                if (node.Name == "StatusInfo")
                                {
                                    StatusInfo = node.InnerText;
                                    continue;
                                }
                                if (node.Name == "IsOnline")
                                {
                                    IsOnline = bool.Parse(node.InnerText);
                                    continue;
                                }
                            }
                            //检查获得的远程用户状态信息，根据结果决定是否更新本地系统的网络用户列表---------------------------
                            frmPublicShare.SetUserOnlineStatus(ip, Name, StatusInfo, IsOnline);//需要遍历完<UdpUser>的每一个子节点才能得到所有信息。（亦即foreach执行完）
                        }
                        else if (root.Name == "MyShare")
                        {
                            //MessageBox.Show("组播收到可能是共享列表信息！"+strings);
                            UserShareList usl;
                            XmlDocument document_ = new XmlDocument();
                            document_.LoadXml(strings);
                            XmlElement root_ = document_.DocumentElement;//MyShare
                            XmlNode node_ = root.FirstChild;//<IP>
                            usl.IP = node_.InnerText;
                            usl.ShareElements = new List<ShareElement>();
                            ShareElement element;//共享资源元素
                            while (node_.NextSibling != null)//当前node_为表示IP的node，因此需要前进一下。
                            {
                                node_ = node_.NextSibling;
                                element = new ShareElement();//结构体在这里也必须new?？
                                element.IP = usl.IP;
                                element.ShareName = node_.Attributes["ShareName"].Value;
                                element.IsFolder = bool.Parse(node_.Attributes["IsFolder"].Value);
                                element.LocalPath = node_.Attributes["LocalPath"].Value;
                                element.CanDelete = bool.Parse(node_.Attributes["CanDelete"].Value);
                                element.CanCreate = bool.Parse(node_.Attributes["CanCreate"].Value);
                                element.Size = node_.Attributes["Size"].Value;
                                element.Info = node_.Attributes["Info"].Value;
                                usl.ShareElements.Add(element);//添加到此人的共享列表记录中
                            }
                            if (!UserLists.ContainsKey(usl.IP))
                                UserLists.Add(usl.IP, usl);//添加此人到用户列表中(IP在两个位置存了)
                            else
                            {
                                UserLists.Remove(usl.IP);//删除旧的
                                UserLists.Add(usl.IP, usl);//添加新的
                            }
                            document_ = null;
#warning 在FOrmDocumentAllShare添加各个共享列表到ListView中，注意在这里过滤掉自己的共享内容（干脆添加到UserLists时就不添加）
                            //UserLists is ready                       
                        }
                        document = null;

                    }
                    catch//有可能隐藏异常！！！！要小心！！！
                    {
                        //窗体关闭，退出循环，结束线程
                        break;
                    }



                }//while 
            }
            catch (SocketException ex)
            {
                MessageBox.Show("发生网络错误！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 后台线程函数
        /// <para>1.接收来自服务器的UDP单播,获取其他（多个）所有在线的用户的状态信息；</para>
        /// 2.当本地用户向其他客户端发送目录结构请求后接收其目录结构。(P2P)
        /// <para>后台运行线程。</para>
        /// <para>仅在网络上某用户刚刚上线时候发挥作用AnD以及本地用户请求其他用户的共享目录</para>
        /// <para>!!!Server to Local Client && Other Client To Local Client</para>
        /// </summary>
        private void ReceiveUserInfosWhenNewOnlineProc()
        {
            UdpClient udpClientRecv = new UdpClient(PORT_UDP_RECV_OTHERUSERINFOS_N_P2P_CMD);
            IPEndPoint remoteIpEndPoint = null;//服务器的IpEndPoint
            while (true)
            {
                try
                {
                    //读取
                    byte[] bytes = udpClientRecv.Receive(ref remoteIpEndPoint);//udpClient关闭时此句抛出异常
                    string strings = Encoding.UTF8.GetString(bytes);//strings应该为包含Name、StatusInfo、IsOnline值的XML格式字符串

                    //解析XML字符串
                    string Name = "", StatusInfo = "", ip = "";
                    bool IsOnline = false;

                    XmlDocument document = new XmlDocument();
                    try { document.LoadXml(strings); }
                    catch (Exception ex) { CToolClass.LogError(ex.Message); MessageBox.Show(ex.Message); }
                    XmlNode root = document.DocumentElement;//UdpBroadcast
                    XmlNodeList nodes = root.ChildNodes;
//判断XML格式信息的类型
                        if (root.Name == "UdpUser")//确认是UdpUser状态信息，而不是其他--------------------
                        {
                            foreach (XmlNode node in nodes)//UdpUser的每一个子节点
                            {

                                if (node.Name == "Ip")
                                {
                                    ip = node.InnerText;
                                    continue;
                                }
                                if (node.Name == "Name")
                                {
                                    Name = node.InnerText; //MessageBox.Show(node.InnerText);
                                    continue;
                                }
                                if (node.Name == "StatusInfo")
                                {
                                    StatusInfo = node.InnerText;
                                    continue;
                                }
                                if (node.Name == "IsOnline")
                                {
                                    IsOnline = bool.Parse(node.InnerText);
                                    continue;
                                }
                            }
                                //MessageBox.Show(Name);//test only
                                //检查获得的远程用户状态信息，根据结果决定是否更新本地系统的网络用户列表---------------------------
                                frmPublicShare.SetUserOnlineStatus(ip, Name, StatusInfo, IsOnline);//需要遍历完<UdpUser>的每一个子节点才能得到所有信息。（亦即foreach执行完）
                                                     
                        }
                        else if (root.Name == "MyShare")//是共享列表信息-------------------------------------------------
                        {
                            //MessageBox.Show("单播收到可能是共享列表信息！\n"+strings);
                            UserShareList usl=new UserShareList();//new 一下结构体(usl老是出现异常之后加上的)
                            XmlDocument document_ = new XmlDocument();
                            document_.LoadXml(strings);
                            XmlElement root_ = document_.DocumentElement;//MyShare
                            XmlNode node_ = root.FirstChild;//<IP>
                            usl.IP = node_.InnerText;//ip
                            usl.ShareElements = new List<ShareElement>();
                            ShareElement element;//共享资源元素
                            while (node_.NextSibling != null)//-------------------------遍历MyShare的子节点
                            {
                                node_ = node_.NextSibling;
                                element = new ShareElement();//(usl出现异常之前加上的，而且在这里没有出现异常)
                                element.IP = usl.IP;
                                element.ShareName = node_.Attributes["ShareName"].Value;
                                element.IsFolder = bool.Parse(node_.Attributes["IsFolder"].Value);
                                element.LocalPath = node_.Attributes["LocalPath"].Value;
                                element.CanDelete = bool.Parse(node_.Attributes["CanDelete"].Value);
                                element.CanCreate = bool.Parse(node_.Attributes["CanCreate"].Value);
                                element.Size = node_.Attributes["Size"].Value;
                                element.Info = node_.Attributes["Info"].Value;
                                usl.ShareElements.Add(element);//添加到此人的共享列表记录中
                            }
                            if (!UserLists.ContainsKey(usl.IP))
                                UserLists.Add(usl.IP, usl);//添加此人到用户列表中(IP在两个位置存了)<exception>
#warning 未将对象引用设置到对象的实例。?<可能原因是用户的相关状态信息还没上来，故new一下>------------>已经进行修改，看效果
                            else
                            {
                                UserLists.Remove(usl.IP);//删除旧的
                                UserLists.Add(usl.IP, usl);//添加新的
                            }
                            document_ = null;
                        }
                        else if (root.Name == "RequestForListDirectory")//有其他客户端请求目录列举,接受指令,发送目录结构xml
                        {
                            XmlNode nodeChild = root.FirstChild;
                            string shareName = nodeChild.InnerText;//收到的共享名
                            nodeChild = nodeChild.NextSibling;
                            string path = nodeChild.InnerText;//收到的本地目录
                                                                                //MessageBox.Show("对方请求目录"+path,"接收到目录结构请求");
                            //往此客户端发送目录结构
                            SendResponseOfListDirectoryToAClient(remoteIpEndPoint.Address.ToString(), shareName, path);

                        }
                        else if (root.Name == "ResponseOfListDirectory")//请求目录已经被返回，接收目录结构
                        {
                            //MessageBox.Show(strings,"对方返回目录！");
                            frmDocAllShare.PaintDirectoryInListView(strings,remoteIpEndPoint.Address.ToString());
                        }
                        else//test only
                        {
                            MessageBox.Show("未知XML！");
                        }

                        document = null;
                }//try
                catch
                {
                //    窗体关闭，退出循环，结束线程
                    break;
                }
            }//while
        }
        /// <summary>
        /// 是第一次上线失败（默认为true）
        /// </summary>
        private bool isFirstTimeOnlineError = true;
        /// <summary>
        /// （应定时）发送本地用户状态信息到服务器。
        ///  <para>不管状态信息是否已经更改，只要在线就发送，以通知服务器“我”在线</para>
        /// 使用timer组件，间隔2秒时间执行 ///
        /// </summary>
        /// <param name="isOnline">上线还是下线</param>
        /// <seealso cref="SendShareListToServer"/>
        private void NotifyServerLocalUserStatus(bool isOnline)
        {
            try
            {
                //设置广播内容
                CUserInfo ui = new CUserInfo();
                string xmlString = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                xmlString += "<UdpUser><Name>";
                xmlString += ui.Name;
                xmlString += "</Name><StatusInfo>";
                xmlString += ui.StatusInfo;
                xmlString += "</StatusInfo><IsOnline>";
                xmlString += isOnline.ToString();
                xmlString += "</IsOnline></UdpUser>";
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xmlString);
                //向服务器发送信息
                udpClientSender.Send(bytes, bytes.Length, IPEndPointOfServer);
            }
            catch (Exception ex)
            {
                if (!this.Disposing && isFirstTimeOnlineError)
                {
                    isFirstTimeOnlineError = false;//下次如果失败就不是第一次了，于是不执行本代码段
                    CToolClass.LogError(ex.Message);
                    MessageBox.Show("上线失败，请检查网络连接！");
                }
            }
        }
        /// <summary>
        /// 发送本地共享列表到服务器。
        /// <para>上线时候发送一次；修改时发送一次。</para>
        /// 不进行定时发送。
        /// </summary>
        /// <seealso cref="NotifyServerLocalUserStatus"/>
        public void SendShareListToServer()
        {
            string shareList="";           
            try
            {//读取本地共享列表
                StreamReader streamReader = new StreamReader(MainForm.WorkDirectory + "\\" + "MyShare.xml", Encoding.Default);
                shareList = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"无法读取本地共享列表！");
            }
            if (shareList == "")
            {
                MessageBox.Show("共享列表文件为空文件！","错误");
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(shareList);

            try
            {
                udpClientSender.Send(bytes, bytes.Length, IPEndPointOfServer);
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务器失败，请检查网络连接！", ex.Message);//无法向服务器发送共享列表
            }
        }

        /// <summary>
        /// 使用udpCLientSender发送RequestForListDirectory到另一个客户端
        /// </summary>
        /// <param name="element">对应于要请求的目录的ShareElement</param>
        public void SendRequestForListDirectoryToAClient(ShareElement element)
        {
            if (element.IsFolder)
            {
                string strings = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                strings += "<RequestForListDirectory>";
                strings += "<ShareName>" + element.ShareName.Replace("&", "&amp;").Replace("'", "&apos;") + "</ShareName>";//替换掉XML中的转义字符！！
                strings += "<LocalPath>" + element.LocalPath.Replace("&", "&amp;").Replace("'", "&apos;") + "</LocalPath>";
                strings += "</RequestForListDirectory>";
                byte[] bytes = Encoding.UTF8.GetBytes(strings);
                try
                {
                    udpClientSender.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(element.IP), PORT_UDP_RECV_OTHERUSERINFOS_N_P2P_CMD));
                }
                catch (Exception ex)
                {
                    CToolClass.LogError(ex.Message+"。"+ "向" + element.IP + "发送目录结构请求失败。\n所请求目录：" + element.LocalPath);
                    MessageBox.Show(ex.Message, "向" + element.IP + "发送目录结构请求失败。\n所请求目录：" + element.LocalPath);
                }
            }
            else
            {
                MessageBox.Show("您请求的是文件！");
            }
        }

        /// <summary>
        /// 发送目录结构到特定客户端(P2P)
        /// </summary>
        /// <param name="ip">目标ip</param>
        /// <param name="shareName">共享名/目录名</param>
        /// <param name="path">本地路径</param>
        private void SendResponseOfListDirectoryToAClient(string ip, string shareName, string path)
        {
            string xmlToSend="<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] dirs = di.GetDirectories();
                FileInfo[] files = di.GetFiles();
                //首先获取共享资源的权限属性
                SShareElementAttributes? ssea = CXmlHandle.GetSingleShareElementAttribute(shareName, path);//在根共享目录中查找
                //如果是子文件夹则成了“路径不存在”的情况！应该找到其根共享的权限

                if (ssea == null)//不在根共享中,ssea==null
                {
                    List<SShareElementAttributes?> sseaTmp = new List<SShareElementAttributes?>();//用于临时存储找到的所有符合其根共享路径的
                    foreach (SShareElementAttributes s in CXmlHandle.GetShareElementAttributes())
                    {
                        if (path.Contains(s.LocalPath))//说明可能使其共享根路径
                            sseaTmp.Add(s);
                    }
                    //现在在其共享根路径中找出最长的也即最近的祖先路径(这样应该可以？？)
                    ssea = sseaTmp[0];//首先赋值第一个
                    foreach (SShareElementAttributes? ss in sseaTmp)//然后检查
                    {
                        if (ssea.Value.LocalPath.Length < ss.Value.LocalPath.Length)
                            ssea = ss;//循环，找到最长的那一个
                    }
                }
                if (ssea != null)
                {
                    xmlToSend += "<ResponseOfListDirectory>";
                    //注意XML中的转义字符有5个：<>&'"   但windows允许将其在文件名中使用的有两个：&'
                    //因此需要替换掉
                    foreach (DirectoryInfo dir in dirs)
                    {
                        xmlToSend += "<ShareElement ShareName=\"" + dir.Name.Replace("&", "&amp;").Replace("'", "&apos;") + "\" IsFolder=\"true\" LocalPath=\"" + dir.FullName.Replace("&", "&amp;").Replace("'", "&apos;") + "\" CanDelete=\"" + ssea.Value.CanDelete.ToString()
                        + "\" CanCreate=\"" + ssea.Value.CanCreate.ToString() + "\" Size=\"" + "未知" + "\" Info=\"" + "".Replace("&", "&amp;").Replace("'", "&apos;") + "\" />";
                    }
                    foreach (FileInfo file in files)
                    {
                        xmlToSend += "<ShareElement ShareName=\"" + file.Name.Replace("&", "&amp;").Replace("'", "&apos;") + "\" IsFolder=\"false\" LocalPath=\"" + file.FullName.Replace("&", "&amp;").Replace("'", "&apos;") + "\" CanDelete=\"" + ssea.Value.CanDelete.ToString()
                        + "\" CanCreate=\"" + "false" + "\" Size=\"" + "未知" + "\" Info=\"" + "".Replace("&", "&amp;").Replace("'", "&apos;") + "\" />";
                    }
                    xmlToSend += "</ResponseOfListDirectory>";

                    byte[] bytes = Encoding.UTF8.GetBytes(xmlToSend);

                    try { udpClientSender.Send(bytes, bytes.Length, new IPEndPoint(IPAddress.Parse(ip), PORT_UDP_RECV_OTHERUSERINFOS_N_P2P_CMD)); }
                    catch { CToolClass.LogError("向用户" + ip + "返回目录结构失败！共享名：" + shareName + ";路径：" + path); }
                }
                else//没有找到此路径～
                {
                    CToolClass.LogError("用户" + ip + "向您请求了本地不存在的路径：" + path + ";共享名：" + shareName);
                }
            }
            else
            {
                CToolClass.LogError("用户" + ip + "向您请求了本地不存在的路径：" + path);
            }
        }

        /// <summary>
        /// 载入函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //FrmLogo fl = new FrmLogo();
            //fl.TopMost = true;
            //fl.Show();
                                                                //很奇怪在这里和构造函数里修改在CenterScreen显示为什么无效而只有在设计时修改才成功？
                                                                //this.StartPosition = FormStartPosition.CenterScreen;
                                                                //MessageBox.Show(this.StartPosition.ToString());
                                                                ////this.WindowState = FormWindowState.Maximized;
            //创建一个线程来接收远程主机发来的用户状态信息
            Thread threadUdpReceiver = new Thread(new ThreadStart(ReceiveUserStatusProc));
            threadUdpReceiver.IsBackground = true;
            threadUdpReceiver.Start();
            Thread threadUdpRecv = new Thread(new ThreadStart(ReceiveUserInfosWhenNewOnlineProc));
            threadUdpRecv.IsBackground = true;
            threadUdpRecv.Start();

            //发送本地用户的共享列表到服务器
            Thread threadSendShareList = new Thread(new ThreadStart(SendShareListToServer));
            threadSendShareList.Start();


            //TCP服务器线程
            Thread threadTcpServer = new Thread(new ThreadStart(AcceptTcpClientConnect));
            threadTcpServer.Start();
            threadTcpServer.IsBackground = true;





            //控件精确定位
            dockPanelMain.Location = new Point(0, this.toolStripMain.Location.Y + this.toolStripMain.Height);
            dockPanelMain.Height = this.statusStripMain.Location.Y - dockPanelMain.Location.Y;
            //添加皮肤菜单项
            AddSkinAsMenuItem();
            //skinEngine1.Active = false;//系统默认皮肤

            //Vista renderers------------------------
            //this.menuStripMain.Renderer =
            //    this.toolStripMain.Renderer = new Renderers.WindowsVistaRenderer();
            //dockPanelMain.Location = new Point(0, this.menuStripMain.Location.Y + this.toolStripMain.Location.Y);
//            lock (UserLists)
//            {
//                if (frmDocAllShare.listViewAllShare != null)//控件引用已经引用上了
//                {
//                    while (frmDocAllShare.listViewAllShare.Items.Count == 0)
//                    { frmDocAllShare.PaintAllShareInListView(); }
//                }
//            }
//#warning  test
        }

        private void 异常记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(MainForm.WorkDirectory+"\\exceptions.txt");
        }

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            //路径后退
            string dir = frmDocument.alDir[--frmDocument.CurrentDirIndex].ToString();
            //判断并设置是否禁用前进后退按钮
            if (frmDocument.CurrentDirIndex == 0)//已经到头,禁用
            {
                toolStripButtonBack.Enabled = false;
            }
            else//还没到头
            {
                toolStripButtonBack.Enabled = true;
            }
            if (frmDocument.alDir.Count == frmDocument.CurrentDirIndex + 1)//已经到尾
            {
                toolStripButtonForward.Enabled = false;
            }
            else
            {
                toolStripButtonForward.Enabled = true;
            }
            //--------------进行前进或后退操作---------------------------
            if (dir == "\\")
            {
                frmDocument.PaintListViewWithMyShare();
                return;
            }
            else
                frmDocument.PaintListViewInDirectory(dir);
            frmDocument.pImageComboBox.SelectedIndexChanged -= frmDocument.imageComboBoxShare_SelectedIndexChanged;
            frmDocument.pImageComboBox.Text = dir;
            frmDocument.pImageComboBox.SelectedIndexChanged += frmDocument.imageComboBoxShare_SelectedIndexChanged;
        }

        private void toolStripButtonForward_Click(object sender, EventArgs e)
        {
            ++frmDocument.CurrentDirIndex;
            if (frmDocument.CurrentDirIndex == 0)//已经到头,禁用
            {
                toolStripButtonBack.Enabled = false;
            }
            else//还没到头
            {
                toolStripButtonBack.Enabled = true;
            }
            if (frmDocument.alDir.Count - 1 <= frmDocument.CurrentDirIndex)//当前路径 已经是尾，因此禁用后返回
            {
                toolStripButtonForward.Enabled = false;
                //return;
            }
            else
            {
                toolStripButtonForward.Enabled = true;
            }
            //-----------------
            string dir = frmDocument.alDir[frmDocument.CurrentDirIndex].ToString(); 

            if (dir == "\\")
            {
                frmDocument.PaintListViewWithMyShare();
                return;
            }
            else
                frmDocument.PaintListViewInDirectory(dir);

            frmDocument.pImageComboBox.SelectedIndexChanged -= frmDocument.imageComboBoxShare_SelectedIndexChanged;
            frmDocument.pImageComboBox.Text = dir;
            frmDocument.pImageComboBox.SelectedIndexChanged += frmDocument.imageComboBoxShare_SelectedIndexChanged;
        }

        private void 搜索在线用户UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPublicShare.PanelSearchVisiable = !frmPublicShare.PanelSearchVisiable;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotifyServerLocalUserStatus(false);//通知服务器我要下线
            udpClientReceive.Close();//关闭UDP
            udpClientSender.Close();
            //Tcp部分
            isExit = true;
            allDone.Set();
        }

        private void timerUdpNotifyServer_Tick(object sender, EventArgs e)
        {
            NotifyServerLocalUserStatus(true);
        }

        /// <summary>
        /// vista 效果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vistaTopicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Vista renderers------------------------
            this.menuStripMain.Renderer =
                this.toolStripMain.Renderer = new Renderers.WindowsVistaRenderer();
            dockPanelMain.Location = new Point(0, this.toolStripMain.Location.Y + this.toolStripMain.Height);
            dockPanelMain.Height = this.statusStripMain.Location.Y - this.dockPanelMain.Location.Y;

        }

       //--------------TCP服务器端方法---------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        ///连接TCP客户端方法
        /// </summary>
        private void AcceptTcpClientConnect()
        {
            listener = new TcpListener(PORT_TCP_SERVER_RECEIVE);
            listener.Start();
            while (!isExit)
            {
                //try
                //{
                    //线程同步事件设为非终止，以阻塞其他调用了WaitOne方法的线程
                    allDone.Reset();
                    //开始一个异步操作接受传入的连接尝试
                    listener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientConnectCallback), listener);
                    //阻塞当前线程，直到收到客户端连接信号
                    allDone.WaitOne();
                //}
                //catch (Exception ex)
                //{
                //    CToolClass.LogError(ex.Message);
                //    MessageBox.Show(ex.Message);
                //    break;
                //}
            }
        }
        /// <summary>
        /// 连接客户端方法的回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptTcpClientConnectCallback(IAsyncResult ar)
        {
            //try
            //{
                //线程同步事件设为终止，允许一个或多个等待线程继续
                allDone.Set();
                //获取状态对象TcpListener
                TcpListener myListener = (TcpListener)ar.AsyncState;
                //异步完成接受传入连接，并创建新的TcpClient对象处理远程主机通信
                TcpClient client = myListener.EndAcceptTcpClient(ar);
#warning 添加TcpCLient对象到连接客户端列表
                //开始一个异步读取网络流操作
                ReadWriteObject readObject = new ReadWriteObject(client);
                readObject.networkStream.BeginRead(readObject.readBytes, 0, readObject.readBytes.Length, new AsyncCallback(ReadCallback), readObject);
            //}
            //catch (Exception ex)
            //{
            //    CToolClass.LogError(ex.Message);
            //    MessageBox.Show(ex.Message);
            //}
        }
        /// <summary>
        /// 读取网络流操作回调方法
        /// </summary>
        /// <param name="ar"></param>
        private void ReadCallback(IAsyncResult ar)
        {
            //try
            //{
                ReadWriteObject readObject = (ReadWriteObject)ar.AsyncState;
                //结束异步读取网络流操作
                int count = readObject.networkStream.EndRead(ar);
                string stringReceived = Encoding.UTF8.GetString(readObject.readBytes, 0, count);
                if (stringReceived.StartsWith("FILEREQUEST:"))
                {
                    //客户端所请求的文件路径
                    string fileName = stringReceived.Substring("FILEREQUEST:".Length);
                    //读取此文件并发送到客户端
                    //MessageBox.Show("FILEREQUEST:" + fileName);//test only
                    SendFileLength(fileName, readObject.client);
#warning 这里也用单独一个线程试试还卡否
                }
                else if (stringReceived.StartsWith("FOLDERREQUEST:"))
                {
                    //客户端所请求的文件夹路径
                    string fileName = stringReceived.Substring("FOLDERREQUEST:".Length);
                    //读取此文件夹并发送到客户端
                    MessageBox.Show("FOLDERREQUEST:" + fileName);//test only
                }
                else if (stringReceived.StartsWith("STOPREQUEST"))
                {
                    MessageBox.Show("STOPREQUEST");//test only
                }
                else
                {
                    MessageBox.Show("其他指令！！");//test only
                }
            //}
            //catch (Exception ex)
            //{
            //    CToolClass.LogError(ex.Message);
            //    MessageBox.Show(ex.Message);
            //}
        }
        private void SendFileLength(string fileName,TcpClient client)
        {
#warning 目前只能传输文件而不能传输文件夹
            if (!File.Exists(fileName)) { return; }
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            //ReadWriteObject writeObject=new ReadWriteObject(
            //byte[] fileBuffer = new byte[fileStream.Length];
            StateReadNSend stateRNS = new StateReadNSend(client,fileStream);
            byte[] fileLength = Encoding.UTF8.GetBytes("FILELENGTH:" + stateRNS.FileLength.ToString());
            client.GetStream().BeginWrite(fileLength, 0, fileLength.Length, new AsyncCallback(SendFileLengthCallback), stateRNS);
            //int read; 
            //read = fileStream.Read(fileBuffer, 0, fileBuffer.Length);            
        }
        private void SendFileLengthCallback(IAsyncResult ar)//先发送文件大小，然后发送文件数据
        {
            StateReadNSend stateRNS = (StateReadNSend)ar.AsyncState;
            stateRNS.networkStream.EndWrite(ar);//结束发送文件大小的异步操作
            //开始一个读取文件流的异步操作
            stateRNS.fileStream.BeginRead(stateRNS.fileBuffer, 0, stateRNS.fileBuffer.Length, new AsyncCallback(ReadFileStreamCallback), stateRNS);
        }

        private void ReadFileStreamCallback(IAsyncResult ar)
        {
            StateReadNSend stateRNS = (StateReadNSend)ar.AsyncState;
            try
            {
                int read = stateRNS.fileStream.EndRead(ar);//结束一次读取文件流的异步操作//read为实际读取的字节数
                stateRNS.networkStream.BeginWrite(stateRNS.fileBuffer, 0, read, new AsyncCallback(SendCallback), stateRNS);//开始一个异步操作，发送读取的字节
#warning "无法将数据写入传输连接: 远程主机强迫关闭了一个现有的连接。。"
            }
            catch 
            {
                stateRNS.fileStream.Close();
                stateRNS.networkStream.Close();
                return;
            }
        }
        private void SendCallback(IAsyncResult ar)
        {
            StateReadNSend stateRNS = (StateReadNSend)ar.AsyncState;
            try { stateRNS.networkStream.EndWrite(ar); }//结束一次异步发送操作
            catch//写入网络流发生异常，如果是客户端终止传输则关闭一切，但如果是其他就坏了～
            {
                stateRNS.networkStream.Close();
                stateRNS.fileStream.Close();
                return;
            }
            //傳輸文件完畢，記得主動關閉連接（如果不想保持這個連接），否則，等服務器端的線程執行完畢，客戶端會拋出“遠程主機強迫關閉了現有的連接”
            //(此為試驗所得解決辦法，不確定絕對正確)
            //networkStream.Close();
#warning 如果客户端终止传输，则“无法将数据写入传输连接: 远程主机强迫关闭了一个现有的连接。。”异常
            if (stateRNS.fileStream.Length != stateRNS.fileStream.Position)//没有到文件流的末尾
            {
                stateRNS.fileStream.BeginRead(stateRNS.fileBuffer, 0, stateRNS.fileBuffer.Length, new AsyncCallback(ReadFileStreamCallback), stateRNS);
            }
            else
            {
                //MessageBox.Show("stateRNS.fileStream.Length == stateRNS.fileStream.Position");//test only
                stateRNS.networkStream.Close();
                stateRNS.fileStream.Close();
            }
        }

        private void 系统默认DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinEngine1.Active = false;//设置为系统默认皮肤
            //取消选中其他
            foreach (ToolStripMenuItem item in 皮肤ToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;//只要点击此项就选中
            //手动涂色为系统默认
            foreach (Form frm in Application.OpenForms)
            {
                frm.BackColor = SystemColors.Control;
                //menuStripMain.BackColor = toolStripMain.BackColor = SystemColors.MenuBar;
#warning 第三方皮肤组件导致菜单栏的默认皮肤样式背景色不是蓝的了！
            }
        }
        private void skinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            skinEngine1.SkinFile = ((ToolStripMenuItem)sender).Tag.ToString();
            skinEngine1.Active = true;
            //取消选中其他
            foreach (ToolStripMenuItem item in 皮肤ToolStripMenuItem.DropDownItems)
            {
                item.Checked = false;
            }
            ((ToolStripMenuItem)sender).Checked = true;//只要点击此项就选中
            //frmPersonalProfile.BackColor = frmPersonalProfile.Controls[0].BackColor;//手动设置此窗体的背景色
            foreach (Form frm in Application.OpenForms)
            {
                try { frm.BackColor = frmPersonalProfile.Controls[0].BackColor; }
                catch { }
                //exception:控件不支持透明的背景色。
            }
        }
        /// <summary>
        /// 在系统启动时候,获取所有皮肤文件并添加作为菜单项
        /// </summary>
        private void AddSkinAsMenuItem()
        {
            string path = MainForm.WorkDirectory + "\\skin";
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.ssk");//返回ssk皮肤文件列表
            ToolStripMenuItem item;
            foreach (FileInfo file in files)
            {
                item = new ToolStripMenuItem(file.Name.Replace(".ssk",""), null, new EventHandler(skinToolStripMenuItem_Click));
                item.Tag = file.FullName;
                皮肤ToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void 配置OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOptions fo = new FrmOptions();
            fo.ShowDialog();
        }

        private void 网络资源浏览器NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeifenLuo.WinFormsUI.Docking.IDockContent content = MainForm.PMainForm.FindDocument("网络资源浏览器");

            if (content == null)//该文档窗口已经被关闭了
            {
                frmDocAllShare = new FormDocumentAllShare();
                frmDocAllShare.DockAreas = DockAreas.Document;
                frmDocAllShare.Show(MainForm.PMainForm.dockPanelMain);
            }
            else//没有关闭
            {
                frmDocAllShare = (FormDocumentAllShare)content.DockHandler.Form;
            }

            frmDocAllShare.Activate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmOptions fo = new FrmOptions();
            fo.Show();
        }
// 发送的数据会多出几K，虽然不影响文件的读取，当仍然说明过程存在问题->>已经解决：设置发送读取的文件字节数为实际读取的字节数而不是fileBuffer.Length

    }
}
/*
 页面布局要有保存功能（已经提供）
 * 开始运行时“查看”菜单要从保存的里面读取出来保持一致
 * 
 * “查看”菜单中之项目，当关闭子窗体时应能取消选中
 * 
 * MainForm开始时要检查MyShare.xml是否存在，如不存在则建之
 */