using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Wall : Draw
    {
        protected Render _render;
        public Render render { get { return _render; } }
        public Transform transform;
        public bool active {  get; set; }
       public Wall(Transform transform, string path)
        {
            active = true;
            this.transform = transform;
            _render = new Render(path, transform);
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }
        public void Draw()
        {
            render.Draw(transform);
        }

    }
}
