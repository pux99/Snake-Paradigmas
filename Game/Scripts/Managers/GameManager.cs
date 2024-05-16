using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameManager
    {
        private static readonly GameManager instance = new GameManager();
        public static GameManager Instance {  get { return instance; } }
        public int points = 0;
        public int lives = 3;
        public int currentLevel;
        public List<Sprite> sprites = new List<Sprite>();// porque tube que aser todos los structs publics para poeder hacer esto
        public List<Update> Update=new List<Update>();
    }
}
