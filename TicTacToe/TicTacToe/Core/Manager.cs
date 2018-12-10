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
        public void StartGameLoop() { }

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
            _board.Cells = new Cell[boardSize * boardSize];
            for(int i = 0; i < _board.Cells.Length; i++)
            {
                _board.Cells[i] = new Cell();
            }
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
    }
}
