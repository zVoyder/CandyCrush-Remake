namespace Extension.Colors
{
    using UnityEngine;

    public static class ColorExtension
    {
        /// <summary>
        /// Checks if two color are similar.
        /// </summary>
        /// <param name="a">Color A.</param>
        /// <param name="b">Color B.</param>
        /// <param name="tolerance">Tolerance value.</param>
        /// <returns>True if they are similar, False if they are not.</returns>
        public static bool ColorEquals(this Color a, Color b, float tolerance = 0.01f)
        {
            return Mathf.Abs(a.r - b.r) < tolerance
                && Mathf.Abs(a.g - b.g) < tolerance
                && Mathf.Abs(a.b - b.b) < tolerance
                && Mathf.Abs(a.a - b.a) < tolerance;
        }
    }
}