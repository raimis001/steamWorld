using UnityEngine;
using System.Collections;

public class gameWorld : MonoBehaviour
{
	static gameWorld _instance;
	public static gameWorld Instance { get { return _instance; } }

	public static void DropResource(editorResouce resource, Vector3 position, int amount)
	{

		Vector3 random = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

		gameLoot obj = Instantiate(resource.LootPrefab, position + random, Quaternion.identity) as gameLoot;

		if (Instance == null) return;

		obj.gameObject.transform.SetParent(Instance.transform);
		obj.Amount = amount;
		obj.Resource = resource;

	}

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
