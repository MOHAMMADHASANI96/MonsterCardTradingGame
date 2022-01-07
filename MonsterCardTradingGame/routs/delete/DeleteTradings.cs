using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.delete
{
    public class DeleteTradings : IRouts
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

                String id = subString[1];

                //check substring[1] = id in trade table exists
                if(!new TradesRepository().checkTradeById(id))
                    return ResponseHelper.forbidden("This Id does not exist");
                //check id belongs to this user
                if (!new TradesRepository().checkTradeByIdAndUsername(id,user.username))
                    return ResponseHelper.forbidden("This Card does not belong to this user");
                // Delete card from trade
                if (!new TradesRepository().deleteTradeById(id))
                    return ResponseHelper.serverError();
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
