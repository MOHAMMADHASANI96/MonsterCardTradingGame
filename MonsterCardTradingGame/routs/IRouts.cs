using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public interface IRouts
    {
        public Response processRequest(Request request);
    }
}
