using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake2
{
    public class SnakeCell : Entity 
    {
        public SnakeCell(int x, int y , char sign) : base(x,y,sign)
        {

        }

        public override void Update()
        {
            base.Update();
        }
    }
}
