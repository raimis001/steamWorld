using UnityEngine;
using System.Collections;

public class gameLoot : MonoBehaviour
{

	public int Amount;
	public editorResouce Resource;

	// Use this for initialization
	void Start()
	{
		PlayerControl.Ignore(this.gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseUp()
	{
		int added = Inventory.AddBackpak(Resource, Amount);
		if (added == 0)
		{
			Destroy(gameObject);
		}
		else
		{
			Amount = added;
		}
		
	}
}
