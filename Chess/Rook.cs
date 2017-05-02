using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Rook : Chessman
    {

        private bool isMoved = false;

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

        public override void Move(MyButton source, MyButton dest)
        {
            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0, 1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            //if (this.IsWhite)
            //{
            if ((source_col == dest_col) || (source_row == dest_row))
            {
                if (!isBlocked(source, dest))
                {
                    source.Content = "";
                    this.Current_position = GetFieldnameFromInt(dest_col, dest_row);
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
            else
            {
                throw new InvalidMoveException();
            }

            //else if (((source_col-1 == dest_col) || (source_col + 1 == dest_col)) && (dest_row - source_row == 1))
            //{
            //    Chessman opponent = MainWindow.board.GetChessmanAtField(dest);
            //    if (opponent != null && opponent.Color != this.Color)
            //    {
            //        source.Content = "";
            //        this.Current_position = GetFieldnameFromInt(dest_col, dest_row);
            //        isMoved = true;
            //        MainWindow.board.lastAction = this.Desc + " SCHLÄGT " + opponent.Desc + " AUF " + dest.Name;
            //        MainWindow.board.chessman.Remove(opponent);
            //    }
            //    else
            //    {
            //        throw new InvalidMoveException();
            //    }
            //}
            //else
            //{
            //    throw new InvalidMoveException();
            //}
            //}
            /*else
            {
                if (source_col == dest_col && source_row - dest_row >= 1 && source_row - dest_row <= 2)
                {
                    if ((source_row - dest_row == 1) || (source_row - dest_row == 2 && !isMoved))
                    {
                        if (!isBlocked(source, dest))
                        {
                            source.Content = "";
                            this.Current_position = GetFieldnameFromInt(dest_col, dest_row);
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
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else if (((source_col - 1 == dest_col) || (source_col + 1 == dest_col)) && (source_row - dest_row == 1))
                {
                    Chessman opponent = MainWindow.board.GetChessmanAtField(dest);
                    if (opponent != null && opponent.Color != this.Color)
                    {
                        source.Content = "";
                        this.Current_position = GetFieldnameFromInt(dest_col, dest_row);
                        isMoved = true;
                        MainWindow.board.lastAction = this.Desc + " SCHLÄGT " + opponent.Desc + " AUF " + dest.Name;
                        MainWindow.board.chessman.Remove(opponent);
                    }
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else
                {
                    throw new InvalidMoveException();
                }
            }*/
        }

        private String GetFieldnameFromInt(int col, int row)
        {
            Char prefix = (Char)col;
            string field_name = prefix + "" + row;

            return field_name;
        }

        private bool isBlocked(MyButton source, MyButton dest)
        {

            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0, 1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            // Vertikaler Zug
            if (source_row != dest_row)
            {
                // Vertikal nach Oben
                if(dest_row > source_row)
                {
                    for (int i = source_row + 1; i <= dest_row; i++)
                    {
                        if (MainWindow.board.GetChessmanAtField(MainWindow.board.GetField(GetFieldnameFromInt(dest_col, i))) != null)
                        {
                            return true;
                        }
                    }
                }
                // Vertikal nach unten
                else if (dest_row > source_row)
                {
                    for (int i = source_row - 1; i <= dest_row; i--)
                    {
                        if (MainWindow.board.GetChessmanAtField(MainWindow.board.GetField(GetFieldnameFromInt(dest_col, i))) != null)
                        {
                            return true;
                        }
                    }
                }

            }
            // Horizontaler Zug
            else if (source_col != dest_col)
            {
                for (int i = source_row - 1; i >= dest_row; i--)
                {
                    if (MainWindow.board.GetChessmanAtField(MainWindow.board.GetField(GetFieldnameFromInt(dest_col, i))) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
