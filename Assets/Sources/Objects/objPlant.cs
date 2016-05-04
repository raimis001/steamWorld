using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class objPlant : objResource {

	public Transform Plant;
	public Transform Fruits;

	[HideInInspector]
	public objVaga Parent;

	protected float _scale = 0;
	protected virtual float Scale {
		get { return _scale; }
		set {
			_scale = value;
			if (!Plant) return;

			Vector3 newScale = Vector3.one * ((_scale * 0.8f) + 0.2f);

			int cnt = Plant.childCount;
			for (int i = 0; i < cnt; ++i) {
				Transform tr = Plant.transform.GetChild(i);
				tr.localScale = newScale;
			}
			
		}
	}

	[Range(0f, 1f)]
	public float TestScale;
	public bool DoTest = true;

	protected override void Start() {
		base.Start();
	}
	/*
	protected override bool StartMapItem() {
		if (!base.StartMapItem()) return false;

		Stage = ObjectStage.RIPE;

		return true;
	}
	*/
	protected override void Update() {
		if (!Application.isPlaying) {
			if (DoTest) {
				Scale = TestScale;
			}
			return;
		}

		base.Update();

		if (Stage == ObjectStage.RIPE) {
			_growTime -= Time.smoothDeltaTime;
			//Scale = 1 - _growTime / ((ItemPlant)Item).RipeTime;
			if (_growTime <= 0) {
				Stage = ObjectStage.READY;
			}
		}

	}

	/*
	protected override void ChangeStage(ObjectStage oldStage) {
		base.ChangeStage(oldStage);

		if (Fruits) Fruits.gameObject.SetActive(Stage == ObjectStage.READY);

		if (Stage == ObjectStage.RIPE) {
			Plant.gameObject.SetActive(true);
			Scale = 0;
			_growTime = ((ItemPlant)Item).RipeTime;
		}

		if (Stage == ObjectStage.READY) {
			Scale = 1;
		}

	}

	public virtual void StartGrow(ItemNames itemID, objVaga parent) {
		item_id = itemID;
		Parent = parent;

		Item = ItemsDB.GetItem<ItemPlant>(itemID);
		if (Item == null) return;

		
		Stage = ObjectStage.RIPE;
	}

	public override bool Interaction(MouseKey mouse) {
		if (!base.Interaction(mouse)) return false;

		return Harvest();

	}

	public virtual bool Harvest() {
		if (Stage != ObjectStage.READY) return false;

		Hitpoints = 0;
		Destroy(gameObject);

		if (Parent) Parent.OnHarvest(true);

		return true;
	}

	//public override bool MousePress(MouseKey mouse) {
	//	return false;
	//}
	*/
}
