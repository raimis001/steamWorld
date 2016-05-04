using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public struct TKey {
	public int x;
	public int y;

	public TKey(int _x, int _y) {
		x = _x;
		y = _y;
	}
}

public enum TestKeys {	UP, DOWN, LEFT, RIGHT }

public class HomeKey {
	int x;
	int y;

	Dictionary<TestKeys, TKey> KeyList = new Dictionary<TestKeys, TKey>() {
		{ TestKeys.UP, new TKey(0,1) },
		{ TestKeys.DOWN, new TKey(0,-1) },
		{ TestKeys.LEFT, new TKey(-1,0) },
		{ TestKeys.RIGHT, new TKey(1,0) }
	};

	public HomeKey(int _x, int _y) {
		x = _x;
		y = _y;
	}

	private string idx(int _x, int _y) {
		return _x + ":" + _y;
	}

	public string Index {
		get {
			return idx(x,y);
		}
	}

	public string TestKey(int dx, int dy) {
		return idx(x + dx, y + dy);
	}

	public string TestKey(TestKeys key) {
		return idx(KeyList[key].x + x, KeyList[key].y + y);
	}
}

public class objBlock : objMain {

	static Dictionary<string, objBlock> Home = new Dictionary<string, objBlock>();

	public HomeKey Key;

	public GameObject[] Walls;
	public GameObject[] Doors;

	public GameObject CanvasObject;

	public Text TemperatureText;
	[HideInInspector]
	public float Temperature = 10;
	[HideInInspector]
	public float TemperatureTime = 0;


	/*
				|   1   |
				|   0   |
	-------       --------
	8   3            1  2
	-------       --------
				|   2   |
				|   4   |
	*/

	static int[,] a = {
			{-1,-1,-1,-1 },		//0
			{0,-1,-1,-1 },		//1
			{1, -1, -1, -1 },	//2
			{0, 1, -1, -1 },	//3
			{2, -1, -1, -1 },	//4
			{0, 2, -1,-1 },		//5
			{1, 2, -1, -1 },	//6
			{0, 1,  2, -1 },	//7
			{3, -1, -1, -1 },	//8
			{0, 3, -1, -1 },	//9
			{3, 1, -1, -1 },	//10
			{0, 1,  3, -1 },	//11
			{3, 2, -1, -1 },	//12
			{0, 2,  3, -1 },	//13
			{1, 2,  3, -1 },	//14
			{0, 1, 2, 3 },	//15

		};

	protected override void Start() {
		base.Start();

		//CanvasObject.SetActive(ActiveObject);

		//if (!ActiveObject) return;

		int x = (int)transform.position.x;
		int y = (int)transform.position.z;

		Key = new HomeKey(x,y);

		if (Home.ContainsKey(Key.Index)) {
			Destroy(gameObject);
			return;
		}

		Temperature = ObjectManager.Temperature;

		Home.Add(Key.Index, this);

		RedrawWall();

		if (Home.ContainsKey(Key.TestKey(TestKeys.UP))) Home[Key.TestKey(TestKeys.UP)].RedrawWall();
		if (Home.ContainsKey(Key.TestKey(TestKeys.DOWN))) Home[Key.TestKey(TestKeys.DOWN)].RedrawWall();
		if (Home.ContainsKey(Key.TestKey(TestKeys.RIGHT))) Home[Key.TestKey(TestKeys.RIGHT)].RedrawWall();
		if (Home.ContainsKey(Key.TestKey(TestKeys.LEFT))) Home[Key.TestKey(TestKeys.LEFT)].RedrawWall();

	}

	protected override void Update() {
		base.Update();
		//if (!ActiveObject) return;
		UpdateTemperature();
	}

	void UpdateTemperature() {
		TemperatureTime -= Time.deltaTime;

		if (TemperatureTime <= 0) {

			objFire[] fires = FindObjectsOfType<objFire>();
			foreach (objFire fire in fires) {
				float t = fire.GetTemperature(transform.position, Temperature);
				if (t > Temperature) Temperature = t;
			}

			TemperatureTime = 2f;

			float temp = Temperature;
			float def = Mathf.Lerp(Temperature, ObjectManager.Temperature, 0.3f);

			float div = 1;
			bool aded = false;
			if (Home.ContainsKey(Key.TestKey(0, 1))) {
				div++;
				temp += Home[Key.TestKey(0, 1)].Temperature;
			} else {
				aded = true;
				div++;
				temp += def;
			}
			if (Home.ContainsKey(Key.TestKey(0, -1))) {
				div++;
				temp += Home[Key.TestKey(0, -1)].Temperature;
			} else if (!aded) { 
				aded = true;
				div++;
				temp += def;
			}
			if (Home.ContainsKey(Key.TestKey(1, 0))) {
				div++;
				temp += Home[Key.TestKey(1, 0)].Temperature;
			} else if (!aded) {
				aded = true;
				div++;
				temp += def;
			}
			if (Home.ContainsKey(Key.TestKey(-1, 0))) {
				div++;
				temp += Home[Key.TestKey(-1, 0)].Temperature;
			} else if (!aded) {
				aded = true;
				div++;
				temp += def;
			}

			Temperature = Mathf.Lerp(Temperature, temp / div, 0.1f);

			TemperatureText.text = Temperature.ToString("0");
		}
	}

	public void RedrawWall() {
		int testValue = 0;

		if (Home.ContainsKey(Key.TestKey(0, 1))) testValue += 1;
		if (Home.ContainsKey(Key.TestKey(1, 0))) testValue += 2;
		if (Home.ContainsKey(Key.TestKey(0, -1))) testValue += 4;
		if (Home.ContainsKey(Key.TestKey(-1, 0))) testValue += 8;

		for (int i = 0; i < 4; i++) {
			if (a[testValue, i] > -1) Walls[a[testValue, i]].SetActive(false);
		}

	}


	public void OnWallClick(int wallID) {
		if (MainGUI.MapCursor.Ibase == null || MainGUI.MapCursor.Ibase.Action != ActionTypes.DOOR) return;

		switch (wallID) {
			case 1:
				Walls[0].SetActive(false);
				Walls[0] = Doors[0];
				Walls[0].SetActive(true);
				break;
			case 2:
				Walls[1].SetActive(false);
				Walls[1] = Doors[1];
				Walls[1].SetActive(true);
				break;
			case 4:
				Walls[2].SetActive(false);
				Walls[2] = Doors[2];
				Walls[2].SetActive(true);
				break;
			case 8:
				Walls[3].SetActive(false);
				Walls[3] = Doors[3];
				Walls[3].SetActive(true);
				break;
		}
	}


}
