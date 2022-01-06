using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.get
{
    public class GetStats : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                Stat stat = new StatsRepository().getStat(user.username);

                String jsonString = JsonConvert.SerializeObject(user);
                jsonString += JsonConvert.SerializeObject(stat);

                return ResponseHelper.okJsonPayload(jsonString);

            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.forbidden();
            }
        }
    }
}
