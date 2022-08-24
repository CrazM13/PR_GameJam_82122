using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMP_PosInGrid : MonoBehaviour {

	[SerializeField] float distance;

	
	[ContextMenu("Move Objects")]
	void Move() {
		int totalObjects = transform.childCount;

		int sideLength = (int)Mathf.Sqrt(totalObjects);

		for (int x = 0, i = 0; i < totalObjects; x++) {
			for (int y = 0; y < sideLength && i < totalObjects; y++, i++) {
				transform.GetChild(i).localPosition = new Vector3(x * distance, 0, y * distance);
			}
		}
	}
}
