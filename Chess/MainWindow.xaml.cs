using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using System;

namespace Chess
{

    public partial class MainWindow : Window
    {

        Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
            game.Init();
        }

        public void Select_Field(object sender, RoutedEventArgs e)
        {
            game.board.Select_Field((Square)sender);
        }

        public void Load_Game(object sender, RoutedEventArgs e)
        {
            game.Load_Game();
        }

        public void Save_Game(object sender, RoutedEventArgs e)
        {
            game.Save_Game();
        }

    }
}
