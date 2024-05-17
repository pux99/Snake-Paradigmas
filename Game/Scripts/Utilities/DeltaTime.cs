using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    using System.Diagnostics;

    public static class MyDeltaTimer
    {
        public static float deltaTime = 0;
        static DateTime lastFrameTime = DateTime.Now;
        public static void CalcDeltaTime()
        {
            TimeSpan deltaSpan = DateTime.Now - lastFrameTime;
            deltaTime = (float)deltaSpan.TotalSeconds;
            lastFrameTime = DateTime.Now;
        }
    }
}
