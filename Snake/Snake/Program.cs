using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Snake
{
	class Program
	{
        int speedValue;
        int minSpeed = 45;
        int faster = 15;
        public void Speed()
        {
            if (speedValue > minSpeed)
            {
                speedValue -= faster;
            }
        }
        static void Main( string[] args )
		{
			Console.SetWindowSize( 95, 25 );

            //Console.WriteLine("Mängija: ");
            //string ans = Console.ReadLine();
            //Console.SetCursorPosition(Console.WindowWidth - 1, 15);
            //Console.Write("Mängija : "+ ans);

            Walls walls = new Walls( 80, 25 );
			walls.Draw();

            // Отрисовка точек

            Console.ForegroundColor = ConsoleColor.Green;
            Point p = new Point( 4, 5, '*' );
			Snake snake = new Snake( p, 4, Direction.RIGHT );
			snake.Draw();

			FoodCreator GoldFood = new FoodCreator(80, 25 , '@');
			Point gfood = GoldFood.CreateFood();

			FoodCreator foodCreator = new FoodCreator( 80, 25, '$' );
			Point food = foodCreator.CreateFood2();
			food.Draw();

            FoodCreator BombFood = new FoodCreator(80, 25, '#');
            Point bfood = BombFood.CreateFood3();

            Speed speed = new Speed();

            Score s = new Score(); // eto 0

			
			while (true)
			{
                Console.SetCursorPosition(Console.WindowWidth - 15, 1);
                Console.Write("Score : " + s.score);
            if ( walls.IsHit(snake) || snake.IsHitTail() )
			{
				break;
			}
				if (snake.Eat(food))
				{
					food = foodCreator.CreateFood2();
					food.Draw();
					s.score += 2;
				}
					if (s.score %6 == 0)
					{
						Console.ForegroundColor= ConsoleColor.Yellow;
						gfood.Draw();
					}
						if (snake.Eat(gfood))
						{
							s.score += 6;
						}
							if (s.score % 16 == 0)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								bfood.Draw();
							}
								if (snake.Eat(bfood)) 
								{
									s.score -= 4;
								}
			else
			{
				snake.Move();
			}

				Thread.Sleep(speed.SpeedValue);
				if ( Console.KeyAvailable )
				{
					ConsoleKeyInfo key = Console.ReadKey();
					snake.HandleKey( key.Key );
				}
			}
			WriteGameOver(s);
			Console.ReadLine();
        }


		static void WriteGameOver(Score s)
		{
			int xOffset = 25;
			int yOffset = 8;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.SetCursorPosition( xOffset, yOffset++ );
			WriteText( "============================", xOffset, yOffset++ );
			WriteText( "            END             ", xOffset + 1, yOffset++ );
            WriteText( "Score: "+s.score           , xOffset + 1, yOffset++);
            yOffset++;
			WriteText( "       Edvard Datser        ", xOffset + 2, yOffset++ );
			WriteText( "", xOffset + 1, yOffset++ );
			WriteText( "============================", xOffset, yOffset++ );
		}

		static void WriteText( String text, int xOffset, int yOffset )
		{
			Console.SetCursorPosition( xOffset, yOffset );
			Console.WriteLine( text );
		}

	}
}
