namespace CryptoTax.Forms
{
    partial class CustomCsvImporterDialog
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
            this.ImportButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TransactionDateLabel = new System.Windows.Forms.Label();
            this.TransactionDateInput = new System.Windows.Forms.TextBox();
            this.TransctionTypeInput = new System.Windows.Forms.TextBox();
            this.TransactionTypeLabel = new System.Windows.Forms.Label();
            this.ExchangeInput = new System.Windows.Forms.TextBox();
            this.ExchangeLabel = new System.Windows.Forms.Label();
            this.CryptocurrencyPriceInput = new System.Windows.Forms.TextBox();
            this.CryptocurrencyPriceLabel = new System.Windows.Forms.Label();
            this.CyrptocurrencyAmount = new System.Windows.Forms.Label();
            this.CryptocurrencyAmountInput = new System.Windows.Forms.TextBox();
            this.ExcludeLabel = new System.Windows.Forms.Label();
            this.ExcludeFromPortfolioInput = new System.Windows.Forms.TextBox();
            this.ExcludeFromPortfolioCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(168, 231);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 10;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(249, 231);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(414, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "Provide the CSV header names for the tranaction fields.";
            // 
            // TransactionDateLabel
            // 
            this.TransactionDateLabel.AutoSize = true;
            this.TransactionDateLabel.Location = new System.Drawing.Point(12, 48);
            this.TransactionDateLabel.Name = "TransactionDateLabel";
            this.TransactionDateLabel.Size = new System.Drawing.Size(42, 17);
            this.TransactionDateLabel.TabIndex = 13;
            this.TransactionDateLabel.Text = "Date:";
            // 
            // TransactionDateInput
            // 
            this.TransactionDateInput.Location = new System.Drawing.Point(177, 45);
            this.TransactionDateInput.Name = "TransactionDateInput";
            this.TransactionDateInput.Size = new System.Drawing.Size(171, 22);
            this.TransactionDateInput.TabIndex = 14;
            this.TransactionDateInput.Text = "Closed";
            // 
            // TransctionTypeInput
            // 
            this.TransctionTypeInput.Location = new System.Drawing.Point(177, 73);
            this.TransctionTypeInput.Name = "TransctionTypeInput";
            this.TransctionTypeInput.Size = new System.Drawing.Size(171, 22);
            this.TransctionTypeInput.TabIndex = 16;
            this.TransctionTypeInput.Text = "Type";
            // 
            // TransactionTypeLabel
            // 
            this.TransactionTypeLabel.AutoSize = true;
            this.TransactionTypeLabel.Location = new System.Drawing.Point(12, 76);
            this.TransactionTypeLabel.Name = "TransactionTypeLabel";
            this.TransactionTypeLabel.Size = new System.Drawing.Size(118, 17);
            this.TransactionTypeLabel.TabIndex = 15;
            this.TransactionTypeLabel.Text = "Transaction type:";
            // 
            // ExchangeInput
            // 
            this.ExchangeInput.Location = new System.Drawing.Point(177, 101);
            this.ExchangeInput.Name = "ExchangeInput";
            this.ExchangeInput.Size = new System.Drawing.Size(171, 22);
            this.ExchangeInput.TabIndex = 18;
            this.ExchangeInput.Text = "Exchange";
            // 
            // ExchangeLabel
            // 
            this.ExchangeLabel.AutoSize = true;
            this.ExchangeLabel.Location = new System.Drawing.Point(13, 104);
            this.ExchangeLabel.Name = "ExchangeLabel";
            this.ExchangeLabel.Size = new System.Drawing.Size(74, 17);
            this.ExchangeLabel.TabIndex = 17;
            this.ExchangeLabel.Text = "Exchange:";
            // 
            // CryptocurrencyPriceInput
            // 
            this.CryptocurrencyPriceInput.Location = new System.Drawing.Point(177, 129);
            this.CryptocurrencyPriceInput.Name = "CryptocurrencyPriceInput";
            this.CryptocurrencyPriceInput.Size = new System.Drawing.Size(171, 22);
            this.CryptocurrencyPriceInput.TabIndex = 20;
            this.CryptocurrencyPriceInput.Text = "Limit";
            // 
            // CryptocurrencyPriceLabel
            // 
            this.CryptocurrencyPriceLabel.AutoSize = true;
            this.CryptocurrencyPriceLabel.Location = new System.Drawing.Point(12, 132);
            this.CryptocurrencyPriceLabel.Name = "CryptocurrencyPriceLabel";
            this.CryptocurrencyPriceLabel.Size = new System.Drawing.Size(143, 17);
            this.CryptocurrencyPriceLabel.TabIndex = 21;
            this.CryptocurrencyPriceLabel.Text = "Cryptocurrency price:";
            // 
            // CyrptocurrencyAmount
            // 
            this.CyrptocurrencyAmount.AutoSize = true;
            this.CyrptocurrencyAmount.Location = new System.Drawing.Point(12, 160);
            this.CyrptocurrencyAmount.Name = "CyrptocurrencyAmount";
            this.CyrptocurrencyAmount.Size = new System.Drawing.Size(159, 17);
            this.CyrptocurrencyAmount.TabIndex = 23;
            this.CyrptocurrencyAmount.Text = "Cryptocurrency amount:";
            // 
            // CryptocurrencyAmountInput
            // 
            this.CryptocurrencyAmountInput.Location = new System.Drawing.Point(177, 157);
            this.CryptocurrencyAmountInput.Name = "CryptocurrencyAmountInput";
            this.CryptocurrencyAmountInput.Size = new System.Drawing.Size(171, 22);
            this.CryptocurrencyAmountInput.TabIndex = 22;
            this.CryptocurrencyAmountInput.Text = "Quantity";
            // 
            // ExcludeLabel
            // 
            this.ExcludeLabel.AutoSize = true;
            this.ExcludeLabel.Location = new System.Drawing.Point(12, 189);
            this.ExcludeLabel.Name = "ExcludeLabel";
            this.ExcludeLabel.Size = new System.Drawing.Size(148, 17);
            this.ExcludeLabel.TabIndex = 25;
            this.ExcludeLabel.Text = "Exclude from portfolio:";
            // 
            // ExcludeFromPortfolioInput
            // 
            this.ExcludeFromPortfolioInput.Enabled = false;
            this.ExcludeFromPortfolioInput.Location = new System.Drawing.Point(201, 185);
            this.ExcludeFromPortfolioInput.Name = "ExcludeFromPortfolioInput";
            this.ExcludeFromPortfolioInput.Size = new System.Drawing.Size(147, 22);
            this.ExcludeFromPortfolioInput.TabIndex = 24;
            this.ExcludeFromPortfolioInput.Text = "Non Alonzo Trx";
            // 
            // ExcludeFromPortfolioCheckbox
            // 
            this.ExcludeFromPortfolioCheckbox.AutoSize = true;
            this.ExcludeFromPortfolioCheckbox.Location = new System.Drawing.Point(177, 188);
            this.ExcludeFromPortfolioCheckbox.Name = "ExcludeFromPortfolioCheckbox";
            this.ExcludeFromPortfolioCheckbox.Size = new System.Drawing.Size(18, 17);
            this.ExcludeFromPortfolioCheckbox.TabIndex = 26;
            this.ExcludeFromPortfolioCheckbox.UseVisualStyleBackColor = true;
            // 
            // CustomCsvImporterDialog
            // 
            this.AcceptButton = this.ImportButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 266);
            this.Controls.Add(this.ExcludeFromPortfolioCheckbox);
            this.Controls.Add(this.ExcludeLabel);
            this.Controls.Add(this.ExcludeFromPortfolioInput);
            this.Controls.Add(this.CyrptocurrencyAmount);
            this.Controls.Add(this.CryptocurrencyAmountInput);
            this.Controls.Add(this.CryptocurrencyPriceLabel);
            this.Controls.Add(this.CryptocurrencyPriceInput);
            this.Controls.Add(this.ExchangeInput);
            this.Controls.Add(this.ExchangeLabel);
            this.Controls.Add(this.TransctionTypeInput);
            this.Controls.Add(this.TransactionTypeLabel);
            this.Controls.Add(this.TransactionDateInput);
            this.Controls.Add(this.TransactionDateLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ImportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomCsvImporterDialog";
            this.ShowIcon = false;
            this.Text = "Custom CSV Import Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TransactionDateLabel;
        private System.Windows.Forms.TextBox TransactionDateInput;
        private System.Windows.Forms.TextBox TransctionTypeInput;
        private System.Windows.Forms.Label TransactionTypeLabel;
        private System.Windows.Forms.TextBox ExchangeInput;
        private System.Windows.Forms.Label ExchangeLabel;
        private System.Windows.Forms.TextBox CryptocurrencyPriceInput;
        private System.Windows.Forms.Label CryptocurrencyPriceLabel;
        private System.Windows.Forms.Label CyrptocurrencyAmount;
        private System.Windows.Forms.TextBox CryptocurrencyAmountInput;
        private System.Windows.Forms.Label ExcludeLabel;
        private System.Windows.Forms.TextBox ExcludeFromPortfolioInput;
        private System.Windows.Forms.CheckBox ExcludeFromPortfolioCheckbox;
    }
}