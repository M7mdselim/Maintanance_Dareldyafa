using System.Windows.Forms;

namespace Maintenance_Application
{
    partial class CompleteReq
    {
        private System.ComponentModel.IContainer components = null;
        private Button loadbtn;
        private DataGridView FollowingReqGridView;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.loadbtn = new System.Windows.Forms.Button();
            this.FollowingReqGridView = new System.Windows.Forms.DataGridView();
            this.titleLabel = new System.Windows.Forms.Label();
            this.Updatebtn = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.usertxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.FollowingReqGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // loadbtn
            // 
            this.loadbtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.loadbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.loadbtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(219)))), ((int)(((byte)(200)))));
            this.loadbtn.Location = new System.Drawing.Point(695, 523);
            this.loadbtn.Margin = new System.Windows.Forms.Padding(2);
            this.loadbtn.Name = "loadbtn";
            this.loadbtn.Size = new System.Drawing.Size(154, 35);
            this.loadbtn.TabIndex = 1;
            this.loadbtn.Text = "بحث";
            this.loadbtn.UseVisualStyleBackColor = false;
            this.loadbtn.Click += new System.EventHandler(this.loadbtn_Click);
            // 
            // FollowingReqGridView
            // 
            this.FollowingReqGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.FollowingReqGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.FollowingReqGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FollowingReqGridView.Location = new System.Drawing.Point(12, 95);
            this.FollowingReqGridView.Margin = new System.Windows.Forms.Padding(2);
            this.FollowingReqGridView.Name = "FollowingReqGridView";
            this.FollowingReqGridView.RowTemplate.Height = 24;
            this.FollowingReqGridView.Size = new System.Drawing.Size(837, 424);
            this.FollowingReqGridView.TabIndex = 2;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.titleLabel.Location = new System.Drawing.Point(355, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(175, 33);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "تاكيد باتمام البلاغ";
            // 
            // Updatebtn
            // 
            this.Updatebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.Updatebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Updatebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(219)))), ((int)(((byte)(200)))));
            this.Updatebtn.Location = new System.Drawing.Point(12, 524);
            this.Updatebtn.Name = "Updatebtn";
            this.Updatebtn.Size = new System.Drawing.Size(244, 35);
            this.Updatebtn.TabIndex = 5;
            this.Updatebtn.Text = "تم انجاز العطل من قبل الصيانه";
            this.Updatebtn.UseVisualStyleBackColor = false;
            this.Updatebtn.Click += new System.EventHandler(this.Updatebtn_Click);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.White;
            this.backButton.BackgroundImage = global::Maintenance_Application.Properties.Resources.icons8_back_button_502;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.backButton.ForeColor = System.Drawing.Color.IndianRed;
            this.backButton.Location = new System.Drawing.Point(12, 12);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(70, 72);
            this.backButton.TabIndex = 23;
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Schoolbook", 21.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.label1.Location = new System.Drawing.Point(404, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 34);
            this.label1.TabIndex = 47;
            this.label1.Text = "صيانه";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usertxt
            // 
            this.usertxt.Location = new System.Drawing.Point(734, 25);
            this.usertxt.Name = "usertxt";
            this.usertxt.ReadOnly = true;
            this.usertxt.Size = new System.Drawing.Size(114, 20);
            this.usertxt.TabIndex = 48;
            // 
            // CompleteReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 583);
            this.Controls.Add(this.usertxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.Updatebtn);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.FollowingReqGridView);
            this.Controls.Add(this.loadbtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CompleteReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "متابعه بلاغات";
            this.Load += new System.EventHandler(this.CompleteReq_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FollowingReqGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label titleLabel;
        private Button Updatebtn;
        private Button backButton;
        private Label label1;
        private TextBox usertxt;
    }
}
