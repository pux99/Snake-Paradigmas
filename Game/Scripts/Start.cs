using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    internal class Start
    {
        public Start() { }
        public static Buttons Play = new Buttons(new Transform(30,30,0,5,5), "1234567890", "start the game");
        public static void start(Snake snake,List<PickUP> fruits)
        {
            Play.draw();
            for (int i = 0; i < 6; i++)
                snake.snake.Add(new SnakePart(50, 500, 1,snake));
            //GameManager.Instance.sprites.Add(new Sprite("Sprites/rect4.png", 10, 20, 4, 10, 10, 0, 0, 3));
            //GameManager.Instance.sprites.Add(new Sprite("Sprites/green.png", 10, 20, 4, 10, 10, 0, 0, 1));
            fruits.Add(new Fruit(250, 300, 10));
        }
    }
}
