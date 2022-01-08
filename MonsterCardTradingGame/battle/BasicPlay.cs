using MonsterCardTradingGame.battle.play;
using MonsterCardTradingGame.data_layer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.battle
{
    public class BasicPlay
    {
        public int getResult(Card cardA,Card cardB)
        {
            String resultCompare = compareTypeCards(cardA,cardB);
            switch (resultCompare)
            {
                case "SS":
                    return new TwoSpell().processRequest(cardA, cardB);
                case "MM":
                    return new TwoMonster().processRequest(cardA, cardB);
                default:
                    return new OneSpellOneMonster().processRequest(cardA, cardB);
            }
        }

        public String compareTypeCards(Card cardA,Card cardB)
        {
            // 1-> 2 spell  2-> 2 monster  3-> one monster one spell
            if ((cardA.card_type.ToLower().Equals("spell")) && (cardB.card_type.ToLower().Equals("spell")))
                return "SS";
            else if (!(cardA.card_type.ToLower().Equals("spell")) && !(cardB.card_type.ToLower().Equals("spell")))
                return "MM";
            else
                return "MS";
        }

    }
}
