using System.Windows.Forms;

namespace Maintenance_Application
{
    partial class MonthlyReport
    {
        private void InitializeComponent()
        {
            this.datePicker = new System.Windows.Forms.DateTimePicker();
            this.loadReportButton = new System.Windows.Forms.Button();
            this.FollowingReqGridView = new System.Windows.Forms.DataGridView();
            this.PrintButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.ExportToExcelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.filteringTxtBox = new System.Windows.Forms.TextBox();
            this.filterselectioncombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FollowingReqGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // datePicker
            // 
            this.datePicker.Location = new System.Drawing.Point(465, 552);
            this.datePicker.Name = "datePicker";
            this.datePicker.Size = new System.Drawing.Size(251, 20);
            this.datePicker.TabIndex = 0;
            // 
            // loadReportButton
            // 
            this.loadReportButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.loadReportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.loadReportButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(219)))), ((int)(((byte)(200)))));
            this.loadReportButton.Location = new System.Drawing.Point(722, 529);
            this.loadReportButton.Name = "loadReportButton";
            this.loadReportButton.Size = new System.Drawing.Size(177, 65);
            this.loadReportButton.TabIndex = 1;
            this.loadReportButton.Text = "بحث";
            this.loadReportButton.UseVisualStyleBackColor = false;
            this.loadReportButton.Click += new System.EventHandler(this.loadReportButton_Click);
            // 
            // FollowingReqGridView
            // 
            this.FollowingReqGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FollowingReqGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.FollowingReqGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FollowingReqGridView.Location = new System.Drawing.Point(10, 86);
            this.FollowingReqGridView.Name = "FollowingReqGridView";
            this.FollowingReqGridView.RowTemplate.Height = 24;
            this.FollowingReqGridView.Size = new System.Drawing.Size(889, 407);
            this.FollowingReqGridView.TabIndex = 2;
            // 
            // PrintButton
            // 
            this.PrintButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.PrintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.PrintButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(219)))), ((int)(((byte)(200)))));
            this.PrintButton.Location = new System.Drawing.Point(12, 529);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(177, 65);
            this.PrintButton.TabIndex = 4;
            this.PrintButton.Text = "طباعة";
            this.PrintButton.UseVisualStyleBackColor = false;
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.BackgroundImage = global::Maintenance_Application.Properties.Resources.icons8_back_button_502;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.backButton.ForeColor = System.Drawing.Color.IndianRed;
            this.backButton.Location = new System.Drawing.Point(10, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(70, 68);
            this.backButton.TabIndex = 21;
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // ExportToExcelButton
            // 
            this.ExportToExcelButton.Location = new System.Drawing.Point(10, 498);
            this.ExportToExcelButton.Name = "ExportToExcelButton";
            this.ExportToExcelButton.Size = new System.Drawing.Size(70, 23);
            this.ExportToExcelButton.TabIndex = 22;
            this.ExportToExcelButton.Text = "استخراج ";
            this.ExportToExcelButton.UseVisualStyleBackColor = true;
            this.ExportToExcelButton.Click += new System.EventHandler(this.ExportToExcelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Schoolbook", 21.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.label2.Location = new System.Drawing.Point(372, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 34);
            this.label2.TabIndex = 50;
            this.label2.Text = "Maintenance";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.label3.Location = new System.Drawing.Point(383, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 33);
            this.label3.TabIndex = 49;
            this.label3.Text = "تقرير شهري صيانه";
            // 
            // filteringTxtBox
            // 
            this.filteringTxtBox.Location = new System.Drawing.Point(209, 574);
            this.filteringTxtBox.Name = "filteringTxtBox";
            this.filteringTxtBox.Size = new System.Drawing.Size(229, 20);
            this.filteringTxtBox.TabIndex = 58;
            this.filteringTxtBox.TextChanged += new System.EventHandler(this.filteringTxtBox_TextChanged);
            // 
            // filterselectioncombo
            // 
            this.filterselectioncombo.FormattingEnabled = true;
            this.filterselectioncombo.Items.AddRange(new object[] {
            "اسم المبلغ",
            "المكان",
            "الغرفه",
            "القائم_على_العطل",
            "مغلق_البلاغ"});
            this.filterselectioncombo.Location = new System.Drawing.Point(209, 528);
            this.filterselectioncombo.Name = "filterselectioncombo";
            this.filterselectioncombo.Size = new System.Drawing.Size(121, 21);
            this.filterselectioncombo.TabIndex = 57;
            this.filterselectioncombo.SelectedIndexChanged += new System.EventHandler(this.filterselectioncombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(336, 529);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 22);
            this.label1.TabIndex = 56;
            this.label1.Text = "بحث عن طريق";
            // 
            // MonthlyReport
            // 
            this.ClientSize = new System.Drawing.Size(911, 617);
            this.Controls.Add(this.filteringTxtBox);
            this.Controls.Add(this.filterselectioncombo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ExportToExcelButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.PrintButton);
            this.Controls.Add(this.FollowingReqGridView);
            this.Controls.Add(this.loadReportButton);
            this.Controls.Add(this.datePicker);
            this.Name = "MonthlyReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تقرير يومي";
            this.Load += new System.EventHandler(this.MonthlyReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FollowingReqGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DateTimePicker datePicker;
        private System.Windows.Forms.Button loadReportButton;
        private System.Windows.Forms.DataGridView FollowingReqGridView;
        private Button PrintButton;
        private Button backButton;
        private Button ExportToExcelButton;
        private Label label2;
        private Label label3;
        private TextBox filteringTxtBox;
        private ComboBox filterselectioncombo;
        private Label label1;
    }
}
