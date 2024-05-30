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

        void RollDiceIntro(int moveCount, int user)
        {
            Console.Clear();
            Console.SetCursorPosition(50, 15);
            Console.WriteLine(" [ ROLL DICE ] ");
            Console.SetCursorPosition(42, 17);
            Console.WriteLine(" 주사위를 굴립니다. ");

            Console.SetCursorPosition(42, 17);

            if (user == 0)
                Console.WriteLine("   당신의 주사위 숫자는 ");

            Console.SetCursorPosition(42, 19);
            Console.WriteLine($"       {moveCount} 나왔습니다! ");

            Console.ReadLine();
            Console.Clear();
        }

        void ViewProvCard(int user, Card provCard)
        {
            Console.Clear();

            Console.SetCursorPosition(50, 15);
            if(user != -1)
            {
                Console.WriteLine($" [ COM{user} 의 증명 ] ");

                Console.SetCursorPosition(52, 18);
                Console.WriteLine($" { provCard.GetTypeString()} 카드 ");
                Console.SetCursorPosition(54, 20);
                Console.WriteLine($" { provCard.GetName() } ");
            }
            else
            {
                Console.WriteLine($" [ 아무도 증명하지 못하였습니다. ] ");
            }

            Console.ReadLine();
            Console.Clear();
        }

        void EndinngIntro(int user)
        {
            Console.Clear();
            Console.SetCursorPosition(50, 15);
            if (user == 0)
                Console.WriteLine(" [  WIN  ] ");
            else
                Console.WriteLine(" [ GAME OVER ] ");

            Console.SetCursorPosition(42, 17);

            if (user == 0)
            {
                Console.WriteLine("   당신이 이겼습니다. ");
                Console.SetCursorPosition(42, 18);
                Console.WriteLine("   축하합니다! ");
            }
            else if (user == 1)
            {
                Console.WriteLine("    COM1 가 이겼습니다. ");
                Console.SetCursorPosition(42, 18);
                Console.WriteLine("   아쉽네요 ");
            }
            else if (user == 2)
            {
                Console.WriteLine("    COM2 가 이겼습니다. ");
                Console.SetCursorPosition(42, 18);
                Console.WriteLine("   아쉽네요 ");
            }
            else if (user == 3)
            {
                Console.WriteLine("    COM3 가 이겼습니다. ");
                Console.SetCursorPosition(42, 18);
                Console.WriteLine("   아쉽네요 ");
            }
            else
            {
                Console.WriteLine("    모두 실패하였습니다. ");
                Console.SetCursorPosition(42, 18);
                Console.WriteLine("   아쉽네요 ");
            }

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

            while (gm.Running)  //게임 중일때동안
            {
                
                Console.CursorVisible = false;

                if (user.GetAlive() == false && com1.GetAlive() == false && com2.GetAlive() == false && com3.GetAlive() == false)
                {   //모두 죽어서 실패
                    gm.Running = false;
                    gm.SetWinner(4);
                    break;
                }

                if (gm.turn == 0)   //유저 턴
                {
                    if (user.GetAlive())    //살아있으면 이동 및 선택지 선택 가능
                    {
                        gm.ViewUserCardList();  //유저 카드 목록 뿌려주기
                        Console.Clear();
                        gm.ViewSystemDescription();  //전체 시스템 Description
                        systemLable.Clear();

                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기

                        gm.ViewSystemComment(systemLable, 0);   //시스템창 Comment
                        gm.ViewGameState();     //현재 턴 notice
                        user.ViewUserState(false);  //현재 유저 이동가능 칸수 뿌려주기 초기화

                        int selectMenuNum = 0;
                        int menuNum = 0;

                        Thread.Sleep(200);
                        //********************************** 턴 시작하면 주사위 던지고 시작하기
                        systemLable.Clear();
                        int rollCount = gm.RollDice();  //주사위 굴리기
                        user.SetMoveCount(rollCount);   //유저 객체에 현재 diceCount 추가
                        RollDiceIntro(rollCount, gm.turn);       //주사위 굴리기 intro
                        gm.ViewGameState();

                        gm.ViewSystemDescription();  //전체 시스템 Description
                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                        gm.ViewUserCardList();  //유저 카드 목록 뿌려주기
                        gm.ViewSystemComment(systemLable, 0);

                        if (user.GetLocByCoor(user.position) != 0)    //현재 위치가 장소일뗴
                        {
                            if (user.GetLocByCoor(user.position) == 2)   //중앙홀일때
                            {
                                menuNum = 1;
                                gm.ViewMenuLabel(menuNum);
                                selectMenuNum = gm.ChooseUserMenu(menuNum, user); // 0 :이동하기 , 1: 최종추리하기
                                if (selectMenuNum == 0)  //이동하기 메뉴 선택했을때
                                {
                                    systemLable.Clear();
                                    systemLable.Add("주사위 수 만큼 이동해주세요.");
                                    gm.ViewSystemComment(systemLable, 0);
                                    user.SetUserMove(true); //유저 이동 가능 FLAG
                                }
                                else
                                {
                                    user.SetFinalGuessT();
                                }
                            }
                            else
                            {
                                menuNum = 2;
                                gm.ViewMenuLabel(menuNum);
                                selectMenuNum = gm.ChooseUserMenu(menuNum, user); // 0 :이동하기 , 1: 추리하기
                                if (selectMenuNum == 0)
                                {
                                    systemLable.Clear();
                                    systemLable.Add("주사위 수 만큼 이동해주세요.");
                                    gm.ViewSystemComment(systemLable, 0);
                                    user.SetUserMove(true);
                                }
                                else
                                {
                                    //그냥 추리
                                    // ------------------- 카드 나누기  start ------------------
                                    List<Card> UnHave = user.GetUnHaveCardList(gm);
                                    List<Card> per = new List<Card>();
                                    List<Card> wep = new List<Card>();
                                    per.Clear();
                                    wep.Clear();
                                    for (int i = 0; i < UnHave.Count; i++)
                                    {
                                        if (UnHave[i].GetCardType().Equals(CardType.WEP))
                                            wep.Add(UnHave[i]);
                                        else if (UnHave[i].GetCardType().Equals(CardType.PER))
                                            per.Add(UnHave[i]);
                                    }
                                    // ------------------- 카드 나누기 end ------------------
                                    int guessNum = 0;
                                    int selectS = 0;
                                    int selectT = 0;

                                    gm.menuClear();
                                    while (guessNum < 3)
                                    {
                                        if (guessNum == 0)
                                        {
                                            guessNum++;
                                        }
                                        else if (guessNum == 1)
                                        {
                                            gm.menuClear();
                                            gm.ViewUserGuessMenu(per, 1);
                                            selectS = gm.ChooseUserGuessMenu(per, selectS);
                                            guessNum++;
                                        }
                                        else
                                        {
                                            gm.menuClear();
                                            gm.ViewUserGuessMenu(wep, 2);
                                            selectT = gm.ChooseUserGuessMenu(wep, selectT);
                                            if (selectT != wep.Count)
                                                guessNum++;
                                            else
                                                guessNum--;
                                        }
                                    }
                                    if (guessNum == 3)  //3개 전부다 선택 하고 나서
                                    {
                                        List<Card> temp = new List<Card>();
                                        int locNum = user.GetLocByCoor(user.position);

                                        List<Card> allCards = gm.GetAllCard();

                                        Card userLocCard = new Card(0, CardType.LOC, "temp");

                                        for (int i = 0; i < allCards.Count; i++)
                                        {
                                            if (allCards[i].GetLocNum().Equals(locNum))
                                            {
                                                userLocCard = allCards[i];
                                            }
                                        }

                                        temp.Add(userLocCard);
                                        temp.Add(per[selectS]);
                                        temp.Add(wep[selectT]);
                                        gm.SetGuessCard(temp);  //현재 추리중인 카드에 저장

                                        Thread.Sleep(400);

                                        if (com1.Prove(gm))
                                        {
                                            Card provCard = com1.GetProvCard(gm);
                                            ViewProvCard(1, provCard);
                                        }
                                        else
                                        {
                                            if (com2.Prove(gm))
                                            {
                                                Card provCard = com2.GetProvCard(gm);
                                                ViewProvCard(2, provCard);
                                            }
                                            else
                                            {
                                                if (com3.Prove(gm))
                                                {
                                                    Card provCard = com3.GetProvCard(gm);
                                                    ViewProvCard(2, provCard);
                                                }
                                                else
                                                {
                                                    ViewProvCard(-1, null);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (user.GetLocByCoor(user.position) == 0) //현재 위치가 복도일때
                        {
                            menuNum = 4;
                            gm.ViewMenuLabel(menuNum);
                            selectMenuNum = gm.ChooseUserMenu(menuNum, user); // 0: 이동하기 
                            if (selectMenuNum == 0)
                            {
                                user.SetUserMove(true);
                            }
                        }

                        if (user.GetFinalGuess())
                        {
                            //최종추리하기
                            // ------------------- 카드 나누기  start ------------------
                            List<Card> UnHave = user.GetUnHaveCardList(gm);
                            List<Card> loc = new List<Card>();
                            List<Card> per = new List<Card>();
                            List<Card> wep = new List<Card>();
                            loc.Clear();
                            per.Clear();
                            wep.Clear();
                            for (int i = 0; i < UnHave.Count; i++)
                            {
                                if (UnHave[i].GetCardType().Equals(CardType.LOC))
                                    loc.Add(UnHave[i]);
                                else if (UnHave[i].GetCardType().Equals(CardType.PER))
                                    per.Add(UnHave[i]);
                                else
                                    wep.Add(UnHave[i]);
                            }
                            // ------------------- 카드 나누기 end ------------------
                            int guessNum = 0;
                            int selectF = 0;
                            int selectS = 0;
                            int selectT = 0;

                            gm.menuClear();
                            while (guessNum < 3)
                            {

                                if (guessNum == 0)
                                {
                                    gm.menuClear();
                                    gm.ViewUserGuessMenu(loc, 0);
                                    selectF = gm.ChooseUserGuessMenu(loc, selectF);
                                    if (selectF != loc.Count)
                                    {
                                        guessNum++;
                                    }
                                }
                                else if (guessNum == 1)
                                {
                                    gm.menuClear();
                                    gm.ViewUserGuessMenu(per, 1);
                                    selectS = gm.ChooseUserGuessMenu(per, selectS);
                                    if (selectF != per.Count)
                                        guessNum++;
                                    else
                                        guessNum--;
                                }
                                else
                                {
                                    gm.menuClear();
                                    gm.ViewUserGuessMenu(wep, 2);
                                    selectT = gm.ChooseUserGuessMenu(wep, selectT);
                                    if (selectT != wep.Count)
                                        guessNum++;
                                    else
                                        guessNum--;
                                }
                            }

                            List<Card> correctCards = gm.GetCorrCards();
                            Card corrLoc = new Card(0, CardType.LOC, "temp");
                            Card corrPer = new Card(0, CardType.PER, "temp");
                            Card corrWep = new Card(0, CardType.WEP, "temp");

                            for (int i = 0; i < correctCards.Count; i++)
                            {
                                if (correctCards[i].GetCardType().Equals(CardType.LOC))
                                    corrLoc = correctCards[i];
                                else if (correctCards[i].GetCardType().Equals(CardType.PER))
                                    corrPer = correctCards[i];
                                else
                                    corrWep = correctCards[i];
                            }
                            if (corrLoc.Equals(loc[selectF]))
                            {
                                if (corrPer.Equals(per[selectS]))
                                {
                                    if (corrWep.Equals(per[selectT]))
                                    {
                                        gm.SetWinner(0);
                                        gm.Running = false;
                                    }
                                    else
                                    {
                                        gm.ViewNoUserGuess();
                                        user.SetDie();
                                    }
                                }
                                else
                                {
                                    user.SetDie();
                                    gm.ViewNoUserGuess();
                                }
                            }
                            else
                            {
                                gm.ViewNoUserGuess();
                                user.SetDie();
                            }
                        }

                        if (user.GetUserMove()) //유저 이동
                        {
                            while (user.GetMoveCount() > 0)
                            {
                                user.ViewUserState(true);
                                Init.ViewMap(gm, user, com1, com2, com3);
                                gm.ViewRoomLabel(); //전체 맵 라벨

                                selectMenuNum = gm.ChooseUserMenu(3, user, selectMenuNum);

                                if (selectMenuNum == 0)
                                    user.Move(MoveDir.TOP);
                                else if (selectMenuNum == 1)
                                    user.Move(MoveDir.BOTTOM);
                                else if (selectMenuNum == 2)
                                    user.Move(MoveDir.LEFT);
                                else
                                    user.Move(MoveDir.RIGHT);

                                user.ViewUserState(true);
                                Init.ViewMap(gm, user, com1, com2, com3);
                                gm.ViewRoomLabel(); //전체 맵 라벨
                            }
                        }
                    }

                    user.SetUserMove(false);
                    user.ViewUserState(false);
                    gm.ViewMenuLabel(0); //메뉴 라벨 초기화
                    gm.turn++;
                }
                else if (gm.turn == 1)
                {
                    if (com1.GetAlive())    //살아있는지 여부
                    {
                        gm.ViewGameState();             //현재 턴 notice
                        Thread.Sleep(200);

                        //********************************** 턴 시작하면 주사위 던지고 시작하기
                        systemLable.Clear();
                        int rollCount = gm.RollDice();  //주사위 굴리기
                        com1.SetMoveCount(rollCount);   //유저 객체에 현재 diceCount 추가

                        gm.ViewSystemDescription();  //전체 시스템 Description
                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                        gm.ViewGameState();

                        systemLable.Clear();
                        gm.ViewGameState();
                        gm.ViewSystemComment(systemLable, 0);
                        user.ViewUserState(false);

                        bool isMove = false;    //이동할건지 flag
                        bool isMiddle = false;  //목표 장소가 중앙인지 flag

                        if (com1.CheckCanFinal(gm) == true)    //최종 추리 가능한지 확인
                        {
                            if (com1.CheckComPosMiddle()) //현재 중앙홀에 있는지 확인
                            {
                                List<Card> finalCards = com1.FinalGuess(gm);    //모르는 카드중 1개씩 return
                                int corrNum = com1.CheckFinalGuess(gm, finalCards);

                                com1.ViewFinalGuessIntro(1);

                                if (corrNum == 3)
                                {
                                    gm.SetWinner(1);
                                    com1.ViewFinalGuessCheckIntro(1,true);  //최종추리 label
                                    gm.EndGame();
                                    break;
                                }
                                else
                                {
                                    com1.SetDie();
                                }
                            }
                            else
                            {
                                //중앙으로 이동해서 추리
                                isMove = true;
                                isMiddle = true;
                            }
                        }
                        else
                        {
                            if (com1.CheckIsLoc()) //현재 장소가 장소인지 체크
                            {
                                if (!com1.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com1.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(1);
                                    gm.ViewGuessListIntro(1);
                                }
                            }
                            else
                            {   //현재 위치가 장소가 아닐때
                                systemLable.Clear();
                                systemLable.Add(" COM1 이 이동합니다.        ");
                                gm.ViewSystemComment(systemLable, 0);

                                isMiddle = false;
                                isMove = true;
                            }
                        }

                        if (gm.GetGuessUser() == 1)  //현재 추리중인 유저가 1이면
                        {
                            if (com2.Prove(gm))
                            {
                                //com2 증명
                                Card provCard2 = com2.GetProvCard(gm);
                                com1.AddKnowCard(provCard2);

                                Console.Clear();

                                gm.ViewSystemDescription();  //전체 시스템 Description
                                Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                gm.ViewGameState();

                                systemLable.Clear();
                                systemLable.Add(" COM2 이 증명했습니다.");
                                systemLable.Add(" 다음 턴을 진행합니다.");
                                gm.ViewSystemComment(systemLable, 0);
                                Thread.Sleep(300);
                            }
                            else
                            {
                                Console.Clear();

                                gm.ViewSystemDescription();  //전체 시스템 Description
                                Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                gm.ViewGameState();

                                systemLable.Clear();
                                systemLable.Add(" COM2 이 증명하지 못하였습니다.");
                                systemLable.Add(" 다음 사람이 증명을 진행합니다.");
                                gm.ViewSystemComment(systemLable, 0);
                                Thread.Sleep(300);

                                if (com3.Prove(gm))
                                {
                                    //com3 증명
                                    Card provCard3 = com3.GetProvCard(gm);
                                    com1.AddKnowCard(provCard3);

                                    Console.Clear();

                                    gm.ViewSystemDescription();  //전체 시스템 Description
                                    Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                    gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                    gm.ViewGameState();
                                    
                                    systemLable.Clear();
                                    systemLable.Add(" COM3 이 증명했습니다.");
                                    systemLable.Add(" 다음 턴을 진행합니다.");
                                    gm.ViewSystemComment(systemLable, 0);
                                    Thread.Sleep(300);
                                }
                                else
                                {
                                    Console.Clear();

                                    gm.ViewSystemDescription();  //전체 시스템 Description
                                    Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                    gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                    gm.ViewGameState();

                                    systemLable.Clear();
                                    systemLable.Add(" COM3 이 증명하지 못하였습니다.");
                                    systemLable.Add(" 다음 사람이 증명을 진행합니다.");
                                    gm.ViewSystemComment(systemLable, 0);
                                    Thread.Sleep(300);
                                    {
                                        Card provCardU = gm.UserProv();
                                        if (!provCardU.GetName().Equals("temp"))
                                        {
                                            com1.AddKnowCard(provCardU);
                                        }
                                        else
                                        {
                                            Console.Clear();

                                            gm.ViewSystemDescription();  //전체 시스템 Description
                                            Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                            gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                            gm.ViewGameState();

                                            systemLable.Clear();
                                            systemLable.Add(" 모든 플레이어가 증명하지 못하였습니다.");
                                            systemLable.Add(" 다음 턴을 진행합니다.");
                                            gm.ViewSystemComment(systemLable, 0);
                                            Thread.Sleep(300);

                                            gm.SetGuessCard(null);
                                        }
                                    }
                                }
                            }
                        }

                        if (isMove)
                        {
                            if (isMiddle)
                            {
                                //2번(중앙홀로) 이동하기
                                com1.checkLoc = 2;
                                List<(int, int)> path = com1.FindShortestPath(gm.map, com1.position, (9, 11));
                                if (path != null)
                                {
                                    com1.goPath.Clear();
                                    for (int i = 0; i < path.Count; i++)
                                    {   //가고있는 경로 큐에 담음
                                        com1.goPath.Enqueue(path[i]);
                                    }
                                }
                                com1.ComMove(gm, user, com1, com2, com3);
                            }
                            else
                            {
                                if (com1.CheckGoLocation()) //새로운 장소를 찾아야하는지 여부
                                {
                                    com1.checkLoc = com1.GetRandomNum(gm.GetAllCard());
                                    List<(int, int)> path = com1.FindShortestPath(gm.map, com1.position, com1.GetCoorByNum(com1.checkLoc));
                                    if (path != null)
                                    {
                                        com1.goPath.Clear();
                                        for (int i = 0; i < path.Count; i++)
                                        {   //가고있는 경로 큐에 담음
                                            com1.goPath.Enqueue(path[i]);
                                        }
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
                        gm.ViewGameState();             //현재 턴 notice
                        Thread.Sleep(200);

                        //********************************** 턴 시작하면 주사위 던지고 시작하기
                        systemLable.Clear();
                        int rollCount = gm.RollDice();  //주사위 굴리기
                        com2.SetMoveCount(rollCount);   //유저 객체에 현재 diceCount 추가

                        gm.ViewSystemDescription();  //전체 시스템 Description
                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                        gm.ViewGameState();

                        systemLable.Clear();
                        gm.ViewGameState();
                        gm.ViewSystemComment(systemLable, 0);
                        user.ViewUserState(false);

                        bool isMove = false;    //이동할건지 flag
                        bool isMiddle = false;  //목표 장소가 중앙인지 flag

                        if (com2.CheckCanFinal(gm) == true)    //최종 추리 가능한지 확인
                        {
                            if (com2.CheckComPosMiddle()) //현재 중앙홀에 있는지 확인
                            {
                                List<Card> finalCards = com2.FinalGuess(gm);    //모르는 카드중 1개씩 return
                                int corrNum = com2.CheckFinalGuess(gm, finalCards);

                                com2.ViewFinalGuessIntro(1);

                                if (corrNum == 3)
                                {
                                    gm.SetWinner(2);
                                    com2.ViewFinalGuessCheckIntro(2,true);  //최종추리 label
                                    gm.EndGame();
                                    break;
                                }
                                else
                                {
                                    com2.SetDie();
                                }
                            }
                            else
                            {
                                //중앙으로 이동해서 추리
                                isMove = true;
                                isMiddle = true;
                            }
                        }
                        else
                        {
                            if (com2.CheckIsLoc()) //현재 장소가 장소인지 체크
                            {
                                if (!com2.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com2.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(2);
                                    gm.ViewGuessListIntro(2);
                                }
                            }
                            else
                            {   //현재 위치가 장소가 아닐때
                                systemLable.Clear();
                                systemLable.Add(" COM2 이 이동합니다.        ");
                                gm.ViewSystemComment(systemLable, 0);

                                isMiddle = false;
                                isMove = true;
                            }
                        }

                        if (gm.GetGuessUser() == 2)  //현재 추리중인 유저가 1이면
                        {
                            if (com3.Prove(gm))
                            {
                                //com2 증명
                                Card provCard3 = com3.GetProvCard(gm);
                                com2.AddKnowCard(provCard3);
                                
                                Console.Clear();

                                gm.ViewSystemDescription();  //전체 시스템 Description
                                Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                gm.ViewUserCardList();          //유저 카드 목록 뿌려주기

                                gm.ViewGameState();
                                systemLable.Clear();
                                systemLable.Add(" COM3 이 증명했습니다.");
                                systemLable.Add(" 다음 턴을 진행합니다.");
                                gm.ViewSystemComment(systemLable, 0);
                                Thread.Sleep(300);
                            }
                            else
                            {
                                Console.Clear();

                                gm.ViewSystemDescription();  //전체 시스템 Description
                                Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                gm.ViewGameState();

                                systemLable.Clear();
                                systemLable.Add(" COM3 이 증명하지 못하였습니다.");
                                systemLable.Add(" 다음 사람이 증명을 진행합니다.");
                                gm.ViewSystemComment(systemLable, 0);
                                Thread.Sleep(300);

                                Card provCardU = gm.UserProv();
                                if (!provCardU.GetName().Equals("temp"))
                                {
                                    com1.AddKnowCard(provCardU);
                                }
                                else
                                {
                                    Console.Clear();

                                    gm.ViewSystemDescription();  //전체 시스템 Description
                                    Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                    gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                    gm.ViewGameState();

                                    systemLable.Clear();
                                    systemLable.Add(" 당신이 이 증명하지 못하였습니다.");
                                    systemLable.Add(" 다음 사람이 증명을 진행합니다.");
                                    gm.ViewSystemComment(systemLable, 0);
                                    Thread.Sleep(300);

                                    if (com1.Prove(gm))
                                    {
                                        Console.Clear();

                                        //com1 증명
                                        Card provCard1 = com1.GetProvCard(gm);
                                        com2.AddKnowCard(provCard1);

                                        gm.ViewSystemDescription();  //전체 시스템 Description
                                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                        gm.ViewGameState();

                                        systemLable.Clear();
                                        systemLable.Add(" COM1 이 증명했습니다.");
                                        systemLable.Add(" 다음 턴을 진행합니다.");
                                        gm.ViewSystemComment(systemLable, 0);
                                        Thread.Sleep(300);
                                    }
                                    else
                                    {
                                        Console.Clear();

                                        gm.ViewSystemDescription();  //전체 시스템 Description
                                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                        gm.ViewGameState();

                                        systemLable.Clear();
                                        systemLable.Add(" 모든 플레이어가 증명하지 못하였습니다.");
                                        systemLable.Add(" 다음 턴을 진행합니다.");
                                        gm.ViewSystemComment(systemLable, 0);
                                        Thread.Sleep(300);

                                        gm.SetGuessCard(null);
                                    }
                                }
                            }
                        }

                        if (isMove)
                        {
                            if (isMiddle)
                            {
                                //2번(중앙홀로) 이동하기
                                com2.checkLoc = 2;
                                List<(int, int)> path = com2.FindShortestPath(gm.map, com2.position, (9, 11));
                                if (path != null)
                                {
                                    com2.goPath.Clear();
                                    for (int i = 0; i < path.Count; i++)
                                    {   //가고있는 경로 큐에 담음
                                        com2.goPath.Enqueue(path[i]);
                                    }
                                }
                                com2.ComMove(gm, user, com1, com2, com3);
                            }
                            else
                            {
                                if (com2.CheckGoLocation()) //새로운 장소를 찾아야하는지 여부
                                {
                                    com2.checkLoc = com2.GetRandomNum(gm.GetAllCard());
                                    List<(int, int)> path = com2.FindShortestPath(gm.map, com2.position, com2.GetCoorByNum(com2.checkLoc));
                                    if (path != null)
                                    {
                                        com2.goPath.Clear();
                                        for (int i = 0; i < path.Count; i++)
                                        {   //가고있는 경로 큐에 담음
                                            com2.goPath.Enqueue(path[i]);
                                        }
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
                else if (gm.turn == 3)
                {
                   if (com3.GetAlive())    //살아있는지 여부
                    {
                        gm.ViewGameState();             //현재 턴 notice
                        Thread.Sleep(200);

                        //********************************** 턴 시작하면 주사위 던지고 시작하기
                        systemLable.Clear();
                        int rollCount = gm.RollDice();  //주사위 굴리기
                        com3.SetMoveCount(rollCount);   //유저 객체에 현재 diceCount 추가

                        gm.ViewSystemDescription();  //전체 시스템 Description
                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                        gm.ViewGameState();

                        systemLable.Clear();
                        gm.ViewGameState();
                        gm.ViewSystemComment(systemLable, 0);
                        user.ViewUserState(false);

                        bool isMove = false;    //이동할건지 flag
                        bool isMiddle = false;  //목표 장소가 중앙인지 flag

                        if (com3.CheckCanFinal(gm) == true)    //최종 추리 가능한지 확인
                        {
                            if (com3.CheckComPosMiddle()) //현재 중앙홀에 있는지 확인
                            {
                                List<Card> finalCards = com1.FinalGuess(gm);    //모르는 카드중 1개씩 return
                                int corrNum = com3.CheckFinalGuess(gm, finalCards);

                                com3.ViewFinalGuessIntro(1);

                                if (corrNum == 3)
                                {
                                    gm.SetWinner(3);
                                    com3.ViewFinalGuessCheckIntro(3,true);  //최종추리 label
                                    gm.EndGame();
                                    break;
                                }
                                else
                                {
                                    com3.SetDie();
                                }
                            }
                            else
                            {
                                //중앙으로 이동해서 추리
                                isMove = true;
                                isMiddle = true;
                            }
                        }
                        else
                        {
                            if (com3.CheckIsLoc()) //현재 장소가 장소인지 체크
                            {
                                if (!com1.CheckThisKnow())   //현재 장소가 이미 알고있는 카드가 아니면
                                {
                                    com3.Guess(gm); //추리할카드 저장
                                    gm.SetGuessUser(1);
                                    gm.ViewGuessListIntro(1);
                                }
                            }
                            else
                            {   //현재 위치가 장소가 아닐때
                                systemLable.Clear();
                                systemLable.Add(" COM3 이 이동합니다.        ");
                                gm.ViewSystemComment(systemLable, 0);

                                isMiddle = false;
                                isMove = true;
                            }
                        }

                        if (gm.GetGuessUser() == 3)  //현재 추리중인 유저가 3이면
                        {
                            Card provCardU = gm.UserProv();
                            if (!provCardU.GetName().Equals("temp"))
                            {
                                com1.AddKnowCard(provCardU);
                            }
                            else
                            {
                                Console.Clear();
                                systemLable.Clear();
                                systemLable.Add(" 당신이 증명하지 못하였습니다.");
                                systemLable.Add(" 다음 사람이 증명을 진행합니다.");
                                gm.ViewSystemComment(systemLable, 0);

                                gm.ViewSystemDescription();  //전체 시스템 Description
                                Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                gm.ViewGameState();

                                Thread.Sleep(300);

                                if (com1.Prove(gm))
                                {
                                    Console.Clear();
                                    //com1 증명
                                    Card provCard1 = com1.GetProvCard(gm);
                                    com3.AddKnowCard(provCard1);

                                    gm.ViewSystemDescription();  //전체 시스템 Description
                                    Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                    gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                    gm.ViewGameState();

                                    systemLable.Clear();
                                    systemLable.Add(" COM1 이 증명했습니다.");
                                    systemLable.Add(" 다음 턴을 진행합니다.");
                                    gm.ViewSystemComment(systemLable, 0);
                                    Thread.Sleep(300);
                                }
                                else
                                {
                                    Console.Clear();

                                    systemLable.Clear();
                                    systemLable.Add(" COM1 이 증명하지 못하였습니다.");
                                    systemLable.Add(" 다음 사람이 증명을 진행합니다.");

                                    gm.ViewSystemDescription();  //전체 시스템 Description
                                    Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                    gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                    gm.ViewGameState();
                                    gm.ViewSystemComment(systemLable, 0);
                                    Thread.Sleep(300);

                                    if (com2.Prove(gm))
                                    {
                                        Console.Clear();
                                        //com2 증명
                                        Card provCard2 = com2.GetProvCard(gm);
                                        com3.AddKnowCard(provCard2);

                                        gm.ViewSystemDescription();  //전체 시스템 Description
                                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                        gm.ViewGameState();

                                        systemLable.Clear();
                                        systemLable.Add(" COM2 이 증명했습니다.");
                                        systemLable.Add(" 다음 턴을 진행합니다.");
                                        gm.ViewSystemComment(systemLable, 0);
                                        Thread.Sleep(300);
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        gm.ViewSystemDescription();  //전체 시스템 Description
                                        Init.ViewMap(gm, user, com1, com2, com3);   //맵 뿌려주기
                                        gm.ViewUserCardList();          //유저 카드 목록 뿌려주기
                                        gm.ViewGameState();

                                        systemLable.Clear();
                                        systemLable.Add(" 모든사람이 증명하지 못하였습니다.");
                                        systemLable.Add(" 다음 턴을 진행합니다.");
                                        gm.ViewSystemComment(systemLable, 0);
                                        Thread.Sleep(300);
                                        
                                        gm.SetGuessCard(null);
                                    }
                                }

                            }
                        }

                        if (isMove)
                        {
                            if (isMiddle)
                            {
                                //2번(중앙홀로) 이동하기
                                com3.checkLoc = 2;
                                List<(int, int)> path = com3.FindShortestPath(gm.map, com3.position, (9, 11));
                                if (path != null)
                                {
                                    com3.goPath.Clear();
                                    for (int i = 0; i < path.Count; i++)
                                    {   //가고있는 경로 큐에 담음
                                        com3.goPath.Enqueue(path[i]);
                                    }
                                }
                                com3.ComMove(gm, user, com1, com2, com3);
                            }
                            else
                            {
                                if (com3.CheckGoLocation()) //새로운 장소를 찾아야하는지 여부
                                {
                                    com3.checkLoc = com3.GetRandomNum(gm.GetAllCard());
                                    List<(int, int)> path = com3.FindShortestPath(gm.map, com3.position, com3.GetCoorByNum(com3.checkLoc));
                                    if (path != null)
                                    {
                                        com3.goPath.Clear();
                                        for (int i = 0; i < path.Count; i++)
                                        {   //가고있는 경로 큐에 담음
                                            com3.goPath.Enqueue(path[i]);
                                        }
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
                EndinngIntro(gm.GetWinner());
            }
        }
    }
}
