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

        Schachbrett brett;

        public MainWindow()
        {
            InitializeComponent();
            brett = new Schachbrett();
            brett.Init(this);
        }

        public void Select_Field(object sender, RoutedEventArgs e)
        {

            brett.Select_Field((MyButton)sender);
            
        }
    }
}
