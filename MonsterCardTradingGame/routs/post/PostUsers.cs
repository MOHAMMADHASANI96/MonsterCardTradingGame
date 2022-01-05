using MonsterCardTradingGame.data.repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public class PostUsers : IRouts
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
                    return ResponseHelper.jsonInvalid("Username or Password is empty");
                if (!new UsersRepository().addUser(this.userObject.username, this.userObject.password))
                    return ResponseHelper.jsonInvalid("Username exists");
                return ResponseHelper.ok();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return ResponseHelper.jsonInvalid();
            }
        }
    }
}
