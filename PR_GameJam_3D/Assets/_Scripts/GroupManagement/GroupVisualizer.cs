using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupVisualizer : MonoBehaviour {

	private const float PHI = 1.6180339887498948482045868343656f;

	public float Radius { get; set; }
	[SerializeField, Range(0, 2)] private int alpha;

	private List<Vector3> points = new List<Vector3>();

	public void RecalculatePositions(int count) {
		points.Clear();

		int boundryPointCount = Mathf.RoundToInt(alpha * Mathf.Sqrt(count));
		for (int i = 0; i < count; i++) {
			float distance = GetDistance(i, count, boundryPointCount);
			float theta = (2 * Mathf.PI * i) / Mathf.Pow(PHI, 2);

			Vector3 newPosition = Radius * new Vector3(distance * Mathf.Cos(theta), 0, distance * Mathf.Sin(theta));
			points.Add(newPosition);
		}
	}

	private float GetDistance(int index, int pointCount, int boundryPointCount) {
		if (index > pointCount - boundryPointCount) {
			return 1;
		} if (index < 1) {
			return 0;
		} else {
			return Mathf.Sqrt(index - 0.5f) / Mathf.Sqrt(pointCount - (boundryPointCount + 1) / 2f);
		}
	}

	public Vector3 GetLocalPosition(int index) {
		if (index < 0 || index >= points.Count) return Vector3.zero;

		return points[index];
	}

	public Vector3 GetWorldPosition(int index) {
		if (index < 0 || index >= points.Count) return transform.position;
		return transform.TransformPoint(GetLocalPosition(index));
	}

	private void OnDrawGizmosSelected() {
		Gizmos.DrawWireCube(transform.position, Vector3.one);

		foreach (Vector3 point in points) {
			Gizmos.DrawWireCube(transform.TransformPoint(point), Vector3.one);
		}

		Gizmos.DrawWireCube(transform.position, new Vector3(Radius * 2, Radius * 2, Radius * 2));
	}
}
