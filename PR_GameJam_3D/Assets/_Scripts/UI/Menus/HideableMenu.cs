using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideableMenu : MonoBehaviour {

	[SerializeField] private CanvasGroup canvas;

	public void ShowMenu() {
		canvas.alpha = 1;
		canvas.interactable = true;
	}

	public void HideMenu() {
		canvas.alpha = 0;
		canvas.interactable = false;
	}

}
