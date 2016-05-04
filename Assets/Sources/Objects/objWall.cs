using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objWall : objMain {

	public static Dictionary<string,objWall> Walls = new Dictionary<string,objWall>();

	static string getIndex(Vector2 pos) {
		return pos.x.ToString("0") + ":" + pos.y.ToString("0");
	}

	public GameObject[] WallsTextures;

	public Vector2 Pos {
		get {
			return new Vector2(transform.position.x, transform.position.z);
		}
	}

	public string index {
		get {
			return getIndex(Pos);
		}
	}

	/*
	
			|  1  |
	----			----
		8         2
	----			----
			|  4  | 
	*/

	// Use this for initialization
	override protected void Start () {
		base.Start();

		Walls.Add(index, this);
		RedrawWall();
		RedrawNeibors();

		//Debug.Log("Add wall at:" + index);

	}

	void OnDestroy() {
		//Debug.Log("Remove wall " + index);

		Walls.Remove(index);
		RedrawNeibors();

	}

	virtual public void RedrawWall() {
		HideAllWalls();

		int checksum = 0;
		checksum += ExistWall(0, 1) ? 1 : 0;
		checksum += ExistWall(1, 0) ? 2 : 0;
		checksum += ExistWall(0, -1) ? 4 : 0;
		checksum += ExistWall(-1, 0) ? 8 : 0;


		WallsTextures[checksum].SetActive(true);

	}
	public void HideAllWalls() {
		foreach (GameObject obj in WallsTextures) {
			obj.SetActive(false);
		}

	}

	public void RedrawNeibors() {
		objWall wall;

		wall = GetWall(0, 1);
		if (wall) wall.RedrawWall();
		wall = GetWall(1, 0);
		if (wall) wall.RedrawWall();
		wall = GetWall(0, -1);
		if (wall) wall.RedrawWall();
		wall = GetWall(-1, 0);
		if (wall) wall.RedrawWall();

	}
	
	// Update is called once per frame
	override protected void Update() {
		base.Update();
	}
	protected objWall GetWall(float x, float y) {
		return GetWall(new Vector2(x, y));
	}

	protected objWall GetWall(Vector2 pos) {
		objWall result = null;
		Walls.TryGetValue(getIndex(Pos + pos), out result);
		return result;
	}

	protected bool ExistWall(float x, float y) {
		return ExistWall(new Vector2(x, y));
	}

	protected bool ExistWall(Vector2 pos) {
		string idx = getIndex(Pos + pos);

		return Walls.ContainsKey(idx);
	}


}
