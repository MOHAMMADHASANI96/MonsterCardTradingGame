using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.post
{
    public class PostCardTrade : IRouts
    {
        public Response processRequest(Request request)
        {

            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                String[] subString = Regex.Split(request.path, "/tradings/");
                if (subString[0] == null || subString[1] == null)
                    return ResponseHelper.forbidden();

                String id_trade = subString[1];
                String id_buyer = JsonConvert.DeserializeObject<String>(request.payload);

                Trade trade = new TradesRepository().getTradeById(id_trade);
                Card card = new CardsRepository().getCardByid(id_buyer);

                //check id in trade table exists
                if (!new TradesRepository().checkTradeById(id_trade))
                    return ResponseHelper.forbidden("This Id does not exist");
                //chack id of card in cards table exists
                if (card ==null)
                    return ResponseHelper.forbidden("This Card does not exist");
                //check id belongs to this user
                if (!new CardsRepository().controlCardBelongsToUsername(user.username,id_buyer))
                    return ResponseHelper.forbidden("This Card does not belong to this user");
                //buyer and seller doese not same
                if (trade.username.Equals(card.username))
                    return ResponseHelper.forbidden("Buyer and Seller are same!");
                //check type of card to match

                //check minimum damage to provide
                if(trade.min_damage > card.damage)
                    return ResponseHelper.forbidden("The damage of this card is less than the damage requested");
                //exchange cards and ok response



                return ResponseHelper.ok();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.forbidden();
            }
        }
    }
}
