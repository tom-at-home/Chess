using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Rook : Chessman
    {

        public Rook(bool isWhite, string pos)
        {

            this.IsWhite = isWhite;
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

            this.Current_position = pos;
            this.desc = "TURM";
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

        public override void Move(Square source, Square dest)
        {
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            if (IsMoveValid(dest))
            {
                if (!IsMoveBlocked(dest))
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
                    throw new BlockedMoveException();
                }
            }
            else
            {
                throw new InvalidMoveException();
            }
        }
    }
}
