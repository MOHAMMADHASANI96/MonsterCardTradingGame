using MonsterCardTradingGame.battle;
using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.post
{
    public class PostBattles : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();
                
                // check user have 4 cards
                if(new CardsRepository().getDeckByUsername(user.username).Count !=4)
                    return ResponseHelper.forbidden("This user deos not have 4 cards");

                //check user one Time requested
                if(!BattleController.getInstance().addplayer(user.username))
                    return ResponseHelper.forbidden("This user already in Battle");

                while (BattleController.getInstance().isProcess())
                {
                    try
                    {
                        Thread.Sleep(300);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine("Error:" + exception.Message);
                    }
                }
                return ResponseHelper.okJsonPayload(BattleController.getInstance().getResult());
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.forbidden();
            }
        }
    }
}
