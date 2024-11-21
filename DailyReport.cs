using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maintenance_Application
{
    public partial class DailyReport : Form
    {
         // Define workersTable here
        private string _username;


        private float _initialFormWidth;
        private float _initialFormHeight;
        private ControlInfo[] _controlsInfo;

        public DailyReport(string username)

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
             // Load requests on form load
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

     


        private void loadReportButton_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = datePicker.Value;
            LoadAllRequests(selectedDate);
        }

        private void LoadAllRequests(DateTime selectedDate)
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
       DateReceived as  وقت_استلام_العطل         
                                          --  Add DateClosed field
FROM vw_RequestDetails 
WHERE StatusID = 4  
        AND CONVERT(DATE, DateCompleted) = @Date";  // Filter by year and month

            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Extract the year and month from the selected date

                        // Add the year and month as parameters
                        command.Parameters.AddWithValue("@Date", selectedDate.Date);

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            if (dataTable == null || dataTable.Rows.Count == 0)
                            {
                                MessageBox.Show("لا يوجد");
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }




        
        private List<DataGridViewColumn> columnsToPrint;
        private void PrintButton_Click(object sender, EventArgs e)
        {
           
            columnsToPrint = FollowingReqGridView.Columns.Cast<DataGridViewColumn>()
                .Where(col => col.Visible && col.Name != "UserID").ToList();  // Exclude UserID column from printing

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            // Set landscape mode
            printDocument.DefaultPageSettings.Landscape = true;

            PrintDialog printDialog = new PrintDialog
            {
                Document = printDocument
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        private int currentPage = 0; // Track the current page number
        private int rowsPerPage; // Number of rows per page
        private int totalRows; // Total number of rows



        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Calculate scale factor for fitting content to page width
            int totalWidth = columnsToPrint.Sum(col => col.Width);
            int printableWidth = e.MarginBounds.Width;
            float scaleFactor = (float)printableWidth / totalWidth;

            // Calculate rows per page
            rowsPerPage = (int)((e.MarginBounds.Height - e.MarginBounds.Top) / (FollowingReqGridView.RowTemplate.Height + 5)); // Adjust spacing as needed

            // Print header with title and date on each page
            string headerText = "تقرير يومي";
            string reportDateText = $"التاريخ: {datePicker.Value.Date.ToShortDateString()}";

            // Adjust the y position to decrease space above the header
            float y = e.MarginBounds.Top - 30; // Start closer to the top of the page
            float x = e.MarginBounds.Left;

            // Define font sizes
            Font headerFont = new Font(FollowingReqGridView.Font.FontFamily, 14, FontStyle.Bold);
            Font dateFont = new Font(FollowingReqGridView.Font.FontFamily, 12, FontStyle.Regular);

            // Measure the width of the header and date texts
            SizeF headerSize = e.Graphics.MeasureString(headerText, headerFont);
            SizeF dateSize = e.Graphics.MeasureString(reportDateText, dateFont);

            // Set x positions for right-aligned text
            float headerX = e.MarginBounds.Right - headerSize.Width;
            float dateX = e.MarginBounds.Right - dateSize.Width;

            // Print the header text and date
            e.Graphics.DrawString(headerText, headerFont, Brushes.Black, new PointF(headerX, y));
            e.Graphics.DrawString(reportDateText, dateFont, Brushes.Black, new PointF(dateX, y + headerSize.Height + 5)); // Add space between header and date

            // Add less additional space between date and content
            y += (int)headerSize.Height + (int)dateSize.Height + 30; // Reduce the space as needed

            if (totalWidth > printableWidth)
            {
                scaleFactor = (float)printableWidth / totalWidth;
            }

            int remainingWidth = printableWidth;
            int columnsPrinted = 0;

            // **Print column headers**, skipping hidden columns
            Font columnHeaderFont = new Font(FollowingReqGridView.Font, FontStyle.Bold); // Use bold for column headers
            y += FollowingReqGridView.RowTemplate.Height; // Move down for column headers

            foreach (var column in columnsToPrint)
            {
                // Skip invisible columns
                if (column.Name == "StatusID" || column.Name == "UserID" || column.Name == "AreaID" || column.Name == "RoomID" || column.Name == "WorkerID" || column.Name == "ManagerID")
                    continue;

                int columnWidth = (int)(column.Width * scaleFactor);
                if (remainingWidth < columnWidth)
                {
                    break;
                }

                RectangleF rect = new RectangleF(x, y, columnWidth, FollowingReqGridView.RowTemplate.Height);
                e.Graphics.DrawString(column.HeaderText, columnHeaderFont, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                x += columnWidth;
                remainingWidth -= columnWidth;
                columnsPrinted++;
            }

            // Move down for rows after printing headers
            y += 25 + 5; // Move down for rows, adjust spacing as needed
            x = e.MarginBounds.Left;

            // Calculate total rows if not already done
            if (totalRows == 0)
            {
                totalRows = FollowingReqGridView.Rows.Count;
            }

            // Track rows printed on current page
            int rowsPrinted = 0;

            // Print rows, skipping hidden columns
            for (int i = currentPage * rowsPerPage; i < totalRows; i++)
            {
                if (FollowingReqGridView.Rows[i].IsNewRow) continue;

                x = e.MarginBounds.Left;
                foreach (var cell in FollowingReqGridView.Rows[i].Cells.Cast<DataGridViewCell>())
                {
                    // Skip invisible columns
                    if (cell.OwningColumn.Name == "StatusID" || cell.OwningColumn.Name == "UserID" || cell.OwningColumn.Name == "AreaID" || cell.OwningColumn.Name == "RoomID" || cell.OwningColumn.Name == "WorkerID" || cell.OwningColumn.Name == "ManagerID")
                        continue;

                    int cellWidth = (int)(cell.OwningColumn.Width * scaleFactor);
                    RectangleF rect = new RectangleF(x, y, cellWidth, FollowingReqGridView.RowTemplate.Height);
                    e.Graphics.DrawString(cell.Value?.ToString(), FollowingReqGridView.Font, Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                    x += cellWidth;
                }

                y += FollowingReqGridView.RowTemplate.Height + 5; // Move down for the next row
                rowsPrinted++;

                // Check if we need to create a new page
                if (rowsPrinted >= rowsPerPage) // If printed rows exceed the number of rows per page
                {
                    currentPage++; // Increment page number
                    e.HasMorePages = true;
                    return; // Exit method to trigger the next page
                }
            }

            // If we've finished printing all rows, reset for the next print job
            e.HasMorePages = false;
            currentPage = 0; // Reset page number for the next print job
            totalRows = 0; // Reset total rows
        }

        private void ExportToExcelButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Save as Excel File";
                saveFileDialog.FileName = "DailyReport.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Daily Report");

                            int colIndex = 1; // Column index in Excel starts from 1
                                              // Add column headers
                            for (int i = 0; i < FollowingReqGridView.Columns.Count; i++)
                            {
                                if (FollowingReqGridView.Columns[i].Visible)
                                {
                                    worksheet.Cell(1, colIndex).Value = FollowingReqGridView.Columns[i].HeaderText;
                                    colIndex++;
                                }
                            }

                            // Add rows
                            for (int i = 0; i < FollowingReqGridView.Rows.Count; i++)
                            {
                                colIndex = 1;
                                for (int j = 0; j < FollowingReqGridView.Columns.Count; j++)
                                {
                                    if (FollowingReqGridView.Columns[j].Visible)
                                    {
                                        worksheet.Cell(i + 2, colIndex).Value = FollowingReqGridView.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                                        colIndex++;
                                    }
                                }
                            }

                            // Auto-size columns based on content
                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(saveFileDialog.FileName);
                        }

                        MessageBox.Show("Data successfully exported to Excel.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while exporting data to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home homeform = new Home(_username);

            homeform.ShowDialog();
            this.Close();
        }

        private void DailyReport_Load(object sender, EventArgs e)
        {

        }
    }
    }
