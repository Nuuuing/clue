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
        private GameManager() {}
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

        public void ViewSystemComment(List<string> message, int typeFlag = 0)
        {
            Console.SetCursorPosition(0,25);
            for(int i = 0; i < 60; i++)
            {
                Console.Write("ㅡ");
            }

            Console.SetCursorPosition(18, 28);
             if(typeFlag == 0)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" [ SYSTEM ] ");
                Console.ResetColor();
            }
             else if(typeFlag == 1) //warning
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

        public void menuClear()
        {
            Console.SetCursorPosition(86, 27);
            Console.WriteLine("                                        ");

            Console.SetCursorPosition(88, 30);
            Console.WriteLine("                                        ");

            Console.SetCursorPosition(88, 32);
            Console.WriteLine("                                        ");
            
            Console.SetCursorPosition(88, 34);
            Console.WriteLine("                                        ");
            
            Console.SetCursorPosition(88, 36);
            Console.WriteLine("                                        ");
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

        public void ViewSystemDescription()
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
        //----------------일반 메뉴 START--------------------

        public void ViewMenuLabel( int menuFlag )
        {
            if(menuFlag ==1)    //2번에 위치했을때
            {
                Console.SetCursorPosition(86, 27);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [ MENU ] ");
                Console.ResetColor();
                Console.SetCursorPosition(88, 30);
                Console.WriteLine(" 이동하기       ");
                Console.SetCursorPosition(88, 32);
                Console.WriteLine(" 최종추리하기      ");
                }
            else if(menuFlag ==2) // 0번에 위치했을때
            {
                Console.SetCursorPosition(86, 27);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [ MENU ] ");
                Console.ResetColor();
                Console.SetCursorPosition(88, 30);
                Console.WriteLine(" 이동하기        ");
                Console.SetCursorPosition(88, 32);
                Console.WriteLine(" 추리하기       ");
            }
            else if(menuFlag ==3)
            {
                Console.SetCursorPosition(86, 27);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [ MENU ] ");
                Console.ResetColor();
                Console.SetCursorPosition(88, 30);
                Console.WriteLine(" 위쪽       ");
                Console.SetCursorPosition(88, 32);
                Console.WriteLine(" 아래쪽       ");
                Console.SetCursorPosition(88, 34);
                Console.WriteLine(" 왼쪽         ");
                Console.SetCursorPosition(88, 36);
                Console.WriteLine(" 오른쪽        ");                
            }
            else if(menuFlag ==4)
            {
                Console.SetCursorPosition(86, 27);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [ MENU ] ");
                Console.ResetColor();
                Console.SetCursorPosition(88, 30);
                Console.WriteLine(" 이동하기       ");
            }
            else
            {
                Console.SetCursorPosition(85, 27);
                Console.WriteLine("               ");
                Console.SetCursorPosition(85, 30);
                Console.WriteLine("               ");
                Console.SetCursorPosition(85, 32);
                Console.WriteLine("               ");
                Console.SetCursorPosition(85, 34);
                Console.WriteLine("               ");
                Console.SetCursorPosition(85, 36);
                Console.WriteLine("               ");
            }
            Console.WriteLine("");
        }
        void ViewMenuCursor(int menuFlag , int thisCursor)
        {
            {
                Console.SetCursorPosition(86, 30);
                Console.Write("   ");
                Console.SetCursorPosition(86, 32);
                Console.Write("   ");
                Console.SetCursorPosition(86, 34);
                Console.Write("   ");
                Console.SetCursorPosition(86, 36);
                Console.Write("   ");
            }
            if(menuFlag == 3)
            {
                if(thisCursor ==0)
                {
                    Console.SetCursorPosition(86, 30);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ► ");

                }
                else if(thisCursor ==1)
                {
                    Console.SetCursorPosition(86, 32);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ► ");
                }
                else if(thisCursor == 2)
                {
                    Console.SetCursorPosition(86, 34);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ► ");
                }
                else
                {
                    Console.SetCursorPosition(86, 36);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ► ");
                }
            }
            else if(menuFlag == 1 || menuFlag ==2 )
            {
                if (thisCursor == 0)
                {
                    Console.SetCursorPosition(86, 30);
                    Console.Write(" ► ");
                }
                else
                {
                    Console.SetCursorPosition(86, 32);
                    Console.Write(" ► ");
                }
            }
            else if(menuFlag == 4)
            {
                Console.SetCursorPosition(86, 30);
                Console.Write(" ► ");
            }
        }
        public int ChooseUserMenu(int menuFlag, int defaultNum = 0)
        {
            int menuNum = 0;
            menuNum = defaultNum;
            ConsoleKey key;

            if (menuFlag == 3)
            {
                bool returnTrue = false;

                while (true)
                {
                    ViewMenuCursor(menuFlag, menuNum);
                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (menuNum == 0) menuNum=3;
                            else menuNum--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (menuNum == 3) menuNum = 0;
                            else menuNum++;
                            break;
                        case ConsoleKey.Enter:
                            returnTrue = true;
                            break;
                    }

                   if(returnTrue)
                    {
                        break;
                    }
                }
            }
            else if(menuFlag == 1 || menuFlag == 2)
            {
                bool returnTrue = false;

                while (true)
                {
                    ViewMenuCursor(menuFlag, menuNum);
                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            if (menuNum == 0) menuNum = 1;
                            else menuNum--;
                            break;
                        case ConsoleKey.DownArrow:
                            if (menuNum == 1) menuNum = 0;
                            else menuNum++;
                            break;
                        case ConsoleKey.Enter:
                            returnTrue = true;
                            break;
                    }

                    if (returnTrue)
                    {
                        break;
                    }
                }
            }
            else if( menuFlag == 4)
            {
                bool returnTrue = false;

                while (true)
                {
                    ViewMenuCursor(menuFlag, menuNum);
                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            returnTrue = true;
                            break;
                    }

                    if (returnTrue)
                        break;
                }
            }
            return menuNum;
        }

        //------------------일반 메뉴 END---------------------

        //------------------추리 메뉴 START---------------------

        //TODO: 메뉴 선택 깔끔하게 수정하기

        public void ViewUserGuessMenu(List<Card> guessCards, int menuFlag)
        {
            if(menuFlag == 0)
            {
                Console.SetCursorPosition(86, 27);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [ 장소카드 선택 ] ");
                Console.ResetColor();

                int a = 28;
                for (int i = 0; i < guessCards.Count; i++)
                {
                    if(i%2 == 0)
                    {
                        a = a + 2;
                        Console.SetCursorPosition(88, a);
                    }
                    else
                    {
                        Console.SetCursorPosition(96, a);
                    }
                    Console.WriteLine($" {guessCards[i].GetName()}");
                }

                Console.SetCursorPosition(88, a + 2);
                Console.WriteLine(" 돌아가기 ");
            }
            else
            {
                if (menuFlag == 1 )
                {
                    Console.SetCursorPosition(86, 27);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" [ 인물 카드 선택 ] ");
                    Console.ResetColor();
                }
                else
                {
                    Console.SetCursorPosition(86, 27);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" [ 무기 카드 선택 ] ");
                    Console.ResetColor();
                }
                int a = 28;
                for (int i = 0; i < guessCards.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        a = a + 2;
                        Console.SetCursorPosition(88, a);
                    }
                    else
                    {
                        Console.SetCursorPosition(96, a);
                    }
                    Console.WriteLine($" {guessCards[i].GetName()}");
                }
                
                Console.SetCursorPosition(88, a+2);
                Console.WriteLine(" 돌아가기 ");
            }
        }
        void ViewUserGuessCursor( int thisCursor, int count)
        {
            {
                int a = 28;
                for (int i = 0; i < count; i++)
                {
                    if (i % 2 == 0)
                    {
                        a = a + 2;
                        Console.SetCursorPosition(85, a);
                    }
                    else
                    {
                        Console.SetCursorPosition(93, a);
                    }
                    Console.Write("   ");
                }
            }

            if(thisCursor == 0)
            {
                Console.SetCursorPosition(85, 30);
                Console.Write(" ► ");
            }
            else if(thisCursor == 1)
            {
                Console.SetCursorPosition(93, 30);
                Console.Write(" ► ");
            }
            else if(thisCursor == 2)
            {
                Console.SetCursorPosition(85, 32);
                Console.Write(" ►  ");
            }
            else if(thisCursor == 3)
            {
                Console.SetCursorPosition(93, 32);
                Console.Write(" ►  ");
            }
            else if(thisCursor == 4)
            {
                Console.SetCursorPosition(85, 34);
                Console.Write(" ►  ");
            }
            else if(thisCursor == 5)
            {
                Console.SetCursorPosition(93, 34);
                Console.Write(" ►  ");
            }
            else if(thisCursor == 6)
            {
                Console.SetCursorPosition(85, 36);
                Console.Write(" ►  ");
            }
            else if(thisCursor == 7)
            {
                Console.SetCursorPosition(93, 36);
                Console.Write(" ►  ");
            }
            else
            {
                Console.SetCursorPosition(85, 38);
                Console.Write(" ►  ");
            }
        }
        public int ChooseUserGuessMenu(List<Card> cardList , int defaultNum = 0)
        {
            int menuNum = 0;
            menuNum = defaultNum;
            ConsoleKey key;
            bool returnTrue = false;
            int cardCount = cardList.Count;

            while (true)
            {
                ViewUserGuessCursor(menuNum, cardCount);
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (menuNum == 0) menuNum = cardCount+1;
                        else if (menuNum == 1) menuNum = cardCount;
                        else menuNum -= 2;
                        break;
                    case ConsoleKey.DownArrow:
                        if (menuNum == cardCount+1) menuNum = 0;
                        else if (menuNum == cardCount) menuNum = 1;
                        else menuNum +=2;
                        break;
                    case ConsoleKey.RightArrow:
                        if (menuNum == cardCount+1) menuNum = 0;
                        else if (menuNum %2 == 1) menuNum -= 1;
                        else menuNum++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (menuNum == cardCount+1) menuNum = cardCount;
                        else if (menuNum %2 == 0) menuNum += 1;
                        else menuNum--;
                        break;
                    case ConsoleKey.Enter:
                        returnTrue = true;
                        break;
                }

                if (returnTrue)
                {
                    break;
                }
            }

            return menuNum;
        }

        //------------------추리 메뉴 END---------------------

        //------------------유저 검증 메뉴 START---------------------

        public void ViewCardSelectMenu()
        {
            Console.SetCursorPosition(86, 27);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(" [ 증명할 카드 선택 ] ");
            Console.ResetColor();

            int a = 28;
            for (int i = 0; i < userCard.Count; i++)
            {
                if (i % 2 == 0)
                {
                    a = a + 2;
                    Console.SetCursorPosition(88, a);
                }
                else
                {
                    Console.SetCursorPosition(96, a);
                }
                Console.WriteLine($" {userCard[i].GetName()}");
            }

            Console.SetCursorPosition(88, a + 2);
            Console.WriteLine(" 증명 포기하기 ");
        }
        public void ViewCardSelectCursor(int thisCursor)
        {
            {
                int a = 28;
                for (int i = 0; i < userCard.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        a = a + 2;
                        Console.SetCursorPosition(85, a);
                    }
                    else
                    {
                        Console.SetCursorPosition(93, a);
                    }
                    Console.Write("   ");
                }
            }

            if (thisCursor == 0)
            {
                Console.SetCursorPosition(85, 30);
                Console.Write(" ► ");
            }
            else if (thisCursor == 1)
            {
                Console.SetCursorPosition(93, 30);
                Console.Write(" ► ");
            }
            else if (thisCursor == 2)
            {
                Console.SetCursorPosition(85, 32);
                Console.Write(" ►  ");
            }
            else if (thisCursor == 3)
            {
                Console.SetCursorPosition(93, 32);
                Console.Write(" ►  ");
            }
            else if (thisCursor == 4)
            {
                Console.SetCursorPosition(85, 34);
                Console.Write(" ►  ");
            }
            else if (thisCursor == 5)
            {
                Console.SetCursorPosition(93, 34);
                Console.Write(" ►  ");
            }
        }

        public int ChooseCardSelect()
        {
            List<Card> thisGuess = _instance.GetGuessCard();

            int menuNum = 0;
            ConsoleKey key;
            int cardCount = userCard.Count;
            bool returnTrue = false;

            ViewCardSelectMenu();

            bool checkNo = false;

            while (true)
            {
                if(returnTrue==true && checkNo==false)
                {
                    break;
                }

                ViewCardSelectCursor(menuNum);
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (menuNum == 0) menuNum = cardCount + 1;
                        else if (menuNum == 1) menuNum = cardCount;
                        else menuNum -= 2;
                        break;
                    case ConsoleKey.DownArrow:
                        if (menuNum == cardCount + 1) menuNum = 0;
                        else if (menuNum == cardCount) menuNum = 1;
                        else menuNum += 2;
                        break;
                    case ConsoleKey.RightArrow:
                        if (menuNum == cardCount + 1) menuNum = 0;
                        else if (menuNum % 2 == 1) menuNum -= 1;
                        else menuNum++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (menuNum == cardCount + 1) menuNum = cardCount;
                        else if (menuNum % 2 == 0) menuNum += 1;
                        else menuNum--;
                        break;
                    case ConsoleKey.Enter:
                        returnTrue = true;
                        break;
                }

                if(menuNum == cardCount)
                {
                    break;
                }
                else if (returnTrue && (menuNum != cardCount))
                {
                    for (int i = 0; i < thisGuess.Count; i++)
                    {
                        if (thisGuess[i].Equals(this.userCard[menuNum]))
                        {
                            checkNo = true;
                        }
                    }
                    //TODO: 해당카드는 현재 추리한 카드에 포함되어있지않아 사용할수 X ALERT
                    continue;
                }
                    //TODO: 증명 포기 메뉴 추가하기
            }
            return menuNum;
        }

        public Card UserProv()
        {
            int chooseNum = ChooseCardSelect();
            if (chooseNum == userCard.Count)
            {
                Card noCard = new Card(0, CardType.LOC, "temp");
                return noCard;
            }
            else
            {
                return this.userCard[chooseNum];
            }
        }

        //------------------유저 검증 메뉴 END---------------------
        public void ViewUserCardList()
        {
            Console.SetCursorPosition(60, 12);
            for(int i= 0; i< 55; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(75, 14);
            Console.WriteLine(" [ 유저 의 카드 목록 ]");

            Console.SetCursorPosition(62, 16);
            for(int i =0; i<userCard.Count; i++)
            {
                if( (i == 3) )
                { 
                    Console.SetCursorPosition(62, 18);
                }
                Console.Write($"[ {userCard[i].GetTypeString()} : {userCard[i].GetName() } ] ");
            }
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
