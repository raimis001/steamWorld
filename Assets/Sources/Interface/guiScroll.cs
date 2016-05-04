using UnityEngine;
using System.Collections;

public class guiScroll : MonoBehaviour {

	public GameObject Prefab;
	public Transform Content;

	// Use this for initialization
	void Start() {
		//Clear();
	}

	// Update is called once per frame
	void Update() {

	}

	public void Clear() {
		while (Content.childCount > 0) {
			DestroyImmediate(Content.GetChild(0).gameObject);
		}

		RectTransform rect = Content.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
		Content.localPosition = Vector3.zero;
	}

	public GameObject AddItem(GameObject prefab = null) {
		if (prefab == null) prefab = Prefab;

		GameObject obj = Instantiate(prefab);
		obj.transform.SetParent(Content);
		obj.transform.localScale = Vector3.one;

		RectTransform rect = Content.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(rect.sizeDelta.x + obj.GetComponent<RectTransform>().sizeDelta.x + 2, rect.sizeDelta.y);
		rect.anchoredPosition = Vector2.zero;

		return obj;
	}
}
