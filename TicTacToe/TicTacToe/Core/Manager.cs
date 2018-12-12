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
        public void MoveAndGetUtil()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handle click event initiated by a user
        /// </summary>
        /// <param name="position">Cell position being clicked</param>
        public void HandleClick(int position)
        {
            if (_board.Cells[position].Symbol == default(char))
                MakeMove(position, _minPlayer);
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
            _minPlayer = symbol;
            _maxPlayer = symbol == '0' ? 'X' : '0';
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
            if (board.MovesLeft.Values.Count >= board.Dimension - 1)
                return board.GetBestDebutMove().Position;
            else
            // we are in the middle of the game
            {
                int topUtility = int.MinValue;
                Random rnd = new Random();
                // we  are going to use minimax to get appraisal 
                // of all possible next moves.
                // Compute intensive! Should be async.
                var availableMoves = board
                    .MovesLeft
                    .Keys
                    .Select(position =>
                    {
                        var utility = _engine.MiniMax(board, position, _maxPlayer);
                        if (topUtility < utility)
                            topUtility = utility;
                        return new Tuple<int, int>(position, utility);
                    })
                    .Where(elm => elm.Item2 == topUtility);
                int randomAvailableMove = rnd.Next(0, availableMoves.Count()); 
                return availableMoves.ToArray()[randomAvailableMove].Item1;

            }
        }

        public void HandleTerminalConditions(int condition)
        {
            if (condition == -1) _view.ShowMsg("You win!");
            else if (condition == 1) _view.ShowMsg("Computer wins!");
            else if (condition == 0) _view.ShowMsg("Draw!");
            Updated?.Invoke(this, new EventArgs());
            Task delay = Task.Run(() =>
            {
                Task.Delay(3000);
                _board.Initialize();
            });
        }
    }
}
