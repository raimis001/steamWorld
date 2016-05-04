using UnityEngine;
using System.Collections;

public class objArrow : MonoBehaviour {

	public Transform BaseTransform;

	bool moved = false;
	Vector3 StartVector;
	Vector3 ImpulssVector;
	Vector3 GravityVector = new Vector3(0, -0.5f, 0);
	float force;

	ItemNames weaponID;

	float time;


	public static void Shot(Vector3 start, ItemNames weaponID, float force) {
		ItemWeapon weapon = ItemsDB.GetItem<ItemWeapon>(weaponID);
		if (weapon == null) return;

		GameObject obj = Instantiate(Resources.Load<GameObject>("Objects/weapons/Arrow"), start, Quaternion.identity) as GameObject;

		objArrow arrow = obj.GetComponent<objArrow>();
		arrow.weaponID = weaponID;
		arrow.force = force;

	}

	// Use this for initialization
	void Start() {

		StartVector = transform.position;

		Vector3 direction = ObjectManager.MousePosition(false) - StartVector - new Vector3(0.5f, 0, 0.5f);
		direction.y = 0;
		direction.Normalize();

		ImpulssVector = direction * 3f * force;

		time = Time.time;
		moved = true;
		transform.position = StartVector;
		BaseTransform.LookAt(ObjectManager.MousePosition(false));
	}



	// Update is called once per frame
	void Update() {
		if (!moved) {
			return;
		}

		float t = (Time.time - time);
		Vector3 pos = StartVector + ImpulssVector * t + GravityVector * Mathf.Pow(t, 2) / 2;
		transform.position = pos;
		//pozīcija laikā t = starta pozīcija + sākotnējais impulss* t +gravitācija * t ^ 2 / 2
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log("Triger  with:" + other.name);
		if (other.name.Equals("Terrain")) {
			moved = false;
			Destroy(gameObject);
			return;
		}

		objMain main = other.gameObject.GetComponent<objMain>();
		if (main == null) return;

		if (main.DoShot(weaponID)) {
			Destroy(gameObject);
		}
	
	}

}
