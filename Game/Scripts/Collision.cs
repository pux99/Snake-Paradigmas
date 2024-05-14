using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Collision
    {
        public static bool RectRect(float rect1X, float rect1Y, float rect1Size, float rect2X, float rect2Y, float rect2Size)
        {
            return (rect1X < rect2X + rect2Size &&
                    rect1Y < rect2Y + rect2Size &&
                    rect1X + rect1Size > rect2X &&
                    rect1Y + rect1Size > rect2Y);
        }
    }
}
