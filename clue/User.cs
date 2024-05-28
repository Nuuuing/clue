using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class User : Player
    {
        bool isMove = false;

        public void SetUserMove(bool _flag)
        {
            isMove = _flag;
        }

        public bool GetUserMove()
        {
            return isMove;
        }

        public User(List<Card> startCard)
        {
            AddCard(startCard);
        }

        public void Move(MoveDir dir)   //유저 이동 
        {
            switch (dir)
            {
                case MoveDir.TOP:
                    if ( this.GetLocByCoor( (position.Item1, position.Item2) ) == 2 ||
                            this.GetLocByCoor((position.Item1, position.Item2)) == 0 ||
                                this.GetLocByCoor((position.Item1, position.Item2)) == 7 ||
                                    this.GetLocByCoor((position.Item1, position.Item2)) == 8 ||
                                        this.GetLocByCoor((position.Item1, position.Item2)) == 9 )
                    {
                        if (GameManager.Instance.map[position.Item1 - 1, position.Item2] != 1)
                        {
                            this.position = (position.Item1 -1 , position.Item2);
                            SetMoveCount(GetMoveCount() - 1);
                        }
                    }
                    break;
                case MoveDir.RIGHT:
                    if ( this.GetLocByCoor((position.Item1, position.Item2)) == 2 ||
                            this.GetLocByCoor((position.Item1, position.Item2)) == 0 ||
                                this.GetLocByCoor((position.Item1, position.Item2)) == 10 )
                    {
                        if (GameManager.Instance.map[position.Item1, position.Item2 + 1] != 1)
                        {
                            this.position = (position.Item1, position.Item2 + 1);
                            SetMoveCount(GetMoveCount() - 1);
                        }
                    }
                    break;
                case MoveDir.LEFT:
                    if ( this.GetLocByCoor((position.Item1, position.Item2)) == 2 ||
                            this.GetLocByCoor((position.Item1, position.Item2)) == 0 ||
                                this.GetLocByCoor((position.Item1, position.Item2)) == 6 )
                    {
                        if (GameManager.Instance.map[position.Item1, position.Item2 - 1] != 1)
                        {
                            this.position = (position.Item1, position.Item2 - 1);
                            SetMoveCount(GetMoveCount() - 1);
                        }
                    }
                    break;
                case MoveDir.BOTTOM:
                    if ( this.GetLocByCoor((position.Item1, position.Item2)) == 2 ||
                            this.GetLocByCoor((position.Item1, position.Item2)) == 0 ||
                                this.GetLocByCoor((position.Item1, position.Item2)) == 3 || 
                                    this.GetLocByCoor((position.Item1, position.Item2)) == 4 ||
                                        this.GetLocByCoor((position.Item1, position.Item2)) == 5 ||
                                            this.GetLocByCoor((position.Item1, position.Item2)) == 11 )
                    {
                        if (GameManager.Instance.map[position.Item1 + 1, position.Item2] != 1)
                        {
                            this.position = (position.Item1 + 1, position.Item2);
                            SetMoveCount(GetMoveCount() - 1);
                        }
                    }
                    break;
            }
        }

    }
}
