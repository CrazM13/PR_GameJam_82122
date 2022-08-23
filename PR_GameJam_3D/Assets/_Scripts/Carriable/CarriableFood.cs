using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableFood : CarriableObject {

	protected override void PickUpBy(GroupController group) {
		group.CarryObject(this);
		transform.SetParent(group.transform.parent);
	}

}
