using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Hangman_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {   // Raihan Carder

            List<string> wordBank = File.ReadAllLines("words.txt").ToList();
            List<char> guessedLetters = new List<char>();
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            Random generator = new Random();
            string displayWord = "", word;
            char guess;
            int randomWord, failedGuesses = 0;
            bool done = false, game = false, validguess = false, gameWon = false;

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
                    displayWord += "_";                  
                }

                Console.WriteLine(word); // delete later
                Console.WriteLine(displayWord.Count()); // Delete later
                Console.WriteLine(wordBank.Count); // delete later                                                              

                while (!game)    // Starts game
                {                  
                    HangingMan(failedGuesses);
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int i = 0; i < word.Count(); i++) // Spaces out Letters 
                    {
                        Console.Write(displayWord[i]);
                        Console.Write(" ");
                    }

                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write("Guess a letter: ");
                    while (!validguess)
                    {
                        if (char.TryParse(Console.ReadLine().Trim().ToUpper(), out guess))
                        {
                            if (alphabet.Contains(guess))
                            {
                                validguess = true;

                                if (word.Contains(guess))
                                {
                                    for (int i = 0; i < word.Count(); i++)
                                    {                                     
                                        if (word[i] == guess)
                                        {
                                            displayWord.Remove(i);
                                            displayWord.Insert(i, guess.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    failedGuesses += 1;

                                    if (!guessedLetters.Contains(guess))
                                    {
                                        guessedLetters.Add(guess);
                                    }
                                }
                            }
                            else
                            {
                                Console.Write("Input invalid, Guess a letter: ");
                            }

                        }
                    }
                    validguess = false;

                    if (displayWord.Equals(word))
                    {
                        gameWon = true;
                    }

                    if (failedGuesses == 7 || gameWon)
                    {
                        game = true;
                    }
                    else
                    {
                        Console.Clear();

                        Console.Write("You have guessed: ");
                        for (int i = 0; i < guessedLetters.Count(); i++)
                        {
                            Console.Write(guessedLetters[i] + "");
                        }
                        Console.WriteLine();
                    }
                                      
                    
                }

                Console.Clear();
                Console.WriteLine("GAME OVER");
                Console.ReadLine();
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

