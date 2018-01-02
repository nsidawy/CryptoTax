namespace CryptoTax
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.TransactionDataGrid = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.OpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.SaveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddTransactionButton = new System.Windows.Forms.ToolStripButton();
            this.EditTransactionButton = new System.Windows.Forms.ToolStripButton();
            this.ImportTransactionsButton = new System.Windows.Forms.ToolStripButton();
            this.SummaryLabel = new System.Windows.Forms.Label();
            this.SummaryInfoTablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.SummaryDataGrid = new System.Windows.Forms.DataGridView();
            this.SummaryDataRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.SaveAsButton = new System.Windows.Forms.ToolStripButton();
            this.YearSummaryDataGrid = new System.Windows.Forms.DataGridView();
            this.MainTableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionDataGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SummaryInfoTablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SummaryDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearSummaryDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTableLayout
            // 
            this.MainTableLayout.AutoSize = true;
            this.MainTableLayout.ColumnCount = 1;
            this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MainTableLayout.Controls.Add(this.TransactionDataGrid, 0, 4);
            this.MainTableLayout.Controls.Add(this.SummaryDataGrid, 0, 2);
            this.MainTableLayout.Controls.Add(this.toolStrip1, 0, 0);
            this.MainTableLayout.Controls.Add(this.SummaryLabel, 0, 1);
            this.MainTableLayout.Controls.Add(this.SummaryInfoTablePanel, 0, 3);
            this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.MainTableLayout.Margin = new System.Windows.Forms.Padding(2);
            this.MainTableLayout.Name = "MainTableLayout";
            this.MainTableLayout.RowCount = 5;
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MainTableLayout.Size = new System.Drawing.Size(1382, 641);
            this.MainTableLayout.TabIndex = 0;
            this.MainTableLayout.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // TransactionDataGrid
            // 
            this.TransactionDataGrid.AllowUserToAddRows = false;
            this.TransactionDataGrid.AllowUserToOrderColumns = true;
            this.TransactionDataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TransactionDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TransactionDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TransactionDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TransactionDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TransactionDataGrid.Location = new System.Drawing.Point(3, 459);
            this.TransactionDataGrid.Name = "TransactionDataGrid";
            this.TransactionDataGrid.RowTemplate.Height = 24;
            this.TransactionDataGrid.Size = new System.Drawing.Size(1376, 379);
            this.TransactionDataGrid.TabIndex = 1;
            this.TransactionDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            // OpenFileButton
            // 
            this.OpenFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFileButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileButton.Image")));
            this.OpenFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(24, 24);
            this.OpenFileButton.Text = "&Open";
            this.OpenFileButton.ToolTipText = "Open (CTRL+o)";
            // 
            // SaveButton
            // 
            this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
            this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(24, 24);
            this.SaveButton.Text = "&Save";
            this.SaveButton.ToolTipText = "Save (CTRL+s)";
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
            // SummaryLabel
            // 
            this.SummaryLabel.AutoSize = true;
            this.SummaryLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SummaryLabel.Location = new System.Drawing.Point(3, 27);
            this.SummaryLabel.Name = "SummaryLabel";
            this.SummaryLabel.Size = new System.Drawing.Size(227, 29);
            this.SummaryLabel.TabIndex = 3;
            this.SummaryLabel.Text = "Portfolio Summary";
            // 
            // SummaryInfoTablePanel
            // 
            this.SummaryInfoTablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SummaryInfoTablePanel.ColumnCount = 1;
            this.SummaryInfoTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.SummaryInfoTablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.SummaryInfoTablePanel.Controls.Add(this.YearSummaryDataGrid, 0, 0);
            this.SummaryInfoTablePanel.Location = new System.Drawing.Point(3, 259);
            this.SummaryInfoTablePanel.Name = "SummaryInfoTablePanel";
            this.SummaryInfoTablePanel.RowCount = 1;
            this.SummaryInfoTablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.SummaryInfoTablePanel.Size = new System.Drawing.Size(1376, 194);
            this.SummaryInfoTablePanel.TabIndex = 5;
            // 
            // SummaryDataGrid
            // 
            this.SummaryDataGrid.AllowUserToAddRows = false;
            this.SummaryDataGrid.AllowUserToDeleteRows = false;
            this.SummaryDataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SummaryDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SummaryDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SummaryDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SummaryDataGrid.Location = new System.Drawing.Point(3, 59);
            this.SummaryDataGrid.Name = "SummaryDataGrid";
            this.SummaryDataGrid.ReadOnly = true;
            this.SummaryDataGrid.RowTemplate.Height = 24;
            this.SummaryDataGrid.Size = new System.Drawing.Size(1376, 194);
            this.SummaryDataGrid.TabIndex = 4;
            // 
            // SummaryDataRefreshTimer
            // 
            this.SummaryDataRefreshTimer.Interval = 15000;
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveAsButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveAsButton.Image")));
            this.SaveAsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(24, 24);
            this.SaveAsButton.Text = "saveAsButton";
            this.SaveAsButton.ToolTipText = "Save as... (CTRL+SHIFT+s)";
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
            this.YearSummaryDataGrid.Location = new System.Drawing.Point(3, 3);
            this.YearSummaryDataGrid.Name = "YearSummaryDataGrid";
            this.YearSummaryDataGrid.ReadOnly = true;
            this.YearSummaryDataGrid.RowTemplate.Height = 24;
            this.YearSummaryDataGrid.Size = new System.Drawing.Size(1370, 194);
            this.YearSummaryDataGrid.TabIndex = 5;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 641);
            this.Controls.Add(this.MainTableLayout);
            this.MinimumSize = new System.Drawing.Size(1200, 600);
            this.Name = "MainWindow";
            this.Text = "CryptoTax";
            this.MainTableLayout.ResumeLayout(false);
            this.MainTableLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TransactionDataGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.SummaryInfoTablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SummaryDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearSummaryDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainTableLayout;
        private System.Windows.Forms.DataGridView TransactionDataGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton OpenFileButton;
        private System.Windows.Forms.ToolStripButton SaveButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton AddTransactionButton;
        private System.Windows.Forms.Label SummaryLabel;
        private System.Windows.Forms.DataGridView SummaryDataGrid;
        private System.Windows.Forms.ToolStripButton EditTransactionButton;
        private System.Windows.Forms.Timer SummaryDataRefreshTimer;
        private System.Windows.Forms.ToolStripButton ImportTransactionsButton;
        private System.Windows.Forms.TableLayoutPanel SummaryInfoTablePanel;
        private System.Windows.Forms.ToolStripButton SaveAsButton;
        private System.Windows.Forms.DataGridView YearSummaryDataGrid;
    }
}

