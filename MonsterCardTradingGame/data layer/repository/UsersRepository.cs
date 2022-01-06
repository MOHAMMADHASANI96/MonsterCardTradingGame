using MonsterCardTradingGame.data.entity;
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
        // Registration
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
        // Login
        public User getUser(String username)
        {
            String query = String.Format("Select * from users where username = '{0}'", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    return new User(
                        npgsqlDataReader["username"].ToString(),
                        npgsqlDataReader["name"].ToString(),
                        npgsqlDataReader["password"].ToString(),
                        npgsqlDataReader["token"].ToString(),
                        npgsqlDataReader["bio"].ToString(),
                        npgsqlDataReader["image"].ToString(),
                        Convert.ToInt32(npgsqlDataReader["coin"])
                        );
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                
            }
            return null;
        }

        public User getUserbyToken(String token)
        {
            String query = String.Format("Select * from users where token = '{0}'", token);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    return new User(
                        npgsqlDataReader["username"].ToString(),
                        npgsqlDataReader["name"].ToString(),
                        npgsqlDataReader["password"].ToString(),
                        npgsqlDataReader["token"].ToString(),
                        npgsqlDataReader["bio"].ToString(),
                        npgsqlDataReader["image"].ToString(),
                        Convert.ToInt32(npgsqlDataReader["coin"])
                        );
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);

            }
            return null;
        }

        public bool updateUser(String name,String bio,String image,String username)
        {
            String query = String.Format("Update users set name= '{0}' , bio = '{1}', image= '{2}' where username= '{3}'" ,name, bio,image,username);
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

        public bool updateCoin(int coin, String username)
        {
            String query = String.Format("Update users set coin= '{0}' where username= '{1}'", coin,username);
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
