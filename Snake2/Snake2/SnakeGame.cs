using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake2
{
    public class SnakeGame
    {
        public static Screen screen;
        public static bool gameOver;
        private static Snake snake;
        private static Direction dir;
        private static ConsoleKey key;
        private static List<Food> food;
        static void Main(string[] args)
        {
            Init();
            
            while (true)
            {
                while (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                }
                if (gameOver)
                {
                    OnGameOver();
                }
                else
                {                                      
                    if(rand.Next(20) == 0)
                    {
                        Point pos = GetRandomPlace();
                        food.Add(new SpecialFood(rand.Next(10, 20), pos.x, pos.y, '♦'));
                    }
                   
                    if (food.Count<Food>(n => !(n is SpecialFood)) == 0)
                    {
                        Point pos = GetRandomPlace();
                        food.Add(new Food(2, pos.x, pos.y, '*'));
                    }
                    if (key == ConsoleKey.RightArrow) dir = Direction.Right;
                    if (key == ConsoleKey.LeftArrow) dir = Direction.Left;
                    if (key == ConsoleKey.UpArrow) dir = Direction.Up;
                    if (key == ConsoleKey.DownArrow) dir = Direction.Down;
                    Update();
                    Draw();
                   
                }
                Thread.Sleep(100);

            }
        }

        private static Point GetRandomPlace()
        {
            int x, y;
            do
            {
                x = rand.Next(screen.width);
                y = rand.Next(screen.height);
            }
            while (GetEntity(x, y) != null);
            return new Point(x, y);
        }
        static Random rand = new Random();

        private static void Update()
        {
            snake.Update(dir);
            for(int i = 0; i< food.Count; i++)               
            {
                Entity entity = food[i];
                entity.Update();
            }
        }

        private static void Draw()
        {
            Console.Clear();
            screen.ClearScreen();
            snake.Draw();
            foreach (Entity entity in food)
            {
                entity.Draw();
            }
            string buffer = screen.GetScreen();
            buffer += Environment.NewLine + "Points: " + points;
            Console.WriteLine(buffer);
        }

        public static void GameOver()
        {
            gameOver = true;
        }

        private static void OnGameOver()
        {
            Console.Clear();
            if (key == ConsoleKey.R)
            {
                Init();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                string gameOverString = "Total Points: " + points;
                gameOverString += Environment.NewLine + "You Snooze You Lose";
                gameOverString += Environment.NewLine + "Better luck next time";
                gameOverString += Environment.NewLine + "For Replay Press R, and try not to lose again :)";
                Console.WriteLine(gameOverString);
            }
        }

        private static void Init()
        {
            Console.ForegroundColor = ConsoleColor.White;
            food = new List<Food>();
            screen = new Screen(20, 20);
            snake = new Snake(5, 2, 2);
            gameOver = false;
            points = 0;
            dir = Direction.Right;
        }
        public static Entity GetEntity(int x, int y)
        {
            Entity entity = null;
            if (food.Where<Entity>(n => n.x == x && n.y == y).ToList().Count > 0)
            {
                entity = food.Where<Entity>(n => n.x == x && n.y == y).First();
            }
            Entity cell = snake.GetCell(x, y);
            if (entity == null)
            {
                return cell;
            }
            return entity;
        }   
     
        public static void RemoveFood(Food foodToRemove)
        {
            food.Remove(foodToRemove);
        }

        public static void AddPoints(Food foodEaten)
        {
            points += foodEaten.value;            
        }
        static int points;
    }
}
