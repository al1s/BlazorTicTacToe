using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core
{
    public class Manager : IManager
    {
        public event EventHandler Updated;
        private IView _view;
        private Engine _engine;
        private Board _board;

        /// <summary>
        /// By default the user gets X as symbol
        /// And she's a minimizing player - 
        /// she wants to minimize loss in worst case by her move
        /// </summary>
        private char _minPlayer = 'X';

        /// <summary>
        /// AI by default. Opposite of the user.
        /// </summary>
        private char _maxPlayer = '0';

        /// <summary>
        /// Handles communication between Engine and UI layers
        /// </summary>
        /// <param name="view">UI component</param>
        public Manager(IView view)
        {
            _view = view;
            _engine = new Engine(this);
            _engine.Updated += (obj, e) =>
            {
                Updated?.Invoke(this, e);
            };
            _board = new Board();
        }

        public char GetMaxPlayer
        {
            get
            {
                return _maxPlayer;
            }
        }
        public char GetMinPlayer
        {
            get
            {
                return _minPlayer;
            }
        }

        /// <summary>
        /// Manage board initialization
        /// </summary>
        /// <param name="boardSize">Dimension of the board</param>
        public void InitializeBoard(int boardSize)
        {
            _board.Dimension = boardSize;
            _board.Initialize();
            UpdateBoardView();
        }

        /// <summary>
        /// Sync veiwBoard with an actual board
        /// </summary>
        public void UpdateBoardView() 
        {
            _view.DrawBoard(_board);
        }

        /// <summary>
        /// Handle click event initiated by a user
        /// </summary>
        /// <param name="position">Cell position being clicked</param>
        public void HandleClick(int position)
        {
            if (_board.Cells[position].Symbol == default(char))
            {
                MakeMove(position, _minPlayer);
                var (terminal, utility) = _engine.Utility(_board, position, _minPlayer);
                if (terminal)
                    HandleTerminalConditions(utility);
                else
                    ComputerMove();
            }
        }

        /// <summary>
        /// Invoke page renderer on changes
        /// </summary>
        /// <param name="position">Cell's position</param>
        /// <param name="symbol">Symbol to set for a cell</param>
        public void MakeMove(int position, char symbol)
        {
            _board.Cells[position].Symbol = symbol;
            // to customize design pass changes to UI layer and handle element design
            // there.
            Updated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handle attempt of changing of symbols assigned to parties
        /// </summary>
        /// <param name="symbol">Symbol to assign to user player</param>
        public void SetSymbol(char symbol)
        {
            Console.WriteLine($"user's choice: {symbol}");
            Console.WriteLine($"Symbols before change. minPlayer: {_minPlayer}, maxPlayer: {_maxPlayer}");
            _minPlayer = symbol;
            _maxPlayer = symbol == '0' ? 'X' : '0';
            Console.WriteLine($"Symbols after change. minPlayer: {_minPlayer}, maxPlayer: {_maxPlayer}");
            _board.Initialize();
            Updated?.Invoke(this, new EventArgs());
            if (_maxPlayer == 'X')
            {
                Console.WriteLine("AI's turn");
                ComputerMove();
            }
        }

        /// <summary>
        /// Choose next move from best available with shortcut for first/second
        /// </summary>
        /// <param name="board">A board to choose next move to</param>
        /// <returns>Position for the next move</returns>
        public int ChooseMove(Board board)
        {
            // if we just started and AI needs to make first move
            // we use shortcut route
            if (board.MovesLeft.Values.Count >= board.Dimension * board.Dimension- 1)
                return board.GetBestDebutMove().Position;
            else
            // we are in the middle of the game
            {
                int topUtility = int.MinValue;
                Random rnd = new Random();
                // we  are going to use minimax to get appraisal 
                // of all possible next moves.
                // Compute intensive! Should be async.
                Console.WriteLine($"moves left: {board.MovesLeft.Count()}");
                foreach (var cell in board.MovesLeft)
                {
                    Console.WriteLine($"available cells are (pos): {cell.Key}");
                }
                var availableMoves = board
                    .MovesLeft
                    .Keys
                    .Select(position =>
                    {
                        var utility = _engine.MiniMax(board, position, _maxPlayer);
                        if (topUtility < utility)
                            topUtility = utility;
                        return new Tuple<int, int>(position, utility);
                    }).ToArray();
                var movesWithTopUtility = availableMoves.Where(elm => elm.Item2 == topUtility).ToArray();
                Console.WriteLine($"available moves cnt: {movesWithTopUtility.Count()}");
                foreach (var move in movesWithTopUtility)
                {
                    Console.WriteLine($"available moves are (pos, util): {move.Item1}, {move.Item2}");
                }
                int randomAvailableMove = rnd.Next(0, movesWithTopUtility.Count()); 
                return movesWithTopUtility[randomAvailableMove].Item1;

            }
        }

        /// <summary>
        /// Handle different conditions for game over state
        /// </summary>
        /// <param name="condition">Win, lose, tie</param>
        public void HandleTerminalConditions(int condition)
        {
            if (condition == -1) _view.ShowMsg("You win!");
            else if (condition == 1) _view.ShowMsg("Computer wins!");
            else if (condition == 0) _view.ShowMsg("Draw!");
            Updated?.Invoke(this, new EventArgs());
            _view.HideMsg();            
            _board.Initialize();
        }

        /// <summary>
        /// Make next move and handle game over
        /// </summary>
        public void ComputerMove()
        {
            int nextMove = ChooseMove(_board);
            _board.Cells[nextMove].Symbol = _maxPlayer;
            var (terminal, utility) = _engine.Utility(_board, nextMove, _maxPlayer);
            if (terminal)
                HandleTerminalConditions(utility);
        }
    }
}
