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
        public bool isDeck { get; private set; }

        public Card(String id, String name, double damage, int packageId, String username, bool isDeck)
        {
            this.id = id;
            this.name = name;
            this.damage = damage;
            this.packageId = packageId;
            this.username = username;
            this.isDeck = isDeck;
        }
    }
}
