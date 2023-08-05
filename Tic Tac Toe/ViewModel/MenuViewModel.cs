using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tic_Tac_Toe.Commands;
using Tic_Tac_Toe.ViewModel.Base;

namespace Tic_Tac_Toe.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        
        public NavigationCommand NavigateToGamePage { get => new NavigationCommand(NavigateToPage, new Uri("Views/Pages/GamePage.xaml", UriKind.RelativeOrAbsolute)); }

        public NavigationCommand NavigateToGamePageVsComputer { get => new NavigationCommand(NavigateToPage, new Uri("Views/Pages/GamePageVsComputer.xaml", UriKind.RelativeOrAbsolute)); }
        public NavigationCommand NavigateToTestPage { get => new NavigationCommand(NavigateToPage, new Uri("Views/Pages/TestPage.xaml", UriKind.RelativeOrAbsolute)); }

        public MyRelayCommand QuitCommand => new MyRelayCommand(param => Application.Current.Shutdown());
    }
}
