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
            Console.Clear();
            List<Medewerker> medewerker = new List<Medewerker>();
            if (isConnect())
            {
                string sqlQuery = "SELECT medewerkernummer, voornaam, achternaam, datum_in_dienst FROM `vanderbinckesdb`.`medewerker`;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Medewerker b = new Medewerker(Convert.ToInt32(rdr.GetValue(0)), Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(2)), Convert.ToDateTime(rdr.GetValue(3)));
                    medewerker.Add(b);
                }
            }
            Close();
            medewerker.ForEach(item => Console.WriteLine(item.voornaam + " " + item.achternaam + " Werknemersnummer: " + item.medewerkernummer));
            return medewerker;
        }

        public Medewerker GetMedewerker(int medewerkernummer) {
            Medewerker medewerker = new Medewerker(medewerkernummer);
            if (isConnect())
            {
                string sqlQuery = $"SELECT medewerkernummer, achternaam, voornaam, datum_in_dienstFROM medewerker WHERE medewerkernummer = {medewerkernummer};";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    medewerker = new Medewerker(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetDateTime(3));
                }
            }
            Close();
            return medewerker;
        }

        public void ListAccessoires()
        {
            List<Accessoire> accessoires = new List<Accessoire>();
            if (isConnect())
            {
                string sqlQuery = "SELECT accessoirenummer, naam, huurprijs FROM `vanderbinckesdb`.`accessoire`;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Accessoire b = new Accessoire(Convert.ToInt32(rdr.GetValue(0)), Convert.ToString(rdr.GetValue(1)), Convert.ToDecimal(rdr.GetValue(2)));
                    accessoires.Add(b);
                }
                accessoires.ForEach(item => Console.WriteLine("Nummer: " + item.accessoirenummer + " Naam: " + item.naam + " Prijs: €" + item.huurprijs));
            }
            Close();
        }

        public void CreateMedewerker(Medewerker a)
        {
            if (isConnect())
            {
                string sqlQuery = "INSERT INTO medewerker(voornaam, achternaam, datum_in_dienst) VALUES(@voornaam, @achternaam, @datum)";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@voornaam", a.voornaam);
                cmd.Parameters.AddWithValue("@achternaam", a.achternaam);
                cmd.Parameters.AddWithValue("@datum", a.datumInDienst);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Medewerker toegevoegd: " + a.voornaam + " " + a.achternaam + ". In dienst sinds: " + a.datumInDienst + ". Druk op Enter om door te gaan.");
                Console.ReadLine();
            }
            Close();
        }

        public void EditMedewerker(Medewerker a)
        {
            if (isConnect())
            {
                string sqlQuery = "UPDATE medewerker SET voornaam = @voornaam, achternaam = @achternaam WHERE medewerkernummer = @medewerkernummer";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@voornaam", a.voornaam);
                cmd.Parameters.AddWithValue("@achternaam", a.achternaam);
                cmd.Parameters.AddWithValue("@medewerkernummer", a.medewerkernummer);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Medewerker aangepast: " + a.voornaam + " " + a.achternaam + ". In dienst sinds: " + a.datumInDienst + ". Druk op Enter om door te gaan.");
                Console.ReadLine();
            }
            Close();
        }

        internal void CreateVerhuur(Verhuur a)
        {
            if (isConnect())
            {
                string sqlQuery = "INSERT INTO verhuur(verhuurdatum, bakfietsnummer, aantal_dagen, huurprijstotaal, klantnummer, verhuurder) VALUES(@verhuurdatum, @bakfietsnummer, @aantal_dagen, @huurprijstotaal, @klantnummer, @verhuurder)";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@verhuurdatum", a.verhuurdatum);
                cmd.Parameters.AddWithValue("@bakfietsnummer", a.bakfietsnummer);
                cmd.Parameters.AddWithValue("@aantal_dagen", a.verhuurdagen);
                cmd.Parameters.AddWithValue("@huurprijstotaal", a.huurprijs);
                cmd.Parameters.AddWithValue("@klantnummer", a.klantnummer);
                cmd.Parameters.AddWithValue("@verhuurder", a.medewerker);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Verhuur aangemaakt op klant: " + a.klantnummer);
                Console.ReadLine();
            }
            Close();
        }

        public List<Verhuur> ListBestellingMedewerker(Medewerker a)
        {
            List<Verhuur> verhuur = new List<Verhuur>();
            if (isConnect())
            {
                string sqlQuery = $"SELECT verhuurnummer, verhuurdatum, bakfietsnummer, aantal_dagen, huurprijstotaal, klantnummer, verhuurder FROM verhuur WHERE verhuurder = {a.medewerkernummer};";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Verhuur b = new Verhuur(rdr.GetInt32(0), rdr.GetDateTime(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetDecimal(4), rdr.GetInt32(5), rdr.GetInt32(6));
                    verhuur.Add(b);
                }
            }
            Close();
            verhuur.ForEach(item => Console.WriteLine("#" + item.verhuurnummer + ". " + item.verhuurdatum + " Bakfiets: " + item.bakfietsnummer + " Totaalprijs: €" + item.huurprijs + " Klantnummer: " + item.klantnummer + " Medewerker: " + item.medewerker));
            return verhuur;
        }

        internal Bakfiets GetBakfiets(int bakfietsnummer)
        {
            Bakfiets bakfiets = new Bakfiets(bakfietsnummer);
            if (isConnect())
            {
                string sqlQuery = $"SELECT bakfietsnummer, naam, type, huurprijs FROM bakfiets WHERE bakfietsnummer = {bakfietsnummer};";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    bakfiets = new Bakfiets(bakfietsnummer, Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(2)), Convert.ToDecimal(rdr.GetValue(3)));
                }
            }
            Close();
            return bakfiets;
        }

        public void DeleteMedewerker(Medewerker a)
        {
            if (isConnect())
            {
                string sqlQuery = "DELETE FROM medewerker WHERE medewerkernummer = @medewerkernummer";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                cmd.Parameters.AddWithValue("@medewerkernummer", a.medewerkernummer);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Medewerker verwijderd. Druk op Enter om door te gaan.");
                Console.ReadLine();
            }
        }

        public List<Klant> ListKlanten()
        {
            Console.Clear();
            List<Klant> klant = new List<Klant>();
            if (isConnect())
            {
                string sqlQuery = "SELECT klantnummer, voornaam, naam FROM `vanderbinckesdb`.`klant`;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Klant b = new Klant(Convert.ToInt32(rdr.GetValue(0)), Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(2)));
                    klant.Add(b);
                }
            }
            Close();
            klant.ForEach(item => Console.WriteLine(item.voornaam + " " + item.achternaam + " Klantnummer: " + item.klantnummer));
            return klant;
        }

        public void ListBakfietsen()
        {
            List<Bakfiets> bakfietsen = new List<Bakfiets>();
            if (isConnect())
            {
                string sqlQuery = "SELECT bakfietsnummer, naam, type, huurprijs FROM `vanderbinckesdb`.`bakfiets`;";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Bakfiets b = new Bakfiets(Convert.ToInt32(rdr.GetValue(0)), Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(2)), Convert.ToDecimal(rdr.GetValue(3)));
                    bakfietsen.Add(b);
                }
                Close();
            }
            bakfietsen.ForEach(item => Console.WriteLine("Nummer: " + item.bakfietsnummer + " Naam: " + item.naam + " Type: " + item.type + " Prijs: €" + item.huurprijs));
        }

        public Accessoire GetAccessoire(int accessoirenummer)
        {
            Accessoire accessoire = new Accessoire(accessoirenummer);
            if (isConnect())
            {
                string sqlQuery = $"SELECT accessoirenummer, naam, huurprijs FROM accessoire WHERE accessoirenummer = {accessoirenummer};";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    accessoire = new Accessoire(accessoirenummer, Convert.ToString(rdr.GetValue(1)), Convert.ToDecimal(rdr.GetValue(2)));
                }
            }
            Close();
            return accessoire;
        }

        //public void getVersion() {
        //    var stm = "SELECT VERSION()";
        //    var cmd = new MySqlCommand(stm, connection);
        //    var version = cmd.ExecuteScalar().ToString();
        //    Console.WriteLine($"MySQL version: {version}");
        //}

    }
}
