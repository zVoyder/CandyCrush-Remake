namespace Extension.Vectors
{
    using UnityEngine;

    public static class Vector2Extension
    {
        public static Vector2 Invert(this Vector2 v)
        {
            return new Vector2(v.y, v.x);
        }

        public static Vector2 Sum(this Vector2 vector, float n)
        {
            return new Vector2(vector.x + n, vector.y + n);
        }
    }
}