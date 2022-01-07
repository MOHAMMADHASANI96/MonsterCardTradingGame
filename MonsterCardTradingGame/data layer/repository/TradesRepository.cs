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
            String query = String.Format("select * from trades where id = '{0}'");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
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
            String query = String.Format("select * from trades where id ='{0}' and username='{1}'", id, username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
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
               "VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", username, id, card_id, type, min_damage,false);
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
        public bool checkTradeById(String id)
        {
            String query = String.Format("select * from trades where id ='{0}'", id);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
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
            String query = String.Format("delete from trades where id ='{0}'", id);
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
