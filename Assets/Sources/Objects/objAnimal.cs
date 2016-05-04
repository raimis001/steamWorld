using UnityEngine;
using System.Collections;

public class objAnimal : objResource {

	public ParticleSystem BloodParticle;
	/*
	protected override bool EndHitponts() {
		base.EndHitponts();
		Destroy(gameObject);
		return true;
	}

	public override bool DoShot(ItemNames itemID) {
		ItemWeapon weapon = ItemsDB.GetItem<ItemWeapon>(itemID);
		if (weapon == null) return false;

		Hitpoints--;
		//Debug.Log("I'm hurt!");
		BloodParticle.Play();
		return true;
	}
	*/
}
