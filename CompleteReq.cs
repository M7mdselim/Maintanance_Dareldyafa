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
    public partial class CompleteReq : Form
    {
        // Define workersTable here
        private string _username;
        public CompleteReq(string username)
        {
            InitializeComponent();
            FollowingReqGridView.DataError += FollowingReqGridView_DataError;
            FollowingReqGridView.EditingControlShowing += FollowingReqGridView_EditingControlShowing;
            _username = username;
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
           DateCompleted         -- Add DateClosed field
           FROM vw_RequestDetails 
    WHERE StatusID = 2"; // Only fetch records where StatusID is In Progress

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

        private void UpdateRequestStatusInDatabase(int requestId)
        {
            // Query to update the request status
            string updateQuery = @"
        UPDATE Requests
        SET StatusID = @StatusID
        WHERE RequestID = @RequestID";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();

                // Execute the update query
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@StatusID", 3);  // Change status to 3
                    updateCommand.Parameters.AddWithValue("@RequestID", requestId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
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

        private void CompleteReq_Load(object sender, EventArgs e)
        {
            usertxt.Text = _username;
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
