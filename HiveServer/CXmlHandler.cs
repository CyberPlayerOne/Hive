using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace HiveServer
{
    class CXmlHandler:IDisposable
    {
       /// <summary>
       /// XmlDocument
       /// </summary>
        XmlDocument document;
        /// <summary>
        /// XML文档根节点
        /// </summary>
        XmlNode rootNode;
        /// <summary>
        /// 构造函数，使用XML格式字符串作参数
        /// <para>初始化document和rootNode</para>
        /// </summary>
        /// <param name="xmlString"></param>
        public CXmlHandler(string xmlString)
        {
            document = new XmlDocument();
            document.LoadXml(xmlString);
            rootNode = document.DocumentElement;
        }

        /// <summary>
        /// 检查是否是用户上线下线，应该在构造函数之后执行
        /// </summary>
        /// <returns></returns>
        public bool CheckIsUserStatusXml()
        {
            if (rootNode.Name == "UdpUser")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取用户状态信息
        /// <para>之前必须调用CheckIsUserStatusXml方法以确定这个XML是状态信息的</para>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="statusInfo"></param>
        /// <param name="isOnline"></param>
        public void GetUserStatusContent(ref string name, ref string statusInfo, ref bool isOnline)
        {
            XmlNodeList nodes = rootNode.ChildNodes;
            foreach (XmlNode node in nodes)
            {
                if (node.Name == "Name")
                {
                    name = node.InnerText;
                    continue;
                }
                if (node.Name == "StatusInfo")
                {
                    statusInfo = node.InnerText;
                    continue;
                }
                if (node.Name == "IsOnline")
                {
                    isOnline = bool.Parse(node.InnerText);
                    continue;
                }
            }
        }




        #region IDisposable 成员
        /// <summary>
        /// dispose XmlDocument;实际并没有使用～
        /// </summary>
        void IDisposable.Dispose()
        {
            document = null;
            rootNode = null;
        }

        #endregion
    }
}
