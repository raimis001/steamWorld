using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : ItemClass {
	public int idx;
	
	public InventoryData(ItemNames itemID, int Amount) : base(itemID, Amount) {

	}
	public InventoryData(ItemClass item) : base(item.id, item.amount, item.need) {
	}
}

public class InventoryClass {
	
	public int id;
	public int MaxAmount = 99;
	public int MaxCount = 8;
	public int ColCount = 4;
	private Dictionary<int, InventoryData> InventoryList = new Dictionary<int, InventoryData>();

	public InventoryData this[int index] {
		set {
			if (!InventoryList.ContainsKey(index)) {
				InventoryList.Add(index, value);
				return;
			}
			InventoryList[index] = value;
		}
		get {
			
			if (!InventoryList.ContainsKey(index) || InventoryList[index] == null) { 
				return null;
			}

			return InventoryList[index];
		}
	}
	
	public InventoryClass(int inveID) {
		id = inveID;
	}

	public IVector Position(int idx) {
		int x = idx % ColCount;
		int y = idx / ColCount;

		return new IVector(x,y);
	}
	public int Index(int x, int y) {
		//return (y << ColCount) | x; tikai ar 2 pakāpē
		return y * ColCount + x;
	}
	public int Index(IVector pos) {
		return Index(pos.x, pos.y);
	}

	public int Add(ItemClass item, int idx = -1) {
		int added = item.amount;
		if (idx > -1) {
			//Debug.Log("Adding item:" + item.ToString() + " to slot:" + idx.ToString());
			if (idx >= MaxCount) {
				Debug.Log("Slot index:" + idx.ToString() + " is out of inventory bounds");
				return -1;
			}

			if (InventoryList.ContainsKey(idx) && InventoryList[idx] != null && InventoryList[idx].id != item.id) {
				Debug.Log("Slot contains wrong item");
				return -1;
			}

			if (!InventoryList.ContainsKey(idx) || InventoryList[idx] == null) {
				InventoryList[idx] = new InventoryData(item.id, 0);
				InventoryList[idx].idx = idx;
			}

			if (added < 0) {
				InventoryList[idx].amount += added;
				if (InventoryList[idx].amount <= 0) {
					InventoryList[idx] = null;
				}
				return 0;
			}

			InventoryList[idx].amount += added;
			if (InventoryList[idx].amount <= MaxAmount) {
				return 0;
			}

			added = InventoryList[idx].amount - MaxAmount;
			InventoryList[idx].amount = MaxAmount;
		}

		foreach (InventoryData inve in InventoryList.Values) {
			if (inve == null || inve.id != item.id) continue;
			if (added > 0 && inve.amount >= MaxAmount) continue;

			inve.amount += added;
			if (inve.amount > MaxAmount) {
				added = inve.amount - MaxAmount;
				inve.amount = MaxAmount;
				continue;
			}
			if (inve.amount == 0) {
				InventoryList[inve.idx] = null;
			}
			return 0;
		}

		for (int i = 0; i < MaxCount; i++) {
			if (InventoryList.ContainsKey(i) && InventoryList[i] != null) continue;

			InventoryList[i] = new InventoryData(item.id, added);
			InventoryList[i].idx = i;

			if (InventoryList[i].amount > MaxAmount) {
				added = InventoryList[i].amount - MaxAmount;
				InventoryList[i].amount = MaxAmount;
				continue;
			}

			return 0;

		}

		return added;

	}
	public int Add(ItemNames item_id, int amount, int idx = -1) {
		return Add(new ItemClass(item_id, amount), idx);
	}

	public bool Remove(InventoryData data) {
		return Remove(data.id, data.amount);
	}
	public bool Remove(ItemNames item_id, int amount) {
		if (!Check(item_id, amount)) return false;
		int amt = amount;

		foreach (InventoryData data in InventoryList.Values) {
			if (data == null || data.id != item_id) continue;

			data.amount -= amt;
			if (data.amount < 0) {
				amt = -data.amount;
				data.amount = 0;
			}

			if (data.amount <= 0) {
				InventoryList[data.idx] = null;
			}

			if (amt <= 0) break;

		}

		return true;
	}

	public void Clear() {
		InventoryList.Clear();
	}

	public bool Check(ItemClass data) {
		return Check(data.id, data.amount);
	}
	public bool Check(ItemNames item_id, int amount) {
		int amt = amount;

		foreach (InventoryData data in InventoryList.Values) {
			if (data == null || data.id != item_id) continue;

			amt -= data.amount;
			if (amt <= 0) {
				return true;
			}
		}

		return false;
	}

	public int Need(int idx) {
		InventoryData inve = null;
		InventoryList.TryGetValue(idx, out inve);
		if (inve != null) { 
			return inve.need;
		}

		return -1;
	}
	public int Amount(ItemNames item_id, int idx = -1) {
		int result = 0;
		if (idx > -1) {
			if (InventoryList[idx] != null) {
				result = InventoryList[idx].amount;
			}
			return result;
		}

		foreach (InventoryData data in InventoryList.Values) {
			if (data == null || data.id != item_id) continue;
			result += data.amount;
		}
		return result;
	}

	public bool Split(int idx) {
		if (idx >= MaxCount) return false;
		if (!InventoryList.ContainsKey(idx) || InventoryList[idx] == null) return false;
		if (InventoryList[idx].amount <= 0) return false;

		int free = -1;
		for (int i = 0; i < ColCount; i++) {
			if (InventoryList.ContainsKey(i) && InventoryList[i] != null) continue;
			free = i;
			break;
		}

		if (free < 0) return false;

		InventoryData data = InventoryList[idx];

		int amount = data.amount;
		data.amount = amount / 2;
		amount = amount - data.amount;

		InventoryList[free] = new InventoryData(data.id, amount);
		InventoryList[free].idx = free;


		return true;

	}

}
