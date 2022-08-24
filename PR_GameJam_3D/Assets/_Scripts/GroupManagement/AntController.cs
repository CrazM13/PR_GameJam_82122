using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntController : MonoBehaviour {

	[SerializeField] private float turnSpeed;
	[SerializeField] private float speed;
	[SerializeField] private bool adjustableSpeed = false;

	private float pathLength;
	private Vector3 nextPosition;

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
		var lookPos = nextPosition - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Clock.DeltaTime * turnSpeed);
	}

	private void UpdateMovement() {
		transform.position = Vector3.MoveTowards(transform.position, nextPosition, Clock.DeltaTime * GetCurrentSpeed());

		if (Vector3.Distance(nextPosition, transform.position) < Clock.DeltaTime * speed) {
			if (pathPositions.Count > 0) nextPosition = pathPositions.Dequeue();
			else isPathing = false;
		}
	}

	public float GetCurrentSpeed() {
		if (!adjustableSpeed) return speed;

		if (!isPathing) return 0;

		if (pathLength > 1) return speed * pathLength;

		return speed;
	}

	public void SetTargetDestination(Vector3 position) {
		if (Vector3.Distance(position, transform.position) < Clock.DeltaTime * speed) return;

		pathPositions.Clear();
		nextPosition = transform.position;
		pathLength = 0;

		NavMeshPath path = new NavMeshPath();
		NavMesh.CalculatePath(transform.position, position, -1, path);
	
		if (path.corners.Length > 1) {
	
			for (int i = 1; i < path.corners.Length; i++) {
				Vector3 fixedPosition = new Vector3(path.corners[i].x, 0, path.corners[i].z);
	
				pathPositions.Enqueue(fixedPosition);
				pathLength += Vector3.Distance(path.corners[i - 1], path.corners[i]);
			}
	
			nextPosition = pathPositions.Dequeue();
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
