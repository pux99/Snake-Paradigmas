using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Button : Draw
    {
        private Transform _transform;
        public Transform transform { get { return _transform; } set { _transform = value; } }
        public bool active { get; set; }

        public Button(Transform t_transform)
        {
            active = true;
            this._transform = t_transform;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }

        public void Draw()
        {
            Transform tran = new Transform(_transform.positon.x - 10, _transform.positon.y - 5, _transform.rotation, _transform.scale.x, _transform.scale.y);
            Engine.Draw("Sprites/grey.png", tran.positon.x, tran.positon.y, tran.scale.x, tran.scale.y, tran.rotation);
        }
    }
}


