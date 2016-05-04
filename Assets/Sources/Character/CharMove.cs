using UnityEngine;
using System.Collections;

public class CharMove : MonoBehaviour
{

	public Transform Origin;
	public float Speed = 1;

	float forwardInput, rotateInput;
	Rigidbody rBody;

	Animator animator;

	// Use this for initialization
	void Start()
	{
		rBody = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{

		forwardInput = Input.GetAxis("Vertical");
		rotateInput = Input.GetAxis("Horizontal");

		Vector3 rot = transform.localEulerAngles;
		rot.y = Mathf.Atan2(rotateInput, forwardInput) * Mathf.Rad2Deg;
		Origin.localEulerAngles = rot;
	}

	void FixedUpdate()
	{
		animator.SetFloat("go", Mathf.Abs(rotateInput) + Mathf.Abs(forwardInput));
		rBody.velocity = new Vector3(rotateInput * Speed, 0, forwardInput * Speed);
	}
}
