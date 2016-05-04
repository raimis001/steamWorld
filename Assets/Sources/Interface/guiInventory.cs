using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class guiInventory : MonoBehaviour {

	public GameObject IconPrefab;
	public RectTransform IconPanel;

	private Inventory _inventory = null;
	public int InventoryID {
		get {
			if (_inventory != null) return _inventory.Index;
			return -1;
		}
		set {
			if (Inventory.InventoryList.ContainsKey(value)) {
				_inventory = Inventory.InventoryList[value];
			} else {
				_inventory = null;
			}
			if (_inventory != null) RedrawInventory();
		}
	}

	Dictionary<int, guiCell> Panels = new Dictionary<int, guiCell>();


	void OnEnable() {
		if (InventoryID < 0) InventoryID = 0;
		Inventory.OnResourceAdded += OnResourcePicked;
		RedrawInventory();
	}

	void OnDisable() {
		Inventory.OnResourceAdded -= OnResourcePicked;
	}

	private void OnResourcePicked(editorResouce resource, int amount) {
		RedrawInventory();
	}

	// Update is called once per frame
	void Update() {

	}

	public void OpenInventory(int inveID) {
		if (InventoryID != inveID) InventoryID = inveID;
		if (!gameObject.activeSelf) GetComponent<EasyTween>().OpenCloseObjectAnimation();
	}

	public void CloseInventory(bool imediate = false) {
		if (gameObject.activeSelf) {
			if (!imediate) {
				GetComponent<EasyTween>().OpenCloseObjectAnimation();
			} else {
				gameObject.SetActive(false);
				GetComponent<EasyTween>().ChangeSetState(false);
				MainGUI.DoInventoryClosed(InventoryID);
			}
		}
	}

	public void OnCloseInventory() {
		if (GetComponent<EasyTween>().animationParts.ObjectState == UITween.AnimationParts.State.CLOSE)
			MainGUI.DoInventoryClosed(InventoryID);
	}

	private void RedrawInventory() {
		while (IconPanel.childCount > 0) {
			DestroyImmediate(IconPanel.GetChild(0).gameObject);
		}
		Panels.Clear();
		if (_inventory == null)
		{
			return;
		}

		float w = IconPanel.sizeDelta.x;
		for (int i = 0; i < _inventory.MaxCount; i++) {
			GameObject obj = Instantiate(IconPrefab);
			obj.transform.SetParent(IconPanel);
			Panels.Add(i, obj.GetComponent<guiCell>());

			Panels[i].AmountClass = _inventory[i];

			Panels[i].InventoryID = InventoryID;
			Panels[i].Idx = i;

			Panels[i].AmountClass = _inventory[i];
		}

		int h = _inventory.MaxCount / 4;
		IconPanel.sizeDelta = new Vector2(w, h * 65 + 3);

	}
}
