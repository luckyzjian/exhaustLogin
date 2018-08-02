namespace exhaustLogin
{
    partial class reportSafeViwStatistics
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.已检车辆信息BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.NEIMENG_VMASDataSet = new exhaustLogin.NEIMENG_VMASDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.已检车辆信息TableAdapter = new exhaustLogin.NEIMENG_VMASDataSetTableAdapters.已检车辆信息TableAdapter();
            this.NEIMENG_VMASDataSet1 = new exhaustLogin.NEIMENG_VMASDataSet1();
            this.安检记录BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.安检记录TableAdapter = new exhaustLogin.NEIMENG_VMASDataSet1TableAdapters.安检记录TableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.已检车辆信息BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NEIMENG_VMASDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NEIMENG_VMASDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.安检记录BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // 已检车辆信息BindingSource
            // 
            this.已检车辆信息BindingSource.DataMember = "已检车辆信息";
            this.已检车辆信息BindingSource.DataSource = this.NEIMENG_VMASDataSet;
            // 
            // NEIMENG_VMASDataSet
            // 
            this.NEIMENG_VMASDataSet.DataSetName = "NEIMENG_VMASDataSet";
            this.NEIMENG_VMASDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.安检记录BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "exhaustLogin.ReportSafeStatistics.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ShowPrintButton = false;
            this.reportViewer1.Size = new System.Drawing.Size(839, 466);
            this.reportViewer1.TabIndex = 0;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // 已检车辆信息TableAdapter
            // 
            this.已检车辆信息TableAdapter.ClearBeforeFill = true;
            // 
            // NEIMENG_VMASDataSet1
            // 
            this.NEIMENG_VMASDataSet1.DataSetName = "NEIMENG_VMASDataSet1";
            this.NEIMENG_VMASDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // 安检记录BindingSource
            // 
            this.安检记录BindingSource.DataMember = "安检记录";
            this.安检记录BindingSource.DataSource = this.NEIMENG_VMASDataSet1;
            // 
            // 安检记录TableAdapter
            // 
            this.安检记录TableAdapter.ClearBeforeFill = true;
            // 
            // reportSafeViwStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 466);
            this.Controls.Add(this.reportViewer1);
            this.Name = "reportSafeViwStatistics";
            this.Text = "统计查询";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.reportView_panelFormClosing);
            this.Load += new System.EventHandler(this.reportView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.已检车辆信息BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NEIMENG_VMASDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NEIMENG_VMASDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.安检记录BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource 已检车辆信息BindingSource;
        private NEIMENG_VMASDataSet NEIMENG_VMASDataSet;
        private NEIMENG_VMASDataSetTableAdapters.已检车辆信息TableAdapter 已检车辆信息TableAdapter;
        private System.Windows.Forms.BindingSource 安检记录BindingSource;
        private NEIMENG_VMASDataSet1 NEIMENG_VMASDataSet1;
        private NEIMENG_VMASDataSet1TableAdapters.安检记录TableAdapter 安检记录TableAdapter;
    }
}