using MonsterCardTradingGame.data_layer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonsterCardTradingGame.battle.CardController;

namespace MonsterCardTradingGame.battle.play
{
    public class TwoMonsters : IPlay
    {
        public int processRequest(Card cardA, Card cardB)
        {
            Monster_Type monster_type_A = new CardController().getMonsterType(cardA.card_type);
            Monster_Type monster_type_B = new CardController().getMonsterType(cardB.card_type);

            int compareElement = new CardController().compareElementType(monster_type_A, monster_type_B);
            
            if (compareElement == 1)
                return 1;
            else if (compareElement == -1)
                return -1;
            else if(compareElement == 2)
            {
                if (new CardController().getElementType(cardA.element_type) == Element_Type.Fire)
                    return 1;
                return new CardController().compareDamage(cardA.damage, cardB.damage);
            }
            else if(compareElement ==-2)
            {
                if (new CardController().getElementType(cardB.element_type) == Element_Type.Fire)
                    return -1;
                return new CardController().compareDamage(cardA.damage, cardB.damage);
            }
            else
                return new CardController().compareDamage(cardA.damage, cardB.damage);
        }
    }
}
