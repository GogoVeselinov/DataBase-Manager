using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase_Manager.Forms.AuthForms
{
    public partial class LoginRegisterForm : Form
    {
        public LoginRegisterForm()
        {
            InitializeComponent();
            EnsureDatabaseAndTable();
        }

        private void btnSwitchMode_Click(object sender, EventArgs e)
        {
            if (btnSwitchMode.Text == "Switch to Register")
            {
                btnSwitchMode.Text = "Switch to Login";
                btnAction.Text = "Register";
                lblConfirmPassword.Visible = true;
                txtConfirmPassword.Visible = true;
            }
            else
            {
                btnSwitchMode.Text = "Switch to Register";
                btnAction.Text = "Login";
                lblConfirmPassword.Visible = false;
                txtConfirmPassword.Visible = false;
            }
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (btnAction.Text == "Register")
            {
                string confirmPassword = txtConfirmPassword.Text;
                if (password != confirmPassword)
                {
                    MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Role", "User"); // Default role
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@Username AND Password=@Password";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count == 1)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Dashboard dashboard = new Dashboard();
                            dashboard.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void EnsureDatabaseAndTable()
        {
            string masterConnectionString = "Server=localhost;Integrated Security=true;Database=master;";
            string createDbQuery = "IF DB_ID('DataBase_Manager') IS NULL CREATE DATABASE DataBase_Manager;";
            string createTableQuery = @"
        IF OBJECT_ID('DataBase_Manager.dbo.Users', 'U') IS NULL
        CREATE TABLE DataBase_Manager.dbo.Users (
            Id INT IDENTITY(1,1) PRIMARY KEY,
            Username NVARCHAR(50) NOT NULL,
            Password NVARCHAR(50) NOT NULL,
            Role NVARCHAR(50) NOT NULL
        );";

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }
    }
}
