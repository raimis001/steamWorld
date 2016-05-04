using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MainGUI : MonoBehaviour {

	public delegate void InventoryClosed(int inveID);
	public static event InventoryClosed OnInventoryClosed;

	public static MainGUI Instance;

	public guiInventory BackPackPanel;
	public guiInventory InventoryPanel;

	public static guiMouse GuiCursor;
	public guiMouse guiCursor;

	public static mapCursor MapCursor;
	public mapCursor mapCursor;

	public Image MainIcon;
	public Image MainProgress;

	public guiXPanel xPanel;
	public guiHandPanel HandPanel;
	public guiBPanel bPanel;

	public static ItemClass SelectedItem {
		get {
			if (SelectedSlot < 0) return null;
			return null;// Instance.HandPanel.Inventory[SelectedSlot];
		} 
	}
	public static int SelectedSlot {
		get { return Instance.HandPanel.SelectedSlot; }
		
	}

	void Awake() {
		Instance = this;
	}

	void Start () {
		GuiCursor = guiCursor;
		MapCursor = mapCursor;

		mapCursor.gameObject.SetActive(false);
		guiCursor.gameObject.SetActive(false);

		BackPackPanel.gameObject.SetActive(false);
		InventoryPanel.gameObject.SetActive(false);
		xPanel.gameObject.SetActive(false);


	}

	void Update () {
	
	}
	
	#region Open/Close panels
	public void OpenBackPack() {
		BackPackPanel.OpenInventory(0);
	}

	public void OpenInventory(int inventoryID) {
		xPanel.Close(true);
		InventoryPanel.OpenInventory(inventoryID);
		OpenBackPack();
  }
	public void CloseInventory() {
		InventoryPanel.CloseInventory();
	}

	public void OpenXPanel(objWorkbench workbench) {
		InventoryPanel.CloseInventory(true);
		xPanel.Open(workbench);
	}
	internal static void DoInventoryClosed(int inveID) {
		Instance.SetMainIcon(null);
		if (OnInventoryClosed != null) OnInventoryClosed(inveID);
	}

	internal static void DoBPanelClosed() {
		Instance.SetMainIcon(null);
	}

	internal static void DoXPanelClosed() {
		Instance.SetMainIcon(null);
	}
	#endregion

	public void PressIcon(ItemClass icon) {
		MapCursor.Open(icon);
	}

	public void SetMainIcon(objMain mapObject) {
		if (mapObject == null) {
			MainIcon.enabled = false;
			return;
		}

		if (mapObject is objWorkbench)
		{
			OpenXPanel((objWorkbench)mapObject);
		}

		/*
		ItemNames itemID = mapObject.item_id;

		ItemBase item = ItemsDB.GetItem<ItemBase>(itemID);
		if (item == null) {
			MainIcon.enabled = false;
			return;
		}
		MainIcon.sprite = item.Icon;
		MainIcon.enabled = true;

		if (item is ItemDraft) {
			mapObject.RefreshProgress();
			if (mapObject.Stage == ObjectStage.PLACE) bPanel.Open(mapObject);
			return;
		} 

		MainProgress.enabled = false;
		mapObject.HPBar = null;

		switch (item.Action) {
			case ActionTypes.NONE:
				break;
			case ActionTypes.INVENTORY:
				//if (mapObject is objInventory)
					//OpenInventory(((objInventory)mapObject).Inventory.id);
				break;
			case ActionTypes.WORKBENCH:
				if (mapObject is objWorkbench)
					OpenXPanel((objWorkbench)mapObject);
				break;					 
		}
		*/
	}

}
