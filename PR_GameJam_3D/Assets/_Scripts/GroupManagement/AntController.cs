using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	[SerializeField] private float turnSpeed;
	[SerializeField] private float speed;

	private Vector3 targetPosition;

	public float Speed => speed;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (targetPosition != transform.position) {
			UpdateRotation();
			UpdateMovement();
		}
	}

	private void UpdateRotation() {
		var lookPos = targetPosition - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Clock.DeltaTime * turnSpeed);
	}

	private void UpdateMovement() {
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Clock.DeltaTime * speed);
	}

	public void SetTargetDestination(Vector3 position) {
		targetPosition = new Vector3(position.x, 0, position.z);
	}
}
