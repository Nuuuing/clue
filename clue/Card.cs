using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clue
{
    class Card
    {
        int key;
        CardType type;  //카드 타입
        string name;    //카드 명
        bool know = false;      //카드 확인 여부

        public Card(int key, CardType type, string name)
        {
            this.key = key;
            this.type = type;
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public int GetLocNum()
        {
            if(this.type.Equals(CardType.LOC))
            {
                string gName = this.name;
               if(gName.Equals("식당"))
                {
                    return 3;
                }
               else if (gName.Equals("식당"))
                {
                    return 4;
                }
                else if (gName.Equals("거실"))
                {
                    return 5;
                }
                else if (gName.Equals("마당"))
                {
                    return 6;
                }
                else if (gName.Equals("차고"))
                {
                    return 7;
                }
                else if (gName.Equals("게임룸"))
                {
                    return 8;
                }
                else if (gName.Equals("침실"))
                {
                    return 9;
                }
                else if (gName.Equals("욕실"))
                {
                    return 10;
                }
                else
                {   //서재
                    return 11;
                }
            }
            else
            {
                return 0;
            }
        }

        public string GetTypeString()
        {
            string typeName = "";
            switch (this.type)
            {
                case CardType.LOC:
                    typeName = "장소";
                    break;
                case CardType.PER:
                    typeName = "인물";
                    break;
                case CardType.WEP:
                    typeName = "무기";
                    break;
            }
            return typeName;
        }

        public CardType GetCardType()
        {
            return this.type;
        }
    }
}
