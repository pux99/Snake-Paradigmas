using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PickUP:Draw
    {
        public Transform transform;
        public bool state = true;
        public bool active { get { return active; } }
        private string _texture = "Sprites/apple.png";
        
        public PickUP(int x, int y, int scale)
        {
            transform.positon.x = x;
            transform.positon.y=y;
            transform.scale.x = scale;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }
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
        public Fruit(int x, int y, int scale):base(x,y,scale)
        {
            //transform.positon.x = x;
            //transform.positon.y = y;
            //transform.scale.x = scale;
        }

    }
}
