using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class guiMouse : MonoBehaviour {


	private int _maxAmount;

	private ItemClass _item;
	public ItemClass Item {
		get { return _item; }
		set {
			_item = value;

			if (_item == null) {
				gameObject.SetActive(false);
				Dropped = false;
				return;
			}

			_maxAmount = _item.amount;

			GetComponent<Image>().sprite = _item.item.Icon;
			transform.position = Input.mousePosition;
			AmountText.text = _maxAmount.ToString();
			Dropped = false;
      gameObject.SetActive(true);
		}
	}

	AmountClass _amountClass;
	public AmountClass AmountClass {
		get {
			return _amountClass;
		}
		set {
			_amountClass = value;
			if (_amountClass == null)
			{
				gameObject.SetActive(false);
				Dropped = false;
				return;
			}

			_maxAmount = _amountClass.Amount;

			GetComponent<Image>().sprite = _amountClass.Resource.Icon;
			transform.position = Input.mousePosition;
			AmountText.text = _maxAmount.ToString();
			Dropped = false;
			gameObject.SetActive(true);
		}
	}

	[HideInInspector] public int InventoryID;
	[HideInInspector] public int Idx;
	[HideInInspector] public bool Dropped = false;

	public Text AmountText;


	public void DragEnd() {
		_amountClass = null;
		Dropped = false;
		gameObject.SetActive(false);
	}
	// Use this for initialization
	void Start() {
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		transform.position = Input.mousePosition;
		if (_item == null) return;

		if (Input.mouseScrollDelta != Vector2.zero) {
			_item.amount = Mathf.Clamp(_item.amount + (int)Mathf.Sign(Input.mouseScrollDelta.y), 1, _maxAmount);
			AmountText.text = _item.amount.ToString();
		}
	}
}
