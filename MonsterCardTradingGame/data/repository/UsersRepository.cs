using MTCG.data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.data.repository
{
    public class UsersRepository
    {
        private NpgsqlConnection NpgsqlConn;
        public UsersRepository()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
        }
        public bool addUser(String username, String password)
        {
            String query = String.Format("INSERT INTO users(username, name, password, token, bio, image) " +
                "VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",username,username,password,username+"-mtcgToken",null,null);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                int dataReader = command.ExecuteNonQuery();
                if (dataReader == 0)
                    return false;
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return false;
            }
        }
    }
}
