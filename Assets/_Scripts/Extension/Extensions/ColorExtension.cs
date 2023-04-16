namespace Extension.Colors
{
    using UnityEngine;

    public static class ColorExtension
    {
        public static bool ColorEquals(this Color a, Color b, float tolerance = 0.01f)
        {
            return Mathf.Abs(a.r - b.r) < tolerance
                && Mathf.Abs(a.g - b.g) < tolerance
                && Mathf.Abs(a.b - b.b) < tolerance
                && Mathf.Abs(a.a - b.a) < tolerance;
        }
    }
}