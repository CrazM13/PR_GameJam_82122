using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriableObsticle : CarriableObject {

	[SerializeField] private float destructionTime = 1f;
	private float timeUntilDestroyed = -1;

	protected override void PickUpBy(GroupController group) {
		timeUntilDestroyed = destructionTime;
	}

	private void Update() {
		if (timeUntilDestroyed > 0) {

			// Effect
			transform.position += Vector3.up * Time.deltaTime;
			transform.localScale *= 1 - Time.deltaTime;

			timeUntilDestroyed -= Time.deltaTime;
			if (timeUntilDestroyed <= 0) {
				Destroy(gameObject);
			}
		}
	}

}
