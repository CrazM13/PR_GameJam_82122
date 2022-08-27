using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : HideableMenu {

	void Start() {
		Clock.OnPause.AddListener(ShowMenu);
		Clock.OnUnpause.AddListener(HideMenu);
	}

	public void Resume() {
		Clock.IsPaused = false;
	}
	public void QuitGame() {
		Application.Quit();
	}

}
