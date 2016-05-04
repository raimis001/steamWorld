using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public struct XPanel {
	public int IconID;
	public InventoryClass Inventory;
}

public class guiXPanel : MonoBehaviour {

	public Transform ProgressArrow;
	public Image ProgressBar;

	public Button MakeButton;

	public guiCell[] InventoryPanels;
	public guiCell[] Ingredients;

	public GameObject FuelPanel;
	public guiIconPanel FuelIcon;
	public Image FuelOk;
	public Image FuelCancel;
	public Slider FuelProgress;

	public guiScroll RecepiesList;

	[HideInInspector]
	public objWorkbench Parent;

	#region INIT
	void OnEnable() {
		Inventory.OnResourceAdded += OnResourcePicked;
	}

	void OnDisable() {
		Inventory.OnResourceAdded -= OnResourcePicked;
	}

	void Start() {

	}
	#endregion

	public void Open(objWorkbench workbench)
	{

		Parent = workbench;
		Parent.OnEndRecepie = OnRecepieEnd;

		MakeButton.interactable = false;

		RedrawInventory();

		if (!gameObject.activeSelf)
		{
			GetComponent<EasyTween>().OpenCloseObjectAnimation();
		}

		RecepiesList.Clear();
		foreach (editorRecepie recepie in Parent.Recepies)
		{
			guiCell cell = RecepiesList.AddItem().GetComponent<guiCell>();
			cell.AmountClass = new AmountClass() { Resource = recepie, Amount = 0 };
			cell.Callback = OnRecepieClick;
		}

	}

	// Update is called once per frame
	void Update() {
		Vector3 rot = ProgressArrow.localEulerAngles;
		if (Parent.WorkTime > 0) {
			rot.z = 90 - 360 * Parent.WorkProgress;
			ProgressBar.fillAmount = Parent.WorkProgress;
		} else {
			rot.z = 90;
			ProgressBar.fillAmount = 0;
		}
		ProgressArrow.localEulerAngles = rot;
		FuelProgress.value = Parent.FuelProgress;
	}


	private void OnResourcePicked(editorResouce resource, int amount) {
		RedrawInventory();
	}

	private void OnRecepieEnd()
	{
		Debug.Log("Redraw");
		RedrawInventory();
	}

	void RedrawInventory() {

		for (int i = 0; i < 3; i++)
		{
			if (!Parent || !Parent.Recepie)
			{
				Ingredients[i].gameObject.SetActive(false);
				continue;
			}

			Ingredients[i].gameObject.SetActive(true);
			if (i < Parent.Recepie.Ingredients.Length)
			{
				Ingredients[i].AmountClass = Parent.Recepie.Ingredients[i];
			}

			Ingredients[i].Idx = i;
		}

		for (int i = 0; i < Parent.Inventory.MaxCount; i++) {
			if (!Parent || Parent.Inventory == null) {
				InventoryPanels[i].gameObject.SetActive(false);
				continue;
			}

			InventoryPanels[i].gameObject.SetActive(true);
			InventoryPanels[i].AmountClass = Parent.Inventory[i];
			InventoryPanels[i].InventoryID = Parent.Inventory.Index;
			InventoryPanels[i].Idx = i;
		}

		RecalcRecepie();

		if (!Parent) {
			FuelPanel.SetActive(false);
			return;
		}

		FuelIcon.AcceptList.Clear();
		//ItemBuilding building = ItemsDB.GetItem<ItemBuilding>(Parent.item_id);
		//if (building == null || building.Fuel.Count < 1) {
		//	FuelPanel.SetActive(false);
		//	return;
		//}

		//foreach (ItemNames id in building.Fuel) {
		//	ItemFuel fuel = ItemsDB.GetItem<ItemFuel>(id);
		//	if (fuel == null) continue;
		//	FuelIcon.AcceptList.Add(fuel.FuelID);
		//}

		//FuelIcon.Icon.Item = Parent.Fuel[0];
		//FuelIcon.InventoryID = Parent.Fuel.id;
		FuelIcon.Idx = 0;

		FuelProgress.value = 0;

		FuelOk.enabled = !Parent.FuelStarted;
		FuelCancel.enabled = Parent.FuelStarted;

		FuelPanel.SetActive(true);

	}

	void RecalcRecepie() {
	
		if (!Parent) {
			MakeButton.interactable = false;
			return;
		}

		MakeButton.interactable = Parent.CheckIngredients();
	}

	public void MakeRecepie() {
		if (Parent.StartRecepie()) {
			MakeButton.interactable = false;
		}
	}

	public void StartFuel() {
		//Parent.StartFuel();
	}


	void OnRecepieClick(guiCell cell)
	{
		Parent.SetRecepie(cell.AmountClass.Resource);
		RedrawInventory();

	}

	public void Close(bool immediate = false) {
		if (gameObject.activeSelf) {
			Parent.OnEndRecepie = null;
			Parent = null; 
			if (!immediate) {
				GetComponent<EasyTween>().OpenCloseObjectAnimation();
			} else {
				gameObject.SetActive(false);
				GetComponent<EasyTween>().ChangeSetState(false);
			}
		}
	}

	public void OnCloseXPanel() {
		if (GetComponent<EasyTween>().animationParts.ObjectState == UITween.AnimationParts.State.CLOSE)
			MainGUI.DoXPanelClosed();
	}

}
