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
        public string getName()
        {
            return name;
        }

        public string getTypeString()
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

        public CardType getCardType()
        {
            return this.type;
        }

    }
}
