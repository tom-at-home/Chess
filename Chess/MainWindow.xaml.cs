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

        double last_top_pos;
        double last_left_pos;

        public MainWindow()
        {
            InitializeComponent();
            this.Init();
        }

        public void Init()
        {
            game = new Game(this);
            game.Init();
        }

        public void UpdatePositionValues()
        {
            this.last_left_pos = this.Left;
            this.last_top_pos = this.Top;
        }

        public void Select_Field(object sender, RoutedEventArgs e)
        {
            game.board.Select_Field((Square)sender);
        }

        private void New_Game(object sender, RoutedEventArgs e)
        {
            MessageBoxResult decision = MessageBox.Show("NEUES SPIEL STARTEN?", "BITTE BESTÄTIGEN", MessageBoxButton.YesNo);

            if(decision == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Top = this.last_top_pos;
                mainWindow.Left = this.last_left_pos;
                mainWindow.Show();
            }
        }

        public void Load_Game(object sender, RoutedEventArgs e)
        {
            game.Load_Game();
        }

        public void Save_Game(object sender, RoutedEventArgs e)
        {
            game.Save_Game();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdatePositionValues();
        }

        // Noch sehr experimentell
        private void Selection_DClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int pos = movesList.SelectedIndex;
            string description = this.game.logger.Get(pos).OwnMan.Desc;
            string destination = this.game.logger.Get(pos).OwnManDest;
            info.Content = "Liste selektiert: " + description + " auf " + destination;
        }

    }
}
