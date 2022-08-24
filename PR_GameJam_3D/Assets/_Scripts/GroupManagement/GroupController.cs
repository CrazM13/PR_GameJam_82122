using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupController : MonoBehaviour {

	[SerializeField] private GroupVisualizer groupVisualizer;
	[SerializeField] private CarriableObject startingAnt;

	private List<AntController> groupedAnts = new List<AntController>();
	private List<CarriableObject> carriedObjects = new List<CarriableObject>();

	public UnityEvent<GroupController> OnAntAdded { get; private set; } = new UnityEvent<GroupController>();
	public UnityEvent<GroupController> OnPickUpCarriable { get; private set; } = new UnityEvent<GroupController>();

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
			carriedObjects[i].transform.position = groupVisualizer.GetWorldPosition(i) + Vector3.up;
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
			CarriableObject @object = c.GetComponentInParent<CarriableObject>();
			if (@object && @object.CanBeCarriedBy(this)) {
				@object.AttemptPickUpBy(this);
			}
		}
	}

	public void CarryObject(CarriableObject @object) {
		carriedObjects.Add(@object);
	}

	public int GetCarryObjectScore() {
		int carryReqs = 0;
		foreach (CarriableObject obj in carriedObjects) carryReqs += obj.CarryRequirement;
		return carryReqs;
	}

	public void ClearCarry() {
		foreach (CarriableObject obj in carriedObjects) {
			if (obj is CarriableFood food) food.RemoveCarriableFood();
			Destroy(obj.gameObject);
		}
		carriedObjects.Clear();
	}
}
