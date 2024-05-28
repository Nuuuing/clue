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
        private int guessUser = 0;
        private List<Card> userCard = new List<Card>();
        private List<Card> com1Card = new List<Card>();
        private List<Card> com2Card = new List<Card>();
        private List<Card> com3Card = new List<Card>();
        private int winner;

        private List<Card> thisGuessCard = new List<Card>(); //현재 진행중인 추리 카드

        public bool Running = true; //게임 진행중 flag
        private GameManager() { 
      
        }

        public void SetGuessUser(int num)
        {
            guessUser = num;
        }

        public void SetWinner(int num)
        {
            winner = num;
        }

        public int GetWinner()
        {
            return winner;
        }

        public int GetGuessUser()
        {
            return guessUser;
        }

        public List<Card> GetCorrCards()
        {
            return corrCard;
        }

        public List<Card> GetStartCardList(int num) //유저 번호 -> 해당 유저 카드 목록 return
        {
            if (num == 0)
                return userCard;
            else if (num == 1)
                return com1Card;
            else if (num == 2)
                return com2Card;
            else
                return com3Card;
        }

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

        void SettingPlayerCard()    //전체 카드목록 각 유저에게 나누어줌
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

        public void ViewSystem(List<string> message, int typeFlag = 0)
        {
            Console.SetCursorPosition(0,25);
            for(int i = 0; i < 60; i++)
            {
                Console.Write("ㅡ");
            }

            Console.SetCursorPosition(18, 28);
             if(typeFlag == 0)
            {
                Console.WriteLine(" [ SYSTEM ] ");
            }
             else if(typeFlag == 1)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" [ SYSTEM ] ");
                Console.ResetColor();
            }

             for(int i = 0 ; i < message.Count; i++)
            {
                Console.SetCursorPosition(12, 31+i);
                if(typeFlag == 0) //일반 시스템
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(message[i]);
                    Console.ResetColor();
                }
                else if(typeFlag == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message[i]);
                    Console.ResetColor();
                }
            }

        }

        public void ViewMenu( int menuFlag )
        {
            Console.SetCursorPosition(86, 28);
            Console.WriteLine(" [ MENU ] ");

            if(menuFlag ==1)    //2번에 위치했을때
            {   
                Console.SetCursorPosition(88, 31);
                Console.WriteLine(" 이동하기 ");
                Console.SetCursorPosition(88, 33);
                Console.WriteLine(" 최종추리하기 ");
                }
            else if(menuFlag ==2) // 0번에 위치했을때
            { 
                Console.SetCursorPosition(88, 31);
                Console.WriteLine(" 이동하기 ");
                Console.SetCursorPosition(88, 33);
                Console.WriteLine(" 추리하기 ");
            }
            else if(menuFlag ==3)
            {
                Console.SetCursorPosition(88, 31);
                Console.WriteLine(" ");

                Console.WriteLine(" 왼쪽 ");
            }
            else
            {
                Console.SetCursorPosition(88, 31);
                Console.WriteLine(" 이동하기 ");
            }

            Console.WriteLine("");
        }

        public void ViewRoomLabel()
        {
            Console.SetCursorPosition(26, 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("식당");
            Console.ResetColor();
            
            Console.SetCursorPosition(12, 3);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("부엌");
            Console.ResetColor();
            
            Console.SetCursorPosition(38, 3);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("거실");
            Console.ResetColor();
            
            Console.SetCursorPosition(8, 7);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("서재");
            Console.ResetColor();
            
            Console.SetCursorPosition(6, 14);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("욕실");
            Console.ResetColor();
            
            Console.SetCursorPosition(8, 18);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("침실");
            Console.ResetColor();
            
            Console.SetCursorPosition(20, 18);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("게임룸");
            Console.ResetColor();
            
            Console.SetCursorPosition(40, 18);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("차고");
            Console.ResetColor();
            
            Console.SetCursorPosition(44, 9);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("마당");
            Console.ResetColor();
        }

        public void ViewSystemNotice()
        {
            Console.SetCursorPosition(60, 1);
            for(int i= 0; i< 55; i++)
            {
                Console.Write("-");
            }

            Console.SetCursorPosition(62, 2);
                                 
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 중앙홀 ");
                                 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 식당 ");
                                 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 부엌 ");
                                 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 거실 ");
                                 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 마당 ");

            Console.SetCursorPosition(62, 3);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 차고 ");
                                 
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 게임룸 ");
                                 
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 침실 ");
                                 
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 욕실 ");
                                 
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("□");
            Console.ResetColor();
            Console.Write(" - 서재 ");

            Console.SetCursorPosition(66, 5);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("●");
            Console.ResetColor();
            Console.Write(" - 플레이어 ");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("▼");
            Console.ResetColor();
            Console.Write(" - COM1 ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("▼");
            Console.ResetColor();
            Console.Write(" - COM2 ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("▼");
            Console.ResetColor();
            Console.Write(" - COM3 ");

            Console.SetCursorPosition(60, 6);
            for(int i= 0; i< 55; i++)
            {
                Console.Write("-");
            }
        }

        public void ViewGameState()
        {
            Console.SetCursorPosition(80, 8);
            Console.WriteLine(" [ 현재 턴 ] ");

                Console.SetCursorPosition(80, 10);
            if(turn==0)
            {
                Console.WriteLine(" 유저 의 턴 ");
            }
            else if(turn ==1)
            {
                Console.WriteLine(" COM1 의 턴 ");
            }
            else if(turn ==2)
            {
                Console.WriteLine(" COM2 의 턴 ");
            }
            else
            {
                Console.WriteLine(" COM3 의 턴 ");
            }
        }

        public void ChooseUserMenu()
        {

        }

        public void ViewUserCardList()
        {
            Console.SetCursorPosition(60, 12);
            for(int i= 0; i< 55; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(75, 14);
            Console.WriteLine(" [ 유저 의 카드 목록 ]");

            Console.SetCursorPosition(63, 17);
            for(int i =0; i<userCard.Count; i++)
            {
                Console.Write($"[ {userCard[i].GetName() } ] ");
            }
        }

        /*
            public static void ViewGameState(User user)
        {

            Console.WriteLine(" [현재 턴] ");
            Console.WriteLine("");
            if(GameManager.Instance.turn ==0)
            {
                Console.WriteLine(" 나의 턴");
            }
            else if(GameManager.Instance.turn ==1)
            {
                Console.WriteLine(" COMPUTER 1의 턴");
            }
            else if(GameManager.Instance.turn ==2)
            {
                Console.WriteLine(" COMPUTER 2의 턴");
            }
            else
            {
                Console.WriteLine(" COMPUTER 3의 턴");
            }
            Console.WriteLine("");
            Console.WriteLine(" [ 나의 현재 위치] ");
            Console.WriteLine("");
            Console.WriteLine($" X: {user.position.Item2 + 1} ,  Y : {user.position.Item1 +1 }");
            Console.WriteLine("");
            Console.WriteLine(" [ 나의 카드 목록 ] ");
            Console.WriteLine("");
            List<Card> userCard = GameManager.Instance.GetStartCardList(0);
            for (int i = 0; i < userCard.Count; i++)
            {
                Console.Write($" {userCard[i].GetName()} ");
            }

            Console.WriteLine("");

            #region 컴퓨터 카드 확인
            /*    
            Console.WriteLine("");
            Console.WriteLine(" [ 컴1 카드 목록 ] ");
            Console.WriteLine("");
            List<Card> com1Card = GameManager.Instance.getStartCardList(1);
            for (int i = 0; i < com1Card.Count; i++)
            {
                Console.Write($" {com1Card[i].getName()} ");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" [ 컴2 카드 목록 ] ");
            Console.WriteLine("");
            List<Card> com2Card = GameManager.Instance.getStartCardList(2);
            for (int i = 0; i < com2Card.Count; i++)
            {
                Console.Write($" {com2Card[i].getName()} ");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(" [ 컴3 카드 목록 ] ");
            Console.WriteLine("");
            List<Card> com3Card = GameManager.Instance.getStartCardList(3);
            for (int i = 0; i < com3Card.Count; i++)
            {
                Console.Write($" {com3Card[i].getName()} ");
            }
            #endregion
        }
    */
        void Shuffle<T>(List<T> list)   //카드 셔플
        {
            Random rand = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int j = rand.Next(i, list.Count);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        public void StartGame() //게임 시작시 카드 세팅 진행
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

            int num;
            int checkTime = 0;

            Shuffle(allCardList);

            while (checkTime < 3)
            {
            Random rand = new Random();
                num = rand.Next(0, allCardList.Count());
                Card tempCard = allCardList[num];

                if (corrCard.Count == 0)
                {
                    corrCard.Add(tempCard);
                   // allCardList.Remove(tempCard);
                   //TODO: 삭제하지말고 나중에 전체 정해지면 삭제하던지 놔두던지 확인하기
                    checkTime++;
                }
                else
                {
                    for (int i = 0; i < corrCard.Count; i++)
                    {
                        if (!corrCard[i].GetCardType().Equals(tempCard.GetCardType()))
                        {
                            corrCard.Add(tempCard);
                            allCardList.Remove(tempCard);
                            checkTime++;
                            break;
                        }
                    }
                }
            }
            SettingPlayerCard();
        }

        public int RollDice() //주사위 두개 굴리기
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

        public List<Card> GetAllCard()
        {
            return allCardList;
        }

        public void SetGuessCard(List<Card> _guess)
        {
            thisGuessCard.Clear();
            for(int i =0; i < _guess.Count; i++)
            {
                thisGuessCard.Add(_guess[i]);
            }
        }
        public List<Card> GetGuessCard()
        {
            return thisGuessCard;
        }
    }
}
