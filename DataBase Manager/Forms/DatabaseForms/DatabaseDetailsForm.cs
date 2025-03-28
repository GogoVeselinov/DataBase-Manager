using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase_Manager.Forms.DatabaseForms
{
    public partial class DatabaseDetailsForm : Form
    {
        private readonly string _databaseName;
        private DataTable _tables;
        private DataTable _columns;
        private DataTable _rows;

        public DatabaseDetailsForm(string databaseName)
        {
            _databaseName = databaseName;
            InitializeComponent();
        }

        private void DatabaseDetailsForm_Load(object sender, EventArgs e)
        {
            LoadTables();
        }

        private void LoadTables()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    string query = $"USE [{_databaseName}]; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    _tables = new DataTable();
                    adapter.Fill(_tables);
                    dataGridViewTables.DataSource = _tables;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An error occurred while loading tables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewTables_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string tableName = dataGridViewTables.Rows[e.RowIndex].Cells[0].Value.ToString();
                LoadColumns(tableName);
                LoadRows(tableName);
            }
        }

        private void LoadColumns(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    string query = $"USE [{_databaseName}]; SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    _columns = new DataTable();
                    adapter.Fill(_columns);
                    dataGridViewColumns.DataSource = _columns;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An error occurred while loading columns: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRows(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    // Check if the table exists
                    string checkTableQuery = $"USE [{_databaseName}]; SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";
                    SqlCommand checkTableCommand = new SqlCommand(checkTableQuery, connection);
                    int tableCount = (int)checkTableCommand.ExecuteScalar();

                    if (tableCount > 0)
                    {
                        // Table exists, load rows
                        string query = $"USE [{_databaseName}]; SELECT * FROM [{tableName}]";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        _rows = new DataTable();
                        adapter.Fill(_rows);
                        dataGridViewRows.DataSource = _rows;
                    }
                    else
                    {
                        // Table does not exist, handle accordingly
                        MessageBox.Show($"Table '{tableName}' does not exist in the database.", "Table Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dataGridViewRows.DataSource = null;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"An error occurred while loading rows: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            string tableName = Prompt.ShowDialog("Enter table name:", "Create Table");
            if (!string.IsNullOrEmpty(tableName))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        connection.Open();
                        string query = $"USE [{_databaseName}]; CREATE TABLE [{tableName}] (ID INT PRIMARY KEY)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        LoadTables();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An error occurred while creating the table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                string newTableName = Prompt.ShowDialog("Enter new table name:", "Edit Table", tableName);
                if (!string.IsNullOrEmpty(newTableName) && newTableName != tableName)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                        {
                            connection.Open();
                            string query = $"USE [{_databaseName}]; EXEC sp_rename '{tableName}', '{newTableName}'";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.ExecuteNonQuery();
                            LoadTables();
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show($"An error occurred while renaming the table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        connection.Open();
                        string query = $"USE [{_databaseName}]; DELETE FROM [{tableName}]";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        LoadRows(tableName);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An error occurred while deleting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        connection.Open();
                        string query = $"USE [{_databaseName}]; INSERT INTO [{tableName}] (ID) VALUES (NEWID())";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.ExecuteNonQuery();
                        LoadRows(tableName);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An error occurred while adding a row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                string searchTerm = txtSearch.Text;
                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                    {
                        connection.Open();
                        string query = $"USE [{_databaseName}]; SELECT * FROM [{tableName}] WHERE ID LIKE '%{searchTerm}%'";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        DataTable results = new DataTable();
                        adapter.Fill(results);
                        dataGridViewRows.DataSource = results;
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"An error occurred while searching: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DatabaseDetailsForm_Load_1(object sender, EventArgs e)
        {
        }

        private void dataGridViewColumns_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewTables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
