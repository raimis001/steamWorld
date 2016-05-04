using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class guiIconPanel : MonoBehaviour {

	public guiIcon Icon;
	[HideInInspector] public int InventoryID;
	[HideInInspector] public int Idx;

	public bool AllowDrag = true;
	public bool AllowDrop = true;
	public bool AllowNeed = false;

	[HideInInspector]
	public List<ItemNames> AcceptList = new List<ItemNames>();

	public delegate void PanelCallBack(guiIconPanel item);
	public PanelCallBack Callback;

	public void DragStart() {
		if (!AllowDrag) return;
		if (!Icon || Icon.Item == null || Icon.Item.id < 0) return;

		//Debug.Log("Begin drag " + Icon.Item.id);

		//MainGUI.GuiCursor.Item = new ItemClass(Icon.Item.id, Icon.Item.amount);
		MainGUI.GuiCursor.InventoryID = InventoryID;
		MainGUI.GuiCursor.Idx = Idx;

	}
	public void DragEnd() {
		if (!Icon || Icon.Item == null || Icon.Item.id < 0) {
			MainGUI.GuiCursor.DragEnd();
			return;
		}

		//Debug.Log("End drag " + Icon.Item.id);
		if (MainGUI.GuiCursor.Dropped) {
			ObjectManager.AddResource(InventoryID, Icon.Item.id, -MainGUI.GuiCursor.Item.amount, Idx);
			ObjectManager.RefreshResources();
		}
		MainGUI.GuiCursor.DragEnd();

	}
	public void DragDrop() {
		if (!AllowDrop) return;
		if (!MainGUI.GuiCursor.gameObject.activeSelf) return;

		ItemClass item = MainGUI.GuiCursor.Item;
		if (item == null) return;
		if (AcceptList.Count > 0 && AcceptList.IndexOf(item.id) < 0) return;

		//Debug.Log("Drop drag inventory:" + InventoryID + " idx:" + Idx + " gui id:" + item.id);

		InventoryClass inventory = ObjectManager.InventoryList[InventoryID];

		if (Icon.Item == null || Icon.Item.id < 0 || Icon.Item.id == item.id) {
			if (AllowNeed) {
				int need = inventory.Need(Idx);
				if (need < 1) return;

				int amt = inventory.Amount(item.id, Idx);
				if (amt + item.amount > need) {
					item.amount = need - amt;
				}

			}

			MainGUI.GuiCursor.Dropped = false;
			int added = inventory.Add(item, Idx);

			if (added < 0) {
				Debug.Log("Error added");
				return;
			}

			MainGUI.GuiCursor.Dropped = true;
			if (added == 0) {
				Debug.Log("Added ok " + item.ToString());
				return;
			}

			Debug.Log("Add some " + added);
			item.amount -= added;

		}

  }

	public void DragCancel() {
		Debug.Log("Why CANCEL!?");
		MainGUI.GuiCursor.DragEnd();
	}

	public void OnPanelClick() {
		if (Callback != null) Callback(this);
	}

}
