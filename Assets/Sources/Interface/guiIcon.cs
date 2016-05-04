using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class guiIcon : MonoBehaviour {

	ItemClass _item;
	public ItemClass Item {
		get { return _item; }
		set {
			_item = value;
			if (_item == null || _item.id < 0) {
				GetComponent<Image>().enabled = false;
				AmountPanel.SetActive(false);
				return;
			}

			GetComponent<Image>().sprite = _item.item.Icon;
			if (!AmountText) return;
			if (_item.amount < 0) {
				AmountPanel.SetActive(false);
				return;
			}

			AmountText.text = _item.amount.ToString() + (_item.need > 0 ? "/" + _item.need.ToString() : "");

			GetComponent<Image>().enabled = true;
			AmountPanel.SetActive(true);
		}
	}

	public Text AmountText;
	public Text HandText;
	public GameObject AmountPanel;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void MouseClick() {
		MainGUI gui = GameObject.FindObjectOfType<MainGUI>();
		gui.PressIcon(Item);
	}

}
