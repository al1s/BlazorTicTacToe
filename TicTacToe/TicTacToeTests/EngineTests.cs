using System;
using Xunit;
using TicTacToe.Core.Classes;
using TicTacToe.Core;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeTests
{
    /// <summary>
    /// Engine provides calculation of utility of each position on the board
    /// for players. It minimizes loses for itself (AI). If utility is 1, than 
    /// the AI is winner, -1 the user is winner, 0 - it's a tie.
    /// </summary>
    public class EngineTests :IDisposable
    {
        Manager manager;
        Engine engine;
        Board board;
        public EngineTests()
        {
            // arrange
            manager = new Manager(null);
            engine = new Engine(manager);
            board = new Board();
            int expectedCells = 9;
            board.Dimension = expectedCells / 3;
            // act
            board.Initialize();
        }

        public void Dispose()
        {
            manager = null;
            board = null;
            engine = null;
        }
        /// <summary>
        /// Can win top row
        /// </summary>
        [Fact]
        public void CanWinOnTopRow()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[1].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 2, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win middle row
        /// </summary>
        [Fact]
        public void CanWinMiddleRow()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[3].Symbol = 'X';
            board.Cells[4].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 5, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win bottom row
        /// </summary>
        [Fact]
        public void CanWinBottomRow()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[6].Symbol = 'X';
            board.Cells[7].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 8, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win left column
        /// </summary>
        [Fact]
        public void CanWinLeftCol()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[3].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 6, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win middle column
        /// </summary>
        [Fact]
        public void CanWinMiddleCol()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[1].Symbol = 'X';
            board.Cells[4].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 7, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win right column
        /// </summary>
        [Fact]
        public void CanWinRightCol()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[2].Symbol = 'X';
            board.Cells[5].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 8, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win diagonal
        /// </summary>
        [Fact]
        public void CanWinDiag()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[4].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 8, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }
        /// <summary>
        /// Can win anti-diagonal
        /// </summary>
        [Fact]
        public void CanWinAntiDiag()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[2].Symbol = 'X';
            board.Cells[4].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 6, 'X');
            // assert
            Assert.Equal(-1, result.Item2);
        }        
        /// <summary>
        /// Can lose top row
        /// </summary>
        [Fact]
        public void CanLoseTopRow()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = '0';
            board.Cells[1].Symbol = '0';
            board.Cells[2].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 3, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose middle row
        /// </summary>
        [Fact]
        public void CanLoseMiddleRow()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[3].Symbol = '0';
            board.Cells[4].Symbol = '0';
            board.Cells[5].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 6, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose bottom row
        /// </summary>
        [Fact]
        public void CanLoseBottomRow()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[6].Symbol = '0';
            board.Cells[7].Symbol = '0';
            board.Cells[8].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 0, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose left column
        /// </summary>
        [Fact]
        public void CanLoseLeftCol()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = '0';
            board.Cells[3].Symbol = '0';
            board.Cells[6].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 7, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose middle column
        /// </summary>
        [Fact]
        public void CanLoseMiddleCol()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[1].Symbol = '0';
            board.Cells[4].Symbol = '0';
            board.Cells[7].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 8, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose right column
        /// </summary>
        [Fact]
        public void CanLoseRightCol()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[2].Symbol = '0';
            board.Cells[5].Symbol = '0';
            board.Cells[8].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 0, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose diagonal
        /// </summary>
        [Fact]
        public void CanLoseDiag()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = '0';
            board.Cells[4].Symbol = '0';
            board.Cells[8].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 1, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can lose anti-diagonal
        /// </summary>
        [Fact]
        public void CanLoseAntiDiag()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[2].Symbol = '0';
            board.Cells[4].Symbol = '0';
            board.Cells[6].Symbol = '0';
            // act
            Tuple<bool, int> result = engine.Utility(board, 7, 'X');
            // assert
            Assert.Equal(1, result.Item2);
        }
        /// <summary>
        /// Can tie game
        /// </summary>
        [Fact]
        public void CanTieGame()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[1].Symbol = '0';
            board.Cells[2].Symbol = 'X';
            board.Cells[3].Symbol = '0';
            board.Cells[4].Symbol = '0';
            board.Cells[5].Symbol = 'X';
            board.Cells[6].Symbol = 'X';
            board.Cells[7].Symbol = 'X';
            // act
            Tuple<bool, int> result = engine.Utility(board, 8, '0');
            // assert
            Assert.Equal(0, result.Item2);
        }
        /// <summary>
        /// Determines whether this instance [can test mini maximum early game as0].
        /// </summary>
        [Fact]
        public void CanTestMiniMaxEarlyGameAs0()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[8].Symbol = 'X';
            // act
            int result = engine.MiniMax(board, 1, '0');
            // assert
            Assert.Equal(-1, result);
        }
        /// <summary>
        /// Determines whether this instance [can test mini maximum early game as x].
        /// </summary>
        [Fact]
        public void CanTestMiniMaxEarlyGameAsX()
        {
            // arrange
            BoardTests BST = new BoardTests();
            // act
            int result = engine.MiniMax(board, 0, 'X');
            // assert
            Assert.Equal(0, result);
        }
        /// <summary>
        /// Determines whether this instance [can test mini maximum mid game as0].
        /// </summary>
        [Fact]
        public void CanTestMiniMaxMidGameAs0()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[1].Symbol = '0';
            board.Cells[8].Symbol = 'X';
            board.Cells[7].Symbol = '0';
            board.Cells[2].Symbol = 'X';
            // act
            int result = engine.MiniMax(board, 3, '0');
            // assert
            Assert.Equal(-1, result);
        }
        /// <summary>
        /// Determines whether this instance [can test mini maximum mid game as x].
        /// </summary>
        [Fact]
        public void CanTestMiniMaxMidGameAsX()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[4].Symbol = '0';
            board.Cells[3].Symbol = 'X';
            board.Cells[6].Symbol = '0';
            // act
            int result = engine.MiniMax(board, 8, 'X');
            // assert
            Assert.Equal(1, result);
        }
        /// <summary>
        /// Determines whether this instance [can test mini maximum late game loss as0].
        /// </summary>
        [Fact]
        public void CanTestMiniMaxLateGameLossAs0()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[1].Symbol = '0';
            board.Cells[2].Symbol = 'X';
            board.Cells[3].Symbol = '0';
            board.Cells[4].Symbol = 'X';
            board.Cells[5].Symbol = '0';
            board.Cells[6].Symbol = 'X';
            // act
            int result = engine.MiniMax(board, 8, '0');
            // assert
            Assert.Equal(-1, result);
        }
        /// <summary>
        /// Determines whether this instance [can test mini maximum late game loss as x].
        /// </summary>
        [Fact]
        public void CanTestMiniMaxLateGameLossAsX()
        {
            // arrange
            BoardTests BST = new BoardTests();
            board.Cells[0].Symbol = 'X';
            board.Cells[1].Symbol = '0';
            board.Cells[2].Symbol = 'X';
            board.Cells[3].Symbol = '0';
            board.Cells[4].Symbol = '0';
            board.Cells[6].Symbol = 'X';
            board.Cells[7].Symbol = '0';
            // act
            int result = engine.MiniMax(board, 5, 'X');
            // assert
            Assert.Equal(1, result);
        }            
    }
}
