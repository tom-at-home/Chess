using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{

    [Serializable()]
    class Factory
    {

        Game game;

        public Factory(Game game)
        {
            this.game = game;
        }

        public PawnWhite GetPawnWhite(string init_pos)
        {
            return new PawnWhite(true, init_pos, this.game);
        }

        public PawnBlack GetPawnBlack(string init_pos)
        {
            return new PawnBlack(false, init_pos, this.game);
        }

        public Rook GetRook(string color, string init_pos)
        {
            bool isWhite = color == "white" ? true : false;
            return new Rook(isWhite, init_pos, this.game);
        }

        public Knight GetKnight(string color, string init_pos)
        {
            bool isWhite = color == "white" ? true : false;
            return new Knight(isWhite, init_pos, this.game);
        }

        public Bishop GetBishop(string color, string init_pos)
        {
            bool isWhite = color == "white" ? true : false;
            return new Bishop(isWhite, init_pos, this.game);
        }

        public Queen GetQueen(string color, string init_pos)
        {
            bool isWhite = color == "white" ? true : false;
            return new Queen(isWhite, init_pos, this.game);
        }

        public King GetKing(string color, string init_pos)
        {
            bool isWhite = color == "white" ? true : false;
            return new King(isWhite, init_pos, this.game);
        }

    }
}
