using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.battle
{
    public class CardController
    {
        public enum Element_Type { Water, Fire, Reqular }
        public enum Monster_Type { Goblin, Dragon, Wizard ,Ork ,Knight , Kraken, Elf , Troll }
        public Element_Type getElementType(String element_type)
        {
            switch (element_type)
            {
                case "Water":
                    return Element_Type.Water;
                case "Fire":
                    return Element_Type.Fire;
                default:
                    return Element_Type.Reqular;
            }
        }
        public Monster_Type getMonsterType(String monster_type)
        {
            switch (monster_type)
            {
                case "Goblin":
                    return Monster_Type.Goblin;
                case "Dragon":
                    return Monster_Type.Dragon;
                case "Wizard":
                    return Monster_Type.Wizard;
                case "Ork":
                    return Monster_Type.Ork;
                case "Knight":
                    return Monster_Type.Knight;
                case "Kraken":
                    return Monster_Type.Kraken;
                case "Elf":
                    return Monster_Type.Elf;
                default:
                    return Monster_Type.Troll;
            }
        }
        public int compareDamageDouble(double card1, double card2)
        {
            if ((card1 * 2) > (card2 / 2))
                return 1;
            else if ((card1 * 2) < (card2 / 2))
                return -1;
            else
                return 0;
        }
        public int compareDamage(double card1, double card2)
        {
            if (card1 > card2)
                return 1;
            else if (card1 < card2)
                return -1;
            else
                return 0;
        }
        public int compareElementType(Monster_Type card1, Monster_Type card2)
        {
            switch (card1)
            {   
                case Monster_Type.Goblin:
                    {
                        if (card2 == Monster_Type.Dragon)
                            return -1;
                        return 0;
                    }
                case Monster_Type.Dragon:
                    {
                        if (card2 == Monster_Type.Goblin)
                            return 1;
                        else if (card2 == Monster_Type.Elf)
                            return -2;
                        return 0;
                    }
                case Monster_Type.Wizard:
                    {
                        if (card2 == Monster_Type.Ork)
                            return 1;
                        return 0;
                    }
                case Monster_Type.Ork:
                    {
                        if (card2 == Monster_Type.Wizard)
                            return -1;
                        return 0;
                    }
                case Monster_Type.Elf:
                    {
                        if (card2 == Monster_Type.Dragon)
                            return 2;
                        return 0;
                    }
                default:
                    return 0;
            }
        }
    }
}
