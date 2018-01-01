namespace CryptoTax
{
    partial class AddTransactionDialog
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
            this.TransactionDateInput = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.TransactionTypeInput = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CryptocurrencyInput = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UsdAmountInput = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.CryptoAmountInput = new System.Windows.Forms.NumericUpDown();
            this.AddButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UsdAmountInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CryptoAmountInput)).BeginInit();
            this.SuspendLayout();
            // 
            // TransactionDateInput
            // 
            this.TransactionDateInput.CustomFormat = "MM/dd/yyyy hh:mm:ss tt";
            this.TransactionDateInput.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TransactionDateInput.Location = new System.Drawing.Point(146, 12);
            this.TransactionDateInput.Name = "TransactionDateInput";
            this.TransactionDateInput.Size = new System.Drawing.Size(326, 22);
            this.TransactionDateInput.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Close Date:";
            // 
            // TransactionTypeInput
            // 
            this.TransactionTypeInput.FormattingEnabled = true;
            this.TransactionTypeInput.Location = new System.Drawing.Point(146, 41);
            this.TransactionTypeInput.Name = "TransactionTypeInput";
            this.TransactionTypeInput.Size = new System.Drawing.Size(121, 24);
            this.TransactionTypeInput.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cryptocurrency:";
            // 
            // CryptocurrencyInput
            // 
            this.CryptocurrencyInput.FormattingEnabled = true;
            this.CryptocurrencyInput.Location = new System.Drawing.Point(146, 72);
            this.CryptocurrencyInput.Name = "CryptocurrencyInput";
            this.CryptocurrencyInput.Size = new System.Drawing.Size(121, 24);
            this.CryptocurrencyInput.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "USD Amount:";
            // 
            // UsdAmountInput
            // 
            this.UsdAmountInput.DecimalPlaces = 10;
            this.UsdAmountInput.Location = new System.Drawing.Point(146, 103);
            this.UsdAmountInput.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.UsdAmountInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            this.UsdAmountInput.Name = "UsdAmountInput";
            this.UsdAmountInput.Size = new System.Drawing.Size(120, 22);
            this.UsdAmountInput.TabIndex = 7;
            this.UsdAmountInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Crypto Amount:";
            // 
            // CryptoAmountInput
            // 
            this.CryptoAmountInput.DecimalPlaces = 10;
            this.CryptoAmountInput.Location = new System.Drawing.Point(146, 132);
            this.CryptoAmountInput.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.CryptoAmountInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            this.CryptoAmountInput.Name = "CryptoAmountInput";
            this.CryptoAmountInput.Size = new System.Drawing.Size(120, 22);
            this.CryptoAmountInput.TabIndex = 9;
            this.CryptoAmountInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            655360});
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(192, 181);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 10;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(273, 181);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Buy or Sell:";
            // 
            // AddTransactionDialog
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 218);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.CryptoAmountInput);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.UsdAmountInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CryptocurrencyInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TransactionTypeInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TransactionDateInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTransactionDialog";
            this.ShowIcon = false;
            this.Text = "Add Transaction";
            ((System.ComponentModel.ISupportInitialize)(this.UsdAmountInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CryptoAmountInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker TransactionDateInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TransactionTypeInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CryptocurrencyInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown UsdAmountInput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CryptoAmountInput;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label2;
    }
}