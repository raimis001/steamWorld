using UnityEngine;
using System.Collections;

public class objStoneMill : objWorkbench {

	public Transform Rotator;
	

	protected override void Update() {
		base.Update();

		if (Rotator && WorkTime > 0) {
			Vector3 rot = Rotator.localEulerAngles;
			rot.z += Time.deltaTime * 2f;
			//Rotator.localEulerAngles = rot;

			Rotator.Rotate(new Vector3(0, Time.deltaTime * 100f, 0f));
		}

	}
}
