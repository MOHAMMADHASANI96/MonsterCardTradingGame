using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.data_layer.entity
{
    public class Stat
    {
        public String username { get; private set; }
        public int elo { get; private set; }
        public int win { get; private set; }
        public int lose { get; private set; }
        public int draw { get; private set; }
        public int num_play { get; private set; }

        public Stat(String username, int elo, int win, int lose, int draw, int num_play)
        {
            this.username = username;
            this.elo = elo;
            this.win = win;
            this.lose = lose;
            this.draw = draw;
            this.num_play = num_play;
        }
    }
}
