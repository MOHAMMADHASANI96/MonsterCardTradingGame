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
    public class PostPackages : IRouts
    {
        //private CardObject cardObject;
        struct CardObject
        {
            public String Id { get; set; }
            public String Name { get; set; }
            public double Damage { get; set; }
        }
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();
                if(user.name !="admin")
                    return ResponseHelper.forbidden();

                try
                {
                    List<CardObject> cardsObjectList = JsonConvert.DeserializeObject<List<CardObject>>(request.payload);
                    int package_id = new CardsRepository().getLastPackageId() + 1;
                    foreach (CardObject cardObject in cardsObjectList)
                    {
                        if (String.IsNullOrEmpty(cardObject.Id) || String.IsNullOrEmpty(cardObject.Name) || cardObject.Damage==0)
                            return ResponseHelper.jsonInvalid();
                        if (!new CardsRepository().addCard(cardObject.Id,cardObject.Name, cardObject.Damage,package_id))
                            return ResponseHelper.forbidden();
                    }
                    return ResponseHelper.ok();
                }
                catch (Exception exception)
                {

                    Console.WriteLine("Error:" + exception.Message);
                    return ResponseHelper.forbidden();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.forbidden();
            }
        }
    }
}
