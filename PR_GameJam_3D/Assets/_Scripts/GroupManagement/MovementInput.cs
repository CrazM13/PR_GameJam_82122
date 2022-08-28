using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour {

	[SerializeField] private AntController controller;

	private float idleTimer = 0;
	[SerializeField]  private float timeUntilIdle;


	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		UpdatePause();
		UpdateMouseMovement();
		UpdateKeybordMovement();

#if UNITY_EDITOR
		// Cheat Button
		if (Input.GetKeyDown(KeyCode.K)) ServiceLocator.Instance.FoodCollectables.CollectCollectable();
#endif

		idleTimer += Clock.DeltaTime;
		if (idleTimer > timeUntilIdle) {
			ServiceLocator.Instance.Audio.PlayIdle();
			idleTimer = 0;
		}
	}

	private void UpdateMouseMovement() {
		if (Input.GetMouseButton(0)) {
			Vector3? position = RaycastMouse();
			if (position.HasValue) controller.SetTargetDestination(position.Value);
		} else if (Input.GetMouseButtonDown(1)) {
			Vector3? position = RaycastMouse();
			if (position.HasValue) controller.QueueTargetDestination(position.Value);
		}
	}

	private void UpdateKeybordMovement() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if (x != 0 || y != 0) controller.SetTargetDestination(controller.transform.position + ((Vector3.forward * y) + (Vector3.right * x)));
	}

	private void UpdatePause() {
		if (Input.GetButtonDown("Pause")) {
			Debug.Log("Pause");
			Clock.IsPaused = !Clock.IsPaused;
		}
	}

	private Vector3? RaycastMouse() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground"))) {
			return hit.point;
		}

		return null;
	}
}
