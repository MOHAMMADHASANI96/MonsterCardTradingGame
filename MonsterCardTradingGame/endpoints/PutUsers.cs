using MonsterCardTradingGame.data.entity;
using MonsterCardTradingGame.data.repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.endpoints
{
    public class PutUsers : IEndpoints
    {
        private UserObject userObject;
        struct UserObject
        {
            public String Name { get; set; }
            public String Bio { get; set; }
            public String Image { get; set; }
        }

        public Response processRequest(Request request)
        {
            try
            {
                User user = new Autorization().authorize(request);
                if (user == null)
                    return ResponseHelper.notFound();of

                String[] subString = Regex.Split(request.path, "/users/");
                if (subString[0] == null || subString[1] == null || !user.username.Equals(subString[1]))
                    return ResponseHelper.forbidden();

                try
                {
                    this.userObject = JsonConvert.DeserializeObject<UserObject>(request.payload);
                    if (String.IsNullOrEmpty(this.userObject.Name) || String.IsNullOrEmpty(this.userObject.Bio) || String.IsNullOrEmpty(this.userObject.Image))
                        return ResponseHelper.jsonInvalid();
                    if (!new UsersRepository().updateUser(this.userObject.Name, this.userObject.Bio, this.userObject.Image, user.username))
                        return ResponseHelper.forbidden();
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
