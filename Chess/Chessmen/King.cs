using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    [Serializable()]
    class King : Chessman
    {

        private bool isMoved = false;

        // Speichert, ob der König bereits eine Rochade ausgeführt hat
        private bool HasPerformedCastling = false;

        public King(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {

            this.desc = "KÖNIG";
            this.NotationCode = "K";

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
                Image whiteKing = new Image();
                whiteKing.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/koenig.png"));

                StackPanel whiteKingPnl = new StackPanel();
                whiteKingPnl.Orientation = Orientation.Horizontal;
                whiteKingPnl.Margin = new System.Windows.Thickness(8);
                whiteKingPnl.Children.Add(whiteKing);

                this.View = whiteKingPnl;
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
            }
        }

        // Prüft, ob die kleine Rochade möglich ist
        public bool CanPerformCastlingKingsSide(Square dest)
        {

            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Bewegung bei der kleinen Rochade
            if (source_col - dest_col == -2 && source_row == dest_row)
            {

                Rook rook = this.game.board.GetRook("H" + this.Orig_position.Substring(1, 1));
                if (rook != null && !rook.IsMoved && !this.isMoved && !this.HasPerformedCastling)
                {
                    Square orig_rook_pos = this.game.board.GetSquare(rook.Orig_position);
                    if (CanMoveHorizontal(source, orig_rook_pos))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        // Prüft, ob die große Rochade möglich ist
        public bool CanPerformCastlingQueensSide(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Bewegung bei der großen Rochade
            if (source_col - dest_col == 2 && source_row == dest_row)
            {

                Rook rook = this.game.board.GetRook("A" + this.Orig_position.Substring(1, 1));
                if (rook != null && !rook.IsMoved && !this.isMoved && !this.HasPerformedCastling)
                {
                    Square orig_rook_pos = this.game.board.GetSquare(rook.Orig_position);
                    if (CanMoveHorizontal(source, orig_rook_pos))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        public override bool IsMoveValid(Square dest)
        {
            Square source = this.game.board.GetSquare(this.Current_position);

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
                CanPerformCastlingKingsSide(dest) ||
                CanPerformCastlingQueensSide(dest)
               );
        }

        public override bool IsMoveBlocked(Square dest)
        {
            return false;
        }

        // überprüft, ob sich der König nach diesem Zug selbst in Schach setzen würde
        public bool IsMovePlacingInCheck(Square dest)
        {

            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            string last_pos = this.Current_position;
            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);

            foreach (Chessman chessman in this.game.board.chessman)
            {
                if (this.Color != chessman.Color)
                {
                    if (chessman.IsMoveValid(dest) && !chessman.IsMoveBlocked(dest))
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
                    if (this.game.board.GetChessmanAtSquare(dest) == null)
                    {
                        // Kleine Rochade
                        if (source_col - dest_col == -2 && source_row == dest_row)
                        {
                            Rook rook = this.game.board.GetRook("H" + this.Orig_position.Substring(1, 1));
                            Square rook_source = this.game.board.GetSquare(rook.Orig_position);

                            string last_pos = this.Current_position;
                            string rook_last_pos = rook.Current_position;
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            rook.Current_position = GetSquarenameFromCoordinates(dest_col - 1, dest_row);

                            if (!this.game.board.IsKingInCheck(this.Color))
                            {
                                source.Content = "";
                                rook_source.Content = "";
                                this.isMoved = true;
                                this.HasPerformedCastling = true;
                                rook.IsMoved = true;
                                this.game.board.lastAction = this.Desc + " FÜHRT DIE KLEINE ROCHADE"
                                        + " VON " + source.Name
                                        + " AUF " + dest.Name
                                        + " AUS";

                                // Neuer Logeintrag
                                LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                                log.PerformedCastlingKingsSide = true;
                                log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.waiting_player.Color);
                                this.game.logger.Add(log);
                                this.game.View.movesList.Items.Add(log);
                            }
                            // Zug Rückgängig machen, wenn sich nach dem Zug
                            // der König in Schach befinden würde
                            else
                            {
                                this.Current_position = last_pos;
                                rook.Current_position = rook_last_pos;
                                throw new PlacedInCheckException();
                            }

                        }
                        // Große Rochade
                        else if (source_col - dest_col == 2 && source_row == dest_row)
                        {
                            Rook rook = this.game.board.GetRook("A" + this.Orig_position.Substring(1, 1));
                            Square rook_source = this.game.board.GetSquare(rook.Orig_position);

                            string last_pos = this.Current_position;
                            string rook_last_pos = rook.Current_position;
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            rook.Current_position = GetSquarenameFromCoordinates(dest_col + 1, dest_row);

                            if (!this.game.board.IsKingInCheck(this.Color))
                            {
                                source.Content = "";
                                rook_source.Content = "";
                                this.isMoved = true;
                                this.HasPerformedCastling = true;
                                rook.IsMoved = true;
                                this.game.board.lastAction = this.Desc + " FÜHRT DIE GROßE ROCHADE"
                                        + " VON " + source.Name
                                        + " AUF " + dest.Name
                                        + " AUS";

                                // Neuer Logeintrag
                                LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                                log.PerfomedCastlingQueensSide = true;
                                log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.waiting_player.Color);
                                this.game.logger.Add(log);
                                this.game.View.movesList.Items.Add(log);

                            }
                            // Zug Rückgängig machen, wenn sich nach dem Zug
                            // der König in Schach befinden würde
                            else
                            {
                                this.Current_position = last_pos;
                                rook.Current_position = rook_last_pos;
                                throw new PlacedInCheckException();
                            }
                        }
                        // Einfacher Zug
                        else
                        {
                            string last_pos = this.Current_position;
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);

                            if (!this.game.board.IsKingInCheck(this.Color))
                            {
                                source.Content = "";
                                this.isMoved = true;
                                this.game.board.lastAction = "BEWEGE " + this.Desc
                                        + " VON " + source.Name
                                        + " AUF " + dest.Name;

                                // Neuer Logeintrag
                                LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                                log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.waiting_player.Color);
                                this.game.logger.Add(log);
                                this.game.View.movesList.Items.Add(log);
                            }
                            // Zug Rückgängig machen, wenn sich nach dem Zug
                            // der König in Schach befinden würde
                            else
                            {
                                this.Current_position = last_pos;
                                throw new PlacedInCheckException();
                            }
                        }
                    }
                    else
                    {
                        Chessman chessmanAtDest = this.game.board.GetChessmanAtSquare(dest);
                        // Angriffszug
                        if (chessmanAtDest.Color != this.Color)
                        {
                            string last_pos = this.Current_position;
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            this.game.board.chessman.Remove(chessmanAtDest);

                            if (!this.game.board.IsKingInCheck(this.Color))
                            {
                                source.Content = "";
                                this.isMoved = true;
                                this.game.board.lastAction = this.Desc + " SCHLÄGT " + chessmanAtDest.Desc + " AUF " + dest.Name;

                                // Neuer Logeintrag
                                LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                                log.OpponentMan = chessmanAtDest;
                                log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.waiting_player.Color);
                                this.game.logger.Add(log);
                                this.game.View.movesList.Items.Add(log);

                            }
                            // Zug Rückgängig machen, wenn sich nach dem Zug
                            // der König in Schach befinden würde
                            else
                            {
                                this.Current_position = last_pos;
                                this.game.board.chessman.Add(chessmanAtDest);
                                throw new PlacedInCheckException();
                            }
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
