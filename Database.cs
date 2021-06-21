using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace vanderBinckesBP
{
    class Database
    {
        string connectionString = @"server=localhost;userid=root;password=A=%)cV.W}WJhZm@AEMo6=3A8=;database=vanderbinckesdb";
        public MySqlConnection connection = new MySqlConnection();


        public bool isConnect()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                return true;
            }
            else {
                return false;
            }
        }
        public void Close()
        {
            connection.Close();
        }

        public List<Medewerker> ListMedewerkers()
        {
            List<Medewerker> a = new List<Medewerker>();
            if (isConnect())
            {
                string sqlQuery = "SELECT voornaam, achternaam FROM `vanderbinckesdb`.`medewerker`;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                //Console.WriteLine(cmd.ExecuteScalar().ToString());
                Console.WriteLine(cmd.ExecuteReader());
            }
            Close();
            return a;
        }

        //public void getVersion() {
        //    var stm = "SELECT VERSION()";
        //    var cmd = new MySqlCommand(stm, connection);
        //    var version = cmd.ExecuteScalar().ToString();
        //    Console.WriteLine($"MySQL version: {version}");
        //}

    }
}
