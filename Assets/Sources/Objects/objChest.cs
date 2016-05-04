using UnityEngine;
using System.Collections;
using System;

public class objChest : objInventory {

	public int MaxCount = 12;
	public int MaxAmount = 99;

	#region Chest open/close
	public Transform Cover;

	bool _opened = false;
	public bool Opened {
		get { return _opened; }
		set {
			_opened = value;

			StartCoroutine(OperateChest());
		}
	}

	IEnumerator OperateChest() {

		float openx = _opened ? 40f : 0f;

		float mtime = 0.75f;
		float time = mtime;
		Vector3 rot = Cover.transform.localEulerAngles;

		while (time > 0) {
			time -= Time.smoothDeltaTime;

			float x = Mathf.Lerp(openx,rot.x, time / mtime);
			Cover.transform.localEulerAngles = new Vector3(x, rot.y, rot.z);
			yield return null;
		}
		Cover.transform.localEulerAngles = new Vector3(openx, rot.y, rot.z);
	}
	#endregion

	protected override void Start() {
		base.Start();

		Inventory.MaxCount = MaxCount;
		Inventory.MaxAmount = MaxAmount;

	}

	void OnEnable() {
		MainGUI.OnInventoryClosed += OnInventoryClosed;
	}
	void OnDisable() {
		MainGUI.OnInventoryClosed -= OnInventoryClosed;
	}

	private void OnInventoryClosed(int inveID) {
		//if (Inventory.id != inveID) return;
		if (Opened) Opened = false;
	}

	public override void Interaction() {
		Opened = !Opened;
		if (Opened)
			base.Interaction();
			else MainGUI.Instance.CloseInventory();
	}
}
