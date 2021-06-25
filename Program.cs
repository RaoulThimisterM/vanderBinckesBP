using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace vanderBinckesBP
{
    class Program
    {
        public static Database dbObject = new Database();
        static void Main(string[] args)
        {
            bool login = true;
            List<Medewerker> medewerkers = dbObject.ListMedewerkers();
            while (login)
            {
                Console.WriteLine("Login naar Verhuursysteem.");
                Console.WriteLine("Vul je werknemersnummer in:");
                int loginNummer = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < medewerkers.Count; i++)
                {
                    if (medewerkers[i].medewerkernummer == loginNummer)
                    {
                        Console.WriteLine("Nummer gevonden.");
                        break;
                    }
                    else if (medewerkers[i].medewerkernummer != loginNummer && i == medewerkers.Count -1)
                    {
                        Console.WriteLine("Nummer niet gevonden, probeer het nogmaals.");
                    }
                }
                Console.WriteLine("Vul je wachtwoord in:");
                string datumInDienst = Console.ReadLine();
                if (medewerkers[loginNummer - 1].datumInDienst == Convert.ToDateTime(datumInDienst))
                {
                    Console.WriteLine("Wachtwoord klopt, je wordt ingelogd.");
                    login = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Inlogpoging mislukt. Log opnieuw in.");
                }
            }
            
            Console.WriteLine("Welkom bij het Bakfiets Verhuur Programma van VanderBinckes.");
            while (true)
            {
                RunMainMenu();
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze < 0 || keuze > 7)
                {
                    Console.WriteLine("Ongeldige keuze, probeer het nog een keer.");
                }
                else
                {
                    switch (keuze)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            MaakVerhuur();
                            break;
                        case 2:
                            MaakMedewerker();
                            break;
                        case 3:
                            ListAlleMedewerkers();
                            break;
                        case 4:
                            BewerkMedewerker();
                            break;
                        case 5:
                            VerwijderMedewerker();
                            break;
                        case 6:
                            dbObject.ListKlanten();
                            break;
                        case 7:
                            BestellingPerMedewerker();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void ListAlleMedewerkers()
        {
            List<Medewerker> medewerkers = dbObject.ListMedewerkers();
            medewerkers.ForEach(item => Console.WriteLine(item.voornaam + " " + item.achternaam + " Werknemersnummer: " + item.medewerkernummer));
        }

        private static void BestellingPerMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Vul medewerkernummer in:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Medewerker medewerker = new Medewerker(medewerkernummer);
            dbObject.ListBestellingMedewerker(medewerker);
            Console.WriteLine("Druk op een toets om terug te keren.");
            Console.ReadLine();
        }

        private static void MaakVerhuur()
        {
            Console.Clear();
            Console.WriteLine("Geef je medewerkernummer op:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Geeft het klantnummer op voor verhuur:");
            int klantnummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Datum van verhuur:(Notatie YYYY-MM-DD)");
            DateTime dateTime = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Aantal dagen:");
            int aantalDagen = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Welke bakfiets wordt gekozen?");
            dbObject.ListBakfietsen();
            int bakfietsnummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Welke  accessoires worden toegevoegd? Vul er 1 per keer in. Druk op 0 om te stoppen met toevoegen.");
            dbObject.ListAccessoires();
            List<Accessoire> selAccessoires = new List<Accessoire>();
            while (true)
            {

                int accessoirenummer = Convert.ToInt32(Console.ReadLine());
                if (accessoirenummer == 0)
                {
                    break;
                }
                else
                {
                    selAccessoires.Add(dbObject.GetAccessoire(accessoirenummer));
                    Console.WriteLine("Accessoire toegevoegd!");
                }
            }
            if (selAccessoires.Count == 0)
            {
                Console.WriteLine("Geen accesoires gekozen.");
            }
            else
            {
                Console.WriteLine("De gekozen accessoires:");
                selAccessoires.ForEach(item => Console.WriteLine("Naam: " + item.naam + " Prijs: €" + item.huurprijs));
                //TODO: Accessoires in join tabel toevoegen
            }
            decimal huurprijstotaal = BerekenHuurprijs(bakfietsnummer, aantalDagen, selAccessoires);
            Console.WriteLine("Daarmee is de totale huurprijs: €" + huurprijstotaal);
            Verhuur verhuur = new Verhuur(dateTime, bakfietsnummer, aantalDagen, huurprijstotaal, klantnummer, medewerkernummer);
            dbObject.CreateVerhuur(verhuur);
            Console.ReadLine();
        }

        private static decimal BerekenHuurprijs(int bakfietsnummer, int aantalDagen, List<Accessoire> accesoires)
        {
            decimal huurprijs = 0;
            Bakfiets bakfiets = dbObject.GetBakfiets(bakfietsnummer);
            huurprijs += (bakfiets.huurprijs * aantalDagen);
            accesoires.ForEach(item => huurprijs += item.huurprijs);
            return huurprijs;
        }

        private static void VerwijderMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Medewerker verwijderen.");
            Console.WriteLine("Voer het medewerkernummer in:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Medewerker medewerker = new Medewerker(medewerkernummer);
            dbObject.DeleteMedewerker(medewerker);
        }

        private static void BewerkMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Medewerker bewerken.");
            Console.WriteLine("Voer het medewerkernummer in:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Wat is de nieuwe voornaam?");
            string voornaam = Console.ReadLine();
            Console.WriteLine("Wat is de nieuwe achternaam?");
            string achternaam = Console.ReadLine();
            Medewerker medewerker = new Medewerker(medewerkernummer, voornaam, achternaam, DateTime.Today);
            dbObject.EditMedewerker(medewerker);
        }

        private static void MaakMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Nieuwe medewerker aanmaken.\nWat is de voornaam van de medeweker?");
            string voornaam = Console.ReadLine();
            Console.WriteLine("Wat is de achternaam van de medewerker?");
            string achternaam = Console.ReadLine();
            Console.WriteLine("Wanneer is de medewerker in dienst getreden?(laat leeg voor vandaag. Notatie is YYYY-MM-DD)");
            string datumInDienst = Console.ReadLine();
            DateTime inDienst;
            if (datumInDienst == "")
            {
                inDienst = DateTime.Today;
            }
            else
            {
                inDienst = Convert.ToDateTime(datumInDienst);
            }
            Medewerker medewerker = new Medewerker(voornaam, achternaam, inDienst);
            dbObject.CreateMedewerker(medewerker);
        }

        private static void RunMainMenu()
        {
            Console.WriteLine("\nKies uit het onderstaande menu de gewenste optie.");
            Console.WriteLine("0. Stop programma\n1. Maak nieuwe verhuur" +
                "\n2. Maak nieuwe medewerker aan\n3. Lijst van alle medewerkers" +
                "\n4. Bewerk medewerker\n5. Verwijder medewerker" +
                "\n6. Lijst van alle klanten\n7. Bestelling per medewerker");
        }
    }
}
