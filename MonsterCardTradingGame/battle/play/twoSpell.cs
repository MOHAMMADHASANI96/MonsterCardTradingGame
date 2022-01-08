using MonsterCardTradingGame.data_layer.entity;
using static MonsterCardTradingGame.battle.CardController;


namespace MonsterCardTradingGame.battle.play
{
    public class TwoSpell : IPlay
    {
        public int processRequest(Card cardA, Card cardB)
        {
            // water->fire && fire->normal && normal->water
            Element_Type element_type_A = new CardController().getElementType(cardA.element_type);
            Element_Type element_type_B = new CardController().getElementType(cardB.element_type);

            if((element_type_A == Element_Type.Water && element_type_B == Element_Type.Fire) ||
               (element_type_A == Element_Type.Fire && element_type_B == Element_Type.Reqular) ||
                (element_type_A == Element_Type.Reqular && element_type_B == Element_Type.Water))
                return new CardController().compareDamageDouble(cardA.damage, cardB.damage);

            else if((element_type_A == Element_Type.Water && element_type_B == Element_Type.Water) ||
               (element_type_A == Element_Type.Fire && element_type_B == Element_Type.Fire) ||
                (element_type_A == Element_Type.Reqular && element_type_B == Element_Type.Reqular))
                return new CardController().compareDamage(cardA.damage, cardB.damage);

            else
                return -(new CardController().compareDamageDouble(cardB.damage, cardA.damage));     
        }
    }
}

/*
            switch (element_type_A)
            {
                case Element_Type.Water:
                    {
                        if (element_type_B == Element_Type.Fire)
                            // Damage playerA *2 && damage playerB /2
                            return new CardController().copmairDamage(cardA.damage, cardB.damage);
                        else if (element_type_B == Element_Type.Reqular)
                            // Damage playerB*2 && damage playerA/2
                            return -(new CardController().copmairDamage(cardB.damage, cardA.damage));
                        // no change of damage
                        else
                            return new CardController().copmairDamageSameElementType(cardA.damage, cardB.damage);
                    }
                case Element_Type.Fire:
                    {
                        if (element_type_B == Element_Type.Reqular)
                            // Damage playerA *2 && damage playerB /2
                            return new CardController().copmairDamage(cardA.damage, cardB.damage);
                        else if (element_type_B == Element_Type.Water)
                            // Damage playerB*2 && damage playerA/2
                            return -(new CardController().copmairDamage(cardB.damage, cardA.damage));
                        else
                            // no change of damage
                            return new CardController().copmairDamageSameElementType(cardA.damage, cardB.damage);
                    }
                default:
                    {
                        if (element_type_B == Element_Type.Water)
                            // Damage playerA *2 && damage playerB /2
                            return new CardController().copmairDamage(cardA.damage, cardB.damage);
                        else if (element_type_B == Element_Type.Fire)
                            // Damage playerB*2 && damage playerA/2
                            return -(new CardController().copmairDamage(cardB.damage, cardA.damage));
                        else
                            // no change of damage
                            return new CardController().copmairDamageSameElementType(cardA.damage, cardB.damage);
                    }
            }
            */
