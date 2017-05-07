using Chess.Chessmen;
using System;
using System.Windows.Controls;

namespace Chess
{
    public abstract class Chessman : IMovable
    {
        private bool isWhite;
        protected bool IsWhite
        {
            get { return isWhite; }
            set { isWhite = value; }
        }

        protected string color;
        public string Color
        {
            get { return color; }
        }

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

        public Chessman(bool isWhite, string pos)
        {
            this.IsWhite = isWhite;
            this.orig_position = pos;
            this.Current_position = pos;
        }

        public abstract bool IsMoveValid(Square dest);

        public abstract bool IsMoveBlocked(Square dest);

        //public abstract void Move(Square source, Square dest);

        public virtual void Move(Square source, Square dest)
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
                    if (MainWindow.board.GetChessmanAtSquare(dest) == null)
                    {

                        string last_pos = this.Current_position;
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);

                        if (!MainWindow.board.IsKingInCheck(this.Color))
                        {
                            source.Content = "";
                            //this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            MainWindow.board.lastAction = "BEWEGE " + this.Desc
                                    + " VON " + source.Name
                                    + " AUF " + dest.Name;
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
                        Chessman chessmanAtDest = MainWindow.board.GetChessmanAtSquare(dest);
                        // Angriffszug
                        if (chessmanAtDest.Color != this.Color)
                        {

                            string last_pos = this.Current_position;
                            this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                            MainWindow.board.chessman.Remove(chessmanAtDest);

                            if (!MainWindow.board.IsKingInCheck(this.Color))
                            {
                                source.Content = "";
                                //this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
                                MainWindow.board.lastAction = this.Desc + " SCHLÄGT " + chessmanAtDest.Desc + " AUF " + dest.Name;

                            }
                            // Zug Rückgängig machen, wenn sich nach dem Zug
                            // der König in Schach befinden würde
                            else
                            {
                                this.Current_position = last_pos;
                                MainWindow.board.chessman.Add(chessmanAtDest);
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(i, dest_row))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(i, dest_row))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
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
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(i, j))) != null)
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
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col + 1, source_row + 2)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col + 2, source_row + 1)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col + 2, source_row - 1)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col + 1, source_row - 2)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col - 1, source_row - 2)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col - 2, source_row - 1)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col - 2, source_row + 1)) ||
                dest == MainWindow.board.GetSquare(GetSquarenameFromCoordinates(source_col - 1, source_row + 2))
                )
            {
                return true;
            }

            return false;
        }

        // Überprüft, ob sich der König nach diesem Zug in Schach befinden würde
        public bool IsMovePlacingKingInCheck(Square dest)
        {

            Square source = MainWindow.board.GetSquare(this.Current_position);
            Square kings_pos = MainWindow.board.GetKingsPosition(this.Color);
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

        //// Überprüft, ob sich der eigene König in Schach befindet
        //public bool IsOwnKingInCheck()
        //{
        //    Square kings_pos = MainWindow.board.GetKingsPosition(this.Color);

        //    foreach (Chessman chessman in MainWindow.board.chessman)
        //    {
        //        if (this.Color != chessman.Color)
        //        {
        //            if (chessman.IsMoveValid(kings_pos) && !chessman.IsMoveBlocked(kings_pos))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

    }
}
