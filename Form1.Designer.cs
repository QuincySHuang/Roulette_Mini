namespace Roulette_Huang
{

    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbBetType;
        private System.Windows.Forms.TextBox txtBetAmount;
        private System.Windows.Forms.Button btnPlaceBet;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.ListBox lstResults;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            cmbBetType = new ComboBox();
            txtBetAmount = new TextBox();
            btnPlaceBet = new Button();
            lblBalance = new Label();
            lstResults = new ListBox();
            cmbBetValue = new ComboBox();
            SuspendLayout();
            // 
            // cmbBetType
            // 
            cmbBetType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBetType.FormattingEnabled = true;
            cmbBetType.Location = new Point(30, 30);
            cmbBetType.Name = "cmbBetType";
            cmbBetType.Size = new Size(150, 23);
            cmbBetType.TabIndex = 0;
            // 
            // txtBetAmount
            // 
            txtBetAmount.Location = new Point(320, 30);
            txtBetAmount.Name = "txtBetAmount";
            txtBetAmount.Size = new Size(100, 23);
            txtBetAmount.TabIndex = 2;
            // 
            // btnPlaceBet
            // 
            btnPlaceBet.Location = new Point(440, 25);
            btnPlaceBet.Name = "btnPlaceBet";
            btnPlaceBet.Size = new Size(100, 30);
            btnPlaceBet.TabIndex = 3;
            btnPlaceBet.Text = "Place Bet";
            btnPlaceBet.UseVisualStyleBackColor = true;
            btnPlaceBet.Click += btnPlaceBet_Click;
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Location = new Point(30, 70);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(51, 15);
            lblBalance.TabIndex = 4;
            lblBalance.Text = "Balance:";
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.ItemHeight = 15;
            lstResults.Location = new Point(30, 100);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(510, 169);
            lstResults.TabIndex = 5;
            // 
            // cmbBetValue
            // 
            cmbBetValue.FormattingEnabled = true;
            cmbBetValue.Location = new Point(186, 32);
            cmbBetValue.Name = "cmbBetValue";
            cmbBetValue.Size = new Size(121, 23);
            cmbBetValue.TabIndex = 6;
            // 
            // Form1
            // 
            ClientSize = new Size(791, 572);
            Controls.Add(cmbBetValue);
            Controls.Add(cmbBetType);
            Controls.Add(txtBetAmount);
            Controls.Add(btnPlaceBet);
            Controls.Add(lblBalance);
            Controls.Add(lstResults);
            Name = "Form1";
            Text = "Roulette Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbBetValue;
    }
}
