using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public static class Collision
    {
        public static bool RectRect(int rect1X, int rect1Y, int rect1Size, int rect2X, int rect2Y, int rect2Size)
        {
            return (rect1X < rect2X + rect2Size &&
                    rect1Y < rect2Y + rect2Size &&
                    rect1X + rect1Size > rect2X &&
                    rect1Y + rect1Size > rect2Y);
        }
    }
}
