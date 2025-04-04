﻿using System;

namespace DataBase_Manager.Forms.DatabaseForms
{
    partial class DatabaseDetailsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dataGridViewTables = new System.Windows.Forms.DataGridView();
            this.dataGridViewColumns = new System.Windows.Forms.DataGridView();
            this.dataGridViewRows = new System.Windows.Forms.DataGridView();
            this.lblTables = new System.Windows.Forms.Label();
            this.lblColumns = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.btnEditTable = new System.Windows.Forms.Button();
            this.btnDeleteData = new System.Windows.Forms.Button();
            this.btnAddRow = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRows)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTables
            // 
            this.dataGridViewTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTables.Location = new System.Drawing.Point(20, 50);
            this.dataGridViewTables.Name = "dataGridViewTables";
            this.dataGridViewTables.RowHeadersWidth = 51;
            this.dataGridViewTables.Size = new System.Drawing.Size(591, 251);
            this.dataGridViewTables.TabIndex = 0;
            this.dataGridViewTables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTables_CellClick);
            this.dataGridViewTables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTables_CellContentClick);
            // 
            // dataGridViewColumns
            // 
            this.dataGridViewColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewColumns.Location = new System.Drawing.Point(617, 50);
            this.dataGridViewColumns.Name = "dataGridViewColumns";
            this.dataGridViewColumns.RowHeadersWidth = 51;
            this.dataGridViewColumns.Size = new System.Drawing.Size(633, 251);
            this.dataGridViewColumns.TabIndex = 1;
            this.dataGridViewColumns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewColumns_CellContentClick);
            // 
            // dataGridViewRows
            // 
            this.dataGridViewRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRows.Location = new System.Drawing.Point(20, 353);
            this.dataGridViewRows.Name = "dataGridViewRows";
            this.dataGridViewRows.RowHeadersWidth = 51;
            this.dataGridViewRows.Size = new System.Drawing.Size(1230, 167);
            this.dataGridViewRows.TabIndex = 2;
            // 
            // lblTables
            // 
            this.lblTables.AutoSize = true;
            this.lblTables.Location = new System.Drawing.Point(17, 20);
            this.lblTables.Name = "lblTables";
            this.lblTables.Size = new System.Drawing.Size(53, 16);
            this.lblTables.TabIndex = 3;
            this.lblTables.Text = "Tables:";
            // 
            // lblColumns
            // 
            this.lblColumns.AutoSize = true;
            this.lblColumns.Location = new System.Drawing.Point(614, 20);
            this.lblColumns.Name = "lblColumns";
            this.lblColumns.Size = new System.Drawing.Size(62, 16);
            this.lblColumns.TabIndex = 4;
            this.lblColumns.Text = "Columns:";
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(17, 323);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(44, 16);
            this.lblRows.TabIndex = 5;
            this.lblRows.Text = "Rows:";
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(20, 565);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(120, 40);
            this.btnCreateTable.TabIndex = 6;
            this.btnCreateTable.Text = "Create Table";
            this.btnCreateTable.Click += new System.EventHandler(this.BtnCreateTable_Click);
            // 
            // btnEditTable
            // 
            this.btnEditTable.Location = new System.Drawing.Point(165, 565);
            this.btnEditTable.Name = "btnEditTable";
            this.btnEditTable.Size = new System.Drawing.Size(120, 40);
            this.btnEditTable.TabIndex = 7;
            this.btnEditTable.Text = "Edit Table";
            this.btnEditTable.Click += new System.EventHandler(this.BtnEditTable_Click);
            // 
            // btnDeleteData
            // 
            this.btnDeleteData.Location = new System.Drawing.Point(307, 565);
            this.btnDeleteData.Name = "btnDeleteData";
            this.btnDeleteData.Size = new System.Drawing.Size(120, 40);
            this.btnDeleteData.TabIndex = 8;
            this.btnDeleteData.Text = "Delete Data";
            this.btnDeleteData.Click += new System.EventHandler(this.BtnDeleteData_Click);
            // 
            // btnAddRow
            // 
            this.btnAddRow.Location = new System.Drawing.Point(447, 565);
            this.btnAddRow.Name = "btnAddRow";
            this.btnAddRow.Size = new System.Drawing.Size(120, 40);
            this.btnAddRow.TabIndex = 9;
            this.btnAddRow.Text = "Add Row";
            this.btnAddRow.Click += new System.EventHandler(this.BtnAddRow_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(617, 574);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(400, 22);
            this.txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1023, 565);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 40);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // DatabaseDetailsForm
            // 
            this.ClientSize = new System.Drawing.Size(1262, 639);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAddRow);
            this.Controls.Add(this.btnDeleteData);
            this.Controls.Add(this.btnEditTable);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.lblColumns);
            this.Controls.Add(this.lblTables);
            this.Controls.Add(this.dataGridViewRows);
            this.Controls.Add(this.dataGridViewColumns);
            this.Controls.Add(this.dataGridViewTables);
            this.Name = "DatabaseDetailsForm";
            this.Load += new System.EventHandler(this.DatabaseDetailsForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTables;
        private System.Windows.Forms.DataGridView dataGridViewColumns;
        private System.Windows.Forms.DataGridView dataGridViewRows;
        private System.Windows.Forms.Label lblTables;
        private System.Windows.Forms.Label lblColumns;
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Button btnEditTable;
        private System.Windows.Forms.Button btnDeleteData;
        private System.Windows.Forms.Button btnAddRow;
        private System.Windows.Forms.Button btnSearch;

        private void BtnCreateTable_Click(object sender, EventArgs e)
        {
            // Add event handler code here
        }

        private void BtnEditTable_Click(object sender, EventArgs e)
        {
            // Add event handler code here
        }

        private void BtnDeleteData_Click(object sender, EventArgs e)
        {
            // Add event handler code here
        }

        private void BtnAddRow_Click(object sender, EventArgs e)
        {
            // Add event handler code here
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Add event handler code here
        }
    }
}
