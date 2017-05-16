using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using System;
using System.Windows.Controls;

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

        private void Selected_Listitem(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            info.Content = "Liste geklickt";
        }

        private void Selection_DClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int pos = movesList.SelectedIndex;
            string desc = this.game.logger.Get(movesList.SelectedIndex).OwnMan.Desc;
            string cpos = this.game.logger.Get(movesList.SelectedIndex).OwnMan.Current_position;

            info.Content = "Liste selektiert: " + desc + " auf " + cpos;
        }
    }
}
