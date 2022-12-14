using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarriableFood : CarriableObject {

	[SerializeField] private TMP_Text text;

	protected override void OnStartGame() {
		ServiceLocator.Instance.FoodCollectables.RegisterCollectable();
		text.text = CarryRequirement.ToString();
	}

	protected override void PickUpBy(GroupController group) {
		group.CarryObject(this);
		transform.SetParent(group.transform.parent);
		ShowText(false);
	}

	public void RemoveCarriableFood() {
		ServiceLocator.Instance.FoodCollectables.CollectCollectable();
	}

	public void ShowText(bool show) {
		text.alpha = show ? 1 : 0;
	}

}
