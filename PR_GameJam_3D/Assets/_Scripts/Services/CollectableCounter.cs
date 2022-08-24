using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCounter {

	public int TotalCollectables { get; set; }
	public int FoundCollectables { get; set; }

	public float Progress => (float) FoundCollectables / TotalCollectables;

	public bool IsComplete => FoundCollectables >= TotalCollectables;

	public void RegisterCollectable() {
		TotalCollectables++;
	}

	public void CollectCollectable() {
		FoundCollectables++;
	}

}
