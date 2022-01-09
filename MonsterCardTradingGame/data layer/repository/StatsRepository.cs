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
    public class StatsRepository
    {
        private NpgsqlConnection NpgsqlConn;
        public StatsRepository()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
        }

        public bool addStat(String username)
        {
            String query = String.Format("INSERT INTO stats(username) VALUES('{0}')", username);
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
        public Stat getStat(String username)
        {
            String query = String.Format("Select * from stats where username='{0}'", username);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    return new Stat(
                        npgsqlDataReader["username"].ToString(),
                        Convert.ToInt32(npgsqlDataReader["elo"]),
                        Convert.ToInt32(npgsqlDataReader["win"]),
                        Convert.ToInt32(npgsqlDataReader["lose"]),
                        Convert.ToInt32(npgsqlDataReader["draw"]),
                        Convert.ToInt32(npgsqlDataReader["num_play"])
                        );
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }
            return null;
        }
        public List<Stat> getListStat()
        {
            String query = String.Format("SELECT username, elo, win, lose, draw,num_play FROM stats where username !='admin' order by elo DESC");
            List<Stat> listStat = new List<Stat>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    listStat.Add(new Stat(
                        npgsqlDataReader["username"].ToString(),
                        Convert.ToInt32(npgsqlDataReader["elo"]),
                        Convert.ToInt32(npgsqlDataReader["win"]),
                        Convert.ToInt32(npgsqlDataReader["lose"]),
                        Convert.ToInt32(npgsqlDataReader["draw"]),
                        Convert.ToInt32(npgsqlDataReader["num_play"])
                        ));
                }
                return listStat;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return null;
            }
        }
        public bool updateStatWinnerByUsername(String username)
        {
            String query = String.Format("Update stats Set elo =elo+3,win=win+1 where username = '{0}'", username);
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
        public bool updateStatLoserByUsername(String username)
        {
            String query = String.Format("Update stats Set elo =elo-5,lose = lose +1 where username = '{0}'", username);
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
        public bool updateStatdrawByUsername(String username)
        {
            String query = String.Format("Update stats Set draw = draw +1 where username = '{0}'", username);
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
