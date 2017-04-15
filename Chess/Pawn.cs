using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Pawn : Chessman
    {

        private bool isMoved = false;

        public Pawn(bool isWhite, string pos)
        {
            Image img = new Image();
            img.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer.png"));

            StackPanel stackPnl = new StackPanel();
            stackPnl.Orientation = Orientation.Horizontal;
            stackPnl.Margin = new System.Windows.Thickness(10);
            stackPnl.Children.Add(img);

            this.isWhite = isWhite;
            this.View = stackPnl;
            this.current_position = pos;
            this.desc = "BAUER";
        }

        public override void Move(MyButton source, MyButton dest)
        {
            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0,1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            if (this.isWhite)
            {
                if(source_col == dest_col && dest_row - source_row >= 1 && dest_row - source_row <= 2)
                {
                    if((dest_row - source_row == 1) || (dest_row - source_row == 2 && !isMoved))
                    {
                        source.Content = "";
                        this.current_position = GetFieldnameFromInt(dest_col, dest_row);
                        isMoved = true;
                    }
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else
                {
                    throw new InvalidMoveException();
                }
            }
        }

        private String GetFieldnameFromInt(int dest_col, int dest_row)
        {
            Char prefix = (Char)dest_col;
            string field_name = prefix + "" + dest_row;

            return field_name;
        }
    }
}
