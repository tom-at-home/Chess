using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{
    class Square : Button
    {

        private SolidColorBrush color;
        public SolidColorBrush Color
        {
            get { return color; }
            set { color = value; }
        }

    }
}
