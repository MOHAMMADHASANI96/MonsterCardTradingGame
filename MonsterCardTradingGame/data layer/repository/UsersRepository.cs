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
            String query = String.Format("INSERT INTO users(username, name, password, token)" +
                "VALUES(@username,@name, @password, @token)");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("name", username);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("password", password);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("token", username + "-mtcgToken");
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("Select * from users where username = @username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("Select * from users where token = @token");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("token", token);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("Update users set name= @name , bio = @bio, image= @image where username= @username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("name", name);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("bio", bio);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("image", image);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("username", username);
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("Update users set coin= @coin where username= @username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("coin", coin);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                command.Parameters.AddWithValue("username", username);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
