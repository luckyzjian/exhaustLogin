namespace exhaustLogin
{
    partial class NetcarLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetcarLogin));
            this.panel2 = new System.Windows.Forms.Panel();
            this.axAC_Vehicle1 = new AxVehicle_Login.AxAC_Vehicle();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axAC_Vehicle1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.axAC_Vehicle1);
            this.panel2.Location = new System.Drawing.Point(5, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1024, 602);
            this.panel2.TabIndex = 0;
            // 
            // axAC_Vehicle1
            // 
            this.axAC_Vehicle1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAC_Vehicle1.Location = new System.Drawing.Point(0, 0);
            this.axAC_Vehicle1.Name = "axAC_Vehicle1";
            this.axAC_Vehicle1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAC_Vehicle1.OcxState")));
            this.axAC_Vehicle1.Size = new System.Drawing.Size(1024, 602);
            this.axAC_Vehicle1.TabIndex = 0;
            // 
            // NetcarLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1053, 618);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NetcarLogin";
            this.Text = "carLogin";
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axAC_Vehicle1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private AxVehicle_Login.AxAC_Vehicle axAC_Vehicle1;
    }
}