using System;
using System.Collections.Generic;
using System.IO;
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
        private Render _render;
        public Render render { get { return _render; } }

        public Button(Transform t_transform,string path)
        {
            active = true;
            this._transform = t_transform;
            _transform.position.x -= 10;
            _transform.position.y -= 5;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
            _render = new Render(path, transform);
        }

        public void Draw()
        {
            render.Draw(transform);
        }
    }
}


