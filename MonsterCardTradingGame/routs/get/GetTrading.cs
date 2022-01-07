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
    public class GetTrading : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                List<Trade> listTrade = new TradesRepository().getTrade();

                if (listTrade.Count == 0)
                    return ResponseHelper.ok("Trade is empty");
                String jsonString = JsonConvert.SerializeObject(listTrade);
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
