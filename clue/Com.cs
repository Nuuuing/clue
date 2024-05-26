using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Com : Player
    {
        List<Card> knowLocCard = new List<Card>();    //알고있는 카드 정보
        List<Card> knowWepCard = new List<Card>();
        List<Card> knowPerCard = new List<Card>();
        int checkLoc;           //현재 찾고있는 장소
        public Queue<(int, int)> goPath = new Queue<(int, int)>();   //가고있는 루트

        public Com(List<Card> startCard)
        {
            addCard(startCard);
        }

        public bool CheckGoLocation()   //새로운 장소를 찾아야 하는지 여부
        {
            bool check = true;
            if (knowLocCard != null)
            {
                for (int i = 0; i < knowLocCard.Count; i++)
                {
                    if (knowLocCard[i].getLocNum() == checkLoc)
                    {
                        //알고있는 정보중에 현재 가고있는 장소가 포함되면
                        return true;
                    }
                }
            }
            return check;
        }

        public void AddKnowCard(List<Card> addCardList)
        {
            for (int i = 0; i < addCardList.Count; i++)
            {
                if (addCardList[i].getCardType().Equals(CardType.LOC))
                    knowLocCard.Add(addCardList[i]);
                else if(addCardList[i].getCardType().Equals(CardType.PER))
                    knowPerCard.Add(addCardList[i]);
                else
                    knowWepCard.Add(addCardList[i]);
            }
        }

        public void AddKnowCard(Card addCard)
        {
            if (addCard.getCardType().Equals(CardType.LOC))
                knowLocCard.Add(addCard);
            else if (addCard.getCardType().Equals(CardType.PER))
                knowPerCard.Add(addCard);
            else
                knowWepCard.Add(addCard);
        }

        public bool CheckIsLoc()    //현재 위치가 장소타일인지 확인, but 2일때도 false
        {
            bool isLoc = false;
            //2이면 false
            if (GetLocByCoor(position) != 2 && GetLocByCoor(position) != -1)
            {
                isLoc = true;
            }
            return isLoc;
        }

        public int GetLocByCoor((int, int) _position) //좌표로 장소번호 return
        {
            if (_position == (8, 12) || _position == (9, 11) || _position == (9, 13) || _position == (11, 12))
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
                    return -1;
            }
        }

        public (int, int) GetCoorByNum(int num) //장소번호로 좌표 return
        {
            if (num != 2) //2는 4방향
            {
                if (num == 3)
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
            else
            {
                return (0, 0);
            }
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

        public string GetNameByCoor((int, int) _position) //좌표로 카드 명칭 반환
        {
            if (_position == (8, 12) || _position == (9, 11) || _position == (9, 13) || _position == (11, 12))
            {
                return "중앙홀";
            }
            else
            {
                if (_position == (4, 11))
                    return "식당";
                if (_position == (3, 6))
                    return "부엌";
                if (_position == (3, 17))
                    return "거실";
                if (_position == (10, 20))
                    return "마당";
                if (_position == (16, 17))
                    return "차고";
                if (_position == (16, 11))
                    return "게임룸";
                if (_position == (16, 4))
                    return "침실";
                if (_position == (13, 2))
                    return "욕실";
                if (_position == (10, 4))
                    return "서재";
                else
                    return "error";
            }
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
                if (current == goal)
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
                        // Skip walls
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

        public (int, int) GetRandomCoor(List<Card> _allCard)
        {
            List<Card> tempLocCard = new List<Card>();

            //알고있는 장소말고 다른장소 랜덤 좌표
            for (int i = 0; i < _allCard.Count; i++)
            {
                for (int j = 0; j < knowLocCard.Count; j++)
                {
                    if (_allCard[i].getCardType().Equals(CardType.LOC))
                    {
                        if (!_allCard[i].getName().Equals(knowLocCard[j].getName()))
                        {
                            tempLocCard.Add(_allCard[i]);
                        }
                    }
                }
            }

            Random rand = new Random();
            int randNum = rand.Next(0, tempLocCard.Count);

            return GetCoorByNum(GetLocByName(tempLocCard[randNum].getName()));
        }

        public void Guess( GameManager _instance)
        {
            List<Card> _allCard = _instance.GetAllCard();
            int thisLocNum = GetLocByCoor(this.position);
            bool checkKnow = false;
            for (int i = 0; i < this.knowLocCard.Count; i++)
            {
                //현재 장소가 아는정보인지 확인
                //TODO:현재 장소가 아는정보면 return을 뭐로하지
                if (thisLocNum.Equals(GetLocByName(knowLocCard[i].getName())))
                {
                    checkKnow = true;
                    break;
                }
            }
            if (!checkKnow)
            {
                List<Card> unknowWepCard = new List<Card>(); //모르는 wep 카드 List
                List<Card> unknowPerCard = new List<Card>(); //모르는 per 카드 List
                List<Card> allKnowCard = new List<Card>();

                allKnowCard = knowLocCard;
                for(int i = 0; i < knowPerCard.Count; i++)
                {
                    allKnowCard.Add(knowPerCard[i]);
                }
                for(int i = 0; i< knowWepCard.Count; i ++)
                {
                    allKnowCard.Add(knowWepCard[i]);
                }

                Card thisLocCard;   //현재 위치의 장소카드
                List<Card> newGuessCard = new List<Card>();//새로 뽑은 추리 카드

                for (int i = 0; i < _allCard.Count; i++)
                {
                    for (int j = 0; j < allKnowCard.Count; j++)
                    {
                        if (!_allCard[i].Equals(allKnowCard[j].getName()))
                        {
                            if(_allCard[i].getCardType().Equals(CardType.PER))
                            {
                                unknowPerCard.Add(_allCard[i]);
                            }
                            else if(_allCard[i].getCardType().Equals(CardType.WEP))
                            {
                                unknowWepCard.Add(_allCard[i]);
                            }
                        }
                    }
                    if(_allCard[i].getName().Equals(GetNameByCoor(this.position)))
                    {
                        thisLocCard = _allCard[i];
                        newGuessCard.Add(thisLocCard);
                    }
                }

                Random rand = new Random();
                int wepNum = rand.Next(0, unknowWepCard.Count);
                int perNum = rand.Next(0, unknowPerCard.Count);

                newGuessCard.Add(unknowWepCard[wepNum]);
                newGuessCard.Add(unknowPerCard[perNum]);

                _instance.SetGuessCard(newGuessCard);
            }
        }
    }
}
