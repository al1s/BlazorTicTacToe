using System;
using Xunit;
using TicTacToe.Core.Classes;
using System.Linq;

namespace TicTacToeTests
{
    public class BoardTests
    {
        /// <summary>
        /// Can initialize the board
        /// </summary>
        [Fact]
        public void CanInitializeABoard()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 9;
            board.Dimension = expectedCells / 3;
            // act
            board.Initialize();
            // assert
            Assert.Equal(expectedCells, board.Cells.Length);
        }
        /// <summary>
        /// Can get available moves for empty board
        /// </summary>
        [Fact]
        public void ForJustInitializedBoard_CanGetAllCellsAsAvailableMoves()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 4;
            board.Dimension = expectedCells / 2;
            // act
            board.Initialize();
            var cells = board.MovesLeft;
            // assert
            Assert.Equal(expectedCells, cells.Count);
        }
        /// <summary>
        /// Can set symbol values for cells
        /// </summary>
        [Fact]
        public void CanSetSymbolValuesForCells()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 4;
            int cellToSetValue = 0;
            char symbol = 'X';
            board.Dimension = expectedCells / 2;
            // act
            board.Initialize();
            board.Cells[cellToSetValue].Symbol = symbol;
            //assert
            Assert.Equal(symbol, board.Cells[cellToSetValue].Symbol );
        }
        /// <summary>
        /// Can get available moves for filled board
        /// </summary>
        [Fact]
        public void ForJustInitializedBoard_CanGetOneCellAsAvailableMoves()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 4;
            int filledCells = 3;
            int resultAvailCells = expectedCells - filledCells;
            char symbol = 'X';
            board.Dimension = expectedCells / 2;
            // act
            board.Initialize();
            for(int i = 0; i < filledCells; i++)
            {
                board.Cells[i].Symbol = symbol;
            }
            var cells = board.MovesLeft;
            //assert
            Assert.Equal(resultAvailCells, cells.Count);
        }
        /// <summary>
        /// Can get random single cell
        /// </summary>
        [Fact]
        public void CanGetRandomCellFromBoard()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 4;
            int filledCells = 3;
            int resultAvailCells = expectedCells - filledCells;
            char symbol = 'X';
            board.Dimension = expectedCells / 2;
            // act
            board.Initialize();
            for(int i = 0; i < filledCells; i++)
            {
                board.Cells[i].Symbol = symbol;
            }
            var cells = board.MovesLeft.Values.ToArray();
            var randomCell = board.GetRandomAvailableCell();
            //assert
            Assert.Equal(cells[0], randomCell);
        }
        /// <summary>
        /// Can get random cell from multiple available
        /// </summary>
        [Fact]
        public void CanGetRandomCellFromMultipleAvailable()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 4;
            int filledCells = 2;
            char symbol = 'X';
            board.Dimension = expectedCells / 2;
            // act
            board.Initialize();
            for (int i = 0; i < filledCells; i++)
            {
                board.Cells[i].Symbol = symbol;
            }
            var cells = board.MovesLeft.Values.ToArray();
            var randomCell = board.GetRandomAvailableCell();
            //assert
            Assert.IsType<Cell>(randomCell);
            Assert.Contains(randomCell, cells);
        }
        /// <summary>
        /// Can get best debut move from one available
        /// </summary>
        [Fact]
        public void CanGetOneBestDebutMoveFromOneAvailable()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 9;
            int filledCells = 8;
            char symbol = 'X';
            board.Dimension = expectedCells / 3;
            // act
            board.Initialize();
            for (int i = 0; i < filledCells; i++)
            {
                board.Cells[i].Symbol = symbol;
            }
            var bestDebut = board.GetBestDebutMove();
            // assert
            Assert.IsType<Cell>(bestDebut);
            Assert.Contains(bestDebut.Position, Enumerable.Range(filledCells, expectedCells - filledCells));
            Assert.Equal(bestDebut.Position, filledCells);
        }
        /// <summary>
        /// Can get best debut move from all available
        /// </summary>
        [Fact]
        public void CanGetOneBestDebutMoveFromMultipleAvailable()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 9;
            int filledCells = 5;
            char symbol = 'X';
            board.Dimension = expectedCells / 3;
            // act
            board.Initialize();
            for (int i = 0; i < filledCells; i++)
            {
                board.Cells[i].Symbol = symbol;
            }
            var bestDebut = board.GetBestDebutMove();
            // assert
            Assert.IsType<Cell>(bestDebut);
            Assert.Contains(bestDebut.Position, Enumerable.Range(filledCells, expectedCells - filledCells));
        }
        /// <summary>
        /// Can get best debut move from all available
        /// </summary>
        [Fact]
        public void CanGetOneBestDebutMoveFromAllAvailable()
        {
            // arrange
            Board board = new Board();
            int expectedCells = 9;
            board.Dimension = expectedCells / 3;
            int[] allBestMoves = new int[] { 0, 2, 4, 6, 8 };
            // act
            board.Initialize();
            var bestDebut = board.GetBestDebutMove();
            // assert
            Assert.Contains(bestDebut.Position, allBestMoves);
        }
    }
}
