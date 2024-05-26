using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Player
    {
        int moveCount = 0;                      //이동 가능 칸수
        bool isAlive = true;
        List<Card> cardList = new List<Card>(); //소지 카드 목록
        public (int, int) position = (9, 12);   //현재 위치값, y: 9, x: 12에서 시작

        public void addCard(List<Card> addCardList)
        {
            for (int i = 0; i < addCardList.Count; i++)
            {
                cardList.Add(addCardList[i]);
            }
        }

        public void addCard(Card addCard)
        {
            cardList.Add(addCard);
        }

        public List<Card> getCardList()
        {
            return cardList;
        }
        
        public void setMoveCount(int num)
        {
            moveCount = num;
        }

        public int getMoveCount()
        {
            return moveCount;
        }

        public bool getAlive()
        {
            return isAlive;
        }

        public void setDie()
        {
            isAlive = false;
        }
    }
}
