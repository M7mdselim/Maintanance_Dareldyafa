using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Maintenance_Application
{
    public partial class Form1 : Form
    {
        private string _username;
        public Form1(string username)
        {

            _username = username;

            InitializeComponent();
        }


        private bool isFormLoaded = false;

        private void Fullusernametxt_TextChanged(object sender, EventArgs e)
        {
            // You can add functionality here if needed
        }

        private void Maintenancetypecombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // You can add functionality here if needed
        }

        // Update this event to populate Roomcombo based on the selected AreaID
        private void Areacombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the form has fully loaded
            if (!isFormLoaded) return;

            if (Areacombo.SelectedValue is int areaID)
            {
                // Populate Roomcombo with rooms for the selected area
                PopulateRoomComboBox(areaID);
            }
            else
            {
                MessageBox.Show("Please select a valid area.");
            }
        }


        private void Roomcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // You can add functionality here if needed
        }

        private void Notestxt_TextChanged(object sender, EventArgs e)
        {
            // You can add functionality here if needed
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Fullusernametxt.Text=_username;

            // Populate ComboBoxes
            PopulateMaintenanceTypeComboBox();
            PopulateAreaComboBox();

            // Set flag to indicate form is fully loaded
            isFormLoaded = true;
        }

        private void Submitbtn_Click(object sender, EventArgs e)
        {
            // Gather the input values
            string fullName = Fullusernametxt.Text.Trim();
            string maintenanceType = Maintenancetypecombo.Text.Trim(); // Handle case where nothing is selected
            int areaID = Areacombo.SelectedValue != null ? Convert.ToInt32(Areacombo.SelectedValue) : 0;
            int roomID = Roomcombo.SelectedValue != null ? Convert.ToInt32(Roomcombo.SelectedValue) : 0;
            string notes = Notestxt.Text.Trim();

            // Ensure the required fields are populated
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(maintenanceType) || areaID == 0 || roomID == 0)
            {
                MessageBox.Show("Please ensure all fields are filled in correctly.");
                return;
            }

            // Retrieve UserID using the FullName from Fullusernametxt
            int userID = GetUserIDByFullName(fullName);

            if (userID > 0) // Ensure valid UserID is found
            {
                // Try to insert the request into the Requests table
                try
                {
                    InsertRequest(userID, maintenanceType, areaID, roomID, notes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while submitting the request: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("User not found!");
            }
        }

        // Method to retrieve UserID based on the FullName
        private int GetUserIDByFullName(string fullName)
        {
            int userID = 0;
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT UserID FROM Users WHERE FullName = @FullName", connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        userID = Convert.ToInt32(result); // Convert result to integer (UserID)
                    }
                }
            }
            return userID;
        }

        // Flag to prevent multiple submissions
        private bool isSubmitting = false;

        // Method to insert a new request into the Requests table
        private async void InsertRequest(int userID, string maintenanceType, int areaID, int roomID, string notes)
        {
            if (isSubmitting)
            {
                MessageBox.Show("Please wait until the current request is processed.");
                return; // Prevent further submissions until the current one is processed
            }

            isSubmitting = true; // Set flag to indicate submission is in progress

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    await connection.OpenAsync(); // Use asynchronous open
                    using (SqlCommand command = new SqlCommand(
                        "INSERT INTO Requests (UserID, MaintenanceTypeID, AreaID, RoomID, Description, StatusID, DateSubmitted) " +
                        "VALUES (@UserID, @MaintenanceType, @AreaID, @RoomID, @Description, @StatusID, @DateSubmitted)", connection))
                    {
                        // Add parameters for the SQL query
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@MaintenanceType", maintenanceType);
                        command.Parameters.AddWithValue("@AreaID", areaID);
                        command.Parameters.AddWithValue("@RoomID", roomID);
                        command.Parameters.AddWithValue("@Description", notes);
                        command.Parameters.AddWithValue("@StatusID", 1); // Default status
                        command.Parameters.AddWithValue("@DateSubmitted", DateTime.Now); // Set the current date for DateSubmitted

                        // Execute the command and check if rows were affected
                        int rowsAffected = await command.ExecuteNonQueryAsync(); // Use asynchronous execute
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Request submitted successfully!");
                            RefreshForm(); // Refresh the form upon successful submission
                        }
                        else
                        {
                            MessageBox.Show("Failed to submit the request.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                isSubmitting = false; // Reset the flag
            }
        }

        // Method to refresh the form
        private void RefreshForm()
        {

            
            // Clear the combo boxes and text box
            Maintenancetypecombo.Text = ""; // Deselects the current selection
            Areacombo.Text = "";            // Deselects the current selection
            Roomcombo.Text = "";            // Deselects the current selection
            Notestxt.Clear();                        // Clears the text box

            
            // Implement your form refresh logic here
            // For example, reload data, clear fields, etc.
        }


        // Method to populate the MaintenanceTypeComboBox


        // Method to populate the AreaComboBox
        private void PopulateAreaComboBox()
        {
            var areas = GetAreas();

          

            Areacombo.DataSource = areas;
            Areacombo.DisplayMember = "AreaName";
            Areacombo.ValueMember = "AreaID";

            // Set to -1 to start with no selection
            Areacombo.SelectedIndex = -1;
        }

        private void PopulateRoomComboBox(int areaID)
        {
            var rooms = GetRoomsByArea(areaID);

            // Insert a placeholder option
          

            Roomcombo.DataSource = rooms;
            Roomcombo.DisplayMember = "RoomName";
            Roomcombo.ValueMember = "RoomID";

            // Set to -1 to start with no selection
            Roomcombo.SelectedIndex = -1;
        }

        private void PopulateMaintenanceTypeComboBox()
        {
            var roles = GetRolesStartingFromID(5);

            // Insert a placeholder option

            Maintenancetypecombo.DataSource = roles;
            Maintenancetypecombo.DisplayMember = "RoleName";
            Maintenancetypecombo.ValueMember = "RoleID";

            // Set to -1 to start with no selection
            Maintenancetypecombo.SelectedIndex = -1;
        }


        // Method to retrieve rooms by AreaID from the database
        private List<Room> GetRoomsByArea(int areaID)
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT RoomID, RoomNumber FROM Rooms WHERE AreaID = @AreaID", connection))
                {
                    command.Parameters.AddWithValue("@AreaID", areaID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rooms.Add(new Room
                            {
                                RoomID = reader.GetInt32(0),
                                RoomName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return rooms;
        }

        private List<Role> GetRolesStartingFromID(int startID)
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT RoleID, RoleName FROM Roles WHERE RoleID >= @StartID", connection))
                {
                    command.Parameters.AddWithValue("@StartID", startID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Role
                            {
                                RoleID = reader.GetInt32(0),
                                RoleName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return roles;
        }

        private List<Area> GetAreas()
        {
            List<Area> areas = new List<Area>();

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT AreaID, AreaName FROM Areas", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            areas.Add(new Area
                            {
                                AreaID = reader.GetInt32(0),
                                AreaName = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return areas;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home homeform = new Home(_username);

            homeform.ShowDialog();
            this.Close();
        }
    }

    // Role class for data structure
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }

    // Area class for data structure
    public class Area
    {
        public int AreaID { get; set; }
        public string AreaName { get; set; }
    }

    public class Room
    {
        public int RoomID { get; set; }
        public string RoomName { get; set; }
    }
}
