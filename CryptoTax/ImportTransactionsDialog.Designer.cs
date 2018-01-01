﻿namespace CryptoTax
{
    partial class ImportTransactionsDialog
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
            this.CoinbaseCsvRadioButton = new System.Windows.Forms.RadioButton();
            this.GdaxFillCsvRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.FilenameInput = new System.Windows.Forms.TextBox();
            this.FileBrowseButton = new System.Windows.Forms.Button();
            this.BitrexOrderCsvRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(192, 181);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 25);
            this.ImportButton.TabIndex = 10;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(273, 181);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 25);
            this.CancelButton.TabIndex = 11;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(374, 17);
            this.label1.TabIndex = 12;
            this.label1.Text = "What is the source of the transactions you want to import?";
            // 
            // CoinbaseCsvRadioButton
            // 
            this.CoinbaseCsvRadioButton.AutoSize = true;
            this.CoinbaseCsvRadioButton.Checked = true;
            this.CoinbaseCsvRadioButton.Location = new System.Drawing.Point(28, 43);
            this.CoinbaseCsvRadioButton.Name = "CoinbaseCsvRadioButton";
            this.CoinbaseCsvRadioButton.Size = new System.Drawing.Size(239, 21);
            this.CoinbaseCsvRadioButton.TabIndex = 13;
            this.CoinbaseCsvRadioButton.TabStop = true;
            this.CoinbaseCsvRadioButton.Text = "Coinbase transaction history CSV";
            this.CoinbaseCsvRadioButton.UseVisualStyleBackColor = true;
            // 
            // GdaxFillCsvRadioButton
            // 
            this.GdaxFillCsvRadioButton.AutoSize = true;
            this.GdaxFillCsvRadioButton.Location = new System.Drawing.Point(28, 70);
            this.GdaxFillCsvRadioButton.Name = "GdaxFillCsvRadioButton";
            this.GdaxFillCsvRadioButton.Size = new System.Drawing.Size(162, 21);
            this.GdaxFillCsvRadioButton.TabIndex = 14;
            this.GdaxFillCsvRadioButton.Text = "GDAX fill history CSV";
            this.GdaxFillCsvRadioButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "File name:";
            // 
            // FilenameInput
            // 
            this.FilenameInput.Location = new System.Drawing.Point(107, 132);
            this.FilenameInput.Name = "FilenameInput";
            this.FilenameInput.Size = new System.Drawing.Size(229, 22);
            this.FilenameInput.TabIndex = 16;
            // 
            // FileBrowseButton
            // 
            this.FileBrowseButton.Location = new System.Drawing.Point(342, 132);
            this.FileBrowseButton.Name = "FileBrowseButton";
            this.FileBrowseButton.Size = new System.Drawing.Size(75, 24);
            this.FileBrowseButton.TabIndex = 17;
            this.FileBrowseButton.Text = "Browse...";
            this.FileBrowseButton.UseVisualStyleBackColor = true;
            // 
            // BitrexOrderCsvRadioButton
            // 
            this.BitrexOrderCsvRadioButton.AutoSize = true;
            this.BitrexOrderCsvRadioButton.Location = new System.Drawing.Point(28, 97);
            this.BitrexOrderCsvRadioButton.Name = "BitrexOrderCsvRadioButton";
            this.BitrexOrderCsvRadioButton.Size = new System.Drawing.Size(133, 21);
            this.BitrexOrderCsvRadioButton.TabIndex = 18;
            this.BitrexOrderCsvRadioButton.Text = "Bitrex order CSV";
            this.BitrexOrderCsvRadioButton.UseVisualStyleBackColor = true;
            // 
            // ImportTransactionsDialog
            // 
            this.AcceptButton = this.ImportButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 218);
            this.Controls.Add(this.BitrexOrderCsvRadioButton);
            this.Controls.Add(this.FileBrowseButton);
            this.Controls.Add(this.FilenameInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GdaxFillCsvRadioButton);
            this.Controls.Add(this.CoinbaseCsvRadioButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ImportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportTransactionsDialog";
            this.ShowIcon = false;
            this.Text = "Import transactions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton CoinbaseCsvRadioButton;
        private System.Windows.Forms.RadioButton GdaxFillCsvRadioButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FilenameInput;
        private System.Windows.Forms.Button FileBrowseButton;
        private System.Windows.Forms.RadioButton BitrexOrderCsvRadioButton;
    }
}