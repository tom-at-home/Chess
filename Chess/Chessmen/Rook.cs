using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    [Serializable()]
    class Rook : Chessman
    {

        public Rook(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {

            this.desc = "TURM";
            this.NotationCode = "T";

            if (isWhite)
            {
                this.color = "white";
            }
            else
            {
                this.color = "black";
            }

            this.SetView();

        }

        public void SetView()
        {
            StreamingContext context = new StreamingContext();
            this.OnDeserialized(context);
        }

        [OnDeserialized()]
        internal void OnDeserialized(StreamingContext context)
        {
            if (isWhite)
            {
                Image whiteRook = new Image();
                whiteRook.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/turm.png"));

                StackPanel whiteRookPnl = new StackPanel();
                whiteRookPnl.Orientation = Orientation.Horizontal;
                whiteRookPnl.Margin = new System.Windows.Thickness(8);
                whiteRookPnl.Children.Add(whiteRook);

                this.View = whiteRookPnl;

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

            }
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            return ((source_col == dest_col) || (source_row == dest_row));
        }

        // Prüft, ob der Zug durch andere Schachfiguren versperrt ist
        public override bool IsMoveBlocked(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);
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
