using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class randomGrid : MonoBehaviour {

	public int GridSize = 10;

	[Range(0f, 1f)]
	public float Fill = 0.5f;

	public GameObject[] Flora;

	Dictionary<string, int> Grid = new Dictionary<string, int>();

	// Use this for initialization
	void Start() {
		int floraCount = Flora.Length;

		for (int x = 0; x < GridSize; x++)
			for (int y = 0; y < GridSize; y++) {
				string idx = Index(x, y);
				Grid.Add(idx, 0);

				if (Random.value > Fill) continue;

				Grid[idx] = Random.Range(0, floraCount);

				//Vector3 pos = transform.position + new Vector3(x - 5, 0, y - 5);
				GameObject obj = Instantiate(Flora[Grid[idx]]) as GameObject;
				obj.transform.SetParent(transform);
				obj.transform.localPosition = new Vector3(x - 5, 0, y - 5);

			}
	}

	// Update is called once per frame
	void Update() {

	}



	string Index(int x, int y) {
		return x.ToString() + ":" + y.ToString();
	}
}
