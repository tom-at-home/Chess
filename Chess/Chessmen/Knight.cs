using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    [Serializable()]
    class Knight : Chessman
    {

        public Knight(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {

            this.desc = "SPRINGER";
            this.NotationCode = "S";

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
                Image whiteKnight = new Image();
                whiteKnight.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/springer.png"));

                StackPanel whiteKnightPnl = new StackPanel();
                whiteKnightPnl.Orientation = Orientation.Horizontal;
                whiteKnightPnl.Margin = new System.Windows.Thickness(8);
                whiteKnightPnl.Children.Add(whiteKnight);

                this.View = whiteKnightPnl;

            }
            else
            {
                Image blackKnight = new Image();
                blackKnight.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/springer_sw.png"));

                StackPanel blackKnightPnl = new StackPanel();
                blackKnightPnl.Orientation = Orientation.Horizontal;
                blackKnightPnl.Margin = new System.Windows.Thickness(8);
                blackKnightPnl.Children.Add(blackKnight);

                this.View = blackKnightPnl;

            }
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Prüft auf gültigen Zug
            return CanJump(source, dest);
        }

        public override bool IsMoveBlocked(Square dest)
        {
            return false;
        }
    }
}
