using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase_Manager.Forms.DatabaseForms
{
    public partial class DatabaseDetailsForm : Form
    {
        private string _databaseName;

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
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = $"USE [{_databaseName}]; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable tables = new DataTable();
                adapter.Fill(tables);
                dataGridViewTables.DataSource = tables;
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
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = $"USE [{_databaseName}]; SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable columns = new DataTable();
                adapter.Fill(columns);
                dataGridViewColumns.DataSource = columns;
            }
        }

        private void LoadRows(string tableName)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = $"USE [{_databaseName}]; SELECT * FROM [{tableName}]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable rows = new DataTable();
                adapter.Fill(rows);
                dataGridViewRows.DataSource = rows;
            }
        }

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            string tableName = Prompt.ShowDialog("Enter table name:", "Create Table");
            if (!string.IsNullOrEmpty(tableName))
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
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                string newTableName = Prompt.ShowDialog("Enter new table name:", "Edit Table", tableName);
                if (!string.IsNullOrEmpty(newTableName) && newTableName != tableName)
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
            }
        }

        private void btnDeleteData_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    string query = $"USE [{_databaseName}]; DELETE FROM [{tableName}]";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    LoadRows(tableName);
                }
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                using (SqlConnection connection = new SqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    string query = $"USE [{_databaseName}]; INSERT INTO [{tableName}] (ID) VALUES (NEWID())";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    LoadRows(tableName);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dataGridViewTables.SelectedRows.Count > 0)
            {
                string tableName = dataGridViewTables.SelectedRows[0].Cells[0].Value.ToString();
                string searchTerm = txtSearch.Text;
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
        }
    }
}
