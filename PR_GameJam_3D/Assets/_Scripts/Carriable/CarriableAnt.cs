using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableAnt : CarriableObject {

	[SerializeField] private GameObject groupAntPrefab;

	protected override void PickUpBy(GroupController group) {

		GameObject newAnt = Instantiate(groupAntPrefab, transform.position, transform.rotation, group.transform.parent);

		AntController newAntController = newAnt.GetComponent<AntController>();
		if (newAntController) {
			group.AddAnt(newAntController);
			//newAntController.AdjustSpeed(Random.Range(0.25f, 1.75f));
		}

		Destroy(gameObject);
	}

}
