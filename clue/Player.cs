﻿using System;
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

        public void AddCard(List<Card> addCardList)
        {
            for (int i = 0; i < addCardList.Count; i++)
            {
                cardList.Add(addCardList[i]);
            }
        }

        public void AddCard(Card addCard)
        {
            cardList.Add(addCard);
        }

        public List<Card> GetCardList()
        {
            return cardList;
        }
        
        public int GetLocByCoor((int, int) _position) //좌표로 장소번호 return
        {
            if (_position == (8, 12) || _position == (9, 11) || _position == (9, 13) || _position == (11, 12) || _position == (9, 12))
            {
                return 2;
            }
            else
            {
                if (_position == (4, 11))
                    return 3;
                if (_position == (3, 6))
                    return 4;
                if (_position == (3, 17))
                    return 5;
                if (_position == (10, 20))
                    return 6;
                if (_position == (16, 17))
                    return 7;
                if (_position == (16, 11))
                    return 8;
                if (_position == (16, 4))
                    return 9;
                if (_position == (13, 2))
                    return 10;
                if (_position == (10, 4))
                    return 11;
                else
                    return 0;
            }
        }

         public bool CheckComPosMiddle()
        {
            if(this.position.Equals((9, 12)))
                return true;
            else
                return false;
        }   //2번 위치인지 확인

        public bool CheckIsLoc()    //현재 위치가 장소타일인지 확인, but 2일때도 false
        {
            bool isLoc = false;
            if (GetLocByCoor(position) != 2 && GetLocByCoor(position) != -1)
            {
                isLoc = true;
            }
            return isLoc;
        }

        public bool CheckProov()
        {
            bool isProov = false;
            return isProov;
        }

        public void SetMoveCount(int num)
        {
            moveCount = num;
        }

        public int GetMoveCount()
        {
            return moveCount;
        }

        public bool GetAlive()
        {
            return isAlive;
        }

        public void SetDie()
        {
            isAlive = false;
        }
    }
}
