using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Ensure this is present for database operations

namespace Library_Managem
{
    public partial class MainForm : Form
    {
        // Database connection string
        SqlConnection connect = new SqlConnection(
        @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fj\Documents\Library.mdf;Integrated Security=True;Connect Timeout=30;");

        // Reference to the currently active UserControl in panel1
        private UserControl currentContentControl;

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Library Management System";
        }

        // --- Helper Methods to Load Content into panel1 ---

       private void LoadPanelContent(UserControl contentControl)
{
    panel1.Controls.Clear();
    currentContentControl = contentControl;
    currentContentControl.Dock = DockStyle.Fill;
    panel1.Controls.Add(currentContentControl);
}


        // --- Data Loading Methods (Used by Dashboard, Add/Edit/Delete etc.) ---

        public void LoadBooksData(DataGridView dgv)
        {
            try
            {
                connect.Open();
                string query = "SELECT BookID, Title, Author, PublicationYear, TotalCopies, AvailableCopies FROM Books";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        public void LoadBorrowersData(DataGridView dgv)
        {
            try
            {
                connect.Open();
                string query = "SELECT BorrowerID, Name, Email, Phone FROM Borrowers";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading borrowers: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        public void LoadIssuedBooksData(DataGridView dgv)
        {
            try
            {
                connect.Open();
                string query = @"
                    SELECT
                        ib.IssueID,
                        b.Title AS BookTitle,
                        br.Name AS BorrowerName,
                        ib.IssueDate,
                        ib.DueDate,
                        ib.ReturnDate
                    FROM IssuedBooks ib
                    JOIN Books b ON ib.BookID = b.BookID
                    JOIN Borrowers br ON ib.BorrowerID = br.BorrowerID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading issued books: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        // --- MainForm Event Handlers ---

        private void MainForm_Load(object sender, EventArgs e)
        {
            // You might load a default dashboard or books view here.
            // For example, simulate click on Dashboard button (button1)
            button1_Click(sender, e);
        }

        // Handles the "X" button (label3 in your image)
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // No specific logic here.
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // No specific logic here.
        }

        // Handles the "DashBord" button click (button1 in your image)
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            DataGridView dgv = new DataGridView();
            dgv.Name = "dgvBooksDashboard";
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            panel1.Controls.Add(dgv);
            LoadBooksData(dgv);
        }

        // Handles the "Add Book" button click (button2 in your image)
        private void button2_Click(object sender, EventArgs e)
        {
            // Assuming AddEditBookForm is properly defined for adding/editing books
            // You will need to create and implement this form.
            // AddEditBookForm addBookForm = new AddEditBookForm();
            // if (addBookForm.ShowDialog() == DialogResult.OK)
            // {
            //     // Refresh the books data grid after adding
            //     DataGridView dgvBooksDashboard = panel1.Controls.OfType<DataGridView>().FirstOrDefault(d => d.Name == "dgvBooksDashboard");
            //     if (dgvBooksDashboard != null) { LoadBooksData(dgvBooksDashboard); }
            // }
            MessageBox.Show("Add Book functionality to be implemented. Open AddEditBookForm.", "Info");
        }

        // Handles the "Issue Book" button click (button3 in your image)
        private void button3_Click(object sender, EventArgs e)
        {
            // Assuming an IssueBookForm or similar setup
            // IssueBookForm issueBookForm = new IssueBookForm();
            // if (issueBookForm.ShowDialog() == DialogResult.OK)
            // {
            //    // Refresh relevant data grids
            // }
            MessageBox.Show("Issue Book functionality to be implemented. Open IssueBookForm.", "Info");
        }

        // Handles the "Return Book" button click (button4 in your image)
        private void button4_Click(object sender, EventArgs e)
        {
            // Create an instance of the Return_Issue user control
            Return_Issue returnIssueControl = new Return_Issue();

            // Use your helper method to load the control into panel1
            LoadPanelContent(returnIssueControl);
        }


        // --- NEW: Logout Functionality ---
        // This method handles the click event for the "Logout" label (label4 in your image)
        private void label4_Click(object sender, EventArgs e)
        {
            // Confirm with the user before logging out
            if (MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Close the current MainForm
                this.Close();

                // Create and show a new instance of the LoginForm
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dashBord1.Visible = true;
            addBook1.Visible =false;
            return_Issue1.Visible = false;
            issueBook1.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dashBord1.Visible = false;
            addBook1.Visible = true;
            return_Issue1.Visible = false;
            issueBook1.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            dashBord1.Visible = false;
            addBook1.Visible = false;
            return_Issue1.Visible = false;
            issueBook1.Visible = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dashBord1.Visible = false;
            addBook1.Visible = false;
            return_Issue1.Visible = true;
            issueBook1.Visible = false;
        }

        // Placeholder for other buttons/controls if you add them later, e.g., for Borrowers Management
        // private void btnBorrowersManagement_Click(object sender, EventArgs e) { ... }
    }
}