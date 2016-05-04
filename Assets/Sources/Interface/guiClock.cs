using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class guiClock : MonoBehaviour
{

	public Transform Mask;
	public Transform World;

	[Range(0f, 1f)]
	public float Rotate;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Mask) Mask.localEulerAngles = new Vector3(0, 0, 360f * -Rotate);
		if (World) World.localEulerAngles = new Vector3(0, 0, 360f * Rotate);

	}
}
