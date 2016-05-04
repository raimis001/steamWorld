using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class guiTemperature : MonoBehaviour {

	public Transform Arrow;
	public Text TemperatureText;

	[Range(0f, 1f)]
	public float Value;

	float MaxAngle = 36;
	float MaxTemperature = 50;

	// Use this for initialization
	void Start() {
		Value =  (ObjectManager.Temperature / MaxTemperature + 1f) / 2f;
		TemperatureText.text = ObjectManager.Temperature.ToString("0") + "°";
	}

	// Update is called once per frame
	void Update() {
		//if (Application.isEditor) {
			ObjectManager.Temperature = MaxTemperature * (Value * 2f - 1f);
		//} else {
		//	Value = (ObjectManager.Temperature / MaxTemperature + 1f) / 2f;
		//}

		float angle = MaxAngle * (1f - Value * 2f);// MaxAngle - Value * MaxAngle * 2f;

		Arrow.localEulerAngles = new Vector3(0, 0, angle);

		TemperatureText.text = ObjectManager.Temperature.ToString("0") + "°";
	}
}
