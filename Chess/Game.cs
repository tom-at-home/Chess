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
        }

        internal Chessboard board;

        Player white;
        Player black;

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

            white = new Player("WEISS", "white", new Timer(this.view.timer_lbl_1));
            black = new Player("SCHWARZ", "black", new Timer(this.view.timer_lbl_2));
            white.timer.Start();

            ShowInfo("");
        }

        public Player GetActivePlayer()
        {
            if (white.IsWaiting)
            {
                return black;
            }
            else
            {
                return white;
            }
        }

        public Player GetWaitingPlayer()
        {
            if (white.IsWaiting)
            {
                return white;
            }
            else
            {
                return black;
            }
        }

        public void ShowInfo(string msg, bool rotate = false)
        {
            if (rotate)
            {
                this.view.info.Content = msg;
                this.view.info.Content += "\r\n";
                this.view.info.Content += GetActivePlayer().Name + " IST AM ZUG";
            }
            else
            {
                this.view.info.Content = GetActivePlayer().Name + " IST AM ZUG";
                this.view.info.Content += "\r\n";
                this.view.info.Content += msg;
            }

            if (GetActivePlayer().IsKingCheckmate)
            {
                this.view.info.Content = msg;
                this.view.info.Content += "\r\n";
                this.view.info.Content += GetActivePlayer().Name + " IST SCHACHMATT";
            }
        }

        public void RotatePlayer()
        {
            if (GetActivePlayer() == white)
            {
                white.IsWaiting = true;
                black.IsWaiting = false;
            }
            else
            {
                white.IsWaiting = false;
                black.IsWaiting = true;
            }

            GetWaitingPlayer().timer.Stop();

            // Der Bauer kann nur unmittelbar nach seinem Doppelzug en passant geschlagen werden,
            // daher wird er in dieser Folgerunde aus der Eigenschaft DoubleStepMovedPawn
            // des aktuellen Spielers wieder entfernt
            GetActivePlayer().DoubleStepMovedPawn = null;

            // Prüfen, ob dem Spieler Schach geboten wird
            GetActivePlayer().IsKingInCheck = board.IsKingInCheck(GetActivePlayer().Color);

            // Prüfen, ob der Spieler Matt gesetzt wurde
            if (GetActivePlayer().IsKingInCheck)
            {
                GetActivePlayer().IsKingCheckmate = board.IsKingCheckmate(GetActivePlayer().Color);
            }

            if (!GetActivePlayer().IsKingCheckmate)
            {
                GetActivePlayer().timer.Start();
            }

            RefreshBoardStatus();

        }

        private void RefreshBoardStatus()
        {
            // Färbt die Anzeige des aktiven Spielers
            // Weiss respektive Schwarz
            if (GetActivePlayer() == white)
            {
                this.view.playerIndicator.Fill = Brushes.WhiteSmoke;
            }
            else
            {
                this.view.playerIndicator.Fill = Brushes.Black;
            }

            // Färbt die Infoanzeige pastellrot, 
            // wenn dem aktiven Spieler Schach geboten wird
            if (GetActivePlayer().IsKingInCheck)
            {
                this.view.info.Background = Brushes.IndianRed;
            }
            else
            {
                this.view.info.Background = new SolidColorBrush(Color.FromRgb(0x5A, 0x7E, 0x8F));
            }

            // Färbt die Infoanzeige leuchtrot,
            // wenn der aktive Spieler Matt gesetzt wurde
            if (GetActivePlayer().IsKingCheckmate)
            {
                this.view.info.Background = Brushes.Red;
            }

        }

        internal void Load_Game()
        {

            if (File.Exists("chess.sav"))
            {

                GetActivePlayer().timer.Stop();
                this.View.movesList.Items.Clear();

                FileStream fileStream = new FileStream("chess.sav", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                this.white = (Player)formatter.Deserialize(fileStream);
                this.black = (Player)formatter.Deserialize(fileStream);
                this.board.chessman = (List<Chessman>)formatter.Deserialize(fileStream);
                this.logger.logs = (List<LogEntry>)formatter.Deserialize(fileStream);

                fileStream.Close();

                // Nach dem Laden den Schachfiguren die Eigenschaft - Game - neu zuweisen
                // und die Figuren auf dem Brett neu aufstellen
                foreach (Chessman chessman in this.board.chessman)
                {
                    chessman.Game = this;
                }

                board.Clear();
                board.DisplayChessman();
                this.ShowInfo("");

                // Timer neu zuweisen
                white.timer.display = this.view.timer_lbl_1;
                black.timer.display = this.view.timer_lbl_2;

                white.timer.Reset();
                black.timer.Reset();

                white.timer.RefreshDisplay();
                black.timer.RefreshDisplay();

                GetActivePlayer().timer.Start();

                // Log-Einträge ( Historie der Züge )
                // der Liste wieder neu hinzufügen
                foreach (LogEntry log in this.logger.logs)
                {
                    this.View.movesList.Items.Add(log);
                }

                // Die Status-Anzeige des Schachbretts aktualisieren
                RefreshBoardStatus();
            }
            else
            {
                this.ShowInfo("KEIN GESPEICHERTES SPIEL VORHANDEN");
            }


        }

        internal void Save_Game()
        {

            // Experimente mit BinaryWriter //

            //int i = 433;
            //BinaryWriter sw = new BinaryWriter(new FileStream("chess.X.sav", FileMode.Create)  );
            //sw.Write(i);
            //sw.Write()
            //sw.Flush();
            //sw.Close();

            //BinaryReader sr = new BinaryReader(new FileStream("chess.X.sav", FileMode.Open));

            //int t = sr.ReadInt32();
            //sw.Flush();


            // Serialisieren mit BinaryFormatter //
            FileStream fileStream = new FileStream("chess.sav", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(fileStream, this.white);
            formatter.Serialize(fileStream, this.black);
            formatter.Serialize(fileStream, this.board.chessman);
            formatter.Serialize(fileStream, this.logger.logs);

            fileStream.Close();
        }

    }
}
