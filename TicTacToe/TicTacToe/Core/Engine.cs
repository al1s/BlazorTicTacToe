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
        public Task ChooseMove(Board board)
        {
            throw new NotImplementedException();
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
        /// -1 - if the player loose
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
                return (symbol == playerSymbol)
                    ? new Tuple<bool, int> (true, 1)
                    : new Tuple<bool, int> (true, -1);
            }
            // clone current board to calculate utility for next move
            Board newBoard = board.Clone();
            newBoard.Cells[move].Symbol = player;
            // hard code winning conditions - it's faster then loop through cells
            char[] boardAsArray = newBoard.ToArray();

            // All rows for 3x3 board
            char firstRowFirstElm = boardAsArray[0]; 
            bool firstRowClosed = firstRowFirstElm != default(char) &&
                boardAsArray
                    .Take(3)
                    .All(elm => elm == firstRowFirstElm);
            if (firstRowClosed) return GetResult(firstRowFirstElm, player); 
            
            char secondRowFirstElm = boardAsArray[3]; 
            bool secondRowClosed = secondRowFirstElm != default(char) &&
                boardAsArray
                    .Skip(3)
                    .Take(3)
                    .All(elm => elm == secondRowFirstElm);
            if (secondRowClosed) return GetResult(secondRowFirstElm, player); 

            char thirdRowFirstElm = boardAsArray[6]; 
            bool thirdRowClosed = thirdRowFirstElm != default(char) &&
                boardAsArray
                    .Skip(6)
                    .Take(3)
                    .All(elm => elm == thirdRowFirstElm);
            if (thirdRowClosed) return GetResult(thirdRowFirstElm, player); 

            // All columns for 3x3 board
            char firstColumnFirstElm = boardAsArray[0]; 
            bool firstColumnClosed = firstColumnFirstElm != default(char) &&
                boardAsArray
                    .Where((x, ndx) => ndx % 3 == 0)
                    .All(elm => elm == firstColumnFirstElm);
            if (firstColumnClosed) return GetResult(firstColumnFirstElm, player); 

            char secondColumnFirstElm = boardAsArray[1]; 
            bool secondColumnClosed = secondColumnFirstElm != default(char) &&
                boardAsArray
                    .Where((x, ndx) => (ndx + 2) % 3 == 0)
                    .All(elm => elm == secondColumnFirstElm);
            if (secondColumnClosed) return GetResult(secondColumnFirstElm, player);

            char thirdColumnFirstElm = boardAsArray[1]; 
            bool thirdColumnClosed = thirdColumnFirstElm != default(char) &&
                boardAsArray
                    .Where((x, ndx) => (ndx + 1) % 3 == 0)
                    .All(elm => elm == thirdColumnFirstElm);
            if (thirdColumnClosed) return GetResult(thirdColumnFirstElm, player);

            // Diagonal for 3x3 board
            char diagFirstElm = boardAsArray[0]; 
            bool diagClosed = diagFirstElm != default(char) &&
                boardAsArray
                    .Where((x, ndx) => ndx % 4 == 0)
                    .All(elm => elm == diagFirstElm );
            if (diagClosed) return GetResult(diagFirstElm, player); 

            // Anti Diagonal for 3x3 board
            char antiDiagFirstElm = boardAsArray[2]; 
            bool antiDiagClosed = antiDiagFirstElm != default(char) &&
                boardAsArray
                    .Where((x, ndx) => ndx % 2 == 0 && ndx != 0 && ndx != 8)
                    .All(elm => elm == antiDiagFirstElm );
            if (antiDiagClosed) return GetResult(antiDiagFirstElm, player);

            if (newBoard.MovesLeft.Count == 0)
                return new Tuple<bool, int>(true, 0);
            return new Tuple<bool, int>(false, 0);
        }
    }
}
