﻿using System.Windows;

namespace Chess
{

    public partial class MainWindow : Window
    {

        static internal Chessboard board;

        Player white;
        Player black;

        public Player active_player;
        public Player waiting_player;

        public static MainWindow appInstance;

        public MainWindow()
        {
            InitializeComponent();
            InitGame();
        }

        private void InitGame()
        {
            board = new Chessboard(this);
            board.Init();
            white = new Player("WEISS", "white");
            black = new Player("SCHWARZ", "black");
            active_player = white;
            ShowInfo("");
        }

        public void Select_Field(object sender, RoutedEventArgs e)
        {

            board.Select_Field((Square)sender);
            
            
        }

        public void ShowInfo(string msg, bool rotate = false)
        {
            if(rotate)
            {
                this.info.Content = msg;
                this.info.Content += "\r\n";
                this.info.Content += active_player.Name + " IST AM ZUG";
            }
            else
            {
                this.info.Content = active_player.Name + " IST AM ZUG";
                this.info.Content += "\r\n";
                this.info.Content += msg;
            }
        }

        public void RotatePlayer()
        {
            if(active_player == white)
            {
                active_player = black;
                waiting_player = white;
            }
            else
            {
                active_player = white;
                waiting_player = black;
            }

            active_player.DoubleStepMovedPawn = null;

        }
    }
}
