using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
// Removed: using static System.Windows.Forms.VisualStyles.VisualStyleElement; // Not typically needed

namespace Library_Managem // Ensure this namespace matches your project's namespace
{
    public partial class Return_Issue : UserControl
    {
        // Database connection string.
        // !!! IMPORTANT: Verify this path is correct for YOUR system !!!
        // Using |DataDirectory| makes it more portable if Library.mdf is in your project's output folder.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fj\Documents\Library.mdf;Integrated Security=True;Connect Timeout=30";

        // Variables to hold current issue details after a successful search
        private int currentIssueID = -1;
        private int currentBookID = -1;
        private int currentBorrowerID = -1;
        private DateTime currentIssueDate;
        private DateTime currentDueDate; // Assumed to exist in IssuedBooks table for fine calculation
        private decimal calculatedFine = 0; // For fine calculation

        public Return_Issue()
        {
            InitializeComponent();

            // Attach event handlers to your buttons and input fields.
            // These names MUST match the 'Name' property of your controls in the Designer (from Step 2)!
            this.button1.Click += new System.EventHandler(this.btnReturn_Click);
            this.button2.Click += new System.EventHandler(this.btnClear_Click);
            this.returnbooks_IssueID.Leave += new System.EventHandler(this.txtIssueID_Leave); // Trigger search when focus leaves Issue ID field
            returnbooks_IssueID.KeyDown += new KeyEventHandler(this.txtIssueID_KeyDown); // Trigger search on Enter key press in Issue ID field
        }

        private void Return_Issue_Load(object sender, EventArgs e)
        {
            // Set initial state when the UserControl loads
            ResetForm();
            LoadAllIssuedBooks(); // Load all issued books into the DataGridView on initial load
        }

        /// <summary>
        /// Resets the UI elements to their initial state and clears internal state.
        /// </summary>
        private void ResetForm()
        {
            // Clear textboxes and combobox using their new, meaningful names
            returnbooks_IssueID.Clear();
            returnbooks_Name.Clear();
            returnbooks_Contact.Clear();
            textBox4.Clear();
            textBox1.Clear();
            textBox1.Clear();
            textBox7.Clear();
            comboBox1.SelectedIndex = -1; // Clear combobox selection
            comboBox1.Text = ""; // Also clear combobox text

            // Set fields to read-only as they are for display after a search
            returnbooks_Name.ReadOnly = true;
            returnbooks_Contact.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox1.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            comboBox1.Enabled = false; // Disable combobox for display only

            // Reset internal state variables
            currentIssueID = -1;
            currentBookID = -1;
            currentBorrowerID = -1;
            calculatedFine = 0;

            // Ensure the Return button is disabled until a book is found
            button1.Enabled = false;

            returnbooks_IssueID.Focus(); // Put focus back on the input for a new search
        }

        /// <summary>
        /// Attempts to search for an issued book when the Issue ID text box loses focus.
        /// </summary>
        private void txtIssueID_Leave(object sender, EventArgs e)
        {
            // Only search if the ID is actually entered and valid
            if (!string.IsNullOrWhiteSpace(returnbooks_IssueID.Text) && int.TryParse(returnbooks_IssueID.Text, out int id))
            {
                SearchIssuedBook(id);
            }
            else if (string.IsNullOrWhiteSpace(returnbooks_IssueID.Text))
            {
                ResetForm(); // Clear the form if the ID box becomes empty
            }
        }

        /// <summary>
        /// Allows searching for an issued book by pressing Enter key in the Issue ID text box.
        /// </summary>
        private void txtIssueID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevent the "ding" sound
                if (!string.IsNullOrWhiteSpace(returnbooks_IssueID.Text) && int.TryParse(returnbooks_IssueID.Text, out int id))
                {
                    SearchIssuedBook(id);
                }
                else
                {
                    MessageBox.Show("Please enter a valid numeric Issue ID.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Searches for an actively issued book in the database based on IssueID.
        /// </summary>
        /// <param name="issueId">The IssueID to search for.</param>
        private void SearchIssuedBook(int issueId)
        {
            // Reset fields before a new search displays new data
            ResetForm();
            returnbooks_IssueID.Text = issueId.ToString(); // Keep the entered ID in the textbox

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string query = @"
                        SELECT
                            ib.IssueId,          -- Corrected casing from IssueID to IssueId
                            ib.BookId,           -- Corrected casing from BookID to BookId
                            ib.BorrowerId,       -- Corrected casing from BorrowerID to BorrowerId
                            b.BookTitle AS BookTitle, -- Corrected from b.Title to b.BookTitle
                            b.Author,
                            b.Status AS BookStatus,
                            br.Name AS BorrowerName,
                            br.Email AS BorrowerEmail,
                            br.Phone AS BorrowerPhone,
                            ib.IssueDate,
                            ib.DueDate,          -- Ensure this column exists in IssuedBooks
                            ib.ReturnDate,
                            ib.IsReturned        -- Ensure this column exists in IssuedBooks
                        FROM IssuedBooks ib
                        JOIN Books b ON ib.BookId = b.Id -- Corrected casing from ib.BookID to ib.BookId, and b.BookID to b.Id
                        JOIN Borrowers br ON ib.BorrowerId = br.BorrowerId -- Corrected casing from br.BorrowerID to br.BorrowerId
                        WHERE ib.IssueId = @IssueId AND ib.IsReturned = 0; -- Filters for unreturned books
                    ";

                    SqlCommand cmd = new SqlCommand(query, connect);
                    cmd.Parameters.AddWithValue("@IssueId", issueId); // Corrected parameter name casing

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Book found and is currently issued
                        currentIssueID = reader.GetInt32(reader.GetOrdinal("IssueId"));
                        currentBookID = reader.GetInt32(reader.GetOrdinal("BookId"));
                        currentBorrowerID = reader.GetInt32(reader.GetOrdinal("BorrowerId"));
                        currentIssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate"));
                        currentDueDate = reader.GetDateTime(reader.GetOrdinal("DueDate"));

                        // Populate TextBoxes and ComboBox using new, meaningful names
                        returnbooks_Name.Text = reader["BorrowerName"].ToString();
                        returnbooks_Contact.Text = reader["BorrowerPhone"].ToString();
                        textBox4.Text = reader["BorrowerEmail"].ToString();
                        textBox1.Text = reader["BookTitle"].ToString();
                        textBox6.Text = reader["Author"].ToString();
                        textBox7.Text = currentIssueDate.ToShortDateString();

                        // Populate Status ComboBox
                        string bookStatus = reader["BookStatus"].ToString();
                        if (!comboBox1.Items.Contains(bookStatus))
                        {
                            comboBox1.Items.Add(bookStatus);
                        }
                        comboBox1.SelectedItem = bookStatus;

                        // Calculate overdue days and fine (if any)
                        TimeSpan overdueSpan = DateTime.Today.Subtract(currentDueDate);
                        int overdueDays = (int)Math.Max(0, overdueSpan.TotalDays);

                        decimal fineRatePerDay = 5.00m; // Adjust as needed
                        calculatedFine = overdueDays * fineRatePerDay;

                        // Enable the Return button as a book is found
                        button1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No actively issued book found with this Issue ID. It might be incorrect, or the book has already been returned.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetForm();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Database error during search: " + ex.Message + "\n\nEnsure all required columns exist and are correctly named in your database tables (Books, Borrowers, IssuedBooks).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred during search: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the click event for the "Return" button.
        /// </summary>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (currentIssueID == -1 || currentBookID == -1)
            {
                MessageBox.Show("Please search for an issued book first.", "Action Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string confirmationMessage = $"Are you sure you want to return this book?\n\n" +
                                         $"Book: {textBox1.Text}\n" +
                                         $"Borrower: {returnbooks_Name.Text}\n" +
                                         $"Issued On: {textBox7.Text}\n";

            if (calculatedFine > 0)
            {
                confirmationMessage += $"A fine of {calculatedFine:C2} is due.\n\n";
            }
            confirmationMessage += "Confirm return?";

            if (MessageBox.Show(confirmationMessage, "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PerformBookReturn();
            }
        }

        /// <summary>
        /// Handles the click event for the "Clear" button.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        /// <summary>
        /// Performs the actual database operations to return the book.
        /// Uses a transaction to ensure all updates succeed or fail together.
        /// </summary>
        private void PerformBookReturn()
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    connect.Open();
                    transaction = connect.BeginTransaction();

                    // 1. Update IssuedBooks table: Set ReturnDate and IsReturned to true (1)
                    string updateIssuedSql = "UPDATE IssuedBooks SET ReturnDate = @ReturnDate, IsReturned = 1 WHERE IssueId = @IssueId;"; // Corrected casing for IssueId
                    SqlCommand updateIssuedCmd = new SqlCommand(updateIssuedSql, connect, transaction);
                    updateIssuedCmd.Parameters.AddWithValue("@ReturnDate", DateTime.Today);
                    updateIssuedCmd.Parameters.AddWithValue("@IssueId", currentIssueID); // Corrected parameter name casing
                    updateIssuedCmd.ExecuteNonQuery();

                    // 2. Update Books table: Increment AvailableCopies and update Status
                    string updateBookSql = @"
                        UPDATE Books
                        SET AvailableCopies = AvailableCopies + 1,
                            Status = CASE WHEN (AvailableCopies + 1) > 0 THEN 'Available' ELSE 'Unavailable' END
                        WHERE Id = @BookId; -- Corrected BookID to Id
                    ";
                    SqlCommand updateBookCmd = new SqlCommand(updateBookSql, connect, transaction);
                    updateBookCmd.Parameters.AddWithValue("@BookId", currentBookID); // Corrected parameter name casing
                    updateBookCmd.ExecuteNonQuery();

                    // 3. (Optional) Record the fine if any - Assumes a 'Fines' table exists
                    if (calculatedFine > 0)
                    {
                        string insertFineSql = "INSERT INTO Fines (IssueID, BorrowerID, BookID, FineAmount, FineDate, IsPaid) VALUES (@IssueID, @BorrowerID, @BookID, @FineAmount, @FineDate, 0);";
                        SqlCommand insertFineCmd = new SqlCommand(insertFineSql, connect, transaction);
                        insertFineCmd.Parameters.AddWithValue("@IssueID", currentIssueID);
                        insertFineCmd.Parameters.AddWithValue("@BorrowerID", currentBorrowerID);
                        insertFineCmd.Parameters.AddWithValue("@BookID", currentBookID);
                        insertFineCmd.Parameters.AddWithValue("@FineAmount", calculatedFine);
                        insertFineCmd.Parameters.AddWithValue("@FineDate", DateTime.Today);
                        insertFineCmd.ExecuteNonQuery();

                        MessageBox.Show($"Book returned successfully. Please collect a fine of {calculatedFine:C2} from the borrower.", "Book Returned with Fine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Book returned successfully.", "Book Returned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    transaction.Commit(); // Commit the transaction if all operations succeed
                    ResetForm(); // Reset the UI after successful return
                    LoadAllIssuedBooks(); // Refresh the DataGridView
                }
                catch (SqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback(); // Rollback on database error
                    }
                    MessageBox.Show("Database error during book return: " + ex.Message + "\n\nEnsure 'Fines' table exists and has all required columns, and all foreign keys are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback(); // Rollback on any other unexpected error
                    }
                    MessageBox.Show("An unexpected error occurred during book return: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Loads all currently issued books into the DataGridView on the right.
        /// </summary>
        public void LoadAllIssuedBooks()
        {
            // Ensure the DataGridView exists and is named 'dgvIssuedBooks' in your designer
            // If your DataGridView has a different name, change 'dgvIssuedBooks' here.
            if (dataGridView1 == null) return; // Using the new name

            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();
                    string query = @"
                        SELECT
                            ib.IssueId AS [Issue ID],
                            b.BookTitle AS [Book Title],
                            br.Name AS [Borrower Name],
                            ib.IssueDate AS [Issue Date],
                            ib.DueDate AS [Due Date],
                            ib.ReturnDate AS [Return Date],
                            CASE
                                WHEN ib.IsReturned = 1 THEN 'Returned'
                                ELSE 'Not Returned'
                            END AS Status
                        FROM IssuedBooks ib
                        JOIN Books b ON ib.BookId = b.Id -- Corrected casing for BookId and b.Id
                        JOIN Borrowers br ON ib.BorrowerId = br.BorrowerId -- Corrected casing for BorrowerId
                        WHERE ib.IsReturned = 0 -- Only show currently issued books
                        ORDER BY ib.IssueDate DESC; -- Corrected ORDER BY syntax
                    ";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt; // Using the new name
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells); // Using the new name
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading all issued books: " + ex.Message + "\n\nEnsure 'DueDate', 'IsReturned' exist in IssuedBooks and 'BookTitle', 'Author', 'Status', 'AvailableCopies' exist in Books table. Also check table names and column casings.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void returnbooks_IssueID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}