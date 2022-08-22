using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MonoBehaviour {

	[SerializeField] private AntController controller;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		UpdateMouseMovement();
		UpdateKeybordMovement();
	}

	private void UpdateMouseMovement() {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground"))) {
				controller.SetTargetDestination(hit.point);
			}
		}
	}

	private void UpdateKeybordMovement() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if (x != 0 || y != 0) controller.SetTargetDestination(controller.transform.position + ((Vector3.forward * y) + (Vector3.right * x)));
	}
}
