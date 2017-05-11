using System;
using System.Runtime.Serialization;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{

    [Serializable()]
    public class PawnWhite : Pawn
    {

        public PawnWhite(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {

            Image whitePawn = new Image();
            whitePawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer.png"));

            StackPanel whitePawnPnl = new StackPanel();
            whitePawnPnl.Orientation = Orientation.Horizontal;
            whitePawnPnl.Margin = new System.Windows.Thickness(8);
            whitePawnPnl.Children.Add(whitePawn);

            this.View = whitePawnPnl;
            this.color = "white";

        }

        [OnDeserialized()]
        internal void OnDeserialized(StreamingContext context)
        {
            Image whitePawn = new Image();
            whitePawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer.png"));

            StackPanel whitePawnPnl = new StackPanel();
            whitePawnPnl.Orientation = Orientation.Horizontal;
            whitePawnPnl.Margin = new System.Windows.Thickness(8);
            whitePawnPnl.Children.Add(whitePawn);

            this.View = whitePawnPnl;

        }

        public override bool IsMoveBlocked(Square dest)
        {

            Square source = this.game.board.GetSquare(this.Current_position);
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            for (int i = source_row + 1; i <= dest_row; i++)
            {
                if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
                {
                    return true;
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

            Chessman opponent = null;
            Chessman chessman = this.game.board.GetChessmanAtSquare(dest);
            if (chessman != null && chessman.Color != this.Color)
            {
                opponent = chessman;
            }

            if (!this.isMoved)
            {
                return
                    //  Der Bauer darf ein oder zwei Felder VERTIKAL vorrücken
                    (source_col == dest_col && dest_row - source_row >= 1 && dest_row - source_row <= 2) ||
                    (
                        // oder ein Feld DIAGONAL den Gegner angreifen
                        ((source_col - 1 == dest_col) || (source_col + 1 == dest_col))
                          && (dest_row - source_row == 1)
                          && (opponent != null)
                    );
            }
            else
            {
                return
                    //  Der Bauer darf ein Feld VERTIKAL vorrücken
                    (source_col == dest_col && dest_row - source_row == 1) ||
                    // oder ein Feld DIAGONAL den Gegner schlagen
                    (
                        ((source_col - 1 == dest_col) || (source_col + 1 == dest_col))
                          && (dest_row - source_row == 1)
                          && (opponent != null)
                    ) ||
                    // oder einen gegnerischen Bauer en passant schlagen
                    (
                        ((source_col - 1 == dest_col) || (source_col + 1 == dest_col))
                          && (dest_row - source_row == 1)
                          && (source_row == 5)
                          && (this.game.GetWaitingPlayer().DoubleStepMovedPawn != null)
                          && (Convert.ToInt16(this.game.GetWaitingPlayer().DoubleStepMovedPawn.Current_position.Substring(1, 1)) == 5)
                          && (Convert.ToInt16(Convert.ToChar(this.game.GetWaitingPlayer().DoubleStepMovedPawn.Current_position.Substring(0, 1))) == dest_col)
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
                        string last_pos = this.Current_position;
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);

                        if (!this.game.board.IsKingInCheck(this.Color))
                        {
                            source.Content = "";
                            this.game.board.lastAction = "BEWEGE " + this.Desc
                                    + " VON " + source.Name
                                    + " AUF " + dest.Name;
                            isMoved = true;

                            // Neuer Logeintrag wird initialisiert
                            LogEntry log = new LogEntry(this, last_pos, this.Current_position);

                            // Zieht ein Bauer im Doppelschritt,
                            // kann dieser unmittelbar danach 'en passant' geschlagen werden
                            if (Math.Abs(source_row - dest_row) == 2)
                            {
                                this.game.GetActivePlayer().DoubleStepMovedPawn = this;
                            }
                            // Zieht ein Bauer auf die letzte Linie, kann er umgewandelt werden
                            if (dest_row == 8)
                            {
                                // Ein modales Fenster zur Auswahl der neuen Figur wird geöffnet
                                PromotePawn();
                                // Der Logeintrag wird um die umgewandelte Figur ergänzt
                                log.PromotedIn = this.promotedIn;
                            }
                            log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.GetWaitingPlayer().Color);
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
                    else
                    {
                        throw new BlockedMoveException();
                    }

                }
                // Angriffszug
                else
                {
                    Chessman opponent = this.game.board.GetChessmanAtSquare(dest);
                    string last_pos = this.Current_position;

                    // Standardangriff
                    if (opponent != null)
                    {
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                        this.game.board.chessman.Remove(opponent);

                        if (!this.game.board.IsKingInCheck(this.Color))
                        {
                            source.Content = "";
                            isMoved = true;
                            this.game.board.lastAction = this.Desc + " SCHLÄGT " + opponent.Desc + " AUF " + dest.Name;

                            // Neuer Logeintrag wird initialisiert
                            LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                            log.OpponentMan = opponent;

                            // Zieht ein Bauer auf die letzte Linie, kann er umgewandelt werden
                            if (dest_row == 8)
                            {
                                PromotePawn();
                                log.PromotedIn = this.promotedIn;
                            }

                            log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.GetWaitingPlayer().Color);
                            this.game.logger.Add(log);
                            this.game.View.movesList.Items.Add(log);

                        }
                        // Zug Rückgängig machen, wenn sich nach dem Zug
                        // der König in Schach befinden würde
                        else
                        {
                            this.Current_position = last_pos;
                            this.game.board.chessman.Add(opponent);
                            throw new PlacedInCheckException();
                        }

                    }
                    // En Passant Angriff
                    else
                    {
                        string opponent_curr_pos = GetSquarenameFromCoordinates(dest_col, 5);
                        Square opponent_pos_square = this.game.board.GetSquare(opponent_curr_pos);
                        opponent = this.game.board.GetChessmanAtSquare(opponent_pos_square);

                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                        this.game.board.chessman.Remove(opponent);

                        if (!this.game.board.IsKingInCheck(this.Color))
                        {
                            source.Content = "";
                            opponent_pos_square.Content = "";
                            isMoved = true;
                            this.game.board.lastAction = this.Desc + " SCHLÄGT " + opponent.Desc + " AUF " + dest.Name + " IM VORBEIGEHEN (EN PASSANT)";

                            // Neuer Logeintrag
                            LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                            log.OpponentMan = opponent;
                            log.TookEnPassant = true;
                            log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.GetWaitingPlayer().Color);
                            this.game.logger.Add(log);
                            this.game.View.movesList.Items.Add(log);
                        }
                        // Zug Rückgängig machen, wenn sich nach dem Zug
                        // der König in Schach befinden würde
                        else
                        {
                            this.Current_position = last_pos;
                            this.game.board.chessman.Add(opponent);
                            throw new PlacedInCheckException();
                        }
                    }
                }
            }
            else
            {
                throw new InvalidMoveException();
            }
        }
    }
}
