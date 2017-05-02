using System;
using System.Windows.Controls;

namespace Chess
{
    abstract class Chessman
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

        public String GetSquarenameFromCoordinates(int col, int row)
        {
            Char prefix = (Char)col;
            string field_name = prefix + "" + row;

            return field_name;
        }

        public abstract void Move(Square source, Square dest);

        public int GetColumnCoordinate(Square square)
        {
            return Convert.ToInt16(Convert.ToChar(square.Name.Substring(0, 1)));
        }

        public int GetRowCoordinate(Square square)
        {
            return Convert.ToInt16(square.Name.Substring(1, 1));
        }

        public bool CanMoveVertical(Square source, Square dest)
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

        public bool CanMoveHorizontal(Square source, Square dest)
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

        public bool CanMoveDiagonal(Square source, Square dest)
        {
            int source_col = GetColumnCoordinate(source);
            int source_row = GetRowCoordinate(source);
            int dest_col = GetColumnCoordinate(dest);
            int dest_row = GetRowCoordinate(dest);

            // Diagonal - von links unten nach rechts oben 
            if ((source_row < dest_row) && (source_col < dest_col))
            {
                int i;
                int j = source_row +1;
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

    }
}
