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
        public bool active { get; set; }
        protected Render _render;
        public Render render { get { return _render; } }


        public PickUP(int x, int y, float SizeX ,float SizeY,string path)
        {
            active = true;
            transform.position.x = x;
            transform.position.y=y;
            transform.scale.x = SizeX;
            transform.scale.y = SizeY;
            _render=new Render(path, transform);
            LevelsManager.Instance.CurrentLevel.draws.Add(this);
        }
        public void ChangeToRandomPosition()
        {
            Random rndY = new Random();
            Random rndX = new Random();
            transform.position.x = rndX.Next(10, 490);
            transform.position.y = rndX.Next(10, 490);
        }

        public void Draw()
        {
            render.Draw(transform);
        }

    }
    public class Trash : PickUP,Update, IPickable
    {
        public float toActivaTimer=1;
        public Trash(int x, int y, float SizeX, float SizeY) : base(x, y, SizeX, SizeY, "Sprites/trash_apple.png")
        {
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
        }
        public void Update()
        {
            if (!active)
            {
                toActivaTimer -= MyDeltaTimer.deltaTime;
                if(toActivaTimer < 0)
                {
                    active = true;
                }
            }
        }
        public void PickAp() { }
    }
    public class Fruit : PickUP, IPickable
    {
        public Fruit(int x, int y, float SizeX, float SizeY, string _direction, int _volume) :base(x,y,SizeX,SizeY, "Sprites/apple.png")
        {
            sound = new Sound(_direction, _volume);
        }
        private Sound sound;

        public void Reproducer()
        {
            SoundPlayer player = new SoundPlayer(this.sound.direction);
            player.Play();
        }
        public void PickAp() { }

    }
    public class Portal : PickUP, Update, IPickable
    {
        public float toActivaTimer = 1;
        public Portal(int x, int y, float SizeX, float SizeY) : base(x, y, SizeX, SizeY, "Sprites/Portal.png")
        {
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
        }
        public void Update()
        {
            if (!active)
            {
                toActivaTimer -= MyDeltaTimer.deltaTime;
                if (toActivaTimer < 0)
                {
                    active = true;
                }
            }
        }
        public void PickAp() { }
    }
    public class Reverse : PickUP, Update, IPickable
    {
        public float toActivaTimer = 1;
        public Reverse(int x, int y, float SizeX, float SizeY) : base(x, y, SizeX, SizeY, "Sprites/Switch.png")
        {
            LevelsManager.Instance.CurrentLevel.updates.Add(this);
        }
        public void Update()
        {
            if (!active)
            {
                toActivaTimer -= MyDeltaTimer.deltaTime;
                if (toActivaTimer < 0)
                {
                    active = true;
                }
            }
        }
        public void PickAp() { }
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
