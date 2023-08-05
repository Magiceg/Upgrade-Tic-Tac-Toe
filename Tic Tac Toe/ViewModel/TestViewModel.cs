using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Shapes;
using Tic_Tac_Toe.Commands;
using Tic_Tac_Toe.Model;
using Tic_Tac_Toe.ViewModel.Base;

namespace Tic_Tac_Toe.ViewModel
{
    // this class is needed to test any new fitch
    public class TestViewModel : BaseViewModel
    {
        #region Private Members
        private GameBoard gameBoard;
        private string currentPlayer;
        private string isDrawMessage;
        private string currentGameState;
        private bool isGameOver;
        private int[,] winningLine;

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
        public int[,] WinningLine
        {
            get { return winningLine; }
            set { Set(ref winningLine, value); }
        }

        #endregion

        public TestViewModel()
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

        private void ButtonCommandClick(object parameter)
        {
            Button clickedButton = parameter as Button;
            if (clickedButton != null)
            {
                int row = Grid.GetRow(clickedButton);
                int col = Grid.GetColumn(clickedButton);

                try
                {
                    gameBoard.MakeMove(row, col);
                    int player = gameBoard.GetPlayerAtPosition(row, col);
                    string symbols = (player == 1) ? "X" : "O";
                    Board[row][col] = symbols;

                    isGameOver = gameBoard.IsGameOver();
                    if (isGameOver)
                    {
                        if (gameBoard.IsWinningMove(1))
                        {
                            CurrentPlayer = "PlayerX";
                            WinningLine = gameBoard.GetWinningCombination(1);
                        }
                        else if (gameBoard.IsWinningMove(2))
                        {
                            CurrentPlayer = "PlayerO";
                            WinningLine = gameBoard.GetWinningCombination(2);
                        }
                        else
                        {
                            IsDrawMessage = "It's a draw";
                        }

                        CurrentGameState = "GameOver";
                        IsGameOver = true;

                    }
                    else
                    {
                        CurrentPlayer = (player == 1) ? "Player: O" : "Player: X";
                        CurrentGameState = "Playing";
                    }
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

        }
    }
}
