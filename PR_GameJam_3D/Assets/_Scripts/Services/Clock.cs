using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Clock {

	#region Events
	public static UnityEvent OnPause { get; private set; } = new UnityEvent();
	public static UnityEvent OnUnpause { get; private set; } = new UnityEvent();
	#endregion

	private static bool isPaused;
	public static bool IsPaused {
		get => isPaused;
		set {
			isPaused = value;
			if (isPaused) OnPause.Invoke();
			else OnUnpause.Invoke();
		}
	}

	public static float DeltaTime => IsPaused ? 0 : Time.deltaTime;

}
