﻿namespace CryptoTax.Forms
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.SummaryDataRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.TransactionGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SummaryDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.CryptocurrencyFilterInput = new System.Windows.Forms.ToolStripComboBox();
            this.TransactionDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.YearSummaryDataGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.SaveAsButton = new System.Windows.Forms.ToolStripButton();
            this.OpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddTransactionButton = new System.Windows.Forms.ToolStripButton();
            this.EditTransactionButton = new System.Windows.Forms.ToolStripButton();
            this.ImportTransactionsButton = new System.Windows.Forms.ToolStripButton();
            this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SummaryLabel = new System.Windows.Forms.Label();
            this.TransactionGridContextMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryDataGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionDataGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearSummaryDataGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.MainTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // SummaryDataRefreshTimer
            // 
            this.SummaryDataRefreshTimer.Interval = 15000;
            // 
            // TransactionGridContextMenu
            // 
            this.TransactionGridContextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.TransactionGridContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditTransaction,
            this.DeleteTransaction});
            this.TransactionGridContextMenu.Name = "TransactionGridContextMenu";
            this.TransactionGridContextMenu.Size = new System.Drawing.Size(202, 52);
            // 
            // EditTransaction
            // 
            this.EditTransaction.Name = "EditTransaction";
            this.EditTransaction.Size = new System.Drawing.Size(201, 24);
            this.EditTransaction.Text = "Edit Transaction";
            // 
            // DeleteTransaction
            // 
            this.DeleteTransaction.Name = "DeleteTransaction";
            this.DeleteTransaction.Size = new System.Drawing.Size(201, 24);
            this.DeleteTransaction.Text = "Delete Transaction";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 59);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1376, 657);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.SummaryDataGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1368, 628);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Portfolio Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SummaryDataGrid
            // 
            this.SummaryDataGrid.AllowUserToAddRows = false;
            this.SummaryDataGrid.AllowUserToDeleteRows = false;
            this.SummaryDataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SummaryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.SummaryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SummaryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SummaryDataGrid.Location = new System.Drawing.Point(3, 3);
            this.SummaryDataGrid.Name = "SummaryDataGrid";
            this.SummaryDataGrid.ReadOnly = true;
            this.SummaryDataGrid.RowTemplate.Height = 24;
            this.SummaryDataGrid.Size = new System.Drawing.Size(1362, 622);
            this.SummaryDataGrid.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.AutoScroll = true;
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1368, 628);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transactions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TransactionDataGrid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1362, 622);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.CryptocurrencyFilterInput});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1368, 28);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Enabled = false;
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(124, 25);
            this.toolStripLabel1.Text = "Filter by Crypto: ";
            // 
            // CryptocurrencyFilterInput
            // 
            this.CryptocurrencyFilterInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CryptocurrencyFilterInput.Name = "CryptocurrencyFilterInput";
            this.CryptocurrencyFilterInput.Size = new System.Drawing.Size(121, 28);
            this.CryptocurrencyFilterInput.Sorted = true;
            this.CryptocurrencyFilterInput.ToolTipText = "Filter by cryptocurrency";
            // 
            // TransactionDataGrid
            // 
            this.TransactionDataGrid.AllowUserToAddRows = false;
            this.TransactionDataGrid.AllowUserToOrderColumns = true;
            this.TransactionDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TransactionDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.TransactionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TransactionDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TransactionDataGrid.Location = new System.Drawing.Point(3, 31);
            this.TransactionDataGrid.Name = "TransactionDataGrid";
            this.TransactionDataGrid.ReadOnly = true;
            this.TransactionDataGrid.RowTemplate.Height = 24;
            this.TransactionDataGrid.Size = new System.Drawing.Size(1362, 588);
            this.TransactionDataGrid.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.YearSummaryDataGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1368, 628);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tax Summary";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // YearSummaryDataGrid
            // 
            this.YearSummaryDataGrid.AllowUserToAddRows = false;
            this.YearSummaryDataGrid.AllowUserToDeleteRows = false;
            this.YearSummaryDataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.YearSummaryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.YearSummaryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.YearSummaryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YearSummaryDataGrid.Location = new System.Drawing.Point(0, 0);
            this.YearSummaryDataGrid.Name = "YearSummaryDataGrid";
            this.YearSummaryDataGrid.ReadOnly = true;
            this.YearSummaryDataGrid.RowTemplate.Height = 24;
            this.YearSummaryDataGrid.Size = new System.Drawing.Size(1368, 628);
            this.YearSummaryDataGrid.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveButton,
            this.SaveAsButton,
            this.OpenFileButton,
            this.toolStripSeparator1,
            this.AddTransactionButton,
            this.EditTransactionButton,
            this.ImportTransactionsButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 29);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1382, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(24, 24);
            this.SaveButton.Text = "&Save";
            this.SaveButton.ToolTipText = "Save (ctrl+s)";
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveAsButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveAsButton.Image")));
            this.SaveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(24, 24);
            this.SaveAsButton.Text = "saveAsButton";
            this.SaveAsButton.ToolTipText = "Save as... (ctrl+shift+s)";
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFileButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileButton.Image")));
            this.OpenFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(24, 24);
            this.OpenFileButton.Text = "&Open";
            this.OpenFileButton.ToolTipText = "Open (ctrl+o)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // AddTransactionButton
            // 
            this.AddTransactionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddTransactionButton.Image = ((System.Drawing.Image)(resources.GetObject("AddTransactionButton.Image")));
            this.AddTransactionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddTransactionButton.Name = "AddTransactionButton";
            this.AddTransactionButton.Size = new System.Drawing.Size(118, 24);
            this.AddTransactionButton.Text = "Add transaction";
            // 
            // EditTransactionButton
            // 
            this.EditTransactionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.EditTransactionButton.Image = ((System.Drawing.Image)(resources.GetObject("EditTransactionButton.Image")));
            this.EditTransactionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EditTransactionButton.Name = "EditTransactionButton";
            this.EditTransactionButton.Size = new System.Drawing.Size(116, 24);
            this.EditTransactionButton.Text = "Edit transaction";
            // 
            // ImportTransactionsButton
            // 
            this.ImportTransactionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ImportTransactionsButton.Image = ((System.Drawing.Image)(resources.GetObject("ImportTransactionsButton.Image")));
            this.ImportTransactionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ImportTransactionsButton.Name = "ImportTransactionsButton";
            this.ImportTransactionsButton.Size = new System.Drawing.Size(137, 24);
            this.ImportTransactionsButton.Text = "Import transactons";
            // 
            // MainTableLayout
            // 
            this.MainTableLayout.AutoSize = true;
            this.MainTableLayout.ColumnCount = 1;
            this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayout.Controls.Add(this.SummaryLabel, 0, 0);
            this.MainTableLayout.Controls.Add(this.toolStrip1, 0, 1);
            this.MainTableLayout.Controls.Add(this.tabControl1, 0, 2);
            this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Margin = new System.Windows.Forms.Padding(2);
            this.MainTableLayout.Name = "MainTableLayout";
            this.MainTableLayout.RowCount = 3;
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayout.Size = new System.Drawing.Size(1381, 719);
            this.MainTableLayout.TabIndex = 0;
            this.MainTableLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // SummaryLabel
            // 
            this.SummaryLabel.AutoSize = true;
            this.SummaryLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SummaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SummaryLabel.Location = new System.Drawing.Point(3, 0);
            this.SummaryLabel.Name = "SummaryLabel";
            this.SummaryLabel.Size = new System.Drawing.Size(263, 29);
            this.SummaryLabel.TabIndex = 8;
            this.SummaryLabel.Text = "Crypto Net Worth - $0";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 719);
            this.Controls.Add(this.MainTableLayout);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainWindow";
            this.Text = "CryptoTax";
            this.TransactionGridContextMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryDataGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionDataGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.YearSummaryDataGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainTableLayout.ResumeLayout(false);
            this.MainTableLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer SummaryDataRefreshTimer;
        private System.Windows.Forms.ContextMenuStrip TransactionGridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem EditTransaction;
        private System.Windows.Forms.ToolStripMenuItem DeleteTransaction;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView YearSummaryDataGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripButton SaveAsButton;
        private System.Windows.Forms.ToolStripButton OpenFileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AddTransactionButton;
        private System.Windows.Forms.ToolStripButton EditTransactionButton;
        private System.Windows.Forms.ToolStripButton ImportTransactionsButton;
        private System.Windows.Forms.TableLayoutPanel MainTableLayout;
        private System.Windows.Forms.DataGridView SummaryDataGrid;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox CryptocurrencyFilterInput;
        private System.Windows.Forms.DataGridView TransactionDataGrid;
        private System.Windows.Forms.Label SummaryLabel;
    }
}

