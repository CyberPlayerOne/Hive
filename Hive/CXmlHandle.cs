using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Hive
{
    /// <summary>
    /// 处理MyShare.XML文件的类，包含各种用于处理共享资源（每个作为一个节点存在于XML中）的操作
    /// </summary>
    static class CXmlHandle
    {
        /// <summary>
        /// XmlFile为MyShare.xml文件路径
        /// </summary>
        private static string xmlFile;
        private static XmlDocument xmlDocument;
        private static XmlElement xmlRootElement;//<MyShare>
        
        

        /// <summary>打开默认路径上的MyShare.xml;初始化此类所有字段；
        /// Load()和Save()皆私有成员，在其他私有成员函数里面调用
        /// </summary>
        private static void Load()
        {
            xmlDocument = new XmlDocument();

            xmlFile = MainForm.WorkDirectory + "\\" + "MyShare.xml";
            try
            {
                xmlDocument.Load(xmlFile);
            }
            catch (XmlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }

            xmlRootElement = xmlDocument.DocumentElement;
            //字段初始化完毕
        }
        /// <summary> 保存到XML文件中去
        /// </summary>
        private static void Save()
        {
            try
            {
                xmlDocument.Save(xmlFile);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>使用与结构体SShareElementAttributes的成员相同的参数。建议所有参数根据新LocalPath路径获得，而不依赖于以前的值。
        /// <remarks>
        /// 举例：
        /// <code>
        ///CXmlHandle.ModifyShareElement(
        ///        labelShareName.Text,
        ///        textBoxShareName.Text,
        ///        System.IO.Directory.Exists(textBoxPath.Text),
        ///        textBoxPath.Text,
        ///        checkBoxCanDelete.Checked,
        ///        checkBoxCanCreate.Checked,
        ///        System.IO.Directory.Exists(textBoxPath.Text) ? CToolClass.GetSizeOfFolder(textBoxPath.Text) : (new System.IO.FileInfo(textBoxPath.Text)).Length.ToString(),//!!!!!!!!!!!!!!!!!!!!!!!!!!!!文件夹大小计算方法需要单独线程否则会阻塞
        ///        textBoxInfo.Text);
        /// </code>
        /// </remarks>
        /// </summary>
        /// <param name="OldShareName">原来的共享名！用于在XML中找到这个节点！！重要！！</param>
        /// <param name="ShareName">新共享名</param>
        /// <param name="IsFolder">是否为文件夹</param>
        /// <param name="LocalPath">本地路径</param>
        /// <param name="CanDelete">是否可由访问者删除</param>
        /// <param name="CanCreate">可否由访问者在其中建立文件；如果是文件必须为假</param>
        /// <param name="Size">大小</param>
        /// <param name="Info">其他相关信息</param>
        public static void ModifyShareElement(string OldShareName,string ShareName,bool IsFolder,string LocalPath,bool CanDelete,bool CanCreate,string Size,string Info)
        {
            Load();
            //
            if (!IsFolder) CanCreate = false;
            XmlNode oldNode = GetXmlNodeByShareName(OldShareName);//此node是复制的还是引用的？???应该是引用的
            if (oldNode == null) return;
            //直接修改属性
            oldNode.Attributes["ShareName"].Value = ShareName;
            oldNode.Attributes["IsFolder"].Value = IsFolder.ToString();
            oldNode.Attributes["LocalPath"].Value = LocalPath;
            oldNode.Attributes["CanDelete"].Value = CanDelete.ToString();
            oldNode.Attributes["CanCreate"].Value = CanCreate.ToString();
            oldNode.Attributes["Size"].Value = Size;
            oldNode.Attributes["Info"].Value = Info;//这样应该行了。。。。不用xmlDocument.ReplaceChild()了
            
            //保存
            Save();
        }
        /// <summary>根据ShareName参数删除指定共享（在XML中删除一个ShareElement节点）
        /// </summary>
        /// <param name="ShareName">共享名</param>
        public static void DeleteShareElement(string ShareName)
        {
            Load();
            //找到XmlNode
            XmlNode oldXmlNode = GetXmlNodeByShareName(ShareName);
            if (oldXmlNode != null)
                try { xmlRootElement.RemoveChild(oldXmlNode); }
                catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
            //
            Save();
        }
        /// <summary>根据ShareName返回XmlNode
        /// </summary>
        /// <param name="ShareName"></param>
        /// <returns></returns>
        private static XmlNode GetXmlNodeByShareName(string ShareName)
        {
            Load();
            XmlNodeList xmlNodeList = xmlRootElement.GetElementsByTagName("ShareElement");
            foreach (XmlNode node in xmlNodeList)
            {
                if (node.Attributes["ShareName"].Value == ShareName)
                {
                    return node;
                }                
            }
            Save();
            return null;
        }

        /// <summary>添加新节点(共享资源)到末尾
        /// <para> 已经自动调用Load和Save</para>
        /// </summary>
        /// <param name="ShareName"></param>
        /// <param name="IsFolder"></param>
        /// <param name="LocalPath"></param>
        /// <param name="CanDelete"></param>
        /// <param name="CanCreate"></param>
        /// <param name="Size"></param>
        /// <param name="Info"></param>
        public static void AddNewShareElement(string ShareName,
            bool IsFolder,string LocalPath,bool CanDelete,bool CanCreate,string Size,string Info)
        {
            Load();

            XmlElement elem = xmlDocument.CreateElement("ShareElement");//XML节点元素名
            XmlAttribute attrib;
            attrib = xmlDocument.CreateAttribute("ShareName"); attrib.Value = ShareName; elem.SetAttributeNode(attrib);
            attrib = xmlDocument.CreateAttribute("IsFolder"); attrib.Value = IsFolder.ToString(); elem.SetAttributeNode(attrib);
            attrib = xmlDocument.CreateAttribute("LocalPath"); attrib.Value = LocalPath; elem.SetAttributeNode(attrib);
            attrib = xmlDocument.CreateAttribute("CanDelete"); attrib.Value = CanDelete.ToString(); elem.SetAttributeNode(attrib);
            attrib = xmlDocument.CreateAttribute("CanCreate"); attrib.Value = CanCreate.ToString(); elem.SetAttributeNode(attrib);
            attrib = xmlDocument.CreateAttribute("Size"); attrib.Value = Size; elem.SetAttributeNode(attrib);
            attrib = xmlDocument.CreateAttribute("Info"); attrib.Value = Info; elem.SetAttributeNode(attrib);
            //elem.Attributes.Append(new XmlAttribute(

            xmlRootElement.AppendChild(elem);

            Save();
        }

        /// <summary>根据ShareName返回SShareElementAttributes结构的可空类型实例，只考虑第一个(不可能有第二个);
        /// <para>在转换为原类型时要进行显式转换；</para>
        /// 如果其为空并赋值给原类型变量，则抛出非法操作异常.
        /// </summary>
        /// <param name="ShareName"></param>
        /// <returns>SShareElementAttributes的可空类型SShareElementAttributes?</returns>
        public static SShareElementAttributes? GetSingleShareElementAttribute(string ShareName)
        {
            List<SShareElementAttributes> list = GetShareElementAttributes();
            foreach (SShareElementAttributes attributes in list)
            {
                if (attributes.ShareName.ToLower() == ShareName.ToLower())
                    return attributes;
            }
            return null;
        }
        public static SShareElementAttributes? GetSingleShareElementAttribute(string ShareName,string path)
        {
            List<SShareElementAttributes> list = GetShareElementAttributes();
            foreach (SShareElementAttributes attributes in list)
            {
                if (attributes.ShareName.ToLower() == ShareName.ToLower() && attributes.LocalPath.ToLower() == path.ToLower())
                    return attributes;
            }
            return null;
        }
        /// <summary>返回根据XML文件中所有ShareElement生成的ShareElementAttribute结构体List集合;
        /// </summary>
        /// <returns>ShareElementAttribute结构体List集合</returns>
        public static List<SShareElementAttributes> GetShareElementAttributes()
        {
            Load();
            List<SShareElementAttributes> ShElemAttriList = new List<SShareElementAttributes>();
            SShareElementAttributes shElemAttributes;
            XmlNodeList xmlNodeList = xmlRootElement.GetElementsByTagName("ShareElement");
            //遍历XmlNodeList以把每个XmlNode的属性添加到集合中去，最后返回这个集合
            foreach (XmlNode node in xmlNodeList)
            {
                shElemAttributes.ShareName = node.Attributes["ShareName"].Value;
                shElemAttributes.IsFolder = bool.Parse(node.Attributes["IsFolder"].Value);
                shElemAttributes.LocalPath = node.Attributes["LocalPath"].Value;
                shElemAttributes.CanDelete = bool.Parse(node.Attributes["CanDelete"].Value);
                shElemAttributes.CanCreate = bool.Parse(node.Attributes["CanCreate"].Value);
                shElemAttributes.Size = node.Attributes["Size"].Value;
                shElemAttributes.Info = node.Attributes["Info"].Value;

                ShElemAttriList.Add(shElemAttributes);
            }
            
            Save();
            
            return ShElemAttriList;
        }
       
    }
}
