using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace hrolgarUllr.ExtensionMethods
{
    public static class MyInvokeExtension
    {
        /// <summary>
        /// MyInvoke is a extension method that allows for the invocation of a UnityAction with a delay using a coroutine.
        /// </summary>
        /// <param name="Mono">The MonoBehaviour that will start the coroutine.</param>
        /// <param name="Action">The UnityAction to be invoked.</param>
        /// <example>this.MyInvoke(() => Debug.Log(""));</example>
        public static void MyInvoke (this MonoBehaviour Mono, UnityAction Action) =>
            Mono.StartCoroutine(MyInvokeRoutine(Action));

        /// <summary>
        /// MyInvoke is a extension method that allows for the invocation of a UnityAction with a specified delay using a coroutine.
        /// </summary>
        /// <param name="Mono">The MonoBehaviour that will start the coroutine.</param>
        /// <param name="Action">The UnityAction to be invoked.</param>
        /// <param name="Delay">The amount of time to wait before invoking the UnityAction.</param>
        /// <example>this.MyInvoke(() => Debug.Log(""), 2f);</example>
        public static void MyInvoke (this MonoBehaviour Mono, UnityAction Action, float Delay) =>
            Mono.StartCoroutine(MyInvokeRoutine(Action, Delay));

        /// <summary>
        /// MyInvoke is a extension method that allows for the invocation of two UnityActions with a specified delay using a coroutine.
        /// </summary>
        /// <param name="Mono">The MonoBehaviour that will start the coroutine.</param>
        /// <param name="Action1">The first UnityAction to be invoked.</param>
        /// <param name="Delay">The amount of time to wait before invoking the second UnityAction.</param>
        /// <param name="Action2">The second UnityAction to be invoked.</param>
        /// <example>this.MyInvoke(() => Debug.Log("Here we go!"), 2f, () => Debug.Log("We waited for 2 seconds!");</example>
        public static void MyInvoke (this MonoBehaviour Mono, UnityAction Action1, float? Delay, UnityAction Action2) =>
            Mono.StartCoroutine(MyInvokeRoutine(Action1, Delay, Action2));

        private static IEnumerator MyInvokeRoutine (UnityAction Action1 = null, float? Delay = null, [CanBeNull] UnityAction Action2 = null)
        {
            Action1?.Invoke();
            if (Delay != null) yield return new WaitForSecondsRealtime(Delay.Value);
            Action2?.Invoke();
        }
    }
}
