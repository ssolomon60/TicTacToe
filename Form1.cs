using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        TicTacToeGame game = new TicTacToeGame();
        bool isPlayerTurn = true;
        string difficulty = "EASY";
        Label scoreboardLabel;
        string savePath = "scoreboard.txt";

        public Form1()
        {
            InitializeComponent();
            CreateModeButtons();
            CreateDifficultyButtons();
            CreateScoreboard();
            Load3x3();
            game.LoadScoreboard(savePath);
            UpdateScoreboard();
        }

        void CreateModeButtons()
        {
            Button mode3 = new Button();
            mode3.Text = "3x3 Mode";
            mode3.Top = 0;
            mode3.Left = 0;
            mode3.Width = 120;
            mode3.Click += (s, e) => Load3x3();
            Controls.Add(mode3);

            Button mode4 = new Button();
            mode4.Text = "4x4 Mode";
            mode4.Top = 0;
            mode4.Left = 130;
            mode4.Width = 120;
            mode4.Click += (s, e) => Load4x4();
            Controls.Add(mode4);
        }

        void CreateDifficultyButtons()
        {
            Button easy = new Button();
            easy.Text = "Easy";
            easy.Top = 40;
            easy.Left = 0;
            easy.Width = 80;
            easy.Click += (s, e) => difficulty = "EASY";
            Controls.Add(easy);

            Button med = new Button();
            med.Text = "Medium";
            med.Top = 40;
            med.Left = 90;
            med.Width = 80;
            med.Click += (s, e) => difficulty = "MEDIUM";
            Controls.Add(med);

            Button hard = new Button();
            hard.Text = "Hard";
            hard.Top = 40;
            hard.Left = 180;
            hard.Width = 80;
            hard.Click += (s, e) => difficulty = "HARD";
            Controls.Add(hard);
        }

        void CreateScoreboard()
        {
            scoreboardLabel = new Label();
            scoreboardLabel.Top = 350;
            scoreboardLabel.Left = 0;
            scoreboardLabel.Width = 300;
            scoreboardLabel.Height = 200;
            scoreboardLabel.Font = new System.Drawing.Font("Arial", 14);
            Controls.Add(scoreboardLabel);
        }

        void UpdateScoreboard()
        {
            scoreboardLabel.Text =
                $"Player Wins: {game.PlayerWins}\n" +
                $"Bot Wins: {game.BotWins}\n" +
                $"Ties: {game.Ties}";
            game.SaveScoreboard(savePath);
        }

        void Load3x3()
        {
            ClearBoard();
            game.SetBoardSize(3, 3);
            CreateBoard();
        }

        void Load4x4()
        {
            ClearBoard();
            game.SetBoardSize(4, 4);
            CreateBoard();
        }

        void ClearBoard()
        {
            foreach (Control c in Controls.OfType<Button>().ToList())
                if (c.Text == "")
                    Controls.Remove(c);

            foreach (Control c in Controls.OfType<Button>().Where(x => x.Top > 80).ToList())
                Controls.Remove(c);
        }

        void CreateBoard()
        {
            int size = 80;
            int offsetTop = 80;

            for (int r = 0; r < game.BoardSize; r++)
            {
                for (int c = 0; c < game.BoardSize; c++)
                {
                    Button b = new Button();
                    b.Width = size;
                    b.Height = size;
                    b.Left = c * size;
                    b.Top = r * size + offsetTop;
                    b.Font = new System.Drawing.Font("Arial", 20);
                    b.Click += PlayerMove;

                    game.Buttons[r, c] = b;
                    Controls.Add(b);
                }
            }
        }

        void PlayerMove(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text != "" || !isPlayerTurn)
                return;

            btn.Text = "X";
            isPlayerTurn = false;

            if (CheckGameEnd())
                return;

            BotMove();
        }

        void BotMove()
        {
            if (difficulty == "EASY")
                BotEasy();
            else if (difficulty == "MEDIUM")
                BotMedium();
            else if (difficulty == "HARD")
                BotHard();

            isPlayerTurn = true;
            CheckGameEnd();
        }

        void BotEasy()
        {
            var empty = new List<Button>();
            foreach (Button b in game.Buttons)
                if (b.Text == "")
                    empty.Add(b);

            if (empty.Count == 0) return;

            Random rand = new Random();
            empty[rand.Next(empty.Count)].Text = "O";
        }

        void BotMedium()
        {
            if (TryWinOrBlock("O")) return;
            if (TryWinOrBlock("X")) return;
            BotEasy();
        }

        bool TryWinOrBlock(string mark)
        {
            foreach (Button b in game.Buttons)
            {
                if (b.Text == "")
                {
                    b.Text = mark;
                    if (game.GetWinner() == mark)
                    {
                        if (mark == "X") b.Text = "";
                        return true;
                    }
                    b.Text = "";
                }
            }
            return false;
        }

        void BotHard()
        {
            int bestScore = int.MinValue;
            Button bestMove = null;

            foreach (Button b in game.Buttons)
            {
                if (b.Text == "")
                {
                    b.Text = "O";
                    int score = Minimax(false);
                    b.Text = "";

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = b;
                    }
                }
            }

            if (bestMove != null)
                bestMove.Text = "O";
        }

        int Minimax(bool isMaximizing)
        {
            string winner = game.GetWinner();
            if (winner == "O") return 10;
            if (winner == "X") return -10;
            if (game.IsBoardFull()) return 0;

            int bestScore = isMaximizing ? int.MinValue : int.MaxValue;

            foreach (Button b in game.Buttons)
            {
                if (b.Text == "")
                {
                    b.Text = isMaximizing ? "O" : "X";
                    int score = Minimax(!isMaximizing);
                    b.Text = "";

                    if (isMaximizing)
                        bestScore = Math.Max(score, bestScore);
                    else
                        bestScore = Math.Min(score, bestScore);
                }
            }

            return bestScore;
        }

        bool CheckGameEnd()
        {
            string winner = game.GetWinner();

            if (winner != null)
            {
                if (winner == "X")
                    game.PlayerWins++;
                else
                    game.BotWins++;

                MessageBox.Show(winner + " wins!");
                UpdateScoreboard();
                DisableBoard();
                return true;
            }

            if (game.IsBoardFull())
            {
                game.Ties++;
                MessageBox.Show("It's a tie!");
                UpdateScoreboard();
                return true;
            }

            return false;
        }

        void DisableBoard()
        {
            foreach (Button b in game.Buttons)
                b.Enabled = false;
        }
    }
}
