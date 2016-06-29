namespace Hive
{
    partial class FormDocumentAllShare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocumentAllShare));
            this.imageComboBoxShare = new ImageComboBox.ImageComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listViewShare = new System.Windows.Forms.ListView();
            this.contextMenuStripListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.共享属性SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonAddToFavorite = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonRoot = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.contextMenuStripListView.SuspendLayout();
            this.SuspendLayout();
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
            this.imageComboBoxShare.TabIndex = 7;
            this.imageComboBoxShare.Text = "imageComboBoxShare";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "地址(&D):";
            // 
            // listViewShare
            // 
            this.listViewShare.AllowColumnReorder = true;
            this.listViewShare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewShare.ContextMenuStrip = this.contextMenuStripListView;
            this.listViewShare.LabelEdit = true;
            this.listViewShare.Location = new System.Drawing.Point(0, 92);
            this.listViewShare.Name = "listViewShare";
            this.listViewShare.ShowItemToolTips = true;
            this.listViewShare.Size = new System.Drawing.Size(579, 411);
            this.listViewShare.TabIndex = 8;
            this.listViewShare.UseCompatibleStateImageBehavior = false;
            this.listViewShare.ItemActivate += new System.EventHandler(this.listViewShare_ItemActivate);
            // 
            // contextMenuStripListView
            // 
            this.contextMenuStripListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.共享属性SToolStripMenuItem,
            this.另存为SToolStripMenuItem});
            this.contextMenuStripListView.Name = "contextMenuStrip1";
            this.contextMenuStripListView.Size = new System.Drawing.Size(138, 48);
            this.contextMenuStripListView.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripListView_Opening);
            // 
            // 共享属性SToolStripMenuItem
            // 
            this.共享属性SToolStripMenuItem.Name = "共享属性SToolStripMenuItem";
            this.共享属性SToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.共享属性SToolStripMenuItem.Text = "共享属性(&R)";
            this.共享属性SToolStripMenuItem.Click += new System.EventHandler(this.共享属性SToolStripMenuItem_Click);
            // 
            // 另存为SToolStripMenuItem
            // 
            this.另存为SToolStripMenuItem.Name = "另存为SToolStripMenuItem";
            this.另存为SToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.另存为SToolStripMenuItem.Text = "另存为(&S)...";
            this.另存为SToolStripMenuItem.Click += new System.EventHandler(this.另存为SToolStripMenuItem_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Image = ((System.Drawing.Image)(resources.GetObject("buttonGo.Image")));
            this.buttonGo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonGo.Location = new System.Drawing.Point(515, 61);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(64, 28);
            this.buttonGo.TabIndex = 9;
            this.buttonGo.Text = "转到";
            this.buttonGo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefresh.Image")));
            this.buttonRefresh.Location = new System.Drawing.Point(12, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(47, 47);
            this.buttonRefresh.TabIndex = 10;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(65, 12);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(47, 47);
            this.buttonBack.TabIndex = 11;
            this.buttonBack.Text = "后退";
            this.buttonBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonBack.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonAddToFavorite
            // 
            this.buttonAddToFavorite.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAddToFavorite.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddToFavorite.Image")));
            this.buttonAddToFavorite.Location = new System.Drawing.Point(226, 12);
            this.buttonAddToFavorite.Name = "buttonAddToFavorite";
            this.buttonAddToFavorite.Size = new System.Drawing.Size(47, 47);
            this.buttonAddToFavorite.TabIndex = 12;
            this.buttonAddToFavorite.Text = "收藏";
            this.buttonAddToFavorite.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonAddToFavorite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonAddToFavorite.UseVisualStyleBackColor = true;
            this.buttonAddToFavorite.Click += new System.EventHandler(this.buttonAddToFavorite_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDownload.Image = ((System.Drawing.Image)(resources.GetObject("buttonDownload.Image")));
            this.buttonDownload.Location = new System.Drawing.Point(332, 12);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(47, 47);
            this.buttonDownload.TabIndex = 13;
            this.buttonDownload.Text = "下载";
            this.buttonDownload.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonDownload.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonForward.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.Image")));
            this.buttonForward.Location = new System.Drawing.Point(118, 12);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(47, 47);
            this.buttonForward.TabIndex = 14;
            this.buttonForward.Text = "前进";
            this.buttonForward.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonForward.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearch.Image")));
            this.buttonSearch.Location = new System.Drawing.Point(279, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(47, 47);
            this.buttonSearch.TabIndex = 15;
            this.buttonSearch.Text = "搜索";
            this.buttonSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonRoot
            // 
            this.buttonRoot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRoot.Image = ((System.Drawing.Image)(resources.GetObject("buttonRoot.Image")));
            this.buttonRoot.Location = new System.Drawing.Point(171, 12);
            this.buttonRoot.Name = "buttonRoot";
            this.buttonRoot.Size = new System.Drawing.Size(49, 47);
            this.buttonRoot.TabIndex = 16;
            this.buttonRoot.Text = "根目录";
            this.buttonRoot.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRoot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonRoot.UseVisualStyleBackColor = true;
            this.buttonRoot.Click += new System.EventHandler(this.buttonRoot_Click);
            // 
            // FormDocumentAllShare
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
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.listViewShare);
            this.Controls.Add(this.imageComboBoxShare);
            this.Controls.Add(this.label2);
            this.Name = "FormDocumentAllShare";
            this.TabText = "网络资源浏览器";
            this.Text = "网络资源浏览器";
            this.Load += new System.EventHandler(this.FormDocumentAllShare_Load);
            this.contextMenuStripListView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageComboBox.ImageComboBox imageComboBoxShare;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listViewShare;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonAddToFavorite;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonRoot;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListView;
        private System.Windows.Forms.ToolStripMenuItem 共享属性SToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 另存为SToolStripMenuItem;

    }
}