using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tic_Tac_Toe.Model
{
    public class LineDrawing
    {
        private Canvas _canvas;
        private Line _winningLine;

        public LineDrawing(Canvas canvas)
        {
            _canvas = canvas;
        }
        public void DrawLine(double startX, double startY, double endX, double endY)
        {
            if (_winningLine != null)
                _canvas.Children.Remove(_winningLine);


            _winningLine = new Line()
            {
                X1 = startX,
                Y1 = startY,
                X2 = endX,
                Y2 = endY,
                Stroke = Brushes.DimGray,
                StrokeThickness = 5
            };

            _canvas.Children.Add(_winningLine);

        }
        public void ClearLine()
        {
            if (_winningLine != null)
                _canvas.Children.Remove(_winningLine); // Remove the winning line if it exists

            _winningLine = null;
        }
    }
}
