using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Chess
{
    class Chessboard
    {

        internal MainWindow mainwindow;
        Square selectedField = null;
        Square fieldToMove = null;
        Chessman selectedChessman = null;
        internal List<Chessman> chessman = new List<Chessman>();
        List<Square> squares = new List<Square>();
        internal string lastAction;

        public Chessboard(MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
        }

        public void Init()
        {
            int prefix;
            string name = "";

            for (int row = 8; row > 0; row--)
            {
                prefix = 65;
                for (int col = 0; col < 8; col++)
                {
                    Square square = new Square();
                    square.Content = "";
                    square.Click += mainwindow.Select_Field;
                    square.Width = 50;
                    square.Height = 50;

                    Char prefixChar = (Char)(prefix + col);
                    name = prefixChar + "" + row;
                    square.Name = name;

                    if (row % 2 == 0)
                    {
                        if(col % 2 == 0)
                        {
                            square.Color = Brushes.White;
                            square.Background = square.Color;
                        }
                        else
                        {
                            square.Color = Brushes.RosyBrown;
                            square.Background = square.Color;
                        }
                    }
                    else
                    {
                        if (col % 2 == 0)
                        {
                            square.Color = Brushes.RosyBrown;
                            square.Background = square.Color;
                        }
                        else
                        {
                            square.Color = Brushes.White;
                            square.Background = square.Color;
                        }
                    }
                    mainwindow.panel.Children.Add(square);
                    squares.Add(square);
                }
            }

            Pawn pawn_w_1 = new Pawn(true, "A2");
            Pawn pawn_w_2 = new Pawn(true, "B2");
            Pawn pawn_w_3 = new Pawn(true, "C2");
            Pawn pawn_w_4 = new Pawn(true, "D2");
            Pawn pawn_w_5 = new Pawn(true, "E2");
            Pawn pawn_w_6 = new Pawn(true, "F2");
            Pawn pawn_w_7 = new Pawn(true, "G2");
            Pawn pawn_w_8 = new Pawn(true, "H2");
            Rook rook_w_1 = new Rook(true, "A1");
            Rook rook_w_2 = new Rook(true, "H1");
            Bishop bishop_w_1 = new Bishop(true, "C1");
            Bishop bishop_w_2 = new Bishop(true, "F1");
            Pawn pawn_b_1 = new Pawn(false, "A7");
            Pawn pawn_b_2 = new Pawn(false, "B7");
            Pawn pawn_b_3 = new Pawn(false, "C7");
            Pawn pawn_b_4 = new Pawn(false, "D7");
            Pawn pawn_b_5 = new Pawn(false, "E7");
            Pawn pawn_b_6 = new Pawn(false, "F7");
            Pawn pawn_b_7 = new Pawn(false, "G7");
            Pawn pawn_b_8 = new Pawn(false, "H7");
            Rook rook_b_1 = new Rook(false, "A8");
            Rook rook_b_2 = new Rook(false, "H8");
            Bishop bishop_b_1 = new Bishop(false, "C8");
            Bishop bishop_b_2 = new Bishop(false, "F8");
            chessman.Add(pawn_w_1);
            chessman.Add(pawn_w_2);
            chessman.Add(pawn_w_3);
            chessman.Add(pawn_w_4);
            chessman.Add(pawn_w_5);
            chessman.Add(pawn_w_6);
            chessman.Add(pawn_w_7);
            chessman.Add(pawn_w_8);
            chessman.Add(rook_w_1);
            chessman.Add(rook_w_2);
            chessman.Add(bishop_w_1);
            chessman.Add(bishop_w_2);
            chessman.Add(rook_w_2);
            chessman.Add(pawn_b_1);
            chessman.Add(pawn_b_2);
            chessman.Add(pawn_b_3);
            chessman.Add(pawn_b_4);
            chessman.Add(pawn_b_5);
            chessman.Add(pawn_b_6);
            chessman.Add(pawn_b_7);
            chessman.Add(pawn_b_8);
            chessman.Add(rook_b_1);
            chessman.Add(rook_b_2);
            chessman.Add(bishop_b_1);
            chessman.Add(bishop_b_2);
            DisplayChessman();
        }

        public void Select_Field(Square selected)
        {         
            // SCHACHFIGUR SELEKTIEREN
            if (selectedField == null)
            {
                selectedChessman = GetChessmanAtSquare(selected);
               
                if (selectedChessman != null)
                {
                    if (mainwindow.active_player.Color == selectedChessman.Color)
                    {
                        selectedField = selected;
                        selectedField.Background = Brushes.MediumAquamarine;
                        lastAction = selectedChessman.Desc + " AUF " + selectedField.Name + " AUSGEWÄHLT";
                        mainwindow.ShowInfo(lastAction);
                    }
                    else
                    {
                        mainwindow.ShowInfo("BITTE EINE EIGENE SCHACHFIGUR AUSWÄHLEN");
                    }
                }
                else
                {
                    mainwindow.ShowInfo("BITTE EINE SCHACHFIGUR AUSWÄHLEN");
                }
            }
            // SCHACHFIGUR DE-SELEKTIEREN
            else if (selectedField == selected)
            {
                selectedField.Background = selectedField.Color;
                selectedField = null;
                selectedChessman = null;
                mainwindow.ShowInfo("NICHTS AUSGEWÄHLT");
            }
            // SCHACHFIGUR BEWEGEN
            else
            {                
                fieldToMove = selected;
                if(selectedChessman != null)
                {
                    Square currentField = GetSquare(selectedChessman.Current_position);
                    try
                    {
                        selectedChessman.Move(currentField, fieldToMove);
                        DisplayChessman();
                        mainwindow.RotatePlayer();
                        mainwindow.ShowInfo(lastAction, true);
                    }
                    catch (InvalidMoveException)
                    {
                        mainwindow.ShowInfo("UNGÜLTIGER ZUG");
                    }
                    catch (BlockedMoveException)
                    {
                        mainwindow.ShowInfo("DIESER ZUG IST BLOCKIERT");
                    }
                }
                else
                {
                    mainwindow.ShowInfo("KEINE FIGUR ZUM BEWEGEN AUSGEWÄHLT! - NICHTS AUSGEWÄHLT");
                }
                selectedField.Background = selectedField.Color;
                selectedField = null;
                selectedChessman = null;
            }
        }

        public void DisplayChessman()
        {
            foreach (Chessman item in chessman)
            {
                Square field = GetSquare(item.Current_position);
                field.Content = item.View;
            }
        }

        public Square GetSquare(string pos)
        {
            foreach (Square item in squares)
            {
                if(item.Name == pos)
                {
                    return item;
                }
            }
            return null;
        }

        public Chessman GetChessmanAtSquare(Square selected)
        {
            foreach (Chessman item in chessman)
            {
                if(selected.Name == item.Current_position)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
