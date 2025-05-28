using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Roulette_Huang
{
    public partial class Form1 : Form
    {
        private Game game;
        private BasePlayer player;

        public Form1()
        {
            InitializeComponent();

            game = new Game();
            player = new Player("Player", 1000);

            cmbBetType.DataSource = Enum.GetValues(typeof(BetType));
            cmbBetType.SelectedIndexChanged += CmbBetType_SelectedIndexChanged;
            UpdateBetValues();

            lblBalance.Text = $"Balance: ${player.Balance}";

            CreateRouletteTable(); 
        }

        private void CmbBetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBetValues();
        }

        private void UpdateBetValues()
        {
            cmbBetValue.Items.Clear();
            var selectedType = (BetType)cmbBetType.SelectedItem;

            switch (selectedType)
            {
                case BetType.Number:
                    cmbBetValue.Items.AddRange(Enumerable.Range(0, 37).Select(n => n.ToString()).ToArray());
                    break;
                case BetType.Color:
                    cmbBetValue.Items.AddRange(new string[] { "Red", "Black", "Green" });
                    break;
                case BetType.Dozen:
                    cmbBetValue.Items.AddRange(new string[] { "1st", "2nd", "3rd" });
                    break;
                case BetType.Range:
                    cmbBetValue.Items.AddRange(new string[] { "1-18", "19-36" });
                    break;
                case BetType.OddEven:
                    cmbBetValue.Items.AddRange(new string[] { "Odd", "Even" });
                    break;
            }

            cmbBetValue.SelectedIndex = 0;
        }

        private void btnPlaceBet_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBetAmount.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid bet amount.");
                return;
            }

            if (player.Balance < amount)
            {
                MessageBox.Show("Insufficient balance.");
                return;
            }

            var type = (BetType)cmbBetType.SelectedItem;
            var value = cmbBetValue.SelectedItem.ToString();

            var bet = new Bet(type, value, amount);
            var (result, message) = game.PlayRound(player, bet);

            lblBalance.Text = $"Balance: ${player.Balance}";
            lstResults.Items.Insert(0, $"Landed on: {result.Number} ({result.Color})");
            lstResults.Items.Insert(0, message);
        }

        private void CreateRouletteTable()
        {
            int startX = 30;
            int startY = 280;
            int buttonSize = 40;
            int numbersPerRow = 12;

            for (int i = 0; i <= 36; i++)
            {
                Button btn = new Button();
                btn.Text = i.ToString();
                btn.Size = new Size(buttonSize, buttonSize);
                btn.BackColor = GetRouletteColor(i);
                btn.ForeColor = Color.White;
                btn.Tag = i; 
                btn.Click += BetButton_Click;

                int row = i / numbersPerRow;
                int col = i % numbersPerRow;

                btn.Location = new Point(startX + col * (buttonSize + 5), startY + row * (buttonSize + 5));
                this.Controls.Add(btn);
            }

        }

        private Color GetRouletteColor(int number)
        {
            if (number == 0) return Color.Green;

            int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
            return redNumbers.Contains(number) ? Color.Red : Color.Black;
        }

        private void BetButton_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int number = (int)btn.Tag;

            cmbBetType.SelectedItem = BetType.Number;
            UpdateBetValues();
            cmbBetValue.SelectedItem = number.ToString();
            txtBetAmount.Text = "10"; 

            btnPlaceBet.PerformClick(); 
        }
    }
}
