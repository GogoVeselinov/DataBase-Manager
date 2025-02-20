using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DataBase_Manager.Forms.DatabaseForms
{
    public partial class CreateDatabaseForm : Form
    {
        public CreateDatabaseForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(150, 30);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(200, 20);
            this.txtDatabaseName.TabIndex = 0;
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(150, 70);
            this.numSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(200, 20);
            this.numSize.TabIndex = 1;
            this.numSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(150, 110);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(275, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.AutoSize = true;
            this.lblDatabaseName.Location = new System.Drawing.Point(30, 30);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(87, 13);
            this.lblDatabaseName.TabIndex = 4;
            this.lblDatabaseName.Text = "Database Name:";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(30, 70);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(57, 13);
            this.lblSize.TabIndex = 5;
            this.lblSize.Text = "Size (MB):";
            // 
            // CreateDatabaseForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDatabaseName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.numSize);
            this.Controls.Add(this.txtDatabaseName);
            this.Name = "CreateDatabaseForm";
            this.Text = "Create Database";
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.NumericUpDown numSize;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDatabaseName;
        private System.Windows.Forms.Label lblSize;
    }
}