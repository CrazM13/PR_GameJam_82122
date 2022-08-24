using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer : MonoBehaviour
{
    public Transform targetObject;

    public Vector3 offset;
    public float smoothFactor = 0.5f;

    public bool lokAtTarget = false;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void LateUpdate()
    {
       
        

        Vector3 newPostion = targetObject.transform.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPostion, smoothFactor);

        if (lokAtTarget)
        {
            //transform.LookAt(targetObject);
            Vector3 dir = targetObject.position - transform.position;



            if (dir.x + dir.z != 0)
            {
                Quaternion storeRotation;
                Quaternion newRotation;

                storeRotation = transform.rotation;

               transform.LookAt(targetObject);
                newRotation = transform.rotation;
                

                transform.rotation = Quaternion.Slerp(storeRotation, newRotation, smoothFactor);
            }

        }



    }
}
