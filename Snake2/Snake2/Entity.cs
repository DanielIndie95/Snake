using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class Entity
    {
        public int x;
        public int y;
        public char sign;

        public Entity(int x, int y, char sign)
        {
            this.x = x;
            this.y = y;
            this.sign = sign;
        }

        public virtual void Draw()
        {
            SnakeGame.screen.DrawToScreen(this.x, this.y, this.sign);
        }

        public virtual void Update()
        {

        }

        public virtual void Kill()
        {
            
        }

    }

    public class Food : Entity
    {
        public int value;
        
            public Food(int value , int x, int y , char sign): base(x,y,sign)
            {
                this.value = value;
            }

            public override void Kill()
            {
                base.Kill();
                SnakeGame.RemoveFood(this);
                SnakeGame.AddPoints(this);
            }
    }

    public class SpecialFood : Food
    {
        float real;
        public SpecialFood(int value , int x, int y , char sign) : base(value , x,y,sign)
        {
            real = value;
        }

        public override void Update()
        {
            base.Update();
            real -= 0.5f;
            this.value = (int)real;
            if (value <= 0)
            {
                Kill();

            }
        }
    }
}
