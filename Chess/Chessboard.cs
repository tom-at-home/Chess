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

            PawnWhite pawn_w_1 = new PawnWhite("A2");
            PawnWhite pawn_w_2 = new PawnWhite("B2");
            PawnWhite pawn_w_3 = new PawnWhite("C2");
            PawnWhite pawn_w_4 = new PawnWhite("D2");
            PawnWhite pawn_w_5 = new PawnWhite("E2");
            PawnWhite pawn_w_6 = new PawnWhite("F2");
            PawnWhite pawn_w_7 = new PawnWhite("G2");
            PawnWhite pawn_w_8 = new PawnWhite("H2");
            Rook rook_w_1 = new Rook(true, "A1");
            Knight knight_w_1 = new Knight(true, "B1");
            Bishop bishop_w_1 = new Bishop(true, "C1");
            Queen queen_w = new Queen(true, "D1");
            King king_w = new King(true, "E1");
            Bishop bishop_w_2 = new Bishop(true, "F1");
            Knight knight_w_2 = new Knight(true, "G1");
            Rook rook_w_2 = new Rook(true, "H1");
            PawnBlack pawn_b_1 = new PawnBlack("A7");
            PawnBlack pawn_b_2 = new PawnBlack("B7");
            PawnBlack pawn_b_3 = new PawnBlack("C7");
            PawnBlack pawn_b_4 = new PawnBlack("D7");
            PawnBlack pawn_b_5 = new PawnBlack("E7");
            PawnBlack pawn_b_6 = new PawnBlack("F7");
            PawnBlack pawn_b_7 = new PawnBlack("G7");
            PawnBlack pawn_b_8 = new PawnBlack("H7");
            Rook rook_b_1 = new Rook(false, "A8");
            Knight knight_b_1 = new Knight(false, "B8");
            Bishop bishop_b_1 = new Bishop(false, "C8");
            Queen queen_b = new Queen(false, "D8");
            King king_b = new King(false, "E8");
            Bishop bishop_b_2 = new Bishop(false, "F8");
            Knight knight_b_2 = new Knight(false, "G8");        
            Rook rook_b_2 = new Rook(false, "H8");

            // WEISSE SPIELFIGUREN
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
            chessman.Add(knight_w_1);
            chessman.Add(knight_w_2);
            chessman.Add(queen_w);
            chessman.Add(king_w);

            // SCHWARZE SPIELFIGUREN
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
            chessman.Add(knight_b_1);
            chessman.Add(knight_b_2);
            chessman.Add(queen_b);
            chessman.Add(king_b);

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
                    catch (PlacedInCheckException)
                    {
                        mainwindow.ShowInfo("DIESER ZUG SETZT DEN KÖNIG IN SCHACH");
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

        public Chessman GetChessmanAtSquare(Square square)
        {
            foreach (Chessman item in chessman)
            {
                if(square.Name == item.Current_position)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
