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
            Cells = new Cell[Dimension * Dimension];
            // need to explicitly initialize all cells or will get
            // null reference exception on attempt to read any property
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = new Cell() { Position = i };
            }
        }
        /// <summary>
        /// Get any available for a move cell 
        /// </summary>
        /// <returns>A cell</returns>
        public Cell GetRandomAvailableCell(List<int> filterIn = null)
        {
            Random rnd = new Random();
            if(filterIn != null)
            {
                var filtered = 
                    (from move in MovesLeft
                     join elm in filterIn on move.Key equals elm
                     select move)
                   .ToDictionary(kvp => kvp.Key, kvp => kvp.Value); 
                return filtered.Values.ToArray()[rnd.Next(0, filtered.Count)];
            }
            return MovesLeft.Values.ToArray()[rnd.Next(0, MovesLeft.Count)];
        }
        /// <summary>
        /// Get best first (second if not computer starts) move
        /// </summary>
        /// <returns></returns>
        public Cell GetBestDebutMove()
        {
            // Best initial positions include center cells or tips of
            // diagonals
            List<int> bestPositions = new List<int>
            {
                0,
                Dimension - 1,
                Cells.Length - Dimension,
                Cells.Length - 1,
                Dimension % 2 != 0 ? (Cells.Length - 1) / 2 : -1
            };
            return GetRandomAvailableCell(bestPositions);
        }
    }
}
