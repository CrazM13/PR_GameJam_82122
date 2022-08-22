using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableAnt : CarriableObject {

	[SerializeField] private GameObject groupAntPrefab;

	public override void AttemptPickUpBy(GroupController group) {

		GameObject newAnt = Instantiate(groupAntPrefab, transform.position, transform.rotation, group.transform.parent);

		AntController newAntController = newAnt.GetComponent<AntController>();
		if (newAntController) group.AddAnt(newAntController);

		Destroy(gameObject);
		CanPickUp = false;
	}

}
