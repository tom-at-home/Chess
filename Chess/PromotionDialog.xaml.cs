using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{

    public partial class PromotionDialog : Window
    {

        Pawn toPromote;
        Chessman rook;
        Chessman knight;
        Chessman bishop;
        Chessman queen;

        public PromotionDialog(Pawn toPromote)
        {

            this.toPromote = toPromote;
            InitializeComponent();
            InitBoard();
        }

        public void InitBoard()
        {

            rook = new Rook(toPromote.IsWhite, "A1");
            knight = new Knight(toPromote.IsWhite, "A2");
            bishop = new Bishop(toPromote.IsWhite, "A3");
            queen = new Queen(toPromote.IsWhite, "A4");

            Chessman[] promMan = new Chessman[4] { rook, knight, bishop, queen };


            for (int i = 1; i <= 4; i++)
            {

                Square square = new Square();
                square.Content = promMan[i-1].View;
                square.Click += SetSelectedChessman;
                square.Width = 50;
                square.Height = 50;
                square.Name = "A" + i;

                if (i % 2 == 0)
                {
                    square.Color = Brushes.White;
                    square.Background = square.Color;
                }
                else
                {
                    square.Color = Brushes.RosyBrown;
                    square.Background = square.Color;
                }
                this.panel.Children.Add(square);
            }
        }

        public void SetSelectedChessman(object sender, RoutedEventArgs e)
        {

            Square selected = (Square)sender;

            switch (selected.Name)
            {
                case "A1":
                    this.toPromote.promotedIn = this.rook;
                    break;
                case "A2":
                    this.toPromote.promotedIn = this.knight;
                    break;
                case "A3":
                    this.toPromote.promotedIn = this.bishop;
                    break;
                case "A4":
                    this.toPromote.promotedIn = this.queen;
                    break;
            }
            this.Close();
        }
    }
}
