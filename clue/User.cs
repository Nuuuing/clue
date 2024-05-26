using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class User : Player
    {
        public User(List<Card> startCard)
        {
            addCard(startCard);
        }

        public void move(MoveDir dir)
        {
            //한칸씩 이동 -> 장소타일 위에서는 가운데로 이동, 같은 장소 타일에서는 이동 불가능,
            //움직일 칸 + 현재칸 같은데 0이 아닐때 -> (같은타일) 이동 불가넝
            //TODO: 워프하는 칸도 체크하기
            //TODO: 화면 세팅하면 좌표값 수정하기

            switch (dir)
            {
                case MoveDir.TOP:
                    if (GameManager.Instance.map[position.Item1 - 1, position.Item2] != 1)
                        if (!(GameManager.Instance.map[position.Item1 - 1, position.Item2] == GameManager.Instance.map[position.Item1, position.Item2]))
                        {   //바꿀 포지션 = 지금 포지션이랑 같지 않으면
                            if (GameManager.Instance.map[position.Item1 - 1, position.Item2] == 3)
                            {
                                setMoveCount(getMoveCount() - 1);
                                this.position = (3, 11);
                            }
                            else if (GameManager.Instance.map[position.Item1 - 1, position.Item2] == 5)
                            {
                                setMoveCount(getMoveCount() - 1);
                                this.position = (2, 18);
                            }
                            else if (GameManager.Instance.map[position.Item1 - 1, position.Item2] == 4)
                            {
                                setMoveCount(getMoveCount() - 1);
                                this.position = (2, 4);
                            }
                            else if (GameManager.Instance.map[position.Item1 - 1, position.Item2] == 11)
                            {
                                setMoveCount(getMoveCount() - 1);
                                this.position = (8, 3);
                            }
                            else
                            {
                                setMoveCount(getMoveCount() - 1);
                                this.position = (position.Item1 - 1, position.Item2);
                            }
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 2)
                        {
                            setMoveCount(getMoveCount() -1);
                            this.position = (7, 12);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 0)
                        {
                            setMoveCount(getMoveCount() - 1);
                            this.position = (position.Item1 - 1, position.Item2);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 9)
                        {
                            setMoveCount(getMoveCount() - 1);
                            this.position = (15, 4);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 8)
                        {
                            setMoveCount(getMoveCount() - 1);
                            this.position = (15, 11);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 7)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (15, 17);
                        }
                    break;
                case MoveDir.RIGHT:
                    if (GameManager.Instance.map[position.Item1, position.Item2 + 1] != 1)
                        if (!(GameManager.Instance.map[position.Item1, position.Item2 + 1] == GameManager.Instance.map[position.Item1, position.Item2]))
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (position.Item1, position.Item2 + 1);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 2)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (9, 14);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 0)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (position.Item1, position.Item2 + 1);
                        }
                    break;
                case MoveDir.LEFT:
                    if (GameManager.Instance.map[position.Item1, position.Item2 - 1] != 1)
                        if (!(GameManager.Instance.map[position.Item1, position.Item2 - 1] == GameManager.Instance.map[position.Item1, position.Item2]))
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (position.Item1, position.Item2 - 1);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 2)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (9, 10);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 0)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (position.Item1, position.Item2 - 1);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 9)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (14, 2);
                        }
                    break;
                case MoveDir.BOTTOM:
                    if (GameManager.Instance.map[position.Item1 + 1, position.Item2] != 1)
                        if (!(GameManager.Instance.map[position.Item1 + 1, position.Item2] == GameManager.Instance.map[position.Item1, position.Item2]))
                        {
                            //7, 8, 9,10
                            if (GameManager.Instance.map[position.Item1 + 1, position.Item2] == 7)
                            {
                                 setMoveCount(getMoveCount() -1);;
                                this.position = (16, 18);
                            }
                            else if (GameManager.Instance.map[position.Item1 + 1, position.Item2] == 8)
                            {
                                 setMoveCount(getMoveCount() -1);;
                                this.position = (16, 11);
                            }
                            if (GameManager.Instance.map[position.Item1 + 1, position.Item2] == 9)
                            {
                                 setMoveCount(getMoveCount() -1);;
                                this.position = (17, 3);
                            }
                            else if (GameManager.Instance.map[position.Item1 + 1, position.Item2] == 10)
                            {
                                 setMoveCount(getMoveCount() -1);;
                                this.position = (17, 3);
                            }
                            else
                            {
                                 setMoveCount(getMoveCount() -1);;
                                this.position = (position.Item1 + 1, position.Item2);
                            }
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 0)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (position.Item1 + 1, position.Item2);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 2)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (12, 12);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 11)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (11, 4);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 10)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (17, 3);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 5)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (4, 17);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 3)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (5, 11);
                        }
                        else if (GameManager.Instance.map[position.Item1, position.Item2] == 4)
                        {
                             setMoveCount(getMoveCount() -1);;
                            this.position = (4, 6);
                        }
                    break;
            }
        }


    }
}
