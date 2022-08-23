using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public abstract class CarriableObject : MonoBehaviour {

	[SerializeField] private int requiredNumberOfUnits = 1;
	NavMeshObstacle obstacle;

	private void Start() {
		obstacle = GetComponent<NavMeshObstacle>();

		ServiceLocator.Instance.GroupController.OnAntAdded.AddListener(UpdateObsticle);
		ServiceLocator.Instance.GroupController.OnPickUpCarriable.AddListener(UpdateObsticle);
	}

	public bool CanBeCarriedBy(GroupController group) {
		return requiredNumberOfUnits <= group.GetRemainingUnitCount();
	}

	public void AttemptPickUpBy(GroupController group) {
		PickUpBy(group);
		group.OnAntAdded.RemoveListener(UpdateObsticle);
		group.OnPickUpCarriable.RemoveListener(UpdateObsticle);
		obstacle.enabled = false;
	}

	protected abstract void PickUpBy(GroupController group);

	public int CarryRequirement => requiredNumberOfUnits;

	private void UpdateObsticle(GroupController group) {
		if (CanBeCarriedBy(group)) {
			obstacle.enabled = false;
		} else {
			obstacle.enabled = true;
		}
	}

}
