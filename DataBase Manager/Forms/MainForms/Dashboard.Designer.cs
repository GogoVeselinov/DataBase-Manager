namespace DataBase_Manager
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewDatabases;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCreateDB;
        private System.Windows.Forms.Button btnDeleteDB;
        private System.Windows.Forms.Button btnViewInfo;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Label lblDatabaseCount;
        private System.Windows.Forms.Label lblFilter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCreateDB = new System.Windows.Forms.Button();
            this.btnDeleteDB = new System.Windows.Forms.Button();
            this.btnViewInfo = new System.Windows.Forms.Button();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.dataGridViewDatabases = new System.Windows.Forms.DataGridView();
            this.lblDatabaseCount = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatabases)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtSearch.Location = new System.Drawing.Point(103, 21);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(117, 22);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(230, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 25);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "🔍 Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Items.AddRange(new object[] {
            "All",
            "Online",
            "Offline",
            "Size > 100MB"});
            this.cmbFilter.Location = new System.Drawing.Point(370, 20);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(120, 24);
            this.cmbFilter.TabIndex = 2;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.cmbFilter_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(500, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 25);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄 Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCreateDB
            // 
            this.btnCreateDB.Location = new System.Drawing.Point(20, 60);
            this.btnCreateDB.Name = "btnCreateDB";
            this.btnCreateDB.Size = new System.Drawing.Size(120, 30);
            this.btnCreateDB.TabIndex = 4;
            this.btnCreateDB.Text = "➕ Create DB";
            this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
            // 
            // btnDeleteDB
            // 
            this.btnDeleteDB.Location = new System.Drawing.Point(150, 60);
            this.btnDeleteDB.Name = "btnDeleteDB";
            this.btnDeleteDB.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteDB.TabIndex = 5;
            this.btnDeleteDB.Text = "🗑 Delete DB";
            this.btnDeleteDB.Click += new System.EventHandler(this.btnDeleteDB_Click);
            // 
            // btnViewInfo
            // 
            this.btnViewInfo.Location = new System.Drawing.Point(280, 60);
            this.btnViewInfo.Name = "btnViewInfo";
            this.btnViewInfo.Size = new System.Drawing.Size(120, 30);
            this.btnViewInfo.TabIndex = 6;
            this.btnViewInfo.Text = "📜 View Info";
            this.btnViewInfo.Click += new System.EventHandler(this.btnViewInfo_Click);
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(410, 60);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(120, 30);
            this.btnBackup.TabIndex = 7;
            this.btnBackup.Text = "💾 Backup";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(540, 60);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(120, 30);
            this.btnRestore.TabIndex = 8;
            this.btnRestore.Text = "💾 Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // dataGridViewDatabases
            // 
            this.dataGridViewDatabases.AllowUserToAddRows = false;
            this.dataGridViewDatabases.AllowUserToDeleteRows = false;
            this.dataGridViewDatabases.ColumnHeadersHeight = 29;
            this.dataGridViewDatabases.Location = new System.Drawing.Point(20, 100);
            this.dataGridViewDatabases.Name = "dataGridViewDatabases";
            this.dataGridViewDatabases.ReadOnly = true;
            this.dataGridViewDatabases.RowHeadersWidth = 51;
            this.dataGridViewDatabases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDatabases.Size = new System.Drawing.Size(740, 300);
            this.dataGridViewDatabases.TabIndex = 9;
            this.dataGridViewDatabases.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDatabases_CellDoubleClick);
            // 
            // lblDatabaseCount
            // 
            this.lblDatabaseCount.Location = new System.Drawing.Point(20, 410);
            this.lblDatabaseCount.Name = "lblDatabaseCount";
            this.lblDatabaseCount.Size = new System.Drawing.Size(200, 25);
            this.lblDatabaseCount.TabIndex = 10;
            this.lblDatabaseCount.Text = "Total Databases: 0";
            // 
            // lblFilter
            // 
            this.lblFilter.Location = new System.Drawing.Point(320, 20);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(50, 25);
            this.lblFilter.TabIndex = 11;
            this.lblFilter.Text = "Filter:";
            // 
            // Dashboard
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCreateDB);
            this.Controls.Add(this.btnDeleteDB);
            this.Controls.Add(this.btnViewInfo);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.dataGridViewDatabases);
            this.Controls.Add(this.lblDatabaseCount);
            this.Controls.Add(this.lblFilter);
            this.Name = "Dashboard";
            this.Text = "Database Manager";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDatabases)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
