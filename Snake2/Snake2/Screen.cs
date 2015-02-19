using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class Screen
    {
        public int width;
        public int height;
        private char[,] screen;
        public Screen(int width, int height)
        {
            Init(width , height);
        }

        public void Init(int width, int height)
        {
            this.width = width;
            this.height = height;
            screen = new char[height, width];
            ClearScreen();
            
        }

        public void DrawToScreen(int x, int y, char sign)
        {
            if (!OutOfBorder(x, y))
                screen[y, x] = sign;
        }

        public bool OutOfBorder(int x , int y)
        {
            return ((x < 0) || (y < 0) || (x >= width) || (y >= height));
        }

        public string GetScreen()
        {
            string screenStr = "";
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    screenStr += screen[y, x];
                }
                screenStr += Environment.NewLine;
            }
            return screenStr;
        }

        public void ClearScreen()
        {
            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    screen[y, x] = '.';
                }

            }
        }
    }
}
