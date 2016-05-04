using UnityEngine;
using System.Collections;

public class objInventory : objMain {

	public Inventory Inventory;

	// Use this for initialization
	protected override void Start() {
		base.Start();
		Inventory = Inventory.CreateInventory();
	}
}
