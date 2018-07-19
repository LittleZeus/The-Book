using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Book
{
    static class Extensions
    {
        // Convert 2D Vector to direction angle
        public static float ToAngle(this Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        // Convert 2D Vector to Point
        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        // Generate random number between minValue and maxValue using rand
        public static float NextFloat(this Random rand, float minValue, float maxValue)
        {
            return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        // Generate random vector of length between minLengh
        public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
        {
            double theta = rand.NextDouble() * 2 * Math.PI;
            float length = rand.NextFloat(minLength, maxLength);
            return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
        }

        // Convert 2 Vector2 points to Rectangle
        public static Rectangle toRect(Vector2 a, Vector2 b)
        {
            //we need to figure out the top left and bottom right coordinates
            //we need to account for the fact that a and b could be any two opposite points of a rectangle, not always coming into this method as topleft and bottomright already.
            int smallestX = (int)Math.Min(a.X, b.X); //Smallest X
            int smallestY = (int)Math.Min(a.Y, b.Y); //Smallest Y
            int largestX = (int)Math.Max(a.X, b.X);  //Largest X
            int largestY = (int)Math.Max(a.Y, b.Y);  //Largest Y

            //calc the width and height
            int width = largestX - smallestX;
            int height = largestY - smallestY;

            //assuming Y is small at the top of screen
            return new Rectangle(smallestX, smallestY, width, height);
        }
    }
}
