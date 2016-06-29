using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Hive
{
    public partial class FormPersonalProfile : DockContentNew
    {
        CUserInfo userInfo;
        //------------------------------
        public FormPersonalProfile()
        {
            InitializeComponent();
            IsShowed = true;
        }

        private void FormPersonalProfile_Load(object sender, EventArgs e)
        {
            userInfo = new CUserInfo();
            labelName.Text = textBoxName.Text = userInfo.Name;
            labelStatusInfo.Text = textBoxStatusInfo.Text = userInfo.StatusInfo;
            labelShared.Text = userInfo.Shared.ToString();
            labelDownloaded.Text = userInfo.Downloaded.ToString();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                //显示新头像，并进行新头像广播
                if (fd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.BackgroundImage = Image.FromFile(fd.FileName);
                }
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show("该文件不是有效的图像格式。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void labelName_Click(object sender, EventArgs e)
        {
            labelName.Visible = false;
            textBoxName.Visible = true;
            textBoxName.Focus();
            textBoxName.SelectionStart = textBoxName.Text.Length;
        }

        private void labelStatusInfo_Click(object sender, EventArgs e)
        {
            labelStatusInfo.Visible = false;
            textBoxStatusInfo.Visible = true;
            textBoxStatusInfo.Focus();
            textBoxStatusInfo.SelectionStart = textBoxStatusInfo.Text.Length;
        }

        private void textBoxName_Leave(object sender, EventArgs e)
        {
            labelName.Text = textBoxName.Text;
            labelName.Visible = true;
            textBoxName.Visible = false;
        //  保存用户名到XML文件中去
            CUserInfo.ModifyUserStatusInfo(textBoxName.Text.Replace("'","''"), null);
            //保存后，客户端定时自动上传新状态信息到服务器上去～～～
        }

        private void textBoxStatusInfo_Leave(object sender, EventArgs e)
        {
            labelStatusInfo.Text = textBoxStatusInfo.Text;
            labelStatusInfo.Visible = true;
            textBoxStatusInfo.Visible = false;
            //保存到XML文件中去
            CUserInfo.ModifyUserStatusInfo(null, textBoxStatusInfo.Text.Replace("'","''"));
        }

    }
}