using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentFactory.Krypton.Toolkit;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Maintenance_Application
{
    public partial class Login : KryptonForm
    {
        private string ConnectionString;
        public static string LoggedInUsername { get; private set; }
        public static int LoggedInUserRole { get; private set; }


        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;


        public Login()
        {
            InitializeComponent();
            ConnectionString = DatabaseConfig.connectionString;
            this.AcceptButton = loginbtn; // Set the AcceptButton property








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


        private void loginbtn_Click(object sender, EventArgs e)
        {
            string username = Usertxt.Text;
            string password = passwordtxt.Text;

            // Retrieve the full name along with the role ID
            if (ValidateLogin(username, password, out int roleID, out string fullName))
            {
                LoggedInUsername = fullName; // Store the full name of the user
                LoggedInUserRole = roleID;

                // Create and show the main form based on role
                Form mainForm = CreateFormBasedOnRole(roleID);
                mainForm.FormClosed += (s, args) => Application.Exit(); // Exit the application when the main form is closed

                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Username or Password is Incorrect.");
            }
        }



        private bool ValidateLogin(string username, string password, out int roleID, out string fullName)
        {
            roleID = -1;
            fullName = string.Empty;

            // Query to validate username and password, and retrieve roleID and full name
            string query = @"
        SELECT RoleID, FullName
        FROM Users
        WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Get RoleID and FullName from the result
                            roleID = reader.GetInt32(0);  // Assuming RoleID is the first column
                            fullName = reader.GetString(1);  // Assuming FullName is the second column
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }


        private Form CreateFormBasedOnRole(int roleID)
        {
            switch (roleID)
            {
                case 1:
                    this.Hide();
                    Home Home = new Home(LoggedInUsername);
                    Home.ShowDialog();
                    this.Close();
                    return this;

                // Return form for Cashier

                case 2:

                    this.Hide();
                    Home Homes = new Home(LoggedInUsername);
                    Homes.ShowDialog();
                    this.Close();
                    return this;


                case 3:

                    this.Hide();
                    Home Homess = new Home(LoggedInUsername);
                    Homess.ShowDialog();
                    this.Close();
                    return this;



                // Return form for Control
                // return new ControlForm(LoggedInUsername);
                default:
                   
                  
                    // Default form or error handling
                    throw new InvalidOperationException("Invalid Role Call ur Software Company 'Selim'   01155003537");
            }
        }

        private void kryptonPalette1_PalettePaint(object sender, PaletteLayoutEventArgs e)
        {

        }

        private void kryptonLabel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
