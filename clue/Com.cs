using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace clue
{
    class Com : Player
    {
        List<Card> knowLocCard = new List<Card>();    //알고있는 카드 정보
        List<Card> knowWepCard = new List<Card>();
        List<Card> knowPerCard = new List<Card>();
        public int checkLoc = 0;           //현재 찾고있는 장소
        public Queue<(int, int)> goPath = new Queue<(int, int)>();   //가고있는 루트

        public Com(List<Card> startCard)
        {
            AddCard(startCard);
        }

        public bool CheckGoLocation()   //새로운 장소를 찾아야 하는지 여부
        {
            bool check = true;
            if(checkLoc == 0)
            {
                return true;
            }
            else if (knowLocCard != null)
            {
                for (int i = 0; i < knowLocCard.Count; i++)
                {
                    if (knowLocCard[i].GetLocNum().Equals(checkLoc))
                    {
                        //알고있는 정보중에 현재 가고있는 장소가 포함되면
                        check =true;
                        break;
                    }
                    else
                    {
                        check = false;
                    }
                }
            }

            return check;
        }

        public void AddKnowCard(List<Card> addCardList) //아는 정보 저장 LIST
        {
            for (int i = 0; i < addCardList.Count; i++)
            {
                if (addCardList[i].GetCardType().Equals(CardType.LOC))
                    knowLocCard.Add(addCardList[i]);
                else if(addCardList[i].GetCardType().Equals(CardType.PER))
                    knowPerCard.Add(addCardList[i]);
                else
                    knowWepCard.Add(addCardList[i]);
            }
        }

        public void AddKnowCard(Card addCard)   //아는 정보 저장 1개
        {
            if (addCard.GetCardType().Equals(CardType.LOC))
                knowLocCard.Add(addCard);
            else if (addCard.GetCardType().Equals(CardType.PER))
                knowPerCard.Add(addCard);
            else
                knowWepCard.Add(addCard);
        }

        public (int, int) GetCoorByNum(int num) //장소번호로 좌표 return
        {
            if (num == 2)
                return (9, 11);
            else if (num == 3)
                return (4, 11);
            else if (num == 4)
                return (3, 6);
            else if (num == 5)
                return (3, 17);
            else if (num == 6)
                return (10, 20);
            else if (num == 7)
                return (16, 17);
            else if (num == 8)
                return (16, 11);
            else if (num == 9)
                return (16, 4);
            else if (num == 10)
                return (13, 2);
            else if (num == 11)
                return (10, 4);
            else
                return (0, 0);
        }

        public int GetLocByName(string locName) //장소 명칭으로 장소번호 return
        {
            if (locName.Equals("중앙홀"))
                return 2;
            else if (locName.Equals("식당"))
                return 3;
            else if (locName.Equals("부엌"))
                return 4;
            else if (locName.Equals("거실"))
                return 5;
            else if (locName.Equals("마당"))
                return 6;
            else if (locName.Equals("차고"))
                return 7;
            else if (locName.Equals("게임룸"))
                return 8;
            else if (locName.Equals("침실"))
                return 9;
            else if (locName.Equals("욕실"))
                return 10;
            else if (locName.Equals("서재"))
                return 11;
            else
                return -1;
        }

        public string GetNameByNum(int num) // 번호로 카드 명칭 반환
        {
            if (num.Equals(2))
                return "중앙홀";
            else if (num.Equals(3))
                return "식당";
            else if  (num.Equals(4))
                return "부엌";
            else if  (num.Equals(5))
                return "거실";
            else if  (num.Equals(6))
                return "마당";
            else if  (num.Equals(7))
                return "차고";
            else if  (num.Equals(8))
                return "게임룸";
            else if  (num.Equals(9))
                return "침실";
            else if  (num.Equals(10))
                return "욕실";
            else if  (num.Equals(11))
                return "서재";
            else
                return "복도";
        }

        public string GetNameByCoor((int, int) _position) //좌표로 카드 명칭 반환
        {
            if (_position.Equals((9, 11)))
                return "중앙홀";
            else if (_position.Equals((4, 11)))
                return "식당";
            else if  ( _position.Equals((3, 6)) )
                return "부엌";
            else if  (_position.Equals((3, 17)) )
                return "거실";
            else if  (_position.Equals((10, 20)))
                return "마당";
            else if  (_position.Equals((16, 17)))
                return "차고";
            else if  (_position.Equals((16, 11)))
                return "게임룸";
            else if  (_position.Equals((16, 4)))
                return "침실";
            else if  (_position.Equals((13, 2)))
                return "욕실";
            else if  (_position.Equals((10, 4)))
                return "서재";
            else
                return "error";
        }

        public List<(int, int)> FindShortestPath(int[,] grid, (int, int) start, (int, int) goal)
        {
            //bfs
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int[] dRow = { -1, 1, 0, 0 };
            int[] dCol = { 0, 0, -1, 1 };
            Queue<((int, int), List<(int, int)>)> queue = new Queue<((int, int), List<(int, int)>)>();
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            queue.Enqueue((start, new List<(int, int)> { start }));
            visited.Add(start);

            while (queue.Count > 0)
            {
                var (current, path) = queue.Dequeue();
                int x = current.Item1;
                int y = current.Item2;

                // 목표 지점에 도달하면 경로 반환
                if (current.Equals(goal))
                {
                    return path;
                }

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dRow[i];
                    int ny = y + dCol[i];

                    // 방문한 노드인지 확인
                    if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && !visited.Contains((nx, ny)))
                    {
                        // 벽이면 안찾음!
                        if (grid[nx, ny] == 1)
                            continue;

                        List<(int, int)> newPath = new List<(int, int)>(path) { (nx, ny) };

                        queue.Enqueue(((nx, ny), newPath));
                        visited.Add((nx, ny));
                    }
                }
            }
            return null; // 경로를 찾지 못한 경우
        }

        public int GetRandomNum(List<Card> _allCard)    // 알고 있는 장소 제외 랜덤 장소 좌표 RETURN
        {
            List<Card> tempLocCard = new List<Card>();

            for (int i = 0; i < _allCard.Count; i++)
            {
                if (_allCard[i].GetCardType().Equals(CardType.LOC) &&
                    !knowLocCard.Any(locCard => locCard.GetName().Equals(_allCard[i].GetName())))
                {
                    tempLocCard.Add(_allCard[i]);
                }
            }

            Random rand = new Random();
            int randNum = rand.Next(0, tempLocCard.Count);

            return GetLocByName(tempLocCard[randNum].GetName());
        }

        public bool CheckThisKnow()
        {
            int thisLocNum = GetLocByCoor(this.position);
            bool checkKnow = false;
            for (int i = 0; i < this.knowLocCard.Count; i++)
            {
                //현재 장소가 아는정보인지 확인
                if (thisLocNum.Equals(GetLocByName(knowLocCard[i].GetName())))
                {
                    checkKnow = true;
                    break;
                }
            }
            return checkKnow;
        }   //현재 위치 장소 아는 카드인지 확인

        public void Guess( GameManager _instance)   //추리
        {
            List<Card> _allCard = _instance.GetAllCard();
            int thisLocNum = GetLocByCoor(this.position);

            List<Card> unknowWepCard = new List<Card>(); // 모르는 wep 카드 리스트
            List<Card> unknowPerCard = new List<Card>(); // 모르는 per 카드 리스트
            List<Card> allKnowCard = new List<Card>();   // 전체 아는 카드 리스트

            // 전체 아는 카드 리스트를 만듬
            allKnowCard.AddRange(knowLocCard);
            allKnowCard.AddRange(knowPerCard);
            allKnowCard.AddRange(knowWepCard);

            Card thisLocCard = null;  // 현재 위치의 장소 카드
            List<Card> newGuessCard = new List<Card>(); // 새로 뽑은 추리 카드

            for (int i = 0; i < _allCard.Count; i++)
            {
                bool isKnown = false;

                for (int j = 0; j < allKnowCard.Count; j++)
                {
                    if (_allCard[i].Equals(allKnowCard[j]))
                    {
                        isKnown = true;
                        break;
                    }
                }

                if (!isKnown)
                {
                    if (_allCard[i].GetCardType().Equals(CardType.PER) && !unknowPerCard.Contains(_allCard[i]))
                        unknowPerCard.Add(_allCard[i]);
                    else if (_allCard[i].GetCardType().Equals(CardType.WEP) && !unknowWepCard.Contains(_allCard[i]))
                        unknowWepCard.Add(_allCard[i]);
                }

                if (_allCard[i].GetName().Equals(GetNameByCoor(this.position)))
                    thisLocCard = _allCard[i];
            }

            if (thisLocCard != null)
                newGuessCard.Add(thisLocCard);

            if (unknowWepCard.Count > 0)
            {
                Random rand = new Random();
                int wepNum = rand.Next(0, unknowWepCard.Count);
                newGuessCard.Add(unknowWepCard[wepNum]);
            }

            if (unknowPerCard.Count > 0)
            {
                Random rand = new Random();
                int perNum = rand.Next(0, unknowPerCard.Count);
                newGuessCard.Add(unknowPerCard[perNum]);
            }

            _instance.SetGuessCard(newGuessCard);
        }

        public bool Prove( GameManager _instance)   //증명할수 있는지 체크
        {
            bool isProov = false;
            List<Card> guessCardList = _instance.GetGuessCard();
            List<Card> thisCardList = GetCardList();

            //컴퓨터 증명
            //가지고있는 카드가 guess카드에 1개 이상있으면
            for(int i=0; i <  thisCardList.Count ; i++ )
            {
                for(int j = 0; j < guessCardList.Count ; j++)
                {
                    if(thisCardList[i].Equals(guessCardList[j]))
                        isProov = true;
                }
            }
            return isProov;
        }

        public Card GetProvCard(GameManager _instance)  //증명할 카드
        {
            List<Card> guessCardList = _instance.GetGuessCard();
            List<Card> thisCardList = GetCardList();
            List<Card> sameCardList = new List<Card>();

            for(int i=0; i < thisCardList.Count ; i++ )
            {
                for(int j = 0; j < guessCardList.Count ; j++)
                {
                    if(thisCardList[i].Equals(guessCardList[j]))
                        sameCardList.Add(thisCardList[i]);
                }
            }

            Random rand = new Random();
            int num = rand.Next(0, sameCardList.Count);

            return sameCardList[num];
        }

        public bool CheckCanFinal(GameManager _instance) //최종 추리 가능한지 여부 (모르는 카드 각 2개 이하)
        {
            bool canFinal = false;

            List<Card> allCard = _instance.GetAllCard();
            List<Card> unknownCards = new List<Card>();

             for (int i = 0; i < allCard.Count; i++)
            {
                Card card = allCard[i];
                if (!knowLocCard.Contains(card) && !knowWepCard.Contains(card) && !knowPerCard.Contains(card))
                {
                    unknownCards.Add(card);
                }
            }

            int unknownLocCount = 0;
            int unknownWepCount = 0;
            int unknownPerCount = 0;

            for (int i = 0; i < unknownCards.Count; i++)
            {
                Card card = unknownCards[i];
                switch (card.GetCardType())
                {
                    case CardType.LOC:
                        unknownLocCount++;
                        break;
                    case CardType.WEP:
                        unknownWepCount++;
                        break;
                    case CardType.PER:
                        unknownPerCount++;
                        break;
                }
            }

            if(unknownLocCount <=2)
            {
                if(unknownPerCount <=2)
                {
                    if(unknownWepCount <= 2)
                        canFinal = true;
                    else
                        canFinal = false;
                }
                else
                    canFinal = false;
            }
            else
                canFinal = false;

            return canFinal;
        }

        public void ComMove(GameManager _instance, User user, Com com1, Com com2, Com com3)
        {
            int mvCount = this.GetMoveCount();
            for (int i = 0; i < mvCount ; i++)
            {
                if (this.goPath.Count == 0)
                {
                    break;
                    //도착하면 멈춤
                }

                this.position = this.goPath.Dequeue();
                //맵 출력
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(300);

                Init.ViewMap(_instance, user, com1, com2, com3);
                _instance.ViewRoomLabel();
                //this.SetMoveCount(this.GetMoveCount() - 1);
                //this.ViewUserState(true);
            }
        }

        public List<Card> FinalGuess(GameManager _instance) //최종추리카드 return
        {   
            List<Card> finalCard = new List<Card>();
            List<Card> tempAllCard = _instance.GetAllCard();

            for (int i = 0; i < tempAllCard.Count; i++)
            {
                bool isKnown = false;
                CardType type = tempAllCard[i].GetCardType();

                if (type.Equals(CardType.LOC))
                {
                    for (int j = 0; j < knowLocCard.Count; j++)
                    {
                        if (tempAllCard[i].Equals(knowLocCard[j]))
                        {
                            isKnown = true;
                            break;
                        }
                    }
                }
                else if (type.Equals(CardType.PER))
                {
                    for (int j = 0; j < knowPerCard.Count; j++)
                    {
                        if (tempAllCard[i].Equals(knowPerCard[j]))
                        {
                            isKnown = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < knowWepCard.Count; j++)
                    {
                        if (tempAllCard[i].Equals(knowWepCard[j]))
                        {
                            isKnown = true;
                            break;
                        }
                    }
                }

                if (!isKnown && !finalCard.Contains(tempAllCard[i]))
                    finalCard.Add(tempAllCard[i]);
            }
            return finalCard;
        }

        public int CheckFinalGuess(GameManager _instance, List<Card> fCards)    //맞은 갯수 return
        {
            List<Card> corrCard = _instance.GetCorrCards();

            int checkCorrect = 0;

            for(int i =0; i<corrCard.Count; i++)
            {
                for(int j = 0; j <fCards.Count; j++)
                {
                    if(corrCard[i].Equals(fCards[j]))
                    {
                        checkCorrect++;
                    }
                }
            }

            return checkCorrect;
        }

         public void ViewFinalGuessIntro(int player)
        {
            Console.Clear();
            Console.SetCursorPosition(50, 20);
            Console.WriteLine($" [ COM {player} 가 최종 추리를 합니다. ] ");

            Console.ReadLine();
            Console.Clear();
        }

        public void ViewFinalGuessCheckIntro(int player, bool check)
        {
            Console.Clear();
            Console.SetCursorPosition(42, 20);
            
            if(check)
            {
                Console.WriteLine($" [ COM {player} 의 최종추리 가 맞았습니다. ] ");
            }
            else
            {
                Console.WriteLine($" [ COM {player} 의 최종추리 가 틀렸습니다.] ");
                Console.SetCursorPosition(42, 25);
                Console.WriteLine($"    COM {player} 이 게임에 탈락합니다.  ");
                Console.SetCursorPosition(42, 27);
                Console.WriteLine($"  * 탈락하더라도 증명에는 계속 참여합니다.  ");
            }
        }
    }
}
