using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace vanderBinckesBP
{
    class Program
    {
        static void Main(string[] args)
        {
            Database dbObject = new Database();
            Console.WriteLine("Welkom bij het Bakfiets Verhuur Programma van VanderBinckes.");
            while (true)
            {
                Medewerker raoul = new Medewerker("Raoul", "Thimister");
                RunMainMenu();
                int keuze = Convert.ToInt32(Console.ReadLine());
                if (keuze < 1 || keuze > 5)
                {
                    Console.WriteLine("Ongeldige keuze, probeer het nog een keer.");
                }
                else
                {
                    switch (keuze)
                    {
                        case 1:
                            dbObject.CreateMedewerker(raoul);
                            break;
                        case 2:
                            dbObject.ListMedewerkers();
                            break;
                    }
                }
            }
        }

        private static void RunMainMenu()
        {
            Console.WriteLine("Kies uit het onderstaande menu de gewenste optie.");
            Console.WriteLine("1. Maak nieuwe mederwerker aan\n2. Lijst van alle medewerkers\n3. Bewerk medewerker\n4. keuze 4\n5. keuze 5");
        }
    }
}
