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
    public partial class CloseReq : Form
    {
        // Define workersTable here
        private string _username;


        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;
        public CloseReq(string username)

        {


            _username = username;
            InitializeComponent();
            FollowingReqGridView.DataError += FollowingReqGridView_DataError;
            FollowingReqGridView.EditingControlShowing += FollowingReqGridView_EditingControlShowing;
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

        private void FollowingReqGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (FollowingReqGridView.CurrentCell is DataGridViewComboBoxCell)
            {
                ComboBox comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged; // Unsubscribe to avoid multiple bindings
                    comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;  // Subscribe to handle selection
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedItem is DataRowView selectedRow)
            {
                // Get the UserID from the selected DataRowView
                if (int.TryParse(selectedRow["UserID"].ToString(), out int userId))
                {
                    // Set the cell value to the UserID (as integer)
                    FollowingReqGridView.CurrentRow.Cells["القائم_على_العطل"].Value = userId; // Ensure userId is an integer
                }
                else
                {
                    MessageBox.Show("Invalid UserID format. Please try again.", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid worker from the list.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void FollowingReqGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Get the value that caused the error
            object cellValue = FollowingReqGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // Print the value and its type to the console for debugging
            string valueInfo = cellValue != null ? $"Value: {cellValue}, Type: {cellValue.GetType()}" : "Value: null";
            Console.WriteLine($"Data Error: {e.Exception.Message} in row {e.RowIndex}, column {e.ColumnIndex}. {valueInfo}");

            e.ThrowException = false; // Prevent exception propagation
        }


        private void FollowReq_Load(object sender, EventArgs e)
        {
            FollowingReqGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            LoadAllRequests(); // Load requests on form load
        }

        // Make all columns readonly when loading requests
        private void LoadAllRequests()
        {
            string query = @"
    SELECT RequestID, 
           اسم_المبلغ, 
           المكان, 
           رقم_الغرفه, 
           نوع_العطل,         
           القائم_على_العطل,
           الحاله, 
           مستلم_العطل,
           StatusID, 
           UserID, 
           AreaID, 
           RoomID, 
           WorkerID, 
           ManagerID,
           Description AS نوتس,
           DateSubmitted,      -- Add DateSubmitted field
           DateCompleted ,
           DateReceived as  وقت_استلام_العطل                 -- Add DateClosed field
           FROM vw_RequestDetails 
    WHERE StatusID IN (2,3)"; // Only fetch records where StatusID is In Progress

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        if (dataTable == null || dataTable.Rows.Count == 0)
                        {
                            MessageBox.Show("لا يوجد الان بلاغات على قيد الانتظار");
                             // Exit if there are no rows
                        }

                        FollowingReqGridView.DataSource = dataTable;

                        // Hide specified columns
                        FollowingReqGridView.Columns["StatusID"].Visible = false;
                        FollowingReqGridView.Columns["UserID"].Visible = false;
                        FollowingReqGridView.Columns["AreaID"].Visible = false;
                        FollowingReqGridView.Columns["RoomID"].Visible = false;
                        FollowingReqGridView.Columns["WorkerID"].Visible = false;
                        FollowingReqGridView.Columns["ManagerID"].Visible = false;
                        FollowingReqGridView.Columns["DateCompleted"].Visible = false;

                        // Set all columns to read-only
                        foreach (DataGridViewColumn column in FollowingReqGridView.Columns)
                        {


                            column.ReadOnly = true; // Set all columns to read-only
                        }




                        foreach (DataGridViewRow row in FollowingReqGridView.Rows)
                        {
                            if (row != null)
                            {
                                string statusValue = Convert.ToString(row.Cells["الحاله"].Value);

                                if (statusValue == "اكتمل") // Gray for status 3
                                {
                                    row.Cells["الحاله"].Style.BackColor = Color.Gray;
                                    row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                                }
                                else if (statusValue == "قيد التشغيل") // Green for status 2
                                {
                                    row.Cells["الحاله"].Style.BackColor = Color.Green;
                                    row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                                }


                              

                                else
                                {
                                    // Optional: Log or handle the case where 'الحاله' cell is null
                                    continue;
                                }

                            }
                        }
                            // Force revalidation and refresh of the DataGridView
                            FollowingReqGridView.Invalidate();
                        FollowingReqGridView.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }




        // Update the GetFilteredWorkers method to return all workers
        private DataTable GetFilteredWorkers(string roleName)
        {
            string query = @"
        SELECT u.UserID, u.FullName
        FROM Users u
        INNER JOIN Roles r ON u.RoleID = r.RoleID
        WHERE r.RoleName = @RoleName"; // Filter workers by RoleName (e.g., "Electricity")

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the role name parameter
                    command.Parameters.AddWithValue("@RoleName", roleName);

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        DataTable workersTable = new DataTable();
                        dataAdapter.Fill(workersTable);
                        return workersTable;
                    }
                }
            }
        }


        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if (FollowingReqGridView.CurrentRow != null)
            {
                DataGridViewRow selectedRow = FollowingReqGridView.CurrentRow;
                int requestId = Convert.ToInt32(selectedRow.Cells["RequestID"].Value);

                // Only update the status from 2 to 3
                UpdateRequestStatusInDatabase(requestId);
            }
            else
            {
                MessageBox.Show("Please select a request to update.");
            }
        }
        private async Task<DateTime> GetServerDateTimeAsync(SqlConnection connection)
        {
            string query = "SELECT GETDATE()";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    return (DateTime)await command.ExecuteScalarAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error retrieving server date and time: {ex.Message}");
                    throw;
                }
            }
        }

        private async void UpdateRequestStatusInDatabase(int requestId)
        {
            // Check if the current row is selected
            if (FollowingReqGridView.CurrentRow != null)
            {
                DataGridViewRow selectedRow = FollowingReqGridView.CurrentRow;

                // Retrieve the اسم المبلغ value from the selected row
                string requestorName = selectedRow.Cells["اسم_المبلغ"].Value?.ToString();

                // Check if usertxt.Text matches the اسم المبلغ value
            
            }

            // Query to update the request status and set the DateCompleted
            string updateQuery = @"
            UPDATE Requests
            SET StatusID = @StatusID, DateCompleted = @DateCompleted
            WHERE RequestID = @RequestID";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                await connection.OpenAsync();

                // Get server date and time
                DateTime serverDateTime = await GetServerDateTimeAsync(connection);

                // Execute the update query
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@StatusID", 4);  // Change status to 4
                    updateCommand.Parameters.AddWithValue("@DateCompleted", serverDateTime); // Set current server date and time
                    updateCommand.Parameters.AddWithValue("@RequestID", requestId);

                    int rowsAffected = await updateCommand.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Request status updated successfully!");
                        LoadAllRequests();  // Refresh the DataGridView after the update
                    }
                    else
                    {
                        MessageBox.Show("Update failed. No rows were affected.");
                    }
                }
            }
        }




        private int GetManagerId(string managerFullName, SqlConnection connection)
        {
            string query = "SELECT UserID FROM Users WHERE FullName = @ManagerFullName";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ManagerFullName", managerFullName);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void loadbtn_Click(object sender, EventArgs e)
        {
            LoadAllRequests();
        }

        private void CloseReq_Load(object sender, EventArgs e)
        {
           usertxt.Text= _username;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home homeform = new Home(_username);

            homeform.ShowDialog();
            this.Close();
        }

        private void notcompletedreqtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void returnstatusbtn_Click(object sender, EventArgs e)
        {
            // Ensure the text box is not empty and contains a valid RequestID
            if (string.IsNullOrWhiteSpace(notcompletedreqtxt.Text) || !int.TryParse(notcompletedreqtxt.Text, out int requestId))
            {
                MessageBox.Show("Please enter a valid Request ID.");
                return;
            }

            // Define the query to check the current StatusID of the RequestID
            string checkStatusQuery = "SELECT StatusID FROM vw_RequestDetails WHERE RequestID = @RequestID";
            string updateStatusQuery = "UPDATE vw_RequestDetails SET StatusID = 2 WHERE RequestID = @RequestID";

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    connection.Open();

                    // Check the current status of the request
                    using (SqlCommand checkCommand = new SqlCommand(checkStatusQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@RequestID", requestId);

                        object result = checkCommand.ExecuteScalar();

                        // If the RequestID is found and status is 4, show an error and return
                        if (result != null && Convert.ToInt32(result) == 4 || result != null && Convert.ToInt32(result) == 1)
                        {
                            MessageBox.Show("لا يمكن تغير حاله هذا العطل");
                            return;
                        }
                    }

                    // Proceed with updating the status if it is not 4
                    using (SqlCommand updateCommand = new SqlCommand(updateStatusQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@RequestID", requestId);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Status updated successfully!");
                            notcompletedreqtxt.Clear(); // Clear the text box after successful update
                        }
                        else
                        {
                            MessageBox.Show("No matching Request ID found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


    }
}
