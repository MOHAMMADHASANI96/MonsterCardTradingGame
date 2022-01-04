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
        public bool addCard(String id,String name , double damage, int package_id)
        {
            String query = String.Format("INSERT INTO cards(id,name,damage,package_id,username) " +
                "VALUES('{0}','{1}','{2}','{3}','{4}')", id, name, damage, package_id,null);
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

    }
}
