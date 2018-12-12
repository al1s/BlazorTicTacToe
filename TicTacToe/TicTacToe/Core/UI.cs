using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Core.Classes;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.Core
{
    public class UI : IView
    {
        private static Board _currentBoard { get; set; }
        private static UI _ui = null;
        private static Manager _manager = null;
        private string _message;
        private bool _showMessage;

        private UI()
        {
            _currentBoard = new Board();
            _currentBoard.Cells = new Cell[] { };
            _manager = new Manager(this);
        }
        public static UI Instance
        {
            get {
                if (_ui == null)
                {
                    _ui = new UI();
                }
                return _ui;
            }

        }
        public Board CurrentBoard
        {
            get {
                return _currentBoard;
            }
        }
        public Manager Manager
        {
            get
            {
                return _manager;
            }
        }

        public bool ShowMessage
        {
            get
            {
                return _showMessage;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }
        public void DrawBoard(Board board)
        {
            _currentBoard = board;
        }

        public void ShowMsg(string message)
        {
            _message = message;
            _showMessage = true;
        }

        public void HideMsg()
        {
            _showMessage = false;
        }
    }
}
