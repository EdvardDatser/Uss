using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Snake
{
    class Program
    {
        public static string playerName = "";

        public static Score s = new Score(); // eto 0

        public static string[] strings = { "" };

        static void Main(string[] args)
        {

            Load();

            Console.Write("Write your name: ");
            playerName = Console.ReadLine();

            Console.Clear();

            Console.SetWindowSize(95, 25);

            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Отрисовка точек

            Console.ForegroundColor = ConsoleColor.Green;
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            FoodCreator GoldFood = new FoodCreator(80, 25, '@');
            Point gfood = GoldFood.CreateFood();

            FoodCreator foodCreator = new FoodCreator(80, 25, '$');
            Point food = foodCreator.CreateFood2();
            food.Draw();

            FoodCreator BoostFood = new FoodCreator(80, 25, '/');
            Point bfood = BoostFood.CreateFood3();

            Speed sp = new Speed();

            Boolean drawn = true;

            while (true)
            {
                Console.SetCursorPosition(Console.WindowWidth - 15, 1);
                Console.Write("Score : " + s.score);
                Console.SetCursorPosition(Console.WindowWidth - 15, 6);
                Console.Write("Curr speed: " + sp.SpeedValue);
                Console.SetCursorPosition(Console.WindowWidth - 15, 8);
                Console.Write("Player: " + playerName);
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                else
                {
                    snake.Move();
                }
                if (snake.Eat(food))
                {
                    s.score += 2;
                    drawn = false;
                    Console.SetCursorPosition(Console.WindowWidth - 15, 2);
                    Console.Write("eat food");
                }
                else if (snake.Eat(gfood))
                {
                    s.score += 4;
                    drawn = false;
                    Console.SetCursorPosition(Console.WindowWidth - 15, 2);
                    Console.Write("eat gfood");
                }
                else if (snake.Eat(bfood))
                {
                    sp.speedy();
                    s.score += 2;
                    drawn = false;
                    Console.SetCursorPosition(Console.WindowWidth - 15, 2);
                    Console.Write("eat bood");
                }
                if (drawn == false && s.score != 0 && s.score % 16 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    gfood = GoldFood.CreateFood();
                    gfood.Draw();
                    drawn = true;
                    Console.SetCursorPosition(Console.WindowWidth - 15, 3);
                    Console.Write("drawn1 - " + drawn);
                }
                else if (drawn == false && s.score != 0 && s.score % 6 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    bfood = BoostFood.CreateFood3();
                    bfood.Draw();
                    drawn = true;
                    Console.SetCursorPosition(Console.WindowWidth - 15, 3);
                    Console.Write("drawn2 - " + drawn);
                }
                else if (drawn == false)
                {
                    food = foodCreator.CreateFood2();
                    food.Draw();
                    drawn = true;
                    Console.SetCursorPosition(Console.WindowWidth - 15, 3);
                    Console.Write("drawn3- " + drawn);
                }


                Thread.Sleep(sp.SpeedValue);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver(s);
            SaveResult();
            Console.ReadLine();
        }


        static void Load()
        {
            strings[0] = File.ReadAllText("C:\\Users\\datse\\source\\repos\\Snake\\Snake\\Snake\\Result.txt");
        }
        static void SaveResult()
        {
            strings[0] += "" + s.score + " Name: " + playerName + "\n";
            File.WriteAllLines("C:\\Users\\datse\\source\\repos\\Snake\\Snake\\Snake\\Result.txt", strings);
        }


        static void WriteGameOver(Score s)
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("            END             ", xOffset + 1, yOffset++);
            WriteText("Score: " + s.score, xOffset + 1, yOffset++);
            yOffset++;
            WriteText("       Edvard Datser        ", xOffset + 2, yOffset++);
            WriteText("", xOffset + 1, yOffset++);
            WriteText("============================", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }

    }
}
