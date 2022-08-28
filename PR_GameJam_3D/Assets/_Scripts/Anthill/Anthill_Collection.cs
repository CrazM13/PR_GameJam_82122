using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Anthill_Collection : MonoBehaviour
{
    // Start is called before the first frame update
    public GroupController group;
    [SerializeField] Over_World_Timer timer;
    public float distance;
    private int score;
    [SerializeField] ParticleSystem dropOff;
    void Start()
    {
        group = ServiceLocator.Instance.GroupController;
    }

    private void OnTriggerEnter(Collider other)
    {
        timer.incrementTimer(group.GetCarryObjectScore() * 5);
        group.ClearCarry();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position,5);
        foreach(Collider collider in colliders)
        {
            GroupController groupController = collider.GetComponent<GroupController>();
            if (groupController)
            {
                timer.incrementTimer(group.GetCarryObjectScore()*5);
                group.ClearCarry();
            }
        }

        distance = Vector3.Distance(this.transform.position, group.transform.position);
        if(distance <= 3 && (group.GetCarryObjectScore() > 0))
        {
            timer.incrementTimer(group.GetCarryObjectScore() * 5);
            score += group.GetCarryObjectScore() * 5;
            dropOff.Play();
            group.ClearCarry();
			ServiceLocator.Instance.Audio.PlayDropOff();

			if (ServiceLocator.Instance.FoodCollectables.IsComplete) {
				SceneController.LoadScene(1, 1, 2);
			}
        }
    }
    public int getScore()
    {
        return score;
    }
}
