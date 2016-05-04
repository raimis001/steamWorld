using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class TreeInstrument {
	public ItemNames id;
	public float HP;
}

public class objTree : objResource {

	public Transform RandomBase;
	public GameObject Canvas;

	//public TreeInstrument[] AllowInstruments;
	public List<TreeInstrument> AllowInstruments = new List<TreeInstrument>();

	Vector3 terrainPos;

	protected override void Start() {
		base.Start();
		Stage = ObjectStage.CHOP;

		if (RandomBase) {
			RandomBase.localPosition = new Vector3(Random.value, 0, Random.value);
			RandomBase.localEulerAngles = new Vector3(RandomBase.localEulerAngles.x, Random.Range(0f, 360f), RandomBase.localEulerAngles.z);
			BoxCollider col = GetComponent<BoxCollider>();
			if (col) col.center = new Vector3(RandomBase.localPosition.x, col.center.y, RandomBase.localPosition.z);
			terrainPos = RandomBase.position;
		} else {
			terrainPos = transform.position;
		}

		TerrainManager.UpdateTerrain(terrainPos, 5);
	}

	/*
	public override bool Interaction(MouseKey mouse) {
		if (Stage != ObjectStage.CHOP) return false;
		if (!base.Interaction(mouse)) return false;

		if (AllowInstruments.Count < 1) {//Nav definēts instruments
			if (Canvas) Canvas.SetActive(true);
			Hitpoints--;
			return true;
		}

		if (MainGUI.SelectedItem == null) {
			foreach (TreeInstrument instr in AllowInstruments) {
				if (instr.id == ItemNames.all) {
					if (Canvas) Canvas.SetActive(true);
					ChangeHitpoints(instr.HP);
					return true;
				}
			}
			return false;
		}

		foreach (TreeInstrument instr in AllowInstruments) {
			if (instr.id == MainGUI.SelectedItem.id) {
				if (Canvas) Canvas.SetActive(true);
				ChangeHitpoints(instr.HP);
				return true;
			}
		}

		return false;

	}

	protected override bool EndHitponts() {
		base.EndHitponts();	
		Destroy(gameObject);
		
		return true;
	}

	void OnDestroy() {
		TerrainManager.UpdateTerrain(terrainPos, 3);
	}
	*/
}
