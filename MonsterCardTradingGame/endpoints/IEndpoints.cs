using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public interface IEndpoints
    {
        public Response processRequest(Request request);
    }
}
