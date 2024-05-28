using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Game
    {
        public void Run()
        {
            Init.GameManagerInit();

            GameManager gm = GameManager.Instance;
            gm.StartGame();

            User user = new User(gm.GetStartCardList(0));
            Com com1 = new Com(gm.GetStartCardList(1));
            Com com2 = new Com(gm.GetStartCardList(2));
            Com com3 = new Com(gm.GetStartCardList(3));
            //카드 나누어주기
            com1.AddKnowCard(com1.GetCardList());
            com2.AddKnowCard(com2.GetCardList());
            com3.AddKnowCard(com3.GetCardList());
            //알고있는 카드 정보에 처음 뽑은 카드 정보 넣기

            //Init.viewGameState(user); // 게임 상태 
            //user.getCardList();   //유저 카드 리스트

            Init.ViewMap(user, com1, com2, com3);
            gm.ViewSystemNotice();
            List<string> systemLable = new List<string>();
            systemLable.Add("추리가 틀렸습니다.");
            systemLable.Add("증명에는 참여합니다.");
            gm.ViewSystem(systemLable, 1);
            gm.ViewRoomLabel();
            gm.ViewGameState();
            gm.ViewUserCardList();
            gm.ViewMenu(2);
            /*
            while (gm.Running)
            {
                if (gm.turn == 0)
                {
                    if (user.GetAlive())
                    { //살아있으면 움직임 계속
                        //TODO: UI 설정

                        //현재 위치가 장소인지 체크 
                        //TODO: 유저 이동한 위치도 가운데 아니고 바로 방문앞....으로해서..?? LOCATION 체크 혹은 멈추는 위치도 LOCATION에 추가하기

                        if (user.CheckComPosMiddle())   //중앙에 있는지 확인
                        {
                            //최종 추리 할건지 이동할건지 메뉴
                            //최종 추리시
                            //카드 3개 선택
                        }
                        else
                        {
                              if(user.CheckIsLoc()) //true -> 현재 장소 타일 위에 
                            {
                                //TODO: 유저 장소타일 위치 다른거 체크해야함
                                //추리할건지 아니면 이동할건지 선택
                            }
                            else
                            {
                                //이동하기 메뉴
                                user.SetUserMove(true);
                            }
                        }

                        if(user.GetUserMove())
                        {
                            user.SetMoveCount(gm.RollDice());   //주사위

                            ConsoleKey key;
                            while (user.GetMoveCount() > 0)
                            {
                                Console.SetCursorPosition(0, 0);

                                Console.WriteLine("(" + user.position.Item1 + "," + user.position.Item2 + ")");
                                Console.WriteLine(user.GetMoveCount());
                                Init.ViewMap(user, com1, com2, com3);

                                key = Console.ReadKey(true).Key;

                                //TODO: 키보드 움직이지말고 메뉴 선택지로 움직이기 -> 메뉴 만들면 수정하기
                                switch (key)
                                {
                                    case ConsoleKey.LeftArrow:
                                        user.Move(MoveDir.LEFT);
                                        break;
                                    case ConsoleKey.RightArrow:
                                        user.Move(MoveDir.RIGHT);
                                        break;
                                    case ConsoleKey.UpArrow:
                                        user.Move(MoveDir.TOP);
                                        break;
                                    case ConsoleKey.DownArrow:
                                        user.Move(MoveDir.BOTTOM);
                                        break;
                                    case ConsoleKey.Q:
                                        //Q키 누를때 스탑 or enter
                                        break;
                                }
                            }
                        }
                    }
                    user.SetUserMove(false);
                    gm.turn++;
                }
                else if (gm.turn == 1)
                {
                    if (com1.GetAlive())    //살아있는지 여부
                    {
                        if(com1.CheckCanFinal())    //최종 추리 가능한지 확인
                        {
                            if(com1.CheckComPosMiddle())
                            {
                                List<Card> finalCards = com1.FinalGuess(gm);
                                 int corrNum = com1.CheckFinalGuess(gm,finalCards);

                                if(corrNum ==3)
                                {
                                    gm.SetWinner(1);
                                    gm.EndGame();
                                    break;
                                }
                                else
                                {
                                    com1.SetDie();
                                    //TODO: 맞은갯수 반환
                                }
                            }
                            else
                            {
                                //2번(중앙홀로) 이동하기
                                List<(int, int)> path = com1.FindShortestPath(gm.map, com1.position, (9,12));
                                    com1.goPath.Clear();
                                    if (path != null)
                                    {
                                        for (int i = 0; i < path.Count; i++)
                                        {   //가고있는 경로 큐에 담음
                                            com1.goPath.Enqueue(path[i]);
                                        }
                                    }
                                    com1.ComMove(gm ,user, com1, com2, com3 );
                            }
                        }
                        else
                        {
                            if (com1.CheckIsLoc())//현재 장소가 장소인지 체크
                            {
                                if(!com1.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com1.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(1);
                                    //TODO: 현재위치 번호를 저장하고 문구띄우기 -> ~~가 ~~로 추리했습니다.
                                }

                                if(gm.GetGuessUser() == 1)  //현재 추리중인 유저가 1이면
                                {
                                    if(com2.Prove(gm))
                                    { 
                                        //com2 증명
                                        Card provCard = com2.GetProvCard(gm);
                                        //TODO: 증명하고 ~~가 증명했습니다. 문구 alert
                                    }
                                    else
                                    {
                                        //TODO: com1 증명 못함 => 다음사람이 증명합니다 alert
                                        if(com3.Prove(gm))
                                        {
                                            //com3 증명
                                            Card provCard = com3.GetProvCard(gm);
                                            //TODO: 증명하고 ~~가 증명했습니다. 문구 alert 
                                        }
                                        else
                                        {
                                            //TODO: com2 증명 못함 => 다음사람이 증명합니다 alert
                                            {
                                                //user 증명
                                                //TODO: 증명추가하기   
                                                //TODO: 유저도 증명못하면 모두 증명 못했음 ALERT
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {   //현재 위치가 장소가 아닐때
                                com1.SetMoveCount(gm.RollDice());   //주사위 굴리기
                                if (com1.CheckGoLocation()) //새로운 장소를 찾아야하는지 여부
                                {
                                    List<(int, int)> path = com1.FindShortestPath(gm.map, com1.position, com1.GetRandomCoor(gm.GetAllCard()));
                                    com1.goPath.Clear();
                                    if (path != null)
                                    {
                                        for (int i = 0; i < path.Count; i++)
                                        {   //가고있는 경로 큐에 담음
                                            com1.goPath.Enqueue(path[i]);
                                        }
                                    }
                                    com1.ComMove(gm ,user, com1, com2, com3 );
                                }
                                else
                                {
                                    com1.ComMove(gm ,user, com1, com2, com3 );
                                }
                            }
                        }
                    }
                    gm.SetGuessUser(-1);    //현재 추리중인 유저 초기화
                    Console.ReadLine();
                    gm.turn++;
                }
                else if (gm.turn == 2)
                {
                    gm.turn++;
                }
                else
                {
                    gm.turn = 0;
                }
            }

            {
                if(gm.GetWinner() == 0)
                {
                    //유저 우승
                }
                else if(gm.GetWinner() == 1)
                {
                       //com1 우승
                }
                else if(gm.GetWinner() == 2)
                {
                    //com2 우승
                }
                else
                {
                    //com3 우승
                }
            }
            */
        }
    }
}
