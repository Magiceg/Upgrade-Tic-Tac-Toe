using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_Tac_Toe.Commands.Base;

namespace Tic_Tac_Toe.Commands
{
    public class MyRelayCommand : BaseCommand
    {
        private readonly Action<object> execute;
       

        public MyRelayCommand(Action<object> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public override void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
