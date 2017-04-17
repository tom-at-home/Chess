﻿using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    class Pawn : Chessman
    {

        private bool isMoved = false;

        public Pawn(bool isWhite, string pos)
        {
            Image whitePawn = new Image();
            whitePawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer.png"));

            StackPanel whitePawnPnl = new StackPanel();
            whitePawnPnl.Orientation = Orientation.Horizontal;
            whitePawnPnl.Margin = new System.Windows.Thickness(10);
            whitePawnPnl.Children.Add(whitePawn);

            Image blackPawn = new Image();
            blackPawn.Source = new BitmapImage(new Uri(@"pack://siteoforigin:,,,/Images/bauer_sw.png"));

            StackPanel blackPawnPnl = new StackPanel();
            blackPawnPnl.Orientation = Orientation.Horizontal;
            blackPawnPnl.Margin = new System.Windows.Thickness(10);
            blackPawnPnl.Children.Add(blackPawn);

            this.isWhite = isWhite;
            if (isWhite)
            {
                this.View = whitePawnPnl;
                this.color = "white";
            }
            else
            {
                this.view = blackPawnPnl;
                this.color = "black";
            }           
            this.current_position = pos;
            this.desc = "BAUER";
        }
        /*
        public override void Move(MyButton source, MyButton dest)
        {
            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0,1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            if (this.isWhite)
            {
                if(source_col == dest_col && dest_row - source_row == 2)
                {
                    if(!isMoved && !isBlocked(source, dest))
                    {
                        source.Content = "";
                        this.current_position = GetFieldnameFromInt(dest_col, dest_row);
                        isMoved = true;
                    }
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else if (source_col == dest_col && dest_row - source_row == 1)
                {
                    if (!isBlocked(source, dest))
                    {
                        source.Content = "";
                        this.current_position = GetFieldnameFromInt(dest_col, dest_row);
                        isMoved = true;
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
                if (source_col == dest_col && source_row - dest_row == 2)
                {
                    if (!isMoved && !isBlocked(source, dest))
                    {
                        source.Content = "";
                        this.current_position = GetFieldnameFromInt(dest_col, dest_row);
                        isMoved = true;
                    }
                    else
                    {
                        throw new InvalidMoveException();
                    }
                }
                else if (source_col == dest_col && source_row - dest_row == 1)
                {
                    if (!isBlocked(source, dest))
                    {
                        source.Content = "";
                        this.current_position = GetFieldnameFromInt(dest_col, dest_row);
                        isMoved = true;
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
        }*/
        
        public override void Move(MyButton source, MyButton dest)
        {
            int source_col = Convert.ToInt16(Convert.ToChar(source.Name.Substring(0, 1)));
            int source_row = Convert.ToInt16(source.Name.Substring(1, 1));
            int dest_col = Convert.ToInt16(Convert.ToChar(dest.Name.Substring(0, 1)));
            int dest_row = Convert.ToInt16(dest.Name.Substring(1, 1));

            if (this.isWhite)
            {
                if (source_col == dest_col && dest_row - source_row >= 1 && dest_row - source_row <= 2)
                {
                    if ((dest_row - source_row == 1) || (dest_row - source_row == 2 && !isMoved))
                    {
                        if(!isBlocked(source, dest))
                        {
                            source.Content = "";
                            this.current_position = GetFieldnameFromInt(dest_col, dest_row);
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
                        if (!isBlocked(source, dest))
                        {
                            source.Content = "";
                            this.current_position = GetFieldnameFromInt(dest_col, dest_row);
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
                else
                {
                    throw new InvalidMoveException();
                }
            }
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

            if(source_row < dest_row)
            {
                for(int i = source_row+1; i <= dest_row; i++)
                {
                    if(MainWindow.board.GetChessmanAtField(MainWindow.board.GetField(GetFieldnameFromInt(dest_col, i))) != null)
                    {
                        return true;
                    }
                }
            }
            else if(source_row > dest_row)
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
