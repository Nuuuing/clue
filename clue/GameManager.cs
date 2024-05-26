using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class GameManager
    {
        private static GameManager _instance;

        private List<Card> allCardList = new List<Card>();  //전체 카드 목록
        private List<Card> corrCard = new List<Card>();     //정답 카드
        public int[,] map;                                  //전체 맵 세팅
        public int turn;                                    //현재 턴 (0:유저,1:com1, 2:com2, 3:com3)

        private List<Card> userCard = new List<Card>();
        private List<Card> com1Card = new List<Card>();
        private List<Card> com2Card = new List<Card>();
        private List<Card> com3Card = new List<Card>();

        public bool Running = true;

        public List<Card> getStartCardList(int num)
        {
            if (num == 0)
            {
                return userCard;
            }
            else if (num == 1)
            {
                return com1Card;
            }
            else if (num == 2)
            {
                return com2Card;
            }
            else
            {
                return com3Card;
            }
        }

        private GameManager() { }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        void settingPlayerCard()
        {
            for (int i = 0; i < allCardList.Count; i++)
            {
                switch (i % 4)
                {
                    case 0:
                        userCard.Add(allCardList[i]);
                        break;
                    case 1:
                        com1Card.Add(allCardList[i]);
                        break;
                    case 2:
                        com2Card.Add(allCardList[i]);
                        break;
                    case 3:
                        com3Card.Add(allCardList[i]);
                        break;
                }
            }
        }

        void Shuffle<T>(List<T> list)
        {
            Random rand = new Random();
            for(int i = 0; i < list.Count; i++)
            {
                int j = rand.Next(i, list.Count);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }    
        }

        public void StartGame()
        {
            string[] cardName = new string[21]
            {
                "스칼렛", "화이트", "플럼", "그린", "머스타드", "피콕",
                "단검", "쇠파이프", "리볼버", "밧줄", "촛대", "렌치",
                "식당", "부엌", "거실", "마당", "차고", "게임룸", "침실", "욕실", "서재"
            };

            for (int i = 0; i < 6; i++)
            {
                allCardList.Add(new Card(i, CardType.PER, cardName[i]));
            }   //인물카드

            for (int i = 0; i < 6; i++)
            {
                allCardList.Add(new Card(i + 6, CardType.WEP, cardName[i + 6]));
            }   //무기카드

            for (int i = 0; i < 9; i++)
            {
                allCardList.Add(new Card(i + 12, CardType.LOC, cardName[i + 12]));
            }   //장소카드

            Random rand = new Random();
            int num;
            int checkTime = 0;

            Shuffle(allCardList);

            while (checkTime < 3)
            {
                num = rand.Next(0, 22);
                Card tempCard = allCardList[num];

                if (!corrCard.Any())
                {
                    corrCard.Add(tempCard);
                    allCardList.Remove(tempCard);
                    checkTime++;
                }
                else
                {
                    for (int i = 0; i < corrCard.Count; i++)
                    {
                        if (!corrCard[i].getCardType().Equals(tempCard.getCardType()))
                        {
                            corrCard.Add(tempCard);
                            allCardList.Remove(tempCard);
                            checkTime++;
                            break;
                        }
                    }
                }
            }

            settingPlayerCard();
        }

        public int rollDice() //주사위 두개 굴리기
        {
            int diceCount;

            Random rand = new Random();

            int dice1 = rand.Next(1, 7);
            int dice2 = rand.Next(1, 7);

            diceCount = dice1 + dice2;

            return diceCount;
        }

        public void EndGame()
        {
            Running = false;
        }
    }
}
