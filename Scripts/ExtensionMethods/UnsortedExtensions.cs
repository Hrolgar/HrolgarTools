using UnityEngine;

namespace hrolgarUllr.ExtensionMethods
{
    public static class UnsortedExtensions
    {
        public static void Fade (this SpriteRenderer renderer, float targetAlpha)
        {
            var color = renderer.color;
            color.a = targetAlpha;
            renderer.color = color;
        }
        
        /// <summary>
        /// Destroys all children of a Transform.
        /// </summary>
        /// <param name="transform">The parent Transform.</param>
        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform child in transform) Object.Destroy(child.gameObject);
        }
    }
}
