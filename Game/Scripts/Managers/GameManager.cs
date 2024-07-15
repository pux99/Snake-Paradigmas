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
        public int SnakeDelay = 5;
        public int SnakeSpeed = 25;
        public bool leaveTrash=false;

        public int snackHead = 0;
        public int snackBody = 0;
    }
}
