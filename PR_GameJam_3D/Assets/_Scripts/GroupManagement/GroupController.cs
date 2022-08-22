using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupController : MonoBehaviour {

	[SerializeField] private GroupVisualizer groupVisualizer;
	[SerializeField] private GroupVisualizer carryVisualizer;
	[SerializeField] private CarriableObject startingAnt;

	private List<AntController> groupedAnts = new List<AntController>();
	private List<CarriableObject> carriedObjects = new List<CarriableObject>();

	// Start is called before the first frame update
	void Start() {
		startingAnt.AttemptPickUpBy(this);
	}

	// Update is called once per frame
	void Update() {
		for (int i = 0; i < groupedAnts.Count; i++) {
			groupedAnts[i].SetTargetDestination(groupVisualizer.GetWorldPosition(i));
		}

		for (int i = 0; i < carriedObjects.Count; i++) {
			carriedObjects[i].transform.position = transform.position + carryVisualizer.GetLocalPosition(i) + Vector3.up;
		}

		AttemptPickupObjects();
	}

	public void AddAnt(AntController ant) {
		groupedAnts.Add(ant);
		groupVisualizer.Radius = Mathf.Sqrt(groupedAnts.Count);
		groupVisualizer.RecalculatePositions(groupedAnts.Count);
	}

	public int GetRemainingUnitCount() {
		int carryReqs = 0;
		foreach (CarriableObject obj in carriedObjects) carryReqs += obj.CarryRequirement;

		return groupedAnts.Count - carryReqs;
	}

	public void AttemptPickupObjects() {
		Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(groupVisualizer.Radius, 0.25f, groupVisualizer.Radius));

		foreach (Collider c in colliders) {
			CarriableObject @object = c.GetComponent<CarriableObject>();
			if (@object && @object.CanBeCarriedBy(this)) {
				@object.AttemptPickUpBy(this);
			}
		}
	}

	public void CarryObject(CarriableObject @object) {
		carriedObjects.Add(@object);
		carryVisualizer.Radius = Mathf.Sqrt(carriedObjects.Count);
		carryVisualizer.RecalculatePositions(carriedObjects.Count);
	}
}
