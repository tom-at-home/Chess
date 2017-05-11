using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{

    public class Square : Button
    {

        private SolidColorBrush color;
        public SolidColorBrush Color
        {
            get { return color; }
            set { color = value; }
        }

    }
}
