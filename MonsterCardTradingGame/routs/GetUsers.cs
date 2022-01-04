using MonsterCardTradingGame.data.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public class GetUsers : IRouts
    {
        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();

                String[] subString = Regex.Split(request.path, "/users/");
                if(subString[0] ==null || subString[1] == null || !user.username.Equals(subString[1]))
                    return ResponseHelper.forbidden();

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
