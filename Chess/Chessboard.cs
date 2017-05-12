using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{

    [Serializable()]
    class Chessboard
    {

        Game game;

        [NonSerialized]
        Square selectedField = null;

        [NonSerialized]
        Square fieldToMove = null;

        Chessman selectedChessman = null;

        internal List<Chessman> chessman = new List<Chessman>();

        [NonSerialized]
        List<Square> squares = new List<Square>();

        internal string lastAction;

        public Chessboard(Game game)
        {
            this.game = game;
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
                    square.Click += game.View.Select_Field;
                    square.Width = 50;
                    square.Height = 50;
                    
                    //square.BorderThickness = Control.BorderThicknessProperty.;
                    //square.OverridesDefaultStyle = true;
                    //square.BorderThickness = new Thickness(0);
                    //square.BorderBrush = Brushes.Transparent;

                    //Style style = new Style
                    //{
                    //    TargetType = typeof(Control)
                    //};
                    ////style.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Green));
                    //style.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(10.0)));

                    //square.Style = style;
                 

                    Char prefixChar = (Char)(prefix + col);
                    name = prefixChar + "" + row;
                    square.Name = name;

                   if (row % 2 == 0)
                    {
                        if(col % 2 == 0)
                        {
                            square.Color = Brushes.WhiteSmoke;
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
                            square.Color = Brushes.WhiteSmoke;
                            square.Background = square.Color;
                        }
                    }
                    game.View.panel.Children.Add(square);
                    squares.Add(square);
                }
            }

            // WEISSE SPIELFIGUREN
            PawnWhite pawn_w_1 = new PawnWhite(true, "A2", game);
            PawnWhite pawn_w_2 = new PawnWhite(true, "B2", game);
            PawnWhite pawn_w_3 = new PawnWhite(true, "C2", game);
            PawnWhite pawn_w_4 = new PawnWhite(true, "D2", game);
            PawnWhite pawn_w_5 = new PawnWhite(true, "E2", game);
            PawnWhite pawn_w_6 = new PawnWhite(true, "F2", game);
            PawnWhite pawn_w_7 = new PawnWhite(true, "G2", game);
            PawnWhite pawn_w_8 = new PawnWhite(true, "H2", game);
            Rook rook_w_1 = new Rook(true, "A1", game);
            Knight knight_w_1 = new Knight(true, "B1", game);
            Bishop bishop_w_1 = new Bishop(true, "C1", game);
            Queen queen_w = new Queen(true, "D1", game);
            King king_w = new King(true, "E1", game);
            Bishop bishop_w_2 = new Bishop(true, "F1", game);
            Knight knight_w_2 = new Knight(true, "G1", game);
            Rook rook_w_2 = new Rook(true, "H1", game);

            // SCHWARZE SPIELFIGUREN
            PawnBlack pawn_b_1 = new PawnBlack(false, "A7", game);
            PawnBlack pawn_b_2 = new PawnBlack(false, "B7", game);
            PawnBlack pawn_b_3 = new PawnBlack(false, "C7", game);
            PawnBlack pawn_b_4 = new PawnBlack(false, "D7", game);
            PawnBlack pawn_b_5 = new PawnBlack(false, "E7", game);
            PawnBlack pawn_b_6 = new PawnBlack(false, "F7", game);
            PawnBlack pawn_b_7 = new PawnBlack(false, "G7", game);
            PawnBlack pawn_b_8 = new PawnBlack(false, "H7", game);
            Rook rook_b_1 = new Rook(false, "A8", game);
            Knight knight_b_1 = new Knight(false, "B8", game);
            Bishop bishop_b_1 = new Bishop(false, "C8", game);
            Queen queen_b = new Queen(false, "D8", game);
            King king_b = new King(false, "E8", game);
            Bishop bishop_b_2 = new Bishop(false, "F8", game);
            Knight knight_b_2 = new Knight(false, "G8", game);
            Rook rook_b_2 = new Rook(false, "H8", game);

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
                    if (game.GetActivePlayer().Color == selectedChessman.Color)
                    {
                        selectedField = selected;
                        selectedField.Background = Brushes.MediumAquamarine;
                        lastAction = selectedChessman.Desc + " AUF " + selectedField.Name + " AUSGEWÄHLT";
                        game.ShowInfo(lastAction);
                    }
                    else
                    {
                        game.ShowInfo("BITTE EINE EIGENE SCHACHFIGUR AUSWÄHLEN");
                    }
                }
                else
                {
                    game.ShowInfo("BITTE EINE SCHACHFIGUR AUSWÄHLEN");
                }
            }
            // SCHACHFIGUR DE-SELEKTIEREN
            else if (selectedField == selected)
            {
                selectedField.Background = selectedField.Color;
                selectedField = null;
                selectedChessman = null;
                game.ShowInfo("NICHTS AUSGEWÄHLT");
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
                        selectedChessman.Game = game;
                        selectedChessman.Move(currentField, fieldToMove);
                        DisplayChessman();
                        game.RotatePlayer();
                        game.ShowInfo(lastAction, true);
                    }
                    catch (InvalidMoveException)
                    {
                        game.ShowInfo("UNGÜLTIGER ZUG");
                    }
                    catch (BlockedMoveException)
                    {
                        game.ShowInfo("DIESER ZUG IST BLOCKIERT");
                    }
                    catch (PlacedInCheckException)
                    {
                        if (game.GetActivePlayer().IsKingInCheck)
                        {
                            game.ShowInfo("DIESER ZUG IST NICHT MÖGLICH (KÖNIG WIRD BEDROHT)");
                        }
                        else
                        {
                            game.ShowInfo("DIESER ZUG SETZT DEN KÖNIG IN SCHACH");
                        }
                    }
                }
                else
                {
                    game.ShowInfo("KEINE FIGUR ZUM BEWEGEN AUSGEWÄHLT! - NICHTS AUSGEWÄHLT");
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

        public void Clear()
        {
            foreach (Square square in squares)
            {
                square.Content = "";
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

        public Rook GetRook(string orig_pos)
        {
            foreach (Chessman item in chessman)
            {
                if (item is Rook && item.Orig_position == orig_pos)
                {
                    return (Rook)item;
                }
            }
            return null;
        }

        public Square GetKingsPosition(string color)
        {
            string orig_pos = color == "white" ? "E1" : "E8";

            foreach (Chessman item in chessman)
            {
                if (item is King && item.Orig_position == orig_pos)
                {
                    return GetSquare(item.Current_position);
                }
            }
            return null;
        }

        // Überprüft, ob sich der König in Schach befindet
        public bool IsKingInCheck(string color)
        {
            Square kings_pos = GetKingsPosition(color);

            foreach (Chessman chessman in chessman)
            {
                if (color != chessman.Color)
                {
                    if (chessman.IsMoveValid(kings_pos) && !chessman.IsMoveBlocked(kings_pos))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
