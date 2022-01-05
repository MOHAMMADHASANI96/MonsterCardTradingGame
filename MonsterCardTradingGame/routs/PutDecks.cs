using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs
{
    public class PutDecks : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                List<String> listId = JsonConvert.DeserializeObject<List<String>>(request.payload);

                if (listId.Count == 0)
                    return ResponseHelper.jsonInvalid();

                if(listId.Count !=4)
                    return ResponseHelper.forbidden("Number of cards must be 4");

                // card not empty and belongs to this username and deck is false
                foreach (String id in listId)
                {
                    if (String.IsNullOrEmpty(id))
                        return ResponseHelper.forbidden("id is empty");
                    if (!new CardsRepository().controlCardBelongsToUsername(user.username, id))
                        return ResponseHelper.forbidden("This card is in deck or not belongs to this username");
                }

                foreach (String id in listId)
                {
                    if (!new CardsRepository().updateDeck(id))
                        return ResponseHelper.serverError();
                }
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
