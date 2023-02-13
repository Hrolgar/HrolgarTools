using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers
{
    public static class UnsortedHelpers
    {
        private static Camera _camera;
        private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new();
        private static PointerEventData _eventDataCurrentPosition;
        private static List<RaycastResult> _raycastResults;
        
        /// <summary>
        /// The main camera in the scene, cached.
        /// </summary>
        /// <returns>The main camera in the scene.</returns>
        public static Camera Camera => _camera ??= Camera.main;
        
        /// <summary>
        /// Gets a WaitForSeconds object for the specified amount of seconds, cached.
        /// </summary>
        /// <param name="seconds">The amount of seconds to wait.</param>
        /// <returns>A WaitForSeconds object for the specified amount of seconds.</returns>
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (WaitDictionary.TryGetValue(seconds, out var wait)) return wait;
            WaitDictionary[seconds] = new WaitForSeconds(seconds);
            return WaitDictionary[seconds];
        }
        
        /// <summary>
        /// Determines if the mouse is currently over a UI element.
        /// </summary>
        /// <returns>True if the mouse is over a UI element, false otherwise.</returns>
        public static bool IsOverUi()
        {
            _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            _raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_eventDataCurrentPosition, _raycastResults);
            return _raycastResults.Count > 0;
        }

        /// <summary>
        /// Gets the world position of a Canvas element.
        /// </summary>
        /// <param name="element">The RectTransform of the Canvas element.</param>
        /// <returns>The world position of the Canvas element.</returns>
        public static Vector2 GetWoldPositionOfCanvasElement (RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
            return result;
        }
    }
}
