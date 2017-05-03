using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class King : Chessman
    {

        public King(bool isWhite, string pos)
        {

            this.IsWhite = isWhite;
            if (isWhite)
            {
                Image whiteKing = new Image();
                whiteKing.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/koenig.png"));

                StackPanel whiteKingPnl = new StackPanel();
                whiteKingPnl.Orientation = Orientation.Horizontal;
                whiteKingPnl.Margin = new System.Windows.Thickness(8);
                whiteKingPnl.Children.Add(whiteKing);

                this.View = whiteKingPnl;
                this.color = "white";
            }
            else
            {
                Image blackKing = new Image();
                blackKing.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/koenig_sw.png"));

                StackPanel blackKingPnl = new StackPanel();
                blackKingPnl.Orientation = Orientation.Horizontal;
                blackKingPnl.Margin = new System.Windows.Thickness(8);
                blackKingPnl.Children.Add(blackKing);

                this.View = blackKingPnl;
                this.color = "black";
            }

            this.Current_position = pos;
            this.desc = "KÖNIG";
        }

        public override void Move(Square source, Square dest)
        {

            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Prüft auf gültigen Zug: VERTIKAL oder HORIZONTAL oder DIAGONAL
            if (
                (source_col == dest_col && Math.Abs(source_row - dest_row) == 1) || 
                (source_row == dest_row && Math.Abs(source_col - dest_col) == 1) || 
                ((Math.Abs(source_col - dest_col) == Math.Abs(source_row - dest_row)) && (Math.Abs(source_col - dest_col) == 1))
               )
            {
                if (!PlacedInCheck(dest))
                {
                    // Gewoehnlicher Zug ohne Angriff
                    if (MainWindow.board.GetChessmanAtSquare(dest) == null)
                    {
                        source.Content = "";
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                        MainWindow.board.lastAction = "BEWEGE " + this.Desc
                                + " VON " + source.Name
                                + " AUF " + dest.Name;
                    }
                    else
                    {
                        Chessman chessmanAtDest = MainWindow.board.GetChessmanAtSquare(dest);
                        // Angriffszug
                        if (chessmanAtDest.Color != this.Color)
                        {
                            source.Content = "";
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            MainWindow.board.lastAction = this.Desc + " SCHLÄGT " + chessmanAtDest.Desc + " AUF " + dest.Name;
                            MainWindow.board.chessman.Remove(chessmanAtDest);
                        }
                        else
                        {
                            throw new BlockedMoveException();
                        }
                    }
                }
                else
                {
                    throw new PlacedInCheckException();
                }
            }
            else
            {
                throw new InvalidMoveException();
            }
        }

        public bool PlacedInCheck(Square dest)
        {

            foreach (Chessman chessman in MainWindow.board.chessman)
            {
                if(this.Color != chessman.Color)
                {

                }
            }

            return false;
        }

        /*
        // Prüft, ob der Zug durch andere Schachfiguren versperrt ist
        private bool IsBlocked(Square source, Square dest)
        {
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
        */

    }
}
