using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe.Model
{
    public class GameBoard
    {
        private int[,] gameBoard;
        private int currentPlayer;

        public int CurrentPlayer => currentPlayer;

        public GameBoard()
        {
            gameBoard = new int[3, 3];
            /* 0 - the cell is empty
             * 1 - Player X
             * 2 - Player O
             */
            currentPlayer = 1;
        }


        public void MakeMove(int row, int col)
        {
            //check that the transmitted coordinates are within the playing field
            if(row < 0 || row >= 3 || col < 0 || col >= 3)
            {
                throw new ArgumentException("Invalid position. Position must be between 0 and 2 for both row and column.");
            }
            //check that the selected position is not occupied
            if (gameBoard[row, col] != 0)
            {
                throw new ArgumentException("Invalid move. The position is already occupied.");
            }

            gameBoard[row, col] = currentPlayer; // the current player's move in the specified position is set
            currentPlayer = (currentPlayer == 1) ? 2 : 1; // switch the player to the next one 
        }
        /* This method is needed in order to cancel the last move.
         * this is important for the correct inplementation of the 
         * MiniMax algorithm
         */
        public void UndoMove(int row, int col)
        {
            //check that the transmitted coordinates are within the playing field
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
            {
                throw new ArgumentException("Invalid position. Position must be between 0 and 2 for both row and column.");
            }

            gameBoard[row, col] = 0; // canceling a move?  setting the cell value to 0
            currentPlayer = (currentPlayer == 1) ? 2 : 1; // switch the current player to the next one 
        }

        public int GetPlayerAtPosition(int row, int col)
        {
            //check that the transmitted coordinates are within the playing field
            if (row < 0 || row >= 3 || col < 0 || col >= 3)
            {
                throw new ArgumentException("Invalid position. Position must be between 0 and 2 for both row and column.");
            }
            
            return gameBoard[row, col]; // return the state of the playing field in the specified position 
            }

        public  bool IsGameOver()
        {
            return IsWinningMove(1) || IsWinningMove(2) || IsBoardFull();
        }
        /* this method checks whether the current
         * player's move is victorious
         */
        public bool IsWinningMove(int player)
        {
            for(int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] == player && gameBoard[i, 1] == player && gameBoard[i, 2] == player)
                {
                    return true;
                }
            }

            for(int i =0; i<3; i++)
            {
                if (gameBoard[0, i] == player && gameBoard[1, i] == player && gameBoard[2, i] == player)
                {
                    return true;
                }
            }

            if ((gameBoard[0, 0] == player  && gameBoard[1,1] == player && gameBoard[2,2] == player ||
                gameBoard[0,2] == player && gameBoard[1,1] == player && gameBoard[2,0] == player))
            {
                return true;
            }
            return false;
        }

        public bool IsBoardFull()
        {
            //check if there are empty cells on the playing field
            for(int row = 0; row<3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    if (gameBoard[row, col] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int[,] GetWinningCombination(int player)
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] == player && gameBoard[i, 1] == player && gameBoard[i, 2] == player)
                {
                    return new int[,] { { i, 0 }, { i, 1 }, { i, 2 } };
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[0, i] == player && gameBoard[1, i] == player && gameBoard[2, i] == player)
                {
                    return new int[,] { { 0, i }, { 1, i }, { 2, i } };
                }
            }

            if ((gameBoard[0, 0] == player && gameBoard[1, 1] == player && gameBoard[2, 2] == player))
            {
                return new int[,] { { 0, 0 }, { 1, 1 }, { 2, 2 } };
            }

            if ((gameBoard[0, 2] == player && gameBoard[1, 1] == player && gameBoard[2, 0] == player))
            {
                return new int[,] { { 0, 2 }, { 1, 1 }, { 2, 0 } };
            }

            return null; // Если выигрышной комбинации нет, вернуть null.
        }
    }
}
