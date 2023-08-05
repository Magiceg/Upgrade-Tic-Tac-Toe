using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Tic_Tac_Toe.ViewModel.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /* The event that will be used to notify
         * subscribers about changes in properties
         */
        public event PropertyChangedEventHandler PropertyChanged;

        //for generation event from "PropertyChanged"
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            /* If there are subs to the event, they will be notified of the 
             * of the property change and will receive info about its name
             */
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /* This method is used to set the value and call "OnPropertyChanged"
         * to notify about property changes
         */
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value; //setting the field value to the passed value "value"
            OnPropertyChanged(propertyName); //calling a method to notify about property change with the specified name
            return true; // indicates to successful execution of the set method 
        }

        /* This method is designed to navigate
         * to the specified uri in the specified page
         */
        protected static void NavigateToPage(Page page, Uri uri)
        {
            NavigationService.GetNavigationService(page).Navigate(uri);
        }
    }
}
