using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Maintenance_Application
{
    public partial class Extrareq : Form
    {
        private string _username;


        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;

        public Extrareq(string username)
        {
            _username = username;
            InitializeComponent();
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

        private void Extrareq_Load(object sender, EventArgs e)
        {
            // Any initialization if needed
        }

        private void idtxt_TextChanged(object sender, EventArgs e)
        {
            // Handle text changes if needed
        }

        private void notestxt_TextChanged(object sender, EventArgs e)
        {
            // Handle text changes if needed
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            // Get the request ID and notes from the text boxes
            int requestId;
            string notes = notestxt.Text;

            // Validate the request ID input
            if (int.TryParse(idtxt.Text, out requestId))
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Check current StatusID for the request
                        string selectQuery = "SELECT StatusID FROM Requests WHERE RequestID = @RequestID";
                        int currentStatusID;

                        using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection, transaction))
                        {
                            selectCommand.Parameters.AddWithValue("@RequestID", requestId);
                            object result = selectCommand.ExecuteScalar();

                            if (result == null)
                            {
                                MessageBox.Show("Request ID not found.");
                                transaction.Rollback();
                                return;
                            }

                            currentStatusID = Convert.ToInt32(result);
                        }

                        // Proceed only if StatusID is 1 or 2
                        if (currentStatusID == 1 || currentStatusID == 2)
                        {
                            // Insert into ExtraRequests table
                            string insertQuery = "INSERT INTO ExtraRequests (RequestID, RequestedName, Notes, DateSubmitted) " +
                                                 "VALUES (@RequestID, @RequestedName, @Notes, @DateSubmitted)";

                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@RequestID", requestId);
                                insertCommand.Parameters.AddWithValue("@RequestedName", _username); // Using the username as the requested name
                                insertCommand.Parameters.AddWithValue("@Notes", notes);
                                insertCommand.Parameters.AddWithValue("@DateSubmitted", DateTime.Now);

                                insertCommand.ExecuteNonQuery();
                            }

                            // Update Requests table
                            string updateQuery = "UPDATE Requests SET StatusID = 5 WHERE RequestID = @RequestID";

                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@RequestID", requestId);
                                updateCommand.ExecuteNonQuery();
                            }

                            // Commit the transaction if both commands succeed
                            transaction.Commit();
                            notestxt.Text = "";
                            idtxt.Text = "";
                            MessageBox.Show("طلب شراء بنجاح");
                        }
                        else
                        {
                            MessageBox.Show("الطاب اكتمل او مغلق لا يمكن طلب شراء لهذا البلاغ");
                            transaction.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if an error occurs
                        transaction.Rollback();
                        MessageBox.Show("رقم البلاغ غير صحيح");
                    }
                }
            }
            else
            {
                MessageBox.Show("رقم البلاغ غير صحيح ....");
            }
        }


        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home homeform = new Home(_username);

            homeform.ShowDialog();
            this.Close();
        }
    }
}
