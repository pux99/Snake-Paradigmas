using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Level : GameObject
    {
        public Level() {
        GameManager.Instance.Update.Add(this);
        }
        public override void Update()
        {
            
        }
    }

    public class Level1 : Level
    {
        //public Snake mySnakea = new Snake(50, 1);
        //public static int[,] grid = new int[50, 50];
        //public static List<PickUP> fruits = new List<PickUP>();
        //
        //public static Buttons Play = new Buttons(new Transform(30, 30, 0, 5, 5), "1234567890", "start the game");
        public override void Start() 
        {
            //Play.draw();
            //for (int i = 0; i < 6; i++)
               // mySnake.snake.Add(new SnakePart(50, 500, 1, mySnake));
            //GameManager.Instance.sprites.Add(new Sprite("Sprites/rect4.png", 10, 20, 4, 10, 10, 0, 0, 3));
            //GameManager.Instance.sprites.Add(new Sprite("Sprites/green.png", 10, 20, 4, 10, 10, 0, 0, 1));
            //fruits.Add(new Fruit(250, 300, 10));
        }
    }


    
}
