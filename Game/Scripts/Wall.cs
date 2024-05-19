using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Wall : Draw
    {
        public Transform transform;
        private string texture;

        public bool active {  get; set; }
       public Wall(Transform transform, string texture)
        {
            active = true;
            this.transform = transform;
            this.texture = texture;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }
        public void Draw()
        {
            Engine.Draw(texture, transform.positon.x, transform.positon.y, transform.scale.x, transform.scale.y);
        }

    }
}
