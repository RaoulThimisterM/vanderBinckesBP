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
            else
            {
                return false;
            }
        }
        public void Close()
        {
            connection.Close();
        }

        public List<Medewerker> ListMedewerkers()
        {
            List<Medewerker> medewerker = new List<Medewerker>();
            if (isConnect())
            {
                string sqlQuery = "SELECT medewerkernummer, voornaam, achternaam FROM `vanderbinckesdb`.`medewerker`;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Medewerker b = new Medewerker(Convert.ToInt32(rdr.GetValue(0)), Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(2)));
                    medewerker.Add(b);
                }
            }
            Close();
            medewerker.ForEach(item => Console.WriteLine(item.voornaam + " " + item.achternaam + " Werknemersnummer: " + item.medewerkernummer));
            return medewerker;
        }

        public void CreateMedewerker(Medewerker a) {
            if (isConnect())
            {
                string sqlQuery = "INSERT INTO medewerker(voornaam, achternaam, datum_in_dienst) VALUES(@voornaam, @achternaam, @datum)";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@voornaam", a.voornaam);
                cmd.Parameters.AddWithValue("@achternaam", a.achternaam);
                cmd.Parameters.AddWithValue("@datum", a.datumInDienst);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Medewerker toegevoegd: " + a.voornaam + " " + a.achternaam + ". In dienst sinds: " + a.datumInDienst);
            }
        }

        //public void getVersion() {
        //    var stm = "SELECT VERSION()";
        //    var cmd = new MySqlCommand(stm, connection);
        //    var version = cmd.ExecuteScalar().ToString();
        //    Console.WriteLine($"MySQL version: {version}");
        //}

    }
}
