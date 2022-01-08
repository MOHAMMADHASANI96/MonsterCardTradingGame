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
        public bool getResult(Card cardA,Card cardB)
        {
            String resultCompair = compairTypeCards(cardA,cardB);
            switch (resultCompair)
            {
                case "SS":
                    return new twoSpell().processRequest(cardA, cardB);
                case "MM":
                    return new twoMonster().processRequest(cardA, cardB);
                default:
                    return new oneSpellOneMonster().processRequest(cardA, cardB);
            }
        }

        public String compairTypeCards(Card cardA,Card cardB)
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
