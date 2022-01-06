using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data_layer.entity;
using MonsterCardTradingGame.data_layer.repository;
using MonsterCardTradingGame.endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.routs.get
{
    public class GetDeckPlainFormat : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                String[] substring = Regex.Split(request.path, "/deck[?]format=");
                if (!substring[1].ToUpper().Equals("PLAIN"))
                    return ResponseHelper.forbidden("There is no correct format");

                List<Card> cardList = new CardsRepository().getDeckByUsername(user.username);

                if (cardList == null || cardList.Count == 0)
                    return ResponseHelper.ok("The deck is empty");

                String plain = null;
                foreach (Card card in cardList)
                {
                    plain += ($"{card.id},{card.name},{card.damage},{card.is_deck}\r\n");
                }
                return ResponseHelper.okPlainPayload(plain);

            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.forbidden();
            }
        }
    }
}
