using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Library_Managem
{
    public partial class AddBook : UserControl
    {
        // MODIFIED: Changed connection string to use |DataDirectory|
        // This makes the database path relative to your application's executable.
        // It's crucial that Library.mdf is set to "Copy if newer" or "Copy always" in its properties
        // in your Visual Studio project to ensure it's copied to the output directory (e.g., bin\Debug).
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fj\Documents\Library.mdf;Integrated Security=True;Connect Timeout=30";
        public AddBook()
        {
            InitializeComponent();
            this.dataGridViewAllBooks.SelectionChanged += new System.EventHandler(this.dataGridViewAllBooks_SelectionChanged);
        }

        private void AddBook_Load(object sender, EventArgs e)
        {
            PopulateStatusComboBox();
            LoadAllBooks();
        }

        private void PopulateStatusComboBox()
        {
            addbooks_states.Items.Clear();
            addbooks_states.Items.Add("Available");
            addbooks_states.Items.Add("Issued");
            addbooks_states.Items.Add("Lost");
            addbooks_states.Items.Add("Damaged");

            if (addbooks_states.Items.Contains("Available"))
            {
                addbooks_states.SelectedItem = "Available";
            }
            else if (addbooks_states.Items.Count > 0)
            {
                addbooks_states.SelectedIndex = 0;
            }
            else
            {
                addbooks_states.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This method is empty. If you need functionality here, add it.
        }

        private void addBooks_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addBooks_title.Text) || string.IsNullOrWhiteSpace(addBooks_authore.Text))
            {
                MessageBox.Show("Please enter both title and author.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Books (BookTitle, Author, PublishedDate, Status) " +
                                   "VALUES (@BookTitle, @Author, @PublishedDate, @Status)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookTitle", addBooks_title.Text.Trim());
                        command.Parameters.AddWithValue("@Author", addBooks_authore.Text.Trim());
                        command.Parameters.AddWithValue("@PublishedDate", addBooks_published.Value);
                        command.Parameters.AddWithValue("@Status", "Available"); // Default to Available when adding

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadAllBooks(); // Refresh the DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            addBooks_title.Text = "";
            addBooks_authore.Text = "";
            addBooks_published.Value = DateTime.Today; // Reset date to today
            addbooks_states.SelectedIndex = -1; // Clear selection
            addbooks_states.Text = ""; // Also clear the text
        }

        private void addBooks_upbtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to update from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(addBooks_title.Text) || string.IsNullOrWhiteSpace(addBooks_authore.Text))
            {
                MessageBox.Show("Please fill in both title and author for the update.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewAllBooks.SelectedRows[0];
            // FIX: Changed "id" to "Id" to match database schema
            if (selectedRow.Cells["Id"].Value == null)
            {
                MessageBox.Show("Could not retrieve Book ID for update. Please select a valid row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // FIX: Changed "id" to "Id" to match database schema
            int bookIDToUpdate = Convert.ToInt32(selectedRow.Cells["Id"].Value);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Books SET BookTitle = @BookTitle, Author = @Author, " +
                                   "PublishedDate = @PublishedDate, Status = @Status " +
                                   "WHERE Id = @BookID"; // FIX: Changed "id" to "Id" in WHERE clause

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookTitle", addBooks_title.Text.Trim());
                        command.Parameters.AddWithValue("@Author", addBooks_authore.Text.Trim());
                        command.Parameters.AddWithValue("@PublishedDate", addBooks_published.Value);
                        command.Parameters.AddWithValue("@Status", addbooks_states.SelectedItem?.ToString() ?? "Unknown");
                        command.Parameters.AddWithValue("@BookID", bookIDToUpdate);

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadAllBooks(); // Refresh the DataGridView
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the book. Check if the book exists or data is unchanged.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error during update: " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addBooks_delbtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewAllBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to delete from the list.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewAllBooks.SelectedRows[0];
            // FIX: Changed "id" to "Id" to match database schema
            if (selectedRow.Cells["Id"].Value == null)
            {
                MessageBox.Show("Could not retrieve Book ID for deletion. Please select a valid row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // FIX: Changed "id" to "Id" to match database schema
            int bookIDToDelete = Convert.ToInt32(selectedRow.Cells["Id"].Value);
            string bookTitleToDelete = selectedRow.Cells["BookTitle"].Value?.ToString();

            DialogResult confirm = MessageBox.Show($"Are you sure you want to delete '{bookTitleToDelete}'? This action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // FIX: Changed "id" to "Id" in WHERE clause
                        string query = "DELETE FROM Books WHERE Id = @BookID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@BookID", bookIDToDelete);

                            connection.Open();
                            int result = command.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadAllBooks(); // Refresh the DataGridView
                                ClearForm();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete the book.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void addBooks_clbtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridViewAllBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAllBooks.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewAllBooks.SelectedRows[0];

                addBooks_title.Text = selectedRow.Cells["BookTitle"].Value?.ToString() ?? "";
                addBooks_authore.Text = selectedRow.Cells["Author"].Value?.ToString() ?? "";

                if (selectedRow.Cells["PublishedDate"].Value != null &&
                    DateTime.TryParse(selectedRow.Cells["PublishedDate"].Value.ToString(), out DateTime publishedDate))
                {
                    addBooks_published.Value = publishedDate;
                }
                else
                {
                    addBooks_published.Value = DateTime.Today;
                }

                if (selectedRow.Cells["Status"].Value != null)
                {
                    string status = selectedRow.Cells["Status"].Value.ToString();
                    int index = addbooks_states.FindStringExact(status);
                    if (index != -1)
                    {
                        addbooks_states.SelectedIndex = index;
                    }
                    else
                    {
                        addbooks_states.SelectedIndex = -1;
                    }
                }
                else
                {
                    addbooks_states.SelectedIndex = -1;
                }
            }
            else
            {
                ClearForm();
            }
        }

        private void LoadAllBooks()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Selecting "Id" as per your database schema
                    string query = "SELECT Id, BookTitle, Author, PublishedDate, Status FROM Books ORDER BY BookTitle ASC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();

                        adapter.Fill(dataTable);

                        dataGridViewAllBooks.DataSource = dataTable;
                        dataGridViewAllBooks.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

                        // Hiding the "Id" column in DataGridView. FIX: Changed "id" to "Id"
                        if (dataGridViewAllBooks.Columns.Contains("Id"))
                        {
                            dataGridViewAllBooks.Columns["Id"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Error loading books: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}