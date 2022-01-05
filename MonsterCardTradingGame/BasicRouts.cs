using MonsterCardTradingGame.endpoints;
using MonsterCardTradingGame.routs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MonsterCardTradingGame.Request;

namespace MonsterCardTradingGame
{
    public class BasicRouts
    {
        private String path;
        private Request request;
        private Methode methode;
        public Response getResponse(Request request)
        {
            this.request = request;
            this.path = request.path;
            this.methode = request.methode;

            switch (methode)
            {
                case Methode.POST:
                    if (this.path.Equals("/users"))
                        return new PostUsers().processRequest(this.request);
                    else if (this.path.Equals("/sessions"))
                        return new PostSessions().processRequest(this.request);
                    else if (this.path.Equals("/packages"))
                        return new PostPackages().processRequest(this.request);
                    else if (this.path.Equals("/transactions/packages"))
                        return new PostTransactions().processRequest(this.request);
                    else
                        return ResponseHelper.notFound();

                case Methode.GET:
                    if (Regex.IsMatch(this.path, "/users/([0-9a-zA-Z.-]+)"))
                        return new GetUsers().processRequest(this.request);
                    else if(Regex.IsMatch(this.path, "/cards"))
                        return new GetCards().processRequest(this.request);
                    else if (Regex.IsMatch(this.path, "/deck"))
                        return new GetDecks().processRequest(this.request);
                    else
                        return ResponseHelper.notFound();

                case Methode.PUT:
                    if (Regex.IsMatch(this.path, "/users/([0-9a-zA-Z.-]+)"))
                        return new PutUsers().processRequest(this.request);
                    else if (Regex.IsMatch(this.path, "/deck"))
                        return new PutDecks().processRequest(this.request);
                    else
                        return ResponseHelper.notFound();


                case Methode.DELETE:
                    return ResponseHelper.notFound();
                
                default:
                    return ResponseHelper.notFound();
            }
        }
    }
}
