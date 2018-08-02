namespace exhaustLogin
{
    partial class mainPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainPanel));
            this.labelTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelStationName = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelWorkMode = new DevComponents.DotNetBar.LabelItem();
            this.labelUserName = new DevComponents.DotNetBar.LabelItem();
            this.labelCarDetectedCount = new DevComponents.DotNetBar.LabelItem();
            this.labelCarAtWaitCount = new DevComponents.DotNetBar.LabelItem();
            this.labelAutoPrint = new DevComponents.DotNetBar.LabelItem();
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.buttonItemLoginCar = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemPrint = new DevComponents.DotNetBar.ButtonItem();
            this.buttonAutoPrint = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemStastic = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemSettings = new DevComponents.DotNetBar.ButtonItem();
            this.buttonRegister = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemPersonelInf = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemRelogin = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItemExit = new DevComponents.DotNetBar.ButtonItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.richTextBoxPrintMsg = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.tabControlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTime
            // 
            this.labelTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(725, 33);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(32, 17);
            this.labelTime.TabIndex = 7;
            this.labelTime.Text = "时间";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(693, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 17);
            this.label10.TabIndex = 6;
            this.label10.Text = "时间:";
            // 
            // labelStationName
            // 
            this.labelStationName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStationName.AutoSize = true;
            this.labelStationName.BackColor = System.Drawing.Color.Transparent;
            this.labelStationName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelStationName.ForeColor = System.Drawing.Color.White;
            this.labelStationName.Location = new System.Drawing.Point(635, 33);
            this.labelStationName.Name = "labelStationName";
            this.labelStationName.Size = new System.Drawing.Size(56, 17);
            this.labelStationName.TabIndex = 5;
            this.labelStationName.Text = "检测站名";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.SlateGray;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(1350, 3);
            this.panel10.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tabControl1);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.ribbonBar1);
            this.panelEx1.Controls.Add(this.labelTime);
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Controls.Add(this.labelStationName);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1350, 752);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.Transparent;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 47);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = -1;
            this.tabControl1.Size = new System.Drawing.Size(1350, 686);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Text = "tabControl1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关闭ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelWorkMode,
            this.labelUserName,
            this.labelCarDetectedCount,
            this.labelCarAtWaitCount,
            this.labelAutoPrint});
            this.bar1.Location = new System.Drawing.Point(0, 733);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1350, 19);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 14;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelWorkMode
            // 
            this.labelWorkMode.Name = "labelWorkMode";
            this.labelWorkMode.Text = "工作状态";
            this.labelWorkMode.Width = 400;
            // 
            // labelUserName
            // 
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Text = "用户";
            this.labelUserName.Width = 100;
            // 
            // labelCarDetectedCount
            // 
            this.labelCarDetectedCount.Name = "labelCarDetectedCount";
            this.labelCarDetectedCount.Text = "已检车辆数";
            this.labelCarDetectedCount.Width = 150;
            // 
            // labelCarAtWaitCount
            // 
            this.labelCarAtWaitCount.Name = "labelCarAtWaitCount";
            this.labelCarAtWaitCount.Text = "等待车辆数";
            this.labelCarAtWaitCount.Width = 150;
            // 
            // labelAutoPrint
            // 
            this.labelAutoPrint.Name = "labelAutoPrint";
            this.labelAutoPrint.Text = "自动打印标识";
            this.labelAutoPrint.Width = 100;
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundMouseOverStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar1.BackgroundStyle.Class = "";
            this.ribbonBar1.ContainerControlProcessDialogKey = true;
            this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItemLoginCar,
            this.buttonItemPrint,
            this.buttonAutoPrint,
            this.buttonItemStastic,
            this.buttonItemSettings,
            this.buttonRegister,
            this.buttonItemPersonelInf,
            this.buttonItemRelogin,
            this.buttonItemExit});
            this.ribbonBar1.Location = new System.Drawing.Point(0, 0);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.Size = new System.Drawing.Size(1350, 47);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.ribbonBar1.TabIndex = 13;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyle.Class = "";
            // 
            // 
            // 
            this.ribbonBar1.TitleStyleMouseOver.Class = "";
            // 
            // buttonItemLoginCar
            // 
            this.buttonItemLoginCar.Name = "buttonItemLoginCar";
            this.buttonItemLoginCar.SubItemsExpandWidth = 14;
            this.buttonItemLoginCar.Text = "车辆登录";
            this.buttonItemLoginCar.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonItemPrint
            // 
            this.buttonItemPrint.Name = "buttonItemPrint";
            this.buttonItemPrint.SubItemsExpandWidth = 14;
            this.buttonItemPrint.Text = "报表打印";
            this.buttonItemPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // buttonAutoPrint
            // 
            this.buttonAutoPrint.Name = "buttonAutoPrint";
            this.buttonAutoPrint.SubItemsExpandWidth = 14;
            this.buttonAutoPrint.Text = "自动打印";
            this.buttonAutoPrint.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonItemStastic
            // 
            this.buttonItemStastic.Name = "buttonItemStastic";
            this.buttonItemStastic.SubItemsExpandWidth = 14;
            this.buttonItemStastic.Text = "统计查询";
            this.buttonItemStastic.Click += new System.EventHandler(this.buttonQuery_Click);
            // 
            // buttonItemSettings
            // 
            this.buttonItemSettings.Name = "buttonItemSettings";
            this.buttonItemSettings.SubItemsExpandWidth = 14;
            this.buttonItemSettings.Text = "系统设置";
            this.buttonItemSettings.Click += new System.EventHandler(this.buttonSysSettings_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.SubItemsExpandWidth = 14;
            this.buttonRegister.Text = "人员管理";
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // buttonItemPersonelInf
            // 
            this.buttonItemPersonelInf.Name = "buttonItemPersonelInf";
            this.buttonItemPersonelInf.SubItemsExpandWidth = 14;
            this.buttonItemPersonelInf.Text = "个人信息";
            this.buttonItemPersonelInf.Click += new System.EventHandler(this.button8_Click);
            // 
            // buttonItemRelogin
            // 
            this.buttonItemRelogin.Name = "buttonItemRelogin";
            this.buttonItemRelogin.SubItemsExpandWidth = 14;
            this.buttonItemRelogin.Text = "重新登录";
            this.buttonItemRelogin.Click += new System.EventHandler(this.buttonReLogin_Click);
            // 
            // buttonItemExit
            // 
            this.buttonItemExit.Name = "buttonItemExit";
            this.buttonItemExit.SubItemsExpandWidth = 14;
            this.buttonItemExit.Text = "系统退出";
            this.buttonItemExit.Click += new System.EventHandler(this.button6_Click);
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(698, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(295, 400);
            this.panel4.TabIndex = 12;
            this.panel4.Visible = false;
            // 
            // richTextBoxPrintMsg
            // 
            this.richTextBoxPrintMsg.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxPrintMsg.Name = "richTextBoxPrintMsg";
            this.richTextBoxPrintMsg.ReadOnly = true;
            this.richTextBoxPrintMsg.Size = new System.Drawing.Size(285, 362);
            this.richTextBoxPrintMsg.TabIndex = 1;
            this.richTextBoxPrintMsg.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "清空消息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(213, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "隐藏";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.panel4);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1350, 660);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "tabItem1";
            // 
            // mainPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1350, 752);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "mainPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "机动车环保检测系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainPanel_FormClosing);
            this.Load += new System.EventHandler(this.mainPanel_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.tabControlPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelStationName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem labelWorkMode;
        private DevComponents.DotNetBar.LabelItem labelUserName;
        private DevComponents.DotNetBar.LabelItem labelCarDetectedCount;
        private DevComponents.DotNetBar.LabelItem labelCarAtWaitCount;
        private DevComponents.DotNetBar.LabelItem labelAutoPrint;
        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem buttonItemLoginCar;
        private DevComponents.DotNetBar.ButtonItem buttonItemPrint;
        private DevComponents.DotNetBar.ButtonItem buttonItemStastic;
        private DevComponents.DotNetBar.ButtonItem buttonItemSettings;
        private DevComponents.DotNetBar.ButtonItem buttonRegister;
        private DevComponents.DotNetBar.ButtonItem buttonItemPersonelInf;
        private DevComponents.DotNetBar.ButtonItem buttonItemRelogin;
        private DevComponents.DotNetBar.ButtonItem buttonItemExit;
        private DevComponents.DotNetBar.ButtonItem buttonAutoPrint;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox richTextBoxPrintMsg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
    }
}

