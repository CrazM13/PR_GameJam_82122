using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Clock {
	
	public static bool IsPaused { get; set; }
	public static float DeltaTime => IsPaused ? 0 : Time.deltaTime;

}
