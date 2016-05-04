using UnityEngine;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

[Serializable]
public class LootsClass
{
	public editorResouce Loot;
	public int Count;
}

[Serializable]
public class InstrumentClass1
{
	public editorInstrument Instrument;
	public int Hitpoint;
}

public class gameTree : objMain
{

	public Transform Base;
	public editorResouce Params;

	public int Hitpoints = 10;
	private int _currentHitpoints = 0;

	[Header("Instruments")]
	public InstrumentClass1[] Instruments;

	[Header("Random values")]
	[Range(0f, 1f)]
	public float RandomRange;
	public LootsClass[] RandomResources;

	[Header("End attributes")]
	public LootsClass[] EndResources;

	[Header("Inteface")]
	public GameObject Canvas;
	public Image ProgressBar;


	bool _killed = false;

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		if (Canvas)
		{
			Canvas.SetActive(false);
		}
	}

	public override void Interaction()
	{
		if (_killed) return;

		_currentHitpoints++;
		RandomDrop();
		ShowProgress();
		if (_currentHitpoints >= Hitpoints)
		{
			KillTree();
		}
	}

	public virtual void KillTree()
	{
		_killed = true;

		foreach (LootsClass loot in EndResources)
		{
			gameWorld.DropResource(loot.Loot, transform.position, loot.Count);
		}

		Destroy(gameObject);
	}

	public virtual void RandomDrop()
	{
		if (Random.value > RandomRange) return;
		int rand = Random.Range(0, RandomResources.Length);

		int amount = Random.Range(1, RandomResources[rand].Count + 1);
		gameWorld.DropResource(RandomResources[rand].Loot, transform.position, amount);

	}

	public virtual void ShowProgress()
	{
		if (!ProgressBar) return;
		if (!Canvas) return;

		Canvas.SetActive(true);
		ProgressBar.fillAmount = (float)_currentHitpoints / (float)Hitpoints;
	}
}
