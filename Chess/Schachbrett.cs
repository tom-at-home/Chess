using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Chess
{
    class Schachbrett
    {

        MainWindow mainwindow;
        MyButton selectedField = null;
        MyButton fieldToMove = null;

        public void Init(MainWindow mainwindow)
        {

            int prefix;
            string name = "";
            this.mainwindow = mainwindow;     

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
                }
            }
        }

        public void Select_Field(MyButton selected)
        {
            if (selectedField == null)
            {
                selectedField = selected;
                selectedField.Background = Brushes.MediumAquamarine;
                mainwindow.info.Content = selectedField.Name + " AUSGEWÄHLT";
            }
            else if (selectedField == selected)
            {
                selectedField.Background = selectedField.Color;
                selectedField = null;
                mainwindow.info.Content = "NICHT AUSGEWÄHLT";
            }
            else
            {
                fieldToMove = selected;
                mainwindow.info.Content = "TRY TO MOVE TO: " + fieldToMove.Name + "\r\nNICHTS AUSGEWÄHLT";
                selectedField.Background = selectedField.Color;
                selectedField = null;
            }
        }
    }
}
