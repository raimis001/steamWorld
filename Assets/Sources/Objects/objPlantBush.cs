using UnityEngine;
using System.Collections;

public class objPlantBush : objPlant {

	public Transform Bush;

	protected override void Start() {
		base.Start();
	}

	public float BushScale {
		set {
			if (!Bush) return;
			Vector3 newScale = Vector3.one * ((value * 0.8f) + 0.2f);

			int cnt = Bush.childCount;
			for (int i = 0; i < cnt; ++i) {
				Transform tr = Bush.transform.GetChild(i);
				tr.localScale = newScale;
			}
		}
	}

	protected override void Update() {
		base.Update();
		if (!Application.isPlaying) {
			if (DoTest) BushScale = TestScale;
			return;
		}

		if (Stage == ObjectStage.GROW) {
			_growTime -= Time.deltaTime;
			//BushScale = 1 - _growTime / ((ItemPlant)Item).GrowTime;
			if (_growTime <= 0) {
				Stage = ObjectStage.RIPE;
			}
		}
	}

	protected override void ChangeStage(ObjectStage oldStage) {
		base.ChangeStage(oldStage);

		if (Stage == ObjectStage.RIPE || Stage == ObjectStage.READY) {
			BushScale = 1;
		}

		if (Stage == ObjectStage.GROW) {
			Fruits.gameObject.SetActive(false);
			Plant.gameObject.SetActive(false);
			Debug.Log("Start grow");
			//_growTime = ((ItemPlant)Item).GrowTime;
			Debug.Log(_growTime);
		}
	}
	/*
	public override void StartGrow(ItemNames itemID, objVaga parent) {
		item_id = itemID;
		Parent = parent;

		Item = ItemsDB.GetItem<ItemPlant>(itemID);
		if (Item == null) return;

		Stage = ObjectStage.GROW;
		
	}

	public override bool Harvest() {
		if (Stage != ObjectStage.READY) return false;

		Hitpoints = 0;
		Stage = ObjectStage.RIPE;

		return true;
	}
	*/
}
