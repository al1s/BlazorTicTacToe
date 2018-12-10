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
        private string _minPlayer = "X";
        private string _maxPlayer = "0";

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
        public void InitializeBoard(int boardSize)
        {
            _board.Dimension = boardSize * boardSize ;
            _board.Initialize();
            UpdateBoardView();
        }
        public void UpdateBoardView()
        {
            _view.DrawBoard(_board);
        }
        public void MoveAndGetUtil()
        {
            throw new NotImplementedException();
        }
        public void HandleClick(int position)
        {
            if(_board.Cells[position].Symbol == string.Empty)
            {
                _board.Cells[position].Symbol = _minPlayer;
                Updated?.Invoke(this, new EventArgs());
            }
        }
        public void SetSymbol(string symbol)
        {
            _minPlayer = symbol;
            _maxPlayer = symbol == "0" ? "X" : "0";
        }
    }
}
