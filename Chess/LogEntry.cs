using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{

    [Serializable()]
    public class LogEntry
    {
        public Chessman OwnMan;
        public Chessman OpponentMan = null;
        public Chessman PromotedIn = null;
        public string OwnManSource;
        public string OwnManDest;

        public bool PlacedInCheck = false;
        public bool PlacedInMate = false;
        public bool PerformedCastlingKingsSide = false;
        public bool PerfomedCastlingQueensSide = false;
        public bool TookEnPassant = false;

        //public string Representation;
        //public string TimeTaken;

        public LogEntry(Chessman OwnMan, string OwnManSource, string OwnManDest)
        {
            this.OwnMan = OwnMan;
            this.OwnManSource = OwnManSource;
            this.OwnManDest = OwnManDest;
        }

        public override string ToString()
        {
            string str = "";

            // Angriffszug
            if(OpponentMan != null)
            {

                str += OwnMan.NotationCode;
                str += OwnManSource.Substring(0, 1).ToLower() + "" + OwnManSource.Substring(1, 1);
                str += "x";
                str += OpponentMan.NotationCode;
                str += OwnManDest.Substring(0, 1).ToLower() + "" + OwnManDest.Substring(1, 1);

                // En Passant geschlagen
                if (TookEnPassant)
                {
                    str += " e.p.";
                }
            }
            // Einfacher Zug ohne Angriff
            else
            {

                if (PerformedCastlingKingsSide)
                {
                    str += "00-00";
                }
                else if (PerfomedCastlingQueensSide)
                {
                    str += "00-00-00";
                }
                else
                {
                    str += OwnMan.NotationCode;
                    str += OwnManSource.Substring(0, 1).ToLower() + "" + OwnManSource.Substring(1, 1);
                    str += "-";
                    str += OwnManDest.Substring(0, 1).ToLower() + "" + OwnManDest.Substring(1, 1);
                }

            }

            // Prüft, ob der Bauer umgewandelt wurde
            if(PromotedIn != null)
            {
                str += PromotedIn.NotationCode;
            }

            // Prüft, ob der Gegner Schach oder Matt gesetzt wurde
            if (PlacedInCheck)
            {
                if (PlacedInMate)
                {
                    str += "#";
                }
                else
                {
                    str += "+";
                }
            }

            return str;
        }
    }
}
