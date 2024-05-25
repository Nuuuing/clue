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

            //while (gm.Running)
            //{
            //    if (turn == 0)
            //    {
            //        //유저 턴
            //    }
            //    else if (turn == 1)
            //    {
            //        //com1 턴
            //    }
            //    else if (turn == 2)
            //    {
            //        //com2 turn 
            //    }
            //    else
            //    {
            //        //com3 turn
            //    }
            //    //맵 뿌려주고
            //    //키 입력받아서 이동
            //    //현재 턴 체크 (유저 or com)
            //    //
            //}

            //Init.viewGameState(user);
            //user.getCardList();

            ConsoleKey key;

            user.setMoveCount(gm.rollDice());

            while (true)
            {

                Console.Clear();

                Console.WriteLine(user.position);
                Console.WriteLine(GameManager.Instance.map[user.position.Item1,user.position.Item2]);
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
    }
}
