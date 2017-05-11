using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    [Serializable()]
    public abstract class Pawn : Chessman
    {
        public bool isMoved = false;

        public Chessman promotedIn;

        public abstract override bool IsMoveBlocked(Square dest);

        public abstract override bool IsMoveValid(Square dest);

        public Pawn(bool isWhite, string pos, Game game) : base(isWhite, pos, game)
        {
            this.desc = "BAUER";
            this.NotationCode = "";
        }

        public void PromotePawn()
        {
            PromotionDialog pd = new PromotionDialog(this.game, this);
            pd.ShowDialog();

            this.game.board.lastAction += " ( IN " + promotedIn.Desc + " UMGEWANDELT )";
            this.game.board.chessman.Add(this.promotedIn);
            this.game.board.chessman.Remove(this);
        }
    }

}
