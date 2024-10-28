namespace Maintenance_Application
{
    partial class Home
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
            this.CashierFormbtn = new System.Windows.Forms.Button();
            this.Followreqbtn = new System.Windows.Forms.Button();
            this.CloseReqbtn = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.signupbtn = new System.Windows.Forms.Button();
            this.updateform = new System.Windows.Forms.Button();
            this.changepassbtn = new System.Windows.Forms.Button();
            this.Monthlyreportbtn = new System.Windows.Forms.Button();
            this.Extrareqbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CashierFormbtn
            // 
            this.CashierFormbtn.BackColor = System.Drawing.Color.Black;
            this.CashierFormbtn.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.CashierFormbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.CashierFormbtn.Location = new System.Drawing.Point(34, 15);
            this.CashierFormbtn.Name = "CashierFormbtn";
            this.CashierFormbtn.Size = new System.Drawing.Size(316, 98);
            this.CashierFormbtn.TabIndex = 16;
            this.CashierFormbtn.Text = "تقديم بلاغ";
            this.CashierFormbtn.UseVisualStyleBackColor = false;
            this.CashierFormbtn.Click += new System.EventHandler(this.CashierFormbtn_Click);
            // 
            // Followreqbtn
            // 
            this.Followreqbtn.BackColor = System.Drawing.Color.Black;
            this.Followreqbtn.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Followreqbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.Followreqbtn.Location = new System.Drawing.Point(34, 119);
            this.Followreqbtn.Name = "Followreqbtn";
            this.Followreqbtn.Size = new System.Drawing.Size(316, 76);
            this.Followreqbtn.TabIndex = 17;
            this.Followreqbtn.Text = "بلاغات مفتوحه";
            this.Followreqbtn.UseVisualStyleBackColor = false;
            this.Followreqbtn.Click += new System.EventHandler(this.DailyReportbtn_Click);
            // 
            // CloseReqbtn
            // 
            this.CloseReqbtn.BackColor = System.Drawing.Color.Black;
            this.CloseReqbtn.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.CloseReqbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.CloseReqbtn.Location = new System.Drawing.Point(34, 201);
            this.CloseReqbtn.Name = "CloseReqbtn";
            this.CloseReqbtn.Size = new System.Drawing.Size(314, 76);
            this.CloseReqbtn.TabIndex = 19;
            this.CloseReqbtn.Text = "قفل بلاغ";
            this.CloseReqbtn.UseVisualStyleBackColor = false;
            this.CloseReqbtn.Click += new System.EventHandler(this.CustomerReportBtn_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Black;
            this.exitButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.exitButton.ForeColor = System.Drawing.Color.IndianRed;
            this.exitButton.Location = new System.Drawing.Point(119, 496);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(130, 52);
            this.exitButton.TabIndex = 20;
            this.exitButton.Text = "خروج";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // signupbtn
            // 
            this.signupbtn.BackColor = System.Drawing.Color.Black;
            this.signupbtn.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.signupbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.signupbtn.Location = new System.Drawing.Point(225, 567);
            this.signupbtn.Name = "signupbtn";
            this.signupbtn.Size = new System.Drawing.Size(125, 35);
            this.signupbtn.TabIndex = 21;
            this.signupbtn.Text = "اضافه حساب";
            this.signupbtn.UseVisualStyleBackColor = false;
            this.signupbtn.Visible = false;
            this.signupbtn.Click += new System.EventHandler(this.signupbtn_Click);
            // 
            // updateform
            // 
            this.updateform.BackColor = System.Drawing.Color.Black;
            this.updateform.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updateform.ForeColor = System.Drawing.Color.IndianRed;
            this.updateform.Location = new System.Drawing.Point(32, 354);
            this.updateform.Name = "updateform";
            this.updateform.Size = new System.Drawing.Size(316, 65);
            this.updateform.TabIndex = 22;
            this.updateform.Text = "تقرير يومي";
            this.updateform.UseVisualStyleBackColor = false;
            this.updateform.Click += new System.EventHandler(this.updateform_Click);
            // 
            // changepassbtn
            // 
            this.changepassbtn.BackColor = System.Drawing.Color.Black;
            this.changepassbtn.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.changepassbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.changepassbtn.Location = new System.Drawing.Point(32, 567);
            this.changepassbtn.Name = "changepassbtn";
            this.changepassbtn.Size = new System.Drawing.Size(121, 35);
            this.changepassbtn.TabIndex = 23;
            this.changepassbtn.Text = "تغيير كلمه سر";
            this.changepassbtn.UseVisualStyleBackColor = false;
            this.changepassbtn.Visible = false;
            this.changepassbtn.Click += new System.EventHandler(this.changepassbtn_Click);
            // 
            // Monthlyreportbtn
            // 
            this.Monthlyreportbtn.BackColor = System.Drawing.Color.Black;
            this.Monthlyreportbtn.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Monthlyreportbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.Monthlyreportbtn.Location = new System.Drawing.Point(34, 425);
            this.Monthlyreportbtn.Name = "Monthlyreportbtn";
            this.Monthlyreportbtn.Size = new System.Drawing.Size(316, 65);
            this.Monthlyreportbtn.TabIndex = 24;
            this.Monthlyreportbtn.Text = "تقرير شهري";
            this.Monthlyreportbtn.UseVisualStyleBackColor = false;
            this.Monthlyreportbtn.Click += new System.EventHandler(this.Monthlyreportbtn_Click_1);
            // 
            // Extrareqbtn
            // 
            this.Extrareqbtn.BackColor = System.Drawing.Color.Black;
            this.Extrareqbtn.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Extrareqbtn.ForeColor = System.Drawing.Color.IndianRed;
            this.Extrareqbtn.Location = new System.Drawing.Point(32, 283);
            this.Extrareqbtn.Name = "Extrareqbtn";
            this.Extrareqbtn.Size = new System.Drawing.Size(316, 65);
            this.Extrareqbtn.TabIndex = 25;
            this.Extrareqbtn.Text = "طلب شراء";
            this.Extrareqbtn.UseVisualStyleBackColor = false;
            this.Extrareqbtn.Click += new System.EventHandler(this.Extrareqbtn_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(386, 610);
            this.Controls.Add(this.Extrareqbtn);
            this.Controls.Add(this.Monthlyreportbtn);
            this.Controls.Add(this.changepassbtn);
            this.Controls.Add(this.updateform);
            this.Controls.Add(this.signupbtn);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.CloseReqbtn);
            this.Controls.Add(this.Followreqbtn);
            this.Controls.Add(this.CashierFormbtn);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.Home_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CashierFormbtn;
        private System.Windows.Forms.Button Followreqbtn;
        private System.Windows.Forms.Button CloseReqbtn;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button signupbtn;
        private System.Windows.Forms.Button updateform;
        private System.Windows.Forms.Button changepassbtn;
        private System.Windows.Forms.Button Monthlyreportbtn;
        private System.Windows.Forms.Button Extrareqbtn;
    }
}