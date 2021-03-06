﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{

    [Serializable()]
    public class Player
    {

        public Timer timer;

        private bool isKingInCheck;
        public bool IsKingInCheck
        {
            get { return isKingInCheck; }
            set { isKingInCheck = value; }
        }

        private bool isKingCheckmate;
        public bool IsKingCheckmate
        {
            get { return isKingCheckmate; }
            set { isKingCheckmate = value; }
        }

        private Chessman doubleStepMovedPawn;
        public Chessman DoubleStepMovedPawn
        {
            get { return doubleStepMovedPawn; }
            set { doubleStepMovedPawn = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
        }

        private string color;
        public string Color
        {
            get { return color; }
        }

        private bool isWhite;
        public bool IsWhite
        {
            get { return isWhite; }
        }

        private bool isWaiting;
        public bool IsWaiting
        {
            get { return isWaiting; }
            set { isWaiting = value; }
        }

        public Player(string name, string color, Timer timer)
        {
            this.name = name;
            this.color = color;

            if(color == "white")
            {
                this.isWhite = true;
            }
            else
            {
                this.isWhite = false;
            }

            this.timer = timer;
        }

    }
}
