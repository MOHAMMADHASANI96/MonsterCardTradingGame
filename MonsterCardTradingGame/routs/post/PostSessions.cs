using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data.repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public class PostSessions : IRouts
    {
        private UserObject userObject;
        struct UserObject
        {
            public String username { get; set; }
            public String password { get; set; }
        }
        public Response processRequest(Request request)
        {
            try
            {
                this.userObject = JsonConvert.DeserializeObject<UserObject>(request.payload);
                if (String.IsNullOrEmpty(this.userObject.username) || String.IsNullOrEmpty(this.userObject.password))
                    return ResponseHelper.jsonInvalid();
                User user = new UsersRepository().getUser(this.userObject.username);
                if (user == null)
                    return ResponseHelper.notFound("Username does not exist");
                if(!user.password.Equals(this.userObject.password))
                    return ResponseHelper.notFound("password does not correct");
                return ResponseHelper.ok("login successful");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.jsonInvalid();
            }
        }
    }
}
