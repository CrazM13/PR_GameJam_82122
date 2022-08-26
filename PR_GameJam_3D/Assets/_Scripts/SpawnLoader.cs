using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLoader : MonoBehaviour
{
    public SpawnableObject[] objLoad;
    public SpawnableObject[] antObjectLoad;
    public Spawner spawner;
        
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < objLoad.Length; i++)
        {
            SpawnRegistry.RegObj(objLoad[i]);

        }

        spawner.Cloud();
        SpawnRegistry.Clear();

        for (int i = 0; i < antObjectLoad.Length; i++)
        {
            SpawnRegistry.RegObj(antObjectLoad[i]);

        }

        spawner.Cloud();


    }
}
