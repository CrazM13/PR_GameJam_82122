using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnRegistry 
{
    private static List<SpawnableObject> regObj = new List<SpawnableObject>();
    public static void RegObj(SpawnableObject spawnable)
    {
        regObj.Add(spawnable);

        
    }

    public static SpawnableObject GetRanObj()
    {
        List<int> indecies = new List<int>();

        for (int i = 0; i < regObj.Count; i ++)
        {
            for(int j = 0; j < regObj[i].weight; j++)
            {
                indecies.Add(i);    
            }
        }

        return regObj[indecies[(int)(Random.value * indecies.Count)]];
    }
}
