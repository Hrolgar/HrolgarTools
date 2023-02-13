using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace hrolgarUllr.ExtensionMethods
{
    public static class VectorExtensions
    {
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
        
        public static void ResetTransform(this GameObject gameObject) => gameObject.transform.ResetTransform();
        
        public static Vector2 ToVector2(this Vector3 vector3) => new Vector2(vector3.x, vector3.y);
        
        public static Vector3 ToVector3(this Vector2 vector2) => new Vector3(vector2.x, vector2.y, 0);
        
        public static Vector2 SetX(this Vector2 vector2, float x) => new Vector2(x, vector2.y);
        public static Vector2 SetY(this Vector2 vector2, float y) => new Vector2(vector2.x, y);
        
        public static Vector3 SetX(this Vector3 vector3, float x) => new Vector3(x, vector3.y, vector3.z);
        public static Vector3 SetY(this Vector3 vector3, float y) => new Vector3(vector3.x, y, vector3.z);
        public static Vector3 SetZ(this Vector3 vector3, float z) => new Vector3(vector3.x, vector3.y, z);
        
        public static Vector2 DistanceBetween(this Vector2 vector2, Vector2 otherVector2) => otherVector2 - vector2;
        public static Vector3 DistanceBetween(this Vector3 vector3, Vector3 otherVector3) => otherVector3 - vector3;
        
        public static Vector3 Flat(this Vector3 vector3) => new(vector3.x, 0, vector3.z);
        public static Vector3 ToVector3Int(this Vector3 vector3) => new Vector3Int((int) vector3.x, (int) vector3.y, (int) vector3.z);
        
        public static Vector2 GetClosestVector2(this Vector2 vector2, IEnumerable<Vector2> otherVectors)
        {
            var array = otherVectors.ToArray();
            if (!array.Any()) throw new Exception("No vectors to compare to");
            var minDistance = Vector2.Distance(vector2, array[0]);
            var closestVector = array[0];
            for (var i = 1; i < array.Length; i++)
            {
                var distance = Vector2.Distance(vector2, array[i]);
                if (!(distance < minDistance)) continue;
                minDistance = distance;
                closestVector = array[i];
            }
            return closestVector;
        }
    }
}
