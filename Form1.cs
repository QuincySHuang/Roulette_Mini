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
                    cmbBetValue.Items.AddRange(new string[] { "Red", "Black" });
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
            int startX = 50;
            int startY = 280;
            int buttonSize = 40;
            int spacing = 5;

            Button btnZero = new Button();
            btnZero.Text = "0";
            btnZero.Size = new Size(buttonSize, buttonSize * 3 + spacing * 2);
            btnZero.BackColor = Color.Green;
            btnZero.ForeColor = Color.White;
            btnZero.Tag = 0;
            btnZero.Location = new Point(startX, startY);
            btnZero.Click += BetButton_Click;
            this.Controls.Add(btnZero);

            int[,] layout = new int[3, 12]
            {
        { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 },
        { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 },
        { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 }
            };

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 12; col++)
                {
                    int number = layout[row, col];
                    Button btn = new Button();
                    btn.Text = number.ToString();
                    btn.Size = new Size(buttonSize, buttonSize);
                    btn.BackColor = GetRouletteColor(number);
                    btn.ForeColor = Color.White;
                    btn.Tag = number;
                    btn.Location = new Point(startX + buttonSize + spacing + col * (buttonSize + spacing),
                                             startY + row * (buttonSize + spacing));
                    btn.Click += BetButton_Click;
                    this.Controls.Add(btn);
                }
            }

            int labelY = startY + 3 * (buttonSize + spacing) + 10;
            int labelX = startX + buttonSize + spacing;

            string[] outsideBets = { "1st 12", "2nd 12", "3rd 12", "1 to 18", "19 to 36", "Even", "Odd", "Red", "Black" };
            for (int i = 0; i < outsideBets.Length; i++)
            {
                Button betBtn = new Button();
                betBtn.Text = outsideBets[i];
                betBtn.Size = new Size(70, 30);
                betBtn.Location = new Point(labelX + i * (75), labelY);
                betBtn.Click += OutsideBet_Click;
                this.Controls.Add(betBtn);
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
        private void OutsideBet_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string betText = btn.Text;

            BetType type;
            string value;

            if (betText.Contains("12"))
            {
                type = BetType.Dozen;
                value = betText.Substring(0, 4);
            }
            else if (betText == "1 to 18" || betText == "19 to 36")
            {
                type = BetType.Range;
                value = betText.Replace(" to ", "-");
            }
            else if (betText == "Odd" || betText == "Even")
            {
                type = BetType.OddEven;
                value = betText;
            }
            else if (betText == "Red" || betText == "Black")
            {
                type = BetType.Color;
                value = betText;
            }
            else
            {
                return; 
            }

            cmbBetType.SelectedItem = type;
            UpdateBetValues(); 
            cmbBetValue.SelectedItem = value;
            txtBetAmount.Text = "10";  

            btnPlaceBet.PerformClick();
        }

    }
}
