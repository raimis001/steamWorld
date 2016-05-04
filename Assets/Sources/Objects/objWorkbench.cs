using UnityEngine;
using System.Collections;

public class objWorkbench : objInventory {

	public delegate void EndRecepie();
	public EndRecepie OnEndRecepie;

	[HideInInspector]
	public editorRecepie Recepie;

	public editorRecepie[] Recepies;

	[HideInInspector]
	public float WorkTime = 0;
	public float WorkProgress {
		get {
			if (WorkTime == 0) return 0;
			if (Recepie == null) return 0;
			return 1- WorkTime / Recepie.Time;
		}
	}

	public Inventory Fuel;

	[HideInInspector]
	public float FuelTime = 0;
	[HideInInspector]
	public bool FuelStarted = false;
	public float FuelProgress {
		get {
			if (!FuelStarted) return 0;
			//if (Fuel[0] == null || Fuel[0].id != ItemNames.all) return 0;

			//ItemFuel fuel = ItemsDB.GetItem<ItemFuel>(FuelID());
			//if (fuel == null) return 0;

			return FuelTime;// / fuel.FuelTime;
		}
	}

	protected override void Start() {
		base.Start();

		Inventory.MaxCount = 5;

		Fuel = Inventory.CreateInventory();
		Fuel.MaxCount = 1;
}

protected override void Update() {
		base.Update();
		if (WorkTime > 0) {
			WorkTime -= Time.deltaTime;
			if (WorkTime <= 0) {
				WorkTime = 0;
				Inventory.Add(Recepie, Recepie.Amount, 0);
				//Inventory.Refresh();
				if (OnEndRecepie != null) OnEndRecepie();
				//if (_recepie != null && _recepie.RecepieType == RecepieTypes.NONSTOP && WorkTime == 0) {
				//	StartRecepie();
				//}
			}
		}
		/*
		if (FuelStarted) {
			FuelTime -= Time.deltaTime;
			if (FuelTime <= 0) {
				FuelStarted = false;
				FuelTime = 0;
				if (Fuel[0].amount < 1) {
					Fuel[0] = null;
					ObjectManager.RefreshResources();
				} else {
					StartFuel();
				}
				
			}
		}
		*/
	}

	public void SetRecepie(editorResouce recepie) {

		if (WorkTime > 0) return;
		Recepie = (editorRecepie)recepie;

		/*
		_recepie = ItemsDB.GetItem<ItemRecepie>(recepieID);

		Recepies.Clear();
		Recepies.Add(_recepie.Result[0], 0);

		int i = 1;
		foreach (ItemClass itm in _recepie.Required) {
			Recepies.Add(itm, i++);
		}

		if (_recepie.RecepieType == RecepieTypes.NONSTOP) {
			StartRecepie();
		}
		*/
	}

	public bool CheckIngredients()
	{
		if (WorkTime > 0) return false;
		if (Recepie == null) return false;
		bool make = true;
		for (int i = 1; i < Recepie.Ingredients.Length; i++)
		{
			if (!Inventory.Check(Recepie.Ingredients[i]))
			{
				make = false;
				break;
			}
		}

		return make;
	}

	public bool StartRecepie() {
		if (WorkTime > 0) return false;
		if (Recepie == null) return false;
		if (!CheckIngredients()) return false;

		for (int i = 0; i < Recepie.Ingredients.Length; i++)
		{
			Inventory.Add(Recepie.Ingredients[i].Resource, -Recepie.Ingredients[i].Amount, -1, false);
		}

		Inventory.Refresh();
		WorkTime = Recepie.Time;
		return true;
	}
	/*
	public override bool Interaction(MouseKey mouse) {
		return base.Interaction(mouse);
	}

	ItemNames FuelID() {
		ItemBuilding building = ItemsDB.GetItem<ItemBuilding>(item_id);
		if (building == null) return ItemNames.all;

		ItemNames fuelID = ItemNames.all;
		foreach (ItemNames id in building.Fuel) {
			ItemFuel fuel = ItemsDB.GetItem<ItemFuel>(id);
			if (fuel == null) continue;
		}
		return fuelID;
	}

	public virtual void StartFuel() {
		if (FuelStarted) return;
		//if (Fuel[0] == null || Fuel[0].id != ItemNames.all || Fuel[0].amount < 1) return;

		ItemBuilding building = ItemsDB.GetItem<ItemBuilding>(item_id);
		if (building == null) return;

		ItemNames fuelID = FuelID();
		if (fuelID == ItemNames.all) return;

		FuelTime = ItemsDB.GetItem<ItemFuel>(fuelID).FuelTime;
		FuelStarted = true;

		Fuel[0].Amount--;
		ObjectManager.RefreshResources();
	}
	*/
}
