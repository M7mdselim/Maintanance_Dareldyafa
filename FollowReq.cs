﻿using OpenQA.Selenium;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumKeys = OpenQA.Selenium.Keys;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using DocumentFormat.OpenXml.Bibliography;
using System.Threading;
using System.IO; // Import this for Directory and File classes
using OpenQA.Selenium.Remote;
using static OpenQA.Selenium.BiDi.Modules.Script.RealmInfo;



namespace Maintenance_Application
{



    public partial class FollowReq : Form
    {
        private IWebDriver driver = null;
        private string _username;



        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;

        public FollowReq(string username)
        {
            _username = username;
            InitializeComponent();
            FollowingReqGridView.DataError += FollowingReqGridView_DataError;


            FollowingReqGridView.CellContentDoubleClick += FollowingReqGridView_CellContentDoubleClick;





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




            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            //IWebElement searchBox = wait.Until(ExpectedConditions.ElementIsVisible(
            //    By.XPath("//div[@contenteditable='true'][@data-tab='3']")));


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

        private async void FollowingReqGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click was on a valid row (not the header)
            if (e.RowIndex >= 0)
            {
                // Get the clicked row
                var row = FollowingReqGridView.Rows[e.RowIndex];

                // Assuming you have an 'ID' column to fetch the Notes and a 'Status' column
                int id = Convert.ToInt32(row.Cells["RequestID"].Value); // Adjust the column name as necessary
                string status = row.Cells["الحاله"].Value.ToString(); // Adjust the column name as necessary

                // Check if the status is either 5 or "طلب شراء"
                if (status == "5" || status == "طلب شراء" || status == "6" || status == "ملاحظات")
                {
                    // Fetch Notes and DateSubmitted from the ExtraReq table
                    var (notes, dateSubmitted) = await GetNotesAndDateFromExtrareqAsync(id);
                    if (status == "5" || status == "طلب شراء")
                    {
                        // Show the Notes and Date in a MessageBox
                        MessageBox.Show($"مطلوب للشراء : {notes}\nتاريخ الطلب: {dateSubmitted}", "مطلوب للشراء", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else {


                        MessageBox.Show($"ملاحظه : {notes}\nتاريخ الطلب: {dateSubmitted}", "ملاحظه", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }

        }

        private async Task<(string Notes, string DateSubmitted)> GetNotesAndDateFromExtrareqAsync(int id)
        {
            string notes = string.Empty;
            string dateSubmitted = string.Empty;

            using (var connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                await connection.OpenAsync();
                string query = @"
            SELECT Notes, DateSubmitted 
            FROM ExtraRequests 
            WHERE RequestID = @ID
            ORDER BY DateSubmitted DESC"; // Orders by DateSubmitted in descending order

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            notes = reader["Notes"]?.ToString() ?? "No notes available.";
                            dateSubmitted = reader["DateSubmitted"]?.ToString() ?? "No date available.";
                        }
                    }
                }
            }

            return (notes, dateSubmitted);
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
            // Load requests on form load
        }

        private void LoadAllRequests()
        {
            // Force revalidation and refresh of the DataGridView



            string query = @"
SELECT RequestID, 
       اسم_المبلغ, 
       المكان, 
       رقم_الغرفه, 
       نوع_العطل,         -- Maintenance Type (e.g., 'Electricity')
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
WHERE StatusID IN (1,2,5,6)
"; // Only fetch records where StatusID is Opened

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
                            // Exit if there are no rows
                            // Exit the method if there are no rows
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

                        // Set all columns to read-only except for القائم_على_العطل
                        foreach (DataGridViewColumn column in FollowingReqGridView.Columns)
                        {
                            column.ReadOnly = true; // Set all columns to read-only
                        }
                        FollowingReqGridView.Columns["القائم_على_العطل"].ReadOnly = false; // Make القائم_على_العطل editable

                        // Iterate over rows to set ComboBox values for cells where WorkerID is NULL
                        foreach (DataGridViewRow row in FollowingReqGridView.Rows)
                        {
                            if (row != null)
                            {
                                string statusValue = Convert.ToString(row.Cells["الحاله"].Value);

                                if (statusValue == "مفتوح") // Yellow for status 1
                                {
                                    row.Cells["الحاله"].Style.BackColor = Color.Yellow;
                                    row.Cells["الحاله"].Style.ForeColor = Color.Black; // Ensure text is readable
                                }
                                else if (statusValue == "قيد التشغيل") // Green for status 2
                                {
                                    row.Cells["الحاله"].Style.BackColor = Color.Green;
                                    row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                                }
                                else if (statusValue == "طلب شراء") // Red for status 5
                                {
                                    row.Cells["الحاله"].Style.BackColor = Color.Red;
                                    row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                                }

                                else if (statusValue == "ملاحظات") // Red for status 5
                                {
                                    row.Cells["الحاله"].Style.BackColor = Color.Purple;
                                    row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                                }
                                else
                                {
                                    // Optional: Log or handle the case where 'الحاله' cell is null
                                    continue;
                                }

                            }
                            if (row.Cells["القائم_على_العطل"].Value == DBNull.Value)
                            {
                                // Get the maintenance type (e.g., 'Electricity') for this row
                                string maintenanceType = row.Cells["نوع_العطل"].Value.ToString();

                                // Fetch filtered workers for the specific maintenance type (role name)
                                DataTable filteredWorkers = GetFilteredWorkers(maintenanceType);

                                if (filteredWorkers != null && filteredWorkers.Rows.Count > 0)
                                {
                                    DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell
                                    {
                                        DataSource = filteredWorkers,
                                        DisplayMember = "FullName",  // Display the worker's full name
                                        ValueMember = "UserID"       // Store the worker's UserID
                                    };

                                    row.Cells["القائم_على_العطل"] = comboBoxCell;

                                    comboBoxCell.Style.BackColor = Color.White; // Set background to white
                                    comboBoxCell.Style.ForeColor = Color.Black;

                                    // Set the styles directly after assignment
                                    // Set text color to black
                                }

                            }
                        }


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
        INNER JOIN Roles r ON u.RoleID = 4
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


        //private void SendMessageToWhatsAppGroup(string groupNumber, string message)
        //{
        //    const string accountSid = "US3d0c07f820c392b285b07c7fe5dbef9c"; // Replace with your Twilio Account SID
        //    const string authToken = "50477cfaa16cad6ff789f6ce02469576";   // Replace with your Twilio Auth Token

        //    TwilioClient.Init(accountSid, authToken);

        //    var messageOptions = new CreateMessageOptions(new Twilio.Types.PhoneNumber($"whatsapp:{groupNumber}"))
        //    {
        //        From = new Twilio.Types.PhoneNumber("whatsapp:+01155003537"), // Replace with your Twilio WhatsApp number
        //        Body = message
        //    };

        //    var msg = MessageResource.Create(messageOptions);
        //    MessageBox.Show($"WhatsApp message sent: {msg.Sid}");
        //}




        // Assuming driver is a class-level variable
        // Initialize driver as a class-level variable

        // Assuming driver is a class-level variable


        public void SendMessageToWhatsAppGroup(string groupName, string message)
        {
            string chromeProfilePath = @"C:\Temp\SeleniumChromeProfile";

            try
            {
                // If the driver is null (not open), create a new driver instance
                if (driver == null)
                {
                    // Ensure the profile directory exists
                    if (!Directory.Exists(chromeProfilePath))
                    {
                        Directory.CreateDirectory(chromeProfilePath);
                    }

                    // Delete the DevToolsActivePort file if it exists
                    string devToolsPortFile = Path.Combine(chromeProfilePath, "DevToolsActivePort");
                    if (File.Exists(devToolsPortFile))
                    {
                        File.Delete(devToolsPortFile);
                    }

                    // Configure ChromeOptions
                    var options = new ChromeOptions();
                    options.AddArgument($"--user-data-dir={chromeProfilePath}"); // Use a unique profile
                    options.AddArgument("--profile-directory=Default");
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-infobars");
                    options.AddArgument("--disable-popup-blocking");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--remote-debugging-port=9222");

                    // Initialize the ChromeDriver
                    driver = new ChromeDriver(options);

                    // Navigate to WhatsApp Web and wait for QR code scan only if not already there
                    driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                    Thread.Sleep(10000); // Wait for WhatsApp Web to load and QR code to scan
                }
                else if (driver.Url != "https://web.whatsapp.com/")
                {
                    // If driver is already open but not on WhatsApp Web, navigate to the page
                    driver.Navigate().GoToUrl("https://web.whatsapp.com/");
                }

                // Search for the group by name
                var searchBox = driver.FindElement(By.XPath("//div[@contenteditable='true'][@data-tab='3']"));
                searchBox.SendKeys(groupName);
                searchBox.SendKeys(OpenQA.Selenium.Keys.Enter);

                // Find the message input box and send the message
                var messageBox = driver.FindElement(By.XPath("//div[@contenteditable='true'][@data-tab='10']"));
                messageBox.SendKeys(message);
                messageBox.SendKeys(OpenQA.Selenium.Keys.Enter);
                Thread.Sleep(3000);

                // Confirm success
                Console.WriteLine("Message sent successfully to the group.");
            }
            catch (NoSuchElementException ex)
            {
                MessageBox.Show($"Element not found: {ex.Message}");
            }
            catch (WebDriverException ex)
            {
                MessageBox.Show($"WebDriver error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}");
            }
        }


        public void QuitDriverOnExit()
        {
            // Quit driver when the program is exiting or when the user requests it
            driver?.Quit();
        }






        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if (FollowingReqGridView.CurrentRow != null)
            {
                DataGridViewRow selectedRow = FollowingReqGridView.CurrentRow;
                int requestId = Convert.ToInt32(selectedRow.Cells["RequestID"].Value);

                object workerValue = selectedRow.Cells["القائم_على_العطل"].Value;
                if (workerValue == null || workerValue == DBNull.Value)
                {
                    MessageBox.Show("Please select a worker from the dropdown.");
                    return;
                }

                if (int.TryParse(workerValue.ToString(), out int workerUserID))
                {
                    UpdateRequestInDatabase(workerUserID, requestId);
                }
                else
                {
                    // Re-check the selected row values
                    selectedRow = FollowingReqGridView.CurrentRow;
                    requestId = Convert.ToInt32(selectedRow.Cells["RequestID"].Value);
                    workerValue = selectedRow.Cells["القائم_على_العطل"].Value;

                    if (workerValue == null || workerValue == DBNull.Value)
                    {
                        MessageBox.Show("Please select a worker from the dropdown.");
                        return;
                    }

                    string workerFullName = workerValue.ToString();
                    string getUserIdQuery = @"SELECT UserID FROM Users WHERE FullName = @FullName";
                    string updateQuery = @"UPDATE Requests 
                SET WorkerID = @WorkerID, 
                    StatusID = @StatusID, 
                    ManagerID = @ManagerID, 
                    DateReceived = GETDATE() 
                WHERE RequestID = @RequestID";

                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                    {
                        connection.Open();

                        int workerId = 0;
                        using (SqlCommand getUserIdCommand = new SqlCommand(getUserIdQuery, connection))
                        {
                            getUserIdCommand.Parameters.AddWithValue("@FullName", workerFullName);
                            object result = getUserIdCommand.ExecuteScalar();

                            if (result != null)
                            {
                                workerId = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show("Worker not found in Users table.");
                                return;
                            }
                        }

                        string managerFullName = usertxt.Text;
                        int managerId = GetManagerId(managerFullName, connection);
                        if (managerId == 0)
                        {
                            MessageBox.Show("Manager not found in Users table.");
                            return;
                        }

                        // Get the current status of the request
                        int currentStatus = Convert.ToInt32(selectedRow.Cells["StatusID"].Value);
                        int newStatus = 0;

                        // Set new status based on current status
                        if (currentStatus == 1)
                        {
                            newStatus = 2; // Update to status 2










                             workerId = Convert.ToInt32(selectedRow.Cells["القائم_على_العطل"].Value);

                            // Fetch the FullName from the database
                            string workerfullname = GetWorkerFullNameById(workerId);

                            // Construct the message
                            string areaName = Convert.ToString(selectedRow.Cells["المكان"].Value); // Area Name
                            string roomName = Convert.ToString(selectedRow.Cells["رقم_الغرفه"].Value);
                            string notes = Convert.ToString(selectedRow.Cells["نوتس"].Value); // Notes
                            string maintenanceType = Convert.ToString(selectedRow.Cells["نوع_العطل"].Value); // Maintenance Type
                            string status = Convert.ToString(selectedRow.Cells["الحاله"].Value); // Status

                            string groupName = "أعطال قسم الصيانه والتطوير"; // Replace with the WhatsApp group name

                            // Construct the message
                            string message = $"    تم إبلاغ بعطل إلى المكان   {areaName}, " +
                                             $"   وقد تم تعيين {workerfullname}, " +
                                             $"   بخصوص {maintenanceType}, " +
                                             $"   إلى غرفة رقم {roomName}." +
                                             $"   الملاحظات: {notes}";

                            // Send the message to the WhatsApp group
                            SendMessageToWhatsAppGroup(groupName, message);
                            // Refresh the DataGridView after the update


                        }
                        else if (currentStatus == 2)
                        {
                            newStatus = 3; // Update to status 3

                            // If status changes to 3, update DateEnded
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@WorkerID", workerId);
                                updateCommand.Parameters.AddWithValue("@StatusID", newStatus);
                                updateCommand.Parameters.AddWithValue("@ManagerID", managerId);
                                updateCommand.Parameters.AddWithValue("@RequestID", requestId);

                                // Additional query to set DateEnded if status is updated to 3
                                string updateDateEndedQuery = @"UPDATE Requests 
                                        SET DateEnded = GETDATE() 
                                        WHERE RequestID = @RequestID AND StatusID = 3";

                                using (SqlCommand dateEndedCommand = new SqlCommand(updateDateEndedQuery, connection))
                                {
                                    dateEndedCommand.Parameters.AddWithValue("@RequestID", requestId);
                                    int dateEndedRowsAffected = dateEndedCommand.ExecuteNonQuery();

                                    if (dateEndedRowsAffected > 0)
                                    {
                                       
                                    }
                                }

                               
                            }
                        }
                        else if (currentStatus == 5)
                        {
                            newStatus = 2; // Update to status 2

                        }


                        else if (currentStatus == 6)
                        {
                            newStatus = 2; // Update to status 2

                        }

                        else
                        {
                            MessageBox.Show("Invalid status for updating.");
                            return;
                        }

                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@WorkerID", workerId);
                            updateCommand.Parameters.AddWithValue("@StatusID", newStatus);
                            updateCommand.Parameters.AddWithValue("@ManagerID", managerId);
                            updateCommand.Parameters.AddWithValue("@RequestID", requestId);

                            int rowsAffected = updateCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                // If updated to status 2, show confirmation message
                                if (newStatus == 2)
                                {
                                    MessageBox.Show("الطلب قيد التشغيل الان !");





                                    workerFullName = Convert.ToString(selectedRow.Cells["القائم_على_العطل"].Value);

                                    // Fetch the FullName from the database
                                    

                                    // Construct the message
                                    string areaName = Convert.ToString(selectedRow.Cells["المكان"].Value); // Area Name
                                    string roomName = Convert.ToString(selectedRow.Cells["رقم_الغرفه"].Value);
                                    string notes = Convert.ToString(selectedRow.Cells["نوتس"].Value); // Notes
                                    string maintenanceType = Convert.ToString(selectedRow.Cells["نوع_العطل"].Value); // Maintenance Type
                                    string status = Convert.ToString(selectedRow.Cells["الحاله"].Value); // Status

                                    string groupName = "أعطال قسم الصيانه والتطوير"; // Replace with the WhatsApp group name

                                    // Construct the message
                                    string message = $"    تم إبلاغ بعطل إلى المكان   {areaName}, " +
                                                     $"   وقد تم تعيين {workerFullName}, " +
                                                     $"   بخصوص {maintenanceType}, " +
                                                     $"   إلى غرفة رقم {roomName}." +
                                                     $"   الملاحظات: {notes}";

                                    // Send the message to the WhatsApp group
                                    SendMessageToWhatsAppGroup(groupName, message);

                                }
                                else
                                {


                                  


                                    // If status changes to 3, update DateEnded
                                   
                                        updateCommand.Parameters.AddWithValue("@WorkerID", workerId);
                                        updateCommand.Parameters.AddWithValue("@StatusID", newStatus);
                                        updateCommand.Parameters.AddWithValue("@ManagerID", managerId);
                                        updateCommand.Parameters.AddWithValue("@RequestID", requestId);

                                        // Additional query to set DateEnded if status is updated to 3
                                        string updateDateEndedQuery = @"UPDATE Requests 
                                        SET DateEnded = GETDATE() 
                                        WHERE RequestID = @RequestID AND StatusID = 3";

                                        using (SqlCommand dateEndedCommand = new SqlCommand(updateDateEndedQuery, connection))
                                        {
                                            dateEndedCommand.Parameters.AddWithValue("@RequestID", requestId);
                                            int dateEndedRowsAffected = dateEndedCommand.ExecuteNonQuery();

                                           
                                        }

                                       
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("الطلب اكتمل");
                                            LoadAllRequests(); // Refresh the DataGridView after the update
                                        }
                                        else
                                        {
                                            MessageBox.Show("Update failed. No rows were affected.");
                                        }
                                    


                                }
                                LoadAllRequests(); // Refresh the DataGridView after the update
                            }
                            else
                            {
                                MessageBox.Show("Update failed. No rows were affected.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a request to update.");
            }
        }

        private string GetWorkerFullNameById(int workerId)
        {
            string query = "SELECT FullName FROM Users WHERE UserID = @WorkerId";
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the WorkerId parameter
                    command.Parameters.AddWithValue("@WorkerId", workerId);

                    connection.Open();
                    object result = command.ExecuteScalar(); // Execute the query to get a single value
                    connection.Close();

                    // Return the FullName if found, or a default message if not
                    return result != null ? result.ToString() : "Unknown Worker";
                }
            }
        }

        private void UpdateRequestInDatabase(int workerUserID, int requestId)
        {
            string getServerDateTimeQuery = "SELECT GETDATE()";
            // Query to update the request with a subquery for WorkerID
            string updateQuery = @"
                UPDATE Requests
                SET WorkerID = @WorkerID,
                    StatusID = @StatusID,
                    ManagerID = @ManagerID,
                    DateReceived = GETDATE()
                WHERE RequestID = @RequestID";

            using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
            {
                connection.Open();

                // Retrieve ManagerID based on the manager's name (from a text box or field)
                string managerFullName = usertxt.Text;
                int managerId = GetManagerId(managerFullName, connection);
                if (managerId == 0)
                {
                    MessageBox.Show($"Manager '{managerFullName}' not found.");
                    return;
                }

                // Execute the update query
                using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@WorkerID", workerUserID);
                    updateCommand.Parameters.AddWithValue("@StatusID", 2);  // Example status
                    updateCommand.Parameters.AddWithValue("@ManagerID", managerId);
                    updateCommand.Parameters.AddWithValue("@RequestID", requestId);
                    updateCommand.Parameters.AddWithValue("@DateReceived", getServerDateTimeQuery);

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("الطلب  قيد التشغيل");


                        DataGridViewRow selectedRow = FollowingReqGridView.CurrentRow;
                        int workerId = Convert.ToInt32(selectedRow.Cells["القائم_على_العطل"].Value);

                        // Fetch the FullName from the database
                        string workerfullname = GetWorkerFullNameById(workerId);

                        // Construct the message
                        string areaName = Convert.ToString(selectedRow.Cells["المكان"].Value); // Area Name
                        string roomName = Convert.ToString(selectedRow.Cells["رقم_الغرفه"].Value);
                        string notes = Convert.ToString(selectedRow.Cells["نوتس"].Value); // Notes
                        string maintenanceType = Convert.ToString(selectedRow.Cells["نوع_العطل"].Value); // Maintenance Type
                        string status = Convert.ToString(selectedRow.Cells["الحاله"].Value); // Status

                        string groupName = "أعطال قسم الصيانه والتطوير"; // Replace with the WhatsApp group name

                        // Construct the message
                        string message = $"    تم إبلاغ بعطل إلى المكان   {areaName}, " +
                                         $"   وقد تم تعيين {workerfullname}, " +
                                         $"   بخصوص {maintenanceType}, " +
                                         $"   إلى غرفة رقم {roomName}." +
                                         $"   الملاحظات: {notes}";

                        // Send the message to the WhatsApp group
                        SendMessageToWhatsAppGroup(groupName, message);
                        LoadAllRequests();
                        // Refresh the DataGridView after the update
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
            //this.Hide();
            //FollowReq refresh = new FollowReq(_username);
            //refresh.ShowDialog();
            //this.Close();
            
        }


        private System.Windows.Forms.Timer refreshTimer;

        private void FollowReq_Load_1(object sender, EventArgs e)
        {
           
            LoadAllRequests(); // Initial load
            usertxt.Text = _username;

            // Initialize the timer
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 30000; // 10 seconds in milliseconds
            refreshTimer.Tick += RefreshTimer_Tick; // Attach event handler
            refreshTimer.Start(); // Start the timer

            // Hook up event handlers to detect activity
            this.MouseMove += new MouseEventHandler(OnUserActivity);
            this.KeyDown += new KeyEventHandler(OnUserActivity);
            FollowingReqGridView.MouseMove += new MouseEventHandler(OnUserActivity);
        }

        private DateTime lastActivityTime;
        private int idleTimeLimit = 30000; // 30 seconds in milliseconds

        // Event handler that gets called every 10 second 
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            // Check if the user has been idle for more than 10 seconds
            if ((DateTime.Now - lastActivityTime).TotalMilliseconds >= idleTimeLimit)
            {
                LoadAllRequests(); // Refresh the request list only if idle
            }
        }

        // Method to track user activity and reset lastActivityTime
        private void OnUserActivity(object sender, EventArgs e)
        {
            lastActivityTime = DateTime.Now; // Update the time of the last activity
        }


        // Optional: Stop the timer when the form is closing
        private void FollowReq_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (refreshTimer != null)
            {
                refreshTimer.Stop(); // Stop the timer when the form is closing
            }
        }


        private void backButton_Click(object sender, EventArgs e)
        {
           
                refreshTimer.Stop(); // Stop the timer when the form is closing
            
            this.Hide();
                Home homeform = new Home(_username);

                homeform.ShowDialog();
                this.Close();
            }

        private void FollowingReqGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FollowingReqGridView_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            foreach (DataGridViewRow row in FollowingReqGridView.Rows)
            {
                if (row != null)
                {
                    string statusValue = Convert.ToString(row.Cells["الحاله"].Value);

                    if (statusValue == "مفتوح") // Yellow for status 1
                    {
                        row.Cells["الحاله"].Style.BackColor = Color.Yellow;
                        row.Cells["الحاله"].Style.ForeColor = Color.Black; // Ensure text is readable
                    }
                    else if (statusValue == "قيد التشغيل") // Green for status 2
                    {
                        row.Cells["الحاله"].Style.BackColor = Color.Green;
                        row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                    }
                    else if (statusValue == "طلب شراء") // Red for status 5
                    {
                        row.Cells["الحاله"].Style.BackColor = Color.Red;
                        row.Cells["الحاله"].Style.ForeColor = Color.White; // Ensure text is readable
                    }
                    else
                    {
                        // Optional: Log or handle the case where 'الحاله' cell is null
                        continue;
                    }

                }
            }
        }

       
    }
    }

