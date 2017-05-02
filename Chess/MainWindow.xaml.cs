using System.Windows;

namespace Chess
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {

        static internal Chessboard board;
        Player white;
        Player black;
        internal Player active_player;
        public MainWindow appInstance;

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

            board.Select_Field((MyButton)sender);
            
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
            }
            else
            {
                active_player = white;
            }
        }
    }
}
