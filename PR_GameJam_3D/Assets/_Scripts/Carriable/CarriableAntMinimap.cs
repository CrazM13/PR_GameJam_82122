using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableAntMinimap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera minimapCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minimapCamera.transform.position = new Vector3(this.transform.position.x, minimapCamera.transform.position.y, this.transform.position.z);
    }
}
