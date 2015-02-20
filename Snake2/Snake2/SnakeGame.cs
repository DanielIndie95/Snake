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

        private static Snake snake1;
        private static Snake snake2;

        private static Direction dir1;
        private static Direction dir2;

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
                    if (key == ConsoleKey.RightArrow) dir1 = Direction.Right;
                    if (key == ConsoleKey.LeftArrow) dir1 = Direction.Left;
                    if (key == ConsoleKey.UpArrow) dir1 = Direction.Up;
                    if (key == ConsoleKey.DownArrow) dir1 = Direction.Down;

                    if (key == ConsoleKey.D) dir2 = Direction.Right;
                    if (key == ConsoleKey.A) dir2 = Direction.Left;
                    if (key == ConsoleKey.W) dir2 = Direction.Up;
                    if (key == ConsoleKey.S) dir2 = Direction.Down;
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
            snake1.Update(dir1);
            snake2.Update(dir2);
            for(int i = 0; i< food.Count; i++)               
            {
                Food entity = food[i];
                entity.Update();
            }
        }

        private static void Draw()
        {
            Console.Clear();
            screen.ClearScreen();
            snake1.Draw();
            snake2.Draw();
            foreach (Entity entity in food)
            {
                entity.Draw();
            }
            string buffer = screen.GetScreen();
            buffer += Environment.NewLine + "Points: " + points1;
            Console.WriteLine(buffer);
        }

        public static void GameOver(Snake lost)
        {
            gameOver = true;
            if (lost == snake1) firstLost = true;
            else firstLost = false;
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
                string gameOverString = "Player 1 Total Points: " + points1;
                gameOverString += Environment.NewLine + "Player 2 Total Points: " + points2;
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
            snake1 = new Snake(5, 2, 2);
            snake2 = new Snake(5, 18, 18);
            gameOver = false;
            points1 = 0;
            dir1 = Direction.Right;
            dir2 = Direction.Right;
        }
        public static Entity GetEntity(int x, int y)
        {
            Entity entity = null;
            if (food.Where<Entity>(n => n.x == x && n.y == y).ToList().Count > 0)
            {
                entity = food.Where<Entity>(n => n.x == x && n.y == y).First();
            }
            Entity cell = snake1.GetCell(x, y);
            Entity cell2 = snake2.GetCell(x, y);
            if (entity == null)
            {
                if(cell2== null)
                return cell;
                return cell2;
            }
            return entity;
        }   
     
        public static void RemoveFood(Food foodToRemove)
        {
            food.Remove(foodToRemove);
        }

        public static void AddPoints(Food foodEaten , Snake snake)
        {
            if (snake == snake1)
                points1 += foodEaten.value;
            else
                points2 += foodEaten.value;
        }
        static int points1;
        static int points2;

        public static bool firstLost;
    }
}
