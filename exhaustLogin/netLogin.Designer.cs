namespace exhaustLogin
{
    partial class netLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(netLogin));
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxJCCS = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.buttonSave = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.listBoxCarAtWait = new System.Windows.Forms.ListBox();
            this.labelX37 = new DevComponents.DotNetBar.LabelX();
            this.axAC_Vehicle1 = new AxVehicle_Login.AxAC_Vehicle();
            this.contextMenuStrip2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAC_Vehicle1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.刷新ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(153, 70);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.刷新ToolStripMenuItem.Text = "刷新";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // textBoxJCCS
            // 
            this.textBoxJCCS.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxJCCS.AutoSize = true;
            this.textBoxJCCS.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.textBoxJCCS.ForeColor = System.Drawing.Color.Red;
            this.textBoxJCCS.Location = new System.Drawing.Point(248, 4);
            this.textBoxJCCS.Name = "textBoxJCCS";
            this.textBoxJCCS.Size = new System.Drawing.Size(23, 24);
            this.textBoxJCCS.TabIndex = 131;
            this.textBoxJCCS.Text = "1";
            // 
            // label22
            // 
            this.label22.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(158, 10);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(85, 16);
            this.label22.TabIndex = 130;
            this.label22.Text = "环检次数:";
            // 
            // buttonSave
            // 
            this.buttonSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonSave.Location = new System.Drawing.Point(443, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(138, 26);
            this.buttonSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonSave.TabIndex = 132;
            this.buttonSave.Text = "保  存";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.textBoxJCCS);
            this.panelEx1.Controls.Add(this.buttonSave);
            this.panelEx1.Controls.Add(this.label22);
            this.panelEx1.Location = new System.Drawing.Point(1, 574);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(941, 32);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // panelEx4
            // 
            this.panelEx4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.buttonX1);
            this.panelEx4.Controls.Add(this.textBoxX1);
            this.panelEx4.Controls.Add(this.listBoxCarAtWait);
            this.panelEx4.Controls.Add(this.labelX37);
            this.panelEx4.Location = new System.Drawing.Point(944, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(242, 606);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 4;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(138, 582);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(101, 21);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 130;
            this.buttonX1.Text = "查  找";
            this.buttonX1.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Location = new System.Drawing.Point(4, 582);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(133, 21);
            this.textBoxX1.TabIndex = 42;
            // 
            // listBoxCarAtWait
            // 
            this.listBoxCarAtWait.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCarAtWait.ContextMenuStrip = this.contextMenuStrip2;
            this.listBoxCarAtWait.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxCarAtWait.ForeColor = System.Drawing.Color.Black;
            this.listBoxCarAtWait.FormattingEnabled = true;
            this.listBoxCarAtWait.ItemHeight = 21;
            this.listBoxCarAtWait.Location = new System.Drawing.Point(3, 29);
            this.listBoxCarAtWait.Name = "listBoxCarAtWait";
            this.listBoxCarAtWait.Size = new System.Drawing.Size(235, 550);
            this.listBoxCarAtWait.TabIndex = 7;
            // 
            // labelX37
            // 
            // 
            // 
            // 
            this.labelX37.BackgroundStyle.Class = "";
            this.labelX37.Location = new System.Drawing.Point(4, 3);
            this.labelX37.Name = "labelX37";
            this.labelX37.Size = new System.Drawing.Size(65, 23);
            this.labelX37.TabIndex = 6;
            this.labelX37.Text = "待检列表";
            // 
            // axAC_Vehicle1
            // 
            this.axAC_Vehicle1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axAC_Vehicle1.Location = new System.Drawing.Point(1, 3);
            this.axAC_Vehicle1.Name = "axAC_Vehicle1";
            this.axAC_Vehicle1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAC_Vehicle1.OcxState")));
            this.axAC_Vehicle1.Size = new System.Drawing.Size(941, 565);
            this.axAC_Vehicle1.TabIndex = 5;
            // 
            // netLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 608);
            this.Controls.Add(this.axAC_Vehicle1);
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Name = "netLogin";
            this.Text = "netLogin";
            this.Load += new System.EventHandler(this.netLogin_Load);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAC_Vehicle1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label textBoxJCCS;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private DevComponents.DotNetBar.ButtonX buttonSave;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private System.Windows.Forms.ListBox listBoxCarAtWait;
        private DevComponents.DotNetBar.LabelX labelX37;
        private AxVehicle_Login.AxAC_Vehicle axAC_Vehicle1;
    }
}