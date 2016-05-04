using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum MouseKey { LEFT, RIGHT }

public class IVector {
	public int x = 0;
	public int y = 0;
	
	public IVector() {
		
	}

	public IVector(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public Vector2 vector2 {
		get { return new Vector2(x,y); }
	}

	public Vector3 vector3 {
		get { return new Vector3(x, y); }
	}

	public override string ToString() {
		return "x:" + x.ToString() + " y:" + y.ToString();
	}

}

[Serializable]
public class ItemClass {
	public ItemNames id;
	public int amount;

	[HideInInspector] public int need = -1;

	public ItemBase item {
		get {
			return ItemsDB.GetItem<ItemBase>(id);
		}
	}

	public ItemClass(ItemNames itemID, int Amount, int Need = -1) {
		id = itemID;
		amount = Amount;
		need = Need;
	}
	public override string ToString() {
		return "id:" + id.ToString() + " amount:" + amount.ToString();
	}
}


public class ObjectManager : MonoBehaviour {

	public static float Temperature = 20f;

	public delegate void ResourcePicked(ItemNames item_id, int amount);
	public static event ResourcePicked OnResourcePicked;

	static ObjectManager _instance;
	public static ObjectManager Instance {
		get { return _instance; }
	}

	public static int InventoryMax = 0;
	public static InventoryClass NewInventory() {
		int inve = ++InventoryMax;
		InventoryList.Add(inve, new InventoryClass(inve));
		return InventoryList[inve];
	}
	public static Dictionary<int, InventoryClass> InventoryList = new Dictionary<int, InventoryClass>();

	public ItemBase this[ItemNames index] {
		get {

			ItemBase item = null;
			ItemsDB.Items.TryGetValue(index, out item);

			return item;
		}
	}

	
	public static Vector3 MousePosition(bool allign = true) {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 pos;

		Collider terrain = GameObject.Find("Terrain").GetComponent<Collider>();

		if (terrain.Raycast(ray, out hit, Mathf.Infinity)) {
			pos = hit.point;
			if (allign) {
				pos.x = Mathf.Round(pos.x ) + 0.5f;
				pos.y = Mathf.Round(pos.y);
				pos.z = Mathf.Round(pos.z) + 0.5f;
			}
		} else {
			return Vector3.zero;
		}

		return pos;

	}

	#region resources
	public static void DropResource(ItemClass res, Vector3 pos) {
		DropResource(res.id, res.amount, pos);
	}
	public static void DropResource(ItemNames resID, int amount, Vector3 pos) {
		if (_instance == null) return;
		if (_instance[resID] == null) return;

		Vector3 p = pos + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
		GameObject res = Instantiate(_instance[resID].Prefab, p, Quaternion.identity) as GameObject;
		res.transform.SetParent(_instance.transform);
		res.GetComponent<lootMain>().item_id = resID;
		res.GetComponent<lootMain>().amount = amount;

	}

	public static int PickResource(ItemNames resID, int amount) {
		int result = InventoryList[0].Add(resID, amount);
		if (result > -1) {
			if (OnResourcePicked != null) OnResourcePicked(resID, amount);
		}
		return result;
	}
	public static bool AddResource(int inventory, ItemNames item_id, int amount, int idx = -1, bool refresh = false) {
		if (!InventoryList.ContainsKey(inventory)) return false;

		InventoryClass inve = InventoryList[inventory];
		int result = inve.Add(item_id, amount, idx);
		if (refresh && result > -1) {
			if (OnResourcePicked != null) OnResourcePicked(item_id, amount);
		}

		return result > -1;
	}
	public static void RefreshResources() {
		if (OnResourcePicked != null) OnResourcePicked(ItemNames.all, 0);
	}
	#endregion

	void Awake() {
		ItemsDB.Init();
		_instance = this;
		InventoryList[0] = new InventoryClass(0);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

}
