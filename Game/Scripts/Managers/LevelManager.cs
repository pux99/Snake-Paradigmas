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
        private static readonly LevelsManager instance = new LevelsManager();

        public static LevelsManager Instance { get { return instance; } }

        private Levels _currentLevel = null;

        public Levels CurrentLevel => _currentLevel;

        public void SetLevel(string levelName)
        {
            if (_currentLevel != null)
                _currentLevel.Reset();
            switch (levelName)
            {
                case "Menu":
                    _currentLevel = new Menu();
                    break;
                case "LevelSelector":
                    _currentLevel = new LevelSelector();
                    break;
                case "Level1":
                    _currentLevel = new Level1();
                    break;
                case "Level2":
                    _currentLevel = new Level2();
                    break;
                case "Level3":
                    _currentLevel = new Level3();
                    break;
                case "Options":
                    _currentLevel = new Options();
                    break;
                case "Defeat":
                    _currentLevel = new Defeat();
                    break;
                case "Victory":
                    _currentLevel = new Victory();
                    break;
                case "Skins":
                    _currentLevel = new Skins();
                    break;
                default:
                    Console.WriteLine("No nay nivel");
                    break;
            }
            Instance._currentLevel.Inizialize();
        }
    }
}
