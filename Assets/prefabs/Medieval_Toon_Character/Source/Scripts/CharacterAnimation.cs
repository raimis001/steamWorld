using UnityEngine;
using System.Collections;

public class CharacterAnimation : MonoBehaviour {

	Animator animator;
	public float v;
	public float h;
	public float run;
	public float angle;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
		float Av = Input.GetAxis("Vertical");
		float Ah = Input.GetAxis("Horizontal");
		
		Vector3 rot = transform.localEulerAngles;

		rot.y = Mathf.Atan2(Ah, Av) * Mathf.Rad2Deg;

		angle = rot.y;
		transform.localEulerAngles = rot;

		v = Av != 0  || Ah != 0 ? 1 : 0;

		if (animator.GetFloat("Run") == 0.2) {
			if (Input.GetKeyDown("space")) {
				animator.SetBool("Jump", true);
			}
		}

		run = Input.GetKey(KeyCode.LeftShift) ? 0.2f : 0;
	}

	void FixedUpdate() {
		animator.SetFloat("Walk", v);
		animator.SetFloat("Run", run);
		//animator.SetFloat("Turn", h);
	}


}
