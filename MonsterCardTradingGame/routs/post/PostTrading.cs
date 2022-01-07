using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.post
{
    public class PostTrading : IRouts
    {

        private TradeObject tradeObject;
        struct TradeObject
        {
            public String Id { get; set; }
            public String CardToTrade { get; set; }
            public String Type { get; set; }
            public double MinimumDamage { get; set; }
        }

        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                this.tradeObject = JsonConvert.DeserializeObject<TradeObject>(request.payload);
                if (String.IsNullOrEmpty(this.tradeObject.Id) || String.IsNullOrEmpty(this.tradeObject.CardToTrade) || String.IsNullOrEmpty(this.tradeObject.Type) ||this.tradeObject.MinimumDamage==0)
                    return ResponseHelper.jsonInvalid();
                //control cardToTrade gehort to this user
                if (!new CardsRepository().controlCardBelongsToUsername(user.username, this.tradeObject.CardToTrade))
                    return ResponseHelper.forbidden("This card does not belong to this user or Card is in deck");
                //control this card does not be in trading
                if (new TradesRepository().checkTradeByIdAndUsername(this.tradeObject.CardToTrade, user.username))
                    return ResponseHelper.forbidden("This Card is on Trade");
                // add card to trade
                if(!new TradesRepository().addTrade(user.username,this.tradeObject.Id, this.tradeObject.CardToTrade, this.tradeObject.Type, this.tradeObject.MinimumDamage))
                    return ResponseHelper.serverError();
                return ResponseHelper.ok();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.jsonInvalid();
            }
        }
    }
}
