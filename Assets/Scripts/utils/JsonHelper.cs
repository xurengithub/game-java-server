using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }

    public static List<T> getJsonList<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        ListWrapper<T> wrapper = JsonUtility.FromJson<ListWrapper<T>> (newJson);
        return wrapper.list;
    }
    private class ListWrapper<T>{
        public List<T> list;
    }
}
