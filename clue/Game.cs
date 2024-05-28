using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace clue
{
    class Game
    {
        void CardSharingIntro()
        {
            Console.Clear();
            Console.SetCursorPosition(50, 15);
            Console.WriteLine(" [ GAME START ] ");
            Console.SetCursorPosition(42, 17);
            Console.WriteLine("카드를 나눠주고 게임을 시작합니다.");

            Console.ReadLine();
            Console.Clear();
        }  

        public void Run()
        {
            List<string> systemLable = new List<string>();
            Init.GameManagerInit(); //초기 설정

            GameManager gm = GameManager.Instance;
            gm.StartGame(); //전체 카드 세팅 + 게임시작

            User user = new User(gm.GetStartCardList(0));
            Com com1 = new Com(gm.GetStartCardList(1));
            Com com2 = new Com(gm.GetStartCardList(2));
            Com com3 = new Com(gm.GetStartCardList(3));
            //카드 나누어주기

            com1.AddKnowCard(com1.GetCardList());
            com2.AddKnowCard(com2.GetCardList());
            com3.AddKnowCard(com3.GetCardList());
            //알고있는 카드 정보에 처음 뽑은 카드 정보 넣기

            CardSharingIntro(); //카드 나누어주기 label

            while (gm.Running) 
            {
                if (gm.turn == 0)   //유저 턴
                {
                    if (user.GetAlive())    //살아있으면 움직임 계속
                    {
                        //TODO: 시스템 합쳐서 메소드로 만들기
                        
                        Console.Clear();
                        gm.ViewSystemNotice();  //전체 시스템 notice
                        systemLable.Clear();
                        systemLable.Add("유저의 턴");
                        
                        Init.ViewMap(user, com1, com2, com3);
                        gm.ViewGameState();
                        gm.ViewUserCardList();
                        
                        gm.ViewSystem(systemLable, 0);
                        user.ViewUserState(false);
                        gm.ViewRoomLabel();

                        int selectMenuNum = 0;
                        int menuNum = 0;
                        if (user.GetMoveCount() == 0)   //턴 시작했을때 움직일 주사위 턴 없으면 주사위 던지기 메뉴 
                        {
                            menuNum = 5;
                            gm.ViewMenu(menuNum);
                            selectMenuNum = gm.ChooseUserMenu(menuNum);

                            if(selectMenuNum == 0)
                            {
                                systemLable.Clear();
                                systemLable.Add("주사위를 굴립니다.");
                                gm.ViewSystem(systemLable, 0);
                                user.SetMoveCount(gm.RollDice());   //주사위 굴리기
                            }
                        }
                        
                        if (user.GetLocByCoor(user.position) !=0 && user.GetMoveCount() !=0)  
                        {
                            if(user.GetLocByCoor(user.position) == 2)
                            {
                                menuNum = 1;
                                gm.ViewMenu(menuNum);
                                selectMenuNum = gm.ChooseUserMenu(menuNum); 
                                if(selectMenuNum == 0)
                                {
                                    systemLable.Clear();
                                    systemLable.Add("주사위 수 만큼 이동합니다.");
                                    gm.ViewSystem(systemLable, 0);
                                    user.SetUserMove(true);
                                }
                            }
                            else
                            {
                                menuNum = 2;
                                gm.ViewMenu(menuNum);
                                selectMenuNum = gm.ChooseUserMenu(menuNum);
                                if(selectMenuNum == 0)
                                {
                                    systemLable.Clear();
                                    systemLable.Add("주사위 수 만큼 이동합니다.");
                                    gm.ViewSystem(systemLable, 0);
                                    user.SetUserMove(true);
                                }
                            }
                        }
                        else if (user.GetLocByCoor(user.position) == 0 && user.GetMoveCount() !=0)
                        {
                            menuNum = 4;
                            gm.ViewMenu(menuNum);
                            selectMenuNum = gm.ChooseUserMenu(menuNum);
                            if(selectMenuNum == 0)
                            {
                                user.SetUserMove(true);
                            }
                        }
                        
                        if (user.GetUserMove())
                        {
                            ConsoleKey key;
                            while (user.GetMoveCount() > 0)
                            {
                                user.ViewUserState(true);

                                Init.ViewMap(user, com1, com2, com3);
                                gm.ViewRoomLabel(); //전체 맵 라벨

                                gm.ViewMenu(3);
                                selectMenuNum = gm.ChooseUserMenu(3, selectMenuNum);
                                if (selectMenuNum == 0)
                                    user.Move(MoveDir.TOP);
                                else if (selectMenuNum == 1)
                                    user.Move(MoveDir.BOTTOM);
                                else if (selectMenuNum == 2)
                                    user.Move(MoveDir.LEFT);
                                else
                                    user.Move(MoveDir.RIGHT);
                            }
                        }

                    }

                    user.SetUserMove(false);
                    user.ViewUserState(false);
                    gm.ViewMenu(0);
                    gm.turn++;
                }
                else if (gm.turn == 1)
                {
                    if (com1.GetAlive())    //살아있는지 여부
                    {
                        gm.ViewSystemNotice();  //전체 시스템 notice
                        systemLable.Clear();
                        gm.ViewGameState();
                        gm.ViewSystem(systemLable, 0);
                        user.ViewUserState(false);
                        if (com1.CheckCanFinal())    //최종 추리 가능한지 확인
                        {
                            if (com1.CheckComPosMiddle())
                            {
                                List<Card> finalCards = com1.FinalGuess(gm);
                                int corrNum = com1.CheckFinalGuess(gm, finalCards);

                                if (corrNum == 3)
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
                                List<(int, int)> path = com1.FindShortestPath(gm.map, com1.position, (9, 11));
                                com1.goPath.Clear();
                                if (path != null)
                                {
                                    for (int i = 0; i < path.Count; i++)
                                    {   //가고있는 경로 큐에 담음
                                        com1.goPath.Enqueue(path[i]);
                                    }
                                }
                                com1.ComMove(gm, user, com1, com2, com3);
                            }
                        }
                        else
                        {
                            if (com1.CheckIsLoc())//현재 장소가 장소인지 체크
                            {
                                if (!com1.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com1.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(1);
                                    //TODO: 현재위치 번호를 저장하고 문구띄우기 -> ~~가 ~~로 추리했습니다.
                                }

                                if (gm.GetGuessUser() == 1)  //현재 추리중인 유저가 1이면
                                {
                                    if (com2.Prove(gm))
                                    {
                                        //com2 증명
                                        Card provCard = com2.GetProvCard(gm);
                                        //TODO: 증명하고 ~~가 증명했습니다. 문구 alert
                                    }
                                    else
                                    {
                                        //TODO: com1 증명 못함 => 다음사람이 증명합니다 alert
                                        if (com3.Prove(gm))
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
                                systemLable.Add(" COM1 이 주사위를 굴립니다.        ");
                                gm.ViewSystem(systemLable, 0);
                                Thread.Sleep(300);
                                com1.SetMoveCount(gm.RollDice());   //주사위 굴리기
                                systemLable.Clear();
                                systemLable.Add(" COM1 이 이동합니다.        ");
                                gm.ViewSystem(systemLable, 0);
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
                                    else
                                    {
                                        Console.WriteLine("no~");
                                    }
                                    com1.ComMove(gm, user, com1, com2, com3);
                                }
                                else
                                {
                                    com1.ComMove(gm, user, com1, com2, com3);
                                }
                            }
                        }
                    }
                    gm.SetGuessUser(-1);    //현재 추리중인 유저 초기화
                    gm.turn++;
                }
                else if (gm.turn == 2)
                {
                    if (com2.GetAlive())    //살아있는지 여부
                    {
                        gm.ViewSystemNotice();  //전체 시스템 notice
                        systemLable.Clear();
                        gm.ViewGameState();
                        gm.ViewSystem(systemLable, 0);
                        user.ViewUserState(false);
                        if (com2.CheckCanFinal())    //최종 추리 가능한지 확인
                        {
                            if (com2.CheckComPosMiddle())
                            {
                                List<Card> finalCards = com2.FinalGuess(gm);
                                int corrNum = com2.CheckFinalGuess(gm, finalCards);

                                if (corrNum == 3)
                                {
                                    gm.SetWinner(1);
                                    gm.EndGame();
                                    break;
                                }
                                else
                                {
                                    com2.SetDie();
                                    //TODO: 맞은갯수 반환
                                }
                            }
                            else
                            {
                                //2번(중앙홀로) 이동하기
                                List<(int, int)> path = com2.FindShortestPath(gm.map, com2.position, (9, 11));
                                com2.goPath.Clear();
                                if (path != null)
                                {
                                    for (int i = 0; i < path.Count; i++)
                                    {   //가고있는 경로 큐에 담음
                                        com2.goPath.Enqueue(path[i]);
                                    }
                                }
                                com2.ComMove(gm, user, com1, com2, com3);
                            }
                        }
                        else
                        {
                            if (com2.CheckIsLoc())//현재 장소가 장소인지 체크
                            {
                                if (!com2.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com2.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(1);
                                    //TODO: 현재위치 번호를 저장하고 문구띄우기 -> ~~가 ~~로 추리했습니다.
                                }

                                if (gm.GetGuessUser() == 1)  //현재 추리중인 유저가 1이면
                                {
                                    if (com3.Prove(gm))
                                    {
                                        //com2 증명
                                        Card provCard = com3.GetProvCard(gm);
                                        //TODO: 증명하고 ~~가 증명했습니다. 문구 alert
                                    }
                                    else
                                    {
                                        //TODO: 유저먼저 증명
                                    }
                                }
                            }
                            else
                            {   //현재 위치가 장소가 아닐때
                                com2.SetMoveCount(gm.RollDice());   //주사위 굴리기
                                systemLable.Clear();
                                systemLable.Add(" COM2 가 이동합니다.        ");
                                if (com1.CheckGoLocation()) //새로운 장소를 찾아야하는지 여부
                                {
                                    List<(int, int)> path = com2.FindShortestPath(gm.map, com2.position, com2.GetRandomCoor(gm.GetAllCard()));
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
                                        Console.WriteLine("no~");
                                    }

                                    com2.ComMove(gm, user, com1, com2, com3);
                                }
                                else
                                {
                                    com2.ComMove(gm, user, com1, com2, com3);
                                }
                            }
                        }
                    }
                    gm.SetGuessUser(-1);    //현재 추리중인 유저 초기화
                    gm.turn++;
                }
                else if(gm.turn == 3)
                {
                    if (com3.GetAlive())    //살아있는지 여부
                    {
                        gm.ViewSystemNotice();  //전체 시스템 notice
                        systemLable.Clear();
                        systemLable.Add("COM3 의 턴");
                        gm.ViewSystem(systemLable, 0);
                        user.ViewUserState(false);
                        if (com3.CheckCanFinal())    //최종 추리 가능한지 확인
                        {
                            if (com3.CheckComPosMiddle())
                            {
                                List<Card> finalCards = com3.FinalGuess(gm);
                                int corrNum = com3.CheckFinalGuess(gm, finalCards);

                                if (corrNum == 3)
                                {
                                    gm.SetWinner(1);
                                    gm.EndGame();
                                    break;
                                }
                                else
                                {
                                    com3.SetDie();
                                    //TODO: 맞은갯수 반환
                                }
                            }
                            else
                            {
                                //2번(중앙홀로) 이동하기
                                List<(int, int)> path = com3.FindShortestPath(gm.map, com3.position, (9, 11));
                                com3.goPath.Clear();
                                if (path != null)
                                {
                                    for (int i = 0; i < path.Count; i++)
                                    {   //가고있는 경로 큐에 담음
                                        com3.goPath.Enqueue(path[i]);
                                    }
                                }
                                com3.ComMove(gm, user, com1, com2, com3);
                            }
                        }
                        else
                        {
                            if (com3.CheckIsLoc())//현재 장소가 장소인지 체크
                            {
                                if (!com3.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com3.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(1);
                                    //TODO: 현재위치 번호를 저장하고 문구띄우기 -> ~~가 ~~로 추리했습니다.
                                }

                                if (gm.GetGuessUser() == 1)  //현재 추리중인 유저가 1이면
                                {
                                    if (com3.Prove(gm))
                                    {
                                        //com2 증명
                                        Card provCard = com3.GetProvCard(gm);
                                        //TODO: 증명하고 ~~가 증명했습니다. 문구 alert
                                    }
                                    else
                                    {
                                        //TODO: 유저먼저 증명
                                    }
                                }
                            }
                            else
                            {   //현재 위치가 장소가 아닐때
                                com3.SetMoveCount(gm.RollDice());   //주사위 굴리기
                                systemLable.Clear();
                                systemLable.Add(" COM3 가 이동합니다.        ");
                                if (com3.CheckGoLocation()) //새로운 장소를 찾아야하는지 여부
                                {
                                    List<(int, int)> path = com3.FindShortestPath(gm.map, com3.position, com3.GetRandomCoor(gm.GetAllCard()));
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
                                        Console.WriteLine("no~");
                                    }

                                    com3.ComMove(gm, user, com1, com2, com3);
                                }
                                else
                                {
                                    com3.ComMove(gm, user, com1, com2, com3);
                                }
                            }
                        }
                    }
                    gm.SetGuessUser(-1);    //현재 추리중인 유저 초기화
                    gm.turn = 0;
                }
            }

            {
                if (gm.GetWinner() == 0)
                {
                    //유저 우승
                }
                else if (gm.GetWinner() == 1)
                {
                    //com1 우승
                }
                else if (gm.GetWinner() == 2)
                {
                    //com2 우승
                }
                else
                {
                    //com3 우승
                }
            }
        }
    }
}
