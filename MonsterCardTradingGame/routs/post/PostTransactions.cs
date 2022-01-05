using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data.repository;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs
{
    public class PostTransactions : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.forbidden("No valid authorization token");

                if (user.coin < 5)
                    return ResponseHelper.forbidden("Coin is not enough");

                int packageId = new CardsRepository().getFirstFreePackageId();
                Console.WriteLine(packageId);
                if (packageId <= 0)
                    return ResponseHelper.forbidden("No Free Package");

                if (new CardsRepository().updateUsernameAfterGetPackage(user.username, packageId))
                    if (new UsersRepository().updateCoin((user.coin - 5), user.username))
                        return ResponseHelper.ok();

            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.forbidden();
            }
            return ResponseHelper.serverError();
        }
    }
}
