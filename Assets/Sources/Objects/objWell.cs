using UnityEngine;
using System.Collections;

public class objWell : objWorkbench {

	public ItemNames WellRecepie;

	protected override void Start() {
		base.Start();
		//item_id = ItemNames.well;

		Inventory.MaxCount = 1;
		//Recepies.MaxCount = 1;

		//SetRecepie(WellRecepie);


	}

}
