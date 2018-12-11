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
                return Cells.Where(cell => cell.Symbol == default(char)).ToDictionary(cell => cell.Position, cell => cell);
            }
        }

        /// <summary>
        /// Define dimension of the board
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

        /// <summary>
        /// Clone current board
        /// </summary>
        /// <returns>Return new board object with all properties equal to original board</returns>
        public Board Clone()
        {
            Board newBoard = new Board();
            newBoard.Dimension = Dimension;
            newBoard.Initialize();
            for (int i = 0; i < Cells.Length; i++)
            {
                newBoard.Cells[i].Position = Cells[i].Position;
                newBoard.Cells[i].Symbol = Cells[i].Symbol;
            }
            return newBoard;
        }

        /// <summary>
        /// Return array of symbols in cells
        /// </summary>
        /// <returns>Array of symbols</returns>
        public char[] ToArray()
        {
            return Cells.Select(cell => cell.Symbol).ToArray();
        }

        /// <summary>
        /// Return a string representation of all symbols in cells
        /// </summary>
        /// <returns>A string with all symbols</returns>
        public override string ToString()
        {
            return string.Join("", ToArray());
        }
    }
}
