using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Hangman_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {   // Raihan Carder

            List<string> wordBank = File.ReadAllLines("words.txt").ToList();
            //List<char> lettersInWord = new List<char>();


            Random generator = new Random();
            string displayWord = "", guessedLetters = "", word;
            char guess;
            int randomWord, failedGuesses = 0;
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
                word = wordBank[randomWord].ToUpper();

                for (int i = 0; i < word.Length; i++)
                {
                    displayWord += "_ ";
                }

                Console.WriteLine(wordBank[randomWord]); // delete later
                Console.WriteLine(wordBank.Count); // delete later
                                                                

                while (!game)    // Starts game
                {
                    
                    HangingMan(failedGuesses);
                    Console.WriteLine();

                    Console.WriteLine(displayWord);

                    Console.WriteLine();
                    Console.WriteLine();
                    //Console.WriteLine($"Guessed letters: {guessedLetters}");

                    if (failedGuesses < 8)
                    {
                        Console.Write("Guess a letter: ");
                       
                        if (char.TryParse(Console.ReadLine().ToUpper().Trim(), out guess))
                        {
                            if (word.Contains(guess))
                            {
                                displayWord = displayWord.Remove(1,2);
                                
                            }
                            else
                            {
                                failedGuesses += 1;
                            }
                        }
                        Console.Clear();
                                                 
                    }
                    else
                    {

                    }
                    


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
            else if (guesses == 1)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else if (guesses == 2)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|    |");
                Console.WriteLine("|    |");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else if (guesses == 3)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|");
                Console.WriteLine("|    |");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else if (guesses == 4)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|/");
                Console.WriteLine("|    |");
                Console.WriteLine("|   ");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else if (guesses == 5)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|/");
                Console.WriteLine("|    |");
                Console.WriteLine("|   /");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else if (guesses == 6)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|/");
                Console.WriteLine("|    |");
                Console.WriteLine("|   / \\");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else if (guesses == 7)
            {
                Console.WriteLine("____");
                Console.WriteLine("|/   |");
                Console.WriteLine("|   (_)");
                Console.WriteLine("|   \\|/");
                Console.WriteLine("|    |");
                Console.WriteLine("|   | |");
                Console.WriteLine("|   ");
                Console.WriteLine("|_____");
            }
            else
            {
                Console.WriteLine("ERROR");
            }

        }
    }
}

