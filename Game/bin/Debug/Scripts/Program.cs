using Game.Scripts;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Windows.Forms.DataVisualization.Charting;

namespace Game
{
    public delegate void VoidEvent();
    public class Program
    {
        static void Main(string[] args)
        {
            Engine.Initialize("Snake",500,500);
            LevelsManager.Instance.SetLevel("Menu");
            while (true)
            {
                MyDeltaTimer.CalcDeltaTime();
                Input();
                Update();//todo: update
                Render();
            }
        }
        static void Render()
        {
            Engine.Clear();
            Drawing();
            Engine.Show();
        }
        static void Drawing()
        {
            LevelsManager.Instance.CurrentLevel.Draw();
        }
        static void Update()
        {
            LevelsManager.Instance.CurrentLevel.Update();
        }
        static void Input()
        {
            LevelsManager.Instance.CurrentLevel.Input();
        }
    }
    

}
    
    
