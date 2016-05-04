using UnityEngine;
using System.Collections;

public class objResource : objMain {

	public ItemClass[] RandomDrop;
	public ItemClass[] EndDrop;

	protected float _growTime;
/*
	protected override void SetHitpoints() {
		DropRandom();
	}

	protected override bool EndHitponts() {
		DropEnd();
		return true;
	}

	protected void ChangeHitpoints(float hp) {
		for (int i = 0; i < hp; i++) {
			Hitpoints--;
		}
	}

	protected void DropEnd() {
		foreach (ItemClass item in EndDrop) {
			ObjectManager.DropResource(item, transform.position);
		}
	}

	protected void DropRandom() {
		if (RandomDrop.Length > 0 && Random.value > 0.5f) {
			int rnd = Random.Range(0, RandomDrop.Length);
			ObjectManager.DropResource(RandomDrop[rnd], transform.position);
		}
	}
	*/
}
