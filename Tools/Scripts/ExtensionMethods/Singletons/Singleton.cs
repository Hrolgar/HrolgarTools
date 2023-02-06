using UnityEngine;
using UnityEngine;

namespace hrolgarUllr.ExtensionMethods.Singletons
{
    /// <summary>
    /// This is a singleton that takes in the MonoBehaviour
    /// <code>
    /// <example>
    /// public class YourClass : SingletonMonoBehaviour<YourClass> 
    /// </example>
    /// </code>
    /// </summary>
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
