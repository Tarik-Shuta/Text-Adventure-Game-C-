using System;
using System.Xml.Schema;

namespace SarajevoShadows
{
    class Program
    {
        static bool hasWeapon=false;
        static bool isAlive = true;
        static bool hasFriend = false;
        static string playerName;

        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to a text based adventure game - Sarajevo Shadows");
            Console.WriteLine("You are a resident of Sarajevo, determined to protect your hometown from hooligans");
            Console.WriteLine("Please enter your name: ");
            playerName=Console.ReadLine();
            
            Console.WriteLine(playerName + " are you ready to play the game?");
            int choice = GetChoice();
            if (choice == 2)
            {
                Console.WriteLine("Go train more, prepare yourself and then come back!");
                Environment.Exit(0);
            }
            MarketSquare();
        }

        static void MarketSquare()
        {
            Console.WriteLine("\nScene 1: The Market Square");
            Console.WriteLine("You see some shopkeepers packing up. There’s a broom handle nearby.");
            Console.WriteLine("1. Help the shopkeepers (Gain information on hooligans).");
            Console.WriteLine("2. Take the broom handle as a weapon.");
            int choice = GetChoice();
            if (choice == 1)
            {
                Console.WriteLine("The shopkeepers thank you and inform you that the hooligans are gathering at the Latin Bridge.");
            }
            else if (choice == 2)
            {
                Console.WriteLine("You pick up the broom handle as a weapon.");
                hasWeapon = true;
            }
            LatinBridge();
        }

        static void LatinBridge()
        {
            Console.WriteLine("\nScene 2: The Latin Bridge");
            Console.WriteLine("You approach the bridge and hear voices of hooligans nearby.");
            Console.WriteLine("1. Cross the bridge directly (Risk a confrontation).");
            Console.WriteLine("2. Take a hidden path along the river Miljacka (Avoid detection).");
            int choice = GetChoice();
                    if (choice==1 && !hasWeapon)
                    {
                        Console.WriteLine("You try to cross directly, but the hooligans spot you. Without a weapon, you are overpowered.");
                        isAlive = false;
                        EndGame();
                    }
                    else if (choice==1 && hasWeapon)
                    {
                        Console.WriteLine("Armed with the broom handle, you fight off the hooligans and cross safely.");
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine("You take the hidden path along the river Miljacka, avoiding detection.");
                    }
                    OldTownTavern();
        }

        static void OldTownTavern()
        {
            Console.WriteLine("\nScene 3: The Old Town Tavern");
            Console.WriteLine("You spot your friend Ahmed in the local tavern. He could be useful in a fight.");
            Console.WriteLine("1. Ask Ahmed for help (Gain an ally).");
            Console.WriteLine("2. Continue alone through the alleys (Rely on stealth).");
            int choice = GetChoice();
            if (choice == 1)
            {
                Console.WriteLine("Ahmed agrees to join you. You now have an ally.");
                hasFriend = true;
            }
            else if (choice == 2)
            {
                Console.WriteLine("You decide to continue alone, relying on stealth to stay safe.");
            }
            CityHall();
        }

        static void CityHall()
        {
            Console.WriteLine("\nScene 4: Sarajevo City Hall (Final Showdown)");
            Console.WriteLine("You arrive at the City Hall, where hooligans are preparing for a major disturbance.");
            Console.WriteLine("1. Confront the hooligans directly (Fight them).");
            Console.WriteLine("2. Signal for police backup (Stall for help).");
            int choice = GetChoice();
            if (choice == 1&& !hasFriend)
            {
                Console.WriteLine("\nYou face the hooligans alone, but without backup, you are outnumbered and overpowered.");
                isAlive = false;
                EndGame();
            }
            else if (choice==1 && hasFriend)
            {
                Console.WriteLine("\nWith Ahmed by your side, you confront the hooligans and hold them off until they flee.");
                EndGame();
            }
            else if (choice == 2)
            {
                Console.WriteLine("\nYou signal for the police, holding the hooligans at bay as best you can.");
                Console.WriteLine("However, the hooligans become more suspicious of your actions and start closing in on you.");
                Console.WriteLine("Before the police can arrive, one of the hooligans spots your signal and shouts, alerting the others.");
                Console.WriteLine("They rush towards you, catching you off guard. You fight back, but you're outnumbered and overwhelmed.");
                isAlive = false;
                EndGame();
                
            }
            
        }
        
        
        static void EndGame()
        {
            if (isAlive)
            {
                Console.WriteLine("Congratulations! You defended Sarajevo and survived the night.");
            }
            else
            {
                Console.WriteLine("Game Over!");
                Console.WriteLine("You fought bravely but was beaten by hooligans!");
            }
            Console.WriteLine("Thank you for playing Sarajevo Shadows!");
            Environment.Exit(0);
        }
         static int GetChoice()
                    {
                        int choice;
                        while (true)
                        {
                            Console.Write("Enter 1 or 2: ");
                            if (int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2))
                                return choice;
                            else
                            {
                                Console.WriteLine("Invalid input. Try again.");
                            }
                        }
                    }
    }
}