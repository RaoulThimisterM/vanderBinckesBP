using System;
using MySql.Data.MySqlClient;


namespace vanderBinckesBP
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Welkom bij het Bakfiets Verhuur Programma van VanderBinckes.");
            //Console.WriteLine("Kies uit het onderstaande menu de gewenste optie.");
            //string keuze = Console.ReadLine();
            Database dbObject = new Database();
            dbObject.isConnect();
        }
    }
}
