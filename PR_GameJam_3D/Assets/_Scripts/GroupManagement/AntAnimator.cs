using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntAnimator : MonoBehaviour {

	[SerializeField] private Animator anim;
	[SerializeField] private AntController ant;

	private bool isMoving = false;


	// Update is called once per frame
	void Update() {
		float speed = ant.GetCurrentSpeed();

		if (speed > 0 && !isMoving) {
			isMoving = true;
			anim.SetBool("IsMoving", isMoving);
		} else if (speed == 0 && isMoving) {
			isMoving = false;
			anim.SetBool("IsMoving", isMoving);
		}
	}
}
