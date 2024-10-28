using Maintenance_Application;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maintenance_Application
{
    public partial class Home : Form
    {
        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;


        private string _username;

        public Home(string username)
        {


            _username = username;
            InitializeComponent();
            // Store initial form size
            _initialFormWidth = this.Width;
            _initialFormHeight = this.Height;

            // Store initial size and location of all controls
            _controlsInfo = new ControlInfo[this.Controls.Count];
            for (int i = 0; i < this.Controls.Count; i++)
            {
                Control c = this.Controls[i];
                _controlsInfo[i] = new ControlInfo(c.Left, c.Top, c.Width, c.Height, c.Font.Size);
            }

            // Set event handler for form resize
            this.Resize += Home_Resize;
        }

        private void SetButtonVisibilityBasedOnRole()
        {
            int roleID = GetRoleIdForCurrentUser();

            if (roleID == 1)
            {

                signupbtn.Visible = true;
                changepassbtn.Visible = true;



            }

            else
            {
                signupbtn.Visible = false;
                changepassbtn.Visible = false;
            }
        }

        private int GetRoleIdForCurrentUser()
        {
            int roleID = 0;
            string username = _username; // Assuming this is how you store the logged-in username

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                string query = @"
               SELECT RoleID , Fullname
            FROM Users
            WHERE Fullname = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out roleID))
                        {
                            return roleID;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }

            return roleID;
        }





        private void Home_Load(object sender, EventArgs e)
        {
            SetButtonVisibilityBasedOnRole();// Additional initialization if needed
        }

        private void DailyReportbtn_Click(object sender, EventArgs e)
        {

            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1 || roleID == 3)
            {
                this.Hide();
            FollowReq followreq = new FollowReq(_username);
            followreq.ShowDialog();
            this.Close();

            }
            else
            {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }


        }

        private void MonthlyReportbtn_Click(object sender, EventArgs e)
        {

            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1 || roleID == 3)
            {
                this.Hide();
            CompleteReq completereq = new CompleteReq(_username);
            completereq.ShowDialog();
            this.Close();
            }
            else
            {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }
        }

        private void CustomerReportBtn_Click(object sender, EventArgs e)
        {

            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1 || roleID == 2)
            {

                this.Hide();
            CloseReq closereq = new CloseReq(_username);
            closereq.ShowDialog();
            this.Close();
            }
            else
            {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }


        }

        private void CashierFormbtn_Click(object sender, EventArgs e)
        {
            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1 || roleID == 2 || roleID == 3)
            {



                this.Hide();
                Form1 makereq = new Form1(_username);
                makereq.ShowDialog();
                this.Close();
            }
            else {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }
        }

        private void Home_Resize(object sender, EventArgs e)
        {
            float widthRatio = this.Width / _initialFormWidth;
            float heightRatio = this.Height / _initialFormHeight;
            ResizeControls(this.Controls, widthRatio, heightRatio);
        }

        private void ResizeControls(Control.ControlCollection controls, float widthRatio, float heightRatio)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                Control control = controls[i];
                ControlInfo controlInfo = _controlsInfo[i];

                control.Left = (int)(controlInfo.Left * widthRatio);
                control.Top = (int)(controlInfo.Top * heightRatio);
                control.Width = (int)(controlInfo.Width * widthRatio);
                control.Height = (int)(controlInfo.Height * heightRatio);

                // Adjust font size
                control.Font = new Font(control.Font.FontFamily, controlInfo.FontSize * Math.Min(widthRatio, heightRatio));
            }
        }

        private class ControlInfo
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public float FontSize { get; set; }

            public ControlInfo(int left, int top, int width, int height, float fontSize)
            {
                Left = left;
                Top = top;
                Width = width;
                Height = height;
                FontSize = fontSize;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("تاكيد خروج ؟؟؟",
                                                 "Confirm Exit",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }




        private void signupbtn_Click(object sender, EventArgs e)
        {
            
        }

        private void updateform_Click(object sender, EventArgs e)
        {

            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1)
            {

                this.Hide();
            DailyReport DP = new DailyReport(_username);
            DP.ShowDialog();
            this.Close();

            }
            else
            {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }
        }

        private void changepassbtn_Click(object sender, EventArgs e)
        {
        }

        private void Monthlyreportbtn_Click_1(object sender, EventArgs e)
        {
            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1)
            {
                this.Hide();
            MonthlyReport MP = new MonthlyReport(_username);
            MP.ShowDialog();
            this.Close();

            }
            else
            {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }

        }

        private void Extrareqbtn_Click(object sender, EventArgs e)
        {

            int roleID = GetRoleIdForCurrentUser();
            if (roleID == 1 || roleID == 3)
            {
                this.Hide();
            Extrareq EP = new Extrareq(_username);
            EP.ShowDialog();
            this.Close();

            }
            else
            {
                MessageBox.Show("Not Allowed to Open this Form :)");
            }
        }
    }
}

