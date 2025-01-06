using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using Newtonsoft.Json;

namespace SarajevoShadows
{
    class Program
    {
        static bool hasWeapon = false;
        static bool hasSword = false;
        static bool gaveBeggarMoney = false;
        static bool hasAhmed = false;
        static bool hasKey = false;
        static Random random = new Random();

        static Dictionary<string, string> gameText;

        static void Main(string[] args)
        {
            LoadGameData();

            Console.WriteLine(gameText["welcome"]);
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();

            Console.WriteLine(gameText["mission"].Replace("{playerName}", playerName));
            MarketSquare();
        }

        static void LoadGameData()
        {
            try
            {
                string jsonData = File.ReadAllText("GameData.json");
                gameText = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading game data: " + ex.Message);
                Environment.Exit(1);
            }
        }

        static void MarketSquare()
        {
            Console.WriteLine(gameText["marketSquare_intro"]);
            Console.WriteLine(gameText["marketSquare_choices"]);
            int choice = GetChoice(2);

            if (choice == 1)
            {
                Console.WriteLine(gameText["marketSquare_help"]);

                if (random.Next(1, 101) <= 50)
                {
                    Console.WriteLine(gameText["marketSquare_key"]);
                    hasKey = true;
                }
            }
            else if (choice == 2)
            {
                Console.WriteLine(gameText["marketSquare_weapon"]);
                hasWeapon = true;
            }

            Console.WriteLine(gameText["marketSquare_next"]);
            choice = GetChoice(2);

            if (choice == 1)
                LatinBridge();
            else if (choice == 2)
                Sebilj();
        }

        static void LatinBridge()
        {
            Console.WriteLine(gameText["latinBridge_intro"]);

            if (hasWeapon)
            {
                Console.WriteLine(gameText["latinBridge_fight"]);
                EndGame(false);
            }
            else
            {
                Console.WriteLine(gameText["latinBridge_defeat"]);
                EndGame(false);
            }
        }

        static void Sebilj()
        {
            Console.WriteLine(gameText["sebilj_intro"]);
            
            Console.WriteLine("1. Give him 1 mark.\n2. Ignore him.");
            if (hasKey)
            {
                Console.WriteLine("3. Offer the mysterious key.");
            }
            
            int choice = hasKey ? GetChoice(3) : GetChoice(2);

            if (choice == 1)
            {
                Console.WriteLine(gameText["sebilj_beggar_yes"]);
                gaveBeggarMoney = true;
            }
            else if (choice == 2)
            {
                Console.WriteLine(gameText["sebilj_beggar_no"]);
            }
            else if (choice == 3 && hasKey)
            {
                Console.WriteLine(gameText["sebilj_beggar_key"]);
            }

            Console.WriteLine(gameText["sebilj_food"]);
            choice = GetChoice(2);

            if (choice == 1)
            {
                Console.WriteLine(gameText["sebilj_cevapi"]);
                EndGame(false);
            }
            else if (choice == 2)
            {
                Console.WriteLine(gameText["sebilj_burek"]);

                Console.WriteLine(gameText["sebilj_ahmed"]);
                
                if (hasKey)
                {
                    Console.WriteLine(gameText["sebilj_key"]);
                }
                choice = hasKey ? GetChoice(3) : GetChoice(2);
                if (choice == 1)
                {
                    Console.WriteLine(gameText["sebilj_ahmed_yes"]);
                    hasAhmed = true;
                }
                else if (choice == 2)
                {
                    Console.WriteLine(gameText["sebilj_ahmed_no"]);
                }
                else if (choice == 3 && hasKey)
                {
                    Console.WriteLine(gameText["sebilj_sword"]);
                    hasKey = false;
                    hasSword = true;
                }

                CityHall();
            }
        }


        static void CityHall()
        {
            Console.WriteLine(gameText["cityHall_intro"]);

            if (hasSword)
            {
                Console.WriteLine(gameText["cityHall_sword"]);
                EndGame(true);
                return;
            }
            if (!hasAhmed)
            {
                Console.WriteLine(gameText["cityHall_noAhmed"]);
                EndGame(false);
                return;
            }

            Console.WriteLine(gameText["cityHall_attack"]);
            int choice = GetChoice(2);

            if (choice == 1)
            {
                Console.WriteLine(gameText["cityHall_ahmed"]);

                if (gaveBeggarMoney)
                {
                    Console.WriteLine(gameText["cityHall_beggar"]);
                    EndGame(true);
                }
                else
                {
                    Console.WriteLine(gameText["cityHall_defeat"]);
                    EndGame(false);
                }
            }
            else
            {
                Console.WriteLine(gameText["cityHall_ambush"]);
                EndGame(false);
            }
        }

        static void EndGame(bool victory)
        {
            Console.WriteLine(victory ? gameText["end_victory"] : gameText["end_defeat"]);

            Console.WriteLine(gameText["end_replay"]);
            int choice = GetChoice(2);
            if (choice == 1)
            {
                ResetGameState();
                MarketSquare();
            }
            else
            {
                Console.WriteLine(gameText["end_exit"]);
                Environment.Exit(0);
            }
        }

        static void ResetGameState()
        {
            hasWeapon = false;
            hasSword = false;
            gaveBeggarMoney = false;
            hasAhmed = false;
            hasKey = false;
        }

        static int GetChoice(int maxOption)
        {
            int choice;
            while (true)
            {
                Console.Write("Enter your choice: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= maxOption)
                    return choice;

                Console.WriteLine($"Invalid input. Please enter a number between 1 and {maxOption}.");
            }
        }
    }
}
