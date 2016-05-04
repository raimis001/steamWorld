using UnityEngine;
using System.Collections;

public class objVaga : objMain {

	public Material[] Materials;

	public Transform Plant;
	
	objPlant PlantObject;
	/*
	protected override void Start() {
		base.Start();
		Stage = ObjectStage.BUILD;
		if (ActiveObject) TerrainManager.UpdateTerrain(transform.position, 4);
	}

	protected override void ChangeStage(ObjectStage oldStage) {
		base.ChangeStage(oldStage);
		if (Stage == ObjectStage.READY) {
			Plant.GetComponent<Renderer>().material = Materials[1];
		} else {
			Plant.GetComponent<Renderer>().material = Materials[0];
		}
	}

	public override bool Interaction(MouseKey mouse) {
		Debug.Log("Click vaga");
		if (!base.Interaction(mouse)) return false;

		if (Stage == ObjectStage.BUILD) {
			Debug.Log("Vaga needs build");
			if (MainGUI.SelectedItem == null) return false;
			if (MainGUI.SelectedItem.amount < 1) return false;

			Debug.Log("Selecetd:" + MainGUI.SelectedItem.ToString());

			ItemGrain grain = ItemsDB.GetItem<ItemGrain>(MainGUI.SelectedItem.id);
			if (grain == null) return false;

			ItemPlant plant = ItemsDB.GetItem<ItemPlant>(grain.Plant);
			if (plant == null) return false;

			GameObject obj = Instantiate(plant.Prefab);
			obj.transform.SetParent(transform);
			obj.transform.localPosition = Vector3.zero;
			PlantObject = obj.GetComponent<objPlant>();
			PlantObject.StartGrow(plant.id, this);

			MainGUI.SelectedItem.amount--;
			ObjectManager.RefreshResources();

			Stage = ObjectStage.READY;

			return true;
		} 

		return false;

	}

	public void OnHarvest(bool removed) {
		Stage = ObjectStage.BUILD;
		PlantObject = null;
	}
	*/
}
