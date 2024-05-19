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
        private string _texture = "Sprites/grey.png";
        private Texture _textures;
        private Vector2 _imgSize;
        public Vector2 imgSize { get { return _imgSize; } }

        public Button(Transform t_transform)
        {
            active = true;
            this._transform = t_transform;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
            _textures = new Texture(_texture);
            _imgSize = new Vector2(_textures.Width * transform.scale.x, _textures.Height * transform.scale.y);
        }

        public void Draw()
        {
            Transform tran = new Transform(_transform.positon.x - 10, _transform.positon.y - 5, _transform.rotation, _transform.scale.x, _transform.scale.y);
            Engine.Draw(_textures, tran.positon.x, tran.positon.y, tran.scale.x, tran.scale.y, tran.rotation);
        }
    }
}


