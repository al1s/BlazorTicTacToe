using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Core.Classes
{
    /// <summary>
    /// Game board
    /// </summary>
    public class Board
    {
        /// <summary>
        /// All cells of the board
        /// </summary>
        public Cell[] Cells { get; set; }
        /// <summary>
        /// All available cells to make move
        /// </summary>
        public Dictionary<int, Cell> MovesLeft
        {
            get
            {
                return Cells.Where(cell => cell.Symbol == string.Empty).ToDictionary(cell => cell.Position, cell => cell);
            }
        }
        /// <summary>
        /// Dimention of the board (currently hard set to 3 * 3)
        /// </summary>
        public int Dimension { get; set; }
        /// <summary>
        /// Populate a board with cells 
        /// </summary>
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
        /// <summary>
        /// Get any available for a move cell 
        /// </summary>
        /// <returns>A cell</returns>
        public Cell GetRandomAvailableCell()
        {
            Random rnd = new Random();
            return MovesLeft.Values.ToArray()[rnd.Next(0, MovesLeft.Count)];
        }
    }
}
