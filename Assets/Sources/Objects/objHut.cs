using UnityEngine;
using System.Collections;

public class objHut : objMain {

	public Transform Roof;
	public Transform Door;
	public GameObject DoorTween;

	Transform Player;
	bool Opened = false;

	protected override void Start() {
		base.Start();
		Player = GameObject.Find("Player").transform;
	}

	protected override void Update() {
		base.Update();

		if (!Player) return;

		Vector2 pos = new Vector2(transform.position.x, transform.position.z);
		Vector2 ppos = new Vector2(Player.position.x, Player.position.z);

		if (Vector2.Distance(ppos, pos) < 1) {
			if (!Opened) {
				Roof.gameObject.SetActive(false);
				iTweenEvent.GetEvent(DoorTween, "CloseDoor").Stop();
				iTweenEvent.GetEvent(DoorTween, "OpenDoor").Play();
				Opened = true;
			}
		} else {
			if (Opened) {
				Roof.gameObject.SetActive(true);
				iTweenEvent.GetEvent(DoorTween, "OpenDoor").Stop();
				iTweenEvent.GetEvent(DoorTween, "CloseDoor").Play();
				Opened = false;
			}
		}

	}

}
