using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    [Serializable()]
    class Queen : Chessman
    {

        public Queen(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {

            this.desc = "DAME";
            this.NotationCode = "D";

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
            if (IsWhite)
            {
                Image whiteQueen = new Image();
                whiteQueen.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/dame.png"));

                StackPanel whiteQueenPnl = new StackPanel();
                whiteQueenPnl.Orientation = Orientation.Horizontal;
                whiteQueenPnl.Margin = new System.Windows.Thickness(8);
                whiteQueenPnl.Children.Add(whiteQueen);

                this.View = whiteQueenPnl;

            }
            else
            {
                Image blackQueen = new Image();
                blackQueen.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/dame_sw.png"));

                StackPanel blackQueenPnl = new StackPanel();
                blackQueenPnl.Orientation = Orientation.Horizontal;
                blackQueenPnl.Margin = new System.Windows.Thickness(8);
                blackQueenPnl.Children.Add(blackQueen);

                this.View = blackQueenPnl;

            }
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Prüft auf gültigen Zug: VERTIKAL oder HORIZONTAL oder DIAGONAL
            return ((source_col == dest_col) || (source_row == dest_row) || Math.Abs(source_col - dest_col) == Math.Abs(source_row - dest_row));
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
            if (source_col == dest_col)
            {
                if (CanMoveVertical(source, dest))
                {
                    return false;
                }
            }
            // Horizontaler Zug
            else if (source_row == dest_row)
            {
                if (CanMoveHorizontal(source, dest))
                {
                    return false;
                }
            }
            // Diagonaler Zug
            else if ((Math.Abs(source_col - dest_col)) == Math.Abs((source_row - dest_row)))
            {
                if (CanMoveDiagonal(source, dest))
                {
                    return false;
                }
            }

            return true;
        }
    }
}