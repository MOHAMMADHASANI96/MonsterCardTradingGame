using MonsterCardTradingGame.data_layer.entity;
using MTCG.data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame.data_layer.repository
{
    public class TradesRepository
    {
        private NpgsqlConnection NpgsqlConn;
        public TradesRepository()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
        }
        public Trade getTradeById(String id)
        {
            String query = String.Format("select * from trades where id = @id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                    return new Trade(
                    npgsqlDataReader["username"].ToString(),
                    npgsqlDataReader["id"].ToString(),
                    npgsqlDataReader["card_id"].ToString(),
                    npgsqlDataReader["type"].ToString(),
                    Convert.ToDouble(npgsqlDataReader["min_damage"]),
                     Convert.ToBoolean(npgsqlDataReader["is_sold"])
                    );
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);   
            }
            return null;
        }
        public List<Trade> getTrade()
        {
            String query = String.Format("select * from trades");
            List<Trade> tradeList = new List<Trade>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                    tradeList.Add(new Trade(
                    npgsqlDataReader["username"].ToString(),
                    npgsqlDataReader["id"].ToString(),
                    npgsqlDataReader["card_id"].ToString(),
                    npgsqlDataReader["type"].ToString(),
                    Convert.ToDouble(npgsqlDataReader["min_damage"]),
                     Convert.ToBoolean(npgsqlDataReader["is_sold"])
                    ));
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return null;
            }
            return tradeList;
        }
        public bool checkTradeByIdAndUsername(String id, String username)
        {
            String query = String.Format("select * from trades where id =@id and username=@username");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("username", username);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                NpgsqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read() == false)
                    return false;
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return false;
            }
        }
        public bool addTrade(String username, String id,String card_id, String type, double min_damage)
        {
            String query = String.Format("INSERT INTO trades(username, id, card_id, type, min_damage, is_sold) " +
               "VALUES(@username,@id,@card_id,@type,@min_damage,@is_sold)");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("card_id", card_id);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("type", type);
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("min_damage", min_damage);
                command.Parameters[4].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Double;
                command.Parameters.AddWithValue("is_sold", false);
                command.Parameters[5].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
        public bool checkTradeById(String id)
        {
            String query = String.Format("select * from trades where id =@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                NpgsqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read() == false)
                    return false;
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return false;
            }
        }
        public bool deleteTradeById(String id)
        {
            String query = String.Format("delete from trades where id =@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
        public bool updateIsSoldById(String id)
        {

            String query = String.Format("Update trades Set is_sold =@is_sold where id = @id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("is_sold", true);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
