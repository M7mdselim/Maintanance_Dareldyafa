namespace Maintenance_Application
{
    partial class Form1
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
            this.Fullusernametxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Maintenancetypecombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Areacombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Roomcombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Notestxt = new System.Windows.Forms.TextBox();
            this.Submitbtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Fullusernametxt
            // 
            this.Fullusernametxt.Location = new System.Drawing.Point(47, 122);
            this.Fullusernametxt.Name = "Fullusernametxt";
            this.Fullusernametxt.ReadOnly = true;
            this.Fullusernametxt.Size = new System.Drawing.Size(254, 20);
            this.Fullusernametxt.TabIndex = 0;
            this.Fullusernametxt.TextChanged += new System.EventHandler(this.Fullusernametxt_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(325, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "اسم المبلغ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(325, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "نوع العطل";
            // 
            // Maintenancetypecombo
            // 
            this.Maintenancetypecombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Maintenancetypecombo.FormattingEnabled = true;
            this.Maintenancetypecombo.Location = new System.Drawing.Point(47, 173);
            this.Maintenancetypecombo.Name = "Maintenancetypecombo";
            this.Maintenancetypecombo.Size = new System.Drawing.Size(254, 21);
            this.Maintenancetypecombo.TabIndex = 3;
            this.Maintenancetypecombo.SelectedIndexChanged += new System.EventHandler(this.Maintenancetypecombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(349, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 22);
            this.label3.TabIndex = 4;
            this.label3.Text = "المكان";
            // 
            // Areacombo
            // 
            this.Areacombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Areacombo.FormattingEnabled = true;
            this.Areacombo.Location = new System.Drawing.Point(47, 231);
            this.Areacombo.Name = "Areacombo";
            this.Areacombo.Size = new System.Drawing.Size(254, 21);
            this.Areacombo.TabIndex = 5;
            this.Areacombo.SelectedIndexChanged += new System.EventHandler(this.Areacombo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(354, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "غرفه";
            // 
            // Roomcombo
            // 
            this.Roomcombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Roomcombo.FormattingEnabled = true;
            this.Roomcombo.Location = new System.Drawing.Point(47, 286);
            this.Roomcombo.Name = "Roomcombo";
            this.Roomcombo.Size = new System.Drawing.Size(254, 21);
            this.Roomcombo.TabIndex = 7;
            this.Roomcombo.SelectedIndexChanged += new System.EventHandler(this.Roomcombo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(332, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "ملاحظات";
            // 
            // Notestxt
            // 
            this.Notestxt.Location = new System.Drawing.Point(47, 347);
            this.Notestxt.Name = "Notestxt";
            this.Notestxt.Size = new System.Drawing.Size(254, 20);
            this.Notestxt.TabIndex = 9;
            this.Notestxt.TextChanged += new System.EventHandler(this.Notestxt_TextChanged);
            // 
            // Submitbtn
            // 
            this.Submitbtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Submitbtn.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Submitbtn.Location = new System.Drawing.Point(121, 403);
            this.Submitbtn.Name = "Submitbtn";
            this.Submitbtn.Size = new System.Drawing.Size(139, 50);
            this.Submitbtn.TabIndex = 10;
            this.Submitbtn.Text = "تاكيد";
            this.Submitbtn.UseVisualStyleBackColor = false;
            this.Submitbtn.Click += new System.EventHandler(this.Submitbtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(136, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 44);
            this.label6.TabIndex = 11;
            this.label6.Text = "ابلاغ بعطل";
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Transparent;
            this.backButton.BackgroundImage = global::Maintenance_Application.Properties.Resources.icons8_back_button_502;
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.backButton.ForeColor = System.Drawing.Color.IndianRed;
            this.backButton.Location = new System.Drawing.Point(12, 7);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(51, 51);
            this.backButton.TabIndex = 22;
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 486);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Submitbtn);
            this.Controls.Add(this.Notestxt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Roomcombo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Areacombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Maintenancetypecombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Fullusernametxt);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Fullusernametxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox Maintenancetypecombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Areacombo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Roomcombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Notestxt;
        private System.Windows.Forms.Button Submitbtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button backButton;
    }
}

