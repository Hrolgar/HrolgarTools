using UnityEngine;

namespace hrolgarUllr.ExtensionMethods.Singletons
{
    /// <summary>
    /// This class is a singleton MonoBehaviour, which means that there can only be one instance of it in the scene.
    /// </summary>
    /// <typeparam name="T">The class type of the singleton.</typeparam>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if (Instance is not null)
                Destroy(gameObject);
            Instance = this as T;
        }
    }
}
