using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public abstract class CarriableObject : MonoBehaviour {

	[SerializeField] private GameObject pickupEffect;
	[SerializeField] private int requiredNumberOfUnits = 1;
	NavMeshObstacle obstacle;

	public bool CanPickUp { get; set; } = true;

	private void Start() {
		obstacle = GetComponentInChildren<NavMeshObstacle>();

		ServiceLocator.Instance.GroupController.OnAntAdded.AddListener(UpdateObsticle);
		ServiceLocator.Instance.GroupController.OnPickUpCarriable.AddListener(UpdateObsticle);

		OnStartGame();
	}

	public bool CanBeCarriedBy(GroupController group) {
		return CanPickUp && requiredNumberOfUnits <= group.GetRemainingUnitCount();
	}

	public void AttemptPickUpBy(GroupController group) {
		PickUpBy(group);
		group.OnAntAdded.RemoveListener(UpdateObsticle);
		group.OnPickUpCarriable.RemoveListener(UpdateObsticle);
		obstacle.enabled = false;
		CanPickUp = false;

		GameObject newEffect = Instantiate(pickupEffect, transform.position + Vector3.up, Quaternion.identity);
		ParticleSystem particles = newEffect.GetComponent<ParticleSystem>();
		if (particles) particles.Play();
		Destroy(newEffect, 5f);
	}

	protected abstract void PickUpBy(GroupController group);

	protected virtual void OnStartGame() { /* MT */ }

	public int CarryRequirement => requiredNumberOfUnits;

	private void UpdateObsticle(GroupController group) {
		if (CanBeCarriedBy(group)) {
			obstacle.enabled = false;
		} else {
			obstacle.enabled = true;
		}
	}

}
