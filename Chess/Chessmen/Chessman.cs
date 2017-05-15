using Chess.Chessmen;
using System;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace Chess
{
    [Serializable()]
    public abstract class Chessman : IMovable
    {

        protected Game game;
        public Game Game
        {
            get { return game; }
            set { game = value; }
        }

        protected bool isWhite;
        public bool IsWhite
        {
            get { return isWhite; }
            set { isWhite = value; }
        }

        protected string color;
        public string Color
        {
            get { return color; }
        }

        [NonSerialized]
        private StackPanel view;        
        public StackPanel View
        {
            get { return view; }
            set { view = value; }
        }

        protected string orig_position;
        public string Orig_position
        {
            get { return orig_position; }
        }

        private string current_position;
        public string Current_position
        {
            get { return current_position; }
            set { current_position = value; }
        }

        protected string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        protected string notationCode;
        public string NotationCode
        {
            get { return notationCode; }
            set { notationCode = value; }
        }

        public Chessman(bool isWhite, string pos, Game game)
        {
            this.IsWhite = isWhite;
            this.orig_position = pos;
            this.Current_position = pos;
            this.game = game;
        }

        public abstract bool IsMoveValid(Square dest);

        public abstract bool IsMoveBlocked(Square dest);

        //public abstract void Move(Square source, Square dest);

        public virtual void Move(Square source, Square dest, bool silentMode = false)
        {

            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Prüft auf gültigen Zug: VERTIKAL oder HORIZONTAL oder DIAGONAL
            if (IsMoveValid(dest))
            {
                if (!IsMoveBlocked(dest))
                {
                    // Gewoehnlicher Zug ohne Angriff
                    if (this.game.board.GetChessmanAtSquare(dest) == null)
                    {

                        string last_pos = this.Current_position;
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);

                        if (!this.game.board.IsKingInCheck(this.Color))
                        {

                            if (!silentMode)
                            {
                                source.Content = "";

                                this.game.board.lastAction = "BEWEGE " + this.Desc
                                        + " VON " + source.Name
                                        + " AUF " + dest.Name;

                                // Neuer Logeintrag
                                LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                                log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.GetWaitingPlayer().Color);
                                this.game.logger.Add(log);
                                this.game.View.movesList.Items.Add(log);
                            }

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
                        Chessman chessmanAtDest = this.game.board.GetChessmanAtSquare(dest);
                        // Angriffszug
                        if (chessmanAtDest.Color != this.Color)
                        {

                            string last_pos = this.Current_position;
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            this.game.board.chessman.Remove(chessmanAtDest);

                            if (!this.game.board.IsKingInCheck(this.Color))
                            {
                                if (!silentMode)
                                {
                                    source.Content = "";
                                    //this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                                    this.game.board.lastAction = this.Desc + " SCHLÄGT " + chessmanAtDest.Desc + " AUF " + dest.Name;

                                    // Neuer Logeintrag
                                    LogEntry log = new LogEntry(this, last_pos, this.Current_position);
                                    log.OpponentMan = chessmanAtDest;
                                    log.PlacedInCheck = this.game.board.IsKingInCheck(this.game.GetWaitingPlayer().Color);
                                    this.game.logger.Add(log);
                                    this.game.View.movesList.Items.Add(log);
                                }

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

        public String GetSquarenameFromCoordinates(int col, int row)
        {
            Char prefix = (Char)col;
            string field_name = prefix + "" + row;

            return field_name;
        }

        public int GetColumnCoordinate(Square square)
        {
            return Convert.ToInt16(Convert.ToChar(square.Name.Substring(0, 1)));
        }

        public int GetRowCoordinate(Square square)
        {
            return Convert.ToInt16(square.Name.Substring(1, 1));
        }

        protected bool CanMoveVertical(Square source, Square dest)
        {
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Vertikal - von unten nach oben
            if (source_row < dest_row)
            {
                for (int i = source_row + 1; i < dest_row; i++)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
                    {
                        return false;
                    }
                }
            }
            // Vertikal - von oben nach unten
            else if (source_row > dest_row)
            {
                for (int i = source_row - 1; i > dest_row; i--)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
                    {
                        return false;
                    }
                }
            }
            // Der Weg ist frei
            return true;
        }

        protected bool CanMoveHorizontal(Square source, Square dest)
        {
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Horizontal - von links nach rechts
            if (source_col < dest_col)
            {
                for (int i = source_col + 1; i < dest_col; i++)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(i, dest_row))) != null)
                    {
                        return false;
                    }
                }
            }
            // Horizontal - von rechts nach links
            else if (source_col > dest_col)
            {
                for (int i = source_col - 1; i > dest_col; i--)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(i, dest_row))) != null)
                    {
                        return false;
                    }
                }
            }
            // Der Weg ist frei
            return true;
        }

        protected bool CanMoveDiagonal(Square source, Square dest)
        {
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Diagonal - von links unten nach rechts oben 
            if ((source_row < dest_row) && (source_col < dest_col))
            {
                int i;
                int j = source_row + 1;
                for (i = source_col + 1; i < dest_col; i++, j++)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
                    {
                        return false;
                    }
                }
            }
            // Diagonal - von rechts oben nach links unten
            else if ((source_row > dest_row) && (source_col > dest_col))
            {
                int i;
                int j = source_row - 1;
                for (i = source_col - 1; i > dest_col; i--, j--)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
                    {
                        return false;
                    }
                }
            }
            // Diagonal - von rechts unten nach links oben
            else if ((source_row < dest_row) && (source_col > dest_col))
            {
                int i;
                int j = source_row + 1;
                for (i = source_col - 1; i > dest_col; i--, j++)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
                    {
                        return false;
                    }
                }
            }
            // Diagonal - von links oben nach rechts unten 
            else if ((source_row > dest_row) && (source_col < dest_col))
            {
                int i;
                int j = source_row - 1;
                for (i = source_col + 1; i < dest_col; i++, j--)
                {
                    if (this.game.board.GetChessmanAtSquare(this.game.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
                    {
                        return false;
                    }
                }
            }
            // Der Weg ist frei
            return true;
        }

        protected bool CanJump(Square source, Square dest)
        {
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            if (
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col + 1, source_row + 2)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col + 2, source_row + 1)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col + 2, source_row - 1)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col + 1, source_row - 2)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col - 1, source_row - 2)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col - 2, source_row - 1)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col - 2, source_row + 1)) ||
                dest == this.game.board.GetSquare(GetSquarenameFromCoordinates(source_col - 1, source_row + 2))
                )
            {
                return true;
            }

            return false;
        }

        // Überprüft, ob sich der König nach diesem Zug in Schach befinden würde
        public bool IsMovePlacingKingInCheck(Square dest)
        {

            Square source = this.game.board.GetSquare(this.Current_position);
            Square kings_pos = this.game.board.GetKingsPosition(this.Color);
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
                    if (chessman.IsMoveValid(kings_pos) && !chessman.IsMoveBlocked(kings_pos))
                    {
                        this.Current_position = last_pos;
                        return true;
                    }
                }
            }

            this.Current_position = last_pos;
            return false;
        }
    }
}
