using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Keep this one for SQL Server

// Remove these if not used, they aren't standard for basic ADO.NET ops
// using System.Diagnostics.Eventing.Reader;
// using System.Runtime.Remoting.Messaging;


namespace Library_Managem
{
    public partial class RegisterForm : Form
    {
        // Use a using statement for SqlConnection to ensure it's properly disposed
        // Also, it's generally better to open/close connection inside the method
        // if it's not reused across multiple rapid operations.
        // The connection string is fine for LocalDB.
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\fj\Documents\Library.mdf;Integrated Security=True;Connect Timeout=30";


        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            // Initial load logic if any
        }

        // Renamed from SignupBtn_Click to avoid confusion with Register_Button_Click
        // If SignupBtn is another button, you might need to handle its click event too.
        // For now, assuming Register_Button_Click is your main registration trigger.
        private void SignupBtn_Click(object sender, EventArgs e)
        {
            // This method might be empty or handle a different action (e.g., redirect to login)
        }


        private void Register_Button_Click(object sender, EventArgs e)
        {
            // 1. Input Validation: Check if any field is empty
            if (string.IsNullOrWhiteSpace(Register_Username.Text) ||
                string.IsNullOrWhiteSpace(Register_Password.Text) ||
                string.IsNullOrWhiteSpace(Register_Email.Text)) // Make sure Register_Email is linked to your email textbox
            {
                MessageBox.Show("Please fill in all fields.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Stop execution if validation fails
            }

            // Using a 'using' block for SqlConnection ensures proper disposal
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                try
                {
                    connect.Open();

                    // 2. Check if username already exists
                    string checkUsernameQuery = "SELECT COUNT(*) FROM users WHERE Username = @Username";
                    using (SqlCommand cmdCheckUsername = new SqlCommand(checkUsernameQuery, connect))
                    {
                        cmdCheckUsername.Parameters.AddWithValue("@Username", Register_Username.Text.Trim());
                        int usernameCount = (int)cmdCheckUsername.ExecuteScalar();

                        if (usernameCount > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose another.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Stop execution
                        }
                    }

                    // 3. Check if email already exists (important since Email is UNIQUE)
                    string checkEmailQuery = "SELECT COUNT(*) FROM users WHERE Email = @Email";
                    using (SqlCommand cmdCheckEmail = new SqlCommand(checkEmailQuery, connect))
                    {
                        cmdCheckEmail.Parameters.AddWithValue("@Email", Register_Email.Text.Trim());
                        int emailCount = (int)cmdCheckEmail.ExecuteScalar();

                        if (emailCount > 0)
                        {
                            MessageBox.Show("Email already registered. Please use another email or log in.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Stop execution
                        }
                    }

                    // 4. Insert new user (Now including Email!)
                    string insertQuery = "INSERT INTO users (Username, Password, Email, date_register) VALUES (@Username, @Password, @Email, @DateRegister)";
                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, connect))
                    {
                        cmdInsert.Parameters.AddWithValue("@Username", Register_Username.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Password", Register_Password.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@Email", Register_Email.Text.Trim());
                        cmdInsert.Parameters.AddWithValue("@DateRegister", DateTime.Now.Date); // Use DateTime.Now.Date for just the date part

                        cmdInsert.ExecuteNonQuery(); // Execute the insert command
                        MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5. Clear fields after successful registration
                        Register_Username.Text = "";
                        Register_Password.Text = "";
                        Register_Email.Text = "";
                    }
                }
                catch (SqlException ex)
                {
                    // Catch specific SQL errors for better debugging
                    MessageBox.Show($"Database Error: {ex.Message}\nSQL Error Code: {ex.Number}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // 'finally' block is not strictly necessary with 'using' for SqlConnection
                // as 'using' handles automatic closing and disposal.
            }
        }

        private void Register_Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            // The '0' password char might show '0's instead of bullets. '*' is more standard.
            Register_Password.PasswordChar = Register_Showpassword.Checked ? '\0' : '*'; // '\0' makes text visible
        }

        private void Register_Password_TextChanged(object sender, EventArgs e)
        {
            // Can add validation here if needed
        }

        private void Register_username_TextChanged(object sender, EventArgs e)
        {
            // Can add validation here if needed
        }
    }
}