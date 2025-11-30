using System;

namespace TicTacToe
{
    public abstract class Game
    {
        public int PlayerWins { get; set; }
        public int BotWins { get; set; }
        public int Ties { get; set; }

        public abstract void ResetBoard();
        public abstract bool IsBoardFull();
        public abstract string GetWinner();
        public abstract void SetBoardSize(int size, int winLength);

        public virtual void SaveScoreboard(string path)
        {
            string data = $"{PlayerWins},{BotWins},{Ties}";
            System.IO.File.WriteAllText(path, data);
        }

        public virtual void LoadScoreboard(string path)
        {
            if (!System.IO.File.Exists(path))
                return;

            string text = System.IO.File.ReadAllText(path);
            string[] parts = text.Split(',');

            if (parts.Length == 3)
            {
                PlayerWins = int.Parse(parts[0]);
                BotWins = int.Parse(parts[1]);
                Ties = int.Parse(parts[2]);
            }
        }
    }
}
