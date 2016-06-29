namespace Hive
{
    partial class FormPublicSharers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPublicSharers));
            this.listViewOnlineUsers = new System.Windows.Forms.ListView();
            this.contextMenuStripUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.聊天CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.buttonClosePanelSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.listViewFind = new System.Windows.Forms.ListView();
            this.imageListDisplayPic = new System.Windows.Forms.ImageList(this.components);
            this.个人信息PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripUsers.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewOnlineUsers
            // 
            this.listViewOnlineUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewOnlineUsers.Location = new System.Drawing.Point(12, 27);
            this.listViewOnlineUsers.MultiSelect = false;
            this.listViewOnlineUsers.Name = "listViewOnlineUsers";
            this.listViewOnlineUsers.Size = new System.Drawing.Size(266, 236);
            this.listViewOnlineUsers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewOnlineUsers.TabIndex = 0;
            this.listViewOnlineUsers.UseCompatibleStateImageBehavior = false;
            this.listViewOnlineUsers.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStripUsers
            // 
            this.contextMenuStripUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.聊天CToolStripMenuItem,
            this.个人信息PToolStripMenuItem});
            this.contextMenuStripUsers.Name = "contextMenuStripUsers";
            this.contextMenuStripUsers.Size = new System.Drawing.Size(153, 70);
            this.contextMenuStripUsers.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripUsers_Opening);
            // 
            // 聊天CToolStripMenuItem
            // 
            this.聊天CToolStripMenuItem.Name = "聊天CToolStripMenuItem";
            this.聊天CToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.聊天CToolStripMenuItem.Text = "聊天(&C)";
            this.聊天CToolStripMenuItem.Click += new System.EventHandler(this.聊天CToolStripMenuItem_Click);
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.BackColor = System.Drawing.SystemColors.Control;
            this.panelSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelSearch.BackgroundImage")));
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.buttonClosePanelSearch);
            this.panelSearch.Controls.Add(this.label1);
            this.panelSearch.Controls.Add(this.textBoxSearch);
            this.panelSearch.Location = new System.Drawing.Point(4, 3);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(282, 20);
            this.panelSearch.TabIndex = 1;
            // 
            // buttonClosePanelSearch
            // 
            this.buttonClosePanelSearch.BackColor = System.Drawing.Color.Transparent;
            this.buttonClosePanelSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClosePanelSearch.BackgroundImage")));
            this.buttonClosePanelSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClosePanelSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonClosePanelSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonClosePanelSearch.Location = new System.Drawing.Point(262, 0);
            this.buttonClosePanelSearch.Name = "buttonClosePanelSearch";
            this.buttonClosePanelSearch.Size = new System.Drawing.Size(18, 18);
            this.buttonClosePanelSearch.TabIndex = 3;
            this.buttonClosePanelSearch.UseVisualStyleBackColor = false;
            this.buttonClosePanelSearch.Click += new System.EventHandler(this.buttonClosePanelSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(1, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "搜索:";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearch.Location = new System.Drawing.Point(35, -1);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(227, 21);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // listViewFind
            // 
            this.listViewFind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFind.Location = new System.Drawing.Point(12, 49);
            this.listViewFind.Name = "listViewFind";
            this.listViewFind.Size = new System.Drawing.Size(251, 203);
            this.listViewFind.TabIndex = 2;
            this.listViewFind.UseCompatibleStateImageBehavior = false;
            this.listViewFind.View = System.Windows.Forms.View.Details;
            this.listViewFind.Visible = false;
            // 
            // imageListDisplayPic
            // 
            this.imageListDisplayPic.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDisplayPic.ImageStream")));
            this.imageListDisplayPic.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDisplayPic.Images.SetKeyName(0, "Client 2.png");
            // 
            // 个人信息PToolStripMenuItem
            // 
            this.个人信息PToolStripMenuItem.Name = "个人信息PToolStripMenuItem";
            this.个人信息PToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.个人信息PToolStripMenuItem.Text = "@个人信息(&P)";
            // 
            // FormPublicSharers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 264);
            this.Controls.Add(this.listViewFind);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.listViewOnlineUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormPublicSharers";
            this.TabText = "在线用户";
            this.Text = "在线用户";
            this.Load += new System.EventHandler(this.FormPublicShare_Load);
            this.contextMenuStripUsers.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewOnlineUsers;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClosePanelSearch;
        private System.Windows.Forms.ListView listViewFind;
        private System.Windows.Forms.ImageList imageListDisplayPic;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripUsers;
        private System.Windows.Forms.ToolStripMenuItem 聊天CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 个人信息PToolStripMenuItem;
    }
}