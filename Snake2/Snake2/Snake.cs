using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class Snake
    {
        private List<Entity> body;
        private int originX;
        private int originY;
        private Direction lastDir;
        private int eaten;
        public Snake(int bodyLength , int startX , int startY)
        {
            body = new List<Entity>(); //new Entity[bodyLength];
            originX = startX;
            originY = startY;
            eaten = 0;
            InitSnake(bodyLength);
        }

        public void InitSnake(int length)
        {
            for (int i = 0; i < length; i++)
            {
                body.Add(new Entity(originX + i, originY, '@'));
            }
            body[body.Count - 1].sign = '☻';
        }

        public void Update( Direction dir)
        {
            if (Opposite(dir, lastDir))
            {
                dir = lastDir;
            }
            int x = body[body.Count - 1].x;
            int y = body[body.Count - 1].y;
            switch (dir)
            {
                case Direction.Down:
                    {
                        y++;
                        
                    } break;
                case Direction.Up:
                    {
                        y--;
                    }
                    break;
                case Direction.Right:
                    {
                        x++;
                    }
                    break;
                case Direction.Left:
                    {
                        x--;
                    } break;
                    
            }
            if (GetCell(x,y) != null)
            {
                SnakeGame.GameOver();
                return;
            }
            if (SnakeGame.screen.OutOfBorder(y, x))
            {
                if (x == -1)
                {
                    x = SnakeGame.screen.width - 1;
                    
                }
                if (x == SnakeGame.screen.width)
                {
                    x =0;
                }
                if (y == SnakeGame.screen.height)
                {
                    y = 0;
                }
                if (y == -1)
                {
                    y = SnakeGame.screen.height-1;
                }
                
            }

            if (SnakeGame.GetEntity(x, y) is Food) // not the snake
            {
               (SnakeGame.GetEntity(x, y) as Food).Kill();
                eaten++;
                if (eaten % 3 == 0)
                {
                    
                        body.Insert(0, new Entity(body[0].x, body[0].y , '@'));
                }
            }
            
            for (int i = 0; i< body.Count-1; i++)
            {
                body[i].x = body[i + 1].x;
                body[i].y = body[i + 1].y;
            }
            body[body.Count - 1].x = x;
            body[body.Count - 1].y = y;
            lastDir = dir;
        }

        private static bool Opposite(Direction dir1 , Direction dir2)
        {
            return Math.Abs(dir1- dir2) == 1;
        }

        public void Draw()
        {
            foreach (Entity entity in body)
            {
                entity.Draw();
            }
        }

        public Entity GetCell(int x, int y)
        {
            foreach(Entity entity in body)
            {
                if (entity.x == x && entity.y == y)
                {
                    return entity;
                }
            }
            return null;
        }
        
    }

    public enum Direction
    {
        Right = 0,
        Left = 1,
        Up = 3,
        Down =4
    }
}
