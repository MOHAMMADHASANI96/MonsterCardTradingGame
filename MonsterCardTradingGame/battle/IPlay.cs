using MonsterCardTradingGame.data_layer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.battle
{
    public interface IPlay
    {
        public bool processRequest(Card cardA, Card cardB);
    }
}
