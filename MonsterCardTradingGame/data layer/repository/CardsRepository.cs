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
            String query = String.Format("select * from cards where id =@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("INSERT INTO cards(id,name,damage,package_id,element_type,card_type,username) " +
                "VALUES(@id,@name,@damage,@package_id,@element_type,@card_type, @username)");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("name", name);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("damage", damage);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Double;
                command.Parameters.AddWithValue("package_id", package_id);
                command.Parameters[3].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
                command.Parameters.AddWithValue("element_type", element_type);
                command.Parameters[4].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("card_type", card_type);
                command.Parameters[5].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("username", "");
                command.Parameters[6].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("Update cards Set username =@username where Package_id = @package_id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("package_id", packageId);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
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
            String query = String.Format("select * from cards where username =@username");
            List<Card> cardList = new List<Card>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
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
            String query = String.Format("select * from cards where username =@username and is_deck =@is_deck");
            List<Card> cardList = new List<Card>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("is_deck", true);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
            String query = String.Format("select * from cards where username =@username and id=@id and is_deck=@is_deck");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
                command.Parameters[1].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("is_deck", false);
                command.Parameters[2].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Boolean;
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
        public bool updateDeckTrue(String id)
        {
            String query = String.Format("Update cards Set is_deck=@is_deck where id =@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("is_deck", true);
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
        public bool updateUsernameOfCardById(String username, String id)
        {
            String query = String.Format("Update cards Set username =@username where id =@id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("username", username);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("id", id);
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
        public bool updateDeckFalse(String id)
        {
            String query = String.Format("Update cards Set is_deck =@is_deck where id = @id");
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, this.NpgsqlConn);
                command.Parameters.AddWithValue("id", id);
                command.Parameters[0].NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
                command.Parameters.AddWithValue("is_deck", false);
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
