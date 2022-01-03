using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public class Autorization
    {
        public User authorize(Request request)
        {
            if (!request.httpHeaders.ContainsKey("Authorization"))
                return null;
            String token = (String)request.httpHeaders["Authorization"];

            if (!token.StartsWith("Basic ") || token.Length < 7)
                return null;
            token = token.Substring(6);
            return new UsersRepository().getUserbyToken(token);
        }
    }
}
