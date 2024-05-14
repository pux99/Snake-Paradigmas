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
        private static Stopwatch stopwatch=new Stopwatch();
        private static float previousElapsedTime = 0f;

        //public  MyDeltaTimer()
        //{
        //    stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    previousElapsedTime = 0f;
        //}

        public static float DeltaTime()//pregunrtar si esta bien?
        {
            stopwatch.Start();
            float currentTime = (float)stopwatch.Elapsed.TotalSeconds;
            float deltaTime = currentTime - previousElapsedTime;
            previousElapsedTime = currentTime;
            return deltaTime;
        }
    }
}
