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
            //while (login)
            //{
            //    Console.WriteLine("Login naar Verhuursysteem.");
            //    Console.WriteLine("Vul je werknemersnummer in:");
            //    int loginNummer = Convert.ToInt32(Console.ReadLine());
            //    for (int i = 0; i < medewerkers.Count; i++)
            //    {
            //        if (medewerkers[i].medewerkernummer == loginNummer)
            //        {
            //            Console.WriteLine("Nummer gevonden.");
            //            break;
            //        }
            //        else if (medewerkers[i].medewerkernummer != loginNummer && i == medewerkers.Count -1)
            //        {
            //            Console.WriteLine("Nummer niet gevonden, probeer het nogmaals.");
            //        }
            //    }
            //    Console.WriteLine("Vul je wachtwoord in:");
            //    string datumInDienst = Console.ReadLine();
            //    if (medewerkers[loginNummer - 1].datumInDienst == Convert.ToDateTime(datumInDienst))
            //    {
            //        Console.WriteLine("Wachtwoord klopt, je wordt ingelogd.");
            //        login = false;
            //        break;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Inlogpoging mislukt. Log opnieuw in.");
            //    }
            //}
            
            Console.WriteLine("Welkom bij het Bakfiets Verhuur Programma van VanderBinckes.");
            while (true)
            {
                RunMainMenu();
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze < 0 || keuze > 3)
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
                            Verhuurmenu();
                            break;
                        case 2:
                            MedewerkerMenu();
                            break;
                        case 3:
                            KlantMenu();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private static void Verhuurmenu()
        {
            bool menu = true;
            Console.Clear();
            while (menu)
            {
                Console.WriteLine("Verhuur menu\n0. Ga terug naar het vorige menu\n1. Lijst van all verhuren\n2. Verhuur per medewerker\n3. Verhuur per klant\n4. Maak nieuwe verhuur aan\n5. Wijzig verhuur\n6. Verwijder verhuur");
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze < 0 || keuze > 6)
                {
                    Console.WriteLine("Ongeldige keuze, probeer het nog een keer.");
                }
                else
                {
                    switch (keuze)
                    {
                        case 0:
                            menu = false;
                            break;
                        case 1:
                            ListAlleBestellingen();
                            break;
                        case 2:
                            BestellingPerMedewerker();
                            break;
                        case 3:
                            BestellingPerKlant();
                            break;
                        case 4:
                            MaakVerhuur();
                            break;
                        case 5:
                            BewerkVerhuur();
                            break;
                        case 6:
                            VerwijderVerhuur();
                            break;
                        default:
                            break;
                    }
                }
            }
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
        private static void BewerkVerhuur() {
            Console.Clear();
            Console.WriteLine("Geef het verhuurnummer op:");
            int verhuurnummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Geef je medewerkernummer op:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Geeft het nieuwe klantnummer op voor verhuur:");
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
            }
            decimal huurprijstotaal = BerekenHuurprijs(bakfietsnummer, aantalDagen, selAccessoires);
            Console.WriteLine("Daarmee is de totale huurprijs: €" + huurprijstotaal);
            Verhuur verhuur = new Verhuur(verhuurnummer, dateTime, bakfietsnummer, aantalDagen, huurprijstotaal, klantnummer, medewerkernummer);
            dbObject.EditBestelling(verhuur);
        }
        private static void VerwijderVerhuur()
        {
            Console.Clear();
            Console.WriteLine("Verhuur verwijderen");
            Console.WriteLine("Voer het verhuurnummer in:");
            int verhuurnummer = Convert.ToInt32(Console.ReadLine());
            Verhuur verhuur = new Verhuur(verhuurnummer);
            dbObject.DeleteBestelling(verhuur);
        }
        private static void ListAlleBestellingen() {
            dbObject.ListBestelling();
        }
        private static void BestellingPerMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Vul medewerkernummer in:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Medewerker medewerker = new Medewerker(medewerkernummer);
            dbObject.ListBestellingMedewerker(medewerker);
            Console.WriteLine("Druk op een toets om verder te gaan.");
            Console.ReadLine();
        }
        private static void BestellingPerKlant()
        {
            Console.Clear();
            Console.WriteLine("Vul klantnummer in:");
            int klantnummer = Convert.ToInt32(Console.ReadLine());
            Klant klant = new Klant(klantnummer);
            dbObject.ListBestellingKlant(klant);
            Console.WriteLine("Druk op een toets om verder te gaan.");
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

        private static void KlantMenu()
        {
            bool menu = true;
            Console.Clear();
            while (menu)
            {
                Console.WriteLine("Klanten menu\n0. Ga terug naar het vorige menu\n1. Lijst van alle klanten\n2. Maak nieuwe klant aan\n3. Wijzig klant\n4. Verwijder klant");
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze < 0 || keuze > 4)
                {
                    Console.WriteLine("Ongeldige keuze, probeer het nog een keer.");
                }
                else
                {
                    switch (keuze)
                    {
                        case 0:
                            menu = false;
                            break;
                        case 1:
                            ListAlleKlanten();
                            break;
                        case 2:
                            MaakKlant();
                            break;
                        case 3:
                            BewerkKlant();
                            break;
                        case 4:
                            VerwijderKlant();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private static void ListAlleKlanten()
        {
            List<Klant> klanten = dbObject.ListKlanten();
            klanten.ForEach(item => Console.WriteLine(item.voornaam + " " + item.achternaam + " Klantnummer: " + item.klantnummer));
        }
        private static void MaakKlant()
        {
            Console.Clear();
            Console.WriteLine("Nieuwe klant aanmaken.\nWat is de achternaam van de klant?");
            string naam = Console.ReadLine();
            Console.WriteLine("Wat is de voornaam?");
            string voornaam = Console.ReadLine();
            Console.WriteLine("Wat is de postcode?(Notatie 1111AA)");
            string postcode = Console.ReadLine();
            Console.WriteLine("Wat is het huisnummer, zonder toevoeging?");
            int huisnummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Wat is de toevoeging?(Mag leeg zijn)");
            string huisnummerToev = Console.ReadLine();
            Console.WriteLine("Opmerkingen(Mag leeg zijn):");
            string opmerkingen = Console.ReadLine();
            Klant klant = new Klant(voornaam, naam, postcode, huisnummer, huisnummerToev, opmerkingen);
            dbObject.CreateKlant(klant);
        }
        private static void BewerkKlant() {
            Console.Clear();
            Console.WriteLine("Klant bewerken.");
            Console.WriteLine("Voer het klantnummer in:");
            int klantnummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Wat is de nieuwe achternaam van de klant?");
            string naam = Console.ReadLine();
            Console.WriteLine("Wat is de nieuwe voornaam?");
            string voornaam = Console.ReadLine();
            Console.WriteLine("Wat is de nieuwe postcode?(Notatie 1111AA)");
            string postcode = Console.ReadLine();
            Console.WriteLine("Wat is het nieuwe huisnummer, zonder toevoeging?");
            int huisnummer = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Wat is de toevoeging?(Mag leeg zijn)");
            string huisnummerToev = Console.ReadLine();
            Console.WriteLine("Opmerkingen(Mag leeg zijn):");
            string opmerkingen = Console.ReadLine();
            Klant klant = new Klant(klantnummer, voornaam, naam, postcode, huisnummer, huisnummerToev, opmerkingen);
            dbObject.EditKlant(klant);
        }
        private static void VerwijderKlant() {
            Console.Clear();
            Console.WriteLine("Klant verwijderen.");
            Console.WriteLine("Voer het klantnummer in:");
            int klantnummer = Convert.ToInt32(Console.ReadLine());
            Klant klant = new Klant(klantnummer);
            dbObject.DeleteKlant(klant);
        }

        private static void MedewerkerMenu()
        {
            bool menu = true;
            Console.Clear();
            while (menu)
            {
                Console.WriteLine("Medewerker menu\n0. Ga terug naar het vorige menu\n1. Lijst van alle medewerkers\n2. Maak nieuwe werknemer aan\n3. Wijzig werknemer\n4. Verwijder werknemer");
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze < 0 || keuze > 4)
                {
                    Console.WriteLine("Ongeldige keuze, probeer het nog een keer.");
                }
                else
                {
                    switch (keuze)
                    {
                        case 0:
                            menu = false;
                            break;
                        case 1:
                            ListAlleMedewerkers();
                            break;
                        case 2:
                            MaakMedewerker();
                            break;
                        case 3:
                            BewerkMedewerker();
                            break;
                        case 4:
                            VerwijderMedewerker();
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
        private static void VerwijderMedewerker()
        {
            Console.Clear();
            Console.WriteLine("Medewerker verwijderen.");
            Console.WriteLine("Voer het medewerkernummer in:");
            int medewerkernummer = Convert.ToInt32(Console.ReadLine());
            Medewerker medewerker = new Medewerker(medewerkernummer);
            dbObject.DeleteMedewerker(medewerker);
        }
        
        private static void RunMainMenu()
        {
            Console.WriteLine("\nKies uit het onderstaande menu de gewenste optie.");
            Console.WriteLine("0. Stop programma\n1. Verhuur menu" +
                "\n2. Medewerker menu\n3. Klant menu");
        }
    }
}
