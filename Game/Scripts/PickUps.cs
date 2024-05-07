using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PickUP
    {
        public Transform transform;
        //public int x;
        //public int y;
        //public int size = 10;
        public bool state = true;
        private string _texture = "Sprites/apple.png";
        
        //public PickUP(int x, int y, int scale)
        //{
        //    transform.positon.x = x;
        //    transform.positon.y=y;
        //    transform.scale.x = scale;
        //}
        public void effect()
        {

        }
        public void ChangeToRandomPosition()
        {
            Random rndY = new Random();
            Random rndX = new Random();

            transform.positon.x = rndX.Next(10, 490);
            transform.positon.y = rndY.Next(10, 490);
        }
        public void TurnOnOff(bool value)
        {
            state = value;
        }
        public void Draw()
        {
            Engine.Draw(_texture, transform.positon.x, transform.positon.y, .31f, .31f);
        }

    }
    public class Fruit : PickUP
    {
        public Fruit(int x, int y, int scale)
        {
            transform.positon.x = x;
            transform.positon.y = y;
            transform.scale.x = scale;
        }

    }
}
