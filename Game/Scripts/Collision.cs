using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Collision
    {
        public static bool RectRect(float rect1X, float rect1Y, float rect1Width, float rect1Height, float rect2X, float rect2Y, float rect2Width, float rect2Height)
        {
            return (rect1X < rect2X + rect2Width &&
                    rect1Y < rect2Y + rect2Height &&
                    rect1X + rect1Width > rect2X &&
                    rect1Y + rect1Height > rect2Y);
        }
    }
}
