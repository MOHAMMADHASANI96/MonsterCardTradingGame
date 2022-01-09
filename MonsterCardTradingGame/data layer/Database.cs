using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG.data;
using Npgsql;

namespace MTCG.repository
{
    //Verbindung aufbauen
    //singleton klasse (kontrollieren die Verbindung nur einmal existiert) ??? 
    public class Database
    {
        private static String USERID = "postgres";
        private static String PASSWORD = "1234";
        private static int PORT = 5432;
        private static String HOSTNAME = "localhost";
        private static String DATABASE_NAME = "fh_mtcg";

        public static NpgsqlConnection NpgsqlConn { get; set; }

        private Database()
        {
            connect();
        }
        private static Database _instance;
        // static -> wir gereifen ohne objekt 
        public static Database GetInstance()
        {
            if(_instance == null)
                _instance = new Database();
            return _instance;
        }
        private void connect()
        {
           
            string strConn = string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", HOSTNAME, PORT, USERID, PASSWORD, DATABASE_NAME);
            try
            {
                NpgsqlConn = new NpgsqlConnection(strConn);
                Console.WriteLine("Connected to Database");
                
            }
            catch (Exception exc)
            {
                Console.WriteLine("error occurred: " + exc.Message); 
            }
            
        }
        //The TRUNCATE TABLE command deletes the data inside a table, but not the table itself.
        public void truncateTables()
        {
            String query = "truncate table users cascade;truncate table cards cascade;";
            try
            {
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, new NpgsqlConn().getnpgsqlConn());
                NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }
        }
    }
}
