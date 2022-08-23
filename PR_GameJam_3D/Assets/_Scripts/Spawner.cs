using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] int SpawnAttempts;
   [SerializeField] float range;
   
    
    private const int STARTINGY = 10;
   

    // Start is called before the first frame update
    void Start()
    {
        Cloud();
    }

    public void SpawnObject(GameObject fruit, Vector3 pos)
    {
        Instantiate(fruit, pos, Quaternion.identity);

    }

    public void Cloud()
    {
        for (int i = 0; i < SpawnAttempts; i++)
        {
            float x = Random.value;
            float y = Random.value;
            Vector3 point = new Vector3(x * range,STARTINGY,y * range);
        
            if(Physics.Raycast(point, Vector3.down, out RaycastHit hit, STARTINGY, LayerMask.GetMask("Ground")))
            {
                point = hit.point;

                SpawnableObject spawner = SpawnRegistry.GetRanObj();

                SpawnObject(spawner.spawnObject, point);
            }
        
        } 
    }
}
