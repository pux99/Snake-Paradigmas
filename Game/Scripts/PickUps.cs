using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PickUP:Draw
    {
        public Transform transform;
        public bool state = true;
        public bool active { get; set; }
        private string _texture = "Sprites/apple.png";
        private Texture _textures;
        private Vector2 _imgSize;
        public Vector2 imgSize { get { return _imgSize; } }


        public PickUP(int x, int y, float SizeX ,float SizeY)
        {
            active = true;
            transform.positon.x = x;
            transform.positon.y=y;
            transform.scale.x = SizeX;
            transform.scale.y = SizeY;
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
            _textures = new Texture(_texture);
            _imgSize = new Vector2(_textures.Width * transform.scale.x, _textures.Height * transform.scale.y);
            Console.WriteLine(imgSize.x.ToString()+" " +imgSize.y.ToString());
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
            Engine.Draw(_texture, transform.positon.x, transform.positon.y, transform.scale.x, transform.scale.y);
        }

    }
    public class Fruit : PickUP
    {
        public Fruit(int x, int y, float SizeX, float SizeY, string _direction, int _volume) :base(x,y,SizeX,SizeY)
        {
            sound = new Sound(_direction, _volume);
        }
        private Sound sound;

        public void Reproducer()
        {
            SoundPlayer player = new SoundPlayer(this.sound.direction);
            player.Play();
        }

    }
    public class Sound
    {
        private string _direction;
        public string direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        private int _volume;
        public Sound(string direction, int volume)
        {
            _direction = direction;
            _volume = volume;
        }
    }
}
