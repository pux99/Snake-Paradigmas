using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    using System.Diagnostics;

    public class MyDeltaTimer
    {
        private Stopwatch stopwatch;
        private float previousElapsedTime;

        public MyDeltaTimer()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            previousElapsedTime = 0f;
        }

        public float DeltaTime()
        {
            float currentTime = (float)stopwatch.Elapsed.TotalSeconds;
            float deltaTime = currentTime - previousElapsedTime;
            previousElapsedTime = currentTime;
            return deltaTime;
        }
    }
}
