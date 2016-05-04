using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class objConstruction : objInventory {

	public Image Icon;

	public Sprite IconStore;
	public Sprite IconBuild;

	public ItemNames TargetID = ItemNames.all;
	public GameObject[] Stages;

	protected override void Start() {
		base.Start();
		Inventory.MaxCount = 4;

		//InvertProgress = true;
		foreach (GameObject obj in Stages) {
			obj.SetActive(false);
		}

		//if (Item == null) return;
		//if (!(Item is ItemDraft)) return;

		//ItemDraft draft = (ItemDraft)Item;
		//Debug.Log("ad  to inventory");
		//foreach (ItemClass item in draft.Required) {
			//Inventory.Add(item);
		//}

		Stage = ObjectStage.PLACE;
		//if (HPBar) HPBar.fillAmount = 0;
	}

	public override void Interaction() {
		if (Stage == ObjectStage.BUILD) {
			//Hitpoints--;
		}
		return;
	}
/*
	protected override void SetHitpoints() {
		if (_hitpoints < 1) return;

		int stage = Mathf.FloorToInt((float)Stages.Length * (1 - _hitpoints / MaxHitpoints));
		Stages[stage].SetActive(true);
		Debug.Log("Do construct :" + stage.ToString() + " hp:" + _hitpoints.ToString());
	}

	protected override bool EndHitponts() {
		Debug.Log("Endconstruct");
		Stage = ObjectStage.READY;
		if (TargetID < 0) return false;

		ItemBase item = ItemsDB.GetItem<ItemBase>(TargetID);
		if (item == null) return false;

		GameObject obj = Instantiate(item.Prefab, transform.position, Quaternion.identity) as GameObject;
		obj.transform.SetParent(ObjectManager.Instance.transform);


		Destroy(gameObject);

		return true;
	}

	protected override void ChangeStage(ObjectStage oldStage) {
		base.ChangeStage(oldStage);
		if (Stage == ObjectStage.PLACE) {
			Icon.sprite = IconStore;
		} else if (Stage == ObjectStage.BUILD) {
			Icon.sprite = IconBuild;
		} else {
			Icon.enabled = false;
		}
	}

	public void StartBuild() {
		Stage = ObjectStage.BUILD;
		Hitpoints--;
	}
	*/
}
