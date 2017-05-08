using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
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

        public LogEntry(Chessman OwnMan, string OwnManSource, string OwnManDest)
        {
            this.OwnMan = OwnMan;
            this.OwnManSource = OwnManSource;
            this.OwnManDest = OwnManDest;
        }
    }
}
