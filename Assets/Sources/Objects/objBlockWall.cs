using UnityEngine;
using System.Collections;

public class objBlockWall : MonoBehaviour {

	public int Index;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	void OnMouseDown() {
		//Debug.Log("Press wall " + transform.parent.gameObject.name);
		if (!transform.parent) return;

		objBlock block = transform.parent.parent.gameObject.GetComponent<objBlock>();
		if (block == null) return;

		block.OnWallClick(Index);
	}
}
