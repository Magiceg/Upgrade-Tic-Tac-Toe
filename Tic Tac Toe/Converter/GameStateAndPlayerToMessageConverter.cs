using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Tic_Tac_Toe.Converter
{
    public class GameStateAndPlayerToMessageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2)
            {
                string currentGameState = values[0] as string;
                string currentPlayer = values[1] as string;

                if (currentGameState == "GameOver")
                {
                    if (currentPlayer == "PlayerX")
                        return "Winner: X";
                    else if (currentPlayer == "PlayerO")
                        return "Winner: O";
                    else
                        return "It's a draw";
                }
                else
                {
                    return currentPlayer; // Возвращаем currentPlayer напрямую, т.к. он уже содержит "Player: X" или "Player: O"
                }
            }
            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
