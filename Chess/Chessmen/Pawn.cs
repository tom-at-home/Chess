using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Pawn : Chessman
    {

        private bool isMoved = false;

        public Pawn(bool isWhite, string pos) : base(isWhite, pos)
        {

            this.desc = "BAUER";

            Image whitePawn = new Image();
            whitePawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer.png"));

            StackPanel whitePawnPnl = new StackPanel();
            whitePawnPnl.Orientation = Orientation.Horizontal;
            whitePawnPnl.Margin = new System.Windows.Thickness(8);
            whitePawnPnl.Children.Add(whitePawn);

            Image blackPawn = new Image();
            blackPawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer_sw.png"));

            StackPanel blackPawnPnl = new StackPanel();
            blackPawnPnl.Orientation = Orientation.Horizontal;
            blackPawnPnl.Margin = new System.Windows.Thickness(8);
            blackPawnPnl.Children.Add(blackPawn);

            if (isWhite)
            {
                this.View = whitePawnPnl;
                this.color = "white";
            }
            else
            {
                this.View = blackPawnPnl;
                this.color = "black";
            }           

        }

        public override bool IsMoveBlocked(Square dest)
        {
            throw new NotImplementedException();
        }

        public override bool IsMoveValid(Square dest)
        {
            throw new NotImplementedException();
        }

        public override void Move(Square source, Square dest)
        {
            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0, 1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            if (this.IsWhite)
            {
                if (source_col == dest_col && dest_row - source_row >= 1 && dest_row - source_row <= 2)
                {
                    if ((dest_row - source_row == 1) || (dest_row - source_row == 2 && !isMoved))
                    {
                        if(!IsBlocked(source, dest))
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
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else if (((source_col-1 == dest_col) || (source_col + 1 == dest_col)) && (dest_row - source_row == 1))
                {
                    Chessman opponent = MainWindow.board.GetChessmanAtSquare(dest);
                    if (opponent != null && opponent.Color != this.Color)
                    {
                        source.Content = "";
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
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
            }
            else
            {
                if (source_col == dest_col && source_row - dest_row >= 1 && source_row - dest_row <= 2)
                {
                    if ((source_row - dest_row == 1) || (source_row - dest_row == 2 && !isMoved))
                    {
                        if (!IsBlocked(source, dest))
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
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else if (((source_col - 1 == dest_col) || (source_col + 1 == dest_col)) && (source_row - dest_row == 1))
                {
                    Chessman opponent = MainWindow.board.GetChessmanAtSquare(dest);
                    if (opponent != null && opponent.Color != this.Color)
                    {
                        source.Content = "";
                        this.Current_position = GetSquarenameFromCoordinates(dest_col, dest_row);
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
            }
        }

        // Prüft, ob der Zug durch andere Schachfiguren versperrt ist
        private bool IsBlocked(Square source, Square dest)
        {

            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0, 1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            if(source_row < dest_row)
            {
                for(int i = source_row+1; i <= dest_row; i++)
                {
                    if(MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
                    {
                        return true;
                    }
                }
            }
            else if(source_row > dest_row)
            {
                for (int i = source_row - 1; i >= dest_row; i--)
                {
                    if (MainWindow.board.GetChessmanAtSquare(MainWindow.board.GetSquare(GetSquarenameFromCoordinates(dest_col, i))) != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
