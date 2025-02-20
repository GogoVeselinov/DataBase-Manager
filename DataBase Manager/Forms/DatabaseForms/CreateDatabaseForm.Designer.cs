using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DataBase_Manager.Forms.DatabaseForms
{
    public partial class CreateDatabaseForm : Form
    {
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string databaseName = txtDatabaseName.Text.Trim();
            int sizeMB = (int)numSize.Value;
            string dbDirectory = "C:\\SQLData";
            string dataFilePath = Path.Combine(dbDirectory, $"{databaseName}.mdf");
            string logFilePath = Path.Combine(dbDirectory, $"{databaseName}.ldf");

            if (string.IsNullOrWhiteSpace(databaseName))
            {
                MessageBox.Show("Please enter a database name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Ensure the directory exists
                if (!Directory.Exists(dbDirectory))
                {
                    Directory.CreateDirectory(dbDirectory);
                }

                string createDbQuery = $@"
            CREATE DATABASE [{databaseName}]
            ON PRIMARY 
            (NAME = N'{databaseName}_Data', FILENAME = N'{dataFilePath}', SIZE = {sizeMB}MB)
            LOG ON 
            (NAME = N'{databaseName}_Log', FILENAME = N'{logFilePath}', SIZE = 5MB)
        ";

                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(createDbQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Database '{databaseName}' created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

                // Reopen the MainForm (dashboard)
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dashboard mainForm = new Dashboard();
            mainForm.Show();
            this.Close();
        }
    }
}
