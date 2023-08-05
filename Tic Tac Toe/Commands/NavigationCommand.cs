using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tic_Tac_Toe.Commands.Base;

namespace Tic_Tac_Toe.Commands
{
    public class NavigationCommand : BaseCommand
    {
        private readonly Action<Page, Uri> _execute; // for store the method that will be execute when the navigation command is called
        private readonly Uri _uri; //the field that specifies the path

        /* This constructor is used to initialize fields
         * when creating a navigation command object 
         */
        public NavigationCommand(Action<Page, Uri> execute, Uri uri) //Uri is the path to a resourse 
        {
            _execute = execute;
            _uri = uri;
        }

        /* This method will be called when 
         * executing the navigation command
         */
        public override void Execute(object parameter)
        {
            /* This Conversion of the "(Page) parameter" to the "Page" type
             * allows you to pass the current page as a parameter for the 
             * navigation command
             */
            _execute.Invoke((Page)parameter, _uri);
        }
    }
}
