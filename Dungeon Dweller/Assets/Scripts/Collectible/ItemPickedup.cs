using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickedup : MonoBehaviour {

	private Item_Master itemMaster;
	private Player_Master playerMaster;

	public Transform playerInventory;
	public Transform weapon;

	void OnEnable() {
		SetInitialReferences ();
		itemMaster.eventPickupAction += pickupAction;
	}

	void OnDisable() {
		itemMaster.eventPickupAction -= pickupAction;
	}

	void SetInitialReferences() {
		itemMaster = GetComponent<Item_Master> ();
		playerMaster = GameObject.Find ("Hero").GetComponent<Player_Master> ();
	}

	void OnTriggerEnter(Collider playerCollider) {
		if (playerCollider.gameObject.name == "Hero") {
			checkItemPickupAttempt ();
			Destroy (gameObject);
		}
	}

	void checkItemPickupAttempt() {
		if (weapon.root.tag != GameManager_References._playerTag) {
			itemMaster.callEventPickupAction (playerInventory);
		}
	}

	void pickupAction(Transform transformParent) {
		weapon.SetParent (transformParent, false);
		weapon.localPosition = new Vector3 (-0.083f, 0.037f, 0);
		playerMaster.callEventInventoryChanged();
		weapon.gameObject.SetActive (false);
	}
}
