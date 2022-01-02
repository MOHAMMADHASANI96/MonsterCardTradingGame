using MonsterCardTradingGame.endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MonsterCardTradingGame.Request;

namespace MonsterCardTradingGame
{
    public class EndpointController
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
                    {
                        return new PostUsers().processRequest(this.request);
                    }
                    else
                    {
                        return ResponseHelper.notFound();
                    }

                case Methode.GET:
                    return ResponseHelper.notFound();
                case Methode.PUT:
                    return ResponseHelper.notFound();
                case Methode.DELETE:
                    return ResponseHelper.notFound();
                default:
                    return ResponseHelper.notFound();
            }
        }
    }
}
