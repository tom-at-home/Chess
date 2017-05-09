﻿using System.Windows;
using System.Windows.Media;

namespace Chess
{

    public partial class MainWindow : Window
    {

        static internal Chessboard board;

        Player white;
        Player black;

        public Player active_player;
        public Player waiting_player;

        public MemoryLogger logger;

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

            logger = new MemoryLogger();
            //movesList.ItemsSource = logger.GetAll();

            white = new Player("WEISS", "white", new Timer(timer_lbl_1));
            black = new Player("SCHWARZ", "black", new Timer(timer_lbl_2));
            active_player = white;
            waiting_player = black;
            active_player.timer.Start();

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
                playerIndicator.Fill = Brushes.Black;
            }
            else
            {
                active_player = white;
                waiting_player = black;
                playerIndicator.Fill = Brushes.WhiteSmoke;
            }

            active_player.timer.Start();
            waiting_player.timer.Stop();

            active_player.DoubleStepMovedPawn = null;

            // Prüfen, ob dem Spieler Schach geboten wird
            active_player.IsKingInCheck = board.IsKingInCheck(active_player.Color);
            if (active_player.IsKingInCheck)
            {
                this.info.Background = Brushes.IndianRed;
            }
            else
            {
                this.info.Background = new SolidColorBrush(Color.FromRgb(0x5A, 0x7E, 0x8F));
            }
        }
    }
}
