using UnityEngine;
using System.Collections;

public class objDoor : objWall {

	Transform Player;

	public Transform Slide1;
	public Transform Slide2;
	public Transform Pivot;

	bool sliding = false;
	bool stop = false;

	bool _opened = false;
	bool Opened {
		get { return _opened; }
		set {
			_opened = value;
			PlayerControl.Ignore(GetComponent<Collider>(), _opened);
      StartCoroutine(OperateDoor());
		}
	}

	IEnumerator OperateDoor() {
		
		while (sliding) {
			stop = true;
			yield break;
		}

		sliding = true;
		stop = false;
		Vector3 p1 = Slide1.localPosition;
		Vector3 p2 = Slide2.localPosition;

		float step = (_opened ? 1f : -1f);
		float x1 = p1.x;
		float x2 = p2.x;
		float t = 0.74f;
		while (t > 0) {
			t -= Time.smoothDeltaTime;
			p1.x = x1 - Mathf.Lerp(0.49f, 0f, t) * step;
			p2.x = x2 + Mathf.Lerp(0.49f, 0f, t) * step;
			Slide1.localPosition = p1;
			Slide2.localPosition = p2;
			if (stop) break;
			yield return null; // new WaitForSeconds(0.05f);
		}

		p1.x = _opened ? -0.74f : -0.24f; 
		Slide1.localPosition = p1;
		p2.x = _opened ? 0.74f : 0.24f;
		Slide2.localPosition = p2;
		sliding = false;
	}

	protected override void Start() {
		base.Start();

		//Player = GameObject.Find("Player").transform;
		Player = PlayerControl.Instance.transform;
	}
	// Update is called once per frame
	override protected void Update () {
		base.Update();
		
		Vector2 pos = new Vector2(transform.position.x + 0.5f, transform.position.z + 0.5f);
		Vector2 ppos = new Vector2(Player.position.x, Player.position.z);
		
		if (Vector2.Distance(ppos, pos) < 1) {
			if (!Opened) Opened = true;
		} else {
			if (Opened) Opened = false;
		}
		
	}

	public override void RedrawWall() {
		//base.RedrawWall();

		Vector3 rot = Pivot.localEulerAngles;
		if (ExistWall(0, 1) || ExistWall(0, -1)) {
			rot.y = 90f;
		} else {
			rot.y = 0f;
		}

		Pivot.localEulerAngles = rot;

	}



}
