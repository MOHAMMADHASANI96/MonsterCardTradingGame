using MTCG.repository;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG.data
{
    public class NpgsqlConn
    {
        public NpgsqlConnection npgsqlConn;
        public NpgsqlConnection getnpgsqlConn()
        {
            return npgsqlConn;
        }

        public NpgsqlConn()
        {
            npgsqlConn = Database.NpgsqlConn;
            if(npgsqlConn == null)
                npgsqlConn.Open();
            //if (npgsqlConn.State.ToString().Equals("Open"))
            else
            {
                npgsqlConn.Close();
                npgsqlConn.Open();
            }
        }
    }
}
