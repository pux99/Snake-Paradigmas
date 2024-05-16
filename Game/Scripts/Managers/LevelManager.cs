using Game.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class LevelsManager
    {
        private static LevelsManager instance = new LevelsManager();
        
        public static LevelsManager Instance { get { return instance; } }

        private Dictionary<string, Levels> levels = new Dictionary<string, Levels>();

        private Levels currentLevel = null;


        public LevelsManager()
        {
            levels.Clear();
            AddNewLevel("Menu", new Menu());
            AddNewLevel("Gameplay", new Gameplay());


            SetLevel("Menu");
        }

        public Levels CurrentLevel => currentLevel;

        public void SetLevel(string levelName)
        {
           if(levels.TryGetValue(levelName, out var l_currentLevel))
           {
               currentLevel = l_currentLevel;
                currentLevel.Reset();
           } 
           else 
           {
                Console.WriteLine("No nay nivel"); 
           }
        }

        public void AddNewLevel(string levelName, Levels level)
        {
            levels.Add(levelName, level);
        }
    }
}
