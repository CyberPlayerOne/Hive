namespace Hive
{
   /// <summary>
   /// 共享资源属性页
   /// </summary>
    partial class FrmProperty
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProperty));
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.labelShareName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.checkBoxCanCreate = new System.Windows.Forms.CheckBox();
            this.checkBoxCanDelete = new System.Windows.Forms.CheckBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.textBoxShareName = new System.Windows.Forms.TextBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSaveInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Location = new System.Drawing.Point(20, 21);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(33, 33);
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            // 
            // labelShareName
            // 
            this.labelShareName.AutoSize = true;
            this.labelShareName.Location = new System.Drawing.Point(86, 36);
            this.labelShareName.Name = "labelShareName";
            this.labelShareName.Size = new System.Drawing.Size(41, 12);
            this.labelShareName.TabIndex = 1;
            this.labelShareName.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目标类型:";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(17, 76);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(324, 10);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(108, 95);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(41, 12);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "目标位置:";
            // 
            // labelPath
            // 
            this.labelPath.Location = new System.Drawing.Point(108, 127);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(227, 12);
            this.labelPath.TabIndex = 6;
            this.labelPath.Text = "label5";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(17, 155);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(324, 10);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "共享名称:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSize);
            this.groupBox1.Controls.Add(this.checkBoxCanCreate);
            this.groupBox1.Controls.Add(this.checkBoxCanDelete);
            this.groupBox1.Controls.Add(this.textBoxPath);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Controls.Add(this.textBoxShareName);
            this.groupBox1.Controls.Add(this.textBoxInfo);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(17, 171);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 204);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "共享属性";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(91, 113);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(215, 12);
            this.labelSize.TabIndex = 21;
            this.labelSize.Text = "对文件夹只计算子文件，说“大于**M”";
            // 
            // checkBoxCanCreate
            // 
            this.checkBoxCanCreate.AutoSize = true;
            this.checkBoxCanCreate.Location = new System.Drawing.Point(93, 88);
            this.checkBoxCanCreate.Name = "checkBoxCanCreate";
            this.checkBoxCanCreate.Size = new System.Drawing.Size(48, 16);
            this.checkBoxCanCreate.TabIndex = 20;
            this.checkBoxCanCreate.Text = "允许";
            this.toolTip1.SetToolTip(this.checkBoxCanCreate, "是否允许访客向您的共享文件夹上传新文件\r\n(仅对共享文件夹有效)");
            this.checkBoxCanCreate.UseVisualStyleBackColor = true;
            this.checkBoxCanCreate.CheckedChanged += new System.EventHandler(this.SetOkButtonSave);
            // 
            // checkBoxCanDelete
            // 
            this.checkBoxCanDelete.AutoSize = true;
            this.checkBoxCanDelete.Location = new System.Drawing.Point(93, 64);
            this.checkBoxCanDelete.Name = "checkBoxCanDelete";
            this.checkBoxCanDelete.Size = new System.Drawing.Size(48, 16);
            this.checkBoxCanDelete.TabIndex = 19;
            this.checkBoxCanDelete.Text = "允许";
            this.toolTip1.SetToolTip(this.checkBoxCanDelete, "是否允许访客通过共享渠道删除此文件");
            this.checkBoxCanDelete.UseVisualStyleBackColor = true;
            this.checkBoxCanDelete.CheckedChanged += new System.EventHandler(this.SetOkButtonSave);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(93, 38);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(163, 21);
            this.textBoxPath.TabIndex = 17;
            this.toolTip1.SetToolTip(this.textBoxPath, "共享资源在本机上的物理路径.点击“打开”按钮更改路径.");
            this.textBoxPath.TextChanged += new System.EventHandler(this.SetOkButtonSave);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(262, 36);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(56, 23);
            this.buttonOpen.TabIndex = 16;
            this.buttonOpen.Text = "打开...";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // textBoxShareName
            // 
            this.textBoxShareName.Location = new System.Drawing.Point(93, 14);
            this.textBoxShareName.Name = "textBoxShareName";
            this.textBoxShareName.Size = new System.Drawing.Size(225, 21);
            this.textBoxShareName.TabIndex = 15;
            this.toolTip1.SetToolTip(this.textBoxShareName, "共享名称仅作为资源的标识，与资源的打开方式无关.");
            this.textBoxShareName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxShareName_KeyPress);
            this.textBoxShareName.TextChanged += new System.EventHandler(this.SetOkButtonSave);
            this.textBoxShareName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxShareName_KeyDown);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(71, 134);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxInfo.Size = new System.Drawing.Size(247, 61);
            this.textBoxInfo.TabIndex = 14;
            this.textBoxInfo.TextChanged += new System.EventHandler(this.SetOkButtonSave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "简短说明:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "大小:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "允许访客添加:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 65);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "允许访客删除:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "本地路径:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(185, 381);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(266, 381);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelSaveInfo
            // 
            this.labelSaveInfo.AutoSize = true;
            this.labelSaveInfo.Location = new System.Drawing.Point(18, 386);
            this.labelSaveInfo.Name = "labelSaveInfo";
            this.labelSaveInfo.Size = new System.Drawing.Size(143, 12);
            this.labelSaveInfo.TabIndex = 12;
            this.labelSaveInfo.Text = "属性已改变，请注意保存:";
            this.labelSaveInfo.Visible = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Tips";
            // 
            // FrmProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 416);
            this.Controls.Add(this.labelSaveInfo);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelShareName);
            this.Controls.Add(this.pictureBoxIcon);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProperty";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XX文件属性";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Level3FormProperty_KeyDown);
            this.Load += new System.EventHandler(this.Level3FormProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label labelShareName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.TextBox textBoxShareName;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.CheckBox checkBoxCanCreate;
        private System.Windows.Forms.CheckBox checkBoxCanDelete;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSaveInfo;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}