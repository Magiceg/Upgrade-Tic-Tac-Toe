using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tic_Tac_Toe.Views.Pages;

namespace Tic_Tac_Toe.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для GameMainWindow.xaml
    /// </summary>
    public partial class GameMainWindow : Window
    {
        public GameMainWindow()
        {
            InitializeComponent();
            LoadMenuPage();
        }

        private void LoadMenuPage()
        {
            MainFrame.Navigate(new MenuPage());
        }
    }
}
