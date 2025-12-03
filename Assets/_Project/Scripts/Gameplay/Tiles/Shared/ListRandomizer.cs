using System.Collections.Generic;
using UnityEngine;

public class ListRandomizer
{
    private const int MIN_INDEX = 0;
    private const int RANGE_INCLUSIVE_OFFSET = 1;

    public void Shuffle<T>(List<T> list)
    {
        if (list == null)
            return;

        int count = list.Count;

        for (int index = count - 1; index > MIN_INDEX; index--)
        {
            int randomIndex = Random.Range(MIN_INDEX, index + RANGE_INCLUSIVE_OFFSET);
            (list[index], list[randomIndex]) = (list[randomIndex], list[index]);
        }
    }
}