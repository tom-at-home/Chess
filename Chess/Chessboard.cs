using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{
    class Chessboard
    {

        MainWindow mainwindow;
        MyButton selectedField = null;
        MyButton fieldToMove = null;
        Chessman selectedChessman = null;
        List<Chessman> chessman = new List<Chessman>();
        List<MyButton> squares = new List<MyButton>();

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
                    MyButton button = new MyButton();
                    button.Content = "";
                    button.Click += mainwindow.Select_Field;
                    button.Width = 50;
                    button.Height = 50;

                    Char prefixChar = (Char)(prefix + col);
                    name = prefixChar + "" + row;
                    button.Name = name;

                    if (row % 2 == 0)
                    {
                        if(col % 2 == 0)
                        {
                            button.Color = Brushes.White;
                            button.Background = button.Color;
                        }
                        else
                        {
                            button.Color = Brushes.RosyBrown;
                            button.Background = button.Color;
                        }
                    }
                    else
                    {
                        if (col % 2 == 0)
                        {
                            button.Color = Brushes.RosyBrown;
                            button.Background = button.Color;
                        }
                        else
                        {
                            button.Color = Brushes.White;
                            button.Background = button.Color;
                        }
                    }
                    mainwindow.panel.Children.Add(button);
                    squares.Add(button);
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
            chessman.Add(pawn_w_1);
            chessman.Add(pawn_w_2);
            chessman.Add(pawn_w_3);
            chessman.Add(pawn_w_4);
            chessman.Add(pawn_w_5);
            chessman.Add(pawn_w_6);
            chessman.Add(pawn_w_7);
            chessman.Add(pawn_w_8);
            DisplayChessman();
        }

        public void Select_Field(MyButton selected)
        {         
            // SCHACHFIGUR SELEKTIEREN
            if (selectedField == null)
            {
                selectedChessman = GetChessmanAtField(selected);

                if (selectedChessman != null)
                {
                    selectedField = selected;
                    selectedField.Background = Brushes.MediumAquamarine;
                    mainwindow.info.Content = selectedChessman.Desc + " AUF " + selectedField.Name + " AUSGEWÄHLT";
                }
                else
                {
                    mainwindow.info.Content = "BITTE EINE SCHACHFIGUR AUSWÄHLEN";
                }
            }
            // SCHACHFIGUR DE-SELEKTIEREN
            else if (selectedField == selected)
            {
                selectedField.Background = selectedField.Color;
                selectedField = null;
                selectedChessman = null;
                mainwindow.info.Content = "NICHTS AUSGEWÄHLT";
            }
            // SCHACHFIGUR BEWEGEN
            else
            {                
                fieldToMove = selected;
                if(selectedChessman != null)
                {
                    MyButton currentField = GetField(selectedChessman.Current_Position);
                    try
                    {
                        selectedChessman.Move(currentField, fieldToMove);
                        mainwindow.info.Content = "BEWEGE " + selectedChessman.Desc
                                    + " VON " + currentField.Name
                                    + " AUF " + fieldToMove.Name;
                        DisplayChessman();
                    }
                    catch (InvalidMoveException)
                    {
                        mainwindow.info.Content = "UNGÜLTIGER ZUG";
                    }

                }
                else
                {
                    mainwindow.info.Content = "KEINE FIGUR ZUM BEWEGEN AUSGEWÄHLT!\r\nNICHTS AUSGEWÄHLT";
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
                MyButton field = GetField(item.Current_Position);
                field.Content = item.View;
            }
        }

        public MyButton GetField(string pos)
        {
            foreach (MyButton item in squares)
            {
                if(item.Name == pos)
                {
                    return item;
                }
            }
            return null;
        }

        public Chessman GetChessmanAtField(MyButton selected)
        {
            foreach (Chessman item in chessman)
            {
                if(selected.Name == item.Current_Position)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
