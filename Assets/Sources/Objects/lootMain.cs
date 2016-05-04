using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class lootMain : MonoBehaviour {

	public ItemNames item_id = 0;
	public int amount = 1;

	// Use this for initialization
	void Start () {
		
		Collider collider = GetComponent<Collider>();
		PlayerControl.Ignore(collider);


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		//Debug.Log("Loot click");
		if (EventSystem.current.IsPointerOverGameObject()) return;

		int picked = ObjectManager.PickResource(item_id, amount);
		if (picked == 0) {
			Destroy(gameObject);
			return;
		}

		if (picked < 0) return;

		amount = picked;

	}
}
