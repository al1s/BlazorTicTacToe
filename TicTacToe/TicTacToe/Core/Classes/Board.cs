using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Core.Classes
{
    public class Board
    {
        public Cell[] Cells { get; set; }
        public Dictionary<int, Cell> MovesLeft
        {
            get
            {
                return Cells.Where(cell => cell.Symbol == string.Empty).ToDictionary(cell => cell.Position, cell => cell);
            }
        }
        public int Dimension { get; set; }
        public void Initialize()
        {
            Cells = new Cell[Dimension];
            // need to explicitly initialize all cells or will get
            // null reference exception on attempt to read any property
            for(int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell() { Position = i };
            }
        }
    }
}
