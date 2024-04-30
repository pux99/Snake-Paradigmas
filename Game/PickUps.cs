using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PickUP
    {
        public int x;
        public int y;
        public int size = 10;
        public bool state = true;
        private string _texture = "Sprites/apple.png";

        public void effect()
        {

        }
        public void ChangeToRandomPosition()
        {
            Random rndY = new Random();
            Random rndX = new Random();

            x = rndX.Next(10, 490);
            y = rndY.Next(10, 490);
        }
        public void TurnOnOff(bool value)
        {
            state = value;
        }
        public void Draw()
        {
            Engine.Draw(_texture, x, y, .31f, .31f);
        }

    }
    public class Fruit : PickUP
    {


    }
}
