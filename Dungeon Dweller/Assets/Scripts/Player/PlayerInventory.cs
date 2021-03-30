using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

	private Player_Master playerMaster;
	private GameManager_InventoryUI gameManagerInventoryUI;
	private float timeToPlaceInHand = 0.1f;
	private Transform currentlyHeldItem;
	private int counter;
	private string buttonText;
	private List<Transform> listInventory = new List<Transform> ();

	public Transform playerInventory;
	public Transform inventoryUI;
	public GameObject buttonUI;

	void OnEnable() {
		SetInitialReferences ();
		updateInventory ();
		playerMaster.eventInventoryChanged += updateInventory;
	}

	void OnDisable() {
		playerMaster.eventInventoryChanged -= updateInventory;
	}

	void SetInitialReferences() {
		gameManagerInventoryUI = GameObject.Find ("GameManager").GetComponent<GameManager_InventoryUI> ();
		playerMaster = GetComponent<Player_Master> ();
	}

	void updateInventory() {
		counter = 0;
		listInventory.Clear ();
		listInventory.TrimExcess ();
		clearInventoryUI ();

		foreach (Transform child in playerInventory) {
			if (child.CompareTag ("Item")) {
				listInventory.Add (child);
				GameObject gameObject = Instantiate (buttonUI) as GameObject;
				buttonText = child.name;
				gameObject.GetComponentInChildren<Text> ().text = buttonText;
				int index = counter;

				gameObject.GetComponent<Button> ().onClick.AddListener (delegate {
					activateInventoryItem (index);
				});

				gameObject.GetComponent<Button> ().onClick.AddListener (gameManagerInventoryUI.toggleInventoryUI);

				gameObject.transform.SetParent (inventoryUI, false);
				counter++;
			}
		}
	}

	void clearInventoryUI() {
		foreach (Transform child in inventoryUI) {
			Destroy (child.gameObject);
		}
	}

	public void activateInventoryItem(int inventoryIndex) {
		deactivateAllInventoryItem ();
		StartCoroutine (placeItemInHand (listInventory [inventoryIndex]));
	}

	void deactivateAllInventoryItem() {
		foreach (Transform child in playerInventory) {
			if (child.CompareTag ("Item")) {
				child.gameObject.SetActive (false);
			}
		}
	}

	IEnumerator placeItemInHand(Transform itemTransform) {
		yield return new WaitForSeconds (timeToPlaceInHand);
		currentlyHeldItem =	itemTransform;
		currentlyHeldItem.gameObject.SetActive (true);
	}
}
