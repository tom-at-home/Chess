using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    [Serializable()]
    class Bishop : Chessman
    {

        public Bishop(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {

            this.desc = "LÄUFER";
            this.NotationCode = "L";

            if (isWhite)
            {
                this.color = "white";
            }
            else
            {
                this.color = "black";
            }

            this.setView();

        }

        public void setView()
        {
            StreamingContext context = new StreamingContext();
            this.OnDeserialized(context);
        }

        [OnDeserialized()]
        internal void OnDeserialized(StreamingContext context)
        {
            if (IsWhite)
            {
                Image whiteBishop = new Image();
                whiteBishop.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/laufer.png"));

                StackPanel whiteBishopPnl = new StackPanel();
                whiteBishopPnl.Orientation = Orientation.Horizontal;
                whiteBishopPnl.Margin = new System.Windows.Thickness(8);
                whiteBishopPnl.Children.Add(whiteBishop);

                this.View = whiteBishopPnl;
            }
            else
            {
                Image blackBishop = new Image();
                blackBishop.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/laufer_sw.png"));

                StackPanel blackBishopPnl = new StackPanel();
                blackBishopPnl.Orientation = Orientation.Horizontal;
                blackBishopPnl.Margin = new System.Windows.Thickness(8);
                blackBishopPnl.Children.Add(blackBishop);

                this.View = blackBishopPnl;
            }
        }

        // Prüft, ob der Zug zulässig ist
        public override bool IsMoveValid(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            return (Math.Abs(source_col - dest_col)) == Math.Abs((source_row - dest_row));
        }

        // Prüft, ob der Zug durch andere Schachfiguren versperrt ist
        public override bool IsMoveBlocked(Square dest)
        {

            Square source = this.game.board.GetSquare(this.Current_position);

            // Diagonaler Zug
            if (CanMoveDiagonal(source, dest))
            {
                return false;
            }

            return true;
        }
    }
}
