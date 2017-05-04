using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class King : Chessman
    {

        private bool isMoved = false;

        // Speichert, ob der König bereits eine Rochade ausgeführt hat
        private bool HasPerformedCastling = false;

        public King(bool isWhite, string pos) : base(isWhite, pos)
        {

            this.desc = "KÖNIG";

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

        }

        // Prüft, ob die kleine Rochade möglich ist
        public bool CanPerformCastlingKingsSide(Square dest)
        {

            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Bewegung bei der kleinen Rochade
            if (source_col - dest_col == -2)
            {

                Rook rook = MainWindow.board.GetRook("H" + this.Orig_position.Substring(1, 1));
                if(rook != null && !rook.IsMoved && !this.isMoved && !this.HasPerformedCastling)
                {
                    Square orig_rook_pos = MainWindow.board.GetSquare(rook.Orig_position);
                    if (CanMoveHorizontal(source, orig_rook_pos))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        // Prüft, ob die große Rochade möglich ist
        public bool CanPerformCastlingQueensSide()
        {
            return false;
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
                CanPerformCastlingKingsSide(dest) 
                /*|| CheckCastlingQueensSide()*/
               );
        }

        public override bool IsMoveBlocked(Square dest)
        {
            return false;
        }

        // überprüft, ob sich der König nach diesem Zug selbst in Schach setzen würde
        public bool IsMovePlacingInCheck(Square dest)
        {

            Square source = MainWindow.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            string last_pos = this.Current_position;
            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);

            foreach (Chessman chessman in MainWindow.board.chessman)
            {
                if (this.Color != chessman.Color)
                {
                    if(chessman.IsMoveValid(dest) && !chessman.IsMoveBlocked(dest))
                    {
                        this.Current_position = last_pos;
                        return true;
                    }
                }
            }

            this.Current_position = last_pos;
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
                            // Kleine Rochade
                            if (source_col - dest_col == -2)
                            {
                                Rook rook = MainWindow.board.GetRook("H" + this.Orig_position.Substring(1, 1));
                                Square rook_source = MainWindow.board.GetSquare(rook.Orig_position);
                                source.Content = "";
                                rook_source.Content = "";
                                this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                                rook.Current_position = GetSquarenameFromCoordinates(dest_col-1, dest_row);
                                this.isMoved = true;
                                this.HasPerformedCastling = true;
                                rook.IsMoved = true;
                                MainWindow.board.lastAction = this.Desc + " FÜHRT DIE KLEINE ROCHADE"
                                        + " VON " + source.Name
                                        + " AUF " + dest.Name
                                        + " AUS";
                            }
                            else
                            {
                                source.Content = "";
                                this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                                this.isMoved = true;
                                MainWindow.board.lastAction = "BEWEGE " + this.Desc
                                        + " VON " + source.Name
                                        + " AUF " + dest.Name;
                            }
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
