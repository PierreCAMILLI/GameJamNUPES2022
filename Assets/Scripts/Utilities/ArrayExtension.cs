using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension
{
    public static T Random<T>(this IList<T> array)
    {
        return array[UnityEngine.Random.Range(0, array.Count)];
    }
}
