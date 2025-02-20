using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase_Manager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadDatabases()
        {
            string connectionString = "Server=LAPTOP-GEORGI-X;Database=DataBase_Manager;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                        WHERE database_id > 4"; 
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
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
    }
}
