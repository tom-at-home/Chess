using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Bishop : Chessman
    {

        public Bishop(bool isWhite, string pos)
        {

            this.IsWhite = isWhite;
            if (isWhite)
            {
                Image whiteBishop = new Image();
                whiteBishop.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/laufer.png"));

                StackPanel whiteBishopPnl = new StackPanel();
                whiteBishopPnl.Orientation = Orientation.Horizontal;
                whiteBishopPnl.Margin = new System.Windows.Thickness(8);
                whiteBishopPnl.Children.Add(whiteBishop);

                this.View = whiteBishopPnl;
                this.color = "white";
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
                this.color = "black";
            }

            this.Current_position = pos;
            this.desc = "LÄUFER";
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            return (Math.Abs(source_col - dest_col)) == Math.Abs((source_row - dest_row));
        }

        // Prüft, ob der Zug durch andere Schachfiguren versperrt ist
        public override bool IsMoveBlocked(Square dest)
        {

            Square source = MainWindow.board.GetSquare(this.Current_position);

            // Diagonaler Zug
            if (CanMoveDiagonal(source, dest))
            {
                return false;
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
