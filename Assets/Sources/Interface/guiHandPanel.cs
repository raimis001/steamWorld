using UnityEngine;
using System.Collections;
using System;

public class guiHandPanel : MonoBehaviour {

	public Transform IconList;
	public guiScroll HandList;
	public GameObject ToolsPanel;
	public guiIconPanel InstrumentIcon;

	guiCell[] Panels;

	public Inventory Inventory;

	int _selectedSlot = -1;
	public int SelectedSlot {
		get { return _selectedSlot; }
		set {
			_selectedSlot = value;

			if (_selectedSlot < 0 || Inventory[_selectedSlot] == null) {
				if (ToolsPanel.activeSelf) ToolsPanel.GetComponent<EasyTween>().OpenCloseObjectAnimation();
				_selectedSlot = -1;
				InstrumentIcon.Icon.Item = null;
				MainGUI.Instance.PressIcon(null);
				return;
			}

			//InstrumentIcon.Icon.Item = Inventory[_selectedSlot];
			/*
			ItemBase item = ItemsDB.GetItem<ItemBase>(Inventory[_selectedSlot].id);
			if (item is ItemInstrument) {
				CreateInstrument();
			} else if (item is ItemWeapon) {
				CreateWeapon();
			} else if (item is ItemGrain) {
				//MainGUI.Instance.PressIcon(Inventory[_selectedSlot]);
				if (ToolsPanel.activeSelf) ToolsPanel.GetComponent<EasyTween>().OpenCloseObjectAnimation();
			} else {
				if (ToolsPanel.activeSelf) ToolsPanel.GetComponent<EasyTween>().OpenCloseObjectAnimation();
			}
			*/
		}
	}


	// Use this for initialization
	void Start() {

		Panels = IconList.GetComponentsInChildren<guiCell>();

		Inventory = Inventory.CreateInventory();
		Inventory.MaxCount = 10;

		for (int i = 0; i < Panels.Length; i++) {
			guiCell panel = Panels[i];
			panel.InventoryID = Inventory.Index;
			panel.Idx = i;
			panel.AmountClass = null;
			panel.Callback = OnPanelClick;
		}

		InstrumentIcon.Icon.Item = null;

	}



	void CreateInstrument() {
		//Debug.Log("Instrument");
		/*
		ItemNames itemID = Inventory[_selectedSlot].id;

		HandList.Clear();

		InstrumentClassTEMP instr = ItemsDB.Instruments[ItemsDB.GetItem<ItemInstrument>(itemID).InstrumentType];

		foreach (ItemNames id in instr.Buildings) {
			ItemBase item = ItemsDB.GetItem<ItemBase>(id);

			GameObject obj = HandList.AddItem();
			guiIconPanel panel = obj.GetComponent<guiIconPanel>();
			if (!panel) continue;

			panel.Icon.Item = item.Item;
			panel.Icon.AmountPanel.SetActive(false);
			panel.AllowDrag = false;
			panel.AllowDrop = false;
			panel.Callback = OnToolsClick;

		}
		if (!ToolsPanel.activeSelf) {
			ToolsPanel.GetComponent<EasyTween>().OpenCloseObjectAnimation();
		}
		*/
	}

	void CreateWeapon() {
		Debug.Log("Tas ir wēpons!");
		/*
		HandList.Clear();
		ItemNames itemID = Inventory[_selectedSlot].id;
		ItemWeapon weapon = ItemsDB.GetItem<ItemWeapon>(itemID);

		foreach (ItemNames id in weapon.Ammo) {
			ItemBase item = ItemsDB.GetItem<ItemBase>(id);
			GameObject obj = HandList.AddItem();
			guiIconPanel panel = obj.GetComponent<guiIconPanel>();
			if (!panel) continue;

			panel.Icon.Item = item.Item;
			//panel.Icon.AmountPanel.SetActive(false);
			panel.AllowDrag = false;
			panel.AllowDrop = false;
			panel.Callback = OnAmmoClick;
		}

		if (!ToolsPanel.activeSelf) ToolsPanel.GetComponent<EasyTween>().OpenCloseObjectAnimation();
		MainGUI.Instance.PressIcon(weapon.Item);
		*/
	}

	private void OnAmmoClick(guiIconPanel item) {
		throw new NotImplementedException();
	}

	void OnToolsClick(guiIconPanel panel) {
		MainGUI.Instance.PressIcon(panel.Icon.Item);

	}

	void OnEnable() {
		Inventory.OnResourceAdded += OnResourcePicked;
	}

	void OnDisable() {
		Inventory.OnResourceAdded -= OnResourcePicked;
	}

	private void OnResourcePicked(editorResouce resource, int amount) {
		RedrawInventory();
	}

	void RedrawInventory() {
		if (Panels == null) return;

		for (int i = 0; i < Panels.Length; i++) {
			Panels[i].AmountClass = Inventory[i];
		}

		SelectedSlot = SelectedSlot;
	}

	private void OnPanelClick(guiCell cell) {
		SelectedSlot = cell.Idx;
	}

	// Update is called once per frame
	void Update() {
		for (int i = 0; i < 10; ++i) {
			if (Input.GetKeyDown("" + i)) {
				SelectedSlot = i == 0 ? 9 : i - 1;
				break;
			}
		}

		if (Input.GetMouseButtonDown(1) && _selectedSlot > -1) {
			SelectedSlot = -1;
		}
	}
}
