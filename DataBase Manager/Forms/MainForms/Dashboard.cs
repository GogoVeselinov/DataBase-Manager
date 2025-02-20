using System;
using System.Data.SqlClient;
using System.Data;
using DataBase_Manager;
using System.Drawing;
using System.Windows.Forms;
using DataBase_Manager.Forms.DatabaseForms;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DataBase_Manager
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            SetPlaceholderText(txtSearch, "Search Database...");
        }

        private void SetPlaceholderText(TextBox textBox, string placeholder)
        {
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholder;

            textBox.GotFocus += (sender, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.ForeColor = Color.Gray;
                    textBox.Text = placeholder;
                }
            };
        }

        private void LoadDatabases(string filter = "", string search = "")
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            database_id AS DatabaseID, 
                            name AS DatabaseName, 
                            create_date AS CreateDate, 
                            state_desc AS State
                        FROM sys.databases
                        WHERE database_id > 4"; // Exclude system databases

                    if (!string.IsNullOrEmpty(filter))
                    {
                        query += " AND state_desc = @State";
                    }

                    if (!string.IsNullOrEmpty(search) && search != "Search Database...")
                    {
                        query += " AND name LIKE @Search";
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    if (!string.IsNullOrEmpty(filter))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@State", filter);
                    }

                    if (!string.IsNullOrEmpty(search) && search != "Search Database...")
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@Search", "%" + search + "%");
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);
                    dataGridViewDatabases.DataSource = dataTable;

                    lblDatabaseCount.Text = $"Total Databases: {dataTable.Rows.Count}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading databases: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadDatabases();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDatabases();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDatabases(cmbFilter.SelectedItem?.ToString(), txtSearch.Text);
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatabases(cmbFilter.SelectedItem?.ToString(), txtSearch.Text);
        }

        private void btnCreateDB_Click(object sender, EventArgs e)
        {
            CreateDatabaseForm detailsForm = new CreateDatabaseForm();
            detailsForm.Show();
            this.Hide(); // Optionally hide the current form
        }

        private void btnDeleteDB_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatabases.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewDatabases.SelectedRows[0];
                string databaseName = selectedRow.Cells["DatabaseName"].Value.ToString();

                var confirmResult = MessageBox.Show($"Are you sure to delete the database '{databaseName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                        {
                            connection.Open();
                            string deleteDbQuery = $"DROP DATABASE [{databaseName}]";
                            using (SqlCommand command = new SqlCommand(deleteDbQuery, connection))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show($"Database '{databaseName}' deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatabases();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a database first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnViewInfo_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatabases.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewDatabases.SelectedRows[0];
                string databaseName = selectedRow.Cells["DatabaseName"].Value.ToString();

                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        connection.Open();
                        string viewInfoQuery = @"
                    SELECT 
                        name AS DatabaseName, 
                        create_date AS CreateDate, 
                        state_desc AS State,
                        recovery_model_desc AS RecoveryModel,
                        compatibility_level AS CompatibilityLevel
                    FROM sys.databases
                    WHERE name = @DatabaseName";

                        using (SqlCommand command = new SqlCommand(viewInfoQuery, connection))
                        {
                            command.Parameters.AddWithValue("@DatabaseName", databaseName);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string info = $"Database Name: {reader["DatabaseName"]}\n" +
                                                  $"Create Date: {reader["CreateDate"]}\n" +
                                                  $"State: {reader["State"]}\n" +
                                                  $"Recovery Model: {reader["RecoveryModel"]}\n" +
                                                  $"Compatibility Level: {reader["CompatibilityLevel"]}";
                                    MessageBox.Show(info, "Database Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving database info: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a database first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (dataGridViewDatabases.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewDatabases.SelectedRows[0];
                string databaseName = selectedRow.Cells["DatabaseName"].Value.ToString();

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Backup Files (*.bak)|*.bak";
                    saveFileDialog.FileName = $"{databaseName}.bak";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string backupPath = saveFileDialog.FileName;
                        string directoryPath = Path.GetDirectoryName(backupPath);

                        try
                        {
                            // Ensure the SQL Server service account has write permissions to the directory
                            FilePermissionHelper.GrantWritePermission(directoryPath);

                            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                            {
                                connection.Open();
                                string backupDbQuery = $@"
                            BACKUP DATABASE [{databaseName}]
                            TO DISK = @BackupPath";

                                using (SqlCommand command = new SqlCommand(backupDbQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@BackupPath", backupPath);
                                    command.ExecuteNonQuery();
                                }
                            }
                            MessageBox.Show($"Database '{databaseName}' backed up successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("Access denied. Please ensure the SQL Server service account has write permissions to the selected directory.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error backing up database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a database first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Backup Files (*.bak)|*.bak";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string backupPath = openFileDialog.FileName;
                    string directoryPath = Path.GetDirectoryName(backupPath);
                    string databaseName = Path.GetFileNameWithoutExtension(backupPath);

                    try
                    {
                        // Ensure the SQL Server service account has read permissions to the directory
                        FilePermissionHelper.GrantReadPermission(directoryPath);

                        using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                        {
                            connection.Open();
                            string restoreDbQuery = $@"
                        RESTORE DATABASE [{databaseName}]
                        FROM DISK = @BackupPath
                        WITH REPLACE";

                            using (SqlCommand command = new SqlCommand(restoreDbQuery, connection))
                            {
                                command.Parameters.AddWithValue("@BackupPath", backupPath);
                                command.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show($"Database '{databaseName}' restored successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDatabases();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show("Access denied. Please ensure the SQL Server service account has read permissions to the selected directory.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error restoring database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridViewDatabases_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewDatabases.Rows[e.RowIndex];
                string databaseName = selectedRow.Cells["DatabaseName"].Value.ToString();

                // Open the DatabaseDetailsForm for the selected database
                DatabaseDetailsForm detailsForm = new DatabaseDetailsForm(databaseName);
                detailsForm.Show();
            }
        }

    }
}
