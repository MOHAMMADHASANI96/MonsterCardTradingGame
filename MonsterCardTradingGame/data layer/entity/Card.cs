using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.data_layer.entity
{
    public class Card
    {
        public String id { get; private set; }
        public String name { get; private set; }
        public double damage { get; private set; }
        public int packageId { get; private set; }
        public String username { get; private set; }
        public bool is_deck { get; private set; }
        public String element_type { get; private set; }
        public String card_type { get; private set; }

        public Card(String id, String name, double damage, int packageId, String username, bool is_deck,String element_type, String card_type)
        {
            this.id = id;
            this.name = name;
            this.damage = damage;
            this.packageId = packageId;
            this.username = username;
            this.is_deck = is_deck;
            this.element_type = element_type;
            this.card_type = card_type;
        }
    }
}
