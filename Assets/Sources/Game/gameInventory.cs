using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{

	public delegate void ResourceAdded(editorResouce Resource, int amount);
	public static event ResourceAdded OnResourceAdded;

	static int InventoryIndex = 0;
	public static Dictionary<int, Inventory> InventoryList = new Dictionary<int, Inventory>();

	public static Inventory CreateInventory()
	{
		InventoryList.Add(InventoryIndex, new Inventory(InventoryIndex));
		InventoryIndex++;

		return InventoryList[InventoryIndex - 1];
	}
	public static int AddToInventory(int id, editorResouce resource, int amount, int idx = -1, bool refresh = false)
	{
		return AddToInventory(id, new AmountClass() { Resource = resource, Amount = amount }, idx, refresh);
	}

	public static int AddToInventory(int id, AmountClass amount, int idx = -1, bool refresh = false)
	{
		if (!InventoryList.ContainsKey(id) || InventoryList[id] == null)
		{
			return amount.Amount;
		}

		int amt = InventoryList[id].AddResource(amount.Resource, amount.Amount, idx);
		if (refresh && OnResourceAdded != null) OnResourceAdded(amount.Resource, amount.Amount);

		return amt;
	} 

	static Inventory BackPack = CreateInventory();
	public static int AddBackpak(editorResouce resource, int amount)
	{
		return BackPack.Add(resource, amount);
	}

	public static void Refresh()
	{
		if (OnResourceAdded != null) OnResourceAdded(null, 0);
	}

	public int Index;

	public int MaxAmount = 99;
	public int MaxCount = 8;
	public int ColCount = 4;

	private Dictionary<int, AmountClass> Cells = new Dictionary<int, AmountClass>();

	public AmountClass this[int index] {
		set {
			if (!Cells.ContainsKey(index))
			{
				Cells.Add(index, value);
				return;
			}
			Cells[index] = value;
		}
		get {

			if (!Cells.ContainsKey(index) || Cells[index] == null)
			{
				return null;
			}

			return Cells[index];
		}
	}

	public Inventory(int index)
	{
		Index = index;
	}
	public int Add(editorResouce res, int amt, int idx = -1, bool refresh = true)
	{
		int added = AddResource(res, amt, idx);
		if (refresh && OnResourceAdded != null) OnResourceAdded(res, added);
		return added;
	}

	public int Count(editorResouce resource, int idx = -1)
	{
		int result = 0;
		foreach (AmountClass cell in Cells.Values)
		{
			if (cell.Resource.Equals(resource))
			{
				result += cell.Amount;
			}
		}

		return result;
	}
	public bool Check(AmountClass amounts)
	{
		return Check(amounts.Resource, amounts.Amount);
	}

	public bool Check(editorResouce resource, int amount)
	{
		return amount <= Count(resource);
	}

	int AddResource(editorResouce res, int amt, int idx)
	{
		int added = amt;
		if (idx > -1)
		{
			if (idx >= MaxCount)
			{
				Debug.LogError("Slot index:" + idx.ToString() + " is out of inventory bounds");
				return added;
			}

			AmountClass cell = null;
			Cells.TryGetValue(idx, out cell);

			if (cell != null && !cell.Resource.Equals(res))
			{
				Debug.LogError("Slot contains wrong item");
				return added;
			}

			if (added < 0)
			{
				if (cell == null)
				{
					Debug.LogError("Add negative value to empty cell");
					return added;
				}

				cell.Amount += added;
				if (cell.Amount <= 0)
				{
					Cells.Remove(idx);
				}
				return 0;
			}


			if (cell == null)
			{
				Cells.Add(idx, new AmountClass() { Resource = res, Amount = added });
				if (added <= MaxAmount)
				{
					return 0;
				}

				Cells[idx].Amount = MaxAmount;
				added = added - MaxAmount;
			}
			else
			{
				cell.Amount += added;
				if (cell.Amount > MaxAmount)
				{
					added = cell.Amount - MaxAmount;
					cell.Amount = MaxAmount;
				}
				else
				{
					added = 0;
				}
				if (cell.Amount <= 0)
				{
					Cells.Remove(idx);
				}
			}
			
			if (added <= 0) return 0;

		}

		List<int> deletes = new List<int>();
		foreach (int c in Cells.Keys)
		{
			AmountClass cell = Cells[c];
			if (cell.Resource.Equals(res))
			{
				cell.Amount += added;
				added = cell.Amount > MaxAmount ? cell.Amount - MaxAmount : 0;
			}
			if (cell.Amount <= 0)
			{
				deletes.Add(c);
			}

		}
		
		foreach (int c in deletes)
		{
			Cells.Remove(c);
		}

		if (added == 0) return 0;

		for (int i = 0; i < MaxCount; i++)
		{
			AmountClass cell = null;
			Cells.TryGetValue(i, out cell);
			if (cell != null) continue;

			int add = added <= MaxAmount ? added : MaxAmount;
			Cells.Add(i, new AmountClass() { Resource = res, Amount = add });
			added -= add;
			if (added <= 0) break;
		}

		if (added == 0) return 0;

		return added;
	}

}
