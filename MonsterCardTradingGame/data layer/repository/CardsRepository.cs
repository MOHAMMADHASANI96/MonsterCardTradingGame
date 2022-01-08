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
    public class CardsRepository
    {
        private NpgsqlConnection NpgsqlConn;
        public CardsRepository()
        {
            this.NpgsqlConn = new NpgsqlConn().getnpgsqlConn();
        }
        public Card getCardByid(String id)
        {
            String query = String.Format("select * from cards where id ='{0}'", id);
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                    return new Card(
                    npgsqlDataReader["id"].ToString(),
                    npgsqlDataReader["name"].ToString(),
                    Convert.ToDouble(npgsqlDataReader["damage"]),
                    Convert.ToInt32(npgsqlDataReader["package_id"]),
                    npgsqlDataReader["username"].ToString(),
                     Convert.ToBoolean(npgsqlDataReader["is_deck"]),
                     npgsqlDataReader["element_type"].ToString(),
                     npgsqlDataReader["card_type"].ToString()
                    );
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }
            return null;
        }
        public int getLastPackageId()
        {
            String query = String.Format("Select package_id from cards group by package_id order by package_id desc limit 1");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return Convert.ToInt32(dataReader["package_id"]);
                return 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return -1;
            }
        }
        public bool addCard(String id,String name , double damage, int package_id,String element_type, String card_type)
        {
            String query = String.Format("INSERT INTO cards(id,name,damage,package_id,username,element_type,card_type) " +
                "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", id, name, damage, package_id,null,element_type,card_type);
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
        public int getFirstFreePackageId()
        {
            String query = String.Format("Select package_id from cards where username = '' group by package_id order by package_id limit 1");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                    return Convert.ToInt32(dataReader["package_id"]);
                return 0;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return -1;
            }
        }
        public bool updateUsernameAfterGetPackage(String username,int packageId)
        {
            String query = String.Format("Update cards Set username ='{0}' where Package_id = '{1}'", username, packageId);
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
        public List<Card> getListOfCardsByUsername(String username)
        {
            String query = String.Format("select * from cards where username ='{0}'", username);
            List<Card> cardList = new List<Card>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                        cardList.Add(new Card(
                        npgsqlDataReader["id"].ToString(),
                        npgsqlDataReader["name"].ToString(),
                        Convert.ToDouble(npgsqlDataReader["damage"]),
                        Convert.ToInt32(npgsqlDataReader["package_id"]),
                        npgsqlDataReader["username"].ToString(),
                        Convert.ToBoolean(npgsqlDataReader["is_deck"]),
                        npgsqlDataReader["element_type"].ToString(),
                        npgsqlDataReader["card_type"].ToString()
                        ));
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return null;
            }
            return cardList;
        }
        public List<Card> getDeckByUsername(String username)
        {
            String query = String.Format("select * from cards where username ='{0}' and is_deck =true", username);
            List<Card> cardList = new List<Card>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                NpgsqlDataReader npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                    cardList.Add(new Card(
                    npgsqlDataReader["id"].ToString(),
                    npgsqlDataReader["name"].ToString(),
                    Convert.ToDouble(npgsqlDataReader["damage"]),
                    Convert.ToInt32(npgsqlDataReader["package_id"]),
                    npgsqlDataReader["username"].ToString(),
                    Convert.ToBoolean(npgsqlDataReader["is_deck"]),
                    npgsqlDataReader["element_type"].ToString(),
                    npgsqlDataReader["card_type"].ToString()
                    ));
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
                return null;
            }
            return cardList;
        }
        public bool controlCardBelongsToUsername(String username, String id)
        {
            String query = String.Format("select * from cards where username ='{0}' and id='{1}' and is_deck=false", username,id);
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
        public bool updateDeck(String id)
        {
            String query = String.Format("Update cards Set is_deck =true where id = '{0}'",id);
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
        public bool updateUsernameOfCardById(String username, String id)
        {
            String query = String.Format("Update cards Set username ='{0}' where id = '{1}'", username, id);
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
