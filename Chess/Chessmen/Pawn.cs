using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Chess
{
    public abstract class Pawn : Chessman
    {
        public bool isMoved = false;

        public Chessman promotedIn;

        public abstract override bool IsMoveBlocked(Square dest);

        public abstract override bool IsMoveValid(Square dest);

        public Pawn(bool isWhite, string pos) : base(isWhite, pos)
        {
            this.desc = "BAUER";
            this.NotationCode = "";
        }

        public void PromotePawn()
        {
            PromotionDialog pd = new PromotionDialog(this);
            pd.ShowDialog();

            MainWindow.board.lastAction += " ( IN " + promotedIn.Desc + " UMGEWANDELT )";
            MainWindow.board.chessman.Add(this.promotedIn);
            MainWindow.board.chessman.Remove(this);
        }
    }

}
