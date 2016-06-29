using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Hive
{
    /// <summary>
    /// 窗体启动时先显示的logo窗体
    /// </summary>
    public partial class FrmLogo : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmLogo()
        {
            InitializeComponent();
            //timer1.Enabled = true;timer1已在设计时设为启动
            //form fading in
            this.Opacity = 0d;
            timerFading.Start();//timerFading要在Opacity设为0后启动
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
            //(new MainForm()).Show();//要用Application.Run在program.cs里
        }

        private void timerFading_Tick(object sender, EventArgs e)
        {
            OpacityUp();
        }
       /// <summary>
        /// 窗体渐显
       /// </summary> 
        void OpacityUp()
        {
            if (this.Opacity < 1.0d)
            {
                this.Opacity += 0.1d;
            }
            else
            {
                this.timerFading.Stop();
            }
        }

        private void FormLogo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}