using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Chessmen
{
    interface IMovable
    {

        bool IsMoveValid(Square dest);

        bool IsMoveBlocked(Square dest);

        void Move(Square source, Square dest);

    }
}
