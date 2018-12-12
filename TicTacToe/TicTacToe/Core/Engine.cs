using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core
{
    public class Engine : IEngine
    {
        public event EventHandler Updated;
        private IManager _manager;
        public Engine(IManager manager)
        {
            _manager = manager;
        }
        public int MiniMax(Board board, int move, char player)
        {
            // return utility and stop recurrsion
            var (terminal, utility) = Utility(board, move, player);
            if (terminal)
                return utility;
            // Make a move to calculate utility for the next step
            Board newBoard = board.Clone();
            newBoard.Cells[move].Symbol = player;
            player = player == 'X' ? '0' : 'X';
            if(player == _manager.GetMaxPlayer)
            {
                return newBoard.MovesLeft.Select(cell => MiniMax(newBoard, cell.Key, _manager.GetMaxPlayer)).Max();
            }
            if(player == _manager.GetMinPlayer)
            {
                return newBoard.MovesLeft.Select(cell => MiniMax(newBoard, cell.Key, _manager.GetMinPlayer)).Min();
            }
            throw new Exception("Player is not defined");
        }

        /// <summary>
        /// Return terminal conditions and utility for the next move for the player:
        /// 1 - if the player win
        /// -1 - if the player lose
        /// 0 - no winning, no terminal conditions
        /// </summary>
        /// <param name="board">Board position to start with</param>
        /// <param name="move">Suggested move</param>
        /// <param name="player">The player, we calculate utility for</param>
        /// <returns>Tuple of terminal condition and utility value</returns>
        public Tuple<bool, int> Utility(Board board, int move, char player)
        {
                Tuple<bool, int> GetResult(char symbol, char playerSymbol)
                {
                    return (symbol == _manager.GetMaxPlayer)
                        ? new Tuple<bool, int>(true, 1)
                        : new Tuple<bool, int>(true, -1);
                }
                // clone current board to calculate utility for next move
                Board newBoard = board.Clone();
                newBoard.Cells[move].Symbol = player;

                // hard code winning conditions - it's faster than looping through cells
                char[] boardAsArray = newBoard.ToArray();

                // All rows for 3x3 board

                bool firstRowClosed = boardAsArray[0] != default(char) &&
                    boardAsArray[0] == boardAsArray[1] &&
                    boardAsArray[1] == boardAsArray[2];

                if (firstRowClosed) return GetResult(boardAsArray[0], player);

                bool secondRowClosed = boardAsArray[3] != default(char) &&
                    boardAsArray[3] == boardAsArray[4] &&
                    boardAsArray[4] == boardAsArray[5];

                if (secondRowClosed) return GetResult(boardAsArray[3], player);

                bool thirdRowClosed = boardAsArray[6] != default(char) &&
                    boardAsArray[6] == boardAsArray[7] &&
                    boardAsArray[7] == boardAsArray[8];

                if (thirdRowClosed) return GetResult(boardAsArray[6], player);

                // All columns for 3x3 board

                bool firstColumnClosed = boardAsArray[0] != default(char) &&
                    boardAsArray[0] == boardAsArray[3] &&
                    boardAsArray[3] == boardAsArray[6];

                if (firstColumnClosed) return GetResult(boardAsArray[0], player);

                bool secondColumnClosed = boardAsArray[1] != default(char) &&
                    boardAsArray[1] == boardAsArray[4] &&
                    boardAsArray[4] == boardAsArray[7];

                if (secondColumnClosed) return GetResult(boardAsArray[1], player);

                bool thirdColumnClosed = boardAsArray[2] != default(char) &&
                    boardAsArray[2] == boardAsArray[5] &&
                    boardAsArray[5] == boardAsArray[8];

                if (thirdColumnClosed) return GetResult(boardAsArray[2], player);

                // Diagonal for 3x3 board
                bool diagClosed = boardAsArray[0] != default(char) &&
                    boardAsArray[0] == boardAsArray[4] &&
                    boardAsArray[4] == boardAsArray[8];

                if (diagClosed) return GetResult(boardAsArray[0], player);

                // Anti Diagonal for 3x3 board
                bool antiDiagClosed = boardAsArray[2] != default(char) &&
                    boardAsArray[2] == boardAsArray[4] &&
                    boardAsArray[4] == boardAsArray[6];

                if (antiDiagClosed) return GetResult(boardAsArray[2], player);

                if (newBoard.MovesLeft.Count == 0)
                    return new Tuple<bool, int>(true, 0);
                return new Tuple<bool, int>(false, 0);
            }
        }
}
