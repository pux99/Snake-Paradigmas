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
        private string _texture;
        private Texture _textures;
        private Vector2 _imgSize;
        public Vector2 imgSize { get { return _imgSize; } }
        public bool active {  get; set; }
       public Wall(Transform transform, string texture)
        {
            active = true;
            this.transform = transform;
            this._texture = texture;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
            _textures = new Texture(_texture);
            _imgSize = new Vector2(_textures.Width * transform.scale.x, _textures.Height * transform.scale.y);
        }
        public void Draw()
        {
            Engine.Draw(_texture, transform.positon.x, transform.positon.y, transform.scale.x, transform.scale.y);
        }

    }
}
