using MonsterCardTradingGame.data_layer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonsterCardTradingGame.battle.CardController;

namespace MonsterCardTradingGame.battle.play
{
    public class OneSpellOneMonster : IPlay
    {
        public int processRequest(Card cardA, Card cardB)
        {
            // cardA ->spell & cardB ->monster
            if (isSpell(cardA))
            {
                // kraken & spell -> karaken
                if (new CardController().getMonsterType(cardB.card_type) == Monster_Type.Kraken)
                    return -1;

                // knights & waterSpell -> waterSpell win
                else if (new CardController().getMonsterType(cardB.card_type) == Monster_Type.Knight &&
                    new CardController().getElementType(cardA.element_type) == Element_Type.Water)
                    return 1;

                // pure monster & spell -> compare damage
                else if (isPureMonster(cardB))
                    return new CardController().compareDamage(cardA.damage, cardB.damage);

                // not pure monster & spell -> compere damage double
                // same twoSpell card
                return new TwoSpell().processRequest(cardA, cardB);
            }
            // cardA ->monster & cardB ->spell
            else
            {
                // kraken & spell -> karaken
                if (new CardController().getMonsterType(cardA.card_type) == Monster_Type.Kraken)
                    return 1;

                // knights & waterSpell -> waterSpell win
                else if (new CardController().getMonsterType(cardA.card_type) == Monster_Type.Knight &&
                    new CardController().getElementType(cardB.element_type) == Element_Type.Water)
                    return -1;

                // pure monster & spell -> compare damage
                else if (isPureMonster(cardA))
                    return new CardController().compareDamage(cardA.damage, cardB.damage);

                // not pure monster & spell -> compere damage double
                // same twoSpell card
                return new TwoSpell().processRequest(cardA, cardB);
            }
        }
        public bool isSpell(Card card)
        {
            if (card.card_type.ToLower().Equals("spell"))
                return true;
            return false;
        }
        public bool isPureMonster(Card card)
        {
            if (card.element_type == null)
                return true;
            return false;
        }
    }
}
