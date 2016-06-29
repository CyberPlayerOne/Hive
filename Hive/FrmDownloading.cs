using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hive
{
    public partial class FrmDownloading : Form
    {
        /// <summary>
        /// 下载的文件名（属性）
        /// </summary>
        public string LabelFileName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        /// <summary>
        /// 下载方向（属性）
        /// </summary>
        public string LabelDownloading
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }
        /// <summary>
        /// 窗体中进度条控件的maximum（属性）
        /// </summary>
        public int MaximumProperty
        {
            get { return this.progressBar1.Maximum; }
            set { this.progressBar1.Maximum = value; }
        }
        /// <summary>
        /// 窗体进度条控件的value（属性）
        /// </summary>
        public int ValueProperty
        {
            get { return this.progressBar1.Value; }
            set { this.progressBar1.Value = value; }
        }
//#warning 线程间操作无效
        //private delegate void SetValueCallback(int newValue);
        ///// <summary>
        ///// [可跨线程调用]设置窗体进度条控件的value
        ///// </summary>
        ///// <param name="newValue"></param>
        //public void SetValue(int newValue) 
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.Invoke(new SetValueCallback(SetValue), newValue);
        //    }
        //    else
        //        this.progressBar1.Value = newValue;
        //}
        /// <summary>
        /// 在窗体构造函数中置空，然后单独赋值(Cancel时使用)
        /// </summary>
        public string KeyIpNPath;
        public ReadObject ReadObject;

        public FrmDownloading()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 主要使用的构造函数
        /// </summary>
        /// <param name="labelFileName">正在传输的文件名字</param>
        /// <param name="labelDownloading">例如从什么到什么</param>
        /// <param name="maximum">进度条的最大值</param>
        public FrmDownloading(string labelFileName, string labelDownloading, int maximum):this()
        {
            LabelFileName = labelFileName;
            LabelDownloading = labelDownloading;
            MaximumProperty = maximum;
            KeyIpNPath = null;
            ReadObject = null;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //取消下载-------

            this.Close();
#warning NOW!停止传输！
            MainForm.PMainForm.frmDocAllShare.CancelFrmDownloading(this.KeyIpNPath);
        }
    }
}