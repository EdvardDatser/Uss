using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
	class Program
	{
		static void Main( string[] args )
		{
			Console.SetWindowSize( 95, 25 );

			Walls walls = new Walls( 80, 25 );
			walls.Draw();

			// Отрисовка точек

			Point p = new Point( 4, 5, '*' );
			Snake snake = new Snake( p, 4, Direction.RIGHT );
			snake.Draw();

			FoodCreator GoldFood = new FoodCreator(80, 25 , '@');
			Point gfood = GoldFood.CreateFood();
			gfood.Draw();

			FoodCreator foodCreator = new FoodCreator( 80, 25, '$' );
			Point food = foodCreator.CreateFood();
			food.Draw();

			FoodCreator BombFood = new FoodCreator(80, 25, 'õ');
			Point bfood = BombFood.CreateFood();
			bfood.Draw();

			Score s = new Score();

			Speed speed = new Speed();

			while (true)
			{
				Console.ForegroundColor= ConsoleColor.Green;
				if ( walls.IsHit(snake) || snake.IsHitTail() )
				{
					break;
				}
				if(snake.Eat( food ) )
				{

					speed.Speedy();
					food = foodCreator.CreateFood();
					food.Draw();
					s.score++;
					if (s.score == 5)
					{
						Console.ForegroundColor= ConsoleColor.Yellow;
                        gfood = GoldFood.CreateFood();
						gfood.Draw();
						s.score += 10;
                    }

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
            WriteText(						"Score: "+s.score           , xOffset + 1, yOffset++);
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
