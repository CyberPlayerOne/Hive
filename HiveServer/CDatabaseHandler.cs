using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace HiveServer
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    class CDatabaseHandler
    { 
        //-DataBase-----------------------------
        private SqlConnection connection;
        /// <summary>
        /// CommandText要在使用它时初始化
        /// </summary>
        private SqlCommand command;
        private SqlDataReader dataReader;

        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CDatabaseHandler()
        {
            connection = new SqlConnection(@"Data Source=.;Initial Catalog=hiveserver;User ID=sa;Password=sa");
            command = connection.CreateCommand();            
        }

#warning 目前此方法(定时删除非正常下线用户)无效！！！
        /// <summary>
        /// 定时60秒执行一次，用于删除已下线但未通知服务器的用户在数据库中的记录。
        /// <seealso cref="DeleteOfflineUser(string)"/>
        /// </summary>
        /// <returns>当次执行删除的记录数</returns>
        public int UserOfflineWithoutNotification()
        {
            try
            {
                //待删除的IP列表
                System.Collections.Generic.List<string> deleteList = new List<string>();
                int sum = 0;//被删除的非正常下线者技术
                //首先要查询用户设置在线时间，与当前时间作减法，看其绝对值是否大于1分钟（客户端定时30秒通知服务器端他在线）
                command.CommandText = "select ip,LastSetOnlineTime from ClientUsers";
                connection.Open();

                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    //System.Windows.Forms.MessageBox.Show(dataReader[0].ToString() + "<>" + dataReader[1].ToString());
                    DateTime dateTimeLast = DateTime.Parse(dataReader[1].ToString());//exception:该字符串未被识别为有效的 DateTime。
                    DateTime now = DateTime.Now;
                    MainForm.pMainForm.Text = "HivE server [" + now.ToString() + "]";
                    TimeSpan span = now - dateTimeLast;
                    if (span.TotalSeconds > 60)//已经超过60秒钟没有通知服务器他在线了，说明他已经非正常下线{60sec==1min!!!慢}
                    {
                        //SqlCommand command2 = new SqlCommand(null, this.command.Connection);
                        //command2.CommandText = "delete from ClientUsers where ip=\'" + dataReader[0].ToString() + "\'";                    
                        //command2.ExecuteNonQuery();//删除了非正常下线者///exception:已有打开的与此命令相关联的 DataReader，必须首先将它关闭。
                        sum++;
                        deleteList.Add(dataReader[0].ToString());
                    }
                }
                dataReader.Close();//为执行新命令先关闭dataReader或新建DataCommand
                //开始删除deleteList里面的ip用户
                if (deleteList.Count > 0)
                {
                    command.CommandText = "delete from ClientUsers where ip in ("; //未完
                    foreach (string ip in deleteList)
                    {
                        command.CommandText += "\'" + ip + "\',";
                    }
                    command.CommandText = command.CommandText.Remove(command.CommandText.Length - 1);//去掉,
                    command.CommandText += ")";
                }
                command.ExecuteNonQuery();//return 这个也可
                connection.Close();
                return sum;
            }
            catch 
            {
                return 0;   
            }
        }

        /// <summary>
        /// 输入用户上线数据到数据库中。(每次接收到UDP都输入--》这要看客户端何时发送：上线、timer定时、修改后)
        /// <para>此用户如果是新上线用户，则此函数为输入操作；如果此用户为已上线用户，则此函数为修改操作。</para>
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="name">用户名</param>
        /// <param name="statusInfo">状态</param>
        /// <param name="LastSetOnlineTime">上次上线时间</param>
        public void SetOnlineUser(string ip,string name,string statusInfo,string lastSetOnlineTime)
        {            
            connection.Open();
            command.CommandText = "select count(*) from clientUsers where ip=\'" + ip + "\'";
            int count = (int)command.ExecuteScalar();
            if (count != 0)//已经有他的记录了，说明是早前已经上线的用户；故应修改数据
            {
                command.CommandText = "update clientUsers set name=\'" + name + "\',statusInfo=\'" + statusInfo + "\',LastSetOnlineTime=\'" + lastSetOnlineTime + "\' where ip=\'" + ip + "\'";
                if (command.ExecuteNonQuery() != 1)
                    System.Windows.Forms.MessageBox.Show("更新用户状态数据失败！\n用户名:" + name + ",IP:" + ip);
            }
            else//新用户，输入新数据
            {
                command.CommandText = "insert into ClientUsers(ip,name,statusInfo,lastSetOnlineTime) values(\'" + ip + "\',\'" + name + "\',\'" + statusInfo + "\',\'" + lastSetOnlineTime + "\')";
                if (command.ExecuteNonQuery() != 1)
                    System.Windows.Forms.MessageBox.Show("输入新上线用户的状态数据失败！");
            }
            connection.Close();
        }

        /// <summary>
        /// 输入接收到的共享列表到对应用户的共享列表字段        
        /// <para>新上线用户和修改了共享列表的已上线用户会发送其共享列表到服务器而调用此方法(在客户端)</para>
        /// 一种可能不会发生的情况：
        /// <para>如果在没有收到此用户的上线状态信息的前提下就收到了他的共享列表,则也将其共享列表输入，同时其他字段空着</para>
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="shareListXml"></param>
        public void SetUserShareList(string ip, string shareListXml)
        {
            if (shareListXml == string.Empty) System.Windows.Forms.MessageBox.Show("sharelistxml为空！");//test only
            connection.Open();
            command.CommandText = "select count(*) from clientUsers where ip=\'" + ip + "\'";
            int count = (int)command.ExecuteScalar();
            if (count != 0)//已经有他的记录了，说明是早前已经上线的用户；故应修改数据
            {
                command.CommandText = "update clientUsers set shareListXml=\'" + shareListXml + "\' where ip=\'" + ip + "\'";
                if (command.ExecuteNonQuery() != 1)
                    System.Windows.Forms.MessageBox.Show("修改用户共享列表数据失败！\n用户IP:" + ip);
            }
            else//此欲上线用户还没有给本服务器发送上线状态信息；故应输入数据
            {
                command.CommandText = "insert into clientUsers(ip,shareListXml) values(\'" + ip + "\',\'" + shareListXml + "\')";
                if (command.ExecuteNonQuery() != 1)
                {
                    System.Windows.Forms.MessageBox.Show("输入用户共享列表数据失败！\n用户IP:" + ip);
                }
            }
            connection.Close();
        }

        /// <summary>
        /// 下线用户通知服务器，我要下线时，此方法被服务器调用，以删除此用户在数据库中的记录
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>是否找到并删除成功</returns>
        public bool DeleteOfflineUser(string ip)
        {
            connection.Open();
            command.CommandText = "delete from ClientUsers where ip=\'" + ip + "\'";
            if (command.ExecuteNonQuery() == 1)
            {
                connection.Close();
                return true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("用户下线时，服务器删除用户共享列表数据失败！\n用户IP:" + ip);
                connection.Close();
                return false;
            }
        }

        /// <summary>
        ///获取所有在线用户的UserInfo组成的数组,以备于发送。
        /// <para>用于：每当有新上线用户时执行，发送所有在线用户信息（包括状态信息和共享列表）给他</para>
        /// </summary>
        public UserInfo[] GetAllOnlineUsersUserInfo()
        {
            dataAdapter = new SqlDataAdapter("select ip,name,statusInfo,shareListXml from clientusers", connection);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "ClientUsers");
            int count = dataSet.Tables[0].Rows.Count;//在线人数
            UserInfo[] userInfos = new UserInfo[count];
            for (int i = 0; i < count; i++)
            {
                userInfos[i].IP = dataSet.Tables[0].Rows[i][0].ToString();
                userInfos[i].Name = dataSet.Tables[0].Rows[i][1].ToString();
                userInfos[i].StatusInfo = dataSet.Tables[0].Rows[i][2].ToString();
                userInfos[i].ShareListXml = dataSet.Tables[0].Rows[i][3].ToString();
            }
            return userInfos;
        }
        /// <summary>
        /// 获取特定用户的UserInfo( 如果此用户不存在，则返回的UserInfo包含字段皆空)
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public UserInfo? GetSingleUserInfo(string ip)
        {
            command.CommandText = "select count(*) from clientusers where ip=\'" + ip + "\'";
            if (connection.State != ConnectionState.Open)
                connection.Open();
            int i = (int)command.ExecuteScalar();
            if (i == 0)
            {
                connection.Close();//关闭连接
                return null;//没有此用户之记录                
            }
            command.CommandText = "select ip,name,statusInfo,shareListXml from clientusers where ip=\'" + ip + "\'";
            dataReader = command.ExecuteReader();
            dataReader.Read();//前进到第一行～！！！
            UserInfo userInfo;
            userInfo.IP = dataReader[0].ToString();
            userInfo.Name = dataReader[1].ToString();
            userInfo.StatusInfo = dataReader[2].ToString();
            userInfo.ShareListXml = dataReader[3].ToString().Equals(null) ? "" : dataReader[3].ToString();
            connection.Close();
            return userInfo;
        }

        /// <summary>
        ///  定时执行，获取所有在线用户的所有信息以在主窗体控件中显示；
        /// <para>在主窗体显示</para>
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSetToBindControl()
        {
            dataSet = new DataSet();
            dataAdapter = new SqlDataAdapter("select ip,name,statusInfo,lastsetonlinetime,shareListXml from clientusers", connection);
            dataAdapter.Fill(dataSet, "ClientUsers");
            return dataSet;
        }

        /// <summary>
        /// 是否新或修改过的用户信息
        /// 是新的则返回true，否则返回false
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="shareListXml"></param>
        /// <returns></returns>
        public bool NewOrModifiedUser(string ip,string name,string statusInfo)
        {
            UserInfo? ui = GetSingleUserInfo(ip);
            if (ui == null) return true;//新的
            UserInfo userInfo = ui.Value;
            if (userInfo.Name != name || userInfo.StatusInfo != statusInfo)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 是否新或修改过的共享列表
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="shareListXml"></param>
        /// <returns></returns>
        public bool NewOrModifiedShareList(string ip, string shareListXml)
        {
            UserInfo? ui = GetSingleUserInfo(ip);
            if (ui == null) return true;//新的
            UserInfo userInfo = ui.Value;
            if (userInfo.ShareListXml != shareListXml)
                return true;
            else
                return false;
        }
    }
}
