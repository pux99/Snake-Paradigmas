using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Start
    {
        public Start() { }

        public static void start(Snake snake)
        {
            for (int i = 0; i < 6; i++)
                snake.snake.Add(new SnakePart(50, 500, 10,snake));
        }
    }
}
