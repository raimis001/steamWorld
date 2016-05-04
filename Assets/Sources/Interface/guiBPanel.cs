using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class guiBPanel : MonoBehaviour {

	public guiIconPanel ResultIcon;
	public Button MakeButton;

	public guiIconPanel[] InventoryPanels;

	objConstruction Parent;

	void OnEnable() {
		ObjectManager.OnResourcePicked += OnResourcePicked;
	}

	void OnDisable() {
		ObjectManager.OnResourcePicked -= OnResourcePicked;
	}

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void Open(objMain mobject) {
		if (!(mobject is objConstruction)) return;

		Parent = (objConstruction)mobject;

		RedrawInventory();

		ResultIcon.Icon.Item = new ItemClass(Parent.TargetID, -1);

		if (!gameObject.activeSelf) {
			GetComponent<EasyTween>().OpenCloseObjectAnimation();
		}
	}

	private void OnResourcePicked(ItemNames item_id, int amount) {
		RedrawInventory();
	}

	private void RedrawInventory() {
		bool allow = true;
		for (int i = 0; i < 3; i++) {
			if (!Parent || Parent.Inventory == null || i >= Parent.Inventory.MaxCount - 1) {
				InventoryPanels[i].gameObject.SetActive(false);
				continue;
			}

			InventoryPanels[i].gameObject.SetActive(true);
			//InventoryPanels[i].Icon.Item = Parent.Inventory[i];
			//InventoryPanels[i].InventoryID = Parent.Inventory.id;
			InventoryPanels[i].Idx = i;

			//if (Parent.Inventory[i] != null && Parent.Inventory[i].need > Parent.Inventory[i].amount) allow = false;
		}

		MakeButton.interactable = allow;

	}
	/*
	public void StartBuild() {
		Parent.StartBuild();
		GetComponent<EasyTween>().OpenCloseObjectAnimation();
	}
	public void OnCloseXPanel() {
		if (GetComponent<EasyTween>().animationParts.ObjectState == UITween.AnimationParts.State.CLOSE)
			MainGUI.DoBPanelClosed();
	}
	*/
}
