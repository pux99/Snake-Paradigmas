using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Scripts
{
    public class Buttons:Draw
    {
        private Transform transform;
        private string _text;
        public bool active { get { return active; } }
        public Buttons(Transform transform, string text)
        {
            this.transform = transform;
            _text = text;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }

        public void Draw()
        {
            for (int i = 0;i< _text.Count(); i++)
            {
                Transform tran = new Transform(transform.positon.x + i * 50, transform.positon.y);
                Engine.Draw("Sprites/Caraters/" +_text[i].ToString()+".png", tran.positon.x,tran.positon.y,tran.scale.x,tran.scale.y);
            }
        }
    }
}
