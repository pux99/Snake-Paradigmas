using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Scripts.FruitFactory;

namespace Game.Scripts
{
   public static class FruitFactory
    {
        public enum fruit {apple, rottenApple}
        public static IPickable CreateFruit (fruit fruit, Vector2 position)
        {
            switch (fruit)
            {
                case fruit.apple:
                    return new Fruit((int)position.x, (int)position.y, .3125f, .3125f, "Sounds/Munching.wav", 1);
                case fruit.rottenApple:
                    return new Trash((int)position.x, (int)position.y, .03125f, .03125f);
                default:
                    break;
            }
            return null;
        }
    }
    public static class SnakeFactory
    {
        public enum part { snakePart, snakeHead }
        public static Draw CreateSnake(part snake, Vector2 position)
        {
            switch (snake)
            {

                case part.snakePart:
                    switch(GameManager.Instance.snackBody)
                    {
                        case 0:
                            return new SnakePart(position.x, position.y, 1, "Sprites/Body_1.png");   
                        case 1:
                            return new SnakePart(position.x, position.y, 1, "Sprites/Body_2.png");     
                        case 2:
                            return new SnakePart(position.x, position.y, 1, "Sprites/Body_3.png");     
                        default: break;
                    }
                    break;
                case part.snakeHead:
                    switch (GameManager.Instance.snackHead)
                    {
                        case 0:
                            return new SnakePart(position.x, position.y, 1, "Sprites/Head_1.png");
                        case 1:
                            return new SnakePart(position.x, position.y, 1, "Sprites/Head_2.png");
                        case 2:
                            return new SnakePart(position.x, position.y, 1, "Sprites/Head_3.png");
                        default: break;
                    }
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
