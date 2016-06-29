using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Threading;

namespace HiveServer
{
    /// <summary>用户状态信息。用于服务器把某个用户的所有信息（状态信息&共享列表）发送到某个客户端时的数据结构体;
    /// </summary>
    struct UserInfo
    {
        public string IP;
        public string Name;
        public string StatusInfo;
        public String ShareListXml;
    }
    /// <summary>
    /// 服务器主窗体类
    /// </summary>
    public partial class MainForm : Form
    {
        //网络方面的字段-----------------------
        /// <summary>
        /// UDP客户端使用的接收端口8899,亦即本服务器要发送到的目标端口
        /// </summary>
        private const int PORT_UDP_CLIENT_RECEIVE = 8899;
        /// <summary>
        /// 端口9999：用于客户端接收本服务器单播到客户端上的在此客户端上线之前就已经上线的其他用户的状态信息
        /// <para>与服务器上的要发送到的目标端口一致</para>
        /// </summary>
        private const int PORT_UDP_CLIENT_RECV_OTHERUSERINFOS = 9999;
        /// <summary>
        /// 本服务器使用的UDP接收端口
        /// </summary>
        private const int PORT_UDP_SERVER_RECEIVE = 8890;
        /// <summary>
        /// UDP接收对象（from client）
        /// </summary>
        private UdpClient udpClientReceive;

        /// <summary>
        /// 组播地址224.0.0.9
        /// </summary>
        private const string MULTICAST_IP = "224.0.0.9";


        static public MainForm pMainForm;
        ///////////////////////////////
        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            pMainForm = this;
        }

        /// <summary>
        /// 在后台运行的接收线程 
        /// </summary>
        private void ReceiveUserStatusProc()
        {
            udpClientReceive = new UdpClient(PORT_UDP_SERVER_RECEIVE);            
            IPEndPoint remoteIPEndPoint = null;

            string ip, name, statusInfo;
            bool isOnline;
            ip = name = statusInfo = null;
            isOnline = false;//赋初值

            while (true)
            {
                try
                {
                    //关闭udpclient时，此句会产生异常
                    byte[] bytes = udpClientReceive.Receive(ref remoteIPEndPoint);
                    string stringReceived = Encoding.UTF8.GetString(bytes);
                    ip = remoteIPEndPoint.Address.ToString();
                    //------------------/接受客户端上线请求/保存单个用户的资源列表
                    CXmlHandler xmlHandler = new CXmlHandler(stringReceived);
                    if (xmlHandler.CheckIsUserStatusXml())//如果是上线下线通知
                    {
                        xmlHandler.GetUserStatusContent(ref name, ref statusInfo, ref isOnline);

                        //自此，ip, name, statusInfo, isOnline全部获得
                        //判断isOnline，作出是输入数据库还是在数据库中删除的反应---------------
                        CDatabaseHandler dbh = new CDatabaseHandler();
                        if (isOnline)
                        {
                            //查看是否此用户是新的或者修改过的，如是，则组播之到其他用户
                            if (dbh.NewOrModifiedUser(ip, name, statusInfo))
                            {
                                //将这些信息输入到数据库中去
                                dbh.SetOnlineUser(ip, name, statusInfo, DateTime.Now.ToString());
                                UserInfo ui = dbh.GetSingleUserInfo(ip).Value;
                                SendUserInfoToUsers(ui, MULTICAST_IP, true);//发送此用户信息到其他用户
                                SendUserInfosToSingleUser(dbh.GetAllOnlineUsersUserInfo(), ip, true);//发送其他用户信息给此用户
                            }
                        }
                        else
                        {
                            dbh.DeleteOfflineUser(ip);//下线，删除用户
#warning【未完成】//组播用户下线信息
                        }
                    }
                    else //那么就是共享列表了
                    {
                        CDatabaseHandler dbh = new CDatabaseHandler();
                       
                        //查看是否新的或修改过的
                        if (dbh.NewOrModifiedShareList(ip, stringReceived))
                        {
                            dbh.SetUserShareList(ip, stringReceived);
                            UserInfo ui = dbh.GetSingleUserInfo(ip).Value;
                            SendUserInfoToUsers(ui, MULTICAST_IP, false);
                            SendUserInfosToSingleUser(dbh.GetAllOnlineUsersUserInfo(), ip, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message,"1");//!!!!test only,it should be removed when this project is done.
                    break;//退出循环
                }
            }
        }

        /// <summary>
        /// 组播发送某用户的UserInfo或sharelist到其他所有用户
        /// </summary>
        /// <param name="userInfo">UserInfo结构</param>
        /// <param name="MulticastIP">组播地址，应该为MULTICAST_IP  ( = "224.0.0.9")</param>
        /// <param name="sendStatusInfo">发送UserInfo则为true，发送sharelist则为false</param>
        private void SendUserInfoToUsers(UserInfo userInfo, string MulticastIP,bool sendStatusInfo)
        {
            try
            {
                string userStatus = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                userStatus += "<UdpUser><Ip>";
                userStatus += userInfo.IP;
                userStatus += "</Ip><Name>";
                userStatus += userInfo.Name;
                userStatus += "</Name><StatusInfo>";
                userStatus += userInfo.StatusInfo;
                userStatus += "</StatusInfo><IsOnline>";
                userStatus += "true";
                userStatus += "</IsOnline></UdpUser>";
                string shareList = userInfo.ShareListXml;
                byte[] bytes;
                using (UdpClient udpClient = new UdpClient())//使用完关闭
                {
                    udpClient.EnableBroadcast = true;

                    IPEndPoint remote = new IPEndPoint(IPAddress.Parse(MulticastIP), PORT_UDP_CLIENT_RECEIVE);//组播终节点
                    if (sendStatusInfo)//发送UserInfo
                    {
                        bytes = System.Text.Encoding.UTF8.GetBytes(userStatus);
                        udpClient.Send(bytes, bytes.Length, remote);
                    }
                    else//发送sharelist
                    {
                        //添加用户IP节点
                        shareList = shareList.Insert(shareList.IndexOf("<MyShare>") + "<MyShare>".Length, "<IP>" + userInfo.IP + "</IP>");
                        bytes = System.Text.Encoding.UTF8.GetBytes(shareList);
                        udpClient.Send(bytes, bytes.Length, remote);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"2");//一次性发送
            }
        }

// [确认正确解决方法]如果下面的函数没有执行，或者执行后没有被客户端收到，可能的原因是客户端的PORT_UDP_CLIENT_RECEIVE端口被用来组播了，而本方法是单播。换个端口试试
        /// <summary>
        /// 发送其他所有用户的UserInfo和sharelist到某用户
        /// </summary>
        /// <param name="userInfos"></param>
        /// <param name="targetIP"></param>
        private void SendUserInfosToSingleUser(UserInfo[] userInfos, string targetIP,bool sendStatusInfos)
        {
            foreach (UserInfo userInfo in userInfos)
            {
                try
                {
                    string userStatus = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
                    userStatus += "<UdpUser><Ip>";
                    userStatus += userInfo.IP;
                    userStatus += "</Ip><Name>";
                    userStatus += userInfo.Name;
                    userStatus += "</Name><StatusInfo>";
                    userStatus += userInfo.StatusInfo;
                    userStatus += "</StatusInfo><IsOnline>";
                    userStatus += "true";
                    userStatus += "</IsOnline></UdpUser>";
                    string shareList = userInfo.ShareListXml;
                    byte[] bytes;
                    using (UdpClient udpClient = new UdpClient())//发送对象，使用完关闭
                    {
                        IPEndPoint remote = new IPEndPoint(IPAddress.Parse(targetIP), PORT_UDP_CLIENT_RECV_OTHERUSERINFOS);//组播终节点
                        if (sendStatusInfos)
                        {
                            bytes = System.Text.Encoding.UTF8.GetBytes(userStatus);
                            udpClient.Send(bytes, bytes.Length, remote);
                        }
                        else
                        {
                            shareList = shareList.Insert(shareList.IndexOf("<MyShare>") + "<MyShare>".Length, "<IP>" + userInfo.IP + "</IP>");
                            bytes = System.Text.Encoding.UTF8.GetBytes(shareList);
                            udpClient.Send(bytes, bytes.Length, remote);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "3");//一次性发送
                }
            }
        }




        private void MainForm_Load(object sender, EventArgs e)
        {
            //接收线程
            Thread threadReceive = new Thread(new ThreadStart(ReceiveUserStatusProc));
            threadReceive.IsBackground = true;
            threadReceive.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (udpClientReceive != null) udpClientReceive.Close();
        }

        private void timerDeleteAbnormalOfflineUsers_Tick(object sender, EventArgs e)
        {
            CDatabaseHandler cdh = new CDatabaseHandler();
            cdh.UserOfflineWithoutNotification();
        }

        /// <summary>
        /// 定时刷新在线列表,并显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerUpdateGridView_Tick(object sender, EventArgs e)
        {
            DataSet ds;
            try
            {
                CDatabaseHandler cdh = new CDatabaseHandler();
                ds = cdh.GetDataSetToBindControl();
                //ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                this.dataGridViewMain.DataSource = ds.Tables[0];
                ds.Dispose();
            }
            catch(Exception ex)
            {
                //timerUpdateGridView.Stop();

                //if (MessageBox.Show("在连接 SQL server 数据库服务器发生错误！\n请确保成功启动 SQL Server 后再点击'重试',退出请点击'取消'。", "错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error)
                //    == DialogResult.Retry)
                //{
                //    CDatabaseHandler cdh = new CDatabaseHandler();
                //    ds = cdh.GetDataSetToBindControl();
                //    this.dataGridViewMain.DataSource = ds.Tables[0];
                //    ds.Dispose();
                //}
                //else
                //{
                //MessageBox.Show(ex.Message,"timerUpdateGridView");
                Application.Exit();
#warning 在把其滚动条滚向右端的时候，会发生异常，似乎是dataGridView的问题
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DBMS:SQL Server\n"
            + "Database Name:HiveServer\n"
            + "Data Table Name:ClientUsers");
        }

        ///// <summary>
        ///// 刷新DataGridView中数据显示，即重新从数据库中提取数据并绑定。
        ///// </summary>
        //private void UpdateDataGridView()
        //{
        //    CDatabaseHandler cdh = new CDatabaseHandler();
        //    DataSet ds = cdh.GetDataSetToBindControl();
        //    this.dataGridViewMain.DataSource = ds.Tables[0];
        //}
    }
}