using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tic_Tac_Toe.Commands;
using Tic_Tac_Toe.Model;
using Tic_Tac_Toe.ViewModel.Base;

namespace Tic_Tac_Toe.ViewModel
{
    public class GameViewModelVsComputer : BaseViewModel
    {
        #region Private Members
        private GameBoard gameBoard;
        private string currentPlayer;
        private string isDrawMessage;
        private string currentGameState;
        private bool isGameOver;
        private bool isPlayerXTurn = true;
        #endregion

        #region Commands
        public NavigationCommand NavigateToMenuPage { get => new NavigationCommand(NavigateToPage, new Uri("Views/Pages/MenuPage.xaml", UriKind.RelativeOrAbsolute)); }
        public ICommand CellClickButtonCommand { get; }
        public ICommand RestartButtonCommand { get; }
        public ObservableCollection<ObservableCollection<string>> Board { get; }
        #endregion

        #region Property
        public string CurrentPlayer
        {
            get { return currentPlayer; }
            set { Set(ref currentPlayer, value); }
        }
        public string IsDrawMessage
        {
            get { return isDrawMessage; }
            set { Set(ref isDrawMessage, value); }
        }
        public string CurrentGameState
        {
            get { return currentGameState; }
            set { Set(ref currentGameState, value); }
        }
        public bool IsGameOver
        {
            get { return isGameOver; }
            set { Set(ref isGameOver, value); }
        }
        public bool IsPlayerXTurn
        {
            get { return isPlayerXTurn; }
            set { Set(ref isPlayerXTurn, value); }
        }


        #endregion

        public GameViewModelVsComputer()
        {
            gameBoard = new GameBoard();
            CellClickButtonCommand = new MyRelayCommand(ButtonCommandClick);
            RestartButtonCommand = new MyRelayCommand(RestartButtonClick);
            Board = new ObservableCollection<ObservableCollection<string>>();
            InitializeBoard();
            CurrentPlayer = "Player: X";
            CurrentGameState = "Playing";

        }

        private void InitializeBoard()
        {
            Board.Clear();
            for (int i = 0; i < 3; i++)
            {
                var row = new ObservableCollection<string>();
                for (int j = 0; j < 3; j++)
                {
                    row.Add("");
                }
                Board.Add(row);
            }
        }

        private async void ButtonCommandClick(object parameter)
        {
            if (!isPlayerXTurn || isGameOver)
                return;

            Button clickedButton = parameter as Button;
            if (clickedButton != null)
            {
                int row = Grid.GetRow(clickedButton);
                int col = Grid.GetColumn(clickedButton);

                try
                {
                    if (!string.IsNullOrEmpty(Board[row][col]))
                        return;

                    gameBoard.MakeMove(row, col);
                    Board[row][col] = "X";

                    isGameOver = gameBoard.IsGameOver();
                    if (isGameOver)
                    {
                        if (gameBoard.IsWinningMove(1))
                        {
                            CurrentPlayer = "PlayerX";

                        }
                        else if (gameBoard.IsWinningMove(2))
                        {
                            CurrentPlayer = "PlayerO";
                        }
                        else
                        {
                            IsDrawMessage = "It's a draw!";
                        }
                        CurrentGameState = "GameOver";
                        return;
                    }

                    isPlayerXTurn = false;
                    CurrentPlayer = "Player: O";
                    CurrentGameState = "Playing";

                    await Task.Delay(750);

                    int computer = MakeComputerMove();
                    gameBoard.MakeMove(computer / 3, computer % 3);
                    Board[computer / 3][computer % 3] = "O";

                    isGameOver = gameBoard.IsGameOver();
                    if (isGameOver)
                    {
                        if (gameBoard.IsWinningMove(1))
                        {
                            CurrentPlayer = "PlayerX";
                        }
                        else if (gameBoard.IsWinningMove(2))
                        {
                            CurrentPlayer = "PlayerO";
                        }
                        else
                        {
                            IsDrawMessage = "It's a draw!";
                        }
                        CurrentGameState = "GameOver";
                        return;
                    }
                    isPlayerXTurn = true;
                    CurrentPlayer = "Player: X";
                    CurrentGameState = "Playing";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RestartButtonClick(object parameter)
        {
            //clear the game board
            gameBoard = new GameBoard();
            InitializeBoard();
            IsDrawMessage = string.Empty;
            CurrentPlayer = "Player: X";
            CurrentGameState = "Playing";
            IsGameOver = false;
            isPlayerXTurn = true;
        }


        #region AIMove
        private int MakeComputerMove()
        {
            // list of indexes of all available moves on the game board
            List<int> availableMove = new List<int>();
            /* iterating over the entire game board
             * and checking how many cells are available.
             * if the cell is empty, add to  the list.
             */
            for (int i = 0; i < Board.Count; i++)
            {
                for (int j = 0; j < Board[i].Count; j++)
                {
                    if (string.IsNullOrEmpty(Board[i][j]))
                    {
                        /* this formula (i * 3 + j) is needed to obtain
                         * a one-dimensional index from two-dimensional coordinates
                         */
                        availableMove.Add(i * 3 + j);
                    }
                }
            }

            int bestScore = int.MinValue;
            int bestMoveIndex = 0;

            for (int i = 0; i < availableMove.Count; i++)
            {
                /* After the list of "availableMove" is filled, 
                 * sort through its elements. At each iteration,
                 * one code index is taken from the list of "availableMove"
                 * and converts it back to the two-dimensional coordinates row and column
                 */
                int moveIndex = availableMove[i];
                int row = moveIndex / 3;
                int col = moveIndex % 3;

                gameBoard.MakeMove(row, col);
                /* The Minimax method is called recursively
                 * with the false parameter to minimize
                 */
                int score = MiniMax(false);
                /*The computer's turn is canceled in order to return
                 * the game board to its original state for the next
                 * iteration of the iteration.
                 */
                gameBoard.UndoMove(row, col);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMoveIndex = moveIndex;
                }
            }
            return bestMoveIndex;
        }
        /* The isMaximizing parameter specifies whether the current analysis level
         * is maximizing (true) or minimizing (false).
         * In the MiniMax algorithm, the computer seeks to maximize its winnings,
         * and the player seeks to minimize his winnings.
         */
        private int MiniMax(bool isMaximizing)
        {
            if (gameBoard.IsGameOver())
            {
                // if the game is over, we call the evaluation of the current position 
                if (gameBoard.IsWinningMove(2)) // computer's winnings (O)
                {
                    /* If you win, a positive value of 10 is returned,
                     * since this is the best result for the computer.
                     */
                    return 10;
                }
                else if (gameBoard.IsWinningMove(1)) // player's winnings (X)
                {
                    /* If the player wins, a negative value of -10 is returned,
                     * since this is the worst result for the computer
                     */
                    return -10;
                }
                else
                    return 0;
            }
            if (isMaximizing) //looking for the best move for the computer
            {
                int bestScore = int.MinValue;
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (gameBoard.GetPlayerAtPosition(row, col) == 0) // is the cell empty (value 0), i.e. available for a move.
                        {
                            gameBoard.MakeMove(row, col);
                            /* The MiniMax() function is called recursively for the next move,
                             * passing false, since another player is now walking
                             */
                            int score = MiniMax(false);
                            gameBoard.UndoMove(row, col);

                            /* The maximum result is selected from all possible moves,
                             * as the computer strives to choose the best move available.
                             */
                            bestScore = Math.Max(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
            /* The algorithm considers all possible moves for the minimizing player (all empty cells on the board),
             * makes a recursive call to the MiniMax function for each of these moves
             * to calculate the results that can be achieved by the computer (maximizing player),
             * and then selects the move that will give the least result for the computer.
             */
            else
            {
                int bestScore = int.MaxValue;
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (gameBoard.GetPlayerAtPosition(row, col) == 0)
                        {
                            gameBoard.MakeMove(row, col);
                            int score = MiniMax(true);
                            gameBoard.UndoMove(row, col);

                            bestScore = Math.Min(score, bestScore);
                        }
                    }
                }
                return bestScore;
            }
        }
        #endregion




    }
}

