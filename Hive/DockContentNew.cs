using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace Hive
{
    /// <summary>
    /// 继承自原来WeifenLuo的DockContent，并添加了IsShowed属性，表示当前是否显示（激活）。
    /// </summary>
    public class DockContentNew : DockContent
    {
        /// <summary>
        /// 标识此窗体是否已被显示（激活/获得焦点）
        /// </summary>
        public bool IsShowed;
    }
}
