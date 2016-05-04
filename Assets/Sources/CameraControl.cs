using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour {

	public float delta = -15.4f;
	public float minFOV = 3f;
	public float maxFOV = 30f;

	float _fow;

	// Use this for initialization
	void Start () {
		_fow = Camera.main.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z + delta);

		if (EventSystem.current && EventSystem.current.IsPointerOverGameObject()) {
			return;
		}

		if (Camera.main.fieldOfView >= minFOV && Camera.main.fieldOfView <= maxFOV) {
			if (Input.mouseScrollDelta.y != 0) {
				_fow -= Input.mouseScrollDelta.y;
			}
		}
		if (Mathf.Abs(_fow - Camera.main.fieldOfView) > 0.1f) {
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _fow, 0.1f);
		}
		if (Camera.main.fieldOfView < minFOV || Camera.main.fieldOfView > maxFOV) {
			Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minFOV, maxFOV);
			_fow = Camera.main.fieldOfView;
		}

	}
}
