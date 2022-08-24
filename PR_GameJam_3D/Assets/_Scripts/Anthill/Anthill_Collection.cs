using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill_Collection : MonoBehaviour
{
    // Start is called before the first frame update
    public GroupController group = ServiceLocator.Instance.GroupController;
    [SerializeField] Over_World_Timer timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(this.transform.position,new Vector3(1,1,1));
        foreach(Collider collider in colliders)
        {
            GroupController groupController = collider.GetComponent<GroupController>();
            if (groupController)
            {
                timer.incrementTimer((group.GetCarryObjectScore()*5));
                group.ClearCarry();
            }
        }
    }
}
