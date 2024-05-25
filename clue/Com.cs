using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Com : Player
    {
        int goLoc;              //가고있는 위치 정보
        List<Card> knowCard;    //알고있는 카드 정보
        
        public Com(List<Card> startCard)
        {
            addCard(startCard);
        }
    }
}
