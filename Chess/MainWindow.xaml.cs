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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MyButton selectedField = null;
        MyButton fieldToMove = null;

        public MainWindow()
        {
            InitializeComponent();
            Schachbrett brett = new Schachbrett();
            brett.Init(this);
        }

        public void Select_Field(object sender, RoutedEventArgs e)
        {
            
            if(selectedField == null)
            {
                selectedField = (MyButton)sender;
                selectedField.Background = Brushes.MediumAquamarine;
                info.Content = selectedField.Name + " AUSGEWÄHLT";
            }
            else if(selectedField == (MyButton)sender)
            {
                selectedField.Background = selectedField.Color;
                selectedField = null;
                info.Content = "NICHT AUSGEWÄHLT";
            }
            else
            {
                fieldToMove = (MyButton)sender;
                info.Content = "TRY TO MOVE TO: " + fieldToMove.Name + "\r\nNICHTS AUSGEWÄHLT";
                selectedField.Background = selectedField.Color;
                selectedField = null;
            }
            
        }
    }
}
