using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Player
    {

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

        public Player(string name, string color)
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
        }

    }
}
