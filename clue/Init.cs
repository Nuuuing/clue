using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Init
    {
        static void SetMap()
        {
            // 2 - 중앙홀, 3 - 식당, 4 - 부엌,  5 - 거실, 6 - 마당, 7 - 차고, 8 - 게임룸, 9 - 침실, 10 - 욕실, 11 - 서재
            
            GameManager.Instance.map = new int[21, 22]{
                { 1 , 1 , 1 , 1 , 1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 },
                { 1 , 4 , 4 , 4 , 4 ,4 ,4 ,4 ,1 ,3 ,3 ,3 ,3 ,3 ,3, 1 ,5 ,5 ,5 ,5 ,5 ,1 },
                { 1 , 4 , 4 , 4 , 4 ,4 ,4 ,4 ,1 ,3 ,3 ,3 ,3 ,3 ,3, 1 ,5 ,5 ,5 ,5 ,5 ,1 },
                { 1 , 4 , 4 , 4 , 4 ,4 ,4 ,4 ,1 ,3 ,3 ,3 ,3 ,3 ,3, 1 ,5 ,5 ,5 ,5 ,5 ,1 },
                { 1 , 1 , 1 , 1 , 1 ,1 ,0 ,1 ,1 ,3 ,3 ,3 ,3 ,3 ,3 ,1 ,1 ,0 ,1 ,1 ,1 ,1 },
                { 1 ,11 ,11 ,11 ,11 ,1 ,0 ,0 ,1 ,1 ,1 ,0 ,1 ,1 ,1 ,1 ,0 ,0 ,0 ,1 ,1 ,1 },
                { 1 ,11 ,11 ,11 ,11 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 },
                { 1 ,11 ,11 ,11 ,11 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 },
                { 1 ,11 ,11 ,11 ,11 ,1 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 },
                { 1 ,11 ,11 ,11 ,11 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,2 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,6 ,6 },
                { 1 ,11 ,11 ,11 ,11 ,1 ,0 ,0 ,0 ,0 ,0 ,1 ,0 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,6 ,6 },
                { 1 , 1 , 1 , 1 , 0 ,1 ,1 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,6 ,6 },
                { 1 ,10 ,10 , 1 , 0 ,0 ,1 ,1 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,6 ,6 },
                { 1 ,10 ,10 , 0 , 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 ,1 },
                { 1 ,10 ,10 , 1 , 0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,0 ,1 ,1 ,1 ,1 },
                { 1 , 1 , 1 , 1 , 0 ,1 ,1 ,1 ,1 ,1 ,1 ,0 ,1 ,1 ,1 ,1 ,1 ,0 ,1 ,1 ,1 ,1 },
                { 1 , 9 , 9 , 9 , 9 ,9 ,1 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,1 ,7 ,7 ,7 ,7 ,7 ,1 },
                { 1 , 9 , 9 , 9 , 9 ,9 ,1 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,1 ,7 ,7 ,7 ,7 ,7 ,1 },
                { 1 , 9 , 9 , 9 , 9 ,9 ,1 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,1 ,7 ,7 ,7 ,7 ,7 ,1 },
                { 1 , 9 , 9 , 9 , 9 ,9 ,1 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,8 ,1 ,7 ,7 ,7 ,7 ,7 ,1 },
                { 1 , 1 , 1 , 1 , 1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 ,1 }
            };
        }

        public static void ViewMap(User user, Com com1, Com com2, Com com3)
        {
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 22; j++)
                {
                    if ((j+1) % 22 == 1)
                    {
                        Console.WriteLine("");
                    }
                    if (user.position.Item1 == i && user.position.Item2 == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("●");
                        Console.ResetColor();
                    }
                    else if (com1.position.Item1 == i && com1.position.Item2 == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("▼");
                        Console.ResetColor();
                    }
                    else if (com2.position.Item1 == i && com2.position.Item2 == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("▼");
                        Console.ResetColor();
                    }
                    else if (com3.position.Item1 == i && com3.position.Item2 == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("▼");
                        Console.ResetColor();
                    }
                    else
                    {
                        switch (GameManager.Instance.map[i, j])
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                
                                Console.Write("■");
                                Console.ResetColor();
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 7:
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 8:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 9:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 10:
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            case 11:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                Console.Write("□");
                                Console.ResetColor();
                                break;
                            default:
                                Console.Write("□");
                                break;
                        }
                    }
                }
            }
        }

        void SetGameState()
        {
            //현재 위치. 가지고있는 카드 목록

        }

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
            */
            #endregion
        }

        void SetGamePanal()
        {
            //게임 컨트롤 패널 초기화

        }

        void ViewGamePanel()
        { 
            //할수있는 행위 목록

            if(GameManager.Instance.turn ==0)
            {
                //이동
                // 장소에 도착했을때 ->추리

            }
            else
            {
                //남이 추리중 -> 증명 -> 내카드중에서 하나 선택해서 보여주기 
                //증명하기 , 넘어가기
            }
        }

        public static void GameManagerInit()
        {
            //gamemanager 초기화
            Console.SetWindowSize(120, 45);
            Console.SetBufferSize(120, 45);

            SetMap();
        }

    }
}
