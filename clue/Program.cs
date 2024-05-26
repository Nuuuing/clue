using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Program
    {
        static void Main(string[] args)
        {
            Init.gameManagerInit();

            GameManager gm = GameManager.Instance;
            gm.StartGame();

            User user = new User(gm.getStartCardList(0));
            Com com1 = new Com(gm.getStartCardList(1));
            Com com2 = new Com(gm.getStartCardList(2));
            Com com3 = new Com(gm.getStartCardList(3));
            //카드 나누어주기

            //Init.viewGameState(user);
            //user.getCardList();

            while (gm.Running)
            {
                if (gm.turn == 0)
                {
                    if (user.getAlive())
                    { //살아있으면 움직임 계속
                      //유저 턴

                        user.setMoveCount(gm.rollDice());   //주사위

                        //메뉴 선택
                        //1. 이동 2. 추리

                        ConsoleKey key;
                        while (user.getMoveCount() >= 0)
                        {
                            Console.SetCursorPosition(0, 0);

                            Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                            Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                            Init.viewMap(user, com1, com2, com3);

                            key = Console.ReadKey(true).Key;

                            switch (key)
                            {
                                case ConsoleKey.LeftArrow:
                                    user.move(MoveDir.LEFT);
                                    break;
                                case ConsoleKey.RightArrow:
                                    user.move(MoveDir.RIGHT);
                                    break;
                                case ConsoleKey.UpArrow:
                                    user.move(MoveDir.TOP);
                                    break;
                                case ConsoleKey.DownArrow:
                                    user.move(MoveDir.BOTTOM);
                                    break;
                            }
                        }

                    }
                    gm.turn++;
                }
                else if (gm.turn == 1)
                {
                    if (com1.getAlive())    //살아있는지 여부
                    {
                        com1.setMoveCount(gm.rollDice());   //주사위

                        if (com1.CheckGoLocation() == true)
                        {
                            //다시 경로 탐색 true
                            List<(int, int)> path = com1.FindShortestPath(gm.map, (9, 12), (4, 6));
                            com1.goPath.Clear();
                            if (path != null)
                            {
                                for (int i = 0; i < path.Count; i++)
                                {   //가고있는 경로 큐에 담음
                                    com1.goPath.Enqueue(path[i]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Path not found");
                            }
                            //이동
                            for (int i = 0; i < com1.getMoveCount(); i++)
                            {
                                com1.position = com1.goPath.Dequeue();
                                //맵 출력
                            }
                        }
                        else
                        {
                            //이동
                            for (int i = 0; i < com1.getMoveCount(); i++)
                            {
                                com1.position = com1.goPath.Dequeue();
                                //맵 출력
                            }
                        }

                        //com1 턴
                        //    if -현재 턴이 장소 턴 + 가지고 있는 정보가 아니면 추리
                        //else -이동

                    }
                    gm.turn++;
                }
                else if (gm.turn == 2)
                {
                    if (com2.getAlive())
                    {
                        com2.setMoveCount(gm.rollDice());   //주사위

                        if (com2.CheckGoLocation() == true)
                        {
                            //다시 경로 탐색 true
                            List<(int, int)> path = com2.FindShortestPath(gm.map, (9, 12), (4, 6));
                            com2.goPath.Clear();
                            if (path != null)
                            {
                                for (int i = 0; i < path.Count; i++)
                                {   //가고있는 경로 큐에 담음
                                    com2.goPath.Enqueue(path[i]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Path not found2");
                            }
                            //이동
                            for (int i = 0; i < com2.getMoveCount(); i++)
                            {
                                com2.position = com2.goPath.Dequeue();
                                //맵 출력
                            }
                        }
                        else
                        {
                            //이동
                            for (int i = 0; i < com2.getMoveCount(); i++)
                            {
                                com2.position = com2.goPath.Dequeue();
                                //맵 출력
                            }
                        }

                    }
                    gm.turn++;
                }
                else
                {
                    if (com3.getAlive())
                    {
                        com3.setMoveCount(gm.rollDice());   //주사위

                        if (com3.CheckGoLocation() == true)
                        {
                            //다시 경로 탐색 true
                            List<(int, int)> path = com3.FindShortestPath(gm.map, (9, 12), (4, 6));
                            com3.goPath.Clear();
                            if (path != null)
                            {
                                for (int i = 0; i < path.Count; i++)
                                {   //가고있는 경로 큐에 담음
                                    com3.goPath.Enqueue(path[i]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Path not found2");
                            }
                            //이동
                            for (int i = 0; i < com3.getMoveCount(); i++)
                            {
                                com3.position = com3.goPath.Dequeue();
                                //맵 출력
                            }
                        }
                        else
                        {
                            //이동
                            for (int i = 0; i < com3.getMoveCount(); i++)
                            {
                                com3.position = com3.goPath.Dequeue();
                                //맵 출력
                            }
                        }
                    }
                    gm.turn = 0;
                }
            }
        }
    }
}

