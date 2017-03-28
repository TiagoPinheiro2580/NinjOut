using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjOut
{
    static class RectangleHelper
    {
        //r1 rectangle player, r2 rectangle "chão"
        //public static bool TouchTopOf(this Rectangle r1, Rectangle r2)
        //{
        //    return (r1.Bottom >= (r2.Top - 1) && r1.Bottom <= r2.Top + (r2.Height / 2) &&
        //        r1.Right >= r2.Left + (r2.Width / 5) && r1.Left <= r2.Right - (r2.Width / 5));
        //}
        //public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        //{
        //    return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
        //        r1.Top >= r2.Bottom - 1 &&
        //        r1.Right >= r2.Left + (r2.Width / 5) &&
        //        r1.Left <= r2.Right - (r2.Width / 2));
        //}
        //public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        //{
        //    return (r1.Right <= r2.Right &&
        //        r1.Right >= r2.Left - 5 &&
        //        r1.Top <= r2.Bottom - (r2.Width / 4) &&
        //        r1.Bottom >= r2.Top + (r2.Width / 4));
        //}
        //public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        //{
        //    return (r1.Left >= r2.Left &&
        //        r1.Left <= r2.Right + 5 &&
        //        r1.Top <= r2.Bottom - (r2.Width / 4) &&
        //        r1.Bottom >= r2.Top + (r2.Width / 4));
        //}

        public static bool TouchTopOf(this Rectangle r1, Rectangle r2) //pomos this para depois chamarmos rectangle.TouchTopOf(rectangle)
        {
            return (r1.Bottom >= (r2.Top - 1) &&
                r1.Bottom <= r2.Top + (r2.Height / 2) &&
                 r1.Right >= r2.Left + (r2.Width / 5) &&
                 r1.Left <= r2.Right - (r2.Width / 5));
            //return r1.Y+r1.Height >= r2.Y && (r1.Left < r2.Right && r1.Left > r2.Left || r1.Right > r2.Left && r1.Right < r2.Right);

        }

        public static bool TouchBottomOf(this Rectangle r1, Rectangle r2)
        {
            if (r1.Intersects(r2))
                return true;
            else
                return false;
        }

        public static bool TouchLeftOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left <= r2.Right &&
                    r1.Right >= (r2.Left - 5) &&
                    r1.Top <= r2.Bottom - (r2.Width / 4) &&
                    r1.Bottom >= r2.Top + (r2.Width / 4));
            /* return (r1.Right >= r2.Left &&
                    (r2.Bottom < r1.Top && r2.Bottom > r1.Bottom || r2.Top < r1.Top && r2.Top > r1.Bottom));*/

        }

        public static bool TouchRightOf(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                      r1.Left <= (r2.Right + 5) &&
                      r1.Top <= r2.Bottom - (r2.Width / 4) &&
                      r1.Bottom >= r2.Top + (r2.Width / 4));

            /* return r1.Left < r2.Right && r1.Left > r2.Left  &&
                      (isInInterval(r2.Y, r1.Y, r1.Y + r1.Height) ||
                       isInInterval(r2.Y + r2.Height, r1.Y, r1.Y + r1.Height));  */

        }
    }
}
