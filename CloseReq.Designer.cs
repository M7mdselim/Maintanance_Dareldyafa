using System.Windows.Forms;


namespace Maintenance_Application
{
    partial class CloseReq
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
            this.notcompletedreqtxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.returnstatusbtn = new System.Windows.Forms.Button();
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
            this.titleLabel.Location = new System.Drawing.Point(392, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(108, 33);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "قفل البلاغ";
            // 
            // Updatebtn
            // 
            this.Updatebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(44)))), ((int)(((byte)(87)))));
            this.Updatebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.Updatebtn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(219)))), ((int)(((byte)(200)))));
            this.Updatebtn.Location = new System.Drawing.Point(12, 524);
            this.Updatebtn.Name = "Updatebtn";
            this.Updatebtn.Size = new System.Drawing.Size(146, 35);
            this.Updatebtn.TabIndex = 5;
            this.Updatebtn.Text = "قفل البلاغ";
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
            // notcompletedreqtxt
            // 
            this.notcompletedreqtxt.Location = new System.Drawing.Point(385, 527);
            this.notcompletedreqtxt.Name = "notcompletedreqtxt";
            this.notcompletedreqtxt.Size = new System.Drawing.Size(83, 20);
            this.notcompletedreqtxt.TabIndex = 49;
            this.notcompletedreqtxt.TextChanged += new System.EventHandler(this.notcompletedreqtxt_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(474, 530);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "عطل لم يكتمل";
            // 
            // returnstatusbtn
            // 
            this.returnstatusbtn.Location = new System.Drawing.Point(338, 526);
            this.returnstatusbtn.Name = "returnstatusbtn";
            this.returnstatusbtn.Size = new System.Drawing.Size(41, 23);
            this.returnstatusbtn.TabIndex = 51;
            this.returnstatusbtn.Text = "تاكيد";
            this.returnstatusbtn.UseVisualStyleBackColor = true;
            this.returnstatusbtn.Click += new System.EventHandler(this.returnstatusbtn_Click);
            // 
            // CloseReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 583);
            this.Controls.Add(this.returnstatusbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.notcompletedreqtxt);
            this.Controls.Add(this.usertxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.Updatebtn);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.FollowingReqGridView);
            this.Controls.Add(this.loadbtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CloseReq";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "متابعه بلاغات";
            this.Load += new System.EventHandler(this.CloseReq_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FollowingReqGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label titleLabel;
        private Button Updatebtn;
        private Button backButton;
        private Label label1;
        private TextBox usertxt;
        private TextBox notcompletedreqtxt;
        private Label label2;
        private Button returnstatusbtn;
    }
}
