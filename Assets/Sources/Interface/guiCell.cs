using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class guiCell : MonoBehaviour
{

	[HideInInspector]
	public int Idx;
	[HideInInspector]
	public int InventoryID;

	AmountClass _amountClass;
	public AmountClass AmountClass {
		get {
			return _amountClass;
		}
		set {
			_amountClass = value;
			if (AmountText)
			{
				AmountText.text = _amountClass == null ? "" : _amountClass.Amount.ToString();
			}
			if (AmountPanel)
			{
				AmountPanel.SetActive(_amountClass != null && _amountClass.Amount != 0);
			}
			if (Icon)
			{
				Icon.enabled = _amountClass != null;
				Icon.sprite = _amountClass == null ? null : _amountClass.Resource.Icon;
			}
		}
	}

	public bool AllowDrag = true;
	public bool AllowDrop = true;
	public bool AllowNeed = false;

	public int HandValue;

	public Text AmountText;
	public Text HandText;
	public Image Icon;
	public GameObject AmountPanel;

	public delegate void CellCallBack(guiCell cell);
	public CellCallBack Callback;

	void Awake()
	{
		if (HandText)
		{
			HandText.gameObject.SetActive(HandValue > 0);
			HandText.text = HandValue.ToString();
		}
		AmountClass = null;
	}

	#region DRAG
	public void DragBegin()
	{
		if (!AllowDrag) return;
		if (_amountClass == null) return;

		MainGUI.GuiCursor.AmountClass = _amountClass;
		MainGUI.GuiCursor.InventoryID = InventoryID;
		MainGUI.GuiCursor.Idx = Idx;

	}
	public void DragEnd()
	{
		if (!MainGUI.GuiCursor.Dropped)
		{
			MainGUI.GuiCursor.DragEnd();
			return;
		}
		Inventory.AddToInventory(InventoryID, MainGUI.GuiCursor.AmountClass.Resource, -MainGUI.GuiCursor.AmountClass.Amount, Idx, true);
		MainGUI.GuiCursor.DragEnd();
	}
	public void DragCancel()
	{
		Debug.Log("Why CANCEL!?");
		MainGUI.GuiCursor.DragEnd();
	}
	public void DragDrop()
	{
		if (!AllowDrop) return;
		if (!MainGUI.GuiCursor.gameObject.activeSelf) return;
		if (MainGUI.GuiCursor.AmountClass == null) return;

		//AmountClass = MainGUI.GuiCursor.AmountClass;
		Inventory.AddToInventory(InventoryID, MainGUI.GuiCursor.AmountClass, Idx, false);

		//if (AcceptList.Count > 0 && AcceptList.IndexOf(item.id) < 0) return;

		MainGUI.GuiCursor.Dropped = true;

	}
	#endregion

	public void ClickPanel()
	{
		if (Callback != null) Callback(this);
	}


}
