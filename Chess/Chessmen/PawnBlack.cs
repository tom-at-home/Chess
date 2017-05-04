using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class PawnBlack : Chessman
    {

        private bool isMoved = false;

        public PawnBlack(string pos)
        {

            Image blackPawn = new Image();
            blackPawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer_sw.png"));

            StackPanel blackPawnPnl = new StackPanel();
            blackPawnPnl.Orientation = Orientation.Horizontal;
            blackPawnPnl.Margin = new System.Windows.Thickness(8);
            blackPawnPnl.Children.Add(blackPawn);

            this.IsWhite = false;

            this.View = blackPawnPnl;
            this.color = "black";

            this.Current_position = pos;
            this.desc = "BAUER";
        }

        public override bool IsMoveBlocked(Square dest)
        {

            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            for (int i = source_row - 1; i >= dest_row; i--)
            {
                if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
                {
                    return true;
                }
            }

            return false;
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            Chessman opponent = null;
            Chessman chessman = MainWindow.board.GetChessmanAtSquare(dest);
            if (chessman != null && chessman.Color != this.Color)
            {
                opponent = chessman;
            }

            if (!this.isMoved)
            {
                return
                    //  Der Bauer darf ein oder zwei Felder VERTIKAL vorrücken
                    (   source_col == dest_col && source_row - dest_row >= 1 && source_row - dest_row <= 2  ) ||
                    (
                    // oder ein Feld DIAGONAL den Gegner angreifen
                        ((source_col - 1 == dest_col) || (source_col + 1 == dest_col))
                          && (source_row - dest_row == 1)
                          && (opponent != null)
                    );
            }
            else
            {
                return
                    //  Der Bauer darf ein Feld VERTIKAL vorrücken
                    (source_col == dest_col && source_row - dest_row == 1    ) ||
                    // oder ein Feld DIAGONAL den Gegner angreifen
                    (
                        ((source_col - 1 == dest_col) || (source_col + 1 == dest_col))
                          && (source_row - dest_row == 1)
                          && (opponent != null)
                    );
            }
        }

        public override void Move(Square source, Square dest)
        {
            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0, 1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            if (IsMoveValid(dest))
            {
                // Gewoehnlicher Zug ohne Angriff
                if (source_col == dest_col)
                {
                    if (!IsMoveBlocked(dest))
                    {
                        source.Content = "";
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                        MainWindow.board.lastAction = "BEWEGE " + this.Desc
                                + " VON " + source.Name
                                + " AUF " + dest.Name;
                        isMoved = true;
                    }
                    else
                    {
                        throw new BlockedMoveException();
                    }

                }
                // Angriffszug
                else
                {
                    Chessman opponent = MainWindow.board.GetChessmanAtSquare(dest);

                    source.Content = "";
                    this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                    isMoved = true;
                    MainWindow.board.lastAction = this.Desc + " SCHLÄGT " + opponent.Desc + " AUF " + dest.Name;
                    MainWindow.board.chessman.Remove(opponent);
                }
            }
            else
            {
                throw new InvalidMoveException();
            }
        }
    }
}
