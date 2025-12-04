using System.Collections.Generic;
using UnityEngine;

public class ListRandomizer
{
	private const int MIN_INDEX = 0;

	private const int RANGE_INCLUSIVE_OFFSET = 1;

	public void Shuffle<T>(List<T> list)
	{
		if (list != null)
		{
			int count = list.Count;
			for (int index = count - 1; index > 0; index--)
			{
				int randomIndex = Random.Range(0, index + 1);
				int index2 = index;
				int index3 = randomIndex;
				T val = list[randomIndex];
				T val2 = list[index];
				T val4 = (list[index2] = val);
				val4 = (list[index3] = val2);
			}
		}
	}
}
