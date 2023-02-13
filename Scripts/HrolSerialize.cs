using System.Collections.Generic;
using UnityEngine;

namespace hrolgarUllr.Serialization
{
    public static class HrolSerialize 
    {
        /// <summary>
        /// Serialize object to json
        /// </summary>
        /// <param name="list"> List to serialize </param>
        /// <typeparam name="T"> Type of list </typeparam>
        /// <returns> Serialized list </returns>
        public static string SerializeList<T> (this List<T> list) => JsonUtility.ToJson(new SerializationWrapper<T>(list));

        // public static List<T> DeserializeList<T>(string json) 
        // {
        //     var wrapper = JsonUtility.FromJson<SerializationWrapper<T>>(json);
        //     return wrapper.items;
        // }
    }
    
    [System.Serializable]
    public class SerializationWrapper<T>
    {
        public List<T> items;

        public SerializationWrapper(List<T> list)
        {
            items = list;
        }
    }
}
