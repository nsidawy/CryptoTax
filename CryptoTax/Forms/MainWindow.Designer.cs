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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SummaryDataRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.TransactionGridContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.EditTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteTransaction = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.SaveAsButton = new System.Windows.Forms.ToolStripButton();
            this.OpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddTransactionButton = new System.Windows.Forms.ToolStripButton();
            this.EditTransactionButton = new System.Windows.Forms.ToolStripButton();
            this.ImportTransactionsButton = new System.Windows.Forms.ToolStripButton();
            this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.MainWindowTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PortfolioTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.SummaryLabel = new System.Windows.Forms.Label();
            this.PortfolioSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SummaryDataGrid = new System.Windows.Forms.DataGridView();
            this.TransactionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.CryptoFilterInput = new System.Windows.Forms.ToolStripComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.YearSummaryDataGrid = new System.Windows.Forms.DataGridView();
            this.TransactionDataGrid = new System.Windows.Forms.DataGridView();
            this.TransactionGridContextMenu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.MainTableLayout.SuspendLayout();
            this.MainWindowTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.PortfolioTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortfolioSplitContainer)).BeginInit();
            this.PortfolioSplitContainer.Panel1.SuspendLayout();
            this.PortfolioSplitContainer.Panel2.SuspendLayout();
            this.PortfolioSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryDataGrid)).BeginInit();
            this.TransactionTableLayoutPanel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearSummaryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionDataGrid)).BeginInit();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
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
            this.MainTableLayout.Controls.Add(this.toolStrip1, 0, 0);
            this.MainTableLayout.Controls.Add(this.MainWindowTabs, 0, 1);
            this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Margin = new System.Windows.Forms.Padding(2);
            this.MainTableLayout.Name = "MainTableLayout";
            this.MainTableLayout.RowCount = 2;
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainTableLayout.Size = new System.Drawing.Size(1381, 719);
            this.MainTableLayout.TabIndex = 0;
            this.MainTableLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // MainWindowTabs
            // 
            this.MainWindowTabs.Controls.Add(this.tabPage1);
            this.MainWindowTabs.Controls.Add(this.tabPage3);
            this.MainWindowTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainWindowTabs.Location = new System.Drawing.Point(3, 30);
            this.MainWindowTabs.Name = "MainWindowTabs";
            this.MainWindowTabs.SelectedIndex = 0;
            this.MainWindowTabs.Size = new System.Drawing.Size(1376, 686);
            this.MainWindowTabs.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.PortfolioTableLayout);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1368, 657);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "My Portfolio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // PortfolioTableLayout
            // 
            this.PortfolioTableLayout.ColumnCount = 1;
            this.PortfolioTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PortfolioTableLayout.Controls.Add(this.SummaryLabel, 0, 0);
            this.PortfolioTableLayout.Controls.Add(this.PortfolioSplitContainer, 0, 1);
            this.PortfolioTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PortfolioTableLayout.Location = new System.Drawing.Point(3, 3);
            this.PortfolioTableLayout.Name = "PortfolioTableLayout";
            this.PortfolioTableLayout.RowCount = 2;
            this.PortfolioTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.PortfolioTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PortfolioTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PortfolioTableLayout.Size = new System.Drawing.Size(1362, 651);
            this.PortfolioTableLayout.TabIndex = 0;
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
            // PortfolioSplitContainer
            // 
            this.PortfolioSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PortfolioSplitContainer.Location = new System.Drawing.Point(3, 32);
            this.PortfolioSplitContainer.Name = "PortfolioSplitContainer";
            this.PortfolioSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PortfolioSplitContainer.Panel1
            // 
            this.PortfolioSplitContainer.Panel1.Controls.Add(this.SummaryDataGrid);
            // 
            // PortfolioSplitContainer.Panel2
            // 
            this.PortfolioSplitContainer.Panel2.Controls.Add(this.TransactionTableLayoutPanel);
            this.PortfolioSplitContainer.Size = new System.Drawing.Size(1356, 616);
            this.PortfolioSplitContainer.SplitterDistance = 250;
            this.PortfolioSplitContainer.SplitterWidth = 10;
            this.PortfolioSplitContainer.TabIndex = 1;
            // 
            // SummaryDataGrid
            // 
            this.SummaryDataGrid.AllowUserToAddRows = false;
            this.SummaryDataGrid.AllowUserToDeleteRows = false;
            this.SummaryDataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SummaryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.SummaryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SummaryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SummaryDataGrid.Location = new System.Drawing.Point(0, 0);
            this.SummaryDataGrid.Name = "SummaryDataGrid";
            this.SummaryDataGrid.ReadOnly = true;
            this.SummaryDataGrid.RowTemplate.Height = 24;
            this.SummaryDataGrid.Size = new System.Drawing.Size(1356, 250);
            this.SummaryDataGrid.TabIndex = 10;
            // 
            // TransactionTableLayoutPanel
            // 
            this.TransactionTableLayoutPanel.ColumnCount = 1;
            this.TransactionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TransactionTableLayoutPanel.Controls.Add(this.toolStrip2, 0, 0);
            this.TransactionTableLayoutPanel.Controls.Add(this.TransactionDataGrid, 0, 1);
            this.TransactionTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.TransactionTableLayoutPanel.Name = "TransactionTableLayoutPanel";
            this.TransactionTableLayoutPanel.RowCount = 2;
            this.TransactionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TransactionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.TransactionTableLayoutPanel.Size = new System.Drawing.Size(1356, 356);
            this.TransactionTableLayoutPanel.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.CryptoFilterInput});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1356, 28);
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
            // CryptoFilterInput
            // 
            this.CryptoFilterInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CryptoFilterInput.Name = "CryptoFilterInput";
            this.CryptoFilterInput.Size = new System.Drawing.Size(121, 28);
            this.CryptoFilterInput.Sorted = true;
            this.CryptoFilterInput.ToolTipText = "Filter by crypto";
            // 
            // tabPage3
            // 
            this.tabPage3.AutoScroll = true;
            this.tabPage3.Controls.Add(this.YearSummaryDataGrid);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1368, 657);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Tax Summary";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // YearSummaryDataGrid
            // 
            this.YearSummaryDataGrid.AllowUserToAddRows = false;
            this.YearSummaryDataGrid.AllowUserToDeleteRows = false;
            this.YearSummaryDataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.YearSummaryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.YearSummaryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.YearSummaryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YearSummaryDataGrid.Location = new System.Drawing.Point(0, 0);
            this.YearSummaryDataGrid.Name = "YearSummaryDataGrid";
            this.YearSummaryDataGrid.ReadOnly = true;
            this.YearSummaryDataGrid.RowTemplate.Height = 24;
            this.YearSummaryDataGrid.Size = new System.Drawing.Size(1368, 657);
            this.YearSummaryDataGrid.TabIndex = 6;
            // 
            // TransactionDataGrid
            // 
            this.TransactionDataGrid.AllowUserToAddRows = false;
            this.TransactionDataGrid.AllowUserToOrderColumns = true;
            this.TransactionDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TransactionDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.TransactionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TransactionDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TransactionDataGrid.Location = new System.Drawing.Point(3, 31);
            this.TransactionDataGrid.Name = "TransactionDataGrid";
            this.TransactionDataGrid.ReadOnly = true;
            this.TransactionDataGrid.RowTemplate.Height = 24;
            this.TransactionDataGrid.Size = new System.Drawing.Size(1350, 322);
            this.TransactionDataGrid.TabIndex = 10;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1381, 719);
            this.Controls.Add(this.MainTableLayout);
            this.MinimumSize = new System.Drawing.Size(1399, 766);
            this.Name = "MainWindow";
            this.Text = "CryptoTax";
            this.TransactionGridContextMenu.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.MainTableLayout.ResumeLayout(false);
            this.MainTableLayout.PerformLayout();
            this.MainWindowTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.PortfolioTableLayout.ResumeLayout(false);
            this.PortfolioTableLayout.PerformLayout();
            this.PortfolioSplitContainer.Panel1.ResumeLayout(false);
            this.PortfolioSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PortfolioSplitContainer)).EndInit();
            this.PortfolioSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryDataGrid)).EndInit();
            this.TransactionTableLayoutPanel.ResumeLayout(false);
            this.TransactionTableLayoutPanel.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.YearSummaryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer SummaryDataRefreshTimer;
        private System.Windows.Forms.ContextMenuStrip TransactionGridContextMenu;
        private System.Windows.Forms.ToolStripMenuItem EditTransaction;
        private System.Windows.Forms.ToolStripMenuItem DeleteTransaction;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripButton SaveAsButton;
        private System.Windows.Forms.ToolStripButton OpenFileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AddTransactionButton;
        private System.Windows.Forms.ToolStripButton EditTransactionButton;
        private System.Windows.Forms.ToolStripButton ImportTransactionsButton;
        private System.Windows.Forms.TableLayoutPanel MainTableLayout;
        private System.Windows.Forms.TabControl MainWindowTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel PortfolioTableLayout;
        private System.Windows.Forms.Label SummaryLabel;
        private System.Windows.Forms.SplitContainer PortfolioSplitContainer;
        private System.Windows.Forms.DataGridView SummaryDataGrid;
        private System.Windows.Forms.TableLayoutPanel TransactionTableLayoutPanel;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox CryptoFilterInput;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView YearSummaryDataGrid;
        private System.Windows.Forms.DataGridView TransactionDataGrid;
    }
}

