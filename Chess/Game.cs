using System.Windows.Media;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

namespace Chess
{

    [Serializable()]
    public class Game
    {
        [NonSerialized]
        private MainWindow view;
        public MainWindow View
        {
            get { return view; }
           // set { view = value; }
        }

        internal Chessboard board;

        Player white;
        Player black;

        public Player active_player;
        public Player waiting_player;

        public MemoryLogger logger;

        public Game(MainWindow mainWindow)
        {
            this.view = mainWindow;
        }

        public void Init()
        {
            board = new Chessboard(this);
            board.Init();

            logger = new MemoryLogger();
            //movesList.ItemsSource = logger.GetAll();

            white = new Player("WEISS", "white", new Timer(this.view.timer_lbl_1));
            black = new Player("SCHWARZ", "black", new Timer(this.view.timer_lbl_2));
            active_player = white;
            waiting_player = black;
            active_player.timer.Start();

            ShowInfo("");
        }

        public void ShowInfo(string msg, bool rotate = false)
        {
            if (rotate)
            {
                this.view.info.Content = msg;
                this.view.info.Content += "\r\n";
                this.view.info.Content += active_player.Name + " IST AM ZUG";
            }
            else
            {
                this.view.info.Content = active_player.Name + " IST AM ZUG";
                this.view.info.Content += "\r\n";
                this.view.info.Content += msg;
            }
        }

        public void RotatePlayer()
        {
            if (active_player == white)
            {
                active_player = black;
                waiting_player = white;
                this.view.playerIndicator.Fill = Brushes.Black;
            }
            else
            {
                active_player = white;
                waiting_player = black;
                this.view.playerIndicator.Fill = Brushes.WhiteSmoke;
            }

            active_player.timer.Start();
            waiting_player.timer.Stop();

            active_player.DoubleStepMovedPawn = null;

            // Prüfen, ob dem Spieler Schach geboten wird
            active_player.IsKingInCheck = board.IsKingInCheck(active_player.Color);
            if (active_player.IsKingInCheck)
            {
                this.view.info.Background = Brushes.IndianRed;
            }
            else
            {
                this.view.info.Background = new SolidColorBrush(Color.FromRgb(0x5A, 0x7E, 0x8F));
            }
        }

        internal  void Load_Game()
        {
            FileStream fileStream = new FileStream("chess.sav", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            //board.chessman.Clear();

            //board.Clear();

            //board.chessman = (List<Chessman>)formatter.Deserialize(fileStream);
            //Game g  = (Game)formatter.Deserialize(fileStream);
            //g.board.Clear();

            //  this = (Game)formatter.Deserialize(fileStream);

            /////////////

            this.white = (Player)formatter.Deserialize(fileStream);
            this.black = (Player)formatter.Deserialize(fileStream);
            this.active_player = (Player)formatter.Deserialize(fileStream);
            this.waiting_player = (Player)formatter.Deserialize(fileStream);
            this.board.chessman =  (List<Chessman>)formatter.Deserialize(fileStream);

            foreach(Chessman c in this.board.chessman) { 
                c.Game = this;
            }




            //formatter.Deserialize(fileStream, this.white);
            //formatter.Deserialize(fileStream, this.black);
            //formatter.Deserialize(fileStream, this.active_player);
            //formatter.Deserialize(fileStream, this.waiting_player);
            //formatter.Deserialize((fileStream, this.board);





            ////////////



            fileStream.Close();

         //  board.Init();
            
           // board.Clear();
            board.DisplayChessman();
            

        }



        internal void Save_Game()
        {


            //int i = 433;
            //BinaryWriter sw = new BinaryWriter(new FileStream("chess.X.sav", FileMode.Create)  );
            //sw.Write(i);
            //sw.Write()
            //sw.Flush();
            //sw.Close();

            //BinaryReader sr = new BinaryReader(new FileStream("chess.X.sav", FileMode.Open));


            //int t = sr.ReadInt32();
            //sw.Flush();


            FileStream fileStream = new FileStream("chess.sav", FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            
            //formatter.Serialize(fileStream, this);
            formatter.Serialize(fileStream, this.white);
            formatter.Serialize(fileStream, this.black);
            formatter.Serialize(fileStream, this.active_player);
            formatter.Serialize(fileStream, this.waiting_player);
            formatter.Serialize(fileStream, this.board.chessman);

            fileStream.Close();
        }

    }
}
