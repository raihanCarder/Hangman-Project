using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hangman_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {   // Raihan Carder

            List<string> wordBank = File.ReadAllLines("words.txt").ToList();           
            List<char> guessedLetters = new List<char>();
            List<char> correctLetters = new List<char>();
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            Random generator = new Random();
            string displayWord = "", word, playagain, customGame, customWord = "";
            char guess;
            int randomWord, failedGuesses, wins = 0, losses = 0;
            bool done = false, game = false, validguess = false, gameWon = false, validAnswer = false, customMatch = false;

            Console.Title = "Hangman Project";

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine();
            Console.WriteLine("Click Enter To Play!");
            Console.ReadLine();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.WriteLine("How to play Hangman:");
            Console.WriteLine();
            Console.WriteLine("Guess a letter!");
            Console.WriteLine("You get 7 tries to guess the word, run out of tries and you lose.");
            Console.WriteLine("Your goal is to guess the word in under 7 tries.");
            Console.WriteLine("To play couch co-op and choose the word, type 'C'");
            Console.Write("Understand? To play normally Click Enter!");

            customGame = Console.ReadLine().ToUpper().Trim();

            if (customGame == "C")
            {
                Console.Write("Enter a custom word (It'll be hidden): ");
                Console.ForegroundColor = ConsoleColor.White;
                customWord = Console.ReadLine().ToUpper().Trim();
                customMatch = true;

                Console.ForegroundColor = ConsoleColor.Black;

                for (int i = 0; i < customWord.Length; i++)
                {
                    if (!alphabet.Contains(customWord[i]))
                    {
                        customMatch = false;
                    }                  
                }

                if (string.IsNullOrEmpty(customWord))
                {
                    customMatch = false;
                }

                if (customMatch == false)
                {
                    Console.Write("Invalid Custom Word, Click enter to play normally! ");
                    Console.ReadLine();
                }
            }         
                   
           

            while (!done)
            {
                failedGuesses = 0;
                displayWord = "";
                guessedLetters.Clear();
                correctLetters.Clear();

                randomWord = generator.Next(1, 855); // Creates new word that user must guess
                word = wordBank[randomWord].ToUpper();

                if (customMatch == true)
                {
                    word = customWord;
                }
                customMatch = false;

                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Clear();

                for (int i = 0; i < word.Length; i++)
                {
                    displayWord += "_";                  
                }
                                                              
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
                            if (alphabet.Contains(guess) && !correctLetters.Contains(guess) && !guessedLetters.Contains(guess))
                            {
                                validguess = true;

                                if (word.Contains(guess))
                                {
                                    correctLetters.Add(guess);

                                    for (int i = 0; i < word.Count(); i++)
                                    {                                     
                                        if (word[i] == guess)
                                        {
                                            displayWord = displayWord.Remove(i, 1);
                                            displayWord = displayWord.Insert(i, guess.ToString());
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
                        else
                        {
                            Console.Write("Invalid, guess a letter: ");
                        }    
                    }
                    validguess = false;

                    if (displayWord.Equals(word))
                    {
                        gameWon = true;
                    }

                    if (failedGuesses == 8)
                    {
                        game = true;
                        losses++;
                    }
                    else if (gameWon == true)
                    {
                        game = true;
                        wins++;
                        Console.Beep();
                    }
                    else
                    {
                        Console.Clear();

                        Console.Write("You have incorrectly guessed: ");
                        for (int i = 0; i < guessedLetters.Count(); i++)
                        {
                            Console.Write(guessedLetters[i] + " ");
                        }
                        Console.WriteLine();
                    }
                                      
                    
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine("GAME OVER");
                if (gameWon == true)
                {
                    Console.WriteLine($"Congratulations you have correctly guessed the word: {word}!");
                }
                else
                {
                    Console.WriteLine($"You've lost, the Word was: {word}!");
                }
                Console.WriteLine($"You have won {wins} time(s).");
                Console.WriteLine($"You have lost {losses} time(s).");
                Console.Write("Wanna play again? (Yes/No) : ");

                while (!validAnswer)
                {
                    playagain = Console.ReadLine().ToUpper().Trim();

                    if (playagain == "YES")
                    {
                        game = false;
                        gameWon = false;
                        validAnswer = true;
                        Console.Clear();
                    }
                    else if (playagain == "NO")
                    {
                        done = true;
                        validAnswer = true;
                    }
                    else
                    {
                        Console.Write("Invalid Answer, Wanna play again? ");

                    }
                }
                validAnswer = false;
            }


            Console.WriteLine("Thank You for playing! (Song playing, to Quit Click the X!)");
            LeavingSong();
            Console.ReadLine();
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
        public static void LeavingSong() // Super Mario Song  
        {
            Console.Beep(659, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(523, 125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(375); Console.Beep(392, 125); Thread.Sleep(375); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(250); Console.Beep(392, 125); Thread.Sleep(250); Console.Beep(330, 125); Thread.Sleep(250); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(494, 125); Thread.Sleep(125); Console.Beep(466, 125); Thread.Sleep(42); Console.Beep(440, 125); Thread.Sleep(125); Console.Beep(392, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(880, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(784, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(587, 125); Console.Beep(494, 125); Thread.Sleep(375); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125); Thread.Sleep(1125); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(698, 125); Thread.Sleep(125); Console.Beep(698, 125); Console.Beep(698, 125); Thread.Sleep(625); Console.Beep(784, 125); Console.Beep(740, 125); Console.Beep(698, 125); Thread.Sleep(42); Console.Beep(622, 125); Thread.Sleep(125); Console.Beep(659, 125); Thread.Sleep(167); Console.Beep(415, 125); Console.Beep(440, 125); Console.Beep(523, 125); Thread.Sleep(125); Console.Beep(440, 125); Console.Beep(523, 125); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(622, 125); Thread.Sleep(250); Console.Beep(587, 125); Thread.Sleep(250); Console.Beep(523, 125);
        }
    }
}

