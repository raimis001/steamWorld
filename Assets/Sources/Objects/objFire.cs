using UnityEngine;
using System.Collections;

public class objFire : objWorkbench {

	public GameObject Fire;

	public float Temperature = 60f;

	public float GetTemperature(Vector3 pos, float def) {
		if (!FuelStarted) return def;

		float dist = Vector3.Distance(transform.position, pos);
		if (dist > 3) return def;
		if (dist < 1) dist = 1;

		return Temperature * (1 / dist);
	}

	protected override void Start() {
		base.Start();
		FuelStarted = false;

	}

	protected override void Update() {
		base.Update();
		if (Fire.activeSelf && !FuelStarted) {
			Fire.SetActive(false);
		}
	}
	/*
	public override void StartFuel() {
		base.StartFuel();

		Fire.SetActive(FuelStarted);
	}
	*/
}
