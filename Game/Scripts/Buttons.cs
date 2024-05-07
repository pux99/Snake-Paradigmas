using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Buttons
    {
        public Transform transform;
        public string Text;
        public string action;
        public Buttons(Transform transform, string text, string action)
        {
            this.transform = transform;
            Text = text;
            this.action = action;
        }
        public void draw()
        {
            for (int i = 0;i< Text.Count(); i++)
            {
                Transform tran = new Transform(transform.positon.x + i * 15, transform.positon.y);
                GameManager.Instance.sprites.Add(new Sprite("Sprites/apple.png", tran, Vector2.cero(), 10));
               // Engine.Draw("Sprites/green.png",
               //             transform.positon.x + i * 5, transform.positon.y);
            }
        }
    }
}
