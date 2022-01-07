using System;


namespace MonsterCardTradingGame.data_layer.entity
{
    public class Trade 
    {
        public String username { get; private set; }
        public String id { get; private set; }
        public String trade_id { get; private set; }
        public String type { get; private set; }
        public double min_damage { get; private set; }
        public bool is_sold { get; private set; }

        public Trade(String username, String id, String trade_id, String type, double min_damage, bool is_sold)
        {
            this.username = username;
            this.id = id;
            this.trade_id = trade_id;
            this.type = type;
            this.min_damage = min_damage;
            this.is_sold = is_sold;
        }
    }
}
