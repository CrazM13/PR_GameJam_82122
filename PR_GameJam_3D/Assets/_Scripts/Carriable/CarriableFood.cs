using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableFood : CarriableObject {

	public override void AttemptPickUpBy(GroupController group) {
		group.CarryObject(this);
		transform.SetParent(group.transform.parent);
		CanPickUp = false;
	}

}
