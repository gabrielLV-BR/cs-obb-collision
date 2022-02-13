using System;
using System.Numerics;

namespace OBBAlgorithm.utils
{
    public class VectorUtils
    {
        public static Vector2 Rotate(Vector2 vec, float angle)
        {
            float angleRad = angle * Globals.toRadians;
            float angleCos = MathF.Cos(angleRad);
            float angleSin = MathF.Sin(angleRad);

            return new Vector2(
                vec.X * angleCos - vec.Y * angleSin,
                vec.X * angleSin + vec.Y * angleCos
            );
        }

        public static float DotProduct(Vector2 vec1, Vector2 vec2)
        {
            return vec1.X * vec2.X + vec1.Y * vec2.Y;
        }

        public static Vector2 CrossProduct(Vector2 vec)
        {
            return new Vector2(vec.Y, -vec.X);
        }

        public static Vector2 Perpendicular(Vector2 v)
        {
            return new Vector2(v.Y, -v.X);
        }
    }

    public class MathUtils
    {
        public static bool Overlaps(double min1, double max1, double min2, double max2)
        {
            return isBetweenOrdered(min2, min1, max1) || isBetweenOrdered(min1, min2, max2);
        }

        static bool isBetweenOrdered(double val, double lowerBound, double upperBound)
        {
            return lowerBound <= val && val <= upperBound;
        }
    }
}