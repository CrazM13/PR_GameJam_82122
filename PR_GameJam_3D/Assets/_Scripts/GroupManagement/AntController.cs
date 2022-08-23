using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntController : MonoBehaviour {

	[SerializeField] private float turnSpeed;
	[SerializeField] private float speed;

	private Vector3 targetPosition;
	public float Speed => speed;

	private Queue<Vector3> pathPositions = new Queue<Vector3>();
	private bool isPathing = false;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (isPathing) {
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

		if (Vector3.Distance(targetPosition, transform.position) < Clock.DeltaTime * speed) {
			if (pathPositions.Count > 0) targetPosition = pathPositions.Dequeue();
			else isPathing = false;
		}
	}

	public void SetTargetDestination(Vector3 position) {
		if (Vector3.Distance(position, transform.position) < Clock.DeltaTime * speed) return;

		pathPositions.Clear();
		targetPosition = transform.position;
	
		NavMeshPath path = new NavMeshPath();
		NavMesh.CalculatePath(transform.position, position, -1, path);
	
		if (path.corners.Length > 1) {
	
			for (int i = 1; i < path.corners.Length; i++) {
				Vector3 fixedPosition = new Vector3(path.corners[i].x, 0, path.corners[i].z);
	
				pathPositions.Enqueue(fixedPosition);
			}
	
			targetPosition = pathPositions.Dequeue();
			isPathing = true;
		}
		
	}

	private void OnDrawGizmosSelected() {
		Vector3[] positions = pathPositions.ToArray();

		foreach (Vector3 pos in positions) {
			Gizmos.DrawSphere(pos, 1);
		}
	}
}
