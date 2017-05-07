using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    class Rook : Chessman
    {

        private bool isMoved;
        public bool IsMoved
        {
            get { return isMoved; }
            set { isMoved = value; }
        }

        public Rook(bool isWhite, string pos) : base(isWhite, pos)
        {

            this.desc = "TURM";

            if (isWhite)
            {
                Image whiteRook = new Image();
                whiteRook.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/turm.png"));

                StackPanel whiteRookPnl = new StackPanel();
                whiteRookPnl.Orientation = Orientation.Horizontal;
                whiteRookPnl.Margin = new System.Windows.Thickness(8);
                whiteRookPnl.Children.Add(whiteRook);

                this.View = whiteRookPnl;
                this.color = "white";
            }
            else
            {
                Image blackRook = new Image();
                blackRook.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/turm_sw.png"));

                StackPanel blackRookPnl = new StackPanel();
                blackRookPnl.Orientation = Orientation.Horizontal;
                blackRookPnl.Margin = new System.Windows.Thickness(8);
                blackRookPnl.Children.Add(blackRook);

                this.View = blackRookPnl;
                this.color = "black";
            }

        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            return ((source_col == dest_col) || (source_row == dest_row));
        }

        // Prüft, ob der Zug durch andere Schachfiguren versperrt ist
        public override bool IsMoveBlocked(Square dest)
        {
            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Vertikaler Zug
            if (source_row != dest_row)
            {
                if (CanMoveVertical(source, dest))
                {
                    return false;
                }
            }
            // Horizontaler Zug
            else if (source_col != dest_col)
            {
                if (CanMoveHorizontal(source, dest))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
