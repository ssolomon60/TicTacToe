using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TicTacToe
{
    public class TicTacToeGame : Game
    {
        public Button[,] Buttons;
        public int BoardSize = 3;
        public int WinLength = 3;

        public override void SetBoardSize(int size, int winLength)
        {
            BoardSize = size;
            WinLength = winLength;
            Buttons = new Button[size, size];
        }

        public override void ResetBoard()
        {
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                {
                    Buttons[r, c].Enabled = true;
                    Buttons[r, c].Text = "";
                }
        }

        public override bool IsBoardFull()
        {
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                    if (Buttons[r, c].Text == "")
                        return false;

            return true;
        }

        public override string GetWinner()
        {
            for (int r = 0; r < BoardSize; r++)
                for (int c = 0; c < BoardSize; c++)
                {
                    string mark = Buttons[r, c].Text;
                    if (mark == "")
                        continue;

                    if (CheckDirection(r, c, 1, 0, mark)) return mark;
                    if (CheckDirection(r, c, 0, 1, mark)) return mark;
                    if (CheckDirection(r, c, 1, 1, mark)) return mark;
                    if (CheckDirection(r, c, 1, -1, mark)) return mark;
                }

            return null;
        }

        private bool CheckDirection(int row, int col, int dr, int dc, string mark)
        {
            for (int i = 0; i < WinLength; i++)
            {
                int rr = row + dr * i;
                int cc = col + dc * i;

                if (rr < 0 || rr >= BoardSize || cc < 0 || cc >= BoardSize)
                    return false;

                if (Buttons[rr, cc].Text != mark)
                    return false;
            }
            return true;
        }
    }
}
