using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {   // Raihan Carder

            List<string> wordBank = File.ReadAllLines("words.txt").ToList();
            Random generator = new Random();
            int randomWord;
            bool done = false, game = false;

            Console.Title = "Hangman Project";
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine();
            Console.WriteLine("To Play click Enter!");
            Console.ReadLine();
            Console.Clear();
            while (!done)
            {
                randomWord = generator.Next(0, 854); // Creates new word that user must guess

                while (!game)
                {
                    HangingMan(0);
                    Console.ReadLine() ;
                }                
            }
        }
        public static void HangingMan(int guesses)
        {
            if (guesses == 0)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
        }
    }
}

