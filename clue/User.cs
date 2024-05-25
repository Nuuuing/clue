using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class User : Player
    {
        public User(List<Card> startCard)
        {
            addCard(startCard);
        }
    }
}
