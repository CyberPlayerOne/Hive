namespace Hive
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.dockPanelMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.在线用户OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.个人信息PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.我的电脑MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.我的所有共享WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.网络资源浏览器NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.我下载的文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.vistaTopicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.皮肤ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统默认DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收藏AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加到收藏夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.整理收藏夹OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.newThingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加到此收藏夹AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.内置浏览器IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索在线用户UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索共享资源ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.配置OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于HiveAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonForward = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.timerUdpNotifyServer = new System.Windows.Forms.Timer(this.components);
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine(((System.ComponentModel.Component)(this)));
            this.menuStripMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanelMain
            // 
            this.dockPanelMain.ActiveAutoHideContent = null;
            this.dockPanelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dockPanelMain.Location = new System.Drawing.Point(0, 52);
            this.dockPanelMain.Name = "dockPanelMain";
            this.dockPanelMain.Size = new System.Drawing.Size(915, 534);
            this.dockPanelMain.TabIndex = 1;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.查看ToolStripMenuItem,
            this.收藏AToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(915, 24);
            this.menuStripMain.TabIndex = 4;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出EToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 退出EToolStripMenuItem
            // 
            this.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem";
            this.退出EToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.退出EToolStripMenuItem.Text = "退出(&E)";
            this.退出EToolStripMenuItem.Click += new System.EventHandler(this.退出EToolStripMenuItem_Click);
            // 
            // 查看ToolStripMenuItem
            // 
            this.查看ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在线用户OToolStripMenuItem,
            this.个人信息PToolStripMenuItem,
            this.我的电脑MToolStripMenuItem,
            this.toolStripMenuItem2,
            this.我的所有共享WToolStripMenuItem,
            this.网络资源浏览器NToolStripMenuItem,
            this.我下载的文件ToolStripMenuItem,
            this.toolStripMenuItem3,
            this.vistaTopicToolStripMenuItem,
            this.皮肤ToolStripMenuItem});
            this.查看ToolStripMenuItem.Name = "查看ToolStripMenuItem";
            this.查看ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.查看ToolStripMenuItem.Text = "查看(&V)";
            // 
            // 在线用户OToolStripMenuItem
            // 
            this.在线用户OToolStripMenuItem.Checked = true;
            this.在线用户OToolStripMenuItem.CheckOnClick = true;
            this.在线用户OToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.在线用户OToolStripMenuItem.Name = "在线用户OToolStripMenuItem";
            this.在线用户OToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.在线用户OToolStripMenuItem.Text = "在线用户浏览(&O)";
            this.在线用户OToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // 个人信息PToolStripMenuItem
            // 
            this.个人信息PToolStripMenuItem.Checked = true;
            this.个人信息PToolStripMenuItem.CheckOnClick = true;
            this.个人信息PToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.个人信息PToolStripMenuItem.Name = "个人信息PToolStripMenuItem";
            this.个人信息PToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.个人信息PToolStripMenuItem.Text = "个人信息(P)";
            this.个人信息PToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // 我的电脑MToolStripMenuItem
            // 
            this.我的电脑MToolStripMenuItem.Checked = true;
            this.我的电脑MToolStripMenuItem.CheckOnClick = true;
            this.我的电脑MToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.我的电脑MToolStripMenuItem.Name = "我的电脑MToolStripMenuItem";
            this.我的电脑MToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.我的电脑MToolStripMenuItem.Text = "我的电脑(&M)";
            this.我的电脑MToolStripMenuItem.Click += new System.EventHandler(this.查看ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
            // 
            // 我的所有共享WToolStripMenuItem
            // 
            this.我的所有共享WToolStripMenuItem.Name = "我的所有共享WToolStripMenuItem";
            this.我的所有共享WToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.我的所有共享WToolStripMenuItem.Text = "共享管理器(&L)";
            this.我的所有共享WToolStripMenuItem.Click += new System.EventHandler(this.我的所有共享WToolStripMenuItem_Click);
            // 
            // 网络资源浏览器NToolStripMenuItem
            // 
            this.网络资源浏览器NToolStripMenuItem.Name = "网络资源浏览器NToolStripMenuItem";
            this.网络资源浏览器NToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.网络资源浏览器NToolStripMenuItem.Text = "网络资源浏览器(&N)";
            this.网络资源浏览器NToolStripMenuItem.Click += new System.EventHandler(this.网络资源浏览器NToolStripMenuItem_Click);
            // 
            // 我下载的文件ToolStripMenuItem
            // 
            this.我下载的文件ToolStripMenuItem.Name = "我下载的文件ToolStripMenuItem";
            this.我下载的文件ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.我下载的文件ToolStripMenuItem.Text = "下载管理器(&D)";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 6);
            // 
            // vistaTopicToolStripMenuItem
            // 
            this.vistaTopicToolStripMenuItem.Name = "vistaTopicToolStripMenuItem";
            this.vistaTopicToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.vistaTopicToolStripMenuItem.Text = "Vista Topic";
            this.vistaTopicToolStripMenuItem.Click += new System.EventHandler(this.vistaTopicToolStripMenuItem_Click);
            // 
            // 皮肤ToolStripMenuItem
            // 
            this.皮肤ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统默认DToolStripMenuItem});
            this.皮肤ToolStripMenuItem.Name = "皮肤ToolStripMenuItem";
            this.皮肤ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.皮肤ToolStripMenuItem.Text = "皮肤(&S)";
            // 
            // 系统默认DToolStripMenuItem
            // 
            this.系统默认DToolStripMenuItem.Checked = true;
            this.系统默认DToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.系统默认DToolStripMenuItem.Name = "系统默认DToolStripMenuItem";
            this.系统默认DToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.系统默认DToolStripMenuItem.Text = "系统默认(&D)";
            this.系统默认DToolStripMenuItem.Click += new System.EventHandler(this.系统默认DToolStripMenuItem_Click);
            // 
            // 收藏AToolStripMenuItem
            // 
            this.收藏AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加到收藏夹ToolStripMenuItem,
            this.整理收藏夹OToolStripMenuItem,
            this.toolStripMenuItem1,
            this.newThingsToolStripMenuItem});
            this.收藏AToolStripMenuItem.Name = "收藏AToolStripMenuItem";
            this.收藏AToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.收藏AToolStripMenuItem.Text = "收藏(&A)";
            // 
            // 添加到收藏夹ToolStripMenuItem
            // 
            this.添加到收藏夹ToolStripMenuItem.Name = "添加到收藏夹ToolStripMenuItem";
            this.添加到收藏夹ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.添加到收藏夹ToolStripMenuItem.Text = "添加到收藏夹(&A)";
            // 
            // 整理收藏夹OToolStripMenuItem
            // 
            this.整理收藏夹OToolStripMenuItem.Name = "整理收藏夹OToolStripMenuItem";
            this.整理收藏夹OToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.整理收藏夹OToolStripMenuItem.Text = "整理收藏夹(&O)";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // newThingsToolStripMenuItem
            // 
            this.newThingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加到此收藏夹AToolStripMenuItem,
            this.toolStripMenuItem4});
            this.newThingsToolStripMenuItem.Name = "newThingsToolStripMenuItem";
            this.newThingsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newThingsToolStripMenuItem.Text = "new things";
            // 
            // 添加到此收藏夹AToolStripMenuItem
            // 
            this.添加到此收藏夹AToolStripMenuItem.Name = "添加到此收藏夹AToolStripMenuItem";
            this.添加到此收藏夹AToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.添加到此收藏夹AToolStripMenuItem.Text = "添加到此收藏夹(&A)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(169, 6);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内置浏览器IToolStripMenuItem,
            this.搜索在线用户UToolStripMenuItem,
            this.搜索共享资源ToolStripMenuItem,
            this.配置OToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // 内置浏览器IToolStripMenuItem
            // 
            this.内置浏览器IToolStripMenuItem.Name = "内置浏览器IToolStripMenuItem";
            this.内置浏览器IToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.内置浏览器IToolStripMenuItem.Text = "@网页浏览器(&I)";
            // 
            // 搜索在线用户UToolStripMenuItem
            // 
            this.搜索在线用户UToolStripMenuItem.Name = "搜索在线用户UToolStripMenuItem";
            this.搜索在线用户UToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.F)));
            this.搜索在线用户UToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.搜索在线用户UToolStripMenuItem.Text = "搜索在线用户(&U)";
            this.搜索在线用户UToolStripMenuItem.Click += new System.EventHandler(this.搜索在线用户UToolStripMenuItem_Click);
            // 
            // 搜索共享资源ToolStripMenuItem
            // 
            this.搜索共享资源ToolStripMenuItem.Name = "搜索共享资源ToolStripMenuItem";
            this.搜索共享资源ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.搜索共享资源ToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.搜索共享资源ToolStripMenuItem.Text = "@搜索共享资源(&S)";
            // 
            // 配置OToolStripMenuItem
            // 
            this.配置OToolStripMenuItem.Name = "配置OToolStripMenuItem";
            this.配置OToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.配置OToolStripMenuItem.Text = "配置(&O)...";
            this.配置OToolStripMenuItem.Click += new System.EventHandler(this.配置OToolStripMenuItem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.异常记录ToolStripMenuItem,
            this.关于HiveAToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 异常记录ToolStripMenuItem
            // 
            this.异常记录ToolStripMenuItem.Name = "异常记录ToolStripMenuItem";
            this.异常记录ToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.异常记录ToolStripMenuItem.Text = "异常记录！！！";
            this.异常记录ToolStripMenuItem.Click += new System.EventHandler(this.异常记录ToolStripMenuItem_Click);
            // 
            // 关于HiveAToolStripMenuItem
            // 
            this.关于HiveAToolStripMenuItem.Name = "关于HiveAToolStripMenuItem";
            this.关于HiveAToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.关于HiveAToolStripMenuItem.Text = "关于Hive(&A)...";
            // 
            // statusStripMain
            // 
            this.statusStripMain.Location = new System.Drawing.Point(0, 583);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(915, 22);
            this.statusStripMain.TabIndex = 8;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBack,
            this.toolStripButtonForward,
            this.toolStripSeparator1,
            this.toolStripButton4,
            this.toolStripButton3,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButtonOptions});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(915, 29);
            this.toolStripMain.TabIndex = 11;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // toolStripButtonBack
            // 
            this.toolStripButtonBack.Enabled = false;
            this.toolStripButtonBack.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonBack.Image")));
            this.toolStripButtonBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBack.Name = "toolStripButtonBack";
            this.toolStripButtonBack.Size = new System.Drawing.Size(81, 26);
            this.toolStripButtonBack.Text = "打开";
            this.toolStripButtonBack.Click += new System.EventHandler(this.toolStripButtonBack_Click);
            // 
            // toolStripButtonForward
            // 
            this.toolStripButtonForward.Enabled = false;
            this.toolStripButtonForward.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonForward.Image")));
            this.toolStripButtonForward.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonForward.Name = "toolStripButtonForward";
            this.toolStripButtonForward.Size = new System.Drawing.Size(81, 26);
            this.toolStripButtonForward.Text = "目录";
            this.toolStripButtonForward.Click += new System.EventHandler(this.toolStripButtonForward_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(97, 26);
            this.toolStripButton4.Text = "删除";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(73, 26);
            this.toolStripButton3.Text = "搜索";
            this.toolStripButton3.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(97, 26);
            this.toolStripButton2.Text = "下载";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // toolStripButtonOptions
            // 
            this.toolStripButtonOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOptions.Image")));
            this.toolStripButtonOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOptions.Name = "toolStripButtonOptions";
            this.toolStripButtonOptions.Size = new System.Drawing.Size(97, 26);
            this.toolStripButtonOptions.Text = "配置";
            this.toolStripButtonOptions.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // timerUdpNotifyServer
            // 
            this.timerUdpNotifyServer.Enabled = true;
            this.timerUdpNotifyServer.Interval = 2000;
            this.timerUdpNotifyServer.Tick += new System.EventHandler(this.timerUdpNotifyServer_Tick);
            // 
            // skinEngine1
            // 
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            this.skinEngine1.SkinStreamMain = ((System.IO.Stream)(resources.GetObject("skinEngine1.SkinStreamMain")));
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 605);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.dockPanelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HivE";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanelMain;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 在线用户OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 个人信息PToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 我的电脑MToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem 收藏AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加到收藏夹ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 整理收藏夹OToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton toolStripButtonBack;
        private System.Windows.Forms.ToolStripButton toolStripButtonForward;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem 配置OToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 我的所有共享WToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 我下载的文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 内置浏览器IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newThingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加到此收藏夹AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索在线用户UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索共享资源ToolStripMenuItem;
        private System.Windows.Forms.Timer timerUdpNotifyServer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem vistaTopicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 网络资源浏览器NToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem 皮肤ToolStripMenuItem;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.ToolStripMenuItem 系统默认DToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于HiveAToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonOptions;
    }
}