namespace Hive
{
    partial class FormMyComputer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMyComputer));
            this.label1 = new System.Windows.Forms.Label();
            this.treeViewMyComputer = new System.Windows.Forms.TreeView();
            this.contextMenuStripTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.加入共享SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.共享并编辑属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收缩ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.展开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListRoot = new System.Windows.Forms.ImageList(this.components);
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStripTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "请添加您的共享目录或文件:";
            // 
            // treeViewMyComputer
            // 
            this.treeViewMyComputer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewMyComputer.Location = new System.Drawing.Point(14, 51);
            this.treeViewMyComputer.Name = "treeViewMyComputer";
            this.treeViewMyComputer.Size = new System.Drawing.Size(166, 102);
            this.treeViewMyComputer.TabIndex = 1;
            this.treeViewMyComputer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewMyComputer_NodeMouseDoubleClick);
            this.treeViewMyComputer.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMyComputer_BeforeExpand);
            this.treeViewMyComputer.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewMyComputer_NodeMouseClick);
            // 
            // contextMenuStripTreeView
            // 
            this.contextMenuStripTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.加入共享SToolStripMenuItem,
            this.共享并编辑属性ToolStripMenuItem,
            this.打开OToolStripMenuItem,
            this.收缩ToolStripMenuItem,
            this.展开ToolStripMenuItem,
            this.搜索ToolStripMenuItem});
            this.contextMenuStripTreeView.Name = "contextMenuStripTreeView";
            this.contextMenuStripTreeView.Size = new System.Drawing.Size(173, 158);
            this.contextMenuStripTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTreeView_Opening);
            // 
            // 加入共享SToolStripMenuItem
            // 
            this.加入共享SToolStripMenuItem.Name = "加入共享SToolStripMenuItem";
            this.加入共享SToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.加入共享SToolStripMenuItem.Text = "共享(&S)";
            this.加入共享SToolStripMenuItem.Click += new System.EventHandler(this.加入共享SToolStripMenuItem_Click);
            // 
            // 共享并编辑属性ToolStripMenuItem
            // 
            this.共享并编辑属性ToolStripMenuItem.Name = "共享并编辑属性ToolStripMenuItem";
            this.共享并编辑属性ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.共享并编辑属性ToolStripMenuItem.Text = "共享并编辑属性(&E)";
            this.共享并编辑属性ToolStripMenuItem.Click += new System.EventHandler(this.共享并编辑属性ToolStripMenuItem_Click);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.打开OToolStripMenuItem.Text = "在硬盘上打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // 收缩ToolStripMenuItem
            // 
            this.收缩ToolStripMenuItem.Name = "收缩ToolStripMenuItem";
            this.收缩ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.收缩ToolStripMenuItem.Text = "收缩所有根目录(&C)";
            this.收缩ToolStripMenuItem.Click += new System.EventHandler(this.收缩ToolStripMenuItem_Click);
            // 
            // 展开ToolStripMenuItem
            // 
            this.展开ToolStripMenuItem.Name = "展开ToolStripMenuItem";
            this.展开ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.展开ToolStripMenuItem.Text = "展开所有根目录(&X)";
            this.展开ToolStripMenuItem.Click += new System.EventHandler(this.展开ToolStripMenuItem_Click);
            // 
            // 搜索ToolStripMenuItem
            // 
            this.搜索ToolStripMenuItem.Name = "搜索ToolStripMenuItem";
            this.搜索ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.搜索ToolStripMenuItem.Text = "@搜索";
            // 
            // imageListRoot
            // 
            this.imageListRoot.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListRoot.ImageStream")));
            this.imageListRoot.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListRoot.Images.SetKeyName(0, "MyComputer");
            this.imageListRoot.Images.SetKeyName(1, "SoftDiskDrive");
            this.imageListRoot.Images.SetKeyName(2, "HardDiskDrive");
            this.imageListRoot.Images.SetKeyName(3, "CDDrive");
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(53, 24);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(227, 21);
            this.textBoxPath.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "地址:";
            // 
            // FormMyComputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewMyComputer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMyComputer";
            this.TabText = "我的电脑";
            this.Text = "我的电脑";
            this.Load += new System.EventHandler(this.FormMyComputer_Load);
            this.contextMenuStripTreeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeViewMyComputer;
        private System.Windows.Forms.ImageList imageListRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTreeView;
        private System.Windows.Forms.ToolStripMenuItem 加入共享SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 共享并编辑属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收缩ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 展开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label label2;
    }
}