using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Fm = System.Windows.Forms;

namespace Hive
{
    /// <summary>用户个人信息类
    /// </summary>
    class CUserInfo
    {
        public string Name = "";
        public string StatusInfo = "";
        public int Shared = 0;
        public int Downloaded = 0;
        /// <summary>在此构造函数中载入User.xml，并赋值给代表个人信息的4个成员变量。
        /// <para>使用流式机制，一次性获取PersonalProfile的所有内容</para>
        /// </summary>
        public CUserInfo()
        {
            using (XmlReader reader = XmlReader.Create(MainForm.WorkDirectory + "\\" + "User.xml"))
            {
                try
                {
                    if (reader.ReadToDescendant("Name"))
                        Name = reader.ReadElementString();

                    if (reader.ReadToNextSibling("StatusInfo"))
                        StatusInfo = reader.ReadElementString();

                    if (reader.ReadToNextSibling("Shared"))//应该首先从MyShare.xml中读取
                        Shared = int.Parse(reader.ReadElementString("Shared"));//可以检查一下

                    if (reader.ReadToNextSibling("Downloaded"))//应该从下载历史中读取
                        Downloaded = int.Parse(reader.ReadElementString("Downloaded"));
                }
                catch (Exception ex)
                {
                    //日志
                    CToolClass.LogError(ex.Message);
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 修改用户状态信息xml项。
        /// <para>如果输入某参数为null,则表示不修改此参数对应在xml中的项</para>
        /// 在使用时必须把可能引起数据库输入语句歧义的字符如'替换掉！（重要）—!—!
        /// </summary>
        /// <param name="name"></param>
        /// <param name="statusInfo"></param>
        public static void ModifyUserStatusInfo(string name, string statusInfo)
        {
            XmlDocument document = new XmlDocument();
            document.Load(MainForm.WorkDirectory + "\\" + "User.xml");
            XmlNode root = document.DocumentElement;
            if (root.ChildNodes.Count > 0 && root.Name == "User")
            {
                XmlNode node = root.FirstChild;
                if (node.Name == "PersonalProfile")
                {
                    node = node.FirstChild;//Name
                    if (name != null) node.InnerText = name;
                    node = node.NextSibling;//StatusInfo
                    if (statusInfo != null) node.InnerText = statusInfo;

                    document.Save(MainForm.WorkDirectory + "\\" + "User.xml");

                    document = null;//释放xml文件
                }
                else
                    Fm.MessageBox.Show("无法修改本地状态信息文件！\n2");
            }
            else
            {
                Fm.MessageBox.Show("无法修改本地状态信息文件！\n1");
            }
        }
    }

    class CUserSettings
    {

    }
        
}
