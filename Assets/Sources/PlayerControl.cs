using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour {

	private static float _HP = 1;
	public static float HP {
		get { return _HP; }
		set {
			_HP = value;
			if (Instance && Instance.hpProgress) {
				Instance.hpProgress.Value = _HP;
      }
		}
	}

	public static float TIRED = 0;
	public static float HUNGRY = 0;

	public guiProgress hpProgress;
	public guiProgress tiredProgress;
	public guiProgress waterProgress;
	public guiProgress hungryProgress;

	public GameObject Weapon;

	static PlayerControl _instance;
	public static PlayerControl Instance {
		get {  return _instance; }
	}
	public static void Ignore(GameObject obj, bool ignore = true) {
		Ignore(obj.GetComponent<Collider>(), ignore);
	}

	public static void Ignore(Collider collider, bool ignore = true) {
		if (_instance == null) { Debug.Log("Player not found"); return; }
		if (collider == null || !collider.enabled) { /*Debug.Log("Collider disabled");*/ return; };

		Collider pcolldier = _instance.GetComponent<Collider>();
		if (pcolldier == null) { Debug.Log("No player collider"); return; };

		Physics.IgnoreCollision(pcolldier, collider, ignore);
	}

	void Awake() {
		_instance = this;
	}

	void Start() {
		HP = 1;

		//Weapon.SetActive(false);
	}

	void Update() {
	}
	
	//00031476
	public void ActivateWeapon(bool disable = false) {
		Weapon.SetActive(!disable);
	}

	public void DoShot(ItemNames weaponID, float force) {
		objArrow.Shot(Weapon.transform.position - new Vector3(0.5f, 0, 0.5f), weaponID, force);
	}
}
