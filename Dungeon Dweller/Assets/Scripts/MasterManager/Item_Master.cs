using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Master : MonoBehaviour {

	private Player_Master playerMaster;

	public delegate void generalEventHandler(Transform inventory);
	public event generalEventHandler eventPickupAction;

	void OnEnable() {
		SetInitialReferences ();
	}

	void SetInitialReferences() {
		if (GameManager_References._player != null) {
			playerMaster = GameManager_References._player.GetComponent<Player_Master> ();
		}
	}

	public void callEventPickupAction(Transform inventory) {
		if (eventPickupAction != null) {
			eventPickupAction (inventory);
		}
	}
}
