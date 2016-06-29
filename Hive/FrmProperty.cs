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
    /// 属性页窗体
    /// </summary>
    public partial class FrmProperty : Form
    {
        //为保持本窗体控件的私有属性,设置了以下公有字段。
        //字段由外部设置完毕，然后再Form.Show()

        /// <summary>
        ///共享名字段
        /// </summary>
        public string _labelShareName = "";
        /// <summary>
        /// <para>类型，路径，共享名，路径</para>
        /// _labelType这里用“文件夹”或“文件”表示，而不是bool值
        /// </summary>
        public string _labelType = "",//可能值有“文件夹”或“文件”
            _labelPath = "",
            _textBoxShareName = "",
            _textBoxPath = "";
        /// <summary>
        /// 是否可由访问者删除,是否可由访问者建立；默认不可
        /// </summary>
        public bool _checkBoxCanDelete = false,
            _checkBoxCanCreate = false;
        /// <summary>
        /// 资源大小·资源简短信息
        /// </summary>
        public string _labelSize = "",
            _textBoxInfo = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmProperty()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //foreach (Control control in this.Controls)
            //{
            //    if(control is TextBox)
            //        ((TextBox)control).KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            //    if (control is CheckBox)
            //        ((CheckBox)control).KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            //    if (control is Button)
            //        ((Button)control).KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            //}//foreach无法更改遍历元素值
            textBoxShareName.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            textBoxPath.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            buttonOpen.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            checkBoxCanDelete.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            checkBoxCanCreate.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            textBoxInfo.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            buttonOK.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);
            buttonCancel.KeyDown += new KeyEventHandler(Level3FormProperty_KeyDown);            
        }

        private void Level3FormProperty_Load(object sender, EventArgs e)
        {
            labelSaveInfo.Visible = false;//仅更改共享属性时显示
            this.buttonOK.Text = "确定";
            if (_labelType == "文件")
                checkBoxCanCreate.Enabled = false;//逻辑上文件不允许添加文件

            if (!CXmlHandle.GetSingleShareElementAttribute(labelShareName.Text).HasValue)//不是共享根目录下的文件夹                
            {
                label11.Text = "相对路径:";
                labelSaveInfo.Text = "部分共享属性具有继承性。";
                labelSaveInfo.Visible = true;
               foreach (Control control in this.groupBox1.Controls)
                {                        
                    if (control is Button)
                    {
                          control.Enabled = false;
                    }
                    if (control is TextBox)
                        ((TextBox)control).ReadOnly = true;
                    if (control is CheckBox)
                        ((CheckBox)control).Enabled = false;
                }
                //隐藏打开按钮和加长文本框宽度 
                buttonOpen.Visible = false;
                textBoxPath.Width = textBoxShareName.Width;
            }
        }

        /// <summary>(私有方法成员)在重写的Show中使用,用于设置窗体控件
        /// </summary>
        private void SetControlValue()
        {
            this.DoubleBuffered = true;
            labelShareName.Text = _labelShareName;
            labelType.Text = _labelType;
            labelPath.Text = _labelPath;
            textBoxShareName.Text = _textBoxShareName;
            textBoxPath.Text = _textBoxPath;
            if (_checkBoxCanDelete) checkBoxCanDelete.Checked = true;
            else checkBoxCanDelete.Checked = false;
            if (_checkBoxCanCreate) checkBoxCanCreate.Checked = true;
            else checkBoxCanCreate.Checked = false;
            labelSize.Text = _labelSize;
            textBoxInfo.Text = _textBoxInfo;
        }

        /// <summary>重写的Show方法，内先调用SetControlValue();
        /// <para>在调用Show之前应先填写控件字段！！！</para>
        /// </summary>
        public new void Show()
        {
            try
            {
                SetControlValue();

                Icon icon;
                if (_labelType == "文件夹")
                {
                    icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType("文件夹", true);
                }
                else
                {
                    string ext = System.IO.Path.GetExtension(_labelPath);
                    if (ext.ToLower() == ".exe")
                    {
                        try
                        {
                            icon = System.Drawing.Icon.ExtractAssociatedIcon(_labelPath);
                        }
                        catch
                        {
                            icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(".exe", true);
                        }
                    }
                    else
                    {
                        if (ext == string.Empty) ext = ".?";
                        try
                        {
                            icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(ext, true);
                        }
                        catch
                        {
                            icon = Deep.DBFile.DataCommunicate.GetSystemIcon.GetIconByFileType(".?", true);
                        }
                    }
                }
                pictureBoxIcon.Image = icon.ToBitmap();

                base.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        /// <summary>响应ESC键退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Level3FormProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// 重新选择路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (_labelType == "文件夹")
                {
                    FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                    folderDialog.Description = "请选择您要共享的新文件夹:";
                    folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                    folderDialog.SelectedPath = _labelPath;
                    folderDialog.ShowNewFolderButton = true;
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxPath.Text = folderDialog.SelectedPath;
                    }
                }
                else
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.FileName = "";
                    fileDialog.CheckFileExists = true;
                    fileDialog.CheckPathExists = true;
                    System.IO.FileInfo fi = new System.IO.FileInfo(_labelPath);
                    fileDialog.InitialDirectory = fi.DirectoryName;
                    fileDialog.Multiselect = false;
                    fileDialog.Title = "请选择你要共享的文件的新位置:";
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxPath.Text = fileDialog.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///调用CXmlHandle.ModifyShareElement(）直接修改XML文件！完全正确。
        /// 而不是根据选中的item改变（Show()方法的舞模式对话框可以允许选择另一个item）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonOK.Text == "确定")//没有修改
                { this.Close(); this.Dispose(); return; }

                //检查输入属性ShareName(和Path)是否有重复-----------------
                EditBalloon balloonTip;//初始化EditBalloon
                if (textBoxShareName.Text.ToLower() != labelShareName.Text.ToLower()
                    && MainForm.PMainForm.frmDocument.ExistsShareName(textBoxShareName.Text)//如果确实改了共享名&&新共享名与其他共享名有冲突(如果修改后名字还是原来的，就不必也没法检查了(2个))
                    )
                {
                    balloonTip = new EditBalloon();
                    balloonTip.Title = "共享名错误";
                    balloonTip.TitleIcon = TooltipIcon.Error;
                    balloonTip.Text = "在您的共享列表中已经有相同共享名的资源！";
                    balloonTip.Parent = this.textBoxShareName;
                    balloonTip.Show();
                    return;
                }
                if (!System.IO.File.Exists(textBoxPath.Text) && !System.IO.Directory.Exists(textBoxPath.Text))//既不存在此路径的文件也不存在此路径的文件夹
                {
                    balloonTip = new EditBalloon();
                    balloonTip.Title = "路径错误";
                    balloonTip.TitleIcon = TooltipIcon.Error;
                    balloonTip.Text = "您输入的路径不存在任何文件或文件夹！";
                    balloonTip.Parent = this.textBoxPath;
                    balloonTip.Show();
                    return;
                }
                //修改了//要输入到XML文件中去，调用CXmlHandle类的 ModifyShareElement()
                CXmlHandle.ModifyShareElement(
                    labelShareName.Text,
                    textBoxShareName.Text,
                    System.IO.Directory.Exists(textBoxPath.Text),
                    textBoxPath.Text,
                    checkBoxCanDelete.Checked,
                    checkBoxCanCreate.Checked,
                    System.IO.Directory.Exists(textBoxPath.Text) ? CToolClass.GetSizeOfFolder(textBoxPath.Text) : CToolClass.GetSizeOfFile(textBoxPath.Text),//!!!!!!!!!!!!!!!!!!!!!!!!!!!!文件夹大小计算方法需要单独线程否则会阻塞
                    textBoxInfo.Text);
                MainForm.PMainForm.frmDocument.PaintListViewWithMyShare();
                MainForm.PMainForm.SendShareListToServer();//修改共享属性时候发送一遍
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>所有文本框文字改变的事件处理函数；
        /// 用来改变“确定”按钮为"保存"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetOkButtonSave(object sender, EventArgs e)//Show()的时候也会触发，故在Load事件时赋原来值
        {
            this.labelSaveInfo.Visible = true;
            this.buttonOK.Text = "保存";
        }

        /// <summary>
        /// 禁止使用“\”字符的按键事件响应函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxShareName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\\')
            {
                e.Handled = true;

                //弹出警告
                EditBalloon balloonTip;//初始化EditBalloon
                balloonTip = new EditBalloon();
                balloonTip.Title = "非法字符";
                balloonTip.TitleIcon = TooltipIcon.Error;
                balloonTip.Text = "共享名中不能包含字符\\！";
                balloonTip.Parent = this.textBoxShareName;
                balloonTip.Show();
            }
        }

        private void textBoxShareName_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

       
    }
}