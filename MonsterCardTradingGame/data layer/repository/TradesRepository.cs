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
    }
}
