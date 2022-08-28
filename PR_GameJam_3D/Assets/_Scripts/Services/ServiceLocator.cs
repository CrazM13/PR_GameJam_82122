using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour {
	// Readonly services
	public GroupController @GroupController { get; private set; }
	public CollectableCounter FoodCollectables { get; private set; }

	public AudioManager Audio { get; set; }

	// Singleton
	public static ServiceLocator Instance { get; private set; }

	private void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(this);
			return;
		}
		Instance = this;
		LocateServices();
	}

	protected virtual void LocateServices() {
		@GroupController = FindObjectOfType<GroupController>();
		FoodCollectables = new CollectableCounter();
		Audio = FindObjectOfType<AudioManager>();
	}
}
