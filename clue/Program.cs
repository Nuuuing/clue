using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Program
    {
        //TODO: 메소드 명명 수정하기 

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
            com1.AddKnowCard(com1.getCardList());
            com2.AddKnowCard(com2.getCardList());
            com3.AddKnowCard(com3.getCardList());
            //알고있는 카드 정보에 처음 뽑은 카드 정보 넣기

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
                        while (user.getMoveCount() > 0)
                        {
                            Console.SetCursorPosition(0, 0);

                            Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                            Console.WriteLine(user.getMoveCount());
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

                        if (com1.CheckIsLoc())
                        {
                            //현재 장소가 장소인지 체크
                            //TODO: 추리하기
                            //현재위치 번호를 저장하고 문구띄우기 -> ~~가 ~~로 추리했습니다.

                            //TODO: 추리 할 필요가 없는 장소인지 체크하고, 추리 해야할 장소면 추리 진행
                            com1.Guess(gm); //추리할카드 저장
                            //com2 증명
                            //com3 증명
                            //user 증명
                            //TODO: 증명추가하기
                        }
                        else
                        {
                            if (com1.CheckGoLocation() == true)
                            {

                                //밑에 메소드 다 분리하기
                                //다시 경로 탐색 true
                                //TODO: 랜덤으로 장소 좌표 넣기 -> 체크하기
                                List<(int, int)> path = com1.FindShortestPath(gm.map, com1.position, com1.GetRandomCoor(gm.GetAllCard()));
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
                                    if (com1.goPath.Count == 0)
                                    {
                                        break;
                                    }
                                    com1.position = com1.goPath.Dequeue();
                                    //맵 출력
                                    Console.SetCursorPosition(0, 0);

                                    Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                    Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                                    Init.viewMap(user, com1, com2, com3);
                                }
                            }
                            else
                            {
                                //이동
                                for (int i = 0; i < com1.getMoveCount(); i++)
                                {
                                    if (com1.goPath.Count == 0)
                                    {
                                        break;
                                    }
                                    com1.position = com1.goPath.Dequeue();
                                    //맵 출력
                                    Console.SetCursorPosition(0, 0);

                                    Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                    Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                                    Init.viewMap(user, com1, com2, com3);
                                }
                            }
                        }
                    }
                    gm.turn++;
                }
                else if (gm.turn == 2)
                {
                    if (com2.getAlive())
                    {
                        com2.setMoveCount(gm.rollDice());   //주사위

                        if (com2.CheckIsLoc())
                        {
                            com2.Guess(gm); //추리할카드 저장
                        }
                        else
                        {
                            if (com2.CheckGoLocation() == true)
                            {
                                //다시 경로 탐색 true
                                List<(int, int)> path = com2.FindShortestPath(gm.map, com2.position, (4, 6));
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
                                    if (com2.goPath.Count == 0)
                                    {
                                        break;
                                    }
                                    com2.position = com2.goPath.Dequeue();
                                    //맵 출력
                                    Console.SetCursorPosition(0, 0);

                                    Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                    Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                                    Init.viewMap(user, com1, com2, com3);
                                }
                            }
                            else
                            {
                                //이동
                                for (int i = 0; i < com2.getMoveCount(); i++)
                                {
                                    if (com2.goPath.Count == 0)
                                    {
                                        break;
                                    }
                                    com2.position = com2.goPath.Dequeue();
                                    //맵 출력
                                    Console.SetCursorPosition(0, 0);

                                    Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                    Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                                    Init.viewMap(user, com1, com2, com3);
                                }
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

                        if (com2.CheckIsLoc())
                        {
                            com2.Guess(gm); //추리할카드 저장
                        }
                        else
                        {
                            if (com3.CheckGoLocation() == true)
                            {
                                //다시 경로 탐색 true
                                List<(int, int)> path = com3.FindShortestPath(gm.map, com3.position, (4, 6));
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
                                    if (com3.goPath.Count == 0)
                                    {
                                        break;
                                    }
                                    com3.position = com3.goPath.Dequeue();
                                    //queue가 0일때 -> 장소에 도착했을때. 큐 초기화하고 -> 추리시작
                                    //맵 출력
                                    Console.SetCursorPosition(0, 0);

                                    Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                    Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                                    Init.viewMap(user, com1, com2, com3);
                                }
                            }
                            else
                            {
                                //이동
                                for (int i = 0; i < com3.getMoveCount(); i++)
                                {
                                    if (com3.goPath.Count == 0)
                                    {
                                        break;
                                    }

                                    com3.position = com3.goPath.Dequeue();
                                    //맵 출력
                                    Console.SetCursorPosition(0, 0);

                                    Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                    Console.WriteLine(GameManager.Instance.map[user.position.Item1, user.position.Item2]);
                                    Init.viewMap(user, com1, com2, com3);
                                }
                            }
                        }
                    }
                    gm.turn = 0;
                }
            }
        }
    }
}

