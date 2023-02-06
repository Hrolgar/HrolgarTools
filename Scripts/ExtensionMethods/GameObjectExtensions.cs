using UnityEngine;

namespace hrolgarUllr.ExtensionMethods
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// This extension method provides a convenient way to get or add a component of type T to a UnityEngine.GameObject.
        /// If the component of type T already exists on the GameObject, it returns the component,
        /// otherwise it adds a new component of type T to the GameObject and returns it.
        /// </summary>
        public static T GetOrAddComponent<T> (this GameObject obj) where T : Component =>  
            obj.TryGetComponent(out T component) == false ? obj.AddComponent<T>() : component;
        
        /// <summary>
        /// Determines if a GameObject has a specific component of type T.
        /// </summary>
        /// <param name="gameObject">The GameObject to check for the component.</param>
        /// <typeparam name="T">The type of component to check for.</typeparam>
        /// <returns>True if the GameObject has the component, False otherwise.</returns>
        public static bool HasComponent<T>(this GameObject gameObject) where T : MonoBehaviour => gameObject.GetComponent<T>() != null;
        
        

    }
}
