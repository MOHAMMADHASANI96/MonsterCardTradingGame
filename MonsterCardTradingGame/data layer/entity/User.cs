using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.data.entity
{
    public class User
    {
        public String username { get; private set; }
        public String name { get; private set; }
        public String password { get; private set; }
        public String token { get; private set; }
        public String bio { get; private set; }
        public String image { get; private set; }
        public int coin { get; private set; }

        public User(String username,String name,String password, String token, String bio, String image, int coin)
        {
            this.username = username;
            this.name = name;
            this.password = password;
            this.token = token;
            this.bio = bio;
            this.image = image;
            this.coin = coin;
        }
    }
}
