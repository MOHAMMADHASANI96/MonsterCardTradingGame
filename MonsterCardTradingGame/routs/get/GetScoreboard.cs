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
    public class GetScoreboard : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                List<Stat> listStat = new StatsRepository().getListStat();

                if(listStat.Count ==0)
                    return ResponseHelper.okJsonPayload("no Users");

                String jsonString = JsonConvert.SerializeObject(listStat);
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
