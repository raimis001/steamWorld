using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class mapCursor : MonoBehaviour {

	public Image Icon;

	public GameObject Cursor;
	public GameObject Canvas;

	public Sprite CrossHire;
	public Image CrossIcon;
	public Image CrossProgress;


	[HideInInspector] public bool AllignGrid = false;

	GameObject _prefab;

	public ItemBase Ibase;

	// Use this for initialization
	void Start() {
		PlayerControl.Ignore(gameObject);
	}

	// Update is called once per frame
	void Update() {
		Vector3 pos = ObjectManager.MousePosition(AllignGrid) - new Vector3(0.5f,0f, 0.5f) ;
		transform.position = pos;

		int layer = LayerMask.NameToLayer("objects");
		Collider[] hitColliders = Physics.OverlapSphere(pos + new Vector3(0.5f, 0f, 0.5f), 0.4f, 1 << layer);

		if (hitColliders.Length > 0) {
			if (Ibase is ItemBuilding) {
				Cursor.GetComponent<Renderer>().material.color = Color.red;
			} else if (Ibase is ItemGrain) {

				bool vaga = false;
				foreach (Collider col in hitColliders) {
					if (col.GetComponent<objVaga>()) {
						vaga = true;
						break;
					}
				}
				Cursor.GetComponent<Renderer>().material.color = vaga ? Color.white : Color.red;
			}

		} else {
			if (Ibase is ItemBuilding || Ibase is ItemDraft) {
				if (Ibase.Action == ActionTypes.DOOR) return;

				Cursor.GetComponent<Renderer>().material.color = Color.white;

				if (_prefab && Input.GetMouseButtonDown(0)) {

					GameObject obj = Instantiate(Ibase.Prefab);
					obj.transform.SetParent(ObjectManager.Instance.transform);
					obj.transform.position = _prefab.transform.position;

					if (Ibase is ItemDraft) {
						objConstruction site = obj.GetComponent<objConstruction>();
						site.TargetID = ((ItemDraft)Ibase).Result;
						//site.item_id = Ibase.id;
					}

				}
			} else if (Ibase is ItemGrain) {
				Cursor.GetComponent<Renderer>().material.color = Color.red;
			}
		}

		if (Ibase is ItemWeapon) {
			if (Input.GetMouseButton(0) && CrossProgress.fillAmount < 1) {
				CrossProgress.fillAmount += 0.01f;
			}
			if (Input.GetMouseButtonUp(0)) {
				PlayerControl.Instance.DoShot(Ibase.id, CrossProgress.fillAmount);
				CrossProgress.fillAmount = 0;
			}
		}

	}

	public void Open(ItemClass item) {
		if (item == null) {
			Close();
			return;
		}

		Ibase = ItemsDB.GetItem<ItemBase>(item.id);

		if (Ibase == null) {
			Close();
			return;
		}

		//Debug.Log("Try to show cursor");
		Icon.enabled = false;
		AllignGrid = false;

		if (Ibase is ItemDraft || Ibase is ItemBuilding) {

			if (Ibase.Action == ActionTypes.DOOR) {
				Debug.Log("door");
				Icon.sprite = Ibase.Icon;
				Icon.enabled = true;

				CrossIcon.enabled = false;
				CrossProgress.enabled = false;

				Canvas.SetActive(true);
				Cursor.SetActive(false);
				gameObject.SetActive(true);
				return;
			}

			_prefab = Instantiate(Ibase.Prefab);
			_prefab.transform.SetParent(transform);
			_prefab.transform.localPosition = Vector3.zero;

			//objMain main = _prefab.GetComponent<objMain>();
			//main.SetInactive();

			AllignGrid = Ibase.Action == ActionTypes.BLOCK;

			Canvas.SetActive(false);
			Cursor.SetActive(true);
		} else if (Ibase is ItemGrain) {

			_prefab = Instantiate(Resources.Load<GameObject>("Objects/tools/Sow"));
			_prefab.transform.SetParent(transform);
			_prefab.transform.localPosition = Vector3.zero;

			Canvas.SetActive(false);
		} else if (Ibase is ItemWeapon) {
			Debug.Log("Cursor wēpon");


			CrossIcon.sprite = CrossHire;
			CrossIcon.enabled = true;
			CrossProgress.enabled = true;
			CrossProgress.fillAmount = 0;

			Canvas.SetActive(true);
			Cursor.SetActive(false);
		} else {
			Debug.Log("Nothing");
			Close();
			return;
		}

		gameObject.SetActive(true);
	}



	public void Close() {
		if (_prefab) Destroy(_prefab);
		gameObject.SetActive(false);
	}
}
