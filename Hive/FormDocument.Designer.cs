namespace Hive
{
    partial class FormDocument
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocument));
            this.listViewShare = new System.Windows.Forms.ListView();
            this.contextMenuStripMyShare = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.共享属性PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除文件夹EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除共享DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加共享AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.排列图标LToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在新窗口打开NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonGo = new System.Windows.Forms.Button();
            this.imageComboBoxShare = new ImageComboBox.ImageComboBox();
            this.buttonRoot = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonAddToFavorite = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.contextMenuStripMyShare.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewShare
            // 
            this.listViewShare.AllowColumnReorder = true;
            this.listViewShare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewShare.BackgroundImageTiled = true;
            this.listViewShare.ContextMenuStrip = this.contextMenuStripMyShare;
            this.listViewShare.LabelEdit = true;
            this.listViewShare.Location = new System.Drawing.Point(0, 92);
            this.listViewShare.Name = "listViewShare";
            this.listViewShare.ShowItemToolTips = true;
            this.listViewShare.Size = new System.Drawing.Size(579, 411);
            this.listViewShare.TabIndex = 0;
            this.listViewShare.UseCompatibleStateImageBehavior = false;
            this.listViewShare.ItemActivate += new System.EventHandler(this.listViewShare_ItemActivate);
            this.listViewShare.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewShare_BeforeLabelEdit);
            // 
            // contextMenuStripMyShare
            // 
            this.contextMenuStripMyShare.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripMenuItem,
            this.共享属性PToolStripMenuItem,
            this.删除文件夹EToolStripMenuItem,
            this.删除共享DToolStripMenuItem,
            this.添加共享AToolStripMenuItem,
            this.查看VToolStripMenuItem,
            this.排列图标LToolStripMenuItem,
            this.backgroundImageToolStripMenuItem,
            this.在新窗口打开NToolStripMenuItem});
            this.contextMenuStripMyShare.Name = "contextMenuStripMyShare";
            this.contextMenuStripMyShare.Size = new System.Drawing.Size(179, 202);
            this.contextMenuStripMyShare.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripMyShare_Opening);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // 共享属性PToolStripMenuItem
            // 
            this.共享属性PToolStripMenuItem.Name = "共享属性PToolStripMenuItem";
            this.共享属性PToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.共享属性PToolStripMenuItem.Text = "编辑共享属性(&S)...";
            this.共享属性PToolStripMenuItem.Click += new System.EventHandler(this.编辑共享属性PToolStripMenuItem_Click);
            // 
            // 删除文件夹EToolStripMenuItem
            // 
            this.删除文件夹EToolStripMenuItem.Name = "删除文件夹EToolStripMenuItem";
            this.删除文件夹EToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.删除文件夹EToolStripMenuItem.Text = "@删除文件(夹)(&E)";
            // 
            // 删除共享DToolStripMenuItem
            // 
            this.删除共享DToolStripMenuItem.Name = "删除共享DToolStripMenuItem";
            this.删除共享DToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.删除共享DToolStripMenuItem.Text = "删除共享(&D)";
            this.删除共享DToolStripMenuItem.Click += new System.EventHandler(this.删除共享DToolStripMenuItem_Click);
            // 
            // 添加共享AToolStripMenuItem
            // 
            this.添加共享AToolStripMenuItem.Name = "添加共享AToolStripMenuItem";
            this.添加共享AToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.添加共享AToolStripMenuItem.Text = "添加共享(&A)";
            this.添加共享AToolStripMenuItem.Click += new System.EventHandler(this.添加共享AToolStripMenuItem_Click);
            // 
            // 查看VToolStripMenuItem
            // 
            this.查看VToolStripMenuItem.Name = "查看VToolStripMenuItem";
            this.查看VToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.查看VToolStripMenuItem.Text = "@查看(&V)";
            // 
            // 排列图标LToolStripMenuItem
            // 
            this.排列图标LToolStripMenuItem.Name = "排列图标LToolStripMenuItem";
            this.排列图标LToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.排列图标LToolStripMenuItem.Text = "@排列图标(&I)";
            // 
            // backgroundImageToolStripMenuItem
            // 
            this.backgroundImageToolStripMenuItem.Name = "backgroundImageToolStripMenuItem";
            this.backgroundImageToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.backgroundImageToolStripMenuItem.Text = "@显示属性(&R)...";
            this.backgroundImageToolStripMenuItem.Click += new System.EventHandler(this.backgroundImageToolStripMenuItem_Click);
            // 
            // 在新窗口打开NToolStripMenuItem
            // 
            this.在新窗口打开NToolStripMenuItem.Name = "在新窗口打开NToolStripMenuItem";
            this.在新窗口打开NToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.在新窗口打开NToolStripMenuItem.Text = "@在新窗口打开(&N)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "地址(&D):";
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Image = ((System.Drawing.Image)(resources.GetObject("buttonGo.Image")));
            this.buttonGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGo.Location = new System.Drawing.Point(515, 61);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(64, 28);
            this.buttonGo.TabIndex = 4;
            this.buttonGo.Text = "转到";
            this.buttonGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // imageComboBoxShare
            // 
            this.imageComboBoxShare.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageComboBoxShare.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.imageComboBoxShare.ImageList = null;
            this.imageComboBoxShare.Indent = 0;
            this.imageComboBoxShare.ItemHeight = 15;
            this.imageComboBoxShare.Location = new System.Drawing.Point(71, 65);
            this.imageComboBoxShare.Name = "imageComboBoxShare";
            this.imageComboBoxShare.Size = new System.Drawing.Size(438, 21);
            this.imageComboBoxShare.TabIndex = 5;
            this.imageComboBoxShare.Tag = "";
            this.imageComboBoxShare.Text = "imageComboBoxShare";
            this.imageComboBoxShare.SelectedIndexChanged += new System.EventHandler(this.imageComboBoxShare_SelectedIndexChanged);
            this.imageComboBoxShare.KeyDown += new System.Windows.Forms.KeyEventHandler(this.imageComboBoxShare_KeyDown);
            // 
            // buttonRoot
            // 
            this.buttonRoot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRoot.Image = ((System.Drawing.Image)(resources.GetObject("buttonRoot.Image")));
            this.buttonRoot.Location = new System.Drawing.Point(171, 12);
            this.buttonRoot.Name = "buttonRoot";
            this.buttonRoot.Size = new System.Drawing.Size(49, 47);
            this.buttonRoot.TabIndex = 23;
            this.buttonRoot.Text = "根目录";
            this.buttonRoot.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRoot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRoot.UseVisualStyleBackColor = true;
            this.buttonRoot.Click += new System.EventHandler(this.buttonRoot_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearch.Image")));
            this.buttonSearch.Location = new System.Drawing.Point(279, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(47, 47);
            this.buttonSearch.TabIndex = 22;
            this.buttonSearch.Text = "搜索";
            this.buttonSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // buttonForward
            // 
            this.buttonForward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonForward.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.Image")));
            this.buttonForward.Location = new System.Drawing.Point(118, 12);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(47, 47);
            this.buttonForward.TabIndex = 21;
            this.buttonForward.Text = "前进";
            this.buttonForward.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDownload.Image = ((System.Drawing.Image)(resources.GetObject("buttonDownload.Image")));
            this.buttonDownload.Location = new System.Drawing.Point(332, 12);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(47, 47);
            this.buttonDownload.TabIndex = 20;
            this.buttonDownload.Text = "下载";
            this.buttonDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDownload.UseVisualStyleBackColor = true;
            // 
            // buttonAddToFavorite
            // 
            this.buttonAddToFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAddToFavorite.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddToFavorite.Image")));
            this.buttonAddToFavorite.Location = new System.Drawing.Point(226, 12);
            this.buttonAddToFavorite.Name = "buttonAddToFavorite";
            this.buttonAddToFavorite.Size = new System.Drawing.Size(47, 47);
            this.buttonAddToFavorite.TabIndex = 19;
            this.buttonAddToFavorite.Text = "收藏";
            this.buttonAddToFavorite.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAddToFavorite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonAddToFavorite.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(65, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(47, 47);
            this.buttonBack.TabIndex = 18;
            this.buttonBack.Text = "后退";
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(12, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(47, 47);
            this.buttonRefresh.TabIndex = 17;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // FormDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 503);
            this.Controls.Add(this.buttonRoot);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.buttonAddToFavorite);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.imageComboBoxShare);
            this.Controls.Add(this.listViewShare);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonGo);
            this.DoubleBuffered = true;
            this.Name = "FormDocument";
            this.TabText = "共享管理器";
            this.Text = "我的共享(此Text无用)";
            this.ToolTipText = "这是您在本机上设置的共享目录，您可以进行添加、删除等等操作。";
            this.Load += new System.EventHandler(this.FormDocument_Load);
            this.contextMenuStripMyShare.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewShare;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMyShare;
        private System.Windows.Forms.ToolStripMenuItem 删除共享DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 共享属性PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加共享AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonGo;
        private ImageComboBox.ImageComboBox imageComboBoxShare;
        private System.Windows.Forms.ToolStripMenuItem 查看VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 排列图标LToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backgroundImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在新窗口打开NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除文件夹EToolStripMenuItem;
        private System.Windows.Forms.Button buttonRoot;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonAddToFavorite;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonRefresh;

    }
}

