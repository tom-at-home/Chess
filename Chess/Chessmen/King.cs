using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class King : Chessman
    {

        private bool isMoved = false;

        // Speichert, ob der König bereits eine Rochade ausgeführt hat
        private bool HasPerformedCastling;

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

        public bool CheckCastlingKingsSide()
        {
            return true;
        }

        public bool CheckCastlingQueensSide()
        {
            return true;
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = MainWindow.board.GetSquare(this.Current_position);

            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Prüft auf gültigen Zug: VERTIKAL oder HORIZONTAL oder DIAGONAL
            // bei einer Zugweite von genau einem Feld
            return (
                (source_col == dest_col && Math.Abs(source_row - dest_row) == 1) ||
                (source_row == dest_row && Math.Abs(source_col - dest_col) == 1) ||
                ((Math.Abs(source_col - dest_col) == Math.Abs(source_row - dest_row)) && (Math.Abs(source_col - dest_col) == 1)) ||
                CheckCastlingKingsSide() ||
                CheckCastlingQueensSide()
               );
        }

        public override bool IsMoveBlocked(Square dest)
        {
            return false;
        }

        public bool IsMovePlacingInCheck(Square dest)
        {

            foreach (Chessman chessman in MainWindow.board.chessman)
            {
                if (this.Color != chessman.Color)
                {
                    if(chessman.IsMoveValid(dest) && !chessman.IsMoveBlocked(dest))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void Move(Square source, Square dest)
        {

            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Prüft auf gültigen Zug: VERTIKAL oder HORIZONTAL oder DIAGONAL
            // bei einer Reichweite von einem Feld
            if (IsMoveValid(dest))
            {
                if (!IsMoveBlocked(dest))
                {
                    if (!IsMovePlacingInCheck(dest))
                    {
                        // Gewoehnlicher Zug ohne Angriff
                        if (MainWindow.board.GetChessmanAtSquare(dest) == null)
                        {
                            source.Content = "";
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            this.isMoved = true;
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
                                this.isMoved = true;
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
