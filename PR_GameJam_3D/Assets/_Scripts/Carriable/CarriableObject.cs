using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarriableObject : MonoBehaviour {

	[SerializeField] private int requiredNumberOfUnits = 1;

	public bool CanPickUp { get; set; } = true;

	public bool CanBeCarriedBy(GroupController group) {
		return CanPickUp && requiredNumberOfUnits <= group.GetRemainingUnitCount();
	}

	public abstract void AttemptPickUpBy(GroupController group);

	public int CarryRequirement => requiredNumberOfUnits;

}
