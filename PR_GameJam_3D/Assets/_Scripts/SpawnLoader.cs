using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoader : MonoBehaviour
{
    public SpawnableObject[] objLoad;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < objLoad.Length; i++)
        {
            SpawnRegistry.RegObj(objLoad[i]);

        }


    }
}
