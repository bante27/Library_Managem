using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Library_Managem
{
    public partial class IssueBook : UserControl
    {
        // MODIFIED: Connection string as per your previous code
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fj\Documents\Library.mdf;Integrated Security=True;Connect Timeout=30";

        public IssueBook()
        {
            InitializeComponent();
            // Assign event handlers
            this.Load += IssueBook_Load;
            this.issuebooks_AddBtn.Click += issuebooks_AddBtn_Click;
            this.issuebooks_DeleteBtn.Click += issuebooks_DeleteBtn_Click;
            this.issuebooks_UpdateBtn.Click += issuebooks_UpdateBtn_Click;
            this.issuebooks_ClearBtn.Click += issuebooks_ClearBtn_Click;
            this.dataGridViewAllIssuedBooks.SelectionChanged += dataGridViewAllIssuedBooks_SelectionChanged;

            // Optional: Make Book Title and Author read-only if you intend to select them
            // via a search/lookup mechanism rather than direct input.
            // For now, we'll leave them editable, but you could implement a search button.
            // issuebooks_Author.ReadOnly = true;
        }

        private void IssueBook_Load(object sender, EventArgs e)
        {
            LoadAllIssuedBooks(); // Load existing issued books into the DataGridView
            issuebooks_ReturnDate.Enabled = false; // Return Date is initially disabled/read-only for new issues
            // Set default issue date to today
            issuebooks_IssueDate.Value = DateTime.Today;
        }

        private void LoadAllIssuedBooks()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Select all relevant information about issued books
                    // Join Books and Borrowers tables to get friendly names
                    string query = @"
                        SELECT
                            ib.IssueId,
                            b.Id AS BookId, -- Include BookId for later updates/returns
                            br.BorrowerId, -- Include BorrowerId for later updates/returns
                            b.BookTitle,
                            b.Author,
                            br.Name AS BorrowerName,
                            br.Contact AS BorrowerContact,
                            br.Email AS BorrowerEmail,
                            ib.IssueDate,
                            ib.ReturnDate,
                            b.Status AS BookStatus -- The status of the book from the Books table
                        FROM IssuedBooks ib
                        INNER JOIN Books b ON ib.BookId = b.Id
                        INNER JOIN Borrowers br ON ib.BorrowerId = br.BorrowerId
                        ORDER BY ib.IssueDate DESC"; // Order by most recent issues first

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridViewAllIssuedBooks.DataSource = dataTable;
                        dataGridViewAllIssuedBooks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                        // Hide technical ID columns from the user
                        if (dataGridViewAllIssuedBooks.Columns.Contains("IssueId"))
                        {
                            dataGridViewAllIssuedBooks.Columns["IssueId"].Visible = false;
                        }
                        if (dataGridViewAllIssuedBooks.Columns.Contains("BookId"))
                        {
                            dataGridViewAllIssuedBooks.Columns["BookId"].Visible = false;
                        }
                        if (dataGridViewAllIssuedBooks.Columns.Contains("BorrowerId"))
                        {
                            dataGridViewAllIssuedBooks.Columns["BorrowerId"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading issued books: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void issuebooks_AddBtn_Click(object sender, EventArgs e)
        {
            // Input validation for required fields
            if (string.IsNullOrWhiteSpace(issuebooks_Name.Text) ||
                string.IsNullOrWhiteSpace(issuebooks_BookTitle.Text))
            {
                MessageBox.Show("Please fill in Borrower Name and Book Title.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookId = -1;
            int borrowerId = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Open connection once for multiple operations

                    // Step 1: Find BookId and check its status
                    string getBookQuery = "SELECT Id, Author, Status FROM Books WHERE BookTitle = @BookTitle";
                    using (SqlCommand cmd = new SqlCommand(getBookQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@BookTitle", issuebooks_BookTitle.Text.Trim());
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bookId = Convert.ToInt32(reader["Id"]);
                                string currentStatus = reader["Status"].ToString();
                                issuebooks_Author.Text = reader["Author"].ToString(); // Auto-fill author
                                if (currentStatus != "Available")
                                {
                                    MessageBox.Show($"'{issuebooks_BookTitle.Text.Trim()}' is currently '{currentStatus}' and cannot be issued.", "Book Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Stop if book is not available
                                }
                            }
                            else
                            {
                                MessageBox.Show("Book not found. Please ensure the Book Title is correct.", "Book Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Stop if book is not found
                            }
                        }
                    }

                    // Step 2: Find or Add BorrowerId
                    // First, try to find an existing borrower by Name AND Contact/Email
                    string findBorrowerQuery = "SELECT BorrowerId FROM Borrowers WHERE Name = @Name AND (Contact = @Contact OR Email = @Email)";
                    using (SqlCommand findCmd = new SqlCommand(findBorrowerQuery, connection))
                    {
                        findCmd.Parameters.AddWithValue("@Name", issuebooks_Name.Text.Trim());
                        findCmd.Parameters.AddWithValue("@Contact", issuebooks_Contact.Text.Trim());
                        findCmd.Parameters.AddWithValue("@Email", issuebooks_Email.Text.Trim());
                        object result = findCmd.ExecuteScalar(); // Executes the query and returns the first column of the first row

                        if (result != null)
                        {
                            borrowerId = Convert.ToInt32(result); // Borrower found
                        }
                        else
                        {
                            // Borrower not found, add new borrower
                            string addBorrowerQuery = "INSERT INTO Borrowers (Name, Contact, Email) VALUES (@Name, @Contact, @Email); SELECT SCOPE_IDENTITY();";
                            using (SqlCommand addCmd = new SqlCommand(addBorrowerQuery, connection))
                            {
                                addCmd.Parameters.AddWithValue("@Name", issuebooks_Name.Text.Trim());
                                addCmd.Parameters.AddWithValue("@Contact", issuebooks_Contact.Text.Trim());
                                addCmd.Parameters.AddWithValue("@Email", issuebooks_Email.Text.Trim());
                                borrowerId = Convert.ToInt32(addCmd.ExecuteScalar()); // Get the ID of the newly inserted borrower
                            }
                        }
                    }

                    // Step 3: Insert into IssuedBooks
                    string insertIssueQuery = "INSERT INTO IssuedBooks (BookId, BorrowerId, IssueDate, ReturnDate) VALUES (@BookId, @BorrowerId, @IssueDate, NULL)";
                    using (SqlCommand command = new SqlCommand(insertIssueQuery, connection))
                    {
                        command.Parameters.AddWithValue("@BookId", bookId);
                        command.Parameters.AddWithValue("@BorrowerId", borrowerId);
                        command.Parameters.AddWithValue("@IssueDate", issuebooks_IssueDate.Value.Date); // Use .Date to store only date part
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Step 4: Update Book Status to 'Issued' in Books table
                            string updateBookStatusQuery = "UPDATE Books SET Status = 'Issued' WHERE Id = @BookId";
                            using (SqlCommand updateCmd = new SqlCommand(updateBookStatusQuery, connection))
                            {
                                updateCmd.Parameters.AddWithValue("@BookId", bookId);
                                updateCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Book issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadAllIssuedBooks(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to issue the book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void issuebooks_UpdateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllIssuedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an issued book entry to update from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(issuebooks_Name.Text) ||
                string.IsNullOrWhiteSpace(issuebooks_BookTitle.Text))
            {
                MessageBox.Show("Please fill in Borrower Name and Book Title for the update.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewAllIssuedBooks.SelectedRows[0];
            // Retrieve IDs from the hidden columns in the DataGridView
            int issueIdToUpdate = Convert.ToInt32(selectedRow.Cells["IssueId"].Value);
            int originalBookId = Convert.ToInt32(selectedRow.Cells["BookId"].Value); // The book ID linked to this issue record
            int originalBorrowerId = Convert.ToInt32(selectedRow.Cells["BorrowerId"].Value); // The borrower ID linked to this issue record

            int newBookId = originalBookId; // Assume book remains the same initially
            int newBorrowerId = originalBorrowerId; // Assume borrower remains the same initially

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if book title has changed. If so, find new book ID and update statuses.
                    if (issuebooks_BookTitle.Text.Trim() != selectedRow.Cells["BookTitle"].Value.ToString())
                    {
                        // Find the new BookId and check its status
                        string getNewBookQuery = "SELECT Id, Author, Status FROM Books WHERE BookTitle = @NewBookTitle";
                        using (SqlCommand cmd = new SqlCommand(getNewBookQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@NewBookTitle", issuebooks_BookTitle.Text.Trim());
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    newBookId = Convert.ToInt32(reader["Id"]);
                                    string newBookStatus = reader["Status"].ToString();
                                    issuebooks_Author.Text = reader["Author"].ToString(); // Update author field
                                    if (newBookStatus != "Available" && newBookId != originalBookId) // Only if changing to a different book
                                    {
                                        MessageBox.Show($"The new book '{issuebooks_BookTitle.Text.Trim()}' is currently '{newBookStatus}' and cannot be issued.", "Book Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("New Book Title not found. Please ensure it's correct.", "Book Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }

                    // Check if borrower details have changed. If so, find/add new borrower ID.
                    if (issuebooks_Name.Text.Trim() != selectedRow.Cells["BorrowerName"].Value.ToString() ||
                        issuebooks_Contact.Text.Trim() != selectedRow.Cells["BorrowerContact"].Value.ToString() ||
                        issuebooks_Email.Text.Trim() != selectedRow.Cells["BorrowerEmail"].Value.ToString())
                    {
                        string findBorrowerQuery = "SELECT BorrowerId FROM Borrowers WHERE Name = @Name AND (Contact = @Contact OR Email = @Email)";
                        using (SqlCommand findCmd = new SqlCommand(findBorrowerQuery, connection))
                        {
                            findCmd.Parameters.AddWithValue("@Name", issuebooks_Name.Text.Trim());
                            findCmd.Parameters.AddWithValue("@Contact", issuebooks_Contact.Text.Trim());
                            findCmd.Parameters.AddWithValue("@Email", issuebooks_Email.Text.Trim());
                            object result = findCmd.ExecuteScalar();

                            if (result != null)
                            {
                                newBorrowerId = Convert.ToInt32(result);
                            }
                            else
                            {
                                // Borrower not found, add new borrower
                                string addBorrowerQuery = "INSERT INTO Borrowers (Name, Contact, Email) VALUES (@Name, @Contact, @Email); SELECT SCOPE_IDENTITY();";
                                using (SqlCommand addCmd = new SqlCommand(addBorrowerQuery, connection))
                                {
                                    addCmd.Parameters.AddWithValue("@Name", issuebooks_Name.Text.Trim());
                                    addCmd.Parameters.AddWithValue("@Contact", issuebooks_Contact.Text.Trim());
                                    addCmd.Parameters.AddWithValue("@Email", issuebooks_Email.Text.Trim());
                                    newBorrowerId = Convert.ToInt32(addCmd.ExecuteScalar());
                                }
                            }
                        }
                    }


                    // Update the IssuedBooks entry
                    string updateIssueQuery = "UPDATE IssuedBooks SET BookId = @NewBookId, BorrowerId = @NewBorrowerId, IssueDate = @IssueDate, ReturnDate = @ReturnDate WHERE IssueId = @IssueId";
                    using (SqlCommand command = new SqlCommand(updateIssueQuery, connection))
                    {
                        command.Parameters.AddWithValue("@NewBookId", newBookId);
                        command.Parameters.AddWithValue("@NewBorrowerId", newBorrowerId);
                        command.Parameters.AddWithValue("@IssueDate", issuebooks_IssueDate.Value.Date);
                        // Only update ReturnDate if it's explicitly set (e.g., if re-issuing a returned book)
                        // For a general update, we'll keep it as it was if not interacted with.
                        if (issuebooks_ReturnDate.Enabled && issuebooks_ReturnDate.Value.Date != DateTime.MinValue.Date)
                        {
                            command.Parameters.AddWithValue("@ReturnDate", issuebooks_ReturnDate.Value.Date);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ReturnDate", DBNull.Value); // Keep as NULL if not explicitly set
                        }

                        command.Parameters.AddWithValue("@IssueId", issueIdToUpdate);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            // Handle book status changes if book ID was changed
                            if (newBookId != originalBookId)
                            {
                                // Set old book's status back to 'Available' (only if no other issues for it exist)
                                // This is a simplified check. A more robust system would check if it's the *only* outstanding issue.
                                string updateOldBookStatus = "UPDATE Books SET Status = 'Available' WHERE Id = @OldBookId AND NOT EXISTS (SELECT 1 FROM IssuedBooks WHERE BookId = @OldBookId AND ReturnDate IS NULL)";
                                using (SqlCommand updateOldCmd = new SqlCommand(updateOldBookStatus, connection))
                                {
                                    updateOldCmd.Parameters.AddWithValue("@OldBookId", originalBookId);
                                    updateOldCmd.ExecuteNonQuery();
                                }

                                // Set new book's status to 'Issued'
                                string updateNewBookStatus = "UPDATE Books SET Status = 'Issued' WHERE Id = @NewBookId";
                                using (SqlCommand updateNewCmd = new SqlCommand(updateNewBookStatus, connection))
                                {
                                    updateNewCmd.Parameters.AddWithValue("@NewBookId", newBookId);
                                    updateNewCmd.ExecuteNonQuery();
                                }
                            }
                            else // If book title itself didn't change, but borrower/dates did, ensure status is still 'Issued' if return date is null
                            {
                                if (issuebooks_ReturnDate.Value.Date == DateTime.MinValue.Date) // If still issued
                                {
                                    string ensureIssuedStatus = "UPDATE Books SET Status = 'Issued' WHERE Id = @BookId";
                                    using (SqlCommand ensureCmd = new SqlCommand(ensureIssuedStatus, connection))
                                    {
                                        ensureCmd.Parameters.AddWithValue("@BookId", newBookId);
                                        ensureCmd.ExecuteNonQuery();
                                    }
                                }
                                else // If it was returned
                                {
                                    string ensureAvailableStatus = "UPDATE Books SET Status = 'Available' WHERE Id = @BookId";
                                    using (SqlCommand ensureCmd = new SqlCommand(ensureAvailableStatus, connection))
                                    {
                                        ensureCmd.Parameters.AddWithValue("@BookId", newBookId);
                                        ensureCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            MessageBox.Show("Issued book entry updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadAllIssuedBooks(); // Refresh DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the issued book entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error during update: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void issuebooks_DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllIssuedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an issued book entry to delete from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewAllIssuedBooks.SelectedRows[0];
            int issueIdToDelete = Convert.ToInt32(selectedRow.Cells["IssueId"].Value);
            int bookIdAssociated = Convert.ToInt32(selectedRow.Cells["BookId"].Value); // Get associated BookId
            string bookTitle = selectedRow.Cells["BookTitle"].Value?.ToString();
            string borrowerName = selectedRow.Cells["BorrowerName"].Value?.ToString();

            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete the issue record for '{bookTitle}' by '{borrowerName}'? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Step 1: Delete from IssuedBooks table
                        string deleteQuery = "DELETE FROM IssuedBooks WHERE IssueId = @IssueId";
                        using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                        {
                            command.Parameters.AddWithValue("@IssueId", issueIdToDelete);
                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                // Step 2: Potentially update Book Status back to 'Available'
                                // Only update if no other active issues exist for this book
                                string checkActiveIssuesQuery = "SELECT COUNT(*) FROM IssuedBooks WHERE BookId = @BookId AND ReturnDate IS NULL";
                                using (SqlCommand checkCmd = new SqlCommand(checkActiveIssuesQuery, connection))
                                {
                                    checkCmd.Parameters.AddWithValue("@BookId", bookIdAssociated);
                                    int activeIssuesCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                                    if (activeIssuesCount == 0)
                                    {
                                        // If no other active issues, set book status to 'Available'
                                        string updateBookStatusQuery = "UPDATE Books SET Status = 'Available' WHERE Id = @BookId";
                                        using (SqlCommand updateCmd = new SqlCommand(updateBookStatusQuery, connection))
                                        {
                                            updateCmd.Parameters.AddWithValue("@BookId", bookIdAssociated);
                                            updateCmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                                MessageBox.Show("Issued book entry deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ClearForm();
                                LoadAllIssuedBooks(); // Refresh DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the issued book entry.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error during deletion: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void issuebooks_ClearBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            // Clear all textboxes and reset date pickers
            issuebooks_Name.Text = "";
            issuebooks_Email.Text = "";
            issuebooks_Contact.Text = "";
            issuebooks_BookTitle.Text = "";
            issuebooks_Author.Text = ""; // Clear author as it's typically derived
            issuebooks_IssueDate.Value = DateTime.Today;
            issuebooks_ReturnDate.Value = DateTime.Today; // Reset for consistency, though it's disabled
            issuebooks_ReturnDate.Enabled = false; // Ensure it's disabled for new entries
            // Clear selection in DataGridView
            dataGridViewAllIssuedBooks.ClearSelection();
        }

        private void dataGridViewAllIssuedBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAllIssuedBooks.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewAllIssuedBooks.SelectedRows[0];

                // Populate form fields from the selected row
                issuebooks_Name.Text = selectedRow.Cells["BorrowerName"].Value?.ToString() ?? "";
                issuebooks_Contact.Text = selectedRow.Cells["BorrowerContact"].Value?.ToString() ?? "";
                issuebooks_Email.Text = selectedRow.Cells["BorrowerEmail"].Value?.ToString() ?? "";
                issuebooks_BookTitle.Text = selectedRow.Cells["BookTitle"].Value?.ToString() ?? "";
                issuebooks_Author.Text = selectedRow.Cells["Author"].Value?.ToString() ?? "";

                // Handle IssueDate
                if (selectedRow.Cells["IssueDate"].Value != null &&
                    DateTime.TryParse(selectedRow.Cells["IssueDate"].Value.ToString(), out DateTime issueDate))
                {
                    issuebooks_IssueDate.Value = issueDate;
                }
                else
                {
                    issuebooks_IssueDate.Value = DateTime.Today;
                }

                // Handle ReturnDate - enable it for updating, but set to default if null in DB
                if (selectedRow.Cells["ReturnDate"].Value != DBNull.Value &&
                    selectedRow.Cells["ReturnDate"].Value != null &&
                    DateTime.TryParse(selectedRow.Cells["ReturnDate"].Value.ToString(), out DateTime returnDate))
                {
                    issuebooks_ReturnDate.Value = returnDate;
                    issuebooks_ReturnDate.Enabled = true; // Enable if a return date exists
                }
                else
                {
                    issuebooks_ReturnDate.Value = DateTime.Today; // Set to today if null (not yet returned)
                    issuebooks_ReturnDate.Enabled = false; // Keep disabled for active issues (return handled by 'Return Book' module)
                }

                // If you have an "Issue ID" textbox for display:
                // issuebooks_IssueID.Text = selectedRow.Cells["IssueId"].Value?.ToString() ?? "";

            }
            else
            {
                ClearForm(); // Clear form when no row is selected
            }
        }
    }
}
