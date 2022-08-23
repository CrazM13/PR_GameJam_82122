using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SpawnableObject 
{
    [SerializeField] public GameObject spawnObject;
    [SerializeField] public int weight; 
    

    public SpawnableObject(GameObject spawnObject,int weight)
    {
        this.spawnObject = spawnObject;
        this.weight = weight;

    }

}
