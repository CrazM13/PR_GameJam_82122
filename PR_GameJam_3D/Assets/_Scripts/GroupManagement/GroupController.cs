using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GroupController : MonoBehaviour {

	[Header("References")]
	[SerializeField] private GroupVisualizer groupVisualizer;
	[SerializeField] private CarriableObject startingAnt;
	[SerializeField] private TMP_Text text;
	[Header("Settings")]
	[SerializeField] private bool showAntCountAlways = false;
	[SerializeField] private float audioInterval;

	private float timeUntilAudio = 0;


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

		text.transform.position = (6 * Vector3.up) + transform.position;

		AttemptPickupObjects();

		if (timeUntilAudio > 0) {
			timeUntilAudio -= Clock.DeltaTime;
		}
	}

	public void AddAnt(AntController ant) {
		groupedAnts.Add(ant);
		groupVisualizer.Radius = Mathf.Sqrt(groupedAnts.Count);
		groupVisualizer.RecalculatePositions(groupedAnts.Count);
		if (showAntCountAlways || carriedObjects.Count > 0) {
			text.alpha = 1;
			text.text = GetRemainingUnitCount().ToString();
		}
		if (timeUntilAudio <= 0) {
			ServiceLocator.Instance.Audio.PlayJoin();
			timeUntilAudio += audioInterval;
		}
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
				if (carriedObjects.Count > 0) {
					text.alpha = 1;
					text.text = GetRemainingUnitCount().ToString();
				}
			}
		}
	}

	public void CarryObject(CarriableObject @object) {
		carriedObjects.Add(@object);
		if (timeUntilAudio <= 0) {
			ServiceLocator.Instance.Audio.PlayPickUp();
			timeUntilAudio += audioInterval;
		}
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
		text.text = GetRemainingUnitCount().ToString();
		if (!showAntCountAlways) text.alpha = 0;
	}
}
